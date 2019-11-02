Imports Newtonsoft.Json.Linq
Imports ServerManager

Public Class CraftBukkitServer
    Inherits VanillaServer
    Implements IBukkit
    Protected bukkitOptions As BukkitOptions
    Protected pluginList As New List(Of ServerAddons)
    Private Shared CraftBukkitVersionDict As New Dictionary(Of String, String)
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(path As String)
        MyBase.New(path)
    End Sub
    Protected Overridable Sub LoadPlugins() Implements IBukkit.LoadPlugins
        pluginList.Clear()
        Dim pluginPath = IO.Path.Combine(ServerPath, "plugins")
        Dim paths As New List(Of String)
        If IO.Directory.Exists(pluginPath) Then
            If IO.File.Exists(IO.Path.Combine(pluginPath, "pluginList.json")) Then
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
        If IO.File.Exists(IO.Path.Combine(pluginPath, "pluginList.json")) = False Then
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
    Friend Shared Shadows Sub GetVersionList()
        CraftBukkitVersionDict.Clear()
        Dim listURL As String = "https://getbukkit.org/download/craftbukkit"
        Try
            Dim client As New Net.WebClient()
            client.Encoding = System.Text.Encoding.UTF8
            Dim versionList = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectNodes("//div[4]/div[1]/div[1]/div[1]/*")
            For Each version In versionList
                CraftBukkitVersionDict.Add(version.SelectNodes("div[1]/div[1]/h2[1]")(0).InnerText, version.SelectSingleNode("div[1]/div[4]/div[2]/a[1]").GetAttributeValue("href", ""))
            Next
        Catch ex As Exception
            Throw New GetAvailableVersionsException
        End Try
    End Sub
    Protected Friend Overrides Sub GetOptions()
        MyBase.GetOptions()
        bukkitOptions = New BukkitOptions(IO.Path.Combine(ServerPath, "bukkit.yml"))
    End Sub
    Public Overrides Function GetOptionObjects() As AbstractSoftwareOptions()
        Return {bukkitOptions}
    End Function
    Public Overrides Function GetInternalName() As String
        Return "CraftBukkit"
    End Function
    Public Overrides Function GetAdditionalServerInfo() As String()
        Return New String() {"server-memory-max=" & ServerMemoryMax,
                                                  "server-memory-min=" & ServerMemoryMin}
    End Function
    Public Overrides Function CanUpdate() As Boolean
        Return False
    End Function
    Public Overrides Function GetServerFileName() As String
        Return "craftbukkit-" & ServerVersion & ".jar"
    End Function
    Public Overrides Function GetSoftwareVersionString() As String
        Return ServerVersion
    End Function
    Protected Overrides Sub OnReadServerInfo(key As String, value As String)
        Select Case key
            Case "server-memory-max"
                ServerMemoryMax = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
            Case "server-memory-min"
                ServerMemoryMin = IIf(Of Integer)(Integer.TryParse(value, Nothing), value, 0)
        End Select
    End Sub
    Public Overrides Function RunServer() As Process
        If ProcessID = 0 Then
            Dim processInfo As New ProcessStartInfo(GetJavaPath(),
                                                String.Format("-Xms{0}M -Xmx{1}M {2} -jar ""{3}""",
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
        MyBase.SaveServer()
        Dim optionObjects As AbstractSoftwareOptions() = GetOptionObjects()
        If optionObjects IsNot Nothing Then
            For Each options In optionObjects
                Try
                    If options IsNot Nothing Then options.SaveOption()
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub
    Public Overrides Function DownloadAndInstallServer(targetVersion As String) As ServerDownloadTask
        Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
        Dim targetURL As String = CraftBukkitVersionDict(targetVersion)
        Dim url = (New HtmlAgilityPack.HtmlWeb).Load(targetURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[1]/div[1]/div[1]/div[1]/h2[1]/a[1]").GetAttributeValue("href", "")
        Dim DownloadPath As String = IO.Path.Combine(IIf(ServerPath.EndsWith(seperator), ServerPath, ServerPath & seperator), "craftbukkit-" & targetVersion & ".jar")
        Dim task As New ServerDownloadTask
        AddHandler task.DownloadProgressChanged, Sub(percent As Integer)
                                                     Call OnServerDownloading(percent)
                                                 End Sub
        AddHandler task.DownloadCanceled, Sub()
                                              Call OnServerDownloadEnd(True)
                                          End Sub
        AddHandler task.DownloadCompleted, Sub()
                                               ServerVersion = targetVersion
                                               GenerateServerEULA()
                                               Call OnServerDownloadEnd(False)
                                           End Sub
        AddHandler task.DownloadStarted, Sub()
                                             Call OnServerDownloadStart()
                                         End Sub
        task.Download(url, DownloadPath)
        Return task

    End Function
    Public Overrides Function GetAvailableVersions() As String()
        Return CraftBukkitVersionDict.Keys.ToArray
    End Function

    Public Overrides Function GetAvailableVersions(ParamArray args() As (String, String)) As String()
        Return GetAvailableVersions()
    End Function
End Class
