Public Class ServerConsole
    Dim _server As ServerBase
    Dim hub As New ProcessMessageHub()
    Public Sub New(server As ServerBase)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("類型", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 50))
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("時間", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 50))
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("訊息", Color.Black, New Font(DxListView1.Font.FontFamily, 14), 400))
        _server = server
    End Sub

    Private Sub ServerConsole_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If _server.BeforeRunServer() Then
            Dim process As Process = _server.RunServer()
            process.EnableRaisingEvents = True
            If process.HasExited Then
                _server.IsRunning = False
                _server.ProcessID = 0
            Else
                AddHandler process.OutputDataReceived, Sub(obj, args) hub.AddMessage(args.Data)
                AddHandler process.ErrorDataReceived, Sub(obj, args) hub.AddMessage(args.Data)
                AddHandler process.Exited, Sub()
                                               _server.IsRunning = False
                                               _server.ProcessID = 0
                                           End Sub
                process.BeginOutputReadLine()
                process.BeginErrorReadLine()
                AddHandler hub.MessageProcessed, Sub(msg)
                                                     For Each item In msg
                                                         If item Is Nothing Then Continue For
                                                         Dim typeString As String = ""
                                                         Select Case item.MessageType
                                                             Case MinecraftProcessMessage.ProcessMessageType.Info
                                                                 typeString = "訊息"
                                                             Case MinecraftProcessMessage.ProcessMessageType.Warning
                                                                 typeString = "警告"
                                                             Case MinecraftProcessMessage.ProcessMessageType.Error
                                                                 typeString = "錯誤"
                                                             Case Else
                                                                 typeString = "訊息"
                                                         End Select
                                                         DxListView1.Items.Add(New DXListView.DXListViewItem(New DXListView.DXListViewSubItem(typeString), New DXListView.DXListViewSubItem(item.Time.Minute.ToString.PadLeft(2, "0") & ":" & item.Time.Second.ToString.PadLeft(2, "0")), New DXListView.DXListViewSubItem(item.Message)))
                                                     Next
                                                 End Sub
            End If
        End If
    End Sub
    Private Sub ServerConsole_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Y < 30 OrElse e.X = Width - Margin.Right Then
            WinAPI.MoveForm(Handle)
        End If
    End Sub

    Private Sub ServerConsole_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Dim t As New Threading.Thread(Sub()
                                          Try
                                              _server.ShutdownServer()
                                          Catch ex As Exception

                                          End Try
                                      End Sub)
        t.IsBackground = False
        t.Start()
        hub.Dispose()
        GC.Collect()
    End Sub
End Class