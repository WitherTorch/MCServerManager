Public Class ThermosServer
    Inherits CauldronServer
    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {bukkitOptions, spigotOptions, cauldronOptions}
    End Function
    Protected Friend Overrides Sub GetOptions()
        bukkitOptions = New BukkitOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
        spigotOptions = New SpigotOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
        cauldronOptions = New CauldronOptions(IO.Path.Combine(ServerPath, "cauldron.yml"))
    End Sub
    Public Overrides Function GetAvailableVersions() As String()
        Return {"1.7.10"}
    End Function
    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function
    Friend Shared Shadows Sub GetVersionList()
    End Sub
    Public Overrides Function GetInternalName() As String
        Return "Thermos"
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "Thermos-" & ServerVersion & ".jar"
    End Function
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim task As New ServerDownloadTask
        task.Download("https://www.dropbox.com/s/zgo0fmbm0kfkjlp/Thermos.zip?raw=1", IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "thermos-" & ServerVersion & ".zip"))
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               UnZipPackage(IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "thermos-" & ServerVersion & ".zip"))
                                               ServerVersion = targetVersion
                                               GenerateServerEULA()
                                               Call OnServerDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        Return task
    End Function
End Class
