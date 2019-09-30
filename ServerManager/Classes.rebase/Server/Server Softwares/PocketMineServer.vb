Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class PocketMineServer
    Inherits ServerBase
    Implements IBukkit
    Protected seperator As String = IIf(IsUnixLikeSystem, "/", "\")
    Private Shared PocketMineVersionDict As New Dictionary(Of String, String)
    Protected pluginList As New List(Of ServerAddons)
    Private pocketMineOptions As PocketMineOptions

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(path As String)
        MyBase.New()
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
                                    Using unpatcher As New PocketMinePluginUnpatcher(item.Path)
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
            For Each pluginFileInfo In pluginPathInfo.GetFiles("*.phar", IO.SearchOption.TopDirectoryOnly)
                Try
                    Dim item As New ServerAddons(pluginFileInfo.Name, pluginFileInfo.FullName, "", pluginFileInfo.CreationTime, pluginFileInfo.LastWriteTime)
                    If paths.Contains(item.Path) = False Then
                        Using unpatcher As New PocketMinePluginUnpatcher(item.Path)
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
        PocketMineVersionDict.Clear()
        Dim manifestListURL As String = "https://api.github.com/repos/pmmp/PocketMine-MP/releases"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            client.Headers.Add(Net.HttpRequestHeader.UserAgent, "Minecraft-ServerBase-Manager")
            Dim docHtml = client.DownloadString(manifestListURL)
            Dim jsonArray As JArray = JsonConvert.DeserializeObject(Of JArray)(docHtml)
            For Each jsonObject As JObject In jsonArray
                Try
                    Dim tag_name As String = jsonObject.GetValue("tag_name")
                    For Each subJsonObject As JObject In CType(jsonObject.GetValue("assets"), JArray)
                        If subJsonObject.GetValue("name") <> "PocketMine-MP.phar" Then
                            Continue For
                        End If
                        Dim url As String = subJsonObject.GetValue("browser_download_url")
                        PocketMineVersionDict.Add(tag_name, url)
                        Exit For
                    Next
                Catch ex As Exception

                End Try
            Next
            docHtml = Nothing
            client.Dispose()
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Private ServerOptions As PocketMineServerOptions
    Public Overrides Function GetServerProperties() As IServerProperties
        Return ServerOptions
    End Function
    Protected Friend Overridable Sub GetOptions()
        If String.IsNullOrWhiteSpace(ServerPath) Then
            Dim serverOptions As New PocketMineServerOptions()
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
        pocketMineOptions = New PocketMineOptions(IO.Path.Combine(ServerPath, "pocketmine.yml"))
    End Sub
    Public Overrides Function CanUpdate() As Boolean
        Return Version.Parse(ServerVersion) < Version.Parse(PocketMineVersionDict.First.Key)
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "PocketMine-MP.phar"
    End Function
    Public Overrides Function BeforeRunServer() As Boolean
        If String.IsNullOrWhiteSpace(PHPPath) Then
            MsgBox("未指定PHP 路徑",, Application.ProductName)
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' 啟動伺服器
    ''' </summary>
    Public Overrides Function RunServer() As Process
        If ProcessID = 0 Then
            Dim processInfo As New ProcessStartInfo(PHPPath,
                                                String.Format("""{0}""",
                                                             ServerPath.TrimEnd(seperator) & seperator & GetServerFileName()))
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
            pocketMineOptions.SaveOption()
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
        Return "PocketMine"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion
    End Function
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {}
    End Function
    Public Overrides Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
        Dim URL = PocketMineVersionDict(targetServerVersion)
        Dim DownloadPath As String = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "PocketMine-MP.phar")
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
        Return DownloadAndInstallServer(PocketMineVersionDict.First.Key)
    End Function

    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {pocketMineOptions}
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)

    End Sub

    Public Overrides Function GetAvailableVersions() As String()
        Return {PocketMineVersionDict.Keys.ToString}
    End Function

    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function

End Class
