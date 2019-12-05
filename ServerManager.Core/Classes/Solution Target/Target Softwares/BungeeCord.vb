Imports ServerManager

Public Class BungeeCord
    Inherits SolutionTargetBase
    Implements IMemoryChange
    Protected bungeeOptions As BungeeCordOptions
    Public Property MemoryMax As Integer Implements IMemoryChange.MemoryMax
    Public Property MemoryMin As Integer Implements IMemoryChange.MemoryMin
    Protected Overrides Sub AddServer(ByRef server As ServerBase)
        MyBase.AddServer(server)
    End Sub
    Protected Overrides Sub RemoveServer(ByRef server As ServerBase)
        MyBase.RemoveServer(server)
    End Sub
    Protected Overrides Function GetServerFilter(server As ServerBase) As Boolean
        If TypeOf server Is VanillaServer Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Overrides Sub SaveSolution()
    End Sub

    Public Overrides Sub ShutdownSolution()

    End Sub

    Protected Overrides Sub OnReadSolutionInfo(key As String, value As String)

    End Sub

    Public Overrides Function CanUpdate() As Boolean

    End Function

    Public Overrides Function GetTargetFileName() As String
        Return "BungeeCord.jar"
    End Function

    Public Overrides Function GetInternalName() As String
        Return "BungeeCord"
    End Function

    Public Overrides Function GetSoftwareVersionString() As String
        Return "BungeeCord #" & SolutionTargetVersion
    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {bungeeOptions}
    End Function

    Public Overrides Function GetAvailableVersion() As String

    End Function

    Public Overrides Function GetAdditionalSolutionInfo() As String()

    End Function

    Public Overrides Function StartSoluton() As Process

    End Function

    Public Overrides Function DownloadAndInstallTarget(targetServerVersion As String) As ServerDownloadTask

    End Function

    Public Overrides Function UpdateTarget() As ServerDownloadTask

    End Function
End Class
