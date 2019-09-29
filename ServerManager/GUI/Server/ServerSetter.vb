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
    Dim server As ServerBase
    Friend serverOptions As IServerProperties
    Sub New(ByRef server As ServerBase)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
    End Sub
    Sub SetUpdateInfo()
        BeginInvokeIfRequired(Me, Sub()
                                      Dim version As String = VersionLabel.Text
                                      version = version.Replace("<ServerVersionType>", ServerMaker.SoftwareDictionary(server.GetInternalName).ReadableName)
                                      If server.CanUpdate Then
                                          version = version.Replace("<ServerVersionStatus>", "可更新")
                                      Else
                                          version = version.Replace("<ServerVersionStatus>", "無更新")
                                      End If
                                      VersionLabel.Text = version
                                  End Sub)
    End Sub
    Private Sub ServerSetter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MapPanel.Controls.Add(New MapView(server) With {.Dock = DockStyle.Fill})
        SetUpdateInfo()
        UpdateButton.Enabled = server.CanUpdate
        Task.Run(New Action(Sub()
                                serverOptions = server.GetServerProperties
                                BeginInvoke(Sub()
                                                AdvancedPropertyGrid.SelectedObject = serverOptions
                                                If (TypeOf server Is Memoryable) Then
                                                    Dim _server As Memoryable = DirectCast(server, Memoryable)
                                                    ServerMemoryMaxBox.Value = _server.ServerMemoryMax
                                                    ServerMemoryMinBox.Value = _server.ServerMemoryMin
                                                End If
                                            End Sub)
                            End Sub))
        Dim enablePlugins As Boolean = TypeOf server Is IBukkit
        Dim enableMods As Boolean = TypeOf server Is IForge
        If enablePlugins And enableMods Then
            PluginManageButton.Enabled = True
            PluginManageButton.Text = "管理插件/模組"
        ElseIf enablePlugins Then
            PluginManageButton.Enabled = True
            PluginManageButton.Text = "管理插件"
        ElseIf enableMods Then
            PluginManageButton.Enabled = True
            PluginManageButton.Text = "管理模組"
        Else
            PluginManageButton.Enabled = False
            PluginManageButton.Text = "無可用的附加元件介面"
        End If
        TaskList = server.ServerTasks.ToList
        For Each task In TaskList
            TaskListBox.SetItemChecked(TaskListBox.Items.Add(task.Name), task.Enabled)
        Next
        Dim optionObjects As AbstractSoftwareOptions() = server.GetOptionObjects
        If optionObjects IsNot Nothing AndAlso optionObjects.Count > 0 Then
            AdvancedTabPage.Text = "伺服器主設定檔"
            For Each _option In optionObjects
                Dim tab As New TabPage(_option.GetOptionsTitle)
                tab.Controls.Add(New PropertyGrid() With {.Dock = DockStyle.Fill, .SelectedObject = _option})
                SettingTabControl.TabPages.Add(tab)
            Next
        End If
    End Sub

    Private Sub UpdateButton_Click(sender As Object, e As EventArgs) Handles UpdateButton.Click
        AddHandler server.ServerInfoUpdated, AddressOf Server_ServerInfoUpdated
        AddHandler server.ServerDownloadStart, Sub() AddHandler server.ServerDownloading, AddressOf Server_ServerUpdating
        AddHandler server.ServerDownloadEnd, Sub() RemoveHandler server.ServerDownloading, AddressOf Server_ServerUpdating
        server.UpdateServer()
    End Sub
    Private Sub Server_ServerInfoUpdated()
        BeginInvokeIfRequired(Me, Sub()
                                      Dim version As String = "<ServerVersionType>：<ServerVersionStatus>"
                                      version = version.Replace("<ServerVersionType>", ServerMaker.SoftwareDictionary(server.GetInternalName).ReadableName)
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
                                      version = version.Replace("<ServerVersionType>", ServerMaker.SoftwareDictionary(server.GetInternalName).ReadableName)
                                      version = version.Replace("<ServerVersionStatus>", "更新中 (" & percent & "%)")
                                      VersionLabel.Text = version
                                  End Sub)
    End Sub

    Private Sub ServerSetter_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        server.ServerTasks = TaskList.ToArray
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
        Static addonManager As IAddonManagerGUI
        If TypeOf server Is IBukkit AndAlso TypeOf server Is IForge Then
            addonManager = New HybridMPManager(GlobalModule.Manager.ServerEntityList.IndexOf(server))
            CType(addonManager, Form).Show()
        ElseIf TypeOf server Is IBukkit Then
            addonManager = New PluginManager(GlobalModule.Manager.ServerEntityList.IndexOf(server))
            CType(addonManager, Form).Show()
        ElseIf TypeOf server Is IForge Then
            addonManager = New ModManager(GlobalModule.Manager.ServerEntityList.IndexOf(server))
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
                    taskDialog.TaskPeriodUnitCombo.SelectedIndex = task.RepeatingPeriodUnit - 1
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
        UpdateButton.Enabled = server.CanUpdate
    End Sub

    Private Sub MemoryMaxBox_ValueChanged(sender As Object, e As EventArgs) Handles ServerMemoryMaxBox.ValueChanged
        If TypeOf server Is Memoryable Then
            DirectCast(server, Memoryable).ServerMemoryMax = ServerMemoryMaxBox.Value
            If GlobalModule.Manager.Is32BitJava = True And GlobalModule.Manager.HasJava And ServerMemoryMaxBox.Value > 1024 Then
                Label12.Text = "MB (記憶體過大)"
                Label12.ForeColor = Color.Red
            Else
                Label12.Text = "MB"
                Label12.ForeColor = Color.Black
            End If
        End If
    End Sub

    Private Sub MemoryMinBox_ValueChanged(sender As Object, e As EventArgs) Handles ServerMemoryMinBox.ValueChanged
        If TypeOf server Is Memoryable Then
            DirectCast(server, Memoryable).ServerMemoryMin = ServerMemoryMinBox.Value
            If GlobalModule.Manager.Is32BitJava = True And GlobalModule.Manager.HasJava And ServerMemoryMinBox.Value > 1024 Then
                Label14.Text = "MB (記憶體過大)"
                Label14.ForeColor = Color.Red
            Else
                Label14.Text = "MB"
                Label14.ForeColor = Color.Black
            End If
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
