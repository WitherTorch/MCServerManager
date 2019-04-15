﻿Imports System.Net
Imports Microsoft.VisualBasic.ApplicationServices

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
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
            My.Application.Log.DefaultFileLogWriter.Location = Logging.LogFileLocation.Custom
            My.Application.Log.DefaultFileLogWriter.CustomLocation = IO.Path.Combine(My.Application.Info.DirectoryPath, "error-logs")

        End Sub
    End Class
End Namespace