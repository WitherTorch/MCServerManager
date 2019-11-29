Imports System.ComponentModel
Imports System.Threading
Imports System.Threading.Tasks
Imports ServerManager.MinecraftLogParser.MinecraftConsoleMessage
Imports SharpDX.Direct3D11
Imports SharpDX.DXGI
Imports Device = SharpDX.Direct3D11.Device

Public Class BungeeCordConsole

    Dim backgroundProcess As Process
    Dim ownedConsole As New Dictionary(Of Server, ServerConsole)
    Dim ownedWriterForServers As New Dictionary(Of Server, IO.StreamWriter)
    Dim ownedTaskForServers As New Dictionary(Of Server, List(Of ServerTask))
    Dim ownedTasksAndTimersForServers As New Dictionary(Of Server, Dictionary(Of ServerTask, System.Windows.Forms.Timer))
    Dim ownedProcesses As New List(Of Process)
    Dim InputList As New List(Of String)()
    Dim CurrentListLocation As Integer = -1
    Public ReadOnly Property Host As BungeeCordHost
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
    Public Sub New(host As BungeeCordHost)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Host = host
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

    Private Sub ServerConsole_Load(sender As Object, e As EventArgs) Handles Me.Load
        BeginInvoke(New Action(Sub()
                                   Text = Host.BungeeType.ToString & " 控制台 - " & Host.BungeePathName
                                   ServerStatusLabel.Text = ServerStatusLabel.Text.Replace("BungeeCord", Host.BungeeType.ToString)
                                   SettingTabPage.Text = SettingTabPage.Text.Replace("BungeeCord", Host.BungeeType.ToString)
                                   MainTabPage.Text = MainTabPage.Text.Replace("BungeeCord", Host.BungeeType.ToString)
                                   DataStreamTabPage.Text = DataStreamTabPage.Text.Replace("BungeeCord", Host.BungeeType.ToString)
                               End Sub))
        TaskTimer.Enabled = True
        TaskTimer.Start()
        Select Case Host.BungeeType
            Case BungeeCordType.BungeeCord
                Run(IO.Path.Combine(JavaPath, "java.exe"), "-Djline.terminal=jline.UnsupportedTerminal -Xmx" & BungeeCordMemoryMax & "M -Xms" & BungeeCordMemoryMin & "M " & JavaArguments & " -jar """ & IO.Path.Combine(_Host.BungeePath, "bungeecord.jar") & """", _Host.BungeePath, True, False)
            Case BungeeCordType.Waterfall
                Run(IO.Path.Combine(JavaPath, "java.exe"), "-Djline.terminal=jline.UnsupportedTerminal -Xmx" & BungeeCordMemoryMax & "M -Xms" & BungeeCordMemoryMin & "M " & JavaArguments & " -jar """ & IO.Path.Combine(_Host.BungeePath, "waterfall.jar") & """", _Host.BungeePath, True, False)
        End Select
        DataListView.View = View.Details
        RunOwnedServers()
    End Sub

    Private Sub RunOwnedServers()
        For Each bServer In _Host.Servers
            RunOwnedServer(bServer)
        Next
    End Sub
    Private Sub RunOwnedServer(bServer As BungeeCordHost.BungeeServer, Optional justChangeProcess As Boolean = False, Optional page As TabPage = Nothing, Optional changeProcess As Process = Nothing)
        Dim server As Server
        If justChangeProcess Then
            server = CType(page.Tag, ValueTuple(Of Server, Process)).Item1
        Else
            server = bServer.Server
        End If
        Dim dataListView As ListView
        Dim commandBox As MetroFramework.Controls.MetroTextBox
        Static CurrentListLocation As Integer = -1
        Static upNumber As Integer = 0
        Static menu As TableLayoutPanel
        Static InputList As New List(Of String)()
        If justChangeProcess = False Then
            page = New TabPage(bServer.ServerAlias & " (" & server.ServerPathName & ") 資料列表") With {.Size = New Size(792, 424)}
            Dim layout As New Panel() With {.Dock = DockStyle.Fill}
            commandBox = New MetroFramework.Controls.MetroTextBox() With {.Dock = DockStyle.Bottom}
            dataListView = New ListView()
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
            ownedTaskForServers.Add(server, server.ServerTasks.ToList)
            Dim taskTimerDictionary As New Dictionary(Of ServerTask, System.Windows.Forms.Timer)
            For Each task In ownedTaskForServers(server)
                If task.Mode = ServerTask.TaskMode.Repeating Then
                    Dim timer As New System.Windows.Forms.Timer()
                    timer.Interval = task.RepeatingPeriod * ServerConsole.GetBaseIntervalValue(task.RepeatingPeriodUnit)
                    taskTimerDictionary.Add(task, timer)
                    AddHandler timer.Tick, Sub()
                                               RunOwnedServerTask(page, task, New Dictionary(Of String, String))
                                           End Sub
                    timer.Start()
                End If
            Next
            ownedTasksAndTimersForServers.Add(server, taskTimerDictionary)
            AddHandler CType(page.Controls(0).Controls(2).Controls(0), CheckBox).CheckedChanged, Sub(obj, args)
                                                                                                     Try
                                                                                                         Dim process As Process = CType(page.Tag, ValueTuple(Of Server, Process)).Item2
                                                                                                         If process IsNot Nothing AndAlso process.HasExited = False Then
                                                                                                             If Not (ownedConsole.ContainsKey(server) = False OrElse ownedConsole(server).IsDisposed) Then
                                                                                                                 ownedConsole(server).StopLoadingCheckBox.Checked = CType(obj, CheckBox).Checked
                                                                                                             Else
                                                                                                                 If CType(obj, CheckBox).Checked = False Then
                                                                                                                     process.BeginErrorReadLine()
                                                                                                                     process.BeginOutputReadLine()
                                                                                                                 Else
                                                                                                                     process.CancelErrorRead()
                                                                                                                     process.CancelOutputRead()
                                                                                                                 End If
                                                                                                             End If
                                                                                                         End If


                                                                                                     Catch ex As Exception

                                                                                                     End Try
                                                                                                 End Sub
            AddHandler commandBox.KeyDown, Sub(obj, args)
                                               Select Case args.KeyCode
                                                   Case Keys.Enter
                                                       args.SuppressKeyPress = True
                                                       If commandBox.Text.Trim <> "" Then
                                                           Try
                                                               CurrentListLocation = -1
                                                               Select Case ConsoleMode
                                                                   Case True
                                                                       If commandBox.Text.StartsWith("/") Then
                                                                           If server.ServerVersionType = Server.EServerVersionType.CraftBukkit OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Spigot OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Spigot_Git OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Paper OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Akarin OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Cauldron OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Thermos OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Contigo OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Kettle Then
                                                                           End If
                                                                           'backgroundProcess.StandardInput.WriteLine(commandBox.Text.Substring(1))
                                                                           ownedWriterForServers(server).WriteLine(commandBox.Text.Substring(1))
                                                                           If InputList.Count <= 0 OrElse InputList.Last <> commandBox.Text.Substring(1) Then
                                                                               InputList.Add(commandBox.Text.Substring(1))
                                                                           End If
                                                                       Else
                                                                           'backgroundProcess.StandardInput.WriteLine("say " & commandBox.Text)
                                                                           ownedWriterForServers(server).WriteLine("say " & commandBox.Text)
                                                                           If InputList.Count <= 0 OrElse InputList.Last <> "say " & commandBox.Text Then
                                                                               InputList.Add("say " & commandBox.Text)
                                                                           End If
                                                                       End If
                                                                       commandBox.Clear()
                                                                   Case False
                                                                       If server.ServerVersionType = Server.EServerVersionType.CraftBukkit OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Spigot OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Spigot_Git OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Paper OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Akarin OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Cauldron OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Thermos OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Contigo OrElse
                                                               server.ServerVersionType = Server.EServerVersionType.Kettle Then
                                                                       End If
                                                                       'backgroundProcess.StandardInput.WriteLine(commandBox.Text)
                                                                       ownedWriterForServers(server).WriteLine(commandBox.Text)
                                                                       If InputList.Count <= 0 OrElse InputList.Last <> commandBox.Text Then
                                                                           InputList.Add(commandBox.Text)
                                                                       End If
                                                                       commandBox.Clear()
                                                               End Select
                                                           Catch ex As Exception
                                                           End Try
                                                       End If
                                                   Case Keys.Up
                                                       args.SuppressKeyPress = True
                                                       Dim tmp = commandBox.Text
                                                       Dim tmp2 = CurrentListLocation
                                                       Try
                                                           CurrentListLocation += 1
                                                           If ConsoleMode Then
                                                               Dim text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                                                               If text.StartsWith("say ") Then
                                                                   commandBox.Text = text.Substring(4)
                                                               Else
                                                                   commandBox.Text = "/" & text
                                                               End If
                                                           Else
                                                               commandBox.Text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                                                           End If
                                                       Catch ex As Exception
                                                           commandBox.Text = tmp
                                                           CurrentListLocation = tmp2
                                                       End Try
                                                   Case Keys.Down
                                                       args.SuppressKeyPress = True
                                                       Dim tmp = commandBox.Text
                                                       Dim tmp2 = CurrentListLocation
                                                       Try
                                                           CurrentListLocation -= 1
                                                           If CurrentListLocation < -1 Then
                                                               commandBox.Text = tmp
                                                               CurrentListLocation += 1
                                                           ElseIf CurrentListLocation = -1 Then
                                                               commandBox.Text = ""
                                                           Else
                                                               If ConsoleMode Then
                                                                   Dim text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                                                                   If text.StartsWith("say ") Then
                                                                       commandBox.Text = text.Substring(4)
                                                                   Else
                                                                       commandBox.Text = "/" & text
                                                                   End If
                                                               Else
                                                                   commandBox.Text = InputList.Item(InputList.Count - CurrentListLocation - 1)
                                                               End If
                                                           End If
                                                       Catch ex As Exception
                                                           commandBox.Text = tmp
                                                           CurrentListLocation = tmp2
                                                       End Try
                                                   Case Keys.S
                                                       If args.Control = True And args.Alt = False And args.Shift = False Then
                                                           args.Handled = True
                                                           args.SuppressKeyPress = True
                                                           ConsoleMode = Not ConsoleMode
                                                           Select Case ConsoleMode
                                                               Case True
                                                                   ToolTip1.SetToolTip(commandBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式" & vbNewLine & "按Ctrl + K 以開啟/關閉快捷功能表")
                                                               Case False
                                                                   ToolTip1.SetToolTip(commandBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式" & vbNewLine & "按Ctrl + K 以開啟/關閉快捷功能表")
                                                           End Select
                                                       End If
                                                   Case Keys.K
                                                       If args.Control = True And args.Alt = False And args.Shift = False Then
                                                           args.Handled = True
                                                           args.SuppressKeyPress = True
                                                           If menu Is Nothing OrElse menu.IsDisposed Then
                                                               menu = New TableLayoutPanel
                                                               For Each task In ownedTaskForServers(server)
                                                                   If task.Mode = ServerTask.TaskMode.QuickLaunch AndAlso task.Enabled = True Then
                                                                       BeginInvokeIfRequired(Me, Sub()
                                                                                                     Dim button As New Button() With {.Height = 30, .Text = task.Name, .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right}
                                                                                                     AddHandler button.Click, Sub()
                                                                                                                                  RunOwnedServerTask(page, task, New Dictionary(Of String, String))
                                                                                                                              End Sub
                                                                                                     menu.Controls.Add(button, 0, menu.RowCount)
                                                                                                 End Sub)
                                                                   End If
                                                               Next
                                                               menu.Height = dataListView.Height
                                                               menu.Width = Math.Min(dataListView.Width / 2, 100)
                                                               menu.Location = New Point(Me.Location.X - menu.Width - 5, dataListView.Location.Y)
                                                               menu.Dock = DockStyle.Right
                                                               page.Controls.Add(menu)
                                                               menu.AutoScroll = True
                                                               menu.Show()
                                                               menu.SendToBack()
                                                           Else
                                                               If menu.Visible Then
                                                                   page.Controls.Remove(menu)
                                                                   menu.Dispose()
                                                                   menu = Nothing
                                                               Else
                                                                   menu.Show()
                                                               End If
                                                           End If
                                                       End If
                                                   Case Keys.Back
                                                       If args.Control = True And args.Alt = False And args.Shift = False Then
                                                           args.Handled = True
                                                           args.SuppressKeyPress = True
                                                           BeginInvokeIfRequired(Me, Sub() commandBox.Clear())
                                                       End If
                                               End Select
                                           End Sub
            Select Case ConsoleMode
                Case True
                    ToolTip1.SetToolTip(commandBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式" & vbNewLine & "按Ctrl + K 以開啟/關閉快捷功能表")
                Case False
                    ToolTip1.SetToolTip(commandBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式" & vbNewLine & "按Ctrl + K 以開啟/關閉快捷功能表")
            End Select
        Else
            dataListView = CType(page.Controls(0).Controls(0), ListView)
            commandBox = CType(page.Controls(0).Controls(1), MetroFramework.Controls.MetroTextBox)
        End If
        page.Tag = (server, changeProcess)
        Dim alternateInputWriter As New IO.StreamWriter(changeProcess.StandardInput.BaseStream, New Text.UTF8Encoding(False)) With {.AutoFlush = True}
        If ownedWriterForServers.ContainsKey(server) Then
            ownedWriterForServers(server) = alternateInputWriter
        Else
            ownedWriterForServers.Add(server, alternateInputWriter)
        End If
        If justChangeProcess = False Then
            If CType(page.Controls(0).Controls(2).Controls(0), CheckBox).Checked = False Then
                changeProcess.BeginErrorReadLine()
                changeProcess.BeginOutputReadLine()
            Else
                changeProcess.CancelErrorRead()
                changeProcess.CancelOutputRead()
            End If
        End If
        AddHandler changeProcess.ErrorDataReceived, Sub(sender, e)
                                                        Try
                                                            If IsNothing(changeProcess) = False AndAlso changeProcess.HasExited = False Then
                                                                If IsNothing(e.Data) = False Then
                                                                    Task.Run(Sub()
                                                                                 If Not (ownedConsole.ContainsKey(server) = False OrElse ownedConsole(server).IsDisposed) Then
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

                                                                                 BeginInvokeIfRequired(dataListView, Sub()
                                                                                                                         SyncLock dataListView
                                                                                                                             dataListView.Items.Add(item)
                                                                                                                             Try
                                                                                                                                 If dataListView.GetItemRect(dataListView.Items.Count - 2).Y < dataListView.Height Then item.EnsureVisible()
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
                                                                                  If Not (ownedConsole.ContainsKey(server) = False OrElse ownedConsole(server).IsDisposed) Then
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
                                                                                      Case MCServerMessageType.Notify
                                                                                          item.ForeColor = Color.Blue
                                                                                  End Select
                                                                                  Select Case msg.MessageType
                                                                                      Case MCMessageType.PlayerLogin
                                                                                          If NotifyChooseListBox.CheckedIndices.Contains(0) Then _
                                                                                                             NotifyInfoMessage(msg.AddtionalMessage("player") & " 進入 " & bServer.ServerAlias, Text)
                                                                                          BeginInvoke(Sub() PlayerListBox.Items.Add(msg.AddtionalMessage("player") & " (" & bServer.ServerAlias & ")"))
                                                                                          For Each task In ownedTaskForServers(server)
                                                                                              If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                                                                                                                       task.TriggerEvent = ServerTask.TaskTriggerEvent.PlayerLogin Then
                                                                                                  If task.Enabled = True Then
                                                                                                      RunOwnedServerTask(page, task, msg.AddtionalMessage)
                                                                                                  End If
                                                                                              End If
                                                                                          Next
                                                                                      Case MCMessageType.PlayerLogout
                                                                                          If NotifyChooseListBox.CheckedIndices.Contains(1) Then _
                                                                                                             NotifyInfoMessage(msg.AddtionalMessage("player") & " 離開 " & bServer.ServerAlias, Text)
                                                                                          BeginInvoke(Sub() PlayerListBox.Items.Remove(msg.AddtionalMessage("player") & " (" & bServer.ServerAlias & ")"))
                                                                                          For Each task In ownedTaskForServers(server)
                                                                                              If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                                                                                                                       task.TriggerEvent = ServerTask.TaskTriggerEvent.PlayerLogout Then
                                                                                                  If task.Enabled = True Then
                                                                                                      RunOwnedServerTask(page, task, msg.AddtionalMessage)
                                                                                                  End If
                                                                                              End If
                                                                                          Next
                                                                                      Case MCMessageType.PlayerInputCommand
                                                                                          For Each task In ownedTaskForServers(server)
                                                                                              If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                                                                                                           task.TriggerEvent = ServerTask.TaskTriggerEvent.PlayerInputCommand Then
                                                                                                  If task.Enabled = True Then
                                                                                                      Dim testRegex As New Text.RegularExpressions.Regex(task.CheckRegex)
                                                                                                      If String.IsNullOrWhiteSpace(task.CheckRegex) OrElse
                                                                                                      (testRegex.IsMatch(msg.AddtionalMessage("command")) AndAlso testRegex.Match(msg.AddtionalMessage("command")).Value = msg.AddtionalMessage("command")) Then
                                                                                                          RunOwnedServerTask(page, task, msg.AddtionalMessage)
                                                                                                      End If
                                                                                                  End If
                                                                                              End If
                                                                                          Next
                                                                                      Case Else
                                                                                  End Select
                                                                                  Try
                                                                                      BeginInvokeIfRequired(dataListView, Sub()
                                                                                                                              SyncLock dataListView
                                                                                                                                  dataListView.Items.Add(item)
                                                                                                                                  Try
                                                                                                                                      If dataListView.GetItemRect(dataListView.Items.Count - 2).Y < dataListView.Height Then item.EnsureVisible()
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
                                             Try
                                                 If ownedTaskForServers(server) IsNot Nothing Then
                                                     For Each task In ownedTaskForServers(server)
                                                         If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                task.TriggerEvent = ServerTask.TaskTriggerEvent.ServerClosed Then
                                                             If task.Enabled = True Then
                                                                 RunOwnedServerTask(page, task, New Dictionary(Of String, String))
                                                             End If
                                                         End If
                                                     Next
                                                 End If
                                             Catch ex As Exception
                                             End Try
                                             bServer.Server.IsRunning = False
                                             bServer.Server.ProcessID = 0
                                         End Sub
        ownedProcesses.Add(changeProcess)
        If changeProcess IsNot Nothing Then bServer.Server.ProcessID = changeProcess.Id
    End Sub
    Sub RunOwnedServerTask(page As TabPage, task As ServerTask, AddtionalParameters As Dictionary(Of String, String))
        Dim TaskRandomGenerator As New Random
        Static TaskRandomGenNumber As Integer = -1
        Dim server As Server = CType(page.Tag, ValueTuple(Of Server, Process)).Item1
        Select Case task.Command.Action
            Case ServerTask.TaskCommand.CommandAction.StopServer
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
                                                       server.IsRunning = False
                                                   End Sub) With {
    .Name = "Server Manager Close Server Thread",
    .IsBackground = True
                                                             }
                thread.Start()
            Case ServerTask.TaskCommand.CommandAction.RestartServer

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
                                                       BeginInvokeIfRequired(Me, Sub() RestartButton_Click(Me, New EventArgs))
                                                   End Sub) With {
            .Name = "Server Manager Restart Server Thread",
            .IsBackground = True
                                                                     }
                thread.Start()
            Case ServerTask.TaskCommand.CommandAction.RunCommand
                If backgroundProcess IsNot Nothing Then
                    If backgroundProcess.HasExited = False Then
                        Try
                            Dim command As String = task.Command.Data
                            Select Case task.TriggerEvent
                                Case ServerTask.TaskTriggerEvent.PlayerLogin
                                    command = command.Replace("<$ID>", AddtionalParameters("player"))
                                Case ServerTask.TaskTriggerEvent.PlayerLogout
                                    command = command.Replace("<$ID>", AddtionalParameters("player"))
                                Case ServerTask.TaskTriggerEvent.PlayerInputCommand
                                    Dim playerCommand As String = AddtionalParameters("command")
                                    Dim commandName As String = IIf(playerCommand.Contains(" "), playerCommand.Split(New Char() {" "}, 2, StringSplitOptions.None)(0), playerCommand)
                                    Dim commandArg As String = ""
                                    Try
                                        commandArg = IIf(playerCommand.Contains(" "), playerCommand.Split(New Char() {" "}, 2, StringSplitOptions.None)(1), "")
                                    Catch ex As Exception

                                    End Try
                                    command = command.Replace("<$ID>", AddtionalParameters("player"))
                                    command = command.Replace("<$COMMANDNAME>", commandName)
                                    command = command.Replace("<$COMMANDARG>", commandArg)
                                    command = command.Replace("<$COMMANDFULL>", playerCommand)
                                    Dim time = Now
                                    command = command.Replace("<#YEAR>", time.Year)
                                    command = command.Replace("<#MONTH>", time.Month)
                                    command = command.Replace("<#DAY>", time.Day)
                                    command = command.Replace("<#HOUR>", time.Hour)
                                    command = command.Replace("<#MINUTE>", time.Minute)
                                    command = command.Replace("<#SECOND>", time.Second)
                                    command = command.Replace("<#DAYOFWEEK>", CInt(time.DayOfWeek))
                            End Select
                            Dim _thread As New Threading.Thread(Sub()
                                                                    Try
                                                                        Dim SleepRegex As New Text.RegularExpressions.Regex("#sleep [0-9]{1,}")
                                                                        Dim RandomiseRegex As New Text.RegularExpressions.Regex("#randomise [0-9]{1,}")
                                                                        If command.Contains(vbNewLine) Then
                                                                            For Each line In command.Split(vbNewLine)
                                                                                line = line.TrimStart(vbLf)
                                                                                If line.StartsWith("#") Then
                                                                                    If SleepRegex.IsMatch(line) AndAlso SleepRegex.Match(line).Value = line.Trim Then
                                                                                        SpinWait.SpinUntil(Function() False, 50 * New Text.RegularExpressions.Regex("[0-9]{1,}").Match(line).Value)
                                                                                    ElseIf RandomiseRegex.IsMatch(line) AndAlso RandomiseRegex.Match(line).Value = line.Trim Then
                                                                                        TaskRandomGenNumber = New Text.RegularExpressions.Regex("[0-9]{1,}").Match(line).Value
                                                                                    ElseIf command.StartsWith("#backup ") AndAlso String.IsNullOrWhiteSpace(command.Substring(8)) = False Then
                                                                                        Try
                                                                                            BackupServer(page, server, command.Substring(8))
                                                                                        Catch ex As Exception

                                                                                        End Try
                                                                                    End If
                                                                                Else
                                                                                    line.Replace("<#RANDOM>", IIf(TaskRandomGenNumber > -1, TaskRandomGenerator.Next(TaskRandomGenNumber), 0))
                                                                                    ownedWriterForServers(server).WriteLine(line)
                                                                                End If
                                                                            Next
                                                                        Else
                                                                            command = command.TrimStart(vbLf)
                                                                            If command.StartsWith("#") Then
                                                                                If SleepRegex.IsMatch(command) AndAlso SleepRegex.Match(command).Value = command.Trim Then
                                                                                    SpinWait.SpinUntil(Function() False, 50 * New Text.RegularExpressions.Regex("[0-9]{1,}").Match(command).Value)
                                                                                ElseIf RandomiseRegex.IsMatch(command) AndAlso RandomiseRegex.Match(command).Value = command.Trim Then
                                                                                    TaskRandomGenNumber = New Text.RegularExpressions.Regex("[0-9]{1,}").Match(command).Value
                                                                                ElseIf command.StartsWith("#backup ") AndAlso String.IsNullOrWhiteSpace(command.Substring(8)) = False Then
                                                                                    Try
                                                                                        BackupServer(page, server, command.Substring(8))
                                                                                    Catch ex As Exception

                                                                                    End Try
                                                                                End If
                                                                            Else
                                                                                command.Replace("<#RANDOM>", IIf(TaskRandomGenNumber > -1, TaskRandomGenerator.Next(TaskRandomGenNumber), 0))
                                                                                ownedWriterForServers(server).WriteLine(command)
                                                                            End If
                                                                        End If
                                                                    Catch ex As Exception

                                                                    End Try
                                                                End Sub)
                            _thread.IsBackground = True
                            _thread.Start()
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Case ServerTask.TaskCommand.CommandAction.BackupServer
                Dim _thread As New Thread(Sub()
                                              Try
                                                  If String.IsNullOrWhiteSpace(task.Command.Data) = False Then
                                                      BackupServer(page, server, task.Command.Data)
                                                  End If
                                              Catch ex As Exception

                                              End Try
                                          End Sub)
                _thread.IsBackground = True
                _thread.Start()
        End Select
    End Sub
    Sub BackupServer(page As TabPage, server As Server, path As String)
        Dim msg As New MinecraftLogParser.MinecraftConsoleMessage
        msg.ServerMessageType = MCServerMessageType.Notify
        msg.Message = "伺服器備份中..."
        msg.Time = Now
        msg.Thread = Application.ProductName
        msg.MessageType = MCMessageType.None
        If Not (ownedConsole.ContainsKey(server) = False OrElse ownedConsole(server).IsDisposed) Then
            ownedConsole(server).InputMessageToListView(msg)
        End If
        Dim item As New ListViewItem(msg.ServerMessageTypeString)
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
                item.SubItems.Add(msg.Thread)
        End Select
        item.ForeColor = Color.Blue
        item.SubItems.Add(msg.Message)
        item.SubItems.Add(String.Format("{0}:{1}:{2}", msg.Time.Hour.ToString.PadLeft(2, "0"), msg.Time.Minute.ToString.PadLeft(2, "0"), msg.Time.Second.ToString.PadLeft(2, "0")))
        Try
            BeginInvokeIfRequired(Me, Sub() CType(page.Controls(0).Controls(0), ListView).Items.Add(item))
        Catch ex As Exception
        End Try
        Try
            My.Computer.FileSystem.CopyDirectory(server.ServerPath, path)
        Catch ex As Exception

        End Try
        Dim msg2 As New MinecraftLogParser.MinecraftConsoleMessage
        msg2.ServerMessageType = MCServerMessageType.Notify
        msg2.Message = "伺服器備份完成..."
        msg2.Time = Now
        msg2.Thread = Application.ProductName
        msg2.MessageType = MCMessageType.None
        If Not (ownedConsole.ContainsKey(server) = False OrElse ownedConsole(server).IsDisposed) Then
            ownedConsole(server).InputMessageToListView(msg)
        End If
        Dim item2 As New ListViewItem(msg.ServerMessageTypeString)
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
                item2.SubItems.Add(msg.Thread)
        End Select
        item2.ForeColor = Color.Blue
        item2.SubItems.Add(msg.Message)
        item2.SubItems.Add(String.Format("{0}:{1}:{2}", msg2.Time.Hour.ToString.PadLeft(2, "0"), msg2.Time.Minute.ToString.PadLeft(2, "0"), msg2.Time.Second.ToString.PadLeft(2, "0")))
        Try
            BeginInvokeIfRequired(Me, Sub() CType(page.Controls(0).Controls(0), ListView).Items.Add(item2))
        Catch ex As Exception
        End Try
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
                                                                                         item.ForeColor = Color.Red
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
                                                                                          Dim item As New ListViewItem(IIf(String.IsNullOrEmpty(msg.BungeeCordMessageType) = False, msg.BungeeCordMessageType, msg.ServerMessageTypeString).ToString.TrimStart("[").TrimEnd("]"))
                                                                                          item.SubItems.Add(msg.Message)
                                                                                          If String.IsNullOrEmpty(msg.BungeeCordMessageType) = False Then
                                                                                              Select Case item.Text
                                                                                                  Case "警告"
                                                                                                      item.ForeColor = Color.Orange
                                                                                                  Case "錯誤"
                                                                                                      item.ForeColor = Color.Red
                                                                                              End Select
                                                                                          Else
                                                                                              Select Case msg.ServerMessageType
                                                                                                  Case MCServerMessageType.Warning
                                                                                                      item.ForeColor = Color.Orange
                                                                                                  Case MCServerMessageType.Error
                                                                                                      item.ForeColor = Color.Red
                                                                                                  Case MCServerMessageType.Debug
                                                                                                      item.ForeColor = Color.YellowGreen
                                                                                                  Case MCServerMessageType.Trace
                                                                                                      item.ForeColor = Color.Green
                                                                                                  Case MCServerMessageType.Notify
                                                                                                      item.ForeColor = Color.Blue
                                                                                              End Select
                                                                                          End If
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
        BeginInvokeIfRequired(Me, Sub() ServerStatusLabel.Text = Host.BungeeType.ToString & " 狀態：啟動")
        Host.IsRunning = True
        backgroundProcess.EnableRaisingEvents = True

        AddHandler backgroundProcess.Exited, Sub(sender, e)

                                                 TaskTimer.Enabled = False
                                                 If IsDisposed = False Then
                                                     BeginInvokeIfRequired(Me, Sub() ServerStatusLabel.Text = Host.BungeeType.ToString & " 狀態：關閉")
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
            Case Keys.Back
                If e.Control = True And e.Alt = False And e.Shift = False Then
                    e.Handled = True
                    e.SuppressKeyPress = True
                    BeginInvokeIfRequired(Me, Sub() CommandTextBox.Clear())
                End If
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
        Dim server As Server = CType(MainTabControl.SelectedTab.Tag, ValueTuple(Of Server, Process)).Item1
        Dim currentPage As TabPage = MainTabControl.SelectedTab
        If ownedConsole.ContainsKey(server) = False OrElse
                ownedConsole(server).IsDisposed Then
            Dim console As New ServerConsole(server, currentPage.Text, CloneListViewItemCollectionAndChangeToServerConsoleFormat(CType(currentPage.Controls(0).Controls(0), ListView).Items), CType(currentPage.Tag, ValueTuple(Of Server, Process)).Item2, ownedTaskForServers(server), ownedTasksAndTimersForServers(server), CType(currentPage.Controls(0).Controls(2).Controls(0), CheckBox).Checked)
            If ownedConsole.ContainsKey(server) Then
                ownedConsole(server) = console
            Else
                ownedConsole.Add(server, console)
            End If
            AddHandler console.ServerRestarted, Sub(ByRef process As Process)
                                                    RunOwnedServer(Nothing, True, currentPage, process)
                                                End Sub
            AddHandler console.ServerStopLoadingStateChanged, Sub(state)
                                                                  BeginInvokeIfRequired(Me, Sub() CType(currentPage.Controls(0).Controls(2).Controls(0), CheckBox).Checked = state)
                                                              End Sub
            console.Show()
        End If
    End Sub
    Shared Function CloneListViewItemCollectionAndChangeToServerConsoleFormat(collection As ListView.ListViewItemCollection) As ListViewItem()
        Dim newList As New List(Of ListViewItem)
        For Each item As ListViewItem In collection
            item = item.Clone()
            Dim subitem = item.SubItems(2)
            item.SubItems(2) = item.SubItems(3)
            item.SubItems(3) = subitem
            newList.Add(item)
        Next
        Return newList.ToArray
    End Function
End Class