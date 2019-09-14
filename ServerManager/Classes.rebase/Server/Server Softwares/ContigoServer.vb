Public Class ContigoServer
    Inherits ThermosServer
    Friend Shared Shadows Sub GetVersionList()
    End Sub
    Public Overrides Function GetInternalName() As String
        Return "Contigo"
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "Contigo-" & ServerVersion & ".jar"
    End Function
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim task As New ServerDownloadTask
        task.Download("https://www.dropbox.com/s/2l136lm1eesmo57/Contigo-1.7.10.zip?raw=1", IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "contigo-" & ServerVersion & ".zip"))
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               UnZipPackage(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "contigo-" & ServerVersion & ".zip"))
                                               ServerVersion = targetVersion
                                               Call OnServerDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        Return task
    End Function
End Class
