Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager
Imports YamlDotNet.Serialization

Public Class ServerSetter
    Dim TaskList As New List(Of ServerTask)
    Dim ThreadTaskDictionary As New Dictionary(Of ServerTask, Timer)
    Dim server As Server
    Friend serverOptions As IServerOptions
    Sub New(ByRef server As Server)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
    End Sub
    Sub SetUpdateInfo()
        BeginInvokeIfRequired(Me, Sub()
                                      Dim version As String = VersionLabel.Text
                                      version = version.Replace("<ServerVersionType>", GetSimpleVersionName(server.ServerVersionType, server.ServerVersion))
                                      If server.CanUpdate Then
                                          version = version.Replace("<ServerVersionStatus>", "可更新")
                                      Else
                                          version = version.Replace("<ServerVersionStatus>", "無更新")
                                      End If
                                      VersionLabel.Text = version
                                  End Sub)
    End Sub
    Private Sub ServerSetter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreatedForm.Add(Me)
        ShowInTaskbar = MiniState = 0
        MapPanel.Controls.Add(New MapView(server) With {.Dock = DockStyle.Fill})
        SetUpdateInfo()
        If server.ServerVersionType = Server.EServerVersionType.Forge OrElse
                server.ServerVersionType = Server.EServerVersionType.SpongeVanilla OrElse
                server.ServerVersionType = Server.EServerVersionType.Nukkit OrElse
                server.ServerVersionType = Server.EServerVersionType.VanillaBedrock OrElse
                server.ServerVersionType = Server.EServerVersionType.Spigot_Git OrElse
                server.ServerVersionType = Server.EServerVersionType.Akarin Then
            UpdateButton.Enabled = server.CanUpdate
        Else
            Button2.Visible = False
            UpdateButton.Visible = False
            Button2.Enabled = False
            UpdateButton.Enabled = False
        End If
        Task.Run(New Action(Sub()
                                Select Case server.ServerType
                                    Case Server.EServerType.Java
                                        serverOptions = New JavaServerOptions
                                        serverOptions.InputOption(server.ServerOptions)
                                        BeginInvoke(Sub()
                                                        AdvancedPropertyGrid.SelectedObject = serverOptions
                                                        ServerMemoryMaxBox.Value = server.ServerMemoryMax
                                                        ServerMemoryMinBox.Value = server.ServerMemoryMin
                                                    End Sub)
                                    Case Server.EServerType.Bedrock
                                        Select Case server.ServerVersionType
                                            Case Server.EServerVersionType.Nukkit
                                                serverOptions = New NukkitServerOptions
                                                serverOptions.InputOption(server.ServerOptions)
                                                BeginInvoke(Sub()
                                                                AdvancedPropertyGrid.SelectedObject = serverOptions
                                                                ServerMemoryMaxBox.Value = server.ServerMemoryMax
                                                                ServerMemoryMinBox.Value = server.ServerMemoryMin
                                                            End Sub)
                                            Case Server.EServerVersionType.VanillaBedrock
                                                serverOptions = New VanillaBedrockServerOptions
                                                serverOptions.InputOption(server.ServerOptions)
                                                BeginInvoke(Sub()
                                                                AdvancedPropertyGrid.SelectedObject = serverOptions
                                                                GroupBox1.Enabled = False
                                                            End Sub)
                                            Case Server.EServerVersionType.PocketMine
                                                serverOptions = New PocketMineServerOptions
                                                serverOptions.InputOption(server.ServerOptions)
                                                BeginInvoke(Sub()
                                                                AdvancedPropertyGrid.SelectedObject = serverOptions
                                                                GroupBox1.Enabled = False
                                                            End Sub)
                                        End Select
                                End Select
                            End Sub))
        If server.ServerVersionType = Server.EServerVersionType.CraftBukkit OrElse
            server.ServerVersionType = Server.EServerVersionType.Spigot OrElse
            server.ServerVersionType = Server.EServerVersionType.Paper OrElse
            server.ServerVersionType = Server.EServerVersionType.Akarin Then
            PluginManageButton.Enabled = True
            PluginManageButton.Text = "管理插件"
        ElseIf server.ServerVersionType = Server.EServerVersionType.Forge Then
            PluginManageButton.Enabled = True
            PluginManageButton.Text = "管理模組"
        ElseIf server.ServerVersionType = Server.EServerVersionType.Nukkit Then
            PluginManageButton.Enabled = True
            PluginManageButton.Text = "管理插件"
        ElseIf server.ServerVersionType = Server.EServerVersionType.PocketMine Then
            PluginManageButton.Enabled = True
            PluginManageButton.Text = "管理插件"
        ElseIf server.ServerVersionType = Server.EServerVersionType.Cauldron OrElse
                server.ServerVersionType = Server.EServerVersionType.Thermos OrElse
                server.ServerVersionType = Server.EServerVersionType.Contigo OrElse
                server.ServerVersionType = Server.EServerVersionType.Kettle OrElse
                server.ServerVersionType = Server.EServerVersionType.CatServer Then
            PluginManageButton.Enabled = True
            PluginManageButton.Text = "管理插件/模組"
        Else
            PluginManageButton.Enabled = False
            PluginManageButton.Text = "管理插件/模組"
        End If
        TaskList = server.ServerTasks.ToList
        For Each task In TaskList
            TaskListBox.SetItemChecked(TaskListBox.Items.Add(task.Name), task.Enabled)
        Next
        Select Case server.ServerVersionType
            Case Server.EServerVersionType.CraftBukkit
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim tab As New TabPage("Bukkit 設定")
                tab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(tab)
            Case Server.EServerVersionType.Spigot
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Dim spigotTab As New TabPage("Spigot 設定")
                spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                SettingTabControl.TabPages.Add(spigotTab)
            Case Server.EServerVersionType.Spigot_Git
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Dim spigotTab As New TabPage("Spigot 設定")
                spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                SettingTabControl.TabPages.Add(spigotTab)
            Case Server.EServerVersionType.Paper
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Dim spigotTab As New TabPage("Spigot 設定")
                spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                SettingTabControl.TabPages.Add(spigotTab)
                Dim paperTab As New TabPage("Paper 設定")
                paperTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.PaperOptions})
                SettingTabControl.TabPages.Add(paperTab)
            Case Server.EServerVersionType.Akarin
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Dim spigotTab As New TabPage("Spigot 設定")
                spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                SettingTabControl.TabPages.Add(spigotTab)
                Dim paperTab As New TabPage("Paper 設定")
                paperTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.PaperOptions})
                SettingTabControl.TabPages.Add(paperTab)
                Dim akarinTab As New TabPage("Akarin 設定")
                akarinTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.AkarinOptions})
                SettingTabControl.TabPages.Add(akarinTab)
            Case Server.EServerVersionType.Nukkit
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim nukkitTab As New TabPage("Nukkit 設定")
                nukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.NukkitOptions})
                SettingTabControl.TabPages.Add(nukkitTab)
            Case Server.EServerVersionType.Cauldron
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Select Case server.ServerVersion
                    Case "1.5.2"
                        Dim cauldronTab As New TabPage("MCPC 設定")
                        cauldronTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.CauldronOptions})
                        SettingTabControl.TabPages.Add(cauldronTab)
                    Case "1.6.4"
                        Dim spigotTab As New TabPage("Spigot 設定")
                        spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                        SettingTabControl.TabPages.Add(spigotTab)
                        Dim cauldronTab As New TabPage("MCPC 設定")
                        cauldronTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.CauldronOptions})
                        SettingTabControl.TabPages.Add(cauldronTab)
                    Case "1.7.2"
                        Dim spigotTab As New TabPage("Spigot 設定")
                        spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                        SettingTabControl.TabPages.Add(spigotTab)
                        Dim cauldronTab As New TabPage("Cauldron 設定")
                        cauldronTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.CauldronOptions})
                        SettingTabControl.TabPages.Add(cauldronTab)
                    Case "1.7.10"
                        Dim cauldronTab As New TabPage("Cauldron 設定")
                        cauldronTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.CauldronOptions})
                        SettingTabControl.TabPages.Add(cauldronTab)
                End Select
            Case Server.EServerVersionType.Thermos
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Dim spigotTab As New TabPage("Spigot 設定")
                spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                SettingTabControl.TabPages.Add(spigotTab)
                Dim cauldronTab As New TabPage("Cauldron 設定")
                cauldronTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.CauldronOptions})
                SettingTabControl.TabPages.Add(cauldronTab)
            Case Server.EServerVersionType.Contigo
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Dim spigotTab As New TabPage("Spigot 設定")
                spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                SettingTabControl.TabPages.Add(spigotTab)
                Dim cauldronTab As New TabPage("Cauldron 設定")
                cauldronTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.CauldronOptions})
                SettingTabControl.TabPages.Add(cauldronTab)
            Case Server.EServerVersionType.Kettle
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Dim spigotTab As New TabPage("Spigot 設定")
                spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                SettingTabControl.TabPages.Add(spigotTab)
            Case Server.EServerVersionType.PocketMine
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim pocketMineTab As New TabPage("PocketMine-MP 設定")
                pocketMineTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.PocketMineOptions})
                SettingTabControl.TabPages.Add(pocketMineTab)
            Case Server.EServerVersionType.CatServer
                AdvancedTabPage.Text = "伺服器主設定檔"
                Dim bukkitTab As New TabPage("Bukkit 設定")
                bukkitTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.BukkitOptions})
                SettingTabControl.TabPages.Add(bukkitTab)
                Dim spigotTab As New TabPage("Spigot 設定")
                spigotTab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = server.SpigotOptions})
                SettingTabControl.TabPages.Add(spigotTab)
            Case Else
                AdvancedTabPage.Text = "伺服器設定檔"
        End Select
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        AddHandler server.ServerInfoUpdated, AddressOf Server_ServerInfoUpdated
        AddHandler server.ServerUpdateStart, Sub() AddHandler server.ServerUpdating, AddressOf Server_ServerUpdating
        AddHandler server.ServerUpdateEnd, Sub() RemoveHandler server.ServerUpdating, AddressOf Server_ServerUpdating
        server.UpdateServer()
    End Sub
    Private Sub Server_ServerInfoUpdated()
        BeginInvokeIfRequired(Me, Sub()
                                      Dim version As String = "<ServerVersionType>：<ServerVersionStatus>"
                                      version = version.Replace("<ServerVersionType>", GetSimpleVersionName(server.ServerVersionType, server.ServerVersion))
                                      If server.CanUpdate Then
                                          version = version.Replace("<ServerVersionStatus>", "可更新")
                                      Else
                                          version = version.Replace("<ServerVersionStatus>", "無更新")
                                      End If
                                      UpdateButton.Enabled = server.CanUpdate
                                      VersionLabel.Text = version
                                      RemoveHandler server.ServerInfoUpdated, AddressOf Server_ServerInfoUpdated
                                  End Sub)
    End Sub
    Friend Overloads Sub Server_ServerUpdating(percent As Integer)
        BeginInvokeIfRequired(Me, Sub()
                                      Dim version As String = "<ServerVersionType>：<ServerVersionStatus>"
                                      version = version.Replace("<ServerVersionType>", GetSimpleVersionName(server.ServerVersionType, server.ServerVersion))
                                      version = version.Replace("<ServerVersionStatus>", "更新中 (" & percent & "%)")
                                      VersionLabel.Text = version
                                  End Sub)
    End Sub

    Private Sub ServerSetter_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CreatedForm.Remove(Me)
        server.ServerTasks = TaskList.ToArray
        If IsNothing(serverOptions) = False Then
            server.ServerOptions = serverOptions.OutputOption
        End If
        Try
            server.SaveServer()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Using open As New OpenFileDialog()
            open.Title = "開啟圖片..."
            open.Filter = "JPEG 圖片 (*.jpeg,*.jpe,*.jpg)|*.jpeg,*.jpe,*.jpg|PNG 圖片 (*.png)|*.png|GIF 圖像 (*.gif)|*.gif|Windows 點陣圖 (*.bmp)|*.bmp"
            If open.ShowDialog() = DialogResult.OK Then
                Dim bit As Bitmap = Image.FromFile(open.FileName)
                If bit.Size = New Size(64, 64) Then
                    bit.Save(IO.Path.Combine(server.ServerPath, "server-icon.png"), Imaging.ImageFormat.Png)
                Else
                    If IO.File.Exists(IO.Path.Combine(server.ServerPath, "server-icon.png")) Then
                        IO.File.Delete(IO.Path.Combine(server.ServerPath, "server-icon.png"))
                    End If
                    Dim bit2 As New Bitmap(bit, New Size(64, 64))
                    bit2.Save(IO.Path.Combine(server.ServerPath, "server-icon.png"), Imaging.ImageFormat.Png)
                End If
            End If
            server.ReloadServerIcon()
        End Using
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles PluginManageButton.Click
        Static addonManager As IManagerGUI

        If server.ServerVersionType = Server.EServerVersionType.CraftBukkit OrElse
            server.ServerVersionType = Server.EServerVersionType.Spigot OrElse
            server.ServerVersionType = Server.EServerVersionType.Paper OrElse
            server.ServerVersionType = Server.EServerVersionType.Akarin Then
            addonManager = New BukkitPluginManager(GlobalModule.Manager.ServerEntityList.IndexOf(server))
            CType(addonManager, Form).Show()
        ElseIf server.ServerVersionType = Server.EServerVersionType.Forge Then
            addonManager = New ForgeModManager(GlobalModule.Manager.ServerEntityList.IndexOf(server))
            CType(addonManager, Form).Show()
        ElseIf server.ServerVersionType = Server.EServerVersionType.Nukkit Then
            addonManager = New NukkitPluginManager(GlobalModule.Manager.ServerEntityList.IndexOf(server))
            CType(addonManager, Form).Show()
        ElseIf server.ServerVersionType = Server.EServerVersionType.PocketMine Then
            addonManager = New PocketMinePluginManager(GlobalModule.Manager.ServerEntityList.IndexOf(server))
            CType(addonManager, Form).Show()
        ElseIf server.ServerVersionType = Server.EServerVersionType.Cauldron OrElse
            server.ServerVersionType = Server.EServerVersionType.Thermos OrElse
            server.ServerVersionType = Server.EServerVersionType.Contigo OrElse
            server.ServerVersionType = Server.EServerVersionType.Kettle OrElse
            server.ServerVersionType = Server.EServerVersionType.CatServer Then
            addonManager = New HybridMPManager(GlobalModule.Manager.ServerEntityList.IndexOf(server))
            CType(addonManager, Form).Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Static wlForm As WhiteListForm
        wlForm = New WhiteListForm(server)
        wlForm.ShowDialog()
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
                TaskList(e.Index).Enabled = True
            Case CheckState.Unchecked
                TaskList(e.Index).Enabled = False
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
                ThreadTaskDictionary.Remove(TaskList(TaskListBox.SelectedIndex))
            End If
            RemoveTaskAt(TaskListBox.SelectedIndex)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AddHandler server.ServerInfoUpdated, AddressOf Server_ServerInfoUpdated
        server.CheckForUpdate()
    End Sub

    Private Sub MemoryMaxBox_ValueChanged(sender As Object, e As EventArgs) Handles ServerMemoryMaxBox.ValueChanged
        server.ServerMemoryMax = ServerMemoryMaxBox.Value
        If GlobalModule.Manager.Is32BitJava = True And GlobalModule.Manager.HasJava And ServerMemoryMaxBox.Value > 1024 Then
            Label12.Text = "MB (記憶體過大)"
            Label12.ForeColor = Color.Red
        Else
            Label12.Text = "MB"
            Label12.ForeColor = Color.Black
        End If
    End Sub

    Private Sub MemoryMinBox_ValueChanged(sender As Object, e As EventArgs) Handles ServerMemoryMinBox.ValueChanged
        server.ServerMemoryMin = ServerMemoryMinBox.Value
        If GlobalModule.Manager.Is32BitJava = True And GlobalModule.Manager.HasJava And ServerMemoryMinBox.Value > 1024 Then
            Label14.Text = "MB (記憶體過大)"
            Label14.ForeColor = Color.Red
        Else
            Label14.Text = "MB"
            Label14.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
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
End Class
