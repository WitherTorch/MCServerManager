Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

''' <summary>
''' 原版伺服器
''' </summary>
Public Class VanillaServer
    Inherits ServerBase
    Implements Memoryable
    Protected seperator As String = IIf(IsUnixLikeSystem, "/", "\")
    Public Property ServerMemoryMax As Integer Implements Memoryable.ServerMemoryMax
    Public Property ServerMemoryMin As Integer Implements Memoryable.ServerMemoryMin
    Public ReadOnly Property Server2ndVersion As String
    Public Sub New()
        MyBase.New()
        SetOptions()
    End Sub
    Public Sub New(path As String)
        MyBase.New(path)
        SetOptions()
    End Sub
    Private _ServerOptions As JavaServerOptions
    ''' <summary>
    ''' 伺服器主設定檔(server.properties)
    ''' </summary>
    ''' <returns></returns>
    Property ServerOptions As JavaServerOptions
        Get
            Return _ServerOptions
        End Get
        Protected Set(value As JavaServerOptions)
            _ServerOptions = value
        End Set
    End Property
    Protected Friend Overridable Sub SetOptions()
        If String.IsNullOrWhiteSpace(ServerPath) Then
            Dim serverOptions As New JavaServerOptions()
            Me.ServerOptions = serverOptions.OutputOption
        Else
            Try
                If My.Computer.FileSystem.FileExists(IO.Path.Combine(ServerPath, "server.properties")) Then
                    Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                        Dim optionDict As New Dictionary(Of Integer, Boolean)
                        Do Until reader.EndOfStream
                            Try
                                Dim optionText As String = reader.ReadLine
                                If optionText.StartsWith("#") = False Then
                                    Dim options = optionText.Split("=", 2, StringSplitOptions.None)
                                    If options.Count = 2 Then
                                        ServerOptions.SetValue(options(0), options(1))
                                    ElseIf options.Count = 1 Then
                                        If options(0).Trim(" ") <> "" Then ServerOptions.SetValue(options(0), "")
                                    ElseIf options.Count = 0 Then
                                    Else
                                    End If
                                End If
                            Catch ex As Exception
                                Continue Do
                            End Try
                        Loop
                    End Using
                End If
            Catch fileException As IO.FileNotFoundException
            End Try
        End If
    End Sub
    Public Overrides Function CanUpdate() As Boolean
        Return False
    End Function
    Public Overrides Function GetServerFileName() As String
        If String.IsNullOrWhiteSpace(Server2ndVersion) Then
            Return "minecraft_server." & Server2ndVersion & ".jar"
        Else
            Return "minecraft_server." & ServerVersion & ".jar"
        End If
    End Function
    ''' <summary>
    ''' 啟動伺服器
    ''' </summary>
    Public Overrides Function RunServer() As Process
        If ProcessID = 0 Then
            Dim processInfo As New ProcessStartInfo(GetJavaPath(),
                                                String.Format("-Dfile.encoding=UTF-8 -Djline.terminal=jline.UnsupportedTerminal -Xms{0}M -Xmx{1}M {2} ""{3}"" nogui",
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
        Else
            Return Process.GetProcessById(ProcessID)
        End If
    End Function
    Public Overrides Sub SaveServer()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(ServerPath, "server.properties"), "", False)
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), New System.Text.UTF8Encoding(False))
        writer.WriteLine("# Minecraft server properties")
        writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy", System.Globalization.CultureInfo.CurrentUICulture))
        For Each [option] In ServerOptions.OutputOption
            writer.WriteLine(String.Format("{0}={1}", [option].Key, [option].Value))
        Next
        writer.WriteLine()
        writer.Flush()
        writer.Close()
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
    Public Overrides Function GetInternalName() As String
        Return "Vanilla"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        If Server2ndVersion = "" Then
            Return ServerVersion
        Else
            Return Server2ndVersion
        End If
    End Function
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & ServerMemoryMax,
                                                  "server-memory-min=" & ServerMemoryMin,
                                                  "vanilla-build-version=" & Server2ndVersion}
    End Function
    Dim vanilla_isSnap As Boolean = False
    Dim vanilla_isPre As Boolean = False
    Private Function GetVanillaServerURL(targetVersion As String) As String
        Dim preReleaseRegex1 As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]*-[Pp]{1}re[0-9]{1,2}")
        Dim preReleaseRegex2 As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]* [Pp]{1}re-[Rr]{1}elease [0-9]{1,2}")
        Dim snapshotRegex As New Regex("[0-9]{2}w[0-9]{2}[a-z]{1}")
        If Version.TryParse(targetVersion, Nothing) Then
        ElseIf preReleaseRegex1.IsMatch(targetVersion) Then
            vanilla_isPre = True
            If preReleaseRegex1.Match(targetVersion).Value.Contains("1.RV") Then
                targetVersion = preReleaseRegex1.Match(targetVersion).Value
            Else
                targetVersion = preReleaseRegex1.Match(targetVersion).Value
            End If
        ElseIf preReleaseRegex2.IsMatch(targetVersion) Then
            vanilla_isPre = True
            If preReleaseRegex2.Match(targetVersion).Value.Contains("1.RV") Then
                targetVersion = preReleaseRegex2.Match(targetVersion).Value
            Else
                targetVersion = preReleaseRegex2.Match(targetVersion).Value
            End If
        ElseIf snapshotRegex.IsMatch(targetVersion) Then
            vanilla_isSnap = True
            targetVersion = snapshotRegex.Match(targetVersion).Value
        Else
            If targetVersion <> "" Then
                Throw New IllegalServerVersionException
            End If
        End If
        Dim manifestListURL As String = ""
        manifestListURL = VanillaVersionDict(targetVersion)
        If manifestListURL <> "" Then
            Dim client As New Net.WebClient()
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(client.DownloadString(manifestListURL))
            If vanilla_isPre OrElse vanilla_isSnap Then
                Dim assets As String = jsonObject.GetValue("assets").ToString
                assets = New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(assets).Value
                ServerVersion = assets 'Pre/Snapshot Version Only
            Else
                ServerVersion = targetVersion ' Release Version Only
            End If
            Return jsonObject.GetValue("downloads").Item("server").Item("url").ToString
        Else
            Return ""
        End If
    End Function
    Public Overrides Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
        Dim URL = GetVanillaServerURL(targetServerVersion)
        Dim DownloadPath As String = ""
        If vanilla_isPre OrElse vanilla_isSnap Then
            DownloadPath = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "minecraft_server." & Server2ndVersion & ".jar")
        Else
            DownloadPath = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "minecraft_server." & ServerVersion & ".jar")
        End If
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
        task.Download(URL, DownloadPath)
        Return task
    End Function
    Public Overrides Sub UpdateServer()

    End Sub

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {}
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "vanilla-build-version"
                _Server2ndVersion = value
            Case "server-memory-max"
                ServerMemoryMax = IIf(IsNumeric(value), value, 0)
            Case "server-memory-min"
                ServerMemoryMin = IIf(IsNumeric(value), value, 0)
        End Select
    End Sub
End Class
