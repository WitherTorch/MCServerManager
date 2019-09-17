Imports ServerManager

Public Class BDSServer
    Inherits ServerBase
    Public Overrides Sub SaveServer()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub ShutdownServer()
        Throw New NotImplementedException()
    End Sub

    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Function CanUpdate() As Boolean
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetServerFileName() As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetInternalName() As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetSoftwareVersionString() As String
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetAvaillableVersions() As String()
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetAvaillableVersions(ParamArray args() As (String, String)) As String()
        Throw New NotImplementedException()
    End Function

    Public Overrides Function GetAdditionalServerInfo() As String()
        Throw New NotImplementedException()
    End Function

    Public Overrides Function RunServer() As Process
        Throw New NotImplementedException()
    End Function

    Public Overrides Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
        Throw New NotImplementedException()
    End Function

    Public Overrides Function UpdateServer() As ServerDownloadTask
        Throw New NotImplementedException()
    End Function
End Class
