Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class ModPackServerCreateDialog
    Dim mapChooser As CreateMap
    Friend server As Server = Server.CreateServer
    Friend serverOptions As IServerOptions
    Dim ipType As ServerIPType = ServerIPType.Default
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        IPBox.Text = ""
        IPBox.ReadOnly = True
        ipType = ServerIPType.Float
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        IPBox.Text = GlobalModule.Manager.ip
        IPBox.ReadOnly = True
        ipType = ServerIPType.Default
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        IPBox.ReadOnly = False
        ipType = ServerIPType.Custom
    End Sub


    Private Sub Version_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VersionBox.SelectedIndexChanged, VersionTypeBox.SelectedIndexChanged
        If sender Is VersionBox Then
            Select Case server.ServerVersionType
                Case Server.EServerVersionType.Forge
                    server.SetVersion(VersionBox.Text, ForgeVersionDict(New Version(VersionBox.Text)).ToString)
                Case Server.EServerVersionType.SpongeVanilla
                    Dim v = SpongeVanillaVersionList(VersionBox.Text)
                    server.SetVersion(VersionBox.Text, v.SpongeVersion.Major & "." & v.SpongeVersion.Minor & "." & v.SpongeVersion.Build, v.Build, v.SpongeVersionType)
                Case Server.EServerVersionType.Paper
                    server.SetVersion(VersionBox.Text)
                Case Server.EServerVersionType.Akarin
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
                Case Server.EServerVersionType.Nukkit
                    server.SetVersion("1.0", NukkitVersion)
                Case Server.EServerVersionType.VanillaBedrock
                    server.SetVersion(VanillaBedrockVersion.ToString)
                Case Server.EServerVersionType.Vanilla
                    Dim preReleaseRegex As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]*-[Pp]{1}re[0-9]{1,2}")
                    Dim snapshotRegex As New Regex("[0-9]{2}w[0-9]{2}[a-z]{1}")
                    If Version.TryParse(VersionBox.Text, Nothing) Then
                        server.SetVersion(VersionBox.Text)
                    ElseIf preReleaseRegex.IsMatch(VersionBox.Text) Then
                        If preReleaseRegex.Match(VersionBox.Text).Value.Contains("1.RV") Then
                            server.SetVersion("1.9.9999", preReleaseRegex.Match(VersionBox.Text).Value)
                        Else
                            server.SetVersion(New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(VersionBox.Text).Value, preReleaseRegex.Match(VersionBox.Text).Value)
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
        If sender Is VersionTypeBox Then
            VersionBox.Items.Clear()
            VersionBox.Enabled = True
            Select Case VersionTypeBox.SelectedIndex
                Case 0
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.Vanilla)
                    For Each item In VanillaVersionDict.Keys
                        Dim preReleaseRegex As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]*-[Pp]{1}re[0-9]{1,2}")
                        Dim snapshotRegex As New Regex("[0-9]{2}w[0-9]{2}[a-z]{1}")
                        If preReleaseRegex.IsMatch(item) OrElse
                        snapshotRegex.IsMatch(item) Then
                            If My.Settings.ShowSnapshot Then VersionBox.Items.Add(item)
                        Else
                            VersionBox.Items.Add(item)
                        End If
                    Next
                Case 1
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.Forge)
                    Dim keys = ForgeVersionDict.Keys.ToList
                    keys.Sort()
                    keys.Reverse()
                    For Each version In keys
                        VersionBox.Items.Add(version.ToString)
                    Next
                Case 2
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.Spigot)
                    VersionBox.Items.AddRange(SpigotVersionDict.Keys.ToArray)
                Case 3
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.Spigot_Git)
                    VersionBox.Items.AddRange(SpigotGitVersionList.ToArray)
                Case 4
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.CraftBukkit)
                    VersionBox.Items.AddRange(CraftBukkitVersionDict.Keys.ToArray)
                Case 5
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.Paper)
                    Dim keys = PaperVersionDict.Keys.ToList
                    keys.Sort()
                    keys.Reverse()
                    For Each version In keys
                        VersionBox.Items.Add(version.ToString)
                    Next
                Case 6
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.Akarin)
                    For Each item In AkarinVersionList
                        Dim buildVer As String = item.Build.ToString
                        If buildVer = "-1" Then
                            buildVer = "x"
                        End If
                        If item.Major = 100 And item.Minor = 100 Then
                            VersionBox.Items.Add("最新建置版本")
                        Else
                            VersionBox.Items.Add(String.Format("{0}.{1}.{2}", item.Major, item.Minor, buildVer))
                        End If
                    Next
                Case 7
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.SpongeVanilla)
                    Dim l = SpongeVanillaVersionList.Keys.ToList
                    l.Reverse()
                    VersionBox.Items.AddRange(l.ToArray)
                Case 8
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.Cauldron)
                    VersionBox.Items.AddRange({"1.7.10", "1.7.2", "1.6.4", "1.5.2"})
                Case 9
                    server.SetVersionType(Server.EServerType.Java, Server.EServerVersionType.Thermos)
                    VersionBox.Items.Add("1.7.10")
                    VersionBox.SelectedIndex = 0
                    VersionBox.Enabled = False
                Case 10
                    If Environment.OSVersion.Version.Major < 10 Then
                        server.SetVersionType(Server.EServerType.Bedrock, Server.EServerVersionType.Nukkit)
                        VersionBox.Items.Add(String.Format("最新版 ({0})", NukkitVersion))
                    Else
                        server.SetVersionType(Server.EServerType.Bedrock, Server.EServerVersionType.VanillaBedrock)
                        VersionBox.Items.Add(String.Format("最新版 ({0})", VanillaBedrockVersion.ToString))
                    End If
                Case 11
                    server.SetVersionType(Server.EServerType.Bedrock, Server.EServerVersionType.Nukkit)
                    VersionBox.Items.Add(String.Format("最新版 ({0})", NukkitVersion))
                Case Else
                    MsgBox("非法操作!")
                    server.SetVersionType(Server.EServerType.Error, Server.EServerVersionType.Error)
                    VersionTypeBox.SelectedIndex = -1
            End Select

        End If

    End Sub


    Private Sub ServerCreateDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Environment.OSVersion.Version.Major < 10 Then VersionTypeBox.Items.RemoveAt(7)
    End Sub

    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
        If ServerDirBox.Text.Trim <> "" Then
            If (ipType <> ServerIPType.Custom) OrElse
        (ipType = ServerIPType.Custom AndAlso
        (IPBox.Text.Trim <> "" AndAlso IsNumeric(IPBox.Text.Replace(".", "")))) Then
                If VersionTypeBox.SelectedIndex <> -1 And VersionBox.SelectedIndex <> -1 Then
                    server.SetPath(ServerDirBox.Text)
                    If serverOptions Is Nothing Then
                        Select Case server.ServerType
                            Case Server.EServerType.Java
                                serverOptions = New JavaServerOptions
                                serverOptions.InputOption(server.ServerOptions)
                            Case Server.EServerType.Bedrock
                                serverOptions = New NukkitServerOptions
                                serverOptions.InputOption(server.ServerOptions)
                        End Select
                    End If
                    server.ServerOptions = serverOptions.OutputOption
                    Select Case ipType
                        Case ServerIPType.Float
                            Select Case server.ServerType
                                Case Server.EServerType.Java
                                    server.ServerOptions("server-ip") = ""
                                Case Server.EServerType.Bedrock
                                    server.ServerOptions("server-ip") = "0.0.0.0"
                            End Select
                        Case ServerIPType.Default
                            server.ServerOptions("server-ip") = GlobalModule.Manager.ip
                        Case ServerIPType.Custom
                            server.ServerOptions("server-ip") = IPBox.Text
                    End Select
                    Dim helper As New ServerCreateHelper(server, ServerDirBox.Text)
                    helper.Show()
                    Close()
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
End Class

