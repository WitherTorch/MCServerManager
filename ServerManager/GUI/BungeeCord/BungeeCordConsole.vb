Imports System.ComponentModel
Imports System.Threading.Tasks
Imports ServerManager.MinecraftLogParser.MinecraftConsoleMessage

Public Class BungeeCordConsole

    Dim backgroundProcess As Process
    Dim ownedConsole As New Dictionary(Of Server, ServerConsole)
    Dim ownedProcesses As New List(Of Process)
    Dim InputList As New List(Of String)()
    Dim CurrentListLocation As Integer = -1
    Public ReadOnly Property Host As BungeeCordHost
    Dim startInfo As ProcessStartInfo
    Public Sub New(host As BungeeCordHost)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Host = host
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

    Private Sub ServerConsole_Load(sender As Object, e As EventArgs) Handles Me.Load
        BeginInvoke(New Action(Sub()
                                   Text = "BungeeCord 控制台 - " & Host.BungeePathName
                               End Sub))
        TaskTimer.Enabled = True
        TaskTimer.Start()
        Run(IO.Path.Combine(JavaPath, "java.exe"), "-Djline.terminal=jline.UnsupportedTerminal -Xmx" & BungeeCordMemoryMax & "M -Xms" & BungeeCordMemoryMin & "M " & JavaArguments & " -jar """ & IO.Path.Combine(_Host.BungeePath, "bungeecord.jar") & """", _Host.BungeePath, True, False)
        DataListView.View = View.Details
        RunOwnedServers()
    End Sub

    Private Sub RunOwnedServers()
        For Each bServer In _Host.Servers
            RunOwnedServer(bServer)
        Next
    End Sub
    Private Sub RunOwnedServer(bServer As BungeeCordHost.BungeeServer, Optional justChangeProcess As Boolean = False, Optional page As TabPage = Nothing, Optional changeProcess As Process = Nothing)
        Dim server = bServer.Server
        If justChangeProcess = False Then
            page = New TabPage(bServer.ServerAlias & " (" & server.ServerPathName & ") 資料列表") With {.Size = New Size(792, 424)}
            Dim layout As New Panel() With {.Dock = DockStyle.Fill}
            Dim commandBox As New TextBox() With {.Dock = DockStyle.Bottom}
            Dim dataListView As New ListView()
            Dim topPanel As New Panel()
            Dim pauseLoad As New CheckBox()
            Dim _inputList As New List(Of String)()
            Dim _currentListLocation As Integer = -1
            dataListView.Columns.AddRange(New ColumnHeader() {New ColumnHeader() With {.Text = "類型", .Width = 90}, New ColumnHeader() With {.Text = "執行緒", .DisplayIndex = 2, .Width = 116}, New ColumnHeader() With {.DisplayIndex = 1, .Text = "時間", .Width = 69}, New ColumnHeader() With {.Text = "訊息", .DisplayIndex = 3, .Width = 534}})
            dataListView.Dock = DockStyle.Fill
            dataListView.FullRowSelect = True
            dataListView.GridLines = True
            dataListView.MultiSelect = False
            dataListView.UseCompatibleStateImageBehavior = False
            dataListView.View = View.Details
            Select Case server.ServerVersionType
                Case Server.EServerVersionType.Spigot
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.CraftBukkit
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.Nukkit
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.VanillaBedrock
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.Paper
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.Akarin
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.Spigot_Git
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.Cauldron
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.Thermos
                    dataListView.Columns.Remove(dataListView.Columns(1))
                Case Server.EServerVersionType.Contigo
                    dataListView.Columns.Remove(dataListView.Columns(1))
            End Select

            pauseLoad.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            pauseLoad.AutoSize = True
            pauseLoad.Text = "暫停載入"
            pauseLoad.Location = New Point(topPanel.Width - 75, 3)
            pauseLoad.Size = New Size(72, 16)

            topPanel.Dock = DockStyle.Top
            topPanel.Controls.Add(pauseLoad)
            topPanel.AutoSize = True
            topPanel.BackColor = Color.White
            topPanel.Height = 16

            layout.Controls.Add(dataListView)
            layout.Controls.Add(commandBox)
            layout.Controls.Add(topPanel)

            page.Padding = New Padding(0, 0, 0, 3)
            page.BackColor = Color.White
            page.Controls.Add(layout)
            MainTabControl.TabPages.Add(page)
            changeProcess = bServer.RunServer()
        End If
        page.Tag = (server, changeProcess)
        Dim alternateInputWriter As New IO.StreamWriter(changeProcess.StandardInput.BaseStream, New Text.UTF8Encoding(False)) With {.AutoFlush = True}
        AddHandler CType(page.Controls(0).Controls(2).Controls(0), CheckBox).CheckedChanged, Sub()
                                                                                                 Try
                                                                                                     If changeProcess IsNot Nothing AndAlso changeProcess.HasExited = False AndAlso CType(page.Controls(0).Controls(2).Controls(0), CheckBox).Checked = False Then
                                                                                                         changeProcess.BeginErrorReadLine()
                                                                                                         changeProcess.BeginOutputReadLine()
                                                                                                     Else
                                                                                                         changeProcess.CancelErrorRead()
                                                                                                         changeProcess.CancelOutputRead()
                                                                                                     End If
                                                                                                 Catch ex As Exception

                                                                                                 End Try
                                                                                             End Sub
        If CType(page.Controls(0).Controls(2).Controls(0), CheckBox).Checked = False Then
            changeProcess.BeginErrorReadLine()
            changeProcess.BeginOutputReadLine()
        Else
            changeProcess.CancelErrorRead()
            changeProcess.CancelOutputRead()
        End If
        AddHandler changeProcess.ErrorDataReceived, Sub(sender, e)
                                                        Try
                                                            If IsNothing(changeProcess) = False AndAlso changeProcess.HasExited = False Then
                                                                If IsNothing(e.Data) = False Then
                                                                    Task.Run(Sub()
                                                                                 If Not (ownedConsole.ContainsKey(server) = False Or ownedConsole(server).IsDisposed) Then
                                                                                     ownedConsole(server).InputMessageToListView(New MinecraftLogParser.MinecraftConsoleMessage() With {.Message = e.Data}, True)
                                                                                 End If
                                                                                 Dim item As New ListViewItem("錯誤")
                                                                                 item.ForeColor = Color.Red
                                                                                 Select Case server.ServerVersionType
                                                                                     Case Server.EServerVersionType.Spigot
                                                                                     Case Server.EServerVersionType.CraftBukkit
                                                                                     Case Server.EServerVersionType.Nukkit
                                                                                     Case Server.EServerVersionType.VanillaBedrock
                                                                                     Case Server.EServerVersionType.Paper
                                                                                     Case Server.EServerVersionType.Akarin
                                                                                     Case Server.EServerVersionType.Spigot_Git
                                                                                     Case Server.EServerVersionType.Cauldron
                                                                                     Case Server.EServerVersionType.Thermos
                                                                                     Case Server.EServerVersionType.Contigo
                                                                                     Case Else
                                                                                         item.SubItems.Add("")
                                                                                 End Select
                                                                                 Dim nowTime = Now
                                                                                 item.SubItems.Add(String.Format("{0}:{1}:{2}", nowTime.Hour.ToString.PadLeft(2, "0"), nowTime.Minute.ToString.PadLeft(2, "0"), nowTime.Second.ToString.PadLeft(2, "0")))
                                                                                 item.SubItems.Add(e.Data)
                                                                                 If NotifyChooseListBox.CheckedIndices.Contains(3) Then _
                                                                                                                    NotifyInfoMessage(bServer.ServerAlias & " 發出錯誤訊息:" & vbNewLine & e.Data, Text)

                                                                                 BeginInvokeIfRequired(DataListView, Sub()
                                                                                                                         SyncLock DataListView
                                                                                                                             DataListView.Items.Add(item)
                                                                                                                             Try
                                                                                                                                 If DataListView.GetItemRect(DataListView.Items.Count - 2).Y < DataListView.Height Then item.EnsureVisible()
                                                                                                                             Catch ex As Exception

                                                                                                                             End Try
                                                                                                                         End SyncLock
                                                                                                                     End Sub)
                                                                             End Sub)
                                                                End If
                                                            End If
                                                        Catch ex As Exception
                                                        End Try
                                                    End Sub
        AddHandler changeProcess.OutputDataReceived, Sub(sender, e)
                                                         Try
                                                             If IsNothing(changeProcess) = False AndAlso changeProcess.HasExited = False Then
                                                                 If IsNothing(e.Data) = False Then
                                                                     Task.Run(Sub()
                                                                                  Dim msg = MinecraftLogParser.ToConsoleMessage(e.Data, Now)
                                                                                  If Not (ownedConsole.ContainsKey(server) = False Or ownedConsole(server).IsDisposed) Then
                                                                                      ownedConsole(server).InputMessageToListView(msg)
                                                                                  End If
                                                                                  Dim item As New ListViewItem(msg.ServerMessageTypeString)
                                                                                  Select Case server.ServerVersionType
                                                                                      Case Server.EServerVersionType.CraftBukkit
                                                                                      Case Server.EServerVersionType.Spigot
                                                                                      Case Server.EServerVersionType.Spigot_Git
                                                                                      Case Server.EServerVersionType.Paper
                                                                                      Case Server.EServerVersionType.Akarin
                                                                                      Case Server.EServerVersionType.Cauldron
                                                                                      Case Server.EServerVersionType.Thermos
                                                                                      Case Server.EServerVersionType.Contigo
                                                                                      Case Server.EServerVersionType.Nukkit
                                                                                      Case Server.EServerVersionType.VanillaBedrock
                                                                                      Case Else
                                                                                          item.SubItems.Add(msg.Thread)
                                                                                  End Select
                                                                                  item.SubItems.Add(String.Format("{0}:{1}:{2}", msg.Time.Hour.ToString.PadLeft(2, "0"), msg.Time.Minute.ToString.PadLeft(2, "0"), msg.Time.Second.ToString.PadLeft(2, "0")))
                                                                                  item.SubItems.Add(msg.Message)
                                                                                  Select Case msg.ServerMessageType
                                                                                      Case MCServerMessageType.Warning
                                                                                          item.ForeColor = Color.Orange
                                                                                          If NotifyChooseListBox.CheckedIndices.Contains(2) Then _
                                                                                                                    NotifyInfoMessage(bServer.ServerAlias & " 發出警告訊息:" & vbNewLine & msg.Message, Text)
                                                                                      Case MCServerMessageType.Error
                                                                                          item.ForeColor = Color.Red
                                                                                          If NotifyChooseListBox.CheckedIndices.Contains(3) Then _
                                                                                                                    NotifyInfoMessage(bServer.ServerAlias & " 發出錯誤訊息:" & vbNewLine & msg.Message, Text)
                                                                                      Case MCServerMessageType.Debug
                                                                                          item.ForeColor = Color.YellowGreen
                                                                                      Case MCServerMessageType.Trace
                                                                                          item.ForeColor = Color.Green
                                                                                  End Select
                                                                                  Select Case msg.MessageType
                                                                                      Case MCMessageType.PlayerLogin
                                                                                          If NotifyChooseListBox.CheckedIndices.Contains(0) Then _
                                                                                                             NotifyInfoMessage(msg.AddtionalMessage("player") & " 進入 " & bServer.ServerAlias, Text)
                                                                                          BeginInvoke(Sub() PlayerListBox.Items.Add(msg.AddtionalMessage("player") & " (" & bServer.ServerAlias & ")"))
                                                                                      Case MCMessageType.PlayerLogout
                                                                                          If NotifyChooseListBox.CheckedIndices.Contains(1) Then _
                                                                                                             NotifyInfoMessage(msg.AddtionalMessage("player") & " 離開 " & bServer.ServerAlias, Text)
                                                                                          BeginInvoke(Sub() PlayerListBox.Items.Remove(msg.AddtionalMessage("player") & " (" & bServer.ServerAlias & ")"))
                                                                                      Case Else
                                                                                  End Select
                                                                                  Try
                                                                                      BeginInvokeIfRequired(DataListView, Sub()
                                                                                                                              SyncLock DataListView
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
        AddHandler changeProcess.Exited, Sub(sender, e)
                                             bServer.Server.IsRunning = False
                                             bServer.Server.ProcessID = 0
                                         End Sub
        ownedProcesses.Add(changeProcess)
        If changeProcess IsNot Nothing Then bServer.Server.ProcessID = changeProcess.Id
    End Sub

    Private Overloads Sub Run()
        Run("", "", "")
    End Sub
    Private Overloads Sub Run(program As String, serverDir As String)
        Run(program, "", serverDir, True, False)
    End Sub
    Private Overloads Sub Run(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8 As Boolean = True)
        backgroundProcess = Process.Start(PrepareStartInfo(program, args, serverDir, nogui, UTF8))
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

                                                                                         Dim item As New ListViewItem("錯誤")
                                                                                         item.SubItems.Add(e.Data)
                                                                                         Dim nowTime = Now
                                                                                         item.SubItems.Add(String.Format("{0}:{1}:{2}", nowTime.Hour.ToString.PadLeft(2, "0"), nowTime.Minute.ToString.PadLeft(2, "0"), nowTime.Second.ToString.PadLeft(2, "0")))

                                                                                         BeginInvokeIfRequired(Me, Sub()
                                                                                                                       SyncLock Me
                                                                                                                           DataListView.Items.Add(item)
                                                                                                                           Try
                                                                                                                               If DataListView.GetItemRect(DataListView.Items.Count - 2).Y < DataListView.Height Then item.EnsureVisible()
                                                                                                                           Catch ex As Exception

                                                                                                                           End Try
                                                                                                                       End SyncLock
                                                                                                                   End Sub)
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
                                                                                          Dim msg = MinecraftLogParser.ToConsoleMessage(e.Data, Now)
                                                                                          Dim item As New ListViewItem(msg.BungeeCordMessageType)
                                                                                          item.SubItems.Add(msg.Message)
                                                                                          item.SubItems.Add(String.Format("{0}:{1}:{2}", msg.Time.Hour.ToString.PadLeft(2, "0"), msg.Time.Minute.ToString.PadLeft(2, "0"), msg.Time.Second.ToString.PadLeft(2, "0")))

                                                                                          BeginInvokeIfRequired(Me, Sub()
                                                                                                                        SyncLock Me
                                                                                                                            DataListView.Items.Add(item)
                                                                                                                            Try
                                                                                                                                If DataListView.GetItemRect(DataListView.Items.Count - 2).Y < DataListView.Height Then item.EnsureVisible()
                                                                                                                            Catch ex As Exception

                                                                                                                            End Try
                                                                                                                        End SyncLock
                                                                                                                    End Sub)
                                                                                      End Sub)
                                                                         End If
                                                                     End If
                                                                 Catch ex As Exception
                                                                 End Try
                                                             End Sub
        End If
        ServerStatusLabel.Text = "BungeeCord 狀態：啟動"
        Host.IsRunning = True
        backgroundProcess.EnableRaisingEvents = True

        AddHandler backgroundProcess.Exited, Sub(sender, e)

                                                 TaskTimer.Enabled = False
                                                 If IsDisposed = False Then
                                                     Invoke(Sub() ServerStatusLabel.Text = "BungeeCord 狀態：關閉")
                                                 End If
                                                 Host.IsRunning = False
                                                 backgroundProcess = Nothing
                                                 If IsDisposed = False Then
                                                     If CloseCheckBox.Checked Then
                                                         Close()
                                                     End If
                                                 End If
                                                 Console.WriteLine("Process Exited")
                                             End Sub
        TaskTimer.Enabled = True
    End Sub
    Private Overloads Function PrepareStartInfo(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8Encoding As Boolean = False) As ProcessStartInfo
        If IsNothing(startInfo) Then
            Dim processInfo As ProcessStartInfo
            If args = "" Then
                processInfo = New ProcessStartInfo(program)
            Else
                If UTF8Encoding Then args = "-Dfile.encoding=UTF8 " & args
                processInfo = New ProcessStartInfo(program, args)
            End If
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = nogui
            If UTF8Encoding And nogui Then
                processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
                processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
            End If
            If nogui = False Then
                MainTabControl.TabPages.Remove(DataStreamTabPage)
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
        Select Case e.KeyData
            Case Keys.Enter
                Try
                    e.SuppressKeyPress = True
                    If InputList.Count <= 0 OrElse InputList.Last <> CommandTextBox.Text Then
                        InputList.Add(CommandTextBox.Text)
                    End If
                    CurrentListLocation = -1
                    backgroundProcess.StandardInput.WriteLine(CommandTextBox.Text)

                    CommandTextBox.Clear()
                Catch ex As Exception
                End Try
            Case Keys.Up
                e.SuppressKeyPress = True
                Dim tmp = CommandTextBox.Text
                Dim tmp2 = CurrentListLocation
                Try
                    CurrentListLocation += 1
                    CommandTextBox.Text = InputList.Item(InputList.Count - CurrentListLocation - 1)
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
                        CommandTextBox.Text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                    End If
                Catch ex As Exception
                    CommandTextBox.Text = tmp
                    CurrentListLocation = tmp2
                End Try
        End Select
    End Sub

    Private Sub RestartButton_Click(sender As Object, e As EventArgs)
        If IsNothing(backgroundProcess) Then Run()
    End Sub

    Private Sub TaskTimer_Tick(sender As Object, e As EventArgs) Handles TaskTimer.Tick
        BeginInvoke(New Action(Sub()
                                   Try
                                       MemoryLabel.Text = "占用記憶體：" & FitMemoryUnit(Process.GetProcessById(backgroundProcess.Id).WorkingSet64)
                                       IDLabel.Text = "處理序ID：" & backgroundProcess.Id
                                   Catch ex As Exception
                                   End Try
                               End Sub))
    End Sub
    Function FitMemoryUnit(byteCount As System.Numerics.BigInteger) As String
        If byteCount >= System.Numerics.BigInteger.Pow(2, 80) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 80)).ToString & " YiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 70) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 70)).ToString & " ZiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 60) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 60)).ToString & " EiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 50) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 50)).ToString & " PiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 40) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 40)).ToString & " TiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 30) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 30)).ToString & " GiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 20) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 20)).ToString & " MiB"
        ElseIf byteCount >= System.Numerics.BigInteger.Pow(2, 10) Then
            Return (byteCount / System.Numerics.BigInteger.Pow(2, 10)).ToString & " KiB"
        Else
            Return byteCount.ToString & " 位元組"
        End If
    End Function

    Private Sub ServerConsole_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim notifySettings As New List(Of Boolean)
        For i As Integer = 0 To NotifyChooseListBox.Items.Count - 1
            notifySettings.Add(NotifyChooseListBox.GetItemChecked(i))
        Next
        BungeeConsoleMessages = notifySettings.ToArray
        Dim thread As New Threading.Thread(New Threading.ThreadStart(Sub()
                                                                         If backgroundProcess IsNot Nothing Then
                                                                             If backgroundProcess.HasExited = False Then
                                                                                 Try
                                                                                     backgroundProcess.StandardInput.WriteLine("end")
                                                                                 Catch ex As Exception
                                                                                 End Try
                                                                                 Dim dog As New Watchdog(backgroundProcess)
                                                                                 dog.Run()
                                                                             End If
                                                                         End If
                                                                     End Sub)) With {
            .Name = "Server Manager Close BungeeCord Thread",
            .IsBackground = False
                                                                     }
        thread.Start()
        Dim finishedProcessCount As Integer = 0
        For Each process In ownedProcesses
            Dim serverThread As New Threading.Thread(New Threading.ThreadStart(Sub()
                                                                                   If process IsNot Nothing Then
                                                                                       If process.HasExited = False Then
                                                                                           Try
                                                                                               process.StandardInput.WriteLine("stop")
                                                                                           Catch ex As Exception
                                                                                           End Try
                                                                                           process.WaitForExit()
                                                                                       End If
                                                                                   End If
                                                                                   finishedProcessCount += 1
                                                                               End Sub)) With {
            .Name = "Server Manager Close Server Thread",
            .IsBackground = False
                                                                     }
            serverThread.Start()
        Next
        Dim Cthread As New Threading.Thread(Sub()
                                                If ownedProcesses.Count > 0 Then
                                                    Do While finishedProcessCount < ownedProcesses.Count
                                                    Loop
                                                End If
                                                Host.IsRunning = False
                                                RunningBungeeCord = False
                                            End Sub) With {
            .Name = "Server Manager Close BungeeCord Thread",
            .IsBackground = False
                                                                     }
        Cthread.Start()
    End Sub

    Private Sub BungeeCordConsole_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If BungeeConsoleMessages IsNot Nothing Then
            For i As Integer = 0 To BungeeConsoleMessages.Count - 1
                If i < NotifyChooseListBox.Items.Count - 1 Then
                    NotifyChooseListBox.SetItemChecked(i, BungeeConsoleMessages(i))
                End If
            Next
        End If
    End Sub

    Private Sub MainTabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MainTabControl.SelectedIndexChanged
        If MainTabControl.SelectedIndex > 2 Then
            Button1.Visible = True
        Else
            Button1.Visible = False
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ownedConsole.ContainsKey(CType(MainTabControl.SelectedTab.Tag, ValueTuple(Of Server, Process)).Item1) = False OrElse
                ownedConsole(CType(MainTabControl.SelectedTab.Tag, ValueTuple(Of Server, Process)).Item1).IsDisposed Then
            Dim console As New ServerConsole(CType(MainTabControl.SelectedTab.Tag, ValueTuple(Of Server, Process)).Item1, MainTabControl.SelectedTab.Text, CType(MainTabControl.SelectedTab.Controls(0).Controls(0), ListView).Items, CType(MainTabControl.SelectedTab.Tag, ValueTuple(Of Server, Process)).Item2)
            If ownedConsole.ContainsKey(CType(MainTabControl.SelectedTab.Tag, ValueTuple(Of Server, Process)).Item1) Then
                ownedConsole(CType(MainTabControl.SelectedTab.Tag, ValueTuple(Of Server, Process)).Item1) = console
            Else
                ownedConsole.Add(CType(MainTabControl.SelectedTab.Tag, ValueTuple(Of Server, Process)).Item1, console)
            End If
            AddHandler console.ServerRestarted, Sub(ByRef process As Process)
                                                    RunOwnedServer(Nothing, True, MainTabControl.SelectedTab, process)
                                                End Sub
            console.Show()
        End If
    End Sub
End Class