Public Class ServiceHost
    Dim ftpServer As FTPServerLib.FtpServer
    Sub New(WorkingDirectory As String)
        ftpServer = New FTPServerLib.FtpServer(WorkingDirectory)
    End Sub
    Sub Start()
        ftpServer.Start()
    End Sub
    Sub AddUser(username As String, password As String)
        FTPServerLib.UserStore.AddUser(username, password)
    End Sub
    Sub RemoveUser(username As String, password As String)
        FTPServerLib.UserStore.RemoveUser(username, password)
    End Sub

End Class
