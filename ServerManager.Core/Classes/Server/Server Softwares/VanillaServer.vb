Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

''' <summary>
''' 原版伺服器
''' </summary>
Public Class VanillaServer
    Inherits ServerBase
    Implements IMemoryChange
    Protected seperator As String = IIf(IsUnixLikeSystem, "/", "\")
    Public Property MemoryMax As Integer Implements IMemoryChange.MemoryMax
    Public Property MemoryMin As Integer Implements IMemoryChange.MemoryMin
    Friend Property Server2ndVersion As String
    Dim runningProcess As Process
    Protected Shared VanillaVersionDict As New Dictionary(Of String, String)
    Private Shared SnapshotList As New List(Of String)
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(path As String)
        MyBase.New()
        GetOptions()
    End Sub
    Public Overrides Sub ReloadServer()
        MyBase.ReloadServer()
        GetOptions()
    End Sub
    Public Overrides Function BeforeRunServer() As Boolean
        If JavaPath = "" Then
            GUIHost.GUIHandler.MsgBox("未安裝Java 或 正在偵測", APP_NAME)
            Return False
        End If
        Return True
    End Function
    Friend Shared Sub GetVersionList()
        VanillaVersionDict.Clear()
        Dim manifestListURL As String = "https://launchermeta.mojang.com/mc/game/version_manifest.json"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            Dim docHtml = client.DownloadString(manifestListURL)
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
            For Each jsonValue In jsonObject.GetValue("versions").ToObject(Of JArray)
                If jsonValue.Item("type").ToString() = "release" Then
                    VanillaVersionDict.Add(jsonValue.Item("id").ToString(), jsonValue.Item("url").ToString())
                    If (jsonValue.Item("id").ToString() = "1.2.2") Then
                        Exit For
                    End If
                ElseIf jsonValue.Item("type").ToString() = "snapshot" Then
                    VanillaVersionDict.Add(jsonValue.Item("id").ToString(), jsonValue.Item("url").ToString())
                    SnapshotList.Add(jsonValue.Item("id").ToString())
                End If
            Next
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Private ServerOptions As New JavaServerOptions
    Public Overrides Function GetServerProperties() As IServerProperties
        Return ServerOptions
    End Function
    Protected Overrides Sub GetServer()
        MyBase.GetServer()
        GetOptions()
    End Sub
    Protected Friend Overridable Sub GetOptions()
        If String.IsNullOrWhiteSpace(ServerPath) Then
            Dim serverOptions As New JavaServerOptions()
            Me.ServerOptions = serverOptions.OutputOption
        Else
            Try
                If IO.File.Exists(IO.Path.Combine(ServerPath, "server.properties")) Then
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
        If String.IsNullOrWhiteSpace(Server2ndVersion) = False Then
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
                                                String.Format("-Dfile.encoding=UTF-8 -Djline.terminal=jline.UnsupportedTerminal -Xms{0}M -Xmx{1}M {2} -jar ""{3}"" nogui",
                                                              IIf(MemoryMin > 0, MemoryMin, GlobalModule.ServerMemoryMin),
                                                              IIf(MemoryMax > 0, MemoryMin, GlobalModule.ServerMemoryMax),
                                                               JavaArguments, ServerPath.TrimEnd(seperator) & seperator & GetServerFileName()))
            processInfo.UseShellExecute = False
            processInfo.CreateNoWindow = True
            processInfo.StandardErrorEncoding = System.Text.Encoding.UTF8
            processInfo.StandardOutputEncoding = System.Text.Encoding.UTF8
            processInfo.RedirectStandardOutput = True
            processInfo.RedirectStandardError = True
            processInfo.RedirectStandardInput = True
            processInfo.WorkingDirectory = ServerPath
            runningProcess = Process.Start(processInfo) '回傳一個處理序
            ProcessID = runningProcess.Id
            IsRunning = True
        End If
        Return runningProcess
    End Function
    Public Overrides Sub SaveServer()
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(ServerPath, "server.properties"), IO.FileMode.Truncate, IO.FileAccess.Write), New System.Text.UTF8Encoding(False))
        writer.WriteLine("# Minecraft server properties")
        writer.WriteLine("#" & Date.Now.ToString("ddd MMM dd HH:mm:ss K yyyy", System.Globalization.CultureInfo.CurrentUICulture))
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
            runningProcess = Nothing
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
        Return New String() {"server-memory-max=" & MemoryMax,
                                                  "server-memory-min=" & MemoryMin,
                                                  "vanilla-build-version=" & Server2ndVersion}
    End Function
    Protected vanilla_isSnap As Boolean = False
    Private Shared ReadOnly preReleaseRegex1 As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]*-[Pp]{1}re[0-9]{1,2}")
    Private Shared ReadOnly preReleaseRegex2 As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]* [Pp]{1}re-[Rr]{1}elease [0-9]{1,2}")
    Private Shared ReadOnly snapshotRegex As New Regex("[0-9]{2}w[0-9]{2}[a-z]{1}")
    Private Function GetVanillaServerURL(targetVersion As String) As String
        If SnapshotList.Contains(targetVersion) Then vanilla_isSnap = True
        Dim manifestListURL As String = ""
        manifestListURL = VanillaVersionDict(targetVersion)
        If manifestListURL <> "" Then
            Dim client As New Net.WebClient()
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(client.DownloadString(manifestListURL))
            If vanilla_isSnap Then
                Dim assets As String = jsonObject.GetValue("assets").ToString
                assets = New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(assets).Value
                Server2ndVersion = targetVersion
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
        If vanilla_isSnap Then
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
                                               GenerateServerEULA()
                                               Call OnServerDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        task.Download(URL, DownloadPath)
        Return task
    End Function
    Public Overrides Function UpdateServer() As ServerDownloadTask

    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {}
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "vanilla-build-version"
                _Server2ndVersion = value
            Case "server-memory-max"
                MemoryMax = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
            Case "server-memory-min"
                MemoryMin = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
        End Select
    End Sub

    Public Overrides Function GetAvailableVersions() As String()
        Return VanillaVersionDict.Keys.ToArray
    End Function

    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Dim haveSnapshot As Boolean = True
        For Each arg In args
            Try
                If arg.Item1 = "snapshots" Then
                    haveSnapshot = Boolean.Parse(arg.Item1)
                    Exit For
                End If
            Catch ex As Exception
                Throw New GetAvailableVersionsException
            End Try
        Next
        If haveSnapshot Then
            Return VanillaVersionDict.Keys.ToArray
        Else
            Dim result As New List(Of String)
            For Each item In VanillaVersionDict.Keys
                If SnapshotList.Contains(item) = False Then
                    result.Add(item)
                End If
            Next
            Return result.ToArray
        End If
    End Function
End Class
