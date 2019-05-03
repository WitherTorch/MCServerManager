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
                If ListView1.SelectedIndices.Count = 1 Then
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
                    Dim helper As New ModPackServerCreateHelper(server, ServerDirBox.Text, FeedTheBeastPackDict.Values.ToArray(ListView1.SelectedIndices(0)).Item3, FeedTheBeastPackDict.Values.ToArray(ListView1.SelectedIndices(0)).Item2)
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ListView1.Clear()
        Select Case ComboBox1.SelectedIndex
            Case 0
                For Each key In FeedTheBeastPackDict.Keys
                    ListView1.Items.Add(key)
                Next
        End Select
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        ListBox1.Items.Clear()
        If ListView1.SelectedIndices.Count > 0 Then
            Select Case ComboBox1.SelectedIndex
                Case 0
                    For Each pair In FeedTheBeastPackDict.Values.ToArray(ListView1.SelectedIndices(0)).Item1
                        ListBox1.Items.Add(String.Format("{0} (Minecraft {1})", pair.Key, IIf(pair.Value.Contains(";"), pair.Value.Split(";")(0), pair.Value)))
                    Next
            End Select
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex > -1 Then
            Select Case ComboBox1.SelectedIndex
                Case 0 ' Feed The Beast
                    server.SetPackInfo(FeedTheBeastPackDict.Keys.ToArray(ListView1.SelectedIndices(0)), FeedTheBeastPackDict.Values.ToArray(ListView1.SelectedIndices(0)).Item1.Keys.ToArray(ListBox1.SelectedIndex), ModPackServer.ModPackType.FeedTheBeast)
            End Select
        End If
    End Sub
End Class

