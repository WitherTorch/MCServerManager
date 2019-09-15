Imports System.ComponentModel
Imports System.Threading
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager.MinecraftLogParser.MinecraftConsoleMessage

Public Class ServerConsole
    Dim TaskList As New List(Of ServerTask)
    Dim ThreadTaskDictionary As New Dictionary(Of ServerTask, System.Windows.Forms.Timer)
    Dim ConnectionPlayerList As New List(Of String)
    Dim InputList As New List(Of String)()
    Dim CurrentListLocation As Integer = -1
    Dim backgroundProcess As Process
    Dim PlayerListGetState As Integer = 0
    Dim PlayerListGetCount As Integer
    Dim temp_PlayerList As New List(Of String)
    Dim hasHost As Boolean = False
    Dim alternateInputStreamWriter As IO.StreamWriter
    Dim cmd As CMDForm
    Dim outputs As String = ""
    Dim isInBungee As Boolean
    Public ReadOnly Property Server As Server
    Dim startInfo As ProcessStartInfo
    Dim usesType As Server.EServerVersionType
    Dim previousMsg As (String, Date)
    Dim isMessageUpdate As Boolean = False
    Public Event ServerRestarted(ByRef newProcess As Process)
    Public Event ServerStopLoadingStateChanged(PauseLoad As Boolean)
    Friend Sub ReloadUsesType(type As Server.EServerVersionType)
        usesType = type
    End Sub
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
        isInBungee = False
    End Sub

    Public Sub New(ByRef Server As Server, ServerDisplayName As String, items As ListViewItem(), ByRef process As Process, ByRef TaskList As List(Of ServerTask), ByRef TaskDictionary As Dictionary(Of ServerTask, System.Windows.Forms.Timer), PauseLoad As Boolean)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = Server
        isInBungee = True
        Text = "BungeeCord 子伺服器控制台 - " & ServerDisplayName.Substring(0, ServerDisplayName.Length - 5)
        If Server.ServerVersionType = Server.EServerVersionType.Spigot_Git Then
            usesType = Server.EServerVersionType.Spigot
        End If
        DataListView.Items.AddRange(items)
        StopLoadingCheckBox.Checked = PauseLoad
        backgroundProcess = process
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
                                                 _Server.IsRunning = False
                                                 backgroundProcess = Nothing
                                                 If IsDisposed = False Then
                                                     If CloseCheckBox.Checked Then
                                                         Close()
                                                     End If
                                                 End If
                                                 Console.WriteLine("Process Exited")
                                                 _Server.ProcessID = 0
                                             End Sub
        Me.TaskList = TaskList
        ThreadTaskDictionary = TaskDictionary
        If process.StartInfo.StandardOutputEncoding Is System.Text.Encoding.UTF8 Then
            alternateInputStreamWriter = New IO.StreamWriter(process.StandardInput.BaseStream, New System.Text.UTF8Encoding(False)) With {.AutoFlush = True}
        Else
            alternateInputStreamWriter = process.StandardInput
        End If
    End Sub

    Private Sub StopLoadingCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles StopLoadingCheckBox.CheckedChanged
        If isInBungee = False Then
            Select Case StopLoadingCheckBox.Checked = False
                Case True
                    backgroundProcess.BeginErrorReadLine()
                    backgroundProcess.BeginOutputReadLine()
                Case False
                    backgroundProcess.CancelErrorRead()
                    backgroundProcess.CancelOutputRead()
            End Select
        End If
        RaiseEvent ServerStopLoadingStateChanged(StopLoadingCheckBox.Checked)
    End Sub
    Friend Sub InputToConsole(command As String)
        backgroundProcess.StandardInput.WriteLine(command)
    End Sub
    Private Sub ServerConsole_Load(sender As Object, e As EventArgs) Handles Me.Load
        If isInBungee = False Then
            ConnectUPnP()
        End If
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
            Case Server.EServerVersionType.Thermos
                DataListView.Columns.Remove(ColumnHeader2)
            Case Server.EServerVersionType.Contigo
                DataListView.Columns.Remove(ColumnHeader2)
        End Select
        For Each plugin In Server.ServerPlugins
            If plugin.Name = "Minecraft_Server_Manager_Host" Then
                hasHost = True
                Exit For
            End If
        Next

        If isInBungee Then
            TaskTimer.Enabled = True
            TaskTimer.Start()
            If backgroundProcess IsNot Nothing AndAlso backgroundProcess.HasExited = False Then
                RestartButton.Enabled = False
                ForceCloseButton.Enabled = True
            Else
                RestartButton.Enabled = True
                ForceCloseButton.Enabled = False
            End If
        Else
            Run()
            TaskList = Server.ServerTasks.ToList
        End If
        For Each task In TaskList
            TaskListBox.SetItemChecked(TaskListBox.Items.Add(task.Name), task.Enabled)
        Next
    End Sub
    Friend Sub InputMessageToListView(message As MinecraftLogParser.MinecraftConsoleMessage, Optional isError As Boolean = False)
        If isError Then
            outputs &= vbNewLine & message.Message
            If String.IsNullOrWhiteSpace(previousMsg.Item1) = False Then
                Try
                    If previousMsg.Item1 = message.Message AndAlso (Now - previousMsg.Item2).TotalSeconds <= 1 Then
                        previousMsg.Item1 = message.Message
                        previousMsg.Item2 = Now
                        Exit Sub
                    End If
                Catch ex As Exception

                End Try
            Else
                previousMsg.Item1 = message.Message
                previousMsg.Item2 = Now
            End If
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
                                 Case Server.EServerVersionType.Thermos
                                 Case Server.EServerVersionType.Contigo
                                 Case Else
                                     item.SubItems.Add("")
                             End Select
                             item.SubItems.Add(message.Message)
                             Dim nowTime = Now
                             item.SubItems.Add(String.Format("{0}:{1}:{2}", nowTime.Hour.ToString.PadLeft(2, "0"), nowTime.Minute.ToString.PadLeft(2, "0"), nowTime.Second.ToString.PadLeft(2, "0")))
                             If NotifyChooseListBox.CheckedIndices.Contains(3) Then _
                                                                                                                    NotifyInfoMessage("伺服器發出錯誤訊息:" & vbNewLine & message.Message, Text)
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
        Else
            Task.Run(Sub()
                         Try
                             Select Case PlayerListGetState
                                 Case 0
                                                                                                                 'Nothing
                                 Case 1 '偵測/list 回傳頭
                                     Dim ListHeaderRegex As New Text.RegularExpressions.Regex("There are [0-9]{1,}\/[0-9]{1,} player[s]? online:") '/list Header
                                     Dim ListHeaderRegex114 As New Text.RegularExpressions.Regex("There are [0-9]{1,} of a max [0-9]{1,} player[s]? online:") '/list Header
                                     If ListHeaderRegex.IsMatch(message.Message) AndAlso ListHeaderRegex.Match(message.Message).Value = message.Message.Trim Then
                                         Dim PlayerCountRegex As New Text.RegularExpressions.Regex("[0-9]{1,}\/[0-9]{1,}")
                                         Dim PlayerID As String = PlayerCountRegex.Match(message.Message).Value.Split(New Char() {","}, 2)(0)
                                         PlayerListGetState = 2
                                         temp_PlayerList = New List(Of String)
                                         If PlayerListGetCount <= 0 Then
                                             PlayerListGetCount = PlayerID
                                             PlayerListGetState = 0 'Restore to Default
                                         End If
                                     ElseIf ListHeaderRegex114.IsMatch(message.Message) AndAlso message.Message.Trim.StartsWith(ListHeaderRegex114.Match(message.Message).Value) Then
                                         Dim preSplitString As String = message.Message.Substring(ListHeaderRegex114.Match(message.Message).Length - 1)
                                         If String.IsNullOrWhiteSpace(preSplitString) = False Then
                                             preSplitString = preSplitString.Trim
                                             BeginInvokeIfRequired(Me, Sub() PlayerListBox.Items.Clear())
                                             For Each id As String In preSplitString.Split(New String() {" ,"}, StringSplitOptions.RemoveEmptyEntries)
                                                 BeginInvokeIfRequired(Me, Sub()
                                                                               PlayerListBox.Items.Add(id)
                                                                               PlayerListGetState = 0 'Restore to Default
                                                                           End Sub)
                                             Next
                                         End If
                                     End If
                                 Case 2 '偵測玩家ID(每行只有一個)
                                     Dim PlayerIDRegex As New Text.RegularExpressions.Regex("[A-Za-z0-9_-]{1,}")
                                     If PlayerIDRegex.IsMatch(message.Message) AndAlso PlayerIDRegex.Match(message.Message).Value = message.Message.Trim Then
                                         If temp_PlayerList Is Nothing Then temp_PlayerList = New List(Of String)
                                         temp_PlayerList.Add(message.Message.Trim)
                                         PlayerListGetCount -= 1
                                         If PlayerListGetCount <= 0 Then
                                             If temp_PlayerList IsNot Nothing AndAlso temp_PlayerList.Count > 0 Then
                                                 Dim playerList As New List(Of String)
                                                 playerList.AddRange(temp_PlayerList)
                                                 temp_PlayerList = Nothing
                                                 BeginInvokeIfRequired(Me, Sub()
                                                                               PlayerListBox.Items.Clear()
                                                                               PlayerListBox.Items.AddRange(playerList.ToArray)
                                                                           End Sub)
                                             End If
                                             PlayerListGetState = 0 'Restore to Default
                                         End If
                                     End If
                                 Case 3 '有使用Minecraft_Server_Manager_Host 的伺服器專用
                                     Dim playerIDListRegex As New Text.RegularExpressions.Regex("([A-Za-z0-9_-]*){1}(\|[A-Za-z0-9_-]*)*")
                                     If playerIDListRegex.IsMatch(message.Message) AndAlso playerIDListRegex.Match(message.Message).Value = message.Message.Trim Then
                                         If message.Message = "" Then
                                             PlayerListGetState = 0
                                             BeginInvokeIfRequired(Me, Sub()
                                                                           ConnectionPlayerList.Clear()
                                                                           PlayerListBox.Items.Clear()
                                                                       End Sub)
                                         Else
                                             Dim players As String() = message.Message.Split(New Char() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                                             If IsNothing(players) = False Then
                                                 PlayerListGetState = 0
                                                 BeginInvokeIfRequired(Me, Sub()
                                                                               ConnectionPlayerList.Clear()
                                                                               PlayerListBox.Items.Clear()
                                                                               ConnectionPlayerList.AddRange(players)
                                                                               PlayerListBox.Items.AddRange(players)
                                                                           End Sub)
                                             End If
                                         End If
                                     End If
                             End Select
                         Catch ex As Exception

                         End Try
                         Dim item As New ListViewItem(message.ServerMessageTypeString)
                         Select Case Server.ServerVersionType
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
                                 item.SubItems.Add(message.Thread)
                         End Select
                         item.SubItems.Add(message.Message)
                         item.SubItems.Add(String.Format("{0}:{1}:{2}", message.Time.Hour.ToString.PadLeft(2, "0"), message.Time.Minute.ToString.PadLeft(2, "0"), message.Time.Second.ToString.PadLeft(2, "0")))
                         Select Case message.ServerMessageType
                             Case MCServerMessageType.Warning
                                 item.ForeColor = Color.Orange
                                 If NotifyChooseListBox.CheckedIndices.Contains(2) Then _
                                                                                                                    NotifyInfoMessage("伺服器發出警告訊息:" & vbNewLine & message.Message, Text)
                             Case MCServerMessageType.Error
                                 item.ForeColor = Color.Red
                                 If NotifyChooseListBox.CheckedIndices.Contains(3) Then _
                                                                                                                    NotifyInfoMessage("伺服器發出錯誤訊息:" & vbNewLine & message.Message, Text)
                             Case MCServerMessageType.Debug
                                 item.ForeColor = Color.YellowGreen
                             Case MCServerMessageType.Trace
                                 item.ForeColor = Color.Green
                             Case MCServerMessageType.Notify
                                 item.ForeColor = Color.Blue
                         End Select
                         Select Case message.MessageType
                             Case MCMessageType.PlayerConnected
                                 If Server.ServerVersionType = Server.EServerVersionType.Vanilla Then If ConnectionPlayerList.Contains(message.AddtionalMessage("player")) Then ConnectionPlayerList.Add(message.AddtionalMessage("player"))
                             Case MCMessageType.PlayerLostConnected
                                 If Server.ServerVersionType = Server.EServerVersionType.Vanilla Then If ConnectionPlayerList.Contains(message.AddtionalMessage("player")) Then ConnectionPlayerList.Remove(message.AddtionalMessage("player"))
                             Case MCMessageType.PlayerLogin
                                 If NotifyChooseListBox.CheckedIndices.Contains(0) Then _
                                                                                                                    NotifyInfoMessage(message.AddtionalMessage("player") & " 進入伺服器", Text)
                                 BeginInvokeIfRequired(Me, Sub()
                                                               If (Server.ServerVersionType = Server.EServerVersionType.Vanilla AndAlso ConnectionPlayerList.Contains(message.AddtionalMessage("player"))) OrElse Not Server.ServerVersionType = Server.EServerVersionType.Vanilla Then
                                                                   If PlayerListBox.Items.Contains(message.AddtionalMessage("player")) = False Then PlayerListBox.Items.Add(message.AddtionalMessage("player"))
                                                               End If
                                                           End Sub)
                             Case MCMessageType.PlayerLogout
                                 If NotifyChooseListBox.CheckedIndices.Contains(1) Then _
                                                                                                                    NotifyInfoMessage(message.AddtionalMessage("player") & " 離開伺服器", Text)
                                 BeginInvokeIfRequired(Me, Sub()
                                                               If (Server.ServerVersionType = Server.EServerVersionType.Vanilla AndAlso ConnectionPlayerList.Contains(message.AddtionalMessage("player"))) OrElse Not Server.ServerVersionType = Server.EServerVersionType.Vanilla Then
                                                                   If PlayerListBox.Items.Contains(message.AddtionalMessage("player")) Then PlayerListBox.Items.Remove(message.AddtionalMessage("player"))
                                                               End If
                                                           End Sub)
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
                     End Sub)
        End If
    End Sub
    Private Sub ServerConsole_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        BeginInvokeIfRequired(Me, Sub()
                                      Select Case ConsoleMode
                                          Case True
                                              ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式" & vbNewLine & "按Ctrl + K 以開啟/關閉快捷功能表")
                                          Case False
                                              ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式" & vbNewLine & "按Ctrl + K 以開啟/關閉快捷功能表")
                                      End Select
                                  End Sub)
        If ServerConsoleMessages IsNot Nothing Then
            For i As Integer = 0 To ServerConsoleMessages.Count - 1
                If i < NotifyChooseListBox.Items.Count - 1 Then
                    NotifyChooseListBox.SetItemChecked(i, ServerConsoleMessages(i))
                End If
            Next
        End If
    End Sub

    Friend Overloads Sub Run()
        If Server.ServerVersionType = Server.EServerVersionType.Error Then
            Using opn As New OpenFileDialog
                opn.InitialDirectory = Server.ServerPath
                opn.Title = "開啟程式"
                opn.Filter = "Java JAR 程式 (*.jar)|*.jar"
                If opn.ShowDialog = DialogResult.OK Then
                    Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & opn.FileName & """", Server.ServerPath)
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
                                Run(GetJavaPath(), "-Djline.terminal=jline.UnsupportedTerminal -Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "minecraft_server." & Server.Server2ndVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                            Else
                                Run(GetJavaPath(), "-Djline.terminal=jline.UnsupportedTerminal -Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "minecraft_server." & Server.ServerVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                            End If
                        Case Server.EServerVersionType.Forge
                            ' 1.1~1.2 > Server
                            ' 1.3 ~ > Universal
                            ' 1.13+ -> Nothing
                            ' 1.8.9 and 1.7.10 -> Server Version Twice
                            If New Version(Server.ServerVersion) >= New Version(1, 3) Then
                                If New Version(Server.ServerVersion) >= New Version(1, 13) Then
                                    Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                Else
                                    If Server.ServerVersion = "1.8.9" OrElse Server.ServerVersion = "1.7.10" Then
                                        Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-" & Server.ServerVersion & "-universal" & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                    Else
                                        Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-universal" & ".jar") & """" & " nogui", Server.ServerPath, True, True)
                                    End If
                                End If
                            Else
                                Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "forge-" & Server.ServerVersion & "-" & Server.Server2ndVersion & "-server" & ".jar") & """" & " nogui", Server.ServerPath)
                            End If
                        Case Server.EServerVersionType.Spigot
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Spigot_Git
                            Select Case usesType
                                Case Server.EServerVersionType.Spigot
                                    Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                                Case Server.EServerVersionType.CraftBukkit
                                    Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "craftbukkit-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                                Case Else
                                    Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spigot-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                            End Select
                        Case Server.EServerVersionType.CraftBukkit
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "craftbukkit-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.SpongeVanilla
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "spongeVanilla-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Paper
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "paper-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Akarin
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "akarin-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Cauldron
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "server.jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Thermos
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "Thermos-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Contigo
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "Contigo-" & Server.ServerVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.Kettle
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M " & JavaArguments & " -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M -jar " & """" & IO.Path.Combine(Server.ServerPath, "kettle-git-HEAD-" & Server.Server2ndVersion & "-universal.jar") & """", Server.ServerPath)
                    End Select
                Case Server.EServerType.Bedrock
                    Select Case Server.ServerVersionType
                        Case Server.EServerVersionType.Nukkit
                            '-Djline.terminal=jline.UnsupportedTerminal <- JLine Support
                            Run(GetJavaPath(), "-Djline.terminal=jline.UnsupportedTerminal -Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M " & JavaArguments & " -jar " & """" & IO.Path.Combine(Server.ServerPath, "nukkit-" & Server.Server2ndVersion & ".jar") & """", Server.ServerPath)
                        Case Server.EServerVersionType.VanillaBedrock
                            Run("""" & IO.Path.Combine(Server.ServerPath, "bedrock_server.exe") & """", "", Server.ServerPath, True, False)
                        Case Server.EServerVersionType.PocketMine
                            Run(PHPPath, """" & IO.Path.Combine(Server.ServerPath, "PocketMine-MP.phar") & """", Server.ServerPath, True, True)
                    End Select
                Case Server.EServerType.Custom
                    Select Case Server.ServerVersionType
                        Case Server.EServerVersionType.Custom
                            Run(GetJavaPath(), "-Xms" & IIf(Server.ServerMemoryMin <= 0, ServerMemoryMin, Server.ServerMemoryMin) & "M -Xmx" & IIf(Server.ServerMemoryMax <= 0, ServerMemoryMax, Server.ServerMemoryMax) & "M " & JavaArguments & " -jar " & """" & Server.CustomServerRunFile & """", Server.ServerPath)
                    End Select
            End Select
        End If
    End Sub
    Private Overloads Sub Run(program As String, serverDir As String)
        Run(program, "", serverDir, True, False)
    End Sub
    Private Overloads Sub Run(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8 As Boolean = True)
        TaskTimer.Enabled = True
        TaskTimer.Start()
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
            If isInBungee = False Then
                AddHandler backgroundProcess.ErrorDataReceived, Sub(sender, e)
                                                                    Try
                                                                        If IsNothing(backgroundProcess) = False And backgroundProcess.HasExited = False Then
                                                                            If IsNothing(e.Data) = False Then
                                                                                outputs &= vbNewLine & e.Data
                                                                                If String.IsNullOrWhiteSpace(previousMsg.Item1) = False Then
                                                                                    Try
                                                                                        If previousMsg.Item1 = e.Data AndAlso (Now - previousMsg.Item2).TotalSeconds <= 1 Then
                                                                                            previousMsg.Item1 = e.Data
                                                                                            previousMsg.Item2 = Now
                                                                                            Exit Sub
                                                                                        End If
                                                                                    Catch ex As Exception

                                                                                    End Try
                                                                                Else
                                                                                    previousMsg.Item1 = e.Data
                                                                                    previousMsg.Item2 = Now
                                                                                End If
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
                                                                                                     Case Server.EServerVersionType.Thermos
                                                                                                     Case Server.EServerVersionType.Contigo
                                                                                                     Case Else
                                                                                                         item.SubItems.Add("")
                                                                                                 End Select
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
                                                                                 If String.IsNullOrWhiteSpace(previousMsg.Item1) = False Then
                                                                                     Try
                                                                                         If previousMsg.Item1 = e.Data AndAlso (Now - previousMsg.Item2).TotalSeconds <= 1 Then
                                                                                             previousMsg.Item1 = e.Data
                                                                                             previousMsg.Item2 = Now
                                                                                             Exit Sub
                                                                                         End If
                                                                                     Catch ex As Exception

                                                                                     End Try
                                                                                 Else
                                                                                     previousMsg.Item1 = e.Data
                                                                                     previousMsg.Item2 = Now
                                                                                 End If
                                                                                 Task.Run(Sub()
                                                                                              Try
                                                                                                  Dim msg = MinecraftLogParser.ToConsoleMessage(e.Data, Now)
                                                                                                  Try
                                                                                                      Select Case PlayerListGetState
                                                                                                          Case 0
                                                                                                                 'Nothing
                                                                                                          Case 1 '偵測/list 回傳頭
                                                                                                              Dim ListHeaderRegex As New Text.RegularExpressions.Regex("There are [0-9]{1,}\/[0-9]{1,} player[s]? online:") '/list Header
                                                                                                              Dim ListHeaderRegex114 As New Text.RegularExpressions.Regex("There are [0-9]{1,} of a max [0-9]{1,} player[s]? online:") '/list Header
                                                                                                              If ListHeaderRegex.IsMatch(msg.Message) AndAlso ListHeaderRegex.Match(msg.Message).Value = msg.Message.Trim Then
                                                                                                                  Dim PlayerCountRegex As New Text.RegularExpressions.Regex("[0-9]{1,}\/[0-9]{1,}")
                                                                                                                  Dim PlayerID As String = PlayerCountRegex.Match(msg.Message).Value.Split(New Char() {","}, 2)(0)
                                                                                                                  PlayerListGetState = 2
                                                                                                                  temp_PlayerList = New List(Of String)
                                                                                                                  If PlayerListGetCount <= 0 Then
                                                                                                                      PlayerListGetCount = PlayerID
                                                                                                                      PlayerListGetState = 0 'Restore to Default
                                                                                                                  End If
                                                                                                              ElseIf ListHeaderRegex114.IsMatch(msg.Message) AndAlso msg.Message.Trim.StartsWith(ListHeaderRegex114.Match(msg.Message).Value) Then
                                                                                                                  Dim preSplitString As String = msg.Message.Substring(ListHeaderRegex114.Match(msg.Message).Length - 1)
                                                                                                                  If String.IsNullOrWhiteSpace(preSplitString) = False Then
                                                                                                                      preSplitString = preSplitString.Trim
                                                                                                                      BeginInvokeIfRequired(Me, Sub() PlayerListBox.Items.Clear())
                                                                                                                      For Each id As String In preSplitString.Split(New String() {" ,"}, StringSplitOptions.RemoveEmptyEntries)
                                                                                                                          BeginInvokeIfRequired(Me, Sub()
                                                                                                                                                        PlayerListBox.Items.Add(id)
                                                                                                                                                        PlayerListGetState = 0 'Restore to Default
                                                                                                                                                    End Sub)
                                                                                                                      Next
                                                                                                                  End If
                                                                                                              End If
                                                                                                          Case 2 '偵測玩家ID(每行只有一個)
                                                                                                              Dim PlayerIDRegex As New Text.RegularExpressions.Regex("[A-Za-z0-9_-]{1,}")
                                                                                                              If PlayerIDRegex.IsMatch(msg.Message) AndAlso PlayerIDRegex.Match(msg.Message).Value = msg.Message.Trim Then
                                                                                                                  If temp_PlayerList Is Nothing Then temp_PlayerList = New List(Of String)
                                                                                                                  temp_PlayerList.Add(msg.Message.Trim)
                                                                                                                  PlayerListGetCount -= 1
                                                                                                                  If PlayerListGetCount <= 0 Then
                                                                                                                      If temp_PlayerList IsNot Nothing AndAlso temp_PlayerList.Count > 0 Then
                                                                                                                          Dim playerList As New List(Of String)
                                                                                                                          playerList.AddRange(temp_PlayerList)
                                                                                                                          temp_PlayerList = Nothing
                                                                                                                          BeginInvokeIfRequired(Me, Sub()
                                                                                                                                                        PlayerListBox.Items.Clear()
                                                                                                                                                        PlayerListBox.Items.AddRange(playerList.ToArray)
                                                                                                                                                    End Sub)
                                                                                                                      End If
                                                                                                                      PlayerListGetState = 0 'Restore to Default
                                                                                                                  End If
                                                                                                              End If
                                                                                                          Case 3 '有使用Minecraft_Server_Manager_Host 的伺服器專用
                                                                                                              Dim playerIDListRegex As New Text.RegularExpressions.Regex("([A-Za-z0-9_-]*){1}(\|[A-Za-z0-9_-]*)*")
                                                                                                              If playerIDListRegex.IsMatch(msg.Message) AndAlso playerIDListRegex.Match(msg.Message).Value = msg.Message.Trim Then
                                                                                                                  If msg.Message = "" Then
                                                                                                                      PlayerListGetState = 0
                                                                                                                      BeginInvokeIfRequired(Me, Sub()
                                                                                                                                                    ConnectionPlayerList.Clear()
                                                                                                                                                    PlayerListBox.Items.Clear()
                                                                                                                                                End Sub)
                                                                                                                  Else
                                                                                                                      Dim players As String() = msg.Message.Split(New Char() {"|"}, StringSplitOptions.RemoveEmptyEntries)
                                                                                                                      If IsNothing(players) = False Then
                                                                                                                          PlayerListGetState = 0
                                                                                                                          BeginInvokeIfRequired(Me, Sub()
                                                                                                                                                        ConnectionPlayerList.Clear()
                                                                                                                                                        PlayerListBox.Items.Clear()
                                                                                                                                                        ConnectionPlayerList.AddRange(players)
                                                                                                                                                        PlayerListBox.Items.AddRange(players)
                                                                                                                                                    End Sub)
                                                                                                                      End If
                                                                                                                  End If
                                                                                                              End If
                                                                                                      End Select
                                                                                                  Catch ex As Exception

                                                                                                  End Try
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
                                                                                                      Case Server.EServerVersionType.Thermos
                                                                                                      Case Server.EServerVersionType.Contigo
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
                                                                                                      Case MCServerMessageType.Debug
                                                                                                          item.ForeColor = Color.YellowGreen
                                                                                                      Case MCServerMessageType.Trace
                                                                                                          item.ForeColor = Color.Green
                                                                                                      Case MCServerMessageType.Notify
                                                                                                          item.ForeColor = Color.Blue
                                                                                                  End Select
                                                                                                  Select Case msg.MessageType
                                                                                                      Case MCMessageType.PlayerConnected
                                                                                                          If Server.ServerVersionType = Server.EServerVersionType.Vanilla Then If ConnectionPlayerList.Contains(msg.AddtionalMessage("player")) Then ConnectionPlayerList.Add(msg.AddtionalMessage("player"))
                                                                                                      Case MCMessageType.PlayerLostConnected
                                                                                                          If Server.ServerVersionType = Server.EServerVersionType.Vanilla Then If ConnectionPlayerList.Contains(msg.AddtionalMessage("player")) Then ConnectionPlayerList.Remove(msg.AddtionalMessage("player"))
                                                                                                      Case MCMessageType.PlayerLogin
                                                                                                          If NotifyChooseListBox.CheckedIndices.Contains(0) Then _
                                                                                                                    NotifyInfoMessage(msg.AddtionalMessage("player") & " 進入伺服器", Text)
                                                                                                          BeginInvokeIfRequired(Me, Sub()
                                                                                                                                        If (Server.ServerVersionType = Server.EServerVersionType.Vanilla AndAlso ConnectionPlayerList.Contains(msg.AddtionalMessage("player"))) OrElse Not Server.ServerVersionType = Server.EServerVersionType.Vanilla Then
                                                                                                                                            If PlayerListBox.Items.Contains(msg.AddtionalMessage("player")) = False Then PlayerListBox.Items.Add(msg.AddtionalMessage("player"))
                                                                                                                                        End If
                                                                                                                                    End Sub)
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
                                                                                                          BeginInvokeIfRequired(Me, Sub()
                                                                                                                                        If (Server.ServerVersionType = Server.EServerVersionType.Vanilla AndAlso ConnectionPlayerList.Contains(msg.AddtionalMessage("player"))) OrElse Not Server.ServerVersionType = Server.EServerVersionType.Vanilla Then
                                                                                                                                            If PlayerListBox.Items.Contains(msg.AddtionalMessage("player")) Then PlayerListBox.Items.Remove(msg.AddtionalMessage("player"))
                                                                                                                                        End If
                                                                                                                                    End Sub)
                                                                                                          For Each task In TaskList
                                                                                                              If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                                                                                                                       task.TriggerEvent = ServerTask.TaskTriggerEvent.PlayerLogout Then
                                                                                                                  If task.Enabled = True Then
                                                                                                                      RunTask(task, msg.AddtionalMessage)
                                                                                                                  End If
                                                                                                              End If
                                                                                                          Next
                                                                                                      Case MCMessageType.PlayerInputCommand
                                                                                                          For Each task In TaskList
                                                                                                              If task.Mode = ServerTask.TaskMode.Trigger AndAlso
                                                                                                                       task.TriggerEvent = ServerTask.TaskTriggerEvent.PlayerInputCommand Then
                                                                                                                  If task.Enabled = True Then
                                                                                                                      Dim testRegex As New Text.RegularExpressions.Regex(task.CheckRegex)
                                                                                                                      If String.IsNullOrWhiteSpace(task.CheckRegex) OrElse
                                                                                                                  (testRegex.IsMatch(msg.AddtionalMessage("command")) AndAlso testRegex.Match(msg.AddtionalMessage("command")).Value = msg.AddtionalMessage("command")) Then
                                                                                                                          RunTask(task, msg.AddtionalMessage)
                                                                                                                      End If
                                                                                                                  End If
                                                                                                              End If
                                                                                                          Next
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
        End If
        ServerStatusLabel.Text = "伺服器狀態：啟動"
        Server.IsRunning = True
        backgroundProcess.EnableRaisingEvents = True
        RaiseEvent ServerRestarted(backgroundProcess)
        AddHandler backgroundProcess.Exited, Sub(sender, e)
                                                 If isInBungee = False Then
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
                                                 End If
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
        If isInBungee = False Then
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
        End If
        If backgroundProcess IsNot Nothing Then Server.ProcessID = backgroundProcess.Id
    End Sub
    Private Overloads Function PrepareStartInfo(program As String, args As String, serverDir As String, Optional nogui As Boolean = True, Optional UTF8Encoding As Boolean = False) As ProcessStartInfo
        If IsNothing(startInfo) Then
            Dim processInfo As ProcessStartInfo
            If args = "" Then
                processInfo = New ProcessStartInfo(program)
            Else
                If UTF8Encoding AndAlso Server.ServerVersionType <> Server.EServerVersionType.PocketMine Then args = "-Dfile.encoding=UTF-8 " & args
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
                                    If Server.ServerVersionType = Server.EServerVersionType.CraftBukkit OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Spigot OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Spigot_Git OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Paper OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Akarin OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Cauldron OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Thermos OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Contigo OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Kettle Then
                                        If CommandTextBox.Text.Substring(1) = "minecraft:list" Then PlayerListGetState = 1
                                        If CommandTextBox.Text.Substring(1) = "callfunction getPlayerList" Then PlayerListGetState = 3
                                    Else
                                        If CommandTextBox.Text.Substring(1) = "list" Then PlayerListGetState = 1
                                    End If
                                    'backgroundProcess.StandardInput.WriteLine(CommandTextBox.Text.Substring(1))
                                    alternateInputStreamWriter.WriteLine(CommandTextBox.Text.Substring(1))
                                    If InputList.Count <= 0 OrElse InputList.Last <> CommandTextBox.Text.Substring(1) Then
                                        InputList.Add(CommandTextBox.Text.Substring(1))
                                    End If
                                Else
                                    'backgroundProcess.StandardInput.WriteLine("say " & CommandTextBox.Text)
                                    alternateInputStreamWriter.WriteLine("say " & CommandTextBox.Text)
                                    If InputList.Count <= 0 OrElse InputList.Last <> "say " & CommandTextBox.Text Then
                                        InputList.Add("say " & CommandTextBox.Text)
                                    End If
                                End If
                                CommandTextBox.Clear()
                            Case False
                                If Server.ServerVersionType = Server.EServerVersionType.CraftBukkit OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Spigot OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Spigot_Git OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Paper OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Akarin OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Cauldron OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Thermos OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Contigo OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Kettle Then
                                    If CommandTextBox.Text = "minecraft:list" Then PlayerListGetState = 1
                                    If CommandTextBox.Text = "callfunction getPlayerList" Then PlayerListGetState = 3
                                Else
                                    If CommandTextBox.Text = "list" Then PlayerListGetState = 1
                                End If
                                'backgroundProcess.StandardInput.WriteLine(CommandTextBox.Text)
                                alternateInputStreamWriter.WriteLine(CommandTextBox.Text)
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
                    e.SuppressKeyPress = True
                    ConsoleMode = Not ConsoleMode
                    Select Case ConsoleMode
                        Case True
                            ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：Minecraft 聊天欄" & vbNewLine & "按Ctrl + S 以切換模式" & vbNewLine & "按Ctrl + K 以開啟/關閉快捷功能表")
                        Case False
                            ToolTip1.SetToolTip(CommandTextBox, "目前輸入模式：CMD 主控台" & vbNewLine & "按Ctrl + S 以切換模式" & vbNewLine & "按Ctrl + K 以開啟/關閉快捷功能表")
                    End Select
                End If
            Case Keys.K
                If e.Control = True And e.Alt = False And e.Shift = False Then
                    e.Handled = True
                    e.SuppressKeyPress = True
                    Static menu As TableLayoutPanel
                    If menu Is Nothing OrElse menu.IsDisposed Then
                        menu = New TableLayoutPanel
                        For Each task In TaskList
                            If task.Mode = ServerTask.TaskMode.QuickLaunch AndAlso task.Enabled = True Then
                                BeginInvokeIfRequired(Me, Sub()
                                                              Dim button As New Button() With {.Height = 30, .Text = task.Name, .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right}
                                                              AddHandler button.Click, Sub()
                                                                                           RunTask(task, New Dictionary(Of String, String))
                                                                                       End Sub
                                                              menu.Controls.Add(button, 0, menu.RowCount)
                                                          End Sub)
                            End If
                        Next
                        menu.Height = DataListView.Height
                        menu.Width = Math.Min(DataListView.Width / 2, 100)
                        menu.Location = New Point(Me.Location.X - menu.Width - 5, DataListView.Location.Y)
                        menu.Dock = DockStyle.Right
                        DataTabPage.Controls.Add(menu)
                        menu.AutoScroll = True
                        menu.Show()
                        menu.SendToBack()
                    Else
                        If menu.Visible Then
                            DataTabPage.Controls.Remove(menu)
                            menu.Dispose()
                            menu = Nothing
                        Else
                            menu.Show()
                        End If
                    End If
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
        If IsNothing(backgroundProcess) OrElse backgroundProcess.HasExited Then
            Dim spItem1 As New ListViewItem("通知")
            spItem1.ForeColor = Color.Blue
            Select Case Server.ServerVersionType
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
                Case Server.EServerVersionType.Spigot_Git
                Case Server.EServerVersionType.Cauldron
                Case Server.EServerVersionType.Thermos
                Case Server.EServerVersionType.Contigo
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
        BeginInvokeIfRequired(Me, New Action(Sub()
                                                 If backgroundProcess IsNot Nothing AndAlso backgroundProcess.HasExited = False Then
                                                     Try
                                                         MemoryLabel.Text = "占用記憶體：" & FitMemoryUnit(Process.GetProcessById(backgroundProcess.Id).WorkingSet64)
                                                         IDLabel.Text = "處理序ID：" & backgroundProcess.Id
                                                         If ServerStatusLabel.Text <> "伺服器狀態：啟動" Then ServerStatusLabel.Text = "伺服器狀態：啟動"
                                                         Dim maxPlayerCount As Integer = 0
                                                         If Server.ServerOptions.ContainsKey("max-players") AndAlso IsNumeric(Server.ServerOptions("max-players")) Then
                                                             maxPlayerCount = Server.ServerOptions("max-players")
                                                         End If
                                                         Dim playerListTitle As String = String.Format("玩家 ({0}/{1})", PlayerListBox.Items.Count, maxPlayerCount)
                                                         If PlayerGroupBox.Text <> playerListTitle Then PlayerGroupBox.Text = playerListTitle
                                                     Catch ex As Exception
                                                     End Try
                                                 Else
                                                     Try
                                                         MemoryLabel.Text = "占用記憶體：(無)"
                                                         IDLabel.Text = "處理序ID：(無)"
                                                         If ServerStatusLabel.Text <> "伺服器狀態：關閉" Then ServerStatusLabel.Text = "伺服器狀態：關閉"
                                                         Dim maxPlayerCount As Integer = 0
                                                             If Server.ServerOptions.ContainsKey("max-players") AndAlso IsNumeric(Server.ServerOptions("max-players")) Then
                                                             maxPlayerCount = Server.ServerOptions("max-players")
                                                         End If
                                                         Dim playerListTitle As String = String.Format("玩家 ({0}/{1})", PlayerListBox.Items.Count, maxPlayerCount)
                                                         If PlayerGroupBox.Text <> playerListTitle Then PlayerGroupBox.Text = playerListTitle
                                                     Catch ex As Exception
                                                     End Try
                                                 End If
                                                 If False Then
                                                     BeginInvokeIfRequired(Me, Sub()
                                                                                   Try
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
                                                                                       If seekToBottom Then DataListView.EnsureVisible(DataListView.Items.Count - 1)
                                                                                   Catch ex As Exception
                                                                                   End Try
                                                                               End Sub)
                                                     isMessageUpdate = False
                                                 End If
                                             End Sub))
    End Sub

    Private Sub ServerConsole_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If isInBungee = False Then
            Dim notifySettings As New List(Of Boolean)
            For i As Integer = 0 To NotifyChooseListBox.Items.Count - 1
                notifySettings.Add(NotifyChooseListBox.GetItemChecked(i))
            Next
            ServerConsoleMessages = notifySettings.ToArray
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
            .IsBackground = False
                                                                     }
            thread.Start()
        End If
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
        Dim TaskRandomGenerator As New Random
        Static TaskRandomGenNumber As Integer = -1
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
                            Dim _thread As New Thread(Sub()
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
                                                                          ElseIf line.StartsWith("#backup ") AndAlso String.IsNullOrWhiteSpace(line.Substring(8)) = False Then
                                                                              Try
                                                                                  BackupServer(line.Substring(8))
                                                                              Catch ex As Exception

                                                                              End Try
                                                                          End If
                                                                      Else
                                                                          Try
                                                                              line.Replace("<#RANDOM>", IIf(TaskRandomGenNumber > -1, TaskRandomGenerator.Next(TaskRandomGenNumber), 0))
                                                                          Catch ex As Exception

                                                                          End Try
                                                                          alternateInputStreamWriter.WriteLine(line)
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
                                                                              BackupServer(command.Substring(8))
                                                                          Catch ex As Exception

                                                                          End Try
                                                                      End If
                                                                  Else
                                                                      Try
                                                                          command.Replace("<#RANDOM>", IIf(TaskRandomGenNumber > -1, TaskRandomGenerator.Next(TaskRandomGenNumber), 0))
                                                                      Catch ex As Exception

                                                                      End Try
                                                                      alternateInputStreamWriter.WriteLine(command)
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
                                                      BackupServer(task.Command.Data)
                                                  End If
                                              Catch ex As Exception

                                              End Try
                                          End Sub)
                _thread.IsBackground = True
                _thread.Start()
        End Select
    End Sub
    Shared Function GetBaseIntervalValue(type As ServerTask.TaskPeriodUnit) As Integer
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
            Case Else 'Default is tick
                Return 50
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
                    taskDialog.TextBox1.Text = task.CheckRegex
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
                    taskDialog.TaskPeriodUnitCombo.SelectedIndex = task.RepeatingPeriodUnit
                    taskDialog.TaskPeriodUpDown.Value = task.RepeatingPeriod
                Case ServerTask.TaskMode.QuickLaunch
                    taskDialog.TaskTypeComboBox.SelectedIndex = 2
                    taskDialog.Label2.Enabled = False
                    taskDialog.Label3.Enabled = False
                    taskDialog.TaskPeriodUnitCombo.Enabled = False
                    taskDialog.TaskPeriodUpDown.Enabled = False
                    taskDialog.Label4.Enabled = False
                    taskDialog.EventComboBox.Enabled = False
                    taskDialog.Label5.Enabled = False
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
            封禁ToolStripMenuItem.Enabled = True
            踢出ToolStripMenuItem.Enabled = True
            解除OPToolStripMenuItem.Enabled = True
            設定OPToolStripMenuItem.Enabled = True
            更新列表用listToolStripMenuItem.Enabled = True
        Else
            封禁ToolStripMenuItem.Enabled = False
            踢出ToolStripMenuItem.Enabled = False
            解除OPToolStripMenuItem.Enabled = False
            設定OPToolStripMenuItem.Enabled = False
            更新列表用listToolStripMenuItem.Enabled = True
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

    Private Sub 更新列表用listToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 更新列表用listToolStripMenuItem.Click
        If backgroundProcess IsNot Nothing Then
            If backgroundProcess.HasExited = False Then
                Try
                    If Server.ServerVersionType = Server.EServerVersionType.CraftBukkit OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Spigot OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Spigot_Git OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Paper OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Akarin OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Cauldron OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Thermos OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Contigo OrElse
                            Server.ServerVersionType = Server.EServerVersionType.Kettle Then
                        If hasHost Then
                            backgroundProcess.StandardInput.WriteLine("callfunction getPlayerList")
                            PlayerListGetState = 3
                        Else
                            backgroundProcess.StandardInput.WriteLine("minecraft:list")
                            PlayerListGetState = 1
                        End If
                    Else
                        backgroundProcess.StandardInput.WriteLine("list")
                        PlayerListGetState = 1
                    End If
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using fileDialog As New OpenFileDialog
            fileDialog.Title = "匯入排程..."
            fileDialog.Filter = "匯出的排程 (*.json)|*.json"
            If fileDialog.ShowDialog = DialogResult.OK Then
                Try
                    Dim jsonArray As JArray = JsonConvert.DeserializeObject(Of JArray)(My.Computer.FileSystem.ReadAllText(fileDialog.FileName))
                    For Each jsonObject As JObject In jsonArray
                        Dim task As New ServerTask(CInt(jsonObject.GetValue("mode")), jsonObject.GetValue("name"))
                        task.Enabled = jsonObject.GetValue("enabled")
                        Select Case task.Mode
                            Case ServerTask.TaskMode.Repeating
                                task.RepeatingPeriod = jsonObject.GetValue("period")
                                task.RepeatingPeriodUnit = CInt(jsonObject.GetValue("periodUnit"))
                            Case ServerTask.TaskMode.Trigger
                                task.TriggerEvent = CInt(jsonObject.GetValue("event"))
                                task.CheckRegex = IIf(jsonObject.ContainsKey("regex"), jsonObject.GetValue("regex"), "")
                        End Select
                        Dim command As JObject = jsonObject.GetValue("command")
                        Dim taskCommand As New ServerTask.TaskCommand()
                        taskCommand.Action = CInt(command.GetValue("action"))
                        taskCommand.Data = command.GetValue("data")
                        task.Command = taskCommand
                        TaskList.Add(task)
                    Next
                Catch ex As Exception

                End Try
            End If
        End Using
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Not (TaskListBox.SelectedIndices Is Nothing Or TaskListBox.SelectedIndices.Count = 0) Then
            Using fileDialog As New SaveFileDialog
                fileDialog.Title = "匯出排程..."
                fileDialog.Filter = "匯出的排程 (*.json)|*.json"
                If fileDialog.ShowDialog = DialogResult.OK Then
                    Dim jsonArray As New JArray
                    Try
                        Dim taskList As New List(Of ServerTask)
                        For Each index In TaskListBox.SelectedIndices
                            taskList.Add(Me.TaskList(index))
                        Next
                        For Each task As ServerTask In taskList
                            Dim jsonObject As New JObject
                            jsonObject.Add("mode", task.Mode)
                            jsonObject.Add("name", task.Name)
                            jsonObject.Add("enabled", task.Enabled)
                            Select Case task.Mode
                                Case ServerTask.TaskMode.Repeating
                                    jsonObject.Add("period", task.RepeatingPeriod)
                                    jsonObject.Add("periodUnit", task.RepeatingPeriodUnit)
                                Case ServerTask.TaskMode.Trigger
                                    jsonObject.Add("event", task.TriggerEvent)
                                    Select Case task.TriggerEvent
                                        Case ServerTask.TaskTriggerEvent.PlayerInputCommand
                                            jsonObject.Add("regex", task.CheckRegex)
                                    End Select
                            End Select
                            Dim command As New JObject
                            Dim taskCommand As ServerTask.TaskCommand = task.Command
                            command.Add("action", taskCommand.Action)
                            command.Add("data", taskCommand.Data)
                            jsonObject.Add("command", command)
                            jsonArray.Add(jsonObject)
                        Next
                        My.Computer.FileSystem.WriteAllText(fileDialog.FileName, JsonConvert.SerializeObject(jsonArray), False)
                    Catch ex As Exception

                    End Try
                End If
            End Using
        End If
    End Sub
    Sub BackupServer(path As String)
        Dim msg As New MinecraftLogParser.MinecraftConsoleMessage
        msg.ServerMessageType = MCServerMessageType.Notify
        msg.Message = "伺服器備份中..."
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
            BeginInvokeIfRequired(Me, Sub() DataListView.Items.Add(item))
        Catch ex As Exception
        End Try
        Try
            My.Computer.FileSystem.CopyDirectory(Server.ServerPath, path)
        Catch ex As Exception

        End Try
        Dim msg2 As New MinecraftLogParser.MinecraftConsoleMessage
        msg2.ServerMessageType = MCServerMessageType.Notify
        msg2.Message = "伺服器備份完成..."
        msg2.Time = Now
        msg2.Thread = Application.ProductName
        msg2.MessageType = MCMessageType.None
        Dim item2 As New ListViewItem(msg.ServerMessageTypeString)
        Select Case Server.ServerVersionType
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
            BeginInvokeIfRequired(Me, Sub() DataListView.Items.Add(item2))
        Catch ex As Exception
        End Try
    End Sub
End Class