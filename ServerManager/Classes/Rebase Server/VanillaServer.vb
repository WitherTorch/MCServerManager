Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class VanillaServer
    Inherits ServerBase
    Implements Memoryable
    Public Property ServerMemoryMax As Integer Implements Memoryable.ServerMemoryMax
    Public Property ServerMemoryMin As Integer Implements Memoryable.ServerMemoryMin
    Public ReadOnly Property Server2ndVersion As String
    Public Sub New()
        MyBase.New()
        SetMainServerOptions()
    End Sub
    Public Sub New(path As String)
        MyBase.New(path)
        SetMainServerOptions()
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
    Protected Friend Overridable Sub SetMainServerOptions()
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
                                                               JavaArguments, GetServerFileName()))
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
    Public Overrides Function GetReadableName() As String
        Return "原版(Java)"
    End Function
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & ServerMemoryMax,
                                                  "server-memory-min=" & ServerMemoryMin,
                                                  "vanilla-build-version=" & Server2ndVersion}
    End Function
    Dim vanilla_isSnap As Boolean = False
    Dim vanilla_isPre As Boolean = False
    Private Function GetVanillaServerURL() As String
        Dim manifestListURL As String = ""
        If IsNothing(Server2ndVersion) = False OrElse Server2ndVersion <> "" Then
            Dim preReleaseRegex As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]*-[Pp]{1}re[0-9]{1,2}")
            Dim preReleaseRegex2 As New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*")
            Dim preReleaseRegex3 As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]* [Pp]{1}re-[Rr]{1}elease [0-9]{1,2}")
            If preReleaseRegex.IsMatch(Server2ndVersion) Then
                manifestListURL = VanillaVersionDict(Server2ndVersion)
                vanilla_isPre = True
            ElseIf preReleaseRegex3.IsMatch(Server2ndVersion) Then
                manifestListURL = VanillaVersionDict(Server2ndVersion)
                vanilla_isPre = True
            ElseIf ServerVersion = "snapshot" Then
                manifestListURL = VanillaVersionDict(Server2ndVersion)
                vanilla_isSnap = True
            ElseIf preReleaseRegex2.IsMatch(Server2ndVersion) Then
                manifestListURL = VanillaVersionDict(Server2ndVersion)
                vanilla_isPre = True
            Else
                manifestListURL = VanillaVersionDict(ServerVersion)
            End If
        Else
            manifestListURL = VanillaVersionDict(ServerVersion)
        End If
        If manifestListURL <> "" Then
            Dim client As New Net.WebClient()
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(client.DownloadString(manifestListURL))
            If vanilla_isPre OrElse vanilla_isSnap Then
                Dim assets As String = jsonObject.GetValue("assets").ToString
                assets = New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(assets).Value
                ServerVersion = assets
            End If
            Return jsonObject.GetValue("downloads").Item("server").Item("url").ToString
        End If
    End Function
    Public Overrides Sub DownloadServer()

    End Sub
    Public Overrides Sub UpdateServer()

    End Sub
End Class
