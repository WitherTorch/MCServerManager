Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class ModPackServerCreateDialog
    Friend server As ModPackServer = ModPackServer.CreateServer
    Friend serverOptions As IServerOptions
    Dim ipType As ServerIPType = ServerIPType.Default
    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        IPBox.Text = ""
        IPBox.ReadOnly = True
        ipType = ServerIPType.Float
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        IPBox.Text = GlobalModule.Manager.ip(0)
        IPBox.ReadOnly = True
        ipType = ServerIPType.Default
    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        IPBox.ReadOnly = False
        ipType = ServerIPType.Custom
    End Sub



    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click
        If ServerDirBox.Text.Trim <> "" Then
            If (ipType <> ServerIPType.Custom) OrElse
        (ipType = ServerIPType.Custom AndAlso
        (IPBox.Text.Trim <> "" AndAlso IsNumeric(IPBox.Text.Replace(".", "")))) Then
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
                        server.ServerOptions("server-ip") = GlobalModule.Manager.ip(0)
                    Case ServerIPType.Custom
                        server.ServerOptions("server-ip") = IPBox.Text
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
End Class

