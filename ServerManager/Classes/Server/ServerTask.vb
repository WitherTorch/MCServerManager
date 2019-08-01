Public Class ServerTask
    Enum TaskMode
        Repeating
        Trigger
        QuickLaunch
    End Enum
    Enum TaskTriggerEvent
        None
        PlayerLogin
        PlayerLogout
        ServerStarted
        ServerClosed
        PlayerInputCommand
    End Enum
    Enum TaskPeriodUnit
        Tick
        Second
        Minute
        Hour
        Day
    End Enum
    Class TaskCommand
        Enum CommandAction
            None
            StopServer
            RestartServer
            RunCommand
        End Enum
        Public Property Action As CommandAction = CommandAction.None
        Public Property Data As String = ""
    End Class
    Public Property Enabled As Boolean = True
    Public Property Mode As TaskMode
    Public Property TriggerEvent As TaskTriggerEvent = TaskTriggerEvent.None
    Public Property RepeatingPeriod As Long
    Public Property RepeatingPeriodUnit As TaskPeriodUnit
    Public Property Command As TaskCommand
    Public Property CheckRegex As String = ""
    Public Property Name As String
    Sub New(mode As TaskMode, name As String)
        Me.Mode = mode
        Me.Name = name
    End Sub

End Class
