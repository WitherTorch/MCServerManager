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
            Direct2DFactory.Dispose()
            DirectWriteFactory.Dispose()
            WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "servers.txt"), Newtonsoft.Json.JsonConvert.SerializeObject(ServerList.ToArray()))
            'WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "peServers.txt"), BedrockServerDirs)
            My.Settings.Save()
        End Sub

        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            'Dim msg As String = "應用程式發生錯誤!"
            'msg &= (vbNewLine)
            'msg &= (vbNewLine & "例外類型：" & e.Exception.GetType.ToString & " (" & e.Exception.InnerException.GetType.ToString & ")")
            'msg &= (vbNewLine & "內容：" & vbNewLine & e.Exception.InnerException.Message)
            'msg &= (vbNewLine & "StackTrace：" & e.Exception.InnerException.StackTrace)
            'If My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(My.Application.Info.DirectoryPath, "error-logs")) Then
            ' My.Application.Log.WriteEntry(msg, TraceEventType.Error)
            ' My.Application.Log.DefaultFileLogWriter.Flush()
            'End If
            'e.ExitApplication = False
        End Sub

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            'Enable MultiCore JIT
            Runtime.ProfileOptimization.SetProfileRoot(My.Application.Info.DirectoryPath)
            Runtime.ProfileOptimization.StartProfile("startup_cache.profile")

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
            EditorBridge.Hook()
            GUIHost.SetGUIHandler(New GUIHandler)
            GUIHost.SetGUIClass(Of ICMDWindow)(GetType(CMDWindow))
            GUIHost.SetGUIClass(Of IOpenFileDialog)(GetType(MyOpenFileDIalog))
            InternalSoftwareStartup.Startup()
            Dim servers = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(ReadAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "servers.txt")))
            If servers IsNot Nothing Then
                For Each item In servers
                    ServerList.Add(item.ToString)
                Next
            End If
        End Sub
    End Class
    Class GUIHandler
        Implements IGUIHandler
        Public Sub MsgBox(text As String) Implements IGUIHandler.MsgBox
            Microsoft.VisualBasic.MsgBox(text)
        End Sub

        Public Sub MsgBox(text As String, caption As String) Implements IGUIHandler.MsgBox
            Microsoft.VisualBasic.MsgBox(text,, caption)
        End Sub

        Public Function Shell(prompt As String, style As IGUIHandler.AppWinStyle, Optional timeout As Integer = 5000) As Integer Implements IGUIHandler.Shell
            Return Microsoft.VisualBasic.Shell(prompt, style, True, timeout)
        End Function
    End Class
End Namespace
