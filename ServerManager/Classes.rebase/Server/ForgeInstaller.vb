Public Class ForgeInstaller
    Dim serverDir As String
    Sub New(serverDirectory As String)
        serverDir = serverDirectory
    End Sub
    Private Function DownloadMinecraftForgeServerInstaller(craftVersion As String, forgeVersion As String) As ServerDownloadTask
        Dim forgeServerURL As String =
            String.Format("http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}/forge-{0}-{1}-installer.jar", craftVersion, forgeVersion)
        If craftVersion = "1.8.9" OrElse craftVersion = "1.7.10" Then
            forgeServerURL = String.Format("http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}-{0}/forge-{0}-{1}-{0}-installer.jar", craftVersion, forgeVersion)
        End If
        Dim task As New ServerDownloadTask
        task.Download(forgeServerURL, IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.installer.jar", craftVersion)))
        Return task
    End Function
    Private Function DownloadMinecraftForgeServerUniversal(craftVersion As String, forgeVersion As String) As ServerDownloadTask
        Dim forgeServerURL As String =
            String.Format("http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}/forge-{0}-{1}-universal.jar", craftVersion, forgeVersion)
        Dim task As New ServerDownloadTask
        task.Download(forgeServerURL, IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.universal.jar", craftVersion)))
        Return task
    End Function
    Private Function DownloadMinecraftForgeServerJar(craftVersion As String, forgeVersion As String) As ServerDownloadTask
        Dim forgeServerURL As String =
            String.Format("http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}/forge-{0}-{1}-server.jar", craftVersion, forgeVersion)
        Dim task As New ServerDownloadTask
        task.Download(forgeServerURL, IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.server.jar", craftVersion)))
        Return task
    End Function
    Function DownloadForge(craftVersion As String, forgeVersion As String) As ServerDownloadTask
        Dim v As New Version(craftVersion)
        If v > New Version(1, 5, 1) Then
            Return DownloadMinecraftForgeServerInstaller(craftVersion, forgeVersion)
        ElseIf ((v <= New Version(1, 5, 1)) And (v > New Version(1, 2, 5))) Then
            Return DownloadMinecraftForgeServerUniversal(craftVersion, forgeVersion)
        ElseIf (v <= New Version(1, 2, 5)) Then
            Return DownloadMinecraftForgeServerJar(craftVersion, forgeVersion)
        Else
            Return Nothing
        End If
    End Function
    Function InstallForge(craftVersion As String, forgeVersion As String) As DialogResult
        If New Version(craftVersion) > New Version(1, 5, 1) Then
            Dim watcher As New ForgeInstallWindow()
            watcher.Run(GetJavaPath(), " -Xms" & ServerMemoryMin & "M -Xmx" & ServerMemoryMax & "M -jar " & """" & IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.installer.jar", craftVersion)) & """" & " nogui --installServer", serverDir)
            Return watcher.ShowDialog()
        Else
            Return DialogResult.OK
        End If
    End Function
End Class
