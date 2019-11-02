
Public Class CustomServer
    Inherits ServerBase
    Protected seperator As String = IIf(IsUnixLikeSystem, "/", "\")
    Public ServerRunFile As String
    Public Overrides Sub SaveServer()
        GenerateServerInfo()
    End Sub

    Public Overrides Sub ShutdownServer()
        If ProcessID <> 0 Then
            Dim process As Process = RunServer() '取得目前的處理序
            If process IsNot Nothing Then
                If process.HasExited = False Then
                    Try
                        process.StandardInput.WriteLine("stop")
                        Dim dog As New Watchdog(process) ' 呼叫看門狗程序去監測伺服器是否關閉
                        dog.Run()
                    Catch ex As Exception
                    End Try
                    process.WaitForExit()
                End If
            End If
            ProcessID = 0
            IsRunning = False
        End If
    End Sub

    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-run-file"
                ServerRunFile = value
        End Select
    End Sub

    Public Overrides Function CanUpdate() As Boolean
        Return False
    End Function

    Public Overrides Function GetServerFileName() As String
        Return ServerRunFile
    End Function

    Public Overrides Function GetInternalName() As String
        Return "Custom"
    End Function

    Public Overrides Function GetSoftwareVersionString() As String
        Return "自定義伺服器(程式檔案:" & New IO.FileInfo(ServerRunFile).Name & ")"
    End Function

    Public Overrides Function GetServerProperties() As IServerProperties
        Return Nothing
    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {}
    End Function

    Public Overrides Function GetAvailableVersions() As String()
        Return {"目標:批次檔", "目標:執行檔", "目標:Java JAR"}
    End Function

    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function

    Public Overrides Function GetAdditionalServerInfo() As String()
        Return {}
    End Function
    Public Overrides Function BeforeRunServer() As Boolean
        Select Case New IO.FileInfo(ServerRunFile).Extension
            Case ".jar"
                If JavaPath = "" Then
                    GUIHost.GUIHandler.MsgBox("未安裝Java 或 正在偵測", APP_NAME)
                    Return False
                End If
        End Select
        Return True
    End Function
    Public Overrides Function RunServer() As Process
        If ProcessID = 0 Then
            Select Case New IO.FileInfo(ServerRunFile).Extension
                Case ".bat", ".sh", ".exe"
                    Dim processInfo As New ProcessStartInfo(GetServerFileName())
                    processInfo.UseShellExecute = False
                    processInfo.CreateNoWindow = True
                    processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
                    processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
                    processInfo.RedirectStandardOutput = True
                    processInfo.RedirectStandardError = True
                    processInfo.RedirectStandardInput = True
                    processInfo.WorkingDirectory = ServerPath
                    Dim returnProcess As Process = Process.Start(processInfo) '回傳一個處理序
                    ProcessID = returnProcess.Id
                    IsRunning = True
                    Return returnProcess
                Case ".jar"
                    Dim processInfo As New ProcessStartInfo(GetJavaPath(),
                                                String.Format("-Dfile.encoding=UTF-8 -Djline.terminal=jline.UnsupportedTerminal -Xms{0}M -Xmx{1}M {2} -jar ""{3}"" nogui",
                                                              IIf(ServerMemoryMin > 0, ServerMemoryMin, GlobalModule.ServerMemoryMin),
                                                              IIf(ServerMemoryMax > 0, ServerMemoryMin, GlobalModule.ServerMemoryMax),
                                                               JavaArguments, ServerPath.TrimEnd(seperator) & seperator & GetServerFileName()))
                    processInfo.UseShellExecute = False
                    processInfo.CreateNoWindow = True
                    processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
                    processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
                    processInfo.RedirectStandardOutput = True
                    processInfo.RedirectStandardError = True
                    processInfo.RedirectStandardInput = True
                    processInfo.WorkingDirectory = ServerPath
                    Dim returnProcess As Process = Process.Start(processInfo) '回傳一個處理序
                    ProcessID = returnProcess.Id
                    IsRunning = True
                    Return returnProcess
                Case Else
                    Return Nothing
            End Select
        Else
            Return Process.GetProcessById(ProcessID)
        End If
    End Function

    Public Overrides Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
        Dim dialog As IOpenFileDialog = GUIHost.GenerateObjectByInterface(Of IOpenFileDialog)
        Select Case targetServerVersion
            Case "目標:批次檔"
                dialog.Filter = "批次檔 (*.bat,*.sh)|*.bat ,*.sh"
            Case "目標:執行檔"
                dialog.Filter = "Windows 應用程式 (*.exe)|*.exe"
            Case "目標:Java JAR"
                dialog.Filter = "Java JAR (*.jar)|*.jar"
        End Select
        If dialog.ShowDialog = DialogResult.OK Then
            ServerRunFile = dialog.FileName
        End If
        Return New ServerDownloadTask
    End Function

    Public Overrides Function UpdateServer() As ServerDownloadTask
        Return Nothing
    End Function
End Class
