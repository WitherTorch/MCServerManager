Imports System.Text.RegularExpressions
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class NukkitServer
    Inherits ServerBase
    Implements Memoryable
    Implements IBukkit
    Public Property ServerMemoryMax As Integer Implements Memoryable.ServerMemoryMax
    Public Property ServerMemoryMin As Integer Implements Memoryable.ServerMemoryMin
    Public Property nukkitOptions As NukkitOptions
    Protected pluginList As New List(Of ServerAddons)
    Protected seperator As String = IIf(IsUnixLikeSystem, "/", "\")
    Protected Shared NukkitVersion As Integer
    Protected Shared NukkitVersionURL As String
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(path As String)
        MyBase.New(path)
        GetOptions()
    End Sub
    Protected Overridable Sub LoadPlugins() Implements IBukkit.LoadPlugins
        pluginList.Clear()
        Dim pluginPath = IO.Path.Combine(ServerPath, "plugins")
        Dim paths As New List(Of String)
        If IO.Directory.Exists(pluginPath) Then
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(pluginPath, "pluginList.json")) Then
                Dim reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(pluginPath, "pluginList.json"), IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read, 4096, True))
                Dim jsonArray As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(reader.ReadToEnd())
                If jsonArray IsNot Nothing Then
                    For Each jsonObject As JObject In jsonArray
                        Try
                            If IO.File.Exists(jsonObject.GetValue("Path").ToString) = False Then
                                If jsonObject.ContainsKey("LastAccessedDate") = False Or IO.File.GetLastWriteTime(jsonObject.GetValue("Path").ToString) <> jsonObject.GetValue("LastAccessedDate") Then
                                    Dim item As New ServerAddons(jsonObject.GetValue("Name").ToString, jsonObject.GetValue("Path").ToString, jsonObject.GetValue("Version"), jsonObject.GetValue("VersionDate"), IO.File.GetLastWriteTime(jsonObject.GetValue("Path").ToString))
                                    Using unpatcher As New BukkitPluginUnpatcher(item.Path)
                                        Dim info = unpatcher.GetPluginInfo()
                                        If info.IsNull = False Then
                                            item.Name = info.Name
                                            item.Version = info.Version
                                            pluginList.Add(item)
                                        End If
                                    End Using
                                    paths.Add(jsonObject.GetValue("Path").ToString)
                                Else
                                    Dim item As New ServerAddons(jsonObject.GetValue("Name").ToString, jsonObject.GetValue("Path").ToString, jsonObject.GetValue("Version"), jsonObject.GetValue("VersionDate"), jsonObject.GetValue("LastAccessedDate"))
                                    paths.Add(jsonObject.GetValue("Path").ToString)
                                    pluginList.Add(item)
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                End If
            End If
            Dim pluginPathInfo As New IO.DirectoryInfo(pluginPath)
            For Each pluginFileInfo In pluginPathInfo.GetFiles("*.jar", IO.SearchOption.TopDirectoryOnly)
                Try
                    Dim item As New ServerAddons(pluginFileInfo.Name, pluginFileInfo.FullName, "", pluginFileInfo.CreationTime, pluginFileInfo.LastWriteTime)
                    If paths.Contains(item.Path) = False Then
                        Using unpatcher As New BukkitPluginUnpatcher(item.Path)
                            Dim info = unpatcher.GetPluginInfo()
                            If info.IsNull = False Then
                                item.Name = info.Name
                                item.Version = info.Version
                                pluginList.Add(item)
                            End If
                        End Using
                    End If
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub
    Protected Overridable Sub SavePlugin() Implements IBukkit.SavePlugins
        Dim pluginPath = IO.Path.Combine(ServerPath, "plugins")
        If IO.Directory.Exists(pluginPath) = False Then
            IO.Directory.CreateDirectory(pluginPath)
        End If
        If My.Computer.FileSystem.FileExists(IO.Path.Combine(pluginPath, "pluginList.json")) = False Then
            IO.File.Create(IO.Path.Combine(pluginPath, "pluginList.json"))
        End If
        Try
            Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(pluginPath, "pluginList.json"), IO.FileMode.Truncate, IO.FileAccess.Write, IO.FileShare.Read, 4096, True))
            Dim jsonArray As New Newtonsoft.Json.Linq.JArray()
            For Each plugin In pluginList
                Dim jsonObject As New Newtonsoft.Json.Linq.JObject()
                jsonObject.Add("Name", New Newtonsoft.Json.Linq.JValue(plugin.Name))
                jsonObject.Add("Version", New Newtonsoft.Json.Linq.JValue(plugin.Version))
                jsonObject.Add("VersionDate", New Newtonsoft.Json.Linq.JValue(plugin.VersionDate))
                jsonObject.Add("Path", New Newtonsoft.Json.Linq.JValue(plugin.Path))
                jsonArray.Add(jsonObject)
            Next
            writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray))
            writer.Flush()
            writer.Close()
        Catch ex As IO.IOException
        End Try
    End Sub
    Public Overridable Function GetPlugins() As ServerAddons() Implements IBukkit.GetPlugins
        Return pluginList.ToArray()
    End Function
    Public Overridable Sub AddPlugin(plugin As ServerAddons) Implements IBukkit.AddPlugin
        If pluginList.Contains(plugin) = False Then pluginList.Add(plugin)
    End Sub
    Public Overridable Sub RemovePlugin(plugin As ServerAddons) Implements IBukkit.RemovePlugin
        If pluginList.Contains(plugin) Then pluginList.Remove(plugin)
    End Sub
    Public Overrides Sub ReloadServer()
        MyBase.ReloadServer()
        GetOptions()
    End Sub
    Friend Shared Sub GetVersionList()
        NukkitVersion = ""
        Dim manifestListURL As String = "https://ci.nukkitx.com/job/NukkitX/job/Nukkit/job/master/api/json"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            Dim docHtml = client.DownloadString(manifestListURL)
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
            Dim lastSuccessfulBuild = jsonObject.GetValue("lastSuccessfulBuild").ToObject(Of JObject)
            NukkitVersion = lastSuccessfulBuild.Item("number")
            NukkitVersionURL = lastSuccessfulBuild.Item("url")
            docHtml = Nothing
            client.Dispose()
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Private ServerOptions As NukkitServerOptions
    Public Overrides Function GetServerProperties() As IServerProperties
        Return ServerOptions
    End Function
    Protected Friend Overridable Sub GetOptions()
        If String.IsNullOrWhiteSpace(ServerPath) Then
            Dim serverOptions As New NukkitServerOptions()
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
        nukkitOptions = New NukkitOptions(IO.Path.Combine(ServerPath, "nukkit.yml"))
    End Sub
    Public Overrides Function CanUpdate() As Boolean
        Return ServerVersion < NukkitVersion
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "nukkit-" & ServerVersion & ".jar"
    End Function
    ''' <summary>
    ''' 啟動伺服器
    ''' </summary>
    Public Overrides Function RunServer() As Process
        If ProcessID = 0 Then
            Dim processInfo As New ProcessStartInfo(GetJavaPath(),
                                                String.Format("-Xms{0}M -Xmx{1}M {2} ""{3}""",
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
        Try
            GenerateServerInfo()
        Catch ex As Exception

        End Try
        Try
            nukkitOptions.SaveOption()
        Catch ex As Exception

        End Try
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
        Return "Nukkit"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return "#" & ServerVersion
    End Function
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {}
    End Function
    Public Overrides Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
        Dim URL = GetNukkitDownloadURL(NukkitVersionURL)
        Dim DownloadPath As String = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "nukkit-" & targetServerVersion & ".jar")
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
    Public Overrides Function UpdateServer() As ServerDownloadTask
        Return DownloadAndInstallServer(NukkitVersion.ToString)
    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {nukkitOptions}
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "nukkit-build-version"
                ServerVersion = value
        End Select
    End Sub

    Public Overrides Function GetAvailableVersions() As String()
        Return {NukkitVersion.ToString}
    End Function

    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function
End Class

