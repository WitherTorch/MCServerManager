Imports System.ComponentModel
Imports System.Threading
Imports System.Threading.Tasks
Imports ServerManager.MinecraftLogParser.MinecraftConsoleMessage
Imports SharpDX.Direct3D11
Imports SharpDX.DXGI
Imports Device = SharpDX.Direct3D11.Device

Public Class ModPackServerConsole
    Dim InputList As New List(Of String)()
    Dim CurrentListLocation As Integer = -1
    Dim backgroundProcess As Process
    Dim alternateInputStreamWriter As IO.StreamWriter
    Dim cmd As CMDForm
    Dim outputs As String = ""
    Dim isMessageUpdate As Boolean = False
    Public ReadOnly Property Server As ModPackServer
    Dim startInfo As ProcessStartInfo
    'DirectX support
    Dim d As Device
    Dim sc As SwapChain
    Dim target As Texture2D
    Dim targetView As RenderTargetView
    Protected Overrides Sub OnClosing(ByVal e As CancelEventArgs)
        d.Dispose()
        sc.Dispose()
        target.Dispose()
        targetView.Dispose()
        MyBase.OnClosing(e)
    End Sub

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        d.ImmediateContext.ClearRenderTargetView(targetView, New SharpDX.Mathematics.Interop.RawColor4(BackColor.R, BackColor.G, BackColor.B, BackColor.A))
        sc.Present(0, PresentFlags.None)
        MyBase.OnPaint(e)
    End Sub
    Public Sub New(Server As ModPackServer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = Server
        Text = "模組包伺服器控制台 - " & Server.ServerPathName
        Dim scd As SwapChainDescription = New SwapChainDescription() With {
                .BufferCount = 1,
                .Flags = SwapChainFlags.None,
                .IsWindowed = True,
                .ModeDescription = New ModeDescription(Me.ClientSize.Width, Me.ClientSize.Height, New Rational(60, 1), Format.R8G8B8A8_UNorm),
                .OutputHandle = Me.Handle,
                .SampleDescription = New SampleDescription(1, 0),
                .SwapEffect = SwapEffect.Discard,
                .Usage = Usage.RenderTargetOutput
            }
        Try
            Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, scd, d, sc)
        Catch ex As SharpDX.SharpDXException
            scd.ModeDescription.RefreshRate.Numerator = 30 '30 fps
            Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, scd, d, sc)
        End Try
        target = Texture2D.FromSwapChain(Of Texture2D)(sc, 0)
        targetView = New RenderTargetView(d, target)
        d.ImmediateContext.OutputMerger.SetRenderTargets(targetView)
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
        ConnectUPnP()
        TaskTimer.Enabled = True
        TaskTimer.Start()
        Run()
    End Sub
    Private Sub ServerConsole_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        BeginInvokeIfRequired(Me, New Action(Sub()
                                                 Select Case ConsoleMode
                                                     Case True
                                                         ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式")
                                                     Case False
                                                         ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式")
                                                 End Select
                                             End Sub))
        If ServerConsoleMessages IsNot Nothing Then
            For i As Integer = 0 To ServerConsoleMessages.Count - 1
                If i < NotifyChooseListBox.Items.Count - 1 Then
                    NotifyChooseListBox.SetItemChecked(i, ServerConsoleMessages(i))
                End If
            Next
        End If
    End Sub
    Friend Overloads Sub Run()
        Select Case Server.PackType
            Case ModPackServer.ModPackType.FeedTheBeast
                If IsUnixLikeSystem Then
                    Run(GetJavaPath(), String.Format("-server -Xmx{0}M -Xms{1}M {2} -jar ""{3}"" nogui", ModpackServerMemoryMax, ModpackServerMemoryMin, Server.InternalJavaArguments & JavaArguments, IIf(Server.ServerPath.EndsWith("/"), Server.ServerPath, Server.ServerPath & "/") & Server.ServerRunJAR), Server.ServerPath, True, True)
                Else
                    Run(GetJavaPath(), String.Format("-server -Xmx{0}M -Xms{1}M {2} -jar ""{3}"" nogui", ModpackServerMemoryMax, ModpackServerMemoryMin, Server.InternalJavaArguments & JavaArguments, IIf(Server.ServerPath.EndsWith("\"), Server.ServerPath, Server.ServerPath & "\") & Server.ServerRunJAR), Server.ServerPath, True, True)
                End If
            Case ModPackServer.ModPackType.CurseForge
                If IsUnixLikeSystem Then
                    Run(GetJavaPath(), String.Format("-server -Xmx{0}M -Xms{1}M {2} -jar ""{3}"" nogui", ModpackServerMemoryMax, ModpackServerMemoryMin, Server.InternalJavaArguments & JavaArguments, IIf(Server.ServerPath.EndsWith("/"), Server.ServerPath, Server.ServerPath & "/") & Server.ServerRunJAR), Server.ServerPath, True, True)
                Else
                    Run(GetJavaPath(), String.Format("-server -Xmx{0}M -Xms{1}M {2} -jar ""{3}"" nogui", ModpackServerMemoryMax, ModpackServerMemoryMin, Server.InternalJavaArguments & JavaArguments, IIf(Server.ServerPath.EndsWith("\"), Server.ServerPath, Server.ServerPath & "\") & Server.ServerRunJAR), Server.ServerPath, True, True)
                End If
        End Select
    End Sub
    Private Overloads Sub Run(program As String, serverDir As String)
        Run(program, "", serverDir, True, False)
    End Sub
    Private Overloads Sub Run(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8 As Boolean = False)
        backgroundProcess = Process.Start(PrepareStartInfo(program, args, serverDir, nogui, UTF8))
        RestartButton.Enabled = False
        ForceCloseButton.Enabled = True
        If UTF8 Then
            alternateInputStreamWriter = New IO.StreamWriter(backgroundProcess.StandardInput.BaseStream, New System.Text.UTF8Encoding(False)) With {.AutoFlush = True}
        Else
            alternateInputStreamWriter = backgroundProcess.StandardInput
        End If
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
                                                                            outputs &= vbNewLine & e.Data
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
                                                                                             BeginInvokeIfRequired(Me, Sub()
                                                                                                                           SyncLock Me
                                                                                                                               DataListView.Items.Add(item)
                                                                                                                               Try
                                                                                                                                   If DataListView.GetItemRect(DataListView.Items.Count - 2).Y < DataListView.Height Then item.EnsureVisible()
                                                                                                                               Catch ex As Exception

                                                                                                                               End Try
                                                                                                                           End SyncLock
                                                                                                                       End Sub)
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
                                                                             outputs &= vbNewLine & e.Data
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
                                                                                                  Case MCServerMessageType.Debug
                                                                                                      item.ForeColor = Color.YellowGreen
                                                                                                  Case MCServerMessageType.Trace
                                                                                                      item.ForeColor = Color.Green
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
                                                                                              BeginInvokeIfRequired(Me, Sub()
                                                                                                                            SyncLock Me
                                                                                                                                DataListView.Items.Add(item)
                                                                                                                                Try
                                                                                                                                    If DataListView.GetItemRect(DataListView.Items.Count - 2).Y < DataListView.Height Then item.EnsureVisible()
                                                                                                                                Catch ex As Exception

                                                                                                                                End Try
                                                                                                                            End SyncLock
                                                                                                                        End Sub)
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
                        Select Case ConsoleMode
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
                    If ConsoleMode Then
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
                        If ConsoleMode Then
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
                    ConsoleMode = Not ConsoleMode
                    Select Case ConsoleMode
                        Case True
                            ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式")
                        Case False
                            ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式")
                    End Select
                End If
            Case Keys.Back
                If e.Control = True And e.Alt = False And e.Shift = False Then
                    e.Handled = True
                    e.SuppressKeyPress = True
                    BeginInvokeIfRequired(Me, Sub() CommandTextBox.Clear())
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
        BeginInvokeIfRequired(Me, New Action(Sub()
                                                 If backgroundProcess IsNot Nothing AndAlso backgroundProcess.HasExited = False Then
                                                     Try
                                                         MemoryLabel.Text = "占用記憶體：" & FitMemoryUnit(Process.GetProcessById(backgroundProcess.Id).WorkingSet64)
                                                         IDLabel.Text = "處理序ID：" & backgroundProcess.Id
                                                         Dim maxPlayerCount As Integer = IIf(Server.ServerOptions.ContainsKey("max-players") AndAlso IsNumeric(Server.ServerOptions("max-players")), Server.ServerOptions("max-players"), 0)
                                                         Dim playerListTitle As String = String.Format("玩家 ({0}/{1})", PlayerListBox.Items.Count, maxPlayerCount)
                                                         If PlayerGroupBox.Text <> playerListTitle Then PlayerGroupBox.Text = playerListTitle
                                                     Catch ex As Exception
                                                     End Try
                                                 Else
                                                     Try
                                                         MemoryLabel.Text = "占用記憶體：(無)"
                                                         IDLabel.Text = "處理序ID：(無)"
                                                         Dim maxPlayerCount As Integer = IIf(Server.ServerOptions.ContainsKey("max-players") AndAlso IsNumeric(Server.ServerOptions("max-players")), Server.ServerOptions("max-players"), 0)
                                                         Dim playerListTitle As String = String.Format("玩家 ({0}/{1})", PlayerListBox.Items.Count, maxPlayerCount)
                                                         If PlayerGroupBox.Text <> playerListTitle Then PlayerGroupBox.Text = playerListTitle
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
        Dim notifySettings As New List(Of Boolean)
        For i As Integer = 0 To NotifyChooseListBox.Items.Count - 1
            notifySettings.Add(NotifyChooseListBox.GetItemChecked(i))
        Next
        ServerConsoleMessages = notifySettings.ToArray
        Dim thread As New Threading.Thread(Sub()
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
                                           End Sub) With {
            .Name = "Server Manager Close Server Thread",
            .IsBackground = True
                                                                     }
        thread.Start()

    End Sub
    Sub DisconnectUPnP()
        If GlobalModule.Manager.CanUPnP Then
            Try
                If GlobalModule.Manager.ip.Contains(Server.ServerOptions("server-ip")) OrElse Server.ServerOptions("server-ip") = "" Then
                    GlobalModule.Manager.upnpProvider.DestroyPort(Server.ServerOptions("server-port"))
                End If
            Catch ex As Exception
            End Try
        End If
    End Sub
    Sub ConnectUPnP()
        If GlobalModule.Manager.CanUPnP Then
            Try
                If GlobalModule.Manager.ip.Contains(Server.ServerOptions("server-ip")) OrElse Server.ServerOptions("server-ip") = "" Then
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
    Private Sub CMDButton_Click(sender As Object, e As EventArgs) Handles CMDButton.Click
        If backgroundProcess IsNot Nothing Then
            If cmd Is Nothing OrElse cmd.IsDisposed Then
                cmd = New CMDForm(alternateInputStreamWriter, outputs.TrimStart(vbCr, vbLf))
                AddHandler backgroundProcess.OutputDataReceived, Sub(process As Process, args As DataReceivedEventArgs)
                                                                     Try
                                                                         If args.Data IsNot Nothing AndAlso cmd IsNot Nothing Then cmd.AppendText(args.Data)
                                                                     Catch ex As Exception

                                                                     End Try
                                                                 End Sub
                cmd.Show()
            Else
                BeginInvokeIfRequired(cmd, Sub()
                                               If cmd.Visible = False Then
                                                   cmd.Visible = True
                                               End If
                                           End Sub)
            End If
        End If
    End Sub
End Class