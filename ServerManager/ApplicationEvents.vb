Imports System.Net
Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Devices

Namespace My
    ' MyApplication 可以使用下列事件:
    ' Startup:在應用程式啟動時，但尚未建立啟動表單之前引發。
    ' Shutdown:在所有應用程式表單關閉之後引發。如果應用程式不正常終止，就不會引發此事件。
    ' UnhandledException:在應用程式發生未處理的例外狀況時引發。
    ' StartupNextInstance:在啟動單一執行個體應用程式且應用程式已於使用中時引發。 
    ' NetworkAvailabilityChanged:在建立或中斷網路連線時引發。
    Partial Friend Class MyApplication
        Private Sub MyApplication_Shutdown(sender As Object, e As EventArgs) Handles Me.Shutdown
            'WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "servers.txt"), JavaServerDirs)
            'WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "peServers.txt"), BedrockServerDirs)
            My.Settings.Save()
        End Sub
        Dim isDebug As Boolean
        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            Dim msg As String = "應用程式發生錯誤!"
            msg &= (vbNewLine)
            msg &= (vbNewLine & "例外類型：" & e.Exception.GetType.ToString & " (" & e.Exception.InnerException.GetType.ToString & ")")
            msg &= (vbNewLine & "內容：" & vbNewLine & e.Exception.InnerException.Message)
            msg &= (vbNewLine & "StackTrace：" & e.Exception.InnerException.StackTrace)
            If My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(My.Application.Info.DirectoryPath, "error-logs")) Then
                My.Application.Log.WriteEntry(msg, TraceEventType.Error)
                My.Application.Log.DefaultFileLogWriter.Flush()
            End If
            'e.ExitApplication = False
        End Sub

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            'Enable MultiCore JIT
            Runtime.ProfileOptimization.SetProfileRoot(My.Application.Info.DirectoryPath)
            Runtime.ProfileOptimization.StartProfile("startup_cache.profile")

            If e.CommandLine.Contains("-debug") Then
                isDebug = True
            Else
                isDebug = False
            End If
            Dim exePath As String = System.Windows.Forms.Application.ExecutablePath
            If IO.File.Exists(exePath & ".old") Then IO.File.Delete(exePath & ".old")
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            My.Application.Log.DefaultFileLogWriter.Location = Logging.LogFileLocation.Custom
            My.Application.Log.DefaultFileLogWriter.CustomLocation = IO.Path.Combine(My.Application.Info.DirectoryPath, "error-logs")
            Dim PlatID = System.Environment.OSVersion.Platform
            If PlatID = PlatformID.Unix OrElse PlatID = PlatformID.MacOSX OrElse PlatID = 128 Then
                IsUnixLikeSystem = True
            Else
                IsUnixLikeSystem = False
            End If
            Dim logicCore As Integer = System.Environment.ProcessorCount
            Dim cThread As Integer
            System.Threading.ThreadPool.GetMaxThreads(Nothing, cThread)
            Select Case logicCore'假定其1核有2條並行執行緒
                Case 1
                    System.Threading.ThreadPool.SetMaxThreads(4, cThread)
                Case 2
                    System.Threading.ThreadPool.SetMaxThreads(4, cThread)
                Case 3 To 7
                    System.Threading.ThreadPool.SetMaxThreads(Math.Round(logicCore * 6 / 2), cThread)
                Case 8 To 15
                    System.Threading.ThreadPool.SetMaxThreads(Math.Round(logicCore * 10 / 4), cThread)
                Case Is > 16 '
                    System.Threading.ThreadPool.SetMaxThreads(16, cThread) '16 條執行緒為極限
            End Select
        End Sub

        Private Sub MyApplication_NetworkAvailabilityChanged(sender As Object, e As NetworkAvailableEventArgs) Handles Me.NetworkAvailabilityChanged
            If GlobalModule.Manager IsNot Nothing AndAlso GlobalModule.Manager.IsDisposed = False Then GlobalModule.Manager.CheckNetwork()
        End Sub
    End Class
End Namespace
