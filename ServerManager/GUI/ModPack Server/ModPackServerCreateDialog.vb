Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class ModPackServerCreateDialog
    Friend server As ModPackServer = ModPackServer.CreateServer
    Friend serverOptions As IServerOptions
    Dim ipType As ServerIPType = ServerIPType.Default




    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
        If ServerDirBox.Text.Trim <> "" Then
            If (ipType <> ServerIPType.Custom) OrElse
        (ipType = ServerIPType.Custom AndAlso
        (IPAddressComboBox.Text.Trim <> "" AndAlso IsNumeric(IPAddressComboBox.Text.Replace(".", "")))) Then
                server.SetPath(ServerDirBox.Text)
                If serverOptions Is Nothing Then
                    serverOptions = New JavaServerOptions
                    serverOptions.InputOption(server.ServerOptions)
                End If
                server.ServerOptions = serverOptions.OutputOption
                Select Case ipType
                    Case ServerIPType.Float
                        server.ServerOptions("server-ip") = ""
                    Case ServerIPType.Default
                        server.ServerOptions("server-ip") = GlobalModule.Manager.ip(IPAddressComboBox.SelectedIndex)
                    Case ServerIPType.Custom
                        server.ServerOptions("server-ip") = IPAddressComboBox.Text
                End Select
                Dim modpackExplorer As New ModpackExplorer(server)
                modpackExplorer.Show()
                Close()
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

    Private Sub ModPackServerCreateDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IPAddressComboBox.Items.AddRange(GlobalModule.Manager.ip.ToArray)
        ipType = ServerIPType.Float
        IPStyleComboBox.SelectedIndex = 0
    End Sub
End Class

