﻿Imports System.ComponentModel
Imports System.Threading
Imports System.Threading.Tasks
Imports ServerManager.MinecraftLogParser.MinecraftConsoleMessage

Public Class ServerConsole
    Dim TaskList As New List(Of ServerTask)
    Dim ThreadTaskDictionary As New Dictionary(Of ServerTask, System.Windows.Forms.Timer)
    Dim InputList As New List(Of String)()
    Dim CurrentListLocation As Integer = -1
    Dim backgroundProcess As Process
    Public ReadOnly Property Server As Server
    Dim startInfo As ProcessStartInfo
    Dim usesType As Server.EServerVersionType
    Public Sub New(Server As Server, Optional usesServerVersionType As Server.EServerVersionType = Server.EServerVersionType.Error)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = Server
        If usesServerVersionType <> Server.EServerVersionType.Error Then
            Text = "伺服器控制台 - " & Server.ServerPathName & " [" & GetSimpleVersionName(usesServerVersionType) & " 模式" & "]"
            usesType = usesServerVersionType
        Else
            If Server.ServerVersionType = Server.EServerVersionType.Spigot_Git Then
                usesType = Server.EServerVersionType.Spigot
            End If
            Text = "伺服器控制台 - " & Server.ServerPathName
        End If
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
        Select Case Server.ServerVersionType
            Case Server.EServerVersionType.Spigot
                DataListView.Columns.Remove(ColumnHeader2)
            Case Server.EServerVersionType.CraftBukkit
                DataListView.Columns.Remove(ColumnHeader2)
            Case Server.EServerVersionType.Nukkit
                DataListView.Columns.Remove(ColumnHeader2)
            Case Server.EServerVersionType.VanillaBedrock
                DataListView.Columns.Remove(ColumnHeader2)
            Case Server.EServerVersionType.Paper
                DataListView.Columns.Remove(ColumnHeader2)
            Case Server.EServerVersionType.Akarin
                DataListView.Columns.Remove(ColumnHeader2)
            Case Server.EServerVersionType.Spigot_Git
                DataListView.Columns.Remove(ColumnHeader2)
            Case Server.EServerVersionType.Cauldron
                DataListView.Columns.Remove(ColumnHeader2)
        End Select
        TaskTimer.Enabled = True
        TaskTimer.Start()
        Run()
        TaskList = Server.ServerTasks.ToList
        For Each task In TaskList
            TaskListBox.SetItemChecked(TaskListBox.Items.Add(task.Name), task.Enabled)
        Next
    End Sub
    Private Overloads Sub Run()
        If Server.ServerVersionType = Server.EServerVersionType.Error Then
            Using opn As New OpenFileDialog
                opn.InitialDirectory = Server.ServerPath
                opn.Title = "開啟程式"
                opn.Filter = "Java JAR 程式 (*.jar)|*.jar"
                If opn.ShowDialog = DialogResult.OK Then
                    Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & opn.FileName & """", Server.ServerPath)
                Else
                    Exit Sub
                End If
            End Using
        Else
            Select Case Server.ServerType
                Case Server.EServerType.Java
                    Select Case Server.ServerVersionType
                        Case Server.EServerVersionType.Vanilla
                            If Server.Server2ndVersion <> "" Then
                                Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "minecraft_server." & Server.Server2ndVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                            Else
                                Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "minecraft_server." & Server.ServerVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                            End If
                        Case Server.EServerVersionType.Forge
                            ' 1.1~1.2 > Server
                            ' 1.3 ~ > Universal
                            If New Version(Server.ServerVersion) >= New Version(1, 3) Then
                                If New Version(Server.ServerVersion) >= New Version(1, 13) Then
                                    Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                Else
                                    Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-universal" & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                End If
                            Else
                                Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-server" & ".jar") & """" & " nogui", Server.ServerPath)
                            End If
                        Case Server.EServerVersionType.Spigot
                            Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Spigot_Git
                            Select Case usesType
                                Case Server.EServerVersionType.Spigot
                                    Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                                Case Server.EServerVersionType.CraftBukkit
                                    Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "craftbukkit-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                                Case Else
                                    Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            End Select
                        Case Server.EServerVersionType.CraftBukkit
                            Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "craftbukkit-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.SpongeVanilla
                            Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spongeVanilla-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Paper
                            Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "paper-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Akarin
                            Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "akarin-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Cauldron
                            Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M " & JavaArguments & " -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "server.jar") & """", Server.ServerPath)
                    End Select
                Case Server.EServerType.Bedrock
                    Select Case Server.ServerVersionType
                        Case Server.EServerVersionType.Nukkit
                            '-Djline.terminal=jline.UnsupportedTerminal <- JLine Support
                            Run(IO.Path.Combine(JavaPath, "java.exe"), "-Djline.terminal=jline.UnsupportedTerminal -Xms" & GlobalModule.Manager.ServerMemoryMinBox.Value & "M -Xmx" & GlobalModule.Manager.ServerMemoryMaxBox.Value & "M " & JavaArguments & " -jar " & """" & IO.Path.Combine(Server.ServerPath, "nukkit-" & Server.Server2ndVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.VanillaBedrock
                            Run("""" & IO.Path.Combine(Server.ServerPath, "bedrock_server.exe") & """", "", Server.ServerPath, True, False)
                    End Select
            End Select
        End If
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
                                                                                             Select Case Server.ServerVersionType
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
                                                                                              Select Case Server.ServerVersionType
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
                                                                                                      For Each task In TaskList
                                                                                                          If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                                                                                                                       task.TriggerEvent = ServerTask.TaskTriggerEvent.PlayerLogin Then
                                                                                                              If task.Enabled = True Then
                                                                                                                  RunTask(task, msg.AddtionalMessage)
                                                                                                              End If
                                                                                                          End If
                                                                                                      Next
                                                                                                  Case MCMessageType.PlayerLogout
                                                                                                      If NotifyChooseListBox.CheckedIndices.Contains(1) Then _
                                                                                                                    NotifyInfoMessage(msg.AddtionalMessage("player") & " 離開伺服器", Text)
                                                                                                      BeginInvoke(Sub() PlayerListBox.Items.Remove(msg.AddtionalMessage("player")))
                                                                                                      For Each task In TaskList
                                                                                                          If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                                                                                                                       task.TriggerEvent = ServerTask.TaskTriggerEvent.PlayerLogout Then
                                                                                                              If task.Enabled = True Then
                                                                                                                  RunTask(task, msg.AddtionalMessage)
                                                                                                              End If
                                                                                                          End If
                                                                                                      Next
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
                                                 Try
                                                     If TaskList IsNot Nothing Then
                                                         For Each task In TaskList
                                                             If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                task.TriggerEvent = ServerTask.TaskTriggerEvent.ServerClosed Then
                                                                 If task.Enabled = True Then
                                                                     RunTask(task, New Dictionary(Of String, String))
                                                                 End If
                                                             End If
                                                         Next
                                                     End If
                                                 Catch ex As Exception
                                                 End Try
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
        Try
            If TaskList IsNot Nothing Then
                For Each task In TaskList
                    If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                    task.TriggerEvent = ServerTask.TaskTriggerEvent.ServerStarted Then
                        If task.Enabled = True Then
                            RunTask(task, New Dictionary(Of String, String))
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
        End Try
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
            Select Case Server.ServerVersionType
                Case Server.EServerVersionType.Spigot
                Case Server.EServerVersionType.CraftBukkit
                Case Server.EServerVersionType.Nukkit
                Case Server.EServerVersionType.VanillaBedrock
                Case Server.EServerVersionType.Paper
                Case Server.EServerVersionType.Akarin
                Case Else
                    spItem1.SubItems.Add(Application.ProductName)
            End Select
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
            Select Case Server.ServerVersionType
                Case Server.EServerVersionType.Spigot
                Case Server.EServerVersionType.CraftBukkit
                Case Server.EServerVersionType.Nukkit
                Case Server.EServerVersionType.VanillaBedrock
                Case Server.EServerVersionType.Paper
                Case Server.EServerVersionType.Akarin
                Case Else
                    item.SubItems.Add(msg.Thread)
            End Select
            item.ForeColor = Color.Blue
            item.SubItems.Add(msg.Message)
            item.SubItems.Add(String.Format("{0}:{1}:{2}", msg.Time.Hour.ToString.PadLeft(2, "0"), msg.Time.Minute.ToString.PadLeft(2, "0"), msg.Time.Second.ToString.PadLeft(2, "0")))
            Try
                DataListView.Items.Add(item)
            Catch ex As Exception
            End Try
            Dim spItem2 As New ListViewItem("通知")
            spItem2.ForeColor = Color.Blue
            Select Case Server.ServerVersionType
                Case Server.EServerVersionType.Spigot
                Case Server.EServerVersionType.CraftBukkit
                Case Server.EServerVersionType.Nukkit
                Case Server.EServerVersionType.VanillaBedrock
                Case Server.EServerVersionType.Paper
                Case Server.EServerVersionType.Akarin
                Case Else
                    spItem2.SubItems.Add(Application.ProductName)
            End Select
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
                                                                         Server.ServerTasks = TaskList.ToArray
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

    Private Sub CloseCheckBox_Click(sender As Object, e As EventArgs) Handles CloseCheckBox.Click

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

    Sub AddTask(task As ServerTask)
        BeginInvoke(Sub()
                        TaskList.Add(task)
                        TaskListBox.SetItemChecked(TaskListBox.Items.Add(task.Name), True)
                    End Sub)
    End Sub
    Sub RemoveTaskAt(index As Integer)
        BeginInvoke(Sub()
                        TaskList.RemoveAt(index)
                        TaskListBox.Items.RemoveAt(index)
                    End Sub)
    End Sub
    Private Sub AddTaskButton_Click(sender As Object, e As EventArgs) Handles AddTaskButton.Click
        Dim taskDialog As New ServerTaskCreateDialog(Me)
        taskDialog.ShowDialog()
    End Sub

    Private Sub TaskListBox_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles TaskListBox.ItemCheck
        Select Case e.NewValue
            Case CheckState.Checked
                If ThreadTaskDictionary.ContainsKey(TaskList(e.Index)) = False Then
                    Dim task As ServerTask = TaskList(e.Index)
                    task.Enabled = True
                    If task.Mode = ServerTask.TaskMode.Repeating Then
                        Dim timer As New System.Windows.Forms.Timer()
                        timer.Interval = task.RepeatingPeriod * GetBaseIntervalValue(task.RepeatingPeriodUnit)
                        ThreadTaskDictionary.Add(task, timer)
                        AddHandler timer.Tick, Sub()
                                                   RunTask(task, New Dictionary(Of String, String))
                                               End Sub
                        timer.Start()
                    End If
                End If
            Case CheckState.Unchecked
                TaskList(e.Index).Enabled = False
                If ThreadTaskDictionary.ContainsKey(TaskList(e.Index)) Then ThreadTaskDictionary.Remove(TaskList(e.Index))
        End Select
    End Sub
    Sub RunTask(task As ServerTask, AddtionalParameters As Dictionary(Of String, String))
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
                                                       Server.IsRunning = False
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
                            End Select
                            backgroundProcess.StandardInput.WriteLine(command)
                        Catch ex As Exception
                        End Try
                    End If
                End If
        End Select
    End Sub
    Function GetBaseIntervalValue(type As ServerTask.TaskPeriodUnit) As Integer
        Select Case type
            Case ServerTask.TaskPeriodUnit.Tick
                Return 50
            Case ServerTask.TaskPeriodUnit.Second
                Return 1000
            Case ServerTask.TaskPeriodUnit.Minute
                Return 60 * 1000
            Case ServerTask.TaskPeriodUnit.Hour
                Return 60 * 60 * 1000
            Case ServerTask.TaskPeriodUnit.Day
                Return 24 * 60 * 60 * 1000
        End Select
    End Function

    Private Sub EditTaskButton_Click(sender As Object, e As EventArgs) Handles EditTaskButton.Click
        If TaskListBox.SelectedIndex >= 0 Then
            Dim task As ServerTask = TaskList(TaskListBox.SelectedIndex)
            Dim taskDialog As New ServerTaskCreateDialog(Me, task)
            taskDialog.TaskNameTextBox.Text = task.Name
            taskDialog.Text = "編輯行程"
            taskDialog.Button1.Text = "完成"
            Select Case task.Mode
                Case ServerTask.TaskMode.Trigger
                    taskDialog.TaskTypeComboBox.SelectedIndex = 1
                    taskDialog.Label2.Enabled = False
                    taskDialog.Label3.Enabled = False
                    taskDialog.TaskPeriodUnitCombo.Enabled = False
                    taskDialog.TaskPeriodUpDown.Enabled = False
                    taskDialog.Label4.Enabled = True
                    taskDialog.EventComboBox.Enabled = True
                    taskDialog.Label5.Enabled = True
                    taskDialog.EventComboBox.SelectedIndex = task.TriggerEvent - 1
                Case ServerTask.TaskMode.Repeating
                    taskDialog.TaskTypeComboBox.SelectedIndex = 0
                    taskDialog.Label2.Enabled = True
                    taskDialog.Label3.Enabled = True
                    taskDialog.TaskPeriodUnitCombo.Enabled = True
                    taskDialog.TaskPeriodUpDown.Enabled = True
                    taskDialog.Label4.Enabled = False
                    taskDialog.EventComboBox.Enabled = False
                    taskDialog.Label5.Enabled = False
                    taskDialog.TaskPeriodUnitCombo.SelectedIndex = task.RepeatingPeriodUnit - 1
                    taskDialog.TaskPeriodUpDown.Value = task.RepeatingPeriod
            End Select
            taskDialog.RunComboBox.SelectedIndex = task.Command.Action - 1
            taskDialog.RunCommandArgBox.Text = task.Command.Data
            taskDialog.ShowDialog()
        End If
    End Sub

    Private Sub RemoveTaskButton_Click(sender As Object, e As EventArgs) Handles RemoveTaskButton.Click
        If TaskListBox.SelectedIndex >= 0 Then
            If ThreadTaskDictionary.ContainsKey(TaskList(TaskListBox.SelectedIndex)) Then
                Dim timer = ThreadTaskDictionary(TaskList(TaskListBox.SelectedIndex))
                timer.Stop()
                ThreadTaskDictionary.Remove(TaskList(TaskListBox.SelectedIndex))
                timer.Dispose()
            End If
            RemoveTaskAt(TaskListBox.SelectedIndex)
        End If
    End Sub

    Private Sub ServerConsole_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

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