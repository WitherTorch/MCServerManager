Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class BungeeCord
    Inherits SolutionTargetBase
    Implements IMemoryChange
    Const BungeeCordApiJson = "https://ci.md-5.net/job/BungeeCord/lastStableBuild/api/json"
    Const BungeeCordDownloadLink = "https://ci.md-5.net/job/BungeeCord/lastStableBuild/artifact/bootstrap/target/BungeeCord.jar"
    Protected bungeeOptions As BungeeCordOptions
    Dim runningProcess As Process
    Dim serverProcesses As New List(Of Process)
    Public Property MemoryMax As Integer Implements IMemoryChange.MemoryMax
    Public Property MemoryMin As Integer Implements IMemoryChange.MemoryMin
    Protected Overrides Sub AddServer(ByRef server As ServerBase)
        MyBase.AddServer(server)
    End Sub
    Protected Overrides Sub RemoveServer(ByRef server As ServerBase)
        MyBase.RemoveServer(server)
    End Sub
    Protected Overrides Function GetServerFilter(server As ServerBase) As Boolean
        If TypeOf server Is VanillaServer Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Overrides Sub SaveSolution()
        If bungeeOptions IsNot Nothing Then bungeeOptions.SaveOption()
    End Sub
    Protected Overridable Sub StartServers()
        serverProcesses.Clear()
        For Each server In Servers
            If server.BeforeRunServer Then
                server.RunServer()
            End If
        Next
    End Sub
    Protected Overridable Sub ShutdownServers()

    End Sub
    Public Overrides Sub ShutdownSolution()
        If ProcessID <> 0 Then
            Dim process As Process = StartSoluton() '取得目前的處理序
            If process IsNot Nothing Then
                If process.HasExited = False Then
                    Try
                        process.StandardInput.WriteLine("end")
                        Dim dog As New Watchdog(process) ' 呼叫看門狗程序去監測伺服器是否關閉
                        dog.Run()
                    Catch ex As Exception
                    End Try
                    process.WaitForExit()
                End If
            End If
            ProcessID = 0
            runningProcess = Nothing
            IsRunning = False
        End If
    End Sub

    Protected Overrides Sub OnReadSolutionInfo(key As String, value As String)
    End Sub

    Public Overrides Function CanUpdate() As Boolean
        Try
            Try
                Dim client As New Net.WebClient()
                Dim jsonObject As JObject = JsonConvert.DeserializeObject(client.DownloadString(BungeeCordApiJson))
                Dim latestVersion As Integer = jsonObject.GetValue("id")
                Return latestVersion > SolutionTargetVersion
            Catch ex As Net.WebException
                Return False
            End Try
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Overrides Function GetTargetFileName() As String
        Return "BungeeCord.jar"
    End Function

    Public Overrides Function GetInternalName() As String
        Return "BungeeCord"
    End Function

    Public Overrides Function GetSoftwareVersionString() As String
        Return "BungeeCord #" & SolutionTargetVersion
    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {bungeeOptions}
    End Function

    Public Overrides Function GetAvailableVersion() As String
        Dim client As New Net.WebClient()
        Dim jsonObject As JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(client.DownloadString(BungeeCordApiJson))
        Dim latestVersion As Integer = jsonObject.GetValue("id")
        Return latestVersion.ToString
    End Function

    Public Overrides Function GetAdditionalSolutionInfo() As String()
        Return {}
    End Function

    Public Overrides Function StartSoluton() As Process
        If ProcessID = 0 Then
            Dim processInfo As New ProcessStartInfo(GetJavaPath(),
                                                String.Format("--Djline.terminal=jline.UnsupportedTerminal -Xms{0}M -Xmx{1}M {2} -jar ""{3}"" nogui",
                                                              IIf(MemoryMin > 0, MemoryMin, GlobalModule.ServerMemoryMin),
                                                              IIf(MemoryMax > 0, MemoryMin, GlobalModule.ServerMemoryMax),
                                                               JavaArguments, SolutionPath.TrimEnd(seperator) & seperator & GetTargetFileName()))
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = True
            processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
            processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
            processInfo.RedirectStandardOutput = True
            processInfo.RedirectStandardError = True
            processInfo.RedirectStandardInput = True
            processInfo.WorkingDirectory = SolutionPath
            runningProcess = Process.Start(processInfo) '回傳一個處理序
            ProcessID = runningProcess.Id
            IsRunning = True
        End If
        Return runningProcess
    End Function
    Protected seperator As String = IIf(IsUnixLikeSystem, "/", "\")
    Public Overrides Function DownloadAndInstallTarget(targetServerVersion As String) As ServerDownloadTask
        Dim DownloadPath As String = IO.Path.Combine(IIf(SolutionPath.EndsWith(seperator), SolutionPath, SolutionPath & seperator), "BungeeCord.jar")
        Dim task As New ServerDownloadTask
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnTargetDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnTargetDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               Call OnTargetDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnTargetDownloadStart()
                                         End Sub
        task.Download(BungeeCordDownloadLink, DownloadPath)
        Return task
    End Function

    Public Overrides Function UpdateTarget() As ServerDownloadTask
        Return DownloadAndInstallTarget("0")
    End Function
End Class
