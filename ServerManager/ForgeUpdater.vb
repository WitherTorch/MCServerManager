' Forge
' 1.1~1.2 > Download Server
' 1.3~1.5.1 > Download Universal
' 1.5.2 ~ > Download Installer(*.jar)
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class ForgeUpdater
    Dim serverDir As String = ""
    Private _parent As ServerCreateHelper

    Friend Sub New(serverDir As String, parent As ServerCreateHelper)
        Me.serverDir = serverDir
        Me._parent = parent
    End Sub
    Friend Event ForgeDownloadProgressChanged(p As Net.DownloadProgressChangedEventArgs)
    Friend Event ForgeDownloadStart()
    Friend Event ForgeDownloadEnd()

    Protected w As New Net.WebClient()
    Friend Sub ForceClose()
        w.CancelAsync()
    End Sub
    Private Sub DownloadMinecraftForgeServerInstaller(craftVersion As String, forgeVersion As String)
        Dim forgeServerURL As String =
            String.Format("http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}/forge-{0}-{1}-installer.jar", craftVersion, forgeVersion)
        If craftVersion = "1.8.9" OrElse craftVersion = "1.7.10" Then
            forgeServerURL = String.Format("http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}-{0}/forge-{0}-{1}-{0}-installer.jar", craftVersion, forgeVersion)
        End If

        AddHandler w.DownloadProgressChanged, Sub(sender, e)
                                                  RaiseEvent ForgeDownloadProgressChanged(e)
                                              End Sub
        AddHandler w.DownloadFileCompleted, Sub(sender, e)
                                                If e.Cancelled = False Then RaiseEvent ForgeDownloadEnd()
                                            End Sub
        w.DownloadFileAsync(New Uri(forgeServerURL), IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.installer.jar", craftVersion)))
    End Sub
    Private Sub DownloadMinecraftForgeServerUniversal(craftVersion As String, forgeVersion As String)
        Dim forgeServerURL As String =
            String.Format("http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}/forge-{0}-{1}-universal.jar", craftVersion, forgeVersion)
        AddHandler w.DownloadProgressChanged, Sub(sender, e)
                                                  RaiseEvent ForgeDownloadProgressChanged(e)
                                              End Sub
        AddHandler w.DownloadFileCompleted, Sub(sender, e)
                                                If e.Cancelled = False Then RaiseEvent ForgeDownloadEnd()
                                            End Sub
        w.DownloadFileAsync(New Uri(forgeServerURL), IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.universal.jar", craftVersion)))
    End Sub
    Private Sub DownloadMinecraftForgeServerJar(craftVersion As String, forgeVersion As String)
        Dim forgeServerURL As String =
            String.Format("http://files.minecraftforge.net/maven/net/minecraftforge/forge/{0}-{1}/forge-{0}-{1}-server.jar", craftVersion, forgeVersion)
        AddHandler w.DownloadProgressChanged, Sub(sender, e)
                                                  RaiseEvent ForgeDownloadProgressChanged(e)
                                              End Sub
        AddHandler w.DownloadFileCompleted, Sub(sender, e)
                                                If e.Cancelled = False Then RaiseEvent ForgeDownloadEnd()
                                            End Sub
        w.DownloadFileAsync(New Uri(forgeServerURL), IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.server.jar", craftVersion)))
    End Sub
    Sub DownloadForge(craftVersion As String, forgeVersion As String)
        Dim v As New Version(craftVersion)
        If v > New Version(1, 5, 1) Then
            DownloadMinecraftForgeServerInstaller(craftVersion, forgeVersion)
        ElseIf ((v <= New Version(1, 5, 1)) And (v > New Version(1, 2, 5))) Then
            DownloadMinecraftForgeServerUniversal(craftVersion, forgeVersion)
        ElseIf (v <= New Version(1, 2, 5)) Then
            DownloadMinecraftForgeServerJar(craftVersion, forgeVersion)
        End If
    End Sub

    Sub DeleteForgeInstaller(craftVersion As String, forgeVersion As String)
        If New Version(craftVersion) > New Version(1, 5, 1) Then
            My.Computer.FileSystem.DeleteFile(IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.installer.jar", craftVersion)), FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
        End If
    End Sub

    Friend Function InstallForge(craftVersion As String, forgeVersion As String) As DialogResult
        If New Version(craftVersion) > New Version(1, 5, 1) Then
            Dim watcher As New ForgeInstallWindow()
            watcher.Run(IO.Path.Combine(JavaPath, "java.exe"), " -Xms" & ServerMemoryMin & "M -Xmx" & ServerMemoryMax & "M -jar " & """" & IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.installer.jar", craftVersion)) & """" & " nogui --installServer", serverDir)
            Dim rsult As DialogResult
            _parent.Invoke(Sub() rsult = watcher.ShowDialog(_parent))
            Return rsult
        Else
            Return DialogResult.OK
        End If
    End Function
    Friend Sub DisposeClient()
        w.Dispose()
        w = Nothing
    End Sub
End Class
Public Class ForgeUtil2
    Inherits ForgeUpdater
    Dim serverDir As String
    Sub New(serverDir As String)
        MyBase.New(serverDir, Nothing)
        Me.serverDir = serverDir
    End Sub
    Friend Function InstallForge2(craftVersion As String, forgeVersion As String, memoryMin As Integer, memoryMax As Integer) As DialogResult
        If New Version(craftVersion) > New Version(1, 5, 1) Then
            Dim watcher As New ForgeInstallWindow()
            watcher.Run(IO.Path.Combine(JavaPath, "java.exe"), "-Xms" & memoryMin & "M -Xmx" & memoryMax & "M -jar " & """" & IO.Path.Combine(serverDir, String.Format("minecraft-forge.{0}.installer.jar", craftVersion)) & """" & " nogui --installServer", serverDir)
            Return watcher.ShowDialog()
        Else
            Return DialogResult.OK
        End If
    End Function
End Class