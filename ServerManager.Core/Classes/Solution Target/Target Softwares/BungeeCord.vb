Imports ServerManager

Public Class BungeeCord
    Inherits SolutionTargetBase
    Implements IMemoryChange
    Public Property MemoryMax As Integer Implements IMemoryChange.MemoryMax
    Public Property MemoryMin As Integer Implements IMemoryChange.MemoryMin
    Public Overrides Sub SaveSolution()

    End Sub

    Public Overrides Sub ShutdownSolution()

    End Sub

    Protected Overrides Sub OnReadSolutionInfo(key As String, value As String)

    End Sub

    Public Overrides Function CanUpdate() As Boolean

    End Function

    Public Overrides Function GetTargetFileName() As String

    End Function

    Public Overrides Function GetInternalName() As String

    End Function

    Public Overrides Function GetSoftwareVersionString() As String

    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()

    End Function

    Public Overrides Function GetAvailableVersions() As String()

    End Function

    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()

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
