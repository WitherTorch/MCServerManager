Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class ServerCreateDialog
    Dim mapChooser As CreateMap
    Friend server As ServerBase
    Friend serverOptions As IServerProperties
    Dim ipType As ServerIPType = ServerIPType.Default
    Private Sub Version_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VersionBox.SelectedIndexChanged, VersionTypeBox.SelectedIndexChanged
        MapPanel.Enabled = (VersionBox.SelectedIndex <> -1 And VersionTypeBox.SelectedIndex <> -1 And ServerDirBox.Text.Trim <> "")
        If sender Is VersionBox Then
            Select Case server.ServerVersionType
                Case ServerBase.EServerVersionType.Forge
                    server.SetVersion(VersionBox.Text, ForgeVersionDict(New Version(VersionBox.Text)).ToString)
                Case ServerBase.EServerVersionType.SpongeVanilla
                    Dim v = SpongeVanillaVersionList(VersionBox.Text)
                    server.SetVersion(VersionBox.Text, v.SpongeVersion.Major & "." & v.SpongeVersion.Minor & "." & v.SpongeVersion.Build, v.Build, v.SpongeVersionType)
                Case ServerBase.EServerVersionType.Paper
                    server.SetVersion(VersionBox.Text)
                Case ServerBase.EServerVersionType.Akarin
                    If VersionBox.Text = "最新建置版本" Then
                        server.SetVersion("master")
                    Else
                        Dim splitedStrings = VersionBox.Text.Split(".")
                        If splitedStrings.Last() = "x" Then
                            server.SetVersion(String.Format("{0}.{1}", splitedStrings(0), splitedStrings(1)))
                        Else
                            server.SetVersion(String.Format("{0}.{1}.{2}", splitedStrings(0), splitedStrings(1), splitedStrings(2)))
                        End If
                    End If
                Case ServerBase.EServerVersionType.Nukkit
                    server.SetVersion("1.0", NukkitVersion)
                Case ServerBase.EServerVersionType.VanillaBedrock
                    server.SetVersion(VanillaBedrockVersion.ToString)
                Case ServerBase.EServerVersionType.Vanilla
                    Dim preReleaseRegex1 As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]*-[Pp]{1}re[0-9]{1,2}")
                    Dim preReleaseRegex2 As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]* [Pp]{1}re-[Rr]{1}elease [0-9]{1,2}")
                    Dim snapshotRegex As New Regex("[0-9]{2}w[0-9]{2}[a-z]{1}")
                    If Version.TryParse(VersionBox.Text, Nothing) Then
                        server.SetVersion(VersionBox.Text)
                    ElseIf preReleaseRegex1.IsMatch(VersionBox.Text) Then
                        If preReleaseRegex1.Match(VersionBox.Text).Value.Contains("1.RV") Then
                            server.SetVersion("1.9.9999", preReleaseRegex1.Match(VersionBox.Text).Value)
                        Else
                            server.SetVersion(New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(VersionBox.Text).Value, preReleaseRegex1.Match(VersionBox.Text).Value)
                        End If
                    ElseIf preReleaseRegex2.IsMatch(VersionBox.Text) Then
                        If preReleaseRegex2.Match(VersionBox.Text).Value.Contains("1.RV") Then
                            server.SetVersion("1.9.9999", preReleaseRegex2.Match(VersionBox.Text).Value)
                        Else
                            server.SetVersion(New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(VersionBox.Text).Value, preReleaseRegex2.Match(VersionBox.Text).Value)
                        End If
                    ElseIf snapshotRegex.IsMatch(VersionBox.Text) Then
                        server.SetVersion("snapshot", snapshotRegex.Match(VersionBox.Text).Value)
                    Else
                        If VersionBox.Text <> "" Then
                            MsgBox("非法版本!",, Application.ProductName)
                            VersionBox.SelectedIndex = -1
                        End If

                    End If
                Case Else
                    server.SetVersion(VersionBox.Text)
            End Select
        End If
        Static _typeSelectedIndex As Integer = -1
        If sender Is VersionTypeBox Then
            VersionBox.Items.Clear()
            VersionBox.Enabled = True
            VersionBox.Items.AddRange(VersionListLoader.GetVersions(VersionTypeBox.SelectedIndex))
            _typeSelectedIndex = VersionTypeBox.SelectedIndex
        End If

    End Sub

    Private Sub ServerDirBox_TextChanged(sender As Object, e As EventArgs) Handles ServerDirBox.TextChanged
        server.SetPath(ServerDirBox.Text)
        MapPanel.Enabled = (VersionBox.SelectedIndex <> -1 And VersionTypeBox.SelectedIndex <> -1 And ServerDirBox.Text.Trim <> "")
    End Sub
    Private Sub ServerCreateDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        IPAddressComboBox.Items.AddRange(GlobalModule.Manager.ip.ToArray)
        ipType = ServerIPType.Float
        IPStyleComboBox.SelectedIndex = 0
        Dim groupedItems = {New With {.Group = "Java 版",
    .Value = 0,
    .Display = "原版"
}, New With {
    .Group = "Java 版",
    .Value = 1,
    .Display = "Forge"
}, New With {
    .Group = "Java 版",
    .Value = 2,
    .Display = "Spigot"
}, New With {
    .Group = "Java 版",
    .Value = 3,
    .Display = "Spigot (手動建置)"
}, New With {
    .Group = "Java 版",
    .Value = 4,
    .Display = "CraftBukkit"
}, New With {
    .Group = "Java 版",
    .Value = 5,
    .Display = "Paper"
}, New With {
    .Group = "Java 版",
    .Value = 6,
    .Display = "Akarin"
}, New With {
    .Group = "Java 版",
    .Value = 7,
    .Display = "SpongeVanilla"
}, New With {
    .Group = "Java 版",
    .Value = 8,
    .Display = "MCPC/Cauldron"
}, New With {
    .Group = "Java 版",
    .Value = 9,
    .Display = "Thermos"
}, New With {
    .Group = "Java 版",
    .Value = 10,
    .Display = "Contigo"
}, New With {
    .Group = "Java 版",
    .Value = 11,
    .Display = "Kettle"
}, New With {
    .Group = "基岩版",
    .Value = 12,
    .Display = "原版"
 }, New With {
    .Group = "基岩版",
    .Value = 13,
    .Display = "PocketMine-MP"
}, New With {
    .Group = "基岩版",
    .Value = 14,
    .Display = "NukkitX"
}, New With {
    .Group = "其他",
    .Value = 255,
    .Display = "自定義"
}}
        If IsUnixLikeSystem Or System.Environment.OSVersion.Version.Major < 10 Then
            For Each item In groupedItems
                If item.Value = 12 Then
                    Dim list = groupedItems.ToList
                    list.Remove(item)
                    groupedItems = list.ToArray
                End If
            Next
        End If
        VersionTypeBox.GroupMember = "Group"
        VersionTypeBox.ValueMember = "Value"
        VersionTypeBox.DisplayMember = "Display"
        VersionTypeBox.DataSource = groupedItems

        MapPanel.Enabled = (VersionBox.SelectedIndex <> -1 And VersionTypeBox.SelectedIndex <> -1 And ServerDirBox.Text.Trim <> "")
        MapPanel.Controls.Add(New MapView(server) With {.Dock = DockStyle.Fill})
    End Sub
    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
        If ServerDirBox.Text.Trim <> "" Then
            If (ipType <> ServerIPType.Custom) OrElse
            (ipType = ServerIPType.Custom AndAlso
            (IPAddressComboBox.Text.Trim <> "" AndAlso IsNumeric(IPAddressComboBox.Text.Replace(".", "")))) Then
                If VersionTypeBox.SelectedIndex <> -1 And VersionBox.SelectedIndex <> -1 Then
                    Dim mapView As MapView = MapPanel.Controls(0)
                    If mapView.MapNameLabel.Text <> "" Then
                        server.ServerPath = ServerDirBox.Text
                        If serverOptions Is Nothing Then
                            Select Case server.ServerType
                                Case ServerBase.EServerType.Java
                                    serverOptions = New JavaServerOptions
                                    serverOptions.InputOption(server.ServerOptions)
                                Case ServerBase.EServerType.Bedrock
                                    Select Case server.ServerVersionType
                                        Case ServerBase.EServerVersionType.Nukkit
                                            serverOptions = New NukkitServerOptions
                                            serverOptions.InputOption(server.ServerOptions)
                                        Case ServerBase.EServerVersionType.PocketMine
                                            serverOptions = New PocketMineServerOptions
                                            serverOptions.InputOption(server.ServerOptions)
                                        Case ServerBase.EServerVersionType.VanillaBedrock
                                            serverOptions = New BDSServerOptions
                                            serverOptions.InputOption(server.ServerOptions)
                                    End Select
                                Case ServerBase.EServerType.Custom
                                    serverOptions = New JavaServerOptions
                                    serverOptions.InputOption(server.ServerOptions)
                            End Select
                        End If
                        If serverOptions IsNot Nothing Then
                            server.ServerOptions = serverOptions
                        End If
                        Select Case ipType
                            Case ServerIPType.Float
                                Select Case server.ServerType
                                    Case ServerBase.EServerType.Java
                                        server.ServerOptions("server-ip") = ""
                                    Case ServerBase.EServerType.Bedrock
                                        server.ServerOptions("server-ip") = "0.0.0.0"
                                End Select
                            Case ServerIPType.Default
                                server.ServerOptions("server-ip") = GlobalModule.Manager.ip(IPAddressComboBox.SelectedIndex)
                            Case ServerIPType.Custom
                                server.ServerOptions("server-ip") = IPAddressComboBox.Text
                        End Select
                        Dim helper As New ServerCreateHelper(server, ServerDirBox.Text)
                        helper.Show()
                        Close()
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub PortBox_ValueChanged(sender As Object, e As EventArgs) Handles PortBox.ValueChanged
        Try
            serverOptions.SetValue("server-port", PortBox.Value)
        Catch ex As NullReferenceException
            If server.ServerOptions.ContainsKey("server-port") Then
                server.ServerOptions("server-port") = PortBox.Value
            Else
                server.ServerOptions.Add("server-port", PortBox.Value)
            End If
        End Try
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        On Error Resume Next
        Select Case TabControl1.SelectedIndex
            Case 0
                server.ServerOptions = serverOptions.OutputOption
                AdvancedPropertyGrid.SelectedObject = Nothing
            Case 1
                Select Case server.ServerType
                    Case ServerBase.EServerType.Java
                        serverOptions = New JavaServerOptions
                        serverOptions.InputOption(server.ServerOptions)
                        AdvancedPropertyGrid.SelectedObject = serverOptions
                    Case ServerBase.EServerType.Bedrock
                        Select Case server.ServerVersionType
                            Case ServerBase.EServerVersionType.Nukkit
                                serverOptions = New NukkitServerOptions
                                serverOptions.InputOption(server.ServerOptions)
                                AdvancedPropertyGrid.SelectedObject = serverOptions
                            Case ServerBase.EServerVersionType.PocketMine
                                serverOptions = New PocketMineServerOptions
                                serverOptions.InputOption(server.ServerOptions)
                                AdvancedPropertyGrid.SelectedObject = serverOptions
                            Case ServerBase.EServerVersionType.VanillaBedrock
                                serverOptions = New BDSServerOptions
                                serverOptions.InputOption(server.ServerOptions)
                                AdvancedPropertyGrid.SelectedObject = serverOptions
                        End Select
                    Case ServerBase.EServerType.Custom
                        serverOptions = New JavaServerOptions
                        serverOptions.InputOption(server.ServerOptions)
                        AdvancedPropertyGrid.SelectedObject = serverOptions
                End Select
        End Select
    End Sub

    Private Sub ServerDirBrowseBtn_Click(sender As Object, e As EventArgs) Handles ServerDirBrowseBtn.Click
        Static dir As New FolderBrowserDialog
        dir = New FolderBrowserDialog
        dir.ShowNewFolderButton = True
        dir.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        dir.Description = "選擇建立伺服器的資料夾位置"
        If dir.ShowDialog = DialogResult.OK Then
            ServerDirBox.Text = dir.SelectedPath
        End If
    End Sub


    Private Sub 將連接埠設成預設值ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 將連接埠設成預設值ToolStripMenuItem.Click
        Select Case VersionTypeBox.SelectedIndex
            Case 0 To 9
                PortBox.Value = 25565
            Case 10 To 11
                PortBox.Value = 19132
        End Select
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub IPStyleComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles IPStyleComboBox.SelectedIndexChanged
        Select Case IPStyleComboBox.SelectedIndex
            Case 0
                Label5.Enabled = False
                IPAddressComboBox.Enabled = False
                ipType = ServerIPType.Float
            Case 1
                Label5.Enabled = True
                IPAddressComboBox.Enabled = True
                IPAddressComboBox.DropDownStyle = ComboBoxStyle.DropDownList
                ipType = ServerIPType.Default
            Case 2
                Label5.Enabled = True
                IPAddressComboBox.Enabled = True
                IPAddressComboBox.DropDownStyle = ComboBoxStyle.DropDown
                ipType = ServerIPType.Custom
            Case Else
                Label5.Enabled = False
                IPAddressComboBox.Enabled = False
        End Select
    End Sub
End Class
Enum ServerIPType
    Float
    [Default]
    Custom
End Enum
