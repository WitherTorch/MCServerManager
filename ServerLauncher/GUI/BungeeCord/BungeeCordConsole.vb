Imports System.ComponentModel
Imports System.Threading.Tasks
Imports ServerManager.MinecraftLogParser.MinecraftConsoleMessage

Public Class BungeeCordConsole

    Dim backgroundProcess As Process
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
            Dim server = bServer.Server
            Dim page As New TabPage(bServer.ServerAlias & " (" & server.ServerPathName & ") 資料列表") With {.Size = New Size(792, 424)}
            Dim layout As New Panel() With {.Dock = DockStyle.Fill}
            Dim commandBox As New TextBox() With {.Dock = DockStyle.Bottom}
            Dim dataListView As New ListView()
            Dim topPanel As New Panel()
            Dim pauseLoad As New CheckBox()
            Dim _inputList As New List(Of String)()
            Dim _currentListLocation As Integer = -1

            dataListView.Columns.AddRange(New ColumnHeader() {New ColumnHeader() With {.Text = "類型"}, New ColumnHeader() With {.Text = "時間"}, New ColumnHeader() With {.Text = "訊息", .Width = 656}})
            dataListView.Dock = DockStyle.Fill
            dataListView.FullRowSelect = True
            dataListView.GridLines = True
            dataListView.MultiSelect = False
            dataListView.UseCompatibleStateImageBehavior = False
            dataListView.View = View.Details

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
            Dim process = bServer.RunServer()
            Select Case My.Settings.ConsoleInputChat
                Case True
                    ToolTip1.SetToolTip(commandBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式")
                Case False
                    ToolTip1.SetToolTip(commandBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式")
            End Select
            AddHandler commandBox.KeyDown, Sub(sender, e)
                                               Select Case e.KeyCode
                                                   Case Keys.Enter
                                                       e.SuppressKeyPress = True
                                                       If commandBox.Text.Trim <> "" Then
                                                           _currentListLocation = -1
                                                           Select Case My.Settings.ConsoleInputChat
                                                               Case True
                                                                   If commandBox.Text.StartsWith("/") Then
                                                                       process.StandardInput.WriteLine(commandBox.Text.Substring(1))
                                                                       If InputList.Count <= 0 OrElse InputList.Last <> commandBox.Text.Substring(1) Then
                                                                           InputList.Add(commandBox.Text.Substring(1))
                                                                       End If
                                                                   Else
                                                                       process.StandardInput.WriteLine("say " & commandBox.Text)
                                                                       If InputList.Count <= 0 OrElse InputList.Last <> "say " & commandBox.Text Then
                                                                           InputList.Add("say " & commandBox.Text)
                                                                       End If
                                                                   End If
                                                               Case False
                                                                   process.StandardInput.WriteLine(commandBox.Text)
                                                                   If InputList.Count <= 0 OrElse InputList.Last <> commandBox.Text Then
                                                                       InputList.Add(commandBox.Text)
                                                                   End If
                                                           End Select
                                                           commandBox.Clear()
                                                       End If
                                                   Case Keys.Up
                                                       e.SuppressKeyPress = True
                                                       Dim tmp = commandBox.Text
                                                       Dim tmp2 = _currentListLocation
                                                       Try
                                                           _currentListLocation += 1
                                                           If My.Settings.ConsoleInputChat Then
                                                               Dim text = InputList.Item(InputList.Count - _currentListLocation - 1)
                                                               If text.StartsWith("say ") Then
                                                                   commandBox.Text = text.Substring(4)
                                                               Else
                                                                   commandBox.Text = "/" & text
                                                               End If
                                                           Else
                                                               commandBox.Text = InputList.Item(InputList.Count - _currentListLocation - 1)
                                                           End If
                                                       Catch ex As Exception
                                                           commandBox.Text = tmp
                                                           _currentListLocation = tmp2
                                                       End Try
                                                   Case Keys.Down
                                                       e.SuppressKeyPress = True
                                                       Dim tmp = commandBox.Text
                                                       Dim tmp2 = _currentListLocation
                                                       Try
                                                           _currentListLocation -= 1
                                                           If _currentListLocation < -1 Then
                                                               commandBox.Text = tmp
                                                               _currentListLocation += 1
                                                           ElseIf _currentListLocation = -1 Then
                                                               commandBox.Text = ""
                                                           Else
                                                               If My.Settings.ConsoleInputChat Then
                                                                   Dim text = InputList.Item(InputList.Count - _currentListLocation - 1)
                                                                   If text.StartsWith("say ") Then
                                                                       commandBox.Text = text.Substring(4)
                                                                   Else
                                                                       commandBox.Text = "/" & text
                                                                   End If
                                                               Else
                                                                   commandBox.Text = InputList.Item(InputList.Count - _currentListLocation - 1)
                                                               End If
                                                           End If
                                                       Catch ex As Exception
                                                           commandBox.Text = tmp
                                                           _currentListLocation = tmp2
                                                       End Try
                                                   Case Keys.S
                                                       If e.Control = True And e.Shift = False And e.Alt = False Then
                                                           e.Handled = True
                                                           My.Settings.ConsoleInputChat = Not My.Settings.ConsoleInputChat
                                                           Select Case My.Settings.ConsoleInputChat
                                                               Case True
                                                                   ToolTip1.SetToolTip(commandBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式")
                                                               Case False
                                                                   ToolTip1.SetToolTip(commandBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式")
                                                           End Select
                                                       End If
                                               End Select
                                           End Sub
            AddHandler pauseLoad.CheckedChanged, Sub()
                                                     If pauseLoad.Checked = False Then
                                                         process.BeginErrorReadLine()
                                                         process.BeginOutputReadLine()
                                                     Else
                                                         process.CancelErrorRead()
                                                         process.CancelOutputRead()
                                                     End If
                                                 End Sub
            If pauseLoad.Checked = False Then
                process.BeginErrorReadLine()
                process.BeginOutputReadLine()
            Else
                process.CancelErrorRead()
                process.CancelOutputRead()
            End If
            AddHandler process.ErrorDataReceived, Sub(sender, e)
                                                      Try
                                                          If IsNothing(Process) = False AndAlso Process.HasExited = False Then
                                                              If IsNothing(e.Data) = False Then
                                                                  Task.Run(Sub()

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
                                                                                   Case Else
                                                                                       item.SubItems.Add("")
                                                                               End Select
                                                                               Dim nowTime = Now
                                                                               item.SubItems.Add(String.Format("{0}:{1}:{2}", nowTime.Hour.ToString.PadLeft(2, "0"), nowTime.Minute.ToString.PadLeft(2, "0"), nowTime.Second.ToString.PadLeft(2, "0")))
                                                                               item.SubItems.Add(e.Data)
                                                                               If NotifyChooseListBox.CheckedIndices.Contains(3) Then _
                                                                                                                    NotifyInfoMessage(bServer.ServerAlias & " 發出錯誤訊息:" & vbNewLine & e.Data, Text)

                                                                               If InvokeRequired Then
                                                                                   BeginInvoke(Sub()
                                                                                                   Dim seekToBottom As Boolean = False
                                                                                                   If dataListView.Items.Count > 1 Then
                                                                                                       Dim first As Integer = dataListView.TopItem.Index
                                                                                                       Dim h_tot As Integer = dataListView.ClientRectangle.Height - 1
                                                                                                       Dim h_hdr As Integer = dataListView.GetItemRect(first).Y
                                                                                                       Dim h_item As Integer = dataListView.GetItemRect(0).Height
                                                                                                       Dim cntVis As Integer = (h_tot - h_hdr) / h_item
                                                                                                       Dim LastItemIndex = Math.Min(dataListView.Items.Count - 1, first + cntVis)
                                                                                                       If LastItemIndex = dataListView.Items.Count - 1 Then
                                                                                                           seekToBottom = True
                                                                                                       End If
                                                                                                   End If
                                                                                                   Invoke(Sub() dataListView.Items.Add(item))
                                                                                                   If seekToBottom Then dataListView.EnsureVisible(item.Index)
                                                                                               End Sub)
                                                                               Else
                                                                                   Dim seekToBottom As Boolean = False
                                                                                   If dataListView.Items.Count > 1 Then
                                                                                       Dim first As Integer = dataListView.TopItem.Index
                                                                                       Dim h_tot As Integer = dataListView.ClientRectangle.Height - 1
                                                                                       Dim h_hdr As Integer = dataListView.GetItemRect(first).Y
                                                                                       Dim h_item As Integer = dataListView.GetItemRect(0).Height
                                                                                       Dim cntVis As Integer = (h_tot - h_hdr) / h_item
                                                                                       Dim LastItemIndex = Math.Min(dataListView.Items.Count - 1, first + cntVis)
                                                                                       If LastItemIndex = dataListView.Items.Count - 1 Then
                                                                                           seekToBottom = True
                                                                                       End If
                                                                                   End If
                                                                                   dataListView.Items.Add(item)
                                                                                   If seekToBottom Then dataListView.EnsureVisible(item.Index)
                                                                               End If
                                                                           End Sub)
                                                              End If
                                                          End If
                                                      Catch ex As Exception
                                                      End Try
                                                  End Sub
            AddHandler process.OutputDataReceived, Sub(sender, e)
                                                       Try
                                                           If IsNothing(Process) = False AndAlso Process.HasExited = False Then
                                                               If IsNothing(e.Data) = False Then
                                                                   Task.Run(Sub()
                                                                                Dim msg = MinecraftLogParser.ToConsoleMessage(e.Data, Now)
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
                                                                                If InvokeRequired Then
                                                                                    BeginInvoke(Sub()
                                                                                                    Dim seekToBottom As Boolean = False
                                                                                                    If dataListView.Items.Count > 1 Then
                                                                                                        Dim first As Integer = dataListView.TopItem.Index
                                                                                                        Dim h_tot As Integer = dataListView.ClientRectangle.Height - 1
                                                                                                        Dim h_hdr As Integer = dataListView.GetItemRect(first).Y
                                                                                                        Dim h_item As Integer = dataListView.GetItemRect(0).Height
                                                                                                        Dim cntVis As Integer = (h_tot - h_hdr) / h_item
                                                                                                        Dim LastItemIndex = Math.Min(dataListView.Items.Count - 1, first + cntVis)
                                                                                                        If LastItemIndex = dataListView.Items.Count - 1 Then
                                                                                                            seekToBottom = True
                                                                                                        End If
                                                                                                    End If
                                                                                                    Invoke(Sub() dataListView.Items.Add(item))
                                                                                                    If seekToBottom Then dataListView.EnsureVisible(item.Index)
                                                                                                End Sub)
                                                                                Else
                                                                                    Dim seekToBottom As Boolean = False
                                                                                    If dataListView.Items.Count > 1 Then
                                                                                        Dim first As Integer = dataListView.TopItem.Index
                                                                                        Dim h_tot As Integer = dataListView.ClientRectangle.Height - 1
                                                                                        Dim h_hdr As Integer = dataListView.GetItemRect(first).Y
                                                                                        Dim h_item As Integer = dataListView.GetItemRect(0).Height
                                                                                        Dim cntVis As Integer = (h_tot - h_hdr) / h_item
                                                                                        Dim LastItemIndex = Math.Min(dataListView.Items.Count - 1, first + cntVis)
                                                                                        If LastItemIndex = dataListView.Items.Count - 1 Then
                                                                                            seekToBottom = True
                                                                                        End If
                                                                                    End If
                                                                                    dataListView.Items.Add(item)
                                                                                    If seekToBottom Then dataListView.EnsureVisible(item.Index)
                                                                                End If
                                                                            End Sub)
                                                               End If
                                                           End If
                                                       Catch ex As Exception
                                                       End Try
                                                   End Sub
            AddHandler process.Exited, Sub(sender, e)
                                           bServer.Server.IsRunning = False
                                           bServer.Server.ProcessID = 0
                                       End Sub
            ownedProcesses.Add(process)
            If process IsNot Nothing Then bServer.Server.ProcessID = process.Id
        Next
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
                                                GlobalModule.Manager.RunningBungeeCord = False
                                            End Sub) With {
            .Name = "Server Manager Close BungeeCord Thread",
            .IsBackground = False
                                                                     }
        Cthread.Start()
    End Sub

End Class