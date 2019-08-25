Imports System.ComponentModel

Public Class ServerTaskCreateDialog
    Dim _console As ServerConsole
    Dim _setter As ServerSetter
    Dim _task As ServerTask
    Dim isEditMode As Boolean = False
    Dim inSetting As Boolean = False

    Dim taskMethods As String() = {"#sleep 20", "#randomise 100", "#backup C:\backupServer\", "<#YEAR>", "<#MONTH>", "<#DAY>", "<#HOUR>", "<#MINUTE>", "<#SECOND>", "<#DAYOFWEEK>", "<#RANDOM>"}
    Dim taskMethodsDescriptions As String() = {"在指定的遊戲刻數內暫停輸出排程指令(預設指令中的20可以替換)", "計算1到指定數字的亂數序列(預設指令中的100可以替換)", "備份伺服器到指定的資料夾", "現在系統時間的年分", "現在系統時間的月份", "現在系統時間的天數", "現在系統時間的小時數", "現在系統時間的分鐘數", "現在系統時間的秒數", "現在系統時間於一周內的天數", "取出亂數序列中的數字"}

    Dim playerLoginParameters As String() = {"<$ID>"}
    Dim playerLoginParameterDescriptions As String() = {"目標玩家的ID"}

    Dim playerLogoutParameters As String() = {"<$ID>"}
    Dim playerLogoutParameterDescriptions As String() = {"目標玩家的ID"}

    Dim playerInputCommandParameters As String() = {"<$ID>", "<$COMMANDNAME>", "<$COMMANDARG>", "<$COMMANDFULL>"}
    Dim playerInputCommandParameterDescriptions As String() = {"目標玩家的ID", "玩家輸入的指令名稱", "玩家輸入的指令參數", "玩家輸入的指令"}
    Public Sub New(console As ServerConsole, Optional ByRef preTask As ServerTask = Nothing)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _console = console
        _task = preTask
        isEditMode = (IsNothing(preTask) = False)
        inSetting = False
    End Sub
    Public Sub New(setter As ServerSetter, Optional ByRef preTask As ServerTask = Nothing)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _setter = setter
        _task = preTask
        isEditMode = IsNothing(preTask) = False
        inSetting = True
    End Sub
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub ServerTaskCreateDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TaskTypeComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TaskTypeComboBox.SelectedIndexChanged
        Select Case TaskTypeComboBox.SelectedIndex
            Case 0
                Label2.Enabled = True
                Label3.Enabled = True
                TaskPeriodUnitCombo.Enabled = True
                TaskPeriodUpDown.Enabled = True
                Label4.Enabled = False
                EventComboBox.Enabled = False
                Label5.Enabled = False
            Case 1
                Label2.Enabled = False
                Label3.Enabled = False
                TaskPeriodUnitCombo.Enabled = False
                TaskPeriodUpDown.Enabled = False
                Label4.Enabled = True
                EventComboBox.Enabled = True
                Label5.Enabled = True
            Case 2
                Label2.Enabled = False
                Label3.Enabled = False
                TaskPeriodUnitCombo.Enabled = False
                TaskPeriodUpDown.Enabled = False
                Label4.Enabled = False
                EventComboBox.Enabled = False
                Label5.Enabled = False
            Case Else
                Label2.Enabled = False
                Label3.Enabled = False
                TaskPeriodUnitCombo.Enabled = False
                TaskPeriodUpDown.Enabled = False
                Label4.Enabled = False
                EventComboBox.Enabled = False
                Label5.Enabled = False
        End Select
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TaskTypeComboBox.SelectedIndex >= 0 And TaskNameTextBox.Text.Trim <> "" Then
            Select Case TaskTypeComboBox.SelectedIndex
                Case 0
                    Dim task As ServerTask = _task
                    If IsNothing(task) Then
                        task = New ServerTask(ServerTask.TaskMode.Repeating, TaskNameTextBox.Text)
                    Else
                        task.Mode = ServerTask.TaskMode.Repeating
                        task.Name = TaskNameTextBox.Text
                    End If
                    If TaskPeriodUnitCombo.SelectedIndex >= 0 Then
                        task.RepeatingPeriodUnit = TaskPeriodUnitCombo.SelectedIndex
                        task.RepeatingPeriod = TaskPeriodUpDown.Value
                        If RunComboBox.SelectedIndex >= 0 And (RunComboBox.SelectedIndex <> 2 OrElse RunCommandArgBox.Text.Trim <> "") Then
                            Dim command As New ServerTask.TaskCommand()
                            command.Action = RunComboBox.SelectedIndex + 1
                            command.Data = RunCommandArgBox.Text
                            task.Command = command
                            If isEditMode = False Then
                                If inSetting Then
                                    _setter.AddTask(task)
                                Else
                                    _console.AddTask(task)
                                End If
                            End If
                            Close()
                        End If
                    End If
                Case 1
                    Dim task As ServerTask = _task
                    If IsNothing(task) Then
                        task = New ServerTask(ServerTask.TaskMode.Trigger, TaskNameTextBox.Text)
                    Else
                        task.Mode = ServerTask.TaskMode.Trigger
                        task.Name = TaskNameTextBox.Text
                    End If
                    If EventComboBox.SelectedIndex >= 0 Then
                        task.TriggerEvent = EventComboBox.SelectedIndex + 1
                        If task.TriggerEvent = ServerTask.TaskTriggerEvent.PlayerInputCommand Then
                            task.CheckRegex = TextBox1.Text
                        End If
                        If RunComboBox.SelectedIndex >= 0 And (RunComboBox.SelectedIndex <> 2 OrElse RunCommandArgBox.Text.Trim <> "") Then
                            Dim command As New ServerTask.TaskCommand()
                            command.Action = RunComboBox.SelectedIndex + 1
                            command.Data = RunCommandArgBox.Text
                            task.Command = command
                            If isEditMode = False Then
                                If inSetting Then
                                    _setter.AddTask(task)
                                Else
                                    _console.AddTask(task)
                                End If
                            End If
                            Close()
                        End If
                    End If
                Case 2
                    Dim task As ServerTask = _task
                    If IsNothing(task) Then
                        task = New ServerTask(ServerTask.TaskMode.QuickLaunch, TaskNameTextBox.Text)
                    Else
                        task.Mode = ServerTask.TaskMode.QuickLaunch
                        task.Name = TaskNameTextBox.Text
                    End If
                    If RunComboBox.SelectedIndex >= 0 And (RunComboBox.SelectedIndex <> 2 OrElse RunCommandArgBox.Text.Trim <> "") Then
                        Dim command As New ServerTask.TaskCommand()
                        command.Action = RunComboBox.SelectedIndex + 1
                        command.Data = RunCommandArgBox.Text
                        task.Command = command
                        If isEditMode = False Then
                            If inSetting Then
                                _setter.AddTask(task)
                            Else
                                _console.AddTask(task)
                            End If
                        End If
                        Close()
                    End If
            End Select
        End If
    End Sub

    Private Sub ServerTaskCreateDialog_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub InputArgsButton_Click(sender As Object, e As EventArgs) Handles InputArgsButton.Click
        If RunComboBox.SelectedIndex = 2 Then
            Dim paraDialog As New TaskParameterDialog(Me)
            For Each para In taskMethods
                Dim item As New ListViewItem(para)
                item.SubItems.Add(taskMethodsDescriptions(taskMethods.ToList.IndexOf(para)))
                paraDialog.ListView1.Items.Add(item)
            Next
            Select Case EventComboBox.SelectedIndex
                Case 0
                    For Each para In playerLoginParameters
                        Dim item As New ListViewItem(para)
                        item.SubItems.Add(playerLoginParameterDescriptions(playerLoginParameters.ToList.IndexOf(para)))
                        paraDialog.ListView1.Items.Add(item)
                    Next
                Case 1
                    For Each para In playerLogoutParameters
                        Dim item As New ListViewItem(para)
                        item.SubItems.Add(playerLogoutParameterDescriptions(playerLogoutParameters.ToList.IndexOf(para)))
                        paraDialog.ListView1.Items.Add(item)
                    Next
                Case 4
                    For Each para In playerInputCommandParameters
                        Dim item As New ListViewItem(para)
                        item.SubItems.Add(playerInputCommandParameterDescriptions(playerInputCommandParameters.ToList.IndexOf(para)))
                        paraDialog.ListView1.Items.Add(item)
                    Next
            End Select
            paraDialog.ShowDialog()
        End If
    End Sub
    Private Sub EventComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EventComboBox.SelectedIndexChanged
        Select Case EventComboBox.SelectedIndex
            Case 4
                GroupBox3.Enabled = True
            Case Else
                GroupBox3.Enabled = False
        End Select
    End Sub

    Private Sub RunComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RunComboBox.SelectedIndexChanged
        Select Case RunComboBox.SelectedIndex
            Case 2
                RunCommandArgBox.Enabled = True
                InputArgsButton.Enabled = True
            Case Else
                RunCommandArgBox.Enabled = False
                InputArgsButton.Enabled = False
        End Select
    End Sub
End Class