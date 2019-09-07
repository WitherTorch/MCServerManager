Imports ServerManager 

Public Class SpigotServer
    Inherits CraftBukkitServer
    Protected spigotOptions As SpigotOptions
    Protected Friend Overrides Sub SetOptions()
        MyBase.SetOptions()
        spigotOptions = New SpigotOptions(IO.Path.Combine(ServerPath, "spigot.yml"))
        spigotOptions.UseOldVersionSetting = New Version(ServerVersion) <= New Version(1, 11, 2)
    End Sub
    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {bukkitOptions, spigotOptions}
    End Function
    Public Overrides Function GetInternalName() As String
        Return "Spigot"
    End Function
    Public Overrides Function CanUpdate() As Boolean
        Return False
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "spigot-" & ServerVersion & ".jar"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                ServerMemoryMax = IIf(IsNumeric(value), value, 0)
            Case "server-memory-min"
                ServerMemoryMin = IIf(IsNumeric(value), value, 0)
        End Select
    End Sub
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
        Dim targetURL As String = CraftBukkitVersionDict(ServerVersion)
        Dim url = (New HtmlAgilityPack.HtmlWeb).Load(targetURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[1]/div[1]/div[1]/div[1]/h2[1]/a[1]").GetAttributeValue("href", "")
        Dim DownloadPath As String = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "spigot-" & ServerVersion & ".jar")
        Dim task As New ServerDownloadTask
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Call OnServerDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        task.Download(url, DownloadPath)
        Return task
    End Function
End Class
