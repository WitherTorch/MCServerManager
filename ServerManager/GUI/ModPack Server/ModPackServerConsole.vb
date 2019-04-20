Imports System.ComponentModel
Imports System.Threading
Imports System.Threading.Tasks
Imports ServerManager.MinecraftLogParser.MinecraftConsoleMessage
Public Class ModPackServerConsole
    Dim InputList As New List(Of String)()
    Dim CurrentListLocation As Integer = -1
    Dim backgroundProcess As Process
    Public ReadOnly Property Server As ModPackServer
    Dim startInfo As ProcessStartInfo
    Public Sub New(Server As ModPackServer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = Server
        Text = "伺服器控制台 - " & Server.ServerPathName
    End Sub



    Private Sub StopLoadingCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles StopLoadingCheckBox.CheckedChanged
        Select Case StopLoadingCheckBox.Checked = False
            Case True
                backgroundProcess.BeginErrorReadLine()
                backgroundProcess.BeginOutputReadLine()
            Case False
                backgroundProcess.CancelErrorRead()
                backgroundProcess.CancelOutputRead()
        End Select
    End Sub
    Friend Sub InputToConsole(command As String)
        backgroundProcess.StandardInput.WriteLine(command)
    End Sub
    Private Sub ServerConsole_Load(sender As Object, e As EventArgs) Handles Me.Load
        BeginInvoke(New Action(Sub()
                                   Select Case My.Settings.ConsoleInputChat
                                       Case True
                                           ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式")
                                       Case False
                                           ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式")
                                   End Select
                               End Sub))
        ConnectUPnP()
        TaskTimer.Enabled = True
        TaskTimer.Start()
        Run()
    End Sub
    Private Overloads Sub Run()
        Select Case Server.PackType
            Case ModPackServer.ModPackType.FeedTheBeast
                Run(IO.Path.Combine(JavaPath, "java.exe"), String.Format("-server -Xmx{0}M -Xms{1}M {2} -jar {3}", ServerMemoryMax, ServerMemoryMin, Server.InternalJavaArguments & JavaArguments, IIf(Server.ServerPath.EndsWith("\"), Server.ServerPath, Server.ServerPath & "\") & Server.ServerRunJAR), Server.ServerPath, True, True)
        End Select
    End Sub
    Private Overloads Sub Run(program As String, serverDir As String)
        Run(program, "", serverDir, True, False)
    End Sub
    Private Overloads Sub Run(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8 As Boolean = False)
        backgroundProcess = Process.Start(PrepareStartInfo(program, args, serverDir, nogui, UTF8))
        RestartButton.Enabled = False
        ForceCloseButton.Enabled = True
        If nogui Then
            If StopLoadingCheckBox.Checked = False Then
                backgroundProcess.BeginErrorReadLine()
                backgroundProcess.BeginOutputReadLine()
            Else
                backgroundProcess.CancelErrorRead()
                backgroundProcess.CancelOutputRead()
            End If
            AddHandler backgroundProcess.ErrorDataReceived, Sub(sender, e)
                                                                Try
                                                                    If IsNothing(backgroundProcess) = False And backgroundProcess.HasExited = False Then
                                                                        If IsNothing(e.Data) = False Then
                                                                            Task.Run(Sub()
                                                                                         Try
                                                                                             Dim item As New ListViewItem("錯誤")
                                                                                             item.ForeColor = Color.Red
                                                                                             item.SubItems.Add("")
                                                                                             item.SubItems.Add(e.Data)
                                                                                             Dim nowTime = Now
                                                                                             item.SubItems.Add(String.Format("{0}:{1}:{2}", nowTime.Hour.ToString.PadLeft(2, "0"), nowTime.Minute.ToString.PadLeft(2, "0"), nowTime.Second.ToString.PadLeft(2, "0")))
                                                                                             If NotifyChooseListBox.CheckedIndices.Contains(3) Then _
                                                                                                                    NotifyInfoMessage("伺服器發出錯誤訊息:" & vbNewLine & e.Data, Text)
                                                                                             If InvokeRequired Then
                                                                                                 BeginInvoke(Sub()
                                                                                                                 Dim seekToBottom As Boolean = False
                                                                                                                 If DataListView.Items.Count > 1 Then
                                                                                                                     Dim first As Integer = DataListView.TopItem.Index
                                                                                                                     Dim h_tot As Integer = DataListView.ClientRectangle.Height - 1
                                                                                                                     Dim h_hdr As Integer = DataListView.GetItemRect(first).Y
                                                                                                                     Dim h_item As Integer = DataListView.GetItemRect(0).Height
                                                                                                                     Dim cntVis As Integer = (h_tot - h_hdr) / h_item
                                                                                                                     Dim LastItemIndex = Math.Min(DataListView.Items.Count - 1, first + cntVis)
                                                                                                                     If LastItemIndex = DataListView.Items.Count - 1 Then
                                                                                                                         seekToBottom = True
                                                                                                                     End If
                                                                                                                 End If
                                                                                                                 Invoke(Sub() DataListView.Items.Add(item))
                                                                                                                 If seekToBottom Then DataListView.EnsureVisible(item.Index)
                                                                                                             End Sub)
                                                                                             Else
                                                                                                 Dim seekToBottom As Boolean = False
                                                                                                 If DataListView.Items.Count > 1 Then
                                                                                                     Dim first As Integer = DataListView.TopItem.Index
                                                                                                     Dim h_tot As Integer = DataListView.ClientRectangle.Height - 1
                                                                                                     Dim h_hdr As Integer = DataListView.GetItemRect(first).Y
                                                                                                     Dim h_item As Integer = DataListView.GetItemRect(0).Height
                                                                                                     Dim cntVis As Integer = (h_tot - h_hdr) / h_item
                                                                                                     Dim LastItemIndex = Math.Min(DataListView.Items.Count - 1, first + cntVis)
                                                                                                     If LastItemIndex = DataListView.Items.Count - 1 Then
                                                                                                         seekToBottom = True
                                                                                                     End If
                                                                                                 End If
                                                                                                 DataListView.Items.Add(item)
                                                                                                 If seekToBottom Then DataListView.EnsureVisible(item.Index)
                                                                                             End If
                                                                                         Catch ex As Exception
                                                                                         End Try
                                                                                     End Sub)
                                                                        End If
                                                                    End If
                                                                Catch ex As Exception
                                                                End Try
                                                            End Sub
            AddHandler backgroundProcess.OutputDataReceived, Sub(sender, e)
                                                                 Try
                                                                     If IsNothing(backgroundProcess) = False And backgroundProcess.HasExited = False Then
                                                                         If IsNothing(e.Data) = False Then
                                                                             Task.Run(Sub()
                                                                                          Try
                                                                                              Dim msg = MinecraftLogParser.ToConsoleMessage(e.Data, Now)
                                                                                              Dim item As New ListViewItem(msg.ServerMessageTypeString)
                                                                                              item.SubItems.Add(msg.Thread)
                                                                                              item.SubItems.Add(msg.Message)
                                                                                              item.SubItems.Add(String.Format("{0}:{1}:{2}", msg.Time.Hour.ToString.PadLeft(2, "0"), msg.Time.Minute.ToString.PadLeft(2, "0"), msg.Time.Second.ToString.PadLeft(2, "0")))
                                                                                              Select Case msg.ServerMessageType
                                                                                                  Case MCServerMessageType.Warning
                                                                                                      item.ForeColor = Color.Orange
                                                                                                      If NotifyChooseListBox.CheckedIndices.Contains(2) Then _
                                                                                                                    NotifyInfoMessage("伺服器發出警告訊息:" & vbNewLine & msg.Message, Text)
                                                                                                  Case MCServerMessageType.Error
                                                                                                      item.ForeColor = Color.Red
                                                                                                      If NotifyChooseListBox.CheckedIndices.Contains(3) Then _
                                                                                                                    NotifyInfoMessage("伺服器發出錯誤訊息:" & vbNewLine & msg.Message, Text)
                                                                                              End Select
                                                                                              Select Case msg.MessageType
                                                                                                  Case MCMessageType.PlayerLogin
                                                                                                      If NotifyChooseListBox.CheckedIndices.Contains(0) Then _
                                                                                                                    NotifyInfoMessage(msg.AddtionalMessage("player") & " 進入伺服器", Text)
                                                                                                      BeginInvoke(Sub() PlayerListBox.Items.Add(msg.AddtionalMessage("player")))
                                                                                                  Case MCMessageType.PlayerLogout
                                                                                                      If NotifyChooseListBox.CheckedIndices.Contains(1) Then _
                                                                                                                    NotifyInfoMessage(msg.AddtionalMessage("player") & " 離開伺服器", Text)
                                                                                                      BeginInvoke(Sub() PlayerListBox.Items.Remove(msg.AddtionalMessage("player")))
                                                                                                  Case Else
                                                                                              End Select
                                                                                              If InvokeRequired Then
                                                                                                  BeginInvoke(Sub()
                                                                                                                  Dim seekToBottom As Boolean = False
                                                                                                                  If DataListView.Items.Count > 1 Then
                                                                                                                      Dim first As Integer = DataListView.TopItem.Index
                                                                                                                      Dim h_tot As Integer = DataListView.ClientRectangle.Height - 1
                                                                                                                      Dim h_hdr As Integer = DataListView.GetItemRect(first).Y
                                                                                                                      Dim h_item As Integer = DataListView.GetItemRect(0).Height
                                                                                                                      Dim cntVis As Integer = (h_tot - h_hdr) / h_item
                                                                                                                      Dim LastItemIndex = Math.Min(DataListView.Items.Count - 1, first + cntVis)
                                                                                                                      If LastItemIndex = DataListView.Items.Count - 1 Then
                                                                                                                          seekToBottom = True
                                                                                                                      End If
                                                                                                                  End If
                                                                                                                  Invoke(Sub() DataListView.Items.Add(item))
                                                                                                                  If seekToBottom Then DataListView.EnsureVisible(item.Index)
                                                                                                              End Sub)
                                                                                              Else
                                                                                                  Dim seekToBottom As Boolean = False
                                                                                                  If DataListView.Items.Count > 1 Then
                                                                                                      Dim first As Integer = DataListView.TopItem.Index
                                                                                                      Dim h_tot As Integer = DataListView.ClientRectangle.Height - 1
                                                                                                      Dim h_hdr As Integer = DataListView.GetItemRect(first).Y
                                                                                                      Dim h_item As Integer = DataListView.GetItemRect(0).Height
                                                                                                      Dim cntVis As Integer = (h_tot - h_hdr) / h_item
                                                                                                      Dim LastItemIndex = Math.Min(DataListView.Items.Count - 1, first + cntVis)
                                                                                                      If LastItemIndex = DataListView.Items.Count - 1 Then
                                                                                                          seekToBottom = True
                                                                                                      End If
                                                                                                  End If
                                                                                                  DataListView.Items.Add(item)
                                                                                                  If seekToBottom Then DataListView.EnsureVisible(item.Index)
                                                                                              End If
                                                                                          Catch ex As Exception
                                                                                          End Try
                                                                                      End Sub)
                                                                         End If
                                                                     End If
                                                                 Catch ex As Exception
                                                                 End Try
                                                             End Sub
        End If
        ServerStatusLabel.Text = "伺服器狀態：啟動"
        Server.IsRunning = True
        backgroundProcess.EnableRaisingEvents = True
        AddHandler backgroundProcess.Exited, Sub(sender, e)
                                                 If IsDisposed = False Then
                                                     BeginInvokeIfRequired(Me, Sub()
                                                                                   TaskTimer.Enabled = False
                                                                                   TaskTimer_Tick(TaskTimer, New EventArgs)
                                                                                   PlayerListBox.Items.Clear()
                                                                                   ServerStatusLabel.Text = "伺服器狀態：關閉"
                                                                                   RestartButton.Enabled = True
                                                                                   ForceCloseButton.Enabled = False
                                                                               End Sub)
                                                 End If
                                                 Server.IsRunning = False
                                                 backgroundProcess = Nothing
                                                 If IsDisposed = False Then
                                                     If CloseCheckBox.Checked Then
                                                         Close()
                                                     End If
                                                 End If
                                                 Console.WriteLine("Process Exited")
                                                 Server.ProcessID = 0
                                             End Sub
        If backgroundProcess IsNot Nothing Then Server.ProcessID = backgroundProcess.Id
    End Sub
    Private Overloads Function PrepareStartInfo(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8Encoding As Boolean = False) As ProcessStartInfo
        If IsNothing(startInfo) Then
            Dim processInfo As ProcessStartInfo
            If args = "" Then
                processInfo = New ProcessStartInfo(program)
            Else
                If UTF8Encoding Then args = "-Dfile.encoding=""UTF-8"" " & args
                ' processInfo = New ProcessStartInfo("cmd.exe", "/c chcp 65001 && " & """" & program & """ " & args)
                processInfo = New ProcessStartInfo(program, args)
            End If
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = nogui
            If UTF8Encoding And nogui Then
                processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
                processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
            End If
            If nogui = False Then
                MainTabControl.TabPages.Remove(DataTabPage)
            End If
            processInfo.RedirectStandardOutput = nogui
            processInfo.RedirectStandardError = nogui
            processInfo.RedirectStandardInput = nogui
            processInfo.WorkingDirectory = serverDir
            startInfo = processInfo
            Return processInfo
        Else
            Return startInfo
        End If
    End Function

    Private Sub CommandTextBox_KeyDown(sender As Object, e As KeyEventArgs) Handles CommandTextBox.KeyDown

        Static upNumber As Integer = 0
        Select Case e.KeyCode
            Case Keys.Enter
                e.SuppressKeyPress = True
                If CommandTextBox.Text.Trim <> "" Then
                    Try
                        CurrentListLocation = -1
                        Select Case My.Settings.ConsoleInputChat
                            Case True
                                If CommandTextBox.Text.StartsWith("/") Then
                                    backgroundProcess.StandardInput.WriteLine(CommandTextBox.Text.Substring(1))
                                    If InputList.Count <= 0 OrElse InputList.Last <> CommandTextBox.Text.Substring(1) Then
                                        InputList.Add(CommandTextBox.Text.Substring(1))
                                    End If
                                Else
                                    backgroundProcess.StandardInput.WriteLine("say " & CommandTextBox.Text)
                                    If InputList.Count <= 0 OrElse InputList.Last <> "say " & CommandTextBox.Text Then
                                        InputList.Add("say " & CommandTextBox.Text)
                                    End If
                                End If
                                CommandTextBox.Clear()
                            Case False
                                backgroundProcess.StandardInput.WriteLine(CommandTextBox.Text)
                                If InputList.Count <= 0 OrElse InputList.Last <> CommandTextBox.Text Then
                                    InputList.Add(CommandTextBox.Text)
                                End If
                                CommandTextBox.Clear()
                        End Select
                    Catch ex As Exception
                    End Try
                End If
            Case Keys.Up
                e.SuppressKeyPress = True
                Dim tmp = CommandTextBox.Text
                Dim tmp2 = CurrentListLocation
                Try
                    CurrentListLocation += 1
                    If My.Settings.ConsoleInputChat Then
                        Dim text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                        If text.StartsWith("say ") Then
                            CommandTextBox.Text = text.Substring(4)
                        Else
                            CommandTextBox.Text = "/" & text
                        End If
                    Else
                        CommandTextBox.Text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                    End If
                Catch ex As Exception
                    CommandTextBox.Text = tmp
                    CurrentListLocation = tmp2
                End Try
            Case Keys.Down
                e.SuppressKeyPress = True
                Dim tmp = CommandTextBox.Text
                Dim tmp2 = CurrentListLocation
                Try
                    CurrentListLocation -= 1
                    If CurrentListLocation < -1 Then
                        CommandTextBox.Text = tmp
                        CurrentListLocation += 1
                    ElseIf CurrentListLocation = -1 Then
                        CommandTextBox.Text = ""
                    Else
                        If My.Settings.ConsoleInputChat Then
                            Dim text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                            If text.StartsWith("say ") Then
                                CommandTextBox.Text = text.Substring(4)
                            Else
                                CommandTextBox.Text = "/" & text
                            End If
                        Else
                            CommandTextBox.Text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                        End If
                    End If
                Catch ex As Exception
                    CommandTextBox.Text = tmp
                    CurrentListLocation = tmp2
                End Try
            Case Keys.S
                If e.Control = True And e.Alt = False And e.Shift = False Then
                    e.Handled = True
                    My.Settings.ConsoleInputChat = Not My.Settings.ConsoleInputChat
                    Select Case My.Settings.ConsoleInputChat
                        Case True
                            ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式")
                        Case False
                            ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式")
                    End Select
                End If
        End Select
    End Sub

    Private Sub RestartButton_Click(sender As Object, e As EventArgs) Handles RestartButton.Click
        If IsNothing(backgroundProcess) Then
            Dim spItem1 As New ListViewItem("通知")
            spItem1.ForeColor = Color.Blue
            spItem1.SubItems.Add(Application.ProductName)
            spItem1.SubItems.Add("")
            spItem1.SubItems.Add(String.Format("{0}:{1}:{2}", Now.Hour.ToString.PadLeft(2, "0"), Now.Minute.ToString.PadLeft(2, "0"), Now.Second.ToString.PadLeft(2, "0")))
            Try
                DataListView.Items.Add(spItem1)
            Catch ex As Exception
            End Try
            Dim msg As New MinecraftLogParser.MinecraftConsoleMessage
            msg.ServerMessageType = MCServerMessageType.Notify
            msg.Message = "伺服器重啟中..."
            msg.Time = Now
            msg.Thread = Application.ProductName
            msg.MessageType = MCMessageType.None
            Dim item As New ListViewItem(msg.ServerMessageTypeString)
            item.ForeColor = Color.Blue
            item.SubItems.Add(msg.Thread)
            item.SubItems.Add(msg.Message)
            item.SubItems.Add(String.Format("{0}:{1}:{2}", msg.Time.Hour.ToString.PadLeft(2, "0"), msg.Time.Minute.ToString.PadLeft(2, "0"), msg.Time.Second.ToString.PadLeft(2, "0")))
            Try
                DataListView.Items.Add(item)
            Catch ex As Exception
            End Try
            Dim spItem2 As New ListViewItem("通知")
            spItem2.ForeColor = Color.Blue
            spItem2.SubItems.Add(Application.ProductName)
            spItem2.SubItems.Add("")
            spItem2.SubItems.Add(String.Format("{0}:{1}:{2}", Now.Hour.ToString.PadLeft(2, "0"), Now.Minute.ToString.PadLeft(2, "0"), Now.Second.ToString.PadLeft(2, "0")))
            Try
                DataListView.Items.Add(spItem2)
            Catch ex As Exception
            End Try
            Try
                TaskTimer.Enabled = True
                TaskTimer.Start()
                Run()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub TaskTimer_Tick(sender As Object, e As EventArgs) Handles TaskTimer.Tick
        BeginInvoke(New Action(Sub()
                                   If backgroundProcess IsNot Nothing AndAlso backgroundProcess.HasExited = False Then
                                       Try
                                           MemoryLabel.Text = "占用記憶體：" & FitMemoryUnit(Process.GetProcessById(backgroundProcess.Id).WorkingSet64)
                                           IDLabel.Text = "處理序ID：" & backgroundProcess.Id
                                       Catch ex As Exception
                                       End Try
                                   Else
                                       Try
                                           MemoryLabel.Text = "占用記憶體：(無)"
                                           IDLabel.Text = "處理序ID：(無)"
                                       Catch ex As Exception
                                       End Try
                                   End If
                               End Sub))
    End Sub
    Function FitMemoryUnit(byteCount As Integer) As String
        If byteCount >= 2 ^ 30 Then
            Return (Math.Floor(byteCount / (2 ^ 30) * 100 + 0.5) / 100).ToString & " GiB"
        ElseIf byteCount >= 2 ^ 20 Then
            Return (Math.Floor(byteCount / (2 ^ 20) * 100 + 0.5) / 100).ToString & " MiB"
        ElseIf byteCount >= 2 ^ 10 Then
            Return (Math.Floor(byteCount / (2 ^ 10) * 100 + 0.5) / 100).ToString & " KiB"
        Else
            Return byteCount.ToString & " 位元組"
        End If
    End Function

    Private Sub ServerConsole_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim thread As New Threading.Thread(New Threading.ThreadStart(Sub()
                                                                         If backgroundProcess IsNot Nothing Then
                                                                             If backgroundProcess.HasExited = False Then
                                                                                 Try
                                                                                     backgroundProcess.StandardInput.WriteLine("stop")
                                                                                     Dim dog As New Watchdog(backgroundProcess)
                                                                                     dog.Run()
                                                                                 Catch ex As Exception
                                                                                 End Try
                                                                                 backgroundProcess.WaitForExit()
                                                                             End If
                                                                         End If
                                                                         Server.IsRunning = False
                                                                         Server.SaveServer()
                                                                     End Sub)) With {
            .Name = "Server Manager Close Server Thread",
            .IsBackground = True
                                                                     }
        thread.Start()

    End Sub
    Sub DisconnectUPnP()
        If GlobalModule.Manager.CanUPnP Then
            Try
                If Server.ServerOptions("server-ip") = GlobalModule.Manager.ip OrElse Server.ServerOptions("server-ip") = "" Then
                    GlobalModule.Manager.upnpProvider.DestroyPort(Server.ServerOptions("server-port"))
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub
    Sub ConnectUPnP()
        If GlobalModule.Manager.CanUPnP Then
            Try
                If Server.ServerOptions("server-ip") = GlobalModule.Manager.ip OrElse Server.ServerOptions("server-ip") = "" Then
                    GlobalModule.Manager.upnpProvider.PortForward(Server.ServerOptions("server-port"), Server.ServerOptions("motd"))
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub


    Private Sub ListBoxTImer_Tick(sender As Object, e As EventArgs) Handles ListBoxTImer.Tick
        Static layoutState As Integer
        Select Case layoutState
            Case 0
                DataListView.SuspendLayout()
            Case 1
                DataListView.ResumeLayout()
        End Select
        If layoutState >= 1 Then
            layoutState = 0
        Else
            layoutState += 1
        End If
    End Sub

    Private Sub CloseCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CloseCheckBox.CheckedChanged
        If CloseCheckBox.Checked = True Then
            If backgroundProcess IsNot Nothing Then
                If backgroundProcess.HasExited = True Then
                    Close()
                End If
            Else
                Close()
            End If
        End If
    End Sub
    Private Sub 封禁ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 封禁ToolStripMenuItem.Click
        If PlayerListBox.SelectedIndex >= 0 Then
            If backgroundProcess IsNot Nothing Then
                If backgroundProcess.HasExited = False Then
                    Try
                        backgroundProcess.StandardInput.WriteLine("ban " & PlayerListBox.SelectedItem.ToString)
                    Catch ex As Exception
                    End Try
                    backgroundProcess.WaitForExit()
                End If
            End If
        End If
    End Sub

    Private Sub 踢出ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 踢出ToolStripMenuItem.Click
        If PlayerListBox.SelectedIndex >= 0 Then
            If backgroundProcess IsNot Nothing Then
                If backgroundProcess.HasExited = False Then
                    Try
                        backgroundProcess.StandardInput.WriteLine("kick " & PlayerListBox.SelectedItem.ToString)
                    Catch ex As Exception
                    End Try
                    backgroundProcess.WaitForExit()
                End If
            End If
        End If
    End Sub

    Private Sub 設定OPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 設定OPToolStripMenuItem.Click
        If PlayerListBox.SelectedIndex >= 0 Then
            If backgroundProcess IsNot Nothing Then
                If backgroundProcess.HasExited = False Then
                    Try
                        backgroundProcess.StandardInput.WriteLine("op " & PlayerListBox.SelectedItem.ToString)
                    Catch ex As Exception
                    End Try
                    backgroundProcess.WaitForExit()
                End If
            End If
        End If
    End Sub

    Private Sub 解除OPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 解除OPToolStripMenuItem.Click
        If PlayerListBox.SelectedIndex >= 0 Then
            If backgroundProcess IsNot Nothing Then
                If backgroundProcess.HasExited = False Then
                    Try
                        backgroundProcess.StandardInput.WriteLine("deop " & PlayerListBox.SelectedItem.ToString)
                    Catch ex As Exception
                    End Try
                    backgroundProcess.WaitForExit()
                End If
            End If
        End If
    End Sub

    Private Sub NotifyChooseListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles NotifyChooseListBox.SelectedIndexChanged

    End Sub

    Private Sub UserContextMenu_Opening(sender As Object, e As CancelEventArgs) Handles UserContextMenu.Opening
        If PlayerListBox.SelectedIndices IsNot Nothing AndAlso PlayerListBox.SelectedIndices.Count > 0 Then
            e.Cancel = False
        Else
            MsgBox("必須選擇一名玩家!",, Application.ProductName)
            e.Cancel = True
        End If
    End Sub

    Private Sub ForceCloseButton_Click(sender As Object, e As EventArgs) Handles ForceCloseButton.Click
        If MsgBox("強制關閉伺服器將可能導致尚未儲存的資料消失，是否繼續？", MsgBoxStyle.YesNo, Application.ProductName) = MsgBoxResult.Yes Then
            If backgroundProcess IsNot Nothing AndAlso backgroundProcess.HasExited = False Then
                Try
                    backgroundProcess.Kill()
                Catch ex As Exception
                End Try
            End If
        End If
    End Sub

End Class