Public Class ServiceHost
    Dim ftpServer As FTPServerLib.FtpServer
    Public Event OnMessage(msg As String)
    Sub New(WorkingDirectory As String)
        ftpServer = New FTPServerLib.FtpServer(WorkingDirectory, Sub(msg)
                                                                     RaiseEvent OnMessage(msg)
                                                                 End Sub)
    End Sub
    Sub Start()
        ftpServer.Start()
    End Sub
    Sub AddUser(username As String, password As String)
        FTPServerLib.UserStore.AddUser(username, password)
    End Sub
    Sub RemoveUser(username As String)
        FTPServerLib.UserStore.RemoveUser(username)
    End Sub
    Sub [Stop]()
        ftpServer.Stop()
    End Sub
End Class
