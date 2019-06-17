Imports System.ComponentModel
Imports System.Security.Cryptography
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Xml
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports NoIP

Public Class Manager
    Friend ServerEntityList As New List(Of Server)
    Dim VanillaGetVersionThread As Thread
    Dim ForgeGetVersionThread As Thread
    Dim SpigotGetVersionThread As Thread
    Dim SpigotGitGetVersionThread As Thread
    Dim VanillaBedrockGetVersionThread As Thread
    Dim CraftBukkitGetVersionThread As Thread
    Dim NukkitGetVersionThread As Thread
    Dim SpongeVanillaGetVersionThread As Thread
    Dim PaperGetVersionThread As Thread
    Dim AkarinGetVersionThread As Thread
    Dim KettleGetVersionThread As Thread
    Dim FeedTheBeastGetPackThread As Thread
    Dim ATGetPackThread As Thread
    Friend ServerPathList As New List(Of String)
    Friend BungeePathList As New List(Of String)
    Friend ModpackServerPathList As New List(Of String)
    Friend upnpProvider As New UPnPMapper
    Friend NoIPProvider As NoIPProvider
    Dim sharer As String = ""
    Friend CanUPnP As Boolean
    Dim CanEnabledUPnP As Boolean
    Friend ip As New List(Of String)
    Friend HasJava As Boolean = False
    Friend Is32BitJava As Boolean = True
    Public Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。

    End Sub

    Private Sub GetVanillaServerVersionList()
        VanillaGetVersionThread = New Thread(Sub()
                                                 VanillaVersionDict.Clear()
                                                 Dim manifestListURL As String = "https://launchermeta.mojang.com/mc/game/version_manifest.json"
                                                 Try

                                                     Dim client As New Net.WebClient()
                                                     BeginInvoke(New Action(Sub() VanillaLoadingLabel.Text = "原版(Java)：" & "下載列表中..."))
                                                     Dim docHtml = client.DownloadString(manifestListURL)
                                                     BeginInvoke(New Action(Sub() VanillaLoadingLabel.Text = "原版(Java)：" & "載入列表中..."))
                                                     Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
                                                     For Each jsonValue In jsonObject.GetValue("versions").ToObject(Of JArray)
                                                         If jsonValue.Item("type").ToString() = "release" Then
                                                             VanillaVersionDict.Add(jsonValue.Item("id").ToString(), jsonValue.Item("url").ToString())
                                                             If (jsonValue.Item("id").ToString() = "1.2.2") Then
                                                                 Exit For
                                                             End If
                                                         ElseIf jsonValue.Item("type").ToString() = "snapshot" Then
                                                             VanillaVersionDict.Add(jsonValue.Item("id").ToString(), jsonValue.Item("url").ToString())
                                                         End If
                                                     Next
                                                     BeginInvoke(New Action(Sub() VanillaLoadingLabel.Text = "原版(Java)：" & "載入完成"))
                                                 Catch ex As Exception
                                                     BeginInvoke(New Action(Sub() VanillaLoadingLabel.Text = "原版(Java)：" & "(無)"))
                                                 End Try
                                             End Sub)
        VanillaGetVersionThread.Name = "Vanilla GetVersion Thread"
        VanillaGetVersionThread.IsBackground = True
        VanillaGetVersionThread.Start()
    End Sub
    Private Sub GetCraftBukkitServerVersionList()
        CraftBukkitGetVersionThread = New Thread(Sub()
                                                     CraftBukkitVersionDict.Clear()
                                                     Dim listURL As String = "https://getbukkit.org/download/craftbukkit"
                                                     Try
                                                         Dim client As New Net.WebClient()
                                                         BeginInvoke(New Action(Sub() CraftBukkitLoadingLabel.Text = "CraftBukkit：" & "下載列表中..."))
                                                         Dim versionList = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectNodes("//div[4]/div[1]/div[1]/div[1]/*")
                                                         BeginInvoke(New Action(Sub() CraftBukkitLoadingLabel.Text = "CraftBukkit：" & "載入列表中..."))
                                                         For Each version In versionList
                                                             CraftBukkitVersionDict.Add(version.SelectNodes("div[1]/div[1]/h2[1]")(0).InnerText, version.SelectSingleNode("div[1]/div[4]/div[2]/a[1]").GetAttributeValue("href", ""))
                                                         Next
                                                         BeginInvoke(New Action(Sub() CraftBukkitLoadingLabel.Text = "CraftBukkit：" & "載入完成"))
                                                     Catch ex As Exception
                                                         BeginInvoke(New Action(Sub() CraftBukkitLoadingLabel.Text = "CraftBukkit：" & "(無)"))
                                                     End Try
                                                 End Sub)
        CraftBukkitGetVersionThread.Name = "CraftBukkit GetVersion Thread"
        CraftBukkitGetVersionThread.IsBackground = True
        CraftBukkitGetVersionThread.Start()
    End Sub
    Private Sub GetSpigotServerVersionList()
        SpigotGetVersionThread = New Thread(Sub()

                                                SpigotVersionDict.Clear()
                                                Dim listURL As String = "https://getbukkit.org/download/spigot"
                                                Try
                                                    Dim client As New Net.WebClient()
                                                    BeginInvoke(New Action(Sub() SpigotLoadingLabel.Text = "Spigot：" & "下載列表中..."))
                                                    Dim versionList = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectNodes("//div[4]/div[1]/div[1]/div[1]/*")
                                                    BeginInvoke(New Action(Sub() SpigotLoadingLabel.Text = "Spigot：" & "載入列表中..."))
                                                    For Each version In versionList
                                                        SpigotVersionDict.Add(version.SelectNodes("div[1]/div[1]/h2[1]")(0).InnerText, version.SelectSingleNode("div[1]/div[4]/div[2]/a[1]").GetAttributeValue("href", ""))
                                                    Next
                                                    BeginInvoke(New Action(Sub() SpigotLoadingLabel.Text = "Spigot：" & "載入完成"))
                                                Catch ex As Exception
                                                    BeginInvoke(New Action(Sub() SpigotLoadingLabel.Text = "Spigot：" & "(無)"))
                                                End Try
                                            End Sub)
        SpigotGetVersionThread.Name = "Spigot GetVersion Thread"
        SpigotGetVersionThread.IsBackground = True
        SpigotGetVersionThread.Start()
    End Sub
    Private Sub GetSpigotGitServerVersionList()
        SpigotGitGetVersionThread = New Thread(Sub()

                                                   SpigotGitVersionList.Clear()
                                                   Dim listURL As String = "https://hub.spigotmc.org/versions/"
                                                   Try
                                                       Dim client As New Net.WebClient()
                                                       BeginInvoke(New Action(Sub() SpigotGitLoadingLabel.Text = "Spigot (Git 手動組建)：" & "下載列表中..."))
                                                       Dim versionList = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectNodes("/html[1]/body[1]/pre[1]/a")
                                                       BeginInvoke(New Action(Sub() SpigotGitLoadingLabel.Text = "Spigot (Git 手動組建)：" & "載入列表中..."))
                                                       For Each version In versionList
                                                           Dim versionText As String = version.InnerText
                                                           If versionText.EndsWith(".json") Then
                                                               versionText = versionText.Substring(0, versionText.Length - 5)
                                                               Dim versionRegex As New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*")
                                                               If versionRegex.IsMatch(versionText) Then
                                                                   If versionRegex.Match(versionText).Value = versionText Then
                                                                       If versionText.Contains(".") Then
                                                                           SpigotGitVersionList.Add(versionText)
                                                                       End If
                                                                   End If
                                                               End If
                                                           End If
                                                       Next
                                                       BeginInvoke(New Action(Sub() SpigotGitLoadingLabel.Text = "Spigot (Git 手動組建)：" & "載入完成"))
                                                   Catch ex As Exception
                                                       BeginInvoke(New Action(Sub() SpigotGitLoadingLabel.Text = "Spigot (Git 手動組建)：" & "(無)"))
                                                   End Try
                                                   SpigotGitVersionList = SpigotGitVersionList.OrderBy(Function(v As String) As Version
                                                                                                           Return New Version(v)
                                                                                                       End Function).ToList
                                                   SpigotGitVersionList.Reverse()
                                               End Sub)
        SpigotGitGetVersionThread.Name = "Spigot for Git GetVersion Thread"
        SpigotGitGetVersionThread.IsBackground = True
        SpigotGitGetVersionThread.Start()
    End Sub
    Private Sub GetForgeServerVersionList()
        ForgeGetVersionThread = New Thread(Sub()
                                               ForgeVersionDict.Clear()
                                               Dim manifestListURL As String = "https://files.minecraftforge.net/maven/net/minecraftforge/forge/maven-metadata.xml"
                                               Try
                                                   Dim client As New Net.WebClient()
                                                   BeginInvoke(New Action(Sub() ForgeLoadingLabel.Text = "Forge：" & "下載列表中..."))
                                                   Dim docHtml = client.DownloadString(manifestListURL)
                                                   BeginInvoke(New Action(Sub() ForgeLoadingLabel.Text = "Forge：" & "載入列表中..."))
                                                   Dim xmlDocument = New XmlDocument()
                                                   xmlDocument.Load(New IO.StringReader(docHtml))
                                                   Dim tempDict As New Dictionary(Of Version, List(Of Version))
                                                   Dim versionNodes = xmlDocument.SelectNodes("//metadata/versioning/versions/*")
                                                   For Each versionNode As XmlNode In versionNodes
                                                       Dim version As String = versionNode.InnerText
                                                       Try
                                                           Dim mcVersion As Version = New Version(version.Split(New Char() {"-"})(0))
                                                           Dim forgeVersion As Version = New Version(version.Split(New Char() {"-"})(1))
                                                           Dim versionList As List(Of Version)
                                                           If tempDict.ContainsKey(mcVersion) Then
                                                               versionList = tempDict(mcVersion)
                                                           Else
                                                               versionList = New List(Of Version)()
                                                           End If
                                                           versionList.Add(forgeVersion)
                                                           If tempDict.ContainsKey(mcVersion) Then
                                                               tempDict(mcVersion) = versionList
                                                           Else
                                                               tempDict.Add(mcVersion, versionList)
                                                           End If
                                                       Catch ex As Exception
                                                       End Try
                                                   Next
                                                   For Each versionListPair In tempDict
                                                       ForgeVersionDict.Add(versionListPair.Key, versionListPair.Value.Last)
                                                   Next
                                                   tempDict.Clear()
                                                   tempDict = Nothing
                                                   docHtml = Nothing
                                                   client.Dispose()
                                                   BeginInvoke(New Action(Sub() ForgeLoadingLabel.Text = "Forge：" & "載入完成"))
                                               Catch ex As Exception

                                                   BeginInvoke(New Action(Sub() ForgeLoadingLabel.Text = "Forge：" & "(無)"))
                                               End Try
                                           End Sub)
        ForgeGetVersionThread.Name = "Forge GetVersion Thread"
        ForgeGetVersionThread.IsBackground = True
        ForgeGetVersionThread.Start()
    End Sub
    Private Sub GetNukkitServerVersionList()
        NukkitGetVersionThread = New Thread(Sub()
                                                NukkitVersion = ""
                                                Dim manifestListURL As String = "https://ci.nukkitx.com/job/NukkitX/job/Nukkit/job/master/api/json"
                                                Try
                                                    Dim client As New Net.WebClient()
                                                    BeginInvoke(New Action(Sub() NukkitLoadingLabel.Text = "Nukkit：" & "下載列表中..."))
                                                    Dim docHtml = client.DownloadString(manifestListURL)
                                                    BeginInvoke(New Action(Sub() NukkitLoadingLabel.Text = "Nukkit：" & "載入列表中..."))
                                                    Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
                                                    Dim lastSuccessfulBuild = jsonObject.GetValue("lastSuccessfulBuild").ToObject(Of JObject)
                                                    NukkitVersion = lastSuccessfulBuild.Item("number")
                                                    NukkitVersionUrl = lastSuccessfulBuild.Item("url")
                                                    docHtml = Nothing
                                                    client.Dispose()
                                                    BeginInvoke(New Action(Sub() NukkitLoadingLabel.Text = "Nukkit：" & "載入完成"))
                                                Catch ex As Exception
                                                    BeginInvoke(New Action(Sub() NukkitLoadingLabel.Text = "Nukkit：" & "(無)"))
                                                End Try
                                            End Sub)
        NukkitGetVersionThread.Name = "Nukkit GetVersion Thread"
        NukkitGetVersionThread.IsBackground = True
        NukkitGetVersionThread.Start()
    End Sub
    Private Sub GetSpongeVanillaServerVersionList()
        SpongeVanillaGetVersionThread = New Thread(Sub()
                                                       Dim manifestListURL As String = "https://repo.spongepowered.org/maven/org/spongepowered/spongevanilla/maven-metadata.xml"
                                                       Try
                                                           Dim client As New Net.WebClient()
                                                           BeginInvoke(New Action(Sub() SpongeVanillaLoadingLabel.Text = "SpongeVanilla：" & "下載列表中..."))
                                                           Dim docHtml = client.DownloadString(manifestListURL)
                                                           BeginInvoke(New Action(Sub() SpongeVanillaLoadingLabel.Text = "SpongeVanilla：" & "載入列表中..."))
                                                           Dim xmlDocument = New XmlDocument()
                                                           xmlDocument.Load(New IO.StringReader(docHtml))
                                                           Dim versionNodes = xmlDocument.SelectNodes("//metadata/versioning/versions/*")
                                                           Dim versionList As New Dictionary(Of String, List(Of SpongeVersion))
                                                           For Each versionNode As XmlNode In versionNodes
                                                               Dim version As String = versionNode.InnerText
                                                               Dim versionRegex1 As New Regex("[0-9.]{1,}-[0-9.]{1,}-(DEV|BETA)-[0-9]{1,4}") 'Dev/Beta Detect
                                                               Dim versionRegex2 As New Regex("[0-9.]{1,}-[0-9.]{1,}-RC[0-9]{1,4}") 'RC Detect
                                                               Dim versionRegex3 As New Regex("[0-9.]{1,}-[0-9.]{1,}") 'Release Detect
                                                               Dim versionRegexIgnore As New Regex("[0-9.]{1,}-[0-9.]{1,}(DEV|BETA)-[0-9]{1,4}") 'Old Version(Official Doesn't Supported)
                                                               If (versionRegex1.IsMatch(version) _
                                                                   OrElse versionRegex2.IsMatch(version) _
                                                                   OrElse versionRegex3.IsMatch(version)) _
                                                                   And versionRegexIgnore.IsMatch(version) = False Then
                                                                   If versionRegex1.IsMatch(version) Then
                                                                       Dim match = versionRegex1.Match(version).Value
                                                                       Dim versionData = match.Split("-")
                                                                       Dim ChildVersionList As List(Of SpongeVersion)
                                                                       If versionList.ContainsKey(versionData(0)) Then
                                                                           ChildVersionList = versionList(versionData(0))
                                                                       Else
                                                                           ChildVersionList = New List(Of SpongeVersion)
                                                                       End If
                                                                       Select Case versionData(2)
                                                                           Case "DEV"
                                                                               ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(1), SpongeVersionType.Dev, versionData(3)))
                                                                           Case "BETA"
                                                                               ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(1), SpongeVersionType.Beta, versionData(3)))
                                                                       End Select
                                                                       versionList(versionData(0)) = ChildVersionList
                                                                   ElseIf versionRegex2.IsMatch(version) Then
                                                                       Dim match = versionRegex2.Match(version).Value
                                                                       Dim versionData = match.Split("-")
                                                                       Dim ChildVersionList As List(Of SpongeVersion)
                                                                       If versionList.ContainsKey(versionData(0)) Then
                                                                           ChildVersionList = versionList(versionData(0))
                                                                       Else
                                                                           ChildVersionList = New List(Of SpongeVersion)
                                                                       End If
                                                                       ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(1), SpongeVersionType.RC, versionData(2).Substring(2)))
                                                                       versionList(versionData(0)) = ChildVersionList
                                                                   ElseIf versionRegex3.IsMatch(version) Then
                                                                       Dim match = versionRegex3.Match(version).Value
                                                                       Dim versionData = match.Split("-")
                                                                       Dim ChildVersionList As List(Of SpongeVersion)
                                                                       If versionList.ContainsKey(versionData(0)) Then
                                                                           ChildVersionList = versionList(versionData(0))
                                                                       Else
                                                                           ChildVersionList = New List(Of SpongeVersion)
                                                                       End If
                                                                       ChildVersionList.Add(New SpongeVersion(match, versionData(0), versionData(1), SpongeVersionType.Release))
                                                                       versionList(versionData(0)) = ChildVersionList
                                                                   End If
                                                               End If
                                                           Next
                                                           For Each keyValuePair In versionList
                                                               If SpongeVanillaVersionList.ContainsKey(keyValuePair.Key) = False Then SpongeVanillaVersionList.Add(keyValuePair.Key, keyValuePair.Value.Max)
                                                           Next
                                                           docHtml = Nothing
                                                           client.Dispose()
                                                           BeginInvoke(New Action(Sub() SpongeVanillaLoadingLabel.Text = "SpongeVanilla：" & "載入完成"))
                                                       Catch ex As Exception
                                                           BeginInvoke(New Action(Sub() SpongeVanillaLoadingLabel.Text = "SpongeVanilla：" & "(無)"))
                                                       End Try
                                                   End Sub)
        SpongeVanillaGetVersionThread.Name = "SpongeVanilla GetVersion Thread"
        SpongeVanillaGetVersionThread.IsBackground = True
        SpongeVanillaGetVersionThread.Start()
    End Sub
    Private Sub GetVanillaBedrockServerVersionList()
        VanillaBedrockGetVersionThread = New Thread(Sub()
                                                        VanillaBedrockVersion = Nothing
                                                        Dim listURL As String = "https://minecraft.net/zh-hant/download/server/bedrock/"
                                                        Try
                                                            BeginInvoke(New Action(Sub() VanillaBedrockLoadingLabel.Text = "原版(基岩)：" & "下載列表中..."))
                                                            Dim versionNode = (New HtmlAgilityPack.HtmlWeb).Load(listURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[3]/main[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[3]/div[1]/a[1]")
                                                            BeginInvoke(New Action(Sub() VanillaBedrockLoadingLabel.Text = "原版(基岩)：" & "載入列表中..."))
                                                            Dim downloadLink As String = versionNode.GetAttributeValue("href", "")
                                                            'bedrock-server-1.9.0.15.zip
                                                            Dim regex As New Regex("[0-9]{1}.[0-9]{1,2}.[0-9]{1,2}.[0-9]{1,2}")
                                                            If regex.IsMatch(downloadLink) Then
                                                                Dim versionString As String = regex.Match(downloadLink).Value
                                                                VanillaBedrockVersion = New Version(versionString)
                                                                BeginInvoke(New Action(Sub() VanillaBedrockLoadingLabel.Text = "原版(基岩)：" & "載入完成"))
                                                            Else
                                                                BeginInvoke(New Action(Sub() VanillaBedrockLoadingLabel.Text = "原版(基岩)：" & "(無)"))
                                                            End If
                                                        Catch ex As Exception
                                                            BeginInvoke(New Action(Sub() VanillaBedrockLoadingLabel.Text = "原版(基岩)：" & "(無)"))
                                                        End Try
                                                    End Sub)
        VanillaBedrockGetVersionThread.Name = "Bedrock Vanilla GetVersion Thread"
        VanillaBedrockGetVersionThread.IsBackground = True
        VanillaBedrockGetVersionThread.Start()
    End Sub

    Private Sub GetPaperServerVersionList()
        PaperGetVersionThread = New Thread(Sub()
                                               PaperVersionDict.Clear()
                                               Dim manifestListURL As String = "https://papermc.io/api/v1/paper"
                                               Try
                                                   Dim client As New Net.WebClient()
                                                   BeginInvoke(New Action(Sub() PaperLoadingLabel.Text = "Paper：" & "下載列表中..."))
                                                   Dim docHtml = client.DownloadString(manifestListURL)
                                                   BeginInvoke(New Action(Sub() PaperLoadingLabel.Text = "Paper：" & "載入列表中..."))
                                                   Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
                                                   Dim versions As JArray = jsonObject.GetValue("versions")
                                                   For Each version In versions
                                                       Dim mcVersion As Version = Nothing
                                                       If System.Version.TryParse(version, mcVersion) Then
                                                           PaperVersionDict.Add(mcVersion, "https://papermc.io/api/v1/paper/" & version.ToString)
                                                       End If
                                                   Next
                                                   docHtml = Nothing
                                                   client.Dispose()
                                                   BeginInvoke(New Action(Sub() PaperLoadingLabel.Text = "Paper：" & "載入完成"))
                                               Catch ex As Exception
                                                   BeginInvoke(New Action(Sub() PaperLoadingLabel.Text = "Paper：" & "(無)"))
                                               End Try
                                           End Sub)
        PaperGetVersionThread.Name = "Paper GetVersion Thread"
        PaperGetVersionThread.IsBackground = True
        PaperGetVersionThread.Start()
    End Sub
    Private Sub GetAkarinServerVersionList()
        AkarinGetVersionThread = New Thread(Sub()
                                                AkarinVersionList.Clear()
                                                Dim manifestListURL As String = "https://api.github.com/repos/Akarin-project/Akarin/branches"
                                                Try
                                                    Dim client As New Net.WebClient()
                                                    client.Headers.Add(Net.HttpRequestHeader.UserAgent, "Minecraft-Server-Manager")
                                                    BeginInvoke(New Action(Sub() AkarinLoadingLabel.Text = "Akarin：" & "下載列表中..."))
                                                    Dim docHtml = client.DownloadString(manifestListURL)
                                                    BeginInvoke(New Action(Sub() AkarinLoadingLabel.Text = "Akarin：" & "載入列表中..."))
                                                    Dim jsonArray As JArray = JsonConvert.DeserializeObject(Of JArray)(docHtml)
                                                    For Each jsonObject As JObject In jsonArray
                                                        Dim versionString As String = jsonObject.GetValue("name").ToString()
                                                        If versionString.StartsWith("ver/") AndAlso Version.TryParse(versionString.Substring(4), New Version()) Then
                                                            AkarinVersionList.Add(Version.Parse(versionString.Substring(4)))
                                                        ElseIf versionString = "master" Then
                                                            AkarinVersionList.Add(New Version(100, 100))
                                                        End If
                                                    Next
                                                    AkarinVersionList.Sort()
                                                    AkarinVersionList.Reverse()
                                                    docHtml = Nothing
                                                    client.Dispose()
                                                    BeginInvoke(New Action(Sub() AkarinLoadingLabel.Text = "Akarin：" & "載入完成"))
                                                Catch ex As Exception
                                                    BeginInvoke(New Action(Sub() AkarinLoadingLabel.Text = "Akarin：" & "(無)"))
                                                End Try
                                            End Sub)
        AkarinGetVersionThread.Name = "Akarin GetVersion Thread"
        AkarinGetVersionThread.IsBackground = True
        AkarinGetVersionThread.Start()
    End Sub
    Private Sub GetKettleServerVersionList()
        KettleGetVersionThread = New Thread(Sub()
                                                KettleVersionDict.Clear()
                                                Dim manifestListURL As String = "https://api.github.com/repos/KettleFoundation/Kettle/releases"
                                                Try
                                                    Dim client As New Net.WebClient()
                                                    client.Headers.Add(Net.HttpRequestHeader.UserAgent, "Minecraft-Server-Manager")
                                                    BeginInvoke(New Action(Sub() KettleLoadingLabel.Text = "Kettle：" & "下載列表中..."))
                                                    Dim docHtml = client.DownloadString(manifestListURL)
                                                    BeginInvoke(New Action(Sub() KettleLoadingLabel.Text = "Kettle：" & "載入列表中..."))
                                                    Dim jsonArray As JArray = JsonConvert.DeserializeObject(Of JArray)(docHtml)
                                                    For Each jsonObject As JObject In jsonArray
                                                        Try
                                                            Dim name As String = ""
                                                            jsonObject.TryGetValue("name", name)
                                                            If name = "" Then Continue For
                                                            name = name.Replace("Kettle ", "")
                                                            Dim assets As JArray = Nothing
                                                            jsonObject.TryGetValue("assets", assets)
                                                            If assets IsNot Nothing AndAlso assets.Count > 1 Then
                                                                For Each subJsonObject As JObject In assets
                                                                    Dim regex As New Regex("kettle-git-HEAD-[0-9a-f]{7}-universal.jar", RegexOptions.IgnoreCase)
                                                                    If regex.IsMatch(subJsonObject.GetValue("name")) AndAlso
                                                                    regex.Match(subJsonObject.GetValue("name")).Value = subJsonObject.GetValue("name") Then
                                                                        Dim url As String = subJsonObject.GetValue("browser_download_url")
                                                                        If assets.Count = 2 Then
                                                                            KettleVersionDict.Add(name, (url, subJsonObject.GetValue("name"), Nothing))
                                                                        Else
                                                                            For i As Integer = 2 To assets.Count - 1
                                                                                If CType(assets(i), JObject).GetValue("name") = "libraries.zip" Then
                                                                                    KettleVersionDict.Add(name, (url, subJsonObject.GetValue("name"), CType(assets(i), JObject).GetValue("browser_download_url")))
                                                                                    Exit For
                                                                                End If
                                                                            Next
                                                                            If KettleVersionDict.ContainsKey(name) = False Then KettleVersionDict.Add(name, (url, subJsonObject.GetValue("name"), Nothing))
                                                                        End If
                                                                    End If
                                                                Next
                                                            End If
                                                        Catch ex As Exception

                                                        End Try
                                                    Next
                                                    docHtml = Nothing
                                                    client.Dispose()
                                                    BeginInvoke(New Action(Sub() KettleLoadingLabel.Text = "Kettle：" & "載入完成"))
                                                Catch ex As Exception
                                                    BeginInvoke(New Action(Sub() KettleLoadingLabel.Text = "Kettle：" & "(無)"))
                                                End Try
                                            End Sub)
        KettleGetVersionThread.Name = "Kettle GetVersion Thread"
        KettleGetVersionThread.IsBackground = True
        KettleGetVersionThread.Start()
    End Sub
    Private Sub VersionListReloadButton_Click(sender As Object, e As EventArgs) Handles VersionListReloadButton.Click
        UpdateVersionLists()
    End Sub
    Private Sub UpdateVersionLists()
        If My.Computer.Network.IsAvailable Then
            If IsNothing(VanillaGetVersionThread) = False AndAlso VanillaGetVersionThread.IsAlive = True Then
                Try
                    VanillaGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                VanillaGetVersionThread = Nothing
            End If
            If IsNothing(ForgeGetVersionThread) = False AndAlso ForgeGetVersionThread.IsAlive = True Then
                Try
                    ForgeGetVersionThread.Abort()
                Catch ex As Exception
                End Try
                ForgeGetVersionThread = Nothing
            End If
            If IsNothing(SpigotGetVersionThread) = False AndAlso SpigotGetVersionThread.IsAlive = True Then
                Try
                    SpigotGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                SpigotGetVersionThread = Nothing
            End If
            If IsNothing(SpigotGitGetVersionThread) = False AndAlso SpigotGitGetVersionThread.IsAlive = True Then
                Try
                    SpigotGitGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                SpigotGitGetVersionThread = Nothing
            End If
            If IsNothing(CraftBukkitGetVersionThread) = False AndAlso CraftBukkitGetVersionThread.IsAlive = True Then
                Try
                    CraftBukkitGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                CraftBukkitGetVersionThread = Nothing
            End If

            If IsNothing(SpongeVanillaGetVersionThread) = False AndAlso SpongeVanillaGetVersionThread.IsAlive = True Then
                Try
                    SpongeVanillaGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                SpongeVanillaGetVersionThread = Nothing
            End If
            If IsNothing(PaperGetVersionThread) = False AndAlso PaperGetVersionThread.IsAlive = True Then
                Try
                    PaperGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                PaperGetVersionThread = Nothing
            End If
            If IsNothing(AkarinGetVersionThread) = False AndAlso AkarinGetVersionThread.IsAlive = True Then
                Try
                    AkarinGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                AkarinGetVersionThread = Nothing
            End If
            If IsNothing(KettleGetVersionThread) = False AndAlso KettleGetVersionThread.IsAlive = True Then
                Try
                    KettleGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                KettleGetVersionThread = Nothing
            End If
            If IsNothing(NukkitGetVersionThread) = False AndAlso NukkitGetVersionThread.IsAlive = True Then
                Try
                    NukkitGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                NukkitGetVersionThread = Nothing
            End If
            If IsNothing(VanillaBedrockGetVersionThread) = False AndAlso VanillaBedrockGetVersionThread.IsAlive = True Then
                Try
                    VanillaBedrockGetVersionThread.Abort()
                Catch ex As Exception

                End Try
                VanillaBedrockGetVersionThread = Nothing
            End If
            GetVanillaServerVersionList()
            GetForgeServerVersionList()
            GetSpigotServerVersionList()
            GetSpigotGitServerVersionList()
            GetCraftBukkitServerVersionList()
            GetSpongeVanillaServerVersionList()
            GetPaperServerVersionList()
            GetAkarinServerVersionList()
            GetKettleServerVersionList()
            GetNukkitServerVersionList()
            GetVanillaBedrockServerVersionList()
        Else
            VanillaLoadingLabel.Text = "原版：" & "(無)"
            ForgeLoadingLabel.Text = "Forge：" & "(無)"
            SpigotLoadingLabel.Text = "Spigot：" & "(無)"
            SpigotGitLoadingLabel.Text = "Spigot (Git 手動組建)：" & "(無)"
            CraftBukkitLoadingLabel.Text = "CraftBukkit：" & "(無)"
            SpongeVanillaLoadingLabel.Text = "SpongeVanilla：" & "(無)"
            PaperLoadingLabel.Text = "Paper：" & "(無)"
            AkarinLoadingLabel.Text = "Akarin：" & "(無)"
            NukkitLoadingLabel.Text = "Nukkit：" & "(無)"
            VanillaBedrockLoadingLabel.Text = "原版(基岩)：" & "(無)"
            KettleLoadingLabel.Text = "Kettle：(無)"
        End If
        GC.Collect()
    End Sub
    Sub GetFeedTheBeastPackList()
        FeedTheBeastGetPackThread = New Thread(Sub()
                                                   FeedTheBeastPackDict.Clear()
                                                   Dim manifestListURL As String = "https://api.feed-the-beast.com/ss/api/JSON/pack"
                                                   Try
                                                       Dim client As New Net.WebClient()
                                                       BeginInvoke(New Action(Sub() FTBLoadingLabel.Text = "Feed The Beast：" & "下載列表中..."))
                                                       Dim docHtml = client.DownloadString(manifestListURL)
                                                       BeginInvoke(New Action(Sub() FTBLoadingLabel.Text = "Feed The Beast：" & "載入列表中..."))
                                                       Dim jsonMotherObject As JObject = JsonConvert.DeserializeObject(Of JObject)(docHtml)
                                                       For Each jsonProperty As JProperty In jsonMotherObject.Children
                                                           Dim jsonObject As JObject = jsonProperty.Value
                                                           Dim packInfo As (Dictionary(Of String, String), String, String) ' Version List , Directory Name , File Name
                                                           packInfo.Item1 = New Dictionary(Of String, String)()
                                                           packInfo.Item2 = jsonObject.GetValue("dir")
                                                           packInfo.Item3 = jsonObject.GetValue("serverPack")
                                                           If String.IsNullOrEmpty(packInfo.Item3) Then Continue For
                                                           For Each jsonChildObject As JObject In CType(jsonObject.GetValue("versions"), JArray)
                                                               packInfo.Item1.Add(jsonChildObject.GetValue("version"), jsonChildObject.GetValue("mcVersion"))
                                                           Next
                                                           FeedTheBeastPackDict.Add(jsonObject.GetValue("title"), packInfo)
                                                       Next
                                                       docHtml = Nothing
                                                       client.Dispose()
                                                       BeginInvoke(New Action(Sub() FTBLoadingLabel.Text = "Feed The Beast：" & "載入完成"))
                                                   Catch ex As Exception
                                                       BeginInvoke(New Action(Sub() FTBLoadingLabel.Text = "Feed The Beast：" & "(無)"))
                                                   End Try
                                               End Sub)
        FeedTheBeastGetPackThread.Name = "FeedTheBeast GetModpack Thread"
        FeedTheBeastGetPackThread.IsBackground = True
        FeedTheBeastGetPackThread.Start()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CanEnabledUPnP Then
            Select Case CheckBox1.Checked
                Case True
                    CanUPnP = True
                Case False
                    CanUPnP = False
            End Select
        End If
    End Sub
    Friend Function GetExternalIP() As String
        Try
            Dim ExternalIP As String
            ExternalIP = (New Net.WebClient()).DownloadString("https://checkip.dedyn.io")
            Dim IPv4Regex As New Regex("\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")
            Dim IPv6Regex As New Regex("(([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,7}:|([0-9a-fA-F]{1,4}:){1,6}:[0-9a-fA-F]{1,4}|([0-9a-fA-F]{1,4}:){1,5}(:[0-9a-fA-F]{1,4}){1,2}|([0-9a-fA-F]{1,4}:){1,4}(:[0-9a-fA-F]{1,4}){1,3}|([0-9a-fA-F]{1,4}:){1,3}(:[0-9a-fA-F]{1,4}){1,4}|([0-9a-fA-F]{1,4}:){1,2}(:[0-9a-fA-F]{1,4}){1,5}|[0-9a-fA-F]{1,4}:((:[0-9a-fA-F]{1,4}){1,6})|:((:[0-9a-fA-F]{1,4}){1,7}|:)|fe80:(:[0-9a-fA-F]{0,4}){0,4}%[0-9a-zA-Z]{1,}|::(ffff(:0{1,4}){0,1}:){0,1}((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]).){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9])|([0-9a-fA-F]{1,4}:){1,4}:((25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]).){3,3}(25[0-5]|(2[0-4]|1{0,1}[0-9]){0,1}[0-9]))")
            If IPv4Regex.IsMatch(ExternalIP) Then 'IPv4
                ExternalIP = IPv4Regex.Match(ExternalIP).Value
            ElseIf (IPv6Regex.IsMatch(ExternalIP)) Then 'IPv6
                ExternalIP = IPv6Regex.Match(ExternalIP).Value
            Else
                Return "(無)"
            End If
            Return ExternalIP
        Catch ex As Exception
            Return "(無)"
        End Try
    End Function
    Friend Sub CheckNetwork()
        If My.Computer.Network.IsAvailable = False Then
            Button1.Enabled = False
            Button3.Enabled = False
            Button6.Enabled = False
        End If
        'UpdateVersionLists() <--Moved to Shown Event
        Dim tooltip As New ToolTip
        Dim IPSeeker As New Thread(Sub()
                                       Try
                                           If IsNothing(Net.Dns.GetHostAddresses(Net.Dns.GetHostName)) = False Then
                                               BeginInvokeIfRequired(Me, Sub() IPLabel.Text = "內部IP位址：")
                                               Dim ipas = Net.Dns.GetHostAddresses(Net.Dns.GetHostName)
                                               BeginInvokeIfRequired(Me, Sub()
                                                                             Dim flag As Boolean = False
                                                                             For Each i In ipas
                                                                                 If i.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                                                                                     Dim b = i.GetAddressBytes
                                                                                     Dim inip = b(0) & "." & b(1) & "." & b(2) & "." & b(3)
                                                                                     If Array.IndexOf(ipas, i) = ipas.Count - 1 Then
                                                                                         IPLabel.Text &= inip
                                                                                     Else
                                                                                         IPLabel.Text &= inip & " , "
                                                                                     End If
                                                                                     flag = True
                                                                                     IPLabel.Links.Add(New LinkLabel.Link(IPLabel.Text.IndexOf(inip), inip.Length))
                                                                                     tooltip.SetToolTip(IPLabel, "點擊連結複製內部IP位址")
                                                                                     ip.Add(inip)
                                                                                 End If
                                                                             Next
                                                                             If flag Then
                                                                                 IPLabel.Text = IPLabel.Text.Trim.TrimEnd(" ,")
                                                                             End If
                                                                             If ip.Count = 0 Then
                                                                                 IPLabel.Text = "內部IP位址：(無)"
                                                                             End If
                                                                         End Sub)
                                           Else
                                                   BeginInvoke(Sub() IPLabel.Text = "內部IP位址：(無)")
                                           End If
                                       Catch ex As Exception
                                           BeginInvoke(Sub() IPLabel.Text = "內部IP位址：(錯誤)")
                                       End Try
                                   End Sub)
        IPSeeker.Name = "Internal IP Seeker"
        IPSeeker.IsBackground = True
        IPSeeker.Start()
        Select Case My.Computer.Network.IsAvailable
            Case True
                Dim ExternalIPSeeker As New Thread(Sub()
                                                       Invoke(Sub() IPALabel.Text = "網路狀態：" & "已連接")
                                                       Dim exip = GetExternalIP()
                                                       Invoke(Sub() ExternalIPLabel.Text = "外部IP位址：" & exip)
                                                       BeginInvoke(Sub()
                                                                       If exip <> "(無)" Then
                                                                           ExternalIPLabel.LinkArea = New LinkArea(ExternalIPLabel.Text.IndexOf(exip), exip.Length)
                                                                           tooltip.SetToolTip(ExternalIPLabel, "點擊連結複製外部IP位址")
                                                                           AddHandler ExternalIPLabel.LinkClicked, Sub(obj, args)
                                                                                                                       If args.Button = MouseButtons.Left Then
                                                                                                                           My.Computer.Clipboard.SetText(exip)
                                                                                                                           MsgBox("已複製到剪貼簿!")
                                                                                                                       End If
                                                                                                                   End Sub
                                                                       Else
                                                                           ConnectionTabPage.Visible = False
                                                                           TableLayoutPanel3.Enabled = False
                                                                       End If
                                                                   End Sub)
                                                   End Sub)
                ExternalIPSeeker.Name = "External IP Seeker"
                ExternalIPSeeker.IsBackground = True
                ExternalIPSeeker.Start()
                Dim upnpMatcher As New Thread(Sub()
                                                  If upnpProvider.Init() Then
                                                      BeginInvoke(Sub()
                                                                      CanEnabledUPnP = True
                                                                      UPnPLabel.Text = "UPnP 狀態：可用"
                                                                      CheckBox1.Enabled = True
                                                                  End Sub)
                                                  Else
                                                      BeginInvoke(Sub()
                                                                      CanEnabledUPnP = False
                                                                      UPnPLabel.Text = "UPnP 狀態：不可用"
                                                                      CheckBox1.Enabled = False
                                                                  End Sub)
                                                  End If

                                              End Sub)
                upnpMatcher.Name = " UPnP Matcher"
                upnpMatcher.IsBackground = True
                upnpMatcher.Start()
            Case False
                IPALabel.Text = "網路狀態：" & "未連接"
                IPLabel.Text = "內部IP位址：" & "(無)"
                CanEnabledUPnP = False
                UPnPLabel.Text = "UPnP 狀態：不可用"
                CheckBox1.Enabled = False
                ExternalIPLabel.Text = "外部IP位址：" & "(無)"
                ConnectionTabPage.Visible = False
                TableLayoutPanel3.Enabled = False
        End Select
    End Sub
    Private Sub Manager_Load(sender As Object, e As EventArgs) Handles Me.Load
        GlobalModule.Manager = Me
        CheckNetwork()

        If JavaServerDirs <> "" Then
            For Each s In CType(Newtonsoft.Json.JsonConvert.DeserializeObject(JavaServerDirs), Newtonsoft.Json.Linq.JArray)
                Task.Run(Sub()
                             AddServer(s.ToString)
                             ServerPathList.Add(s.ToString)
                         End Sub)
            Next
        End If
        '1.5.1 相容
        If ReadAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "peServers.txt")) <> "" Then
            For Each s In CType(Newtonsoft.Json.JsonConvert.DeserializeObject(ReadAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "peServers.txt"))), Newtonsoft.Json.Linq.JArray)
                Task.Run(Sub()
                             AddServer(s.ToString)
                             ServerPathList.Add(s.ToString)
                         End Sub)
            Next
        End If
        If SolutionDirs <> "" Then
            For Each s In CType(Newtonsoft.Json.JsonConvert.DeserializeObject(SolutionDirs), Newtonsoft.Json.Linq.JArray)
                Task.Run(Sub()
                             AddBungeeSolution(s.ToString)
                             BungeePathList.Add(s.ToString)
                         End Sub)
            Next
        End If
        If ModpackServerDirs <> "" Then
            For Each s In CType(Newtonsoft.Json.JsonConvert.DeserializeObject(ModpackServerDirs), Newtonsoft.Json.Linq.JArray)
                Task.Run(Sub()
                             AddModpackServer(s.ToString)
                             ModpackServerPathList.Add(s.ToString)
                         End Sub)
            Next
        End If

        If My.Computer.FileSystem.FileExists(IO.Path.Combine(My.Application.Info.DirectoryPath, "manager-setting.txt")) Then
            Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(My.Application.Info.DirectoryPath, "manager-setting.txt"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                Dim username = ""
                Dim password = ""
                Dim hosts As JArray = Nothing
                Do Until reader.EndOfStream
                    Dim infoText As String = reader.ReadLine
                    Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                    If info IsNot Nothing AndAlso info.Count >= 2 Then
                        Select Case info(0)
                            Case "memory-min"
                                If IsNumeric(info(1)) Then
                                    ServerMemoryMinBox.Value = info(1)
                                    ServerMemoryMin = info(1)
                                End If
                            Case "memory-max"
                                If IsNumeric(info(1)) Then
                                    ServerMemoryMaxBox.Value = info(1)
                                    ServerMemoryMax = info(1)
                                End If
                            Case "bungee-memory-min"
                                If IsNumeric(info(1)) Then
                                    BungeeMemoryMinBox.Value = info(1)
                                    BungeeCordMemoryMin = info(1)
                                End If
                            Case "bungee-memory-max"
                                If IsNumeric(info(1)) Then
                                    BungeeMemoryMaxBox.Value = info(1)
                                    BungeeCordMemoryMax = info(1)
                                End If
                            Case "java-arguments"
                                JavaArguments = info(1)
                                ArguBox.Text = info(1)
                            Case "java-path"
                                JavaPath = info(1)
                            Case "noip-username"
                                If info(1).Trim <> "" Then
                                    Try
                                        username = System.Text.Encoding.UTF8.GetChars(Convert.FromBase64String(info(1)))
                                    Catch ex As Exception

                                    End Try
                                End If
                            Case "noip-password"
                                If info(1).Trim <> "" Then
                                    Try
                                        password = System.Text.Encoding.UTF8.GetChars(Convert.FromBase64String(info(1)))
                                    Catch ex As Exception

                                    End Try
                                End If
                            Case "noip-hosts"
                                If info(1) <> "" Then
                                    Try
                                        hosts = JsonConvert.DeserializeObject(info(1))
                                    Catch ex As Exception
                                    End Try
                                End If
                            Case "git-bash-path"
                                If info(1) <> "" Then
                                    If IO.File.Exists(info(1)) Then
                                        GitBashPathBox.Text = info(1)
                                        GitBashPath = info(1)
                                    End If
                                End If
                            Case "show-vanilla-snaps"
                                If info(1).Trim <> "" Then
                                    Select Case info(1)
                                        Case "true"
                                            ShowVanillaSnapshot = True
                                            SnapshotCheckBox.Checked = True
                                        Case "false"
                                            ShowVanillaSnapshot = False
                                            SnapshotCheckBox.Checked = False
                                    End Select
                                End If
                            Case "custom-forge-ver"
                                If info(1).Trim <> "" Then
                                    Select Case info(1).Trim
                                        Case "true"
                                            CustomForgeVersion = True
                                            CheckBox3.Checked = True
                                        Case "false"
                                            CustomForgeVersion = False
                                            CheckBox3.Checked = False
                                    End Select
                                End If
                            Case "console-input-mode"
                                If info(1).Trim <> "" Then
                                    Select Case info(1).Trim
                                        Case "true"
                                            ConsoleMode = True
                                        Case "false"
                                            ConsoleMode = False
                                    End Select
                                End If
                            Case "server-console-msgs"
                                If info(1).Trim <> "" Then
                                    Dim resultList As New List(Of Boolean)
                                    For Each c As Char In info(1).Trim
                                        Select Case c.ToString
                                            Case "0"
                                                resultList.Add(False)
                                            Case "1"
                                                resultList.Add(True)
                                        End Select
                                    Next
                                    ServerConsoleMessages = resultList.ToArray()
                                End If
                            Case "bungeecord-console-msgs"
                                If info(1).Trim <> "" Then
                                    Dim resultList As New List(Of Boolean)
                                    For Each c As Char In info(1).Trim
                                        Select Case c.ToString
                                            Case "0"
                                                resultList.Add(False)
                                            Case "1"
                                                resultList.Add(True)
                                        End Select
                                    Next
                                    BungeeConsoleMessages = resultList.ToArray()
                                End If
                        End Select
                    End If
                Loop
                If My.Settings.NoIPPasswordViewChecked Then NoIPPasswordBox.PasswordChar = "*"
                NoIPAccountBox.Text = username
                NoIPPasswordBox.Text = password
                If username <> "" And password <> "" Then
                    Dim loginThread As New Thread(Sub()
                                                      Dim flag As Boolean = False
                                                      LoginNoIP(flag, True)
                                                      If NoIPProvider IsNot Nothing Then
                                                          Do Until flag
                                                          Loop
                                                          If hosts IsNot Nothing Then
                                                              For Each host In hosts
                                                                  Dim max = HostCheckList.Items.Count
                                                                  For i As Integer = 0 To max
                                                                      Dim counter = i
                                                                      If HostCheckList.Items(i).ToString.StartsWith(host) Then
                                                                          BeginInvokeIfRequired(Me, Sub() HostCheckList.SetItemChecked(counter, True))
                                                                          Exit For
                                                                      End If
                                                                  Next
                                                              Next
                                                          End If
                                                      End If
                                                      Button5_Click(Button5, New EventArgs)
                                                  End Sub)
                    loginThread.IsBackground = True
                    loginThread.Name = "No-IP AutoLogin Thread"
                    loginThread.Start()
                End If
            End Using
            If JavaPath = "" Then
                GetJava()
            Else
                If IO.Directory.Exists(JavaPath) Then
                    GetJava(JavaPath)
                Else
                    GetJava()
                End If
            End If
        Else
            GetJava()
        End If
    End Sub
    Friend Overloads Sub GetJava(Optional notInStartup As Boolean = False)
        JavaVersionLabel.Text = "Java 版本：取得中..."
        ToolTip1.SetToolTip(JavaVersionLabel, "")
        Dim thread As New Threading.Thread(New Threading.ThreadStart(Sub()
                                                                         Try
                                                                             Dim process As Process = Process.Start(New ProcessStartInfo("java", "-XshowSettings:properties") With {.CreateNoWindow = True, .ErrorDialog = False, .RedirectStandardOutput = True, .RedirectStandardError = True, .UseShellExecute = False})
                                                                             process.EnableRaisingEvents = True
                                                                             process.BeginOutputReadLine()
                                                                             process.BeginErrorReadLine()
                                                                             Dim JavaArch As String = ""
                                                                             Dim JavaRTName As String = ""
                                                                             Dim JavaRTVersion As String = ""
                                                                             Dim JavaRTPath As String = ""
                                                                             AddHandler process.ErrorDataReceived, Sub(obj, args)
                                                                                                                       Try
                                                                                                                           Dim infoText As String = args.Data.Trim
                                                                                                                           If infoText.Contains(" = ") Then
                                                                                                                               Dim info = infoText.Split(New String() {" = "}, 2, StringSplitOptions.None)
                                                                                                                               Select Case info(0)
                                                                                                                                   Case "sun.arch.data.model"
                                                                                                                                       JavaArch = info(1)
                                                                                                                                   Case "java.runtime.name"
                                                                                                                                       JavaRTName = info(1)
                                                                                                                                   Case "java.version"
                                                                                                                                       JavaRTVersion = info(1)
                                                                                                                                   Case "java.home"
                                                                                                                                       JavaRTPath = IO.Path.Combine(info(1), "bin")
                                                                                                                               End Select
                                                                                                                               If JavaArch <> "" And JavaRTName <> "" And JavaRTVersion <> "" And JavaRTPath <> "" Then
                                                                                                                                   Invoke(Sub()
                                                                                                                                              JavaVersionLabel.Text = "Java 版本：" & JavaRTVersion & " (" & JavaArch & " 位元)"
                                                                                                                                              ToolTip1.SetToolTip(JavaVersionLabel, "版本：" & JavaRTVersion & " (" & JavaArch & " 位元)" & vbNewLine & "路徑：" & JavaRTPath)
                                                                                                                                          End Sub)
                                                                                                                                   JavaPath = ""
                                                                                                                                   Is32BitJava = (JavaArch = 32)
                                                                                                                                   HasJava = True
                                                                                                                                   Try
                                                                                                                                       process.Kill()
                                                                                                                                   Catch ex As Exception
                                                                                                                                   End Try
                                                                                                                               End If
                                                                                                                           End If
                                                                                                                       Catch ex As Exception
                                                                                                                       End Try
                                                                                                                   End Sub
                                                                             AddHandler process.OutputDataReceived, Sub(obj, args)
                                                                                                                        Try
                                                                                                                            Dim infoText As String = args.Data.Trim
                                                                                                                            If infoText.Contains(" = ") Then
                                                                                                                                Dim info = infoText.Split(New String() {" = "}, 2, StringSplitOptions.None)
                                                                                                                                Select Case info(0)
                                                                                                                                    Case "sun.arch.data.model"
                                                                                                                                        JavaArch = info(1)
                                                                                                                                    Case "java.runtime.name"
                                                                                                                                        JavaRTName = info(1)
                                                                                                                                    Case "java.version"
                                                                                                                                        JavaRTVersion = info(1)
                                                                                                                                    Case "java.home"
                                                                                                                                        JavaRTPath = IO.Path.Combine(info(1), "bin")
                                                                                                                                End Select
                                                                                                                                If JavaArch <> "" And JavaRTName <> "" And JavaRTVersion <> "" And JavaRTPath <> "" Then
                                                                                                                                    Invoke(Sub()
                                                                                                                                               JavaVersionLabel.Text = "Java 版本：" & JavaRTVersion & " (" & JavaArch & " 位元)"
                                                                                                                                               ToolTip1.SetToolTip(JavaVersionLabel, "版本：" & JavaRTVersion & " (" & JavaArch & " 位元)" & vbNewLine & "路徑：" & JavaRTPath)
                                                                                                                                           End Sub)
                                                                                                                                    JavaPath = ""
                                                                                                                                    Is32BitJava = (JavaArch = 32)
                                                                                                                                    HasJava = True
                                                                                                                                    Try
                                                                                                                                        process.Kill()
                                                                                                                                    Catch ex As Exception
                                                                                                                                    End Try
                                                                                                                                End If
                                                                                                                            End If
                                                                                                                        Catch ex As Exception
                                                                                                                        End Try
                                                                                                                    End Sub
                                                                             AddHandler process.Exited, Sub()
                                                                                                            If JavaArch = "" OrElse JavaRTName = "" OrElse JavaRTVersion = "" OrElse JavaRTPath = "" Then
                                                                                                                Invoke(Sub()
                                                                                                                           JavaVersionLabel.Text = "Java 版本：(無)"
                                                                                                                           ToolTip1.SetToolTip(JavaVersionLabel, "")
                                                                                                                           JavaPath = ""
                                                                                                                       End Sub)
                                                                                                                HasJava = False
                                                                                                                MsgBox("請確定電腦已經安裝Java", MsgBoxStyle.OkOnly, "偵測不到系統 Java")

                                                                                                            Else
                                                                                                                BeginInvoke(Sub() JavaDefaultBtn.Enabled = False)
                                                                                                            End If
                                                                                                        End Sub
                                                                         Catch ex As Exception

                                                                             Invoke(Sub()
                                                                                        JavaVersionLabel.Text = "Java 版本：(無)"
                                                                                        ToolTip1.SetToolTip(JavaVersionLabel, "")
                                                                                        JavaPath = ""
                                                                                    End Sub)
                                                                             HasJava = False
                                                                             MsgBox("請確定電腦已經安裝Java", MsgBoxStyle.OkOnly, "偵測不到系統 Java")
                                                                         End Try
                                                                     End Sub))
        thread.Name = "Java Version Getter"
        thread.IsBackground = True
        thread.Start()
    End Sub
    Friend Overloads Sub GetJava(JavaPath As String)
        JavaVersionLabel.Text = "Java 版本：取得中..."
        ToolTip1.SetToolTip(JavaVersionLabel, "")
        Dim thread As New Threading.Thread(New Threading.ThreadStart(Sub()
                                                                         Try
                                                                             Dim process As Process = Process.Start(New ProcessStartInfo(GetJavaPath(), "-XshowSettings:properties") With {.CreateNoWindow = True, .ErrorDialog = False, .RedirectStandardOutput = True, .RedirectStandardError = True, .UseShellExecute = False})
                                                                             process.EnableRaisingEvents = True
                                                                             process.BeginOutputReadLine()
                                                                             process.BeginErrorReadLine()
                                                                             Dim JavaArch As String = ""
                                                                             Dim JavaRTName As String = ""
                                                                             Dim JavaRTVersion As String = ""
                                                                             AddHandler process.ErrorDataReceived, Sub(obj, args)
                                                                                                                       Try
                                                                                                                           Dim infoText As String = args.Data.Trim
                                                                                                                           If infoText.Contains(" = ") Then
                                                                                                                               Dim info = infoText.Split(New String() {" = "}, 2, StringSplitOptions.None)
                                                                                                                               Select Case info(0)
                                                                                                                                   Case "sun.arch.data.model"
                                                                                                                                       JavaArch = info(1)
                                                                                                                                   Case "java.runtime.name"
                                                                                                                                       JavaRTName = info(1)
                                                                                                                                   Case "java.version"
                                                                                                                                       JavaRTVersion = info(1)
                                                                                                                               End Select
                                                                                                                               If JavaArch <> "" And JavaRTName <> "" And JavaRTVersion <> "" Then
                                                                                                                                   Invoke(Sub()
                                                                                                                                              JavaVersionLabel.Text = "Java 版本：" & JavaRTVersion & " (" & JavaArch & " 位元)"
                                                                                                                                              ToolTip1.SetToolTip(JavaVersionLabel, "版本：" & JavaRTVersion & " (" & JavaArch & " 位元)" & vbNewLine & "路徑：" & JavaPath)
                                                                                                                                          End Sub)
                                                                                                                                   GlobalModule.JavaPath = JavaPath
                                                                                                                                   Is32BitJava = (JavaArch = 32)
                                                                                                                                   HasJava = True
                                                                                                                                   Try
                                                                                                                                       process.Kill()
                                                                                                                                   Catch ex As Exception
                                                                                                                                   End Try
                                                                                                                               End If
                                                                                                                           End If
                                                                                                                       Catch ex As Exception
                                                                                                                       End Try
                                                                                                                   End Sub
                                                                             AddHandler process.Exited, Sub()
                                                                                                            If JavaArch = "" OrElse JavaRTName = "" OrElse JavaRTVersion = "" Then
                                                                                                                Invoke(Sub()
                                                                                                                           JavaVersionLabel.Text = "Java 版本：(無)"
                                                                                                                           ToolTip1.SetToolTip(JavaVersionLabel, "")

                                                                                                                       End Sub)
                                                                                                                HasJava = False
                                                                                                                MsgBox("請確定Java路徑", MsgBoxStyle.OkOnly, "偵測不到 Java")
                                                                                                            Else
                                                                                                                BeginInvoke(Sub() JavaDefaultBtn.Enabled = True)
                                                                                                            End If
                                                                                                        End Sub
                                                                         Catch ex As Exception
                                                                             Invoke(Sub()
                                                                                        JavaVersionLabel.Text = "Java 版本：(無)"
                                                                                        ToolTip1.SetToolTip(JavaVersionLabel, "")

                                                                                    End Sub)
                                                                             HasJava = False
                                                                             MsgBox("請確定Java路徑", MsgBoxStyle.OkOnly, "偵測不到 Java")
                                                                         End Try
                                                                     End Sub))
        thread.Name = "Java Version Getter"
        thread.IsBackground = True
        thread.Start()
    End Sub

    Friend Sub AddServer(serverDirectory As String)
        SyncLock Me
            If ServerPathList.Contains(serverDirectory) = False And IO.Directory.Exists(serverDirectory) Then
                Dim status As New ServerStatus(serverDirectory)
                If status.Server.ServerVersionType = Server.EServerVersionType.Spigot_Git Then
                    status = New SpigotGitStatus(status.Server)
                End If
                status.Dock = DockStyle.Fill
                AddHandler status.ServerLoaded, Sub()
                                                    If status.InvokeRequired Then
                                                        status.BeginInvoke(Sub() status.Update())
                                                    Else
                                                        status.Update()
                                                    End If
                                                    If ServerListPanel.InvokeRequired Then
                                                        ServerListPanel.BeginInvoke(Sub()
                                                                                        Dim index = ServerListPanel.RowStyles.Count
                                                                                        ServerListPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
                                                                                        ServerListPanel.Controls.Add(status, 0, index)
                                                                                        ServerListPanel.Update()
                                                                                    End Sub)
                                                    Else
                                                        Dim index = ServerListPanel.RowStyles.Count
                                                        ServerListPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
                                                        ServerListPanel.Controls.Add(status, 0, index)
                                                    End If
                                                End Sub
                status.LoadStatus()
                ServerPathList.Add(serverDirectory)
                AddHandler status.DeleteServer, Sub(NoUI)
                                                    If NoUI Then
                                                        Try
                                                            If IsNothing(status) = False Then
                                                                status.CloseServer()
                                                            End If
                                                        Catch ex As Exception
                                                        Finally
                                                            BeginInvokeIfRequired(Me, Sub()
                                                                                          Try
                                                                                              ServerListPanel.Controls.Remove(status)
                                                                                          Catch ex As Exception
                                                                                          End Try
                                                                                          If ServerPathList.Contains(status.Server.ServerPath) Then _
                                                                                                 ServerPathList.Remove(status.Server.ServerPath)
                                                                                      End Sub)
                                                        End Try
                                                    Else
                                                        Try
                                                            Select Case MsgBox("要刪除伺服器的資料夾嗎？", MsgBoxStyle.YesNoCancel, "移除伺服器")
                                                                Case MsgBoxResult.Yes
                                                                    If My.Computer.FileSystem.DirectoryExists(status.Server.ServerPath) Then
                                                                        My.Computer.FileSystem.DeleteDirectory(status.Server.ServerPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
                                                                    End If
                                                                Case MsgBoxResult.Cancel
                                                                    Exit Sub
                                                            End Select
                                                        Catch ex As Exception
                                                        End Try
                                                        Try
                                                            ServerListPanel.Controls.Remove(status)
                                                        Catch ex As Exception
                                                        End Try
                                                        If ServerPathList.Contains(status.Server.ServerPath) Then _
                                                                                                 ServerPathList.Remove(status.Server.ServerPath)
                                                        If IsNothing(status) = False Then
                                                            Try
                                                                status.CloseServer()
                                                            Catch ex As Exception

                                                            End Try
                                                        End If
                                                    End If
                                                End Sub
            End If
        End SyncLock
    End Sub

    Friend Sub AddModpackServer(serverDirectory As String, Optional Register As Boolean = False)
        SyncLock Me
            If ModpackServerPathList.Contains(serverDirectory) = False And IO.Directory.Exists(serverDirectory) Then
                Dim status As New ModPackServerStatus(serverDirectory)
                status.Dock = DockStyle.Fill
                AddHandler status.ServerLoaded, Sub()
                                                    If Register Then
                                                        RegisterModpackServer(status.Server)
                                                    End If
                                                    If status.InvokeRequired Then
                                                        status.BeginInvoke(Sub() status.Update())
                                                    Else
                                                        status.Update()
                                                    End If
                                                    If ModpackServerListPanel.InvokeRequired Then
                                                        ModpackServerListPanel.BeginInvoke(Sub()
                                                                                               Dim index = ModpackServerListPanel.RowStyles.Count
                                                                                               ModpackServerListPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
                                                                                               ModpackServerListPanel.Controls.Add(status, 0, index)
                                                                                               ModpackServerListPanel.Update()
                                                                                           End Sub)
                                                    Else
                                                        Dim index = ModpackServerListPanel.RowStyles.Count
                                                        ModpackServerListPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
                                                        ModpackServerListPanel.Controls.Add(status, 0, index)
                                                    End If
                                                End Sub
                status.LoadStatus()
                ModpackServerPathList.Add(serverDirectory)
                AddHandler status.DeleteServer, Sub(NoUI)
                                                    If NoUI Then
                                                        Try
                                                            If IsNothing(status) = False Then
                                                                UnRegisterModpackServer(status)
                                                                status.CloseServer()
                                                            End If
                                                        Catch ex As Exception
                                                        Finally
                                                            Try
                                                                ModpackServerListPanel.Controls.Remove(status)
                                                            Catch ex As Exception
                                                            End Try
                                                            If ModpackServerPathList.Contains(status.Server.ServerPath) Then _
                                                                    ModpackServerPathList.Remove(status.Server.ServerPath)
                                                        End Try
                                                    Else
                                                        Try
                                                            Select Case MsgBox("要刪除伺服器的資料夾嗎？", MsgBoxStyle.YesNoCancel, "移除伺服器")
                                                                Case MsgBoxResult.Yes
                                                                    If My.Computer.FileSystem.DirectoryExists(status.Server.ServerPath) Then
                                                                        My.Computer.FileSystem.DeleteDirectory(status.Server.ServerPath, FileIO.DeleteDirectoryOption.DeleteAllContents)
                                                                    End If
                                                                Case MsgBoxResult.Cancel
                                                                    Exit Sub
                                                            End Select
                                                        Catch ex As Exception
                                                        End Try
                                                        Try
                                                            ModpackServerListPanel.Controls.Remove(status)
                                                        Catch ex As Exception
                                                        End Try
                                                        If ModpackServerPathList.Contains(status.Server.ServerPath) Then _
                                                                    ModpackServerPathList.Remove(status.Server.ServerPath)
                                                        If IsNothing(status) = False Then
                                                            Try
                                                                UnRegisterModpackServer(status)
                                                            Catch ex As Exception
                                                            End Try
                                                            Try
                                                                status.CloseServer()
                                                            Catch ex As Exception
                                                            End Try
                                                        End If
                                                    End If
                                                End Sub
            End If
        End SyncLock
    End Sub

    Sub RegisterModpackServer(server As ModPackServer)
        SyncLock Me
            If ModpackServerDirs <> "" Then
                Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(ModpackServerDirs)
                array.Add(New Newtonsoft.Json.Linq.JValue(server.ServerPath))
                ModpackServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
            Else
                Dim array = New Newtonsoft.Json.Linq.JArray()
                array.Add(New Newtonsoft.Json.Linq.JValue(server.ServerPath))
                ModpackServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
            End If
        End SyncLock
    End Sub
    Sub UnRegisterModpackServer(status As ModPackServerStatus)
        SyncLock Me
            Try
                If ModpackServerDirs <> "" Then
                    Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(ModpackServerDirs)
                    For i As Integer = 0 To array.Count
                        If array(i).ToString = status.Server.ServerPath Then array.RemoveAt(i)
                    Next
                    ModpackServerDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
                End If
            Catch ex As Exception
            End Try
        End SyncLock
    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Static dialog As New OpenFileDialog
        dialog.Filter = "伺服器資訊(server.info)|server.info"
        If dialog.ShowDialog = DialogResult.OK Then
            If ServerPathList.Contains(New IO.FileInfo(dialog.FileName).Directory.FullName) Then
                MsgBox("匯入時發生錯誤" & vbNewLine & "原因：伺服器已經存在於伺服器列表。",, Application.ProductName)
            Else
                AddServer(New IO.FileInfo(dialog.FileName).Directory.FullName)
            End If
        End If
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim create As New ServerCreateDialog()
        create.Show(Me)
    End Sub
    Private Sub Manager_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        NotifyIcon1.Visible = False
        NotifyIcon1.Icon = Nothing
        NotifyIcon1.Text = ""
        NotifyIcon1.Dispose()
        NotifyIcon1 = Nothing
        Dim saveThread As New Thread(Sub()
                                         Dim username, password, hosts As String
                                         If NoIPProvider IsNot Nothing Then
                                             username = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(NoIPProvider._username))
                                             password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(NoIPProvider._password))
                                             If NoIPProvider._targets IsNot Nothing AndAlso NoIPProvider._targets.Count > 0 Then
                                                 Dim arr As New JArray
                                                 For Each host In NoIPProvider._targets
                                                     arr.Add(host)
                                                 Next
                                                 hosts = JsonConvert.SerializeObject(arr)
                                             Else
                                                 hosts = ""
                                             End If
                                         Else
                                             username = ""
                                             password = ""
                                             hosts = ""
                                         End If
                                         My.Computer.FileSystem.WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "manager-setting.txt"),
                                            "memory-min=" & ServerMemoryMin & vbNewLine &
                                            "memory-max=" & ServerMemoryMax & vbNewLine &
                                            "bungee-memory-min=" & BungeeCordMemoryMin & vbNewLine &
                                            "bungee-memory-max=" & BungeeCordMemoryMax & vbNewLine &
                                            "java-arguments=" & JavaArguments & vbNewLine &
                                            "java-path=" & JavaPath & vbNewLine &
                                            "noip-username=" & username & vbNewLine &
                                            "noip-password=" & password & vbNewLine &
                                            "noip-hosts= " & hosts & vbNewLine &
                                            "git-bash-path=" & GitBashPath & vbNewLine &
                                            "show-vanilla-snaps=" & ShowVanillaSnapshot.ToString.ToLower & vbNewLine &
                                            "custom-forge-ver=" & CustomForgeVersion.ToString.ToLower & vbNewLine &
                                            "console-input-mode=" & ConsoleMode.ToString.ToLower & vbNewLine &
                                           "server-console-msgs=" & ToZeroAndOne(ServerConsoleMessages) & vbNewLine &
                                           "bungeecord-console-msgs=" & ToZeroAndOne(BungeeConsoleMessages), False, System.Text.Encoding.UTF8)
                                         WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "servers.txt"), JsonConvert.SerializeObject(ServerPathList))
                                         WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "solutions.txt"), SolutionDirs)
                                         WriteAllText(IO.Path.Combine(My.Application.Info.DirectoryPath, "modPackServer.txt"), ModpackServerDirs)
                                     End Sub) With {.IsBackground = False, .Name = "ServerManager Save Setting Thread"}
        saveThread.Start()
        Dim serverCloseThread As New Thread(Sub()
                                                Try
                                                    For i = 0 To ServerListPanel.RowCount
                                                        CType(ServerListPanel.GetControlFromPosition(0, i), ServerStatus).CloseServer()
                                                    Next
                                                Catch ex As Exception
                                                End Try
                                            End Sub) With {.IsBackground = False, .Name = "Servers Close Thread"}
        serverCloseThread.Start()
        Dim bungeeCloseThread As New Thread(Sub()
                                                Try
                                                    For Each item In SolutionListPanel.Controls
                                                        If item IsNot Nothing Then CType(item, BungeeCordStatus).CloseSolution()
                                                    Next
                                                Catch ex As Exception
                                                End Try
                                            End Sub) With {.IsBackground = False, .Name = "Solutions Close Thread"}
        bungeeCloseThread.Start()
        Dim modpackCloseThread As New Thread(Sub()
                                                 Try
                                                     For Each item In ModpackServerListPanel.Controls
                                                         If item IsNot Nothing Then CType(item, ModPackServerStatus).CloseServer()
                                                     Next
                                                 Catch ex As Exception
                                                 End Try
                                             End Sub) With {.IsBackground = False, .Name = "Solutions Close Thread"}
        modpackCloseThread.Start()
    End Sub
    Private Sub ArguBox_TextChanged(sender As Object, e As EventArgs) Handles ArguBox.TextChanged
        JavaArguments = ArguBox.Text
    End Sub

    Sub AddBungeeSolution(solutionDirectory As String, Optional Register As Boolean = False)
        If BungeePathList.Contains(solutionDirectory) = False And IO.Directory.Exists(solutionDirectory) Then
            Dim status As New BungeeCordStatus(solutionDirectory)
            status.Dock = DockStyle.Fill
            AddHandler status.BungeeCordLoaded, Sub()
                                                    If Register Then
                                                        RegisterSolution(status.Host)
                                                    End If
                                                    status.Update()
                                                End Sub
            Dim index = SolutionListPanel.RowStyles.Count
            SolutionListPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            SolutionListPanel.Controls.Add(status, 0, index)
            BungeePathList.Add(solutionDirectory)
            AddHandler status.DeleteBungeeCordSolution, Sub(NoUI)
                                                            If NoUI Then
                                                                Try
                                                                    If IsNothing(status) = False Then
                                                                        UnRegisterSolution(status)
                                                                        status.CloseSolution()
                                                                    End If
                                                                Catch ex As Exception
                                                                Finally
                                                                    Try
                                                                        SolutionListPanel.Controls.Remove(status)
                                                                    Catch ex As Exception
                                                                    End Try
                                                                    If BungeePathList.Contains(status.Host.BungeePath) Then _
                                                                             BungeePathList.Remove(status.Host.BungeePath)
                                                                End Try
                                                            Else
                                                                Try
                                                                    Select Case MsgBox("要刪除伺服器的資料夾嗎？", MsgBoxStyle.YesNoCancel, "移除伺服器")
                                                                        Case MsgBoxResult.Yes
                                                                            If My.Computer.FileSystem.DirectoryExists(status.Host.BungeePath) Then
                                                                                My.Computer.FileSystem.DeleteDirectory(status.Host.BungeePath, FileIO.DeleteDirectoryOption.DeleteAllContents)
                                                                            End If
                                                                        Case MsgBoxResult.Cancel
                                                                            Exit Sub
                                                                    End Select
                                                                Catch ex As Exception
                                                                End Try
                                                                Try
                                                                    SolutionListPanel.Controls.Remove(status)
                                                                Catch ex As Exception
                                                                End Try
                                                                If BungeePathList.Contains(status.Host.BungeePath) Then _
                                                                             BungeePathList.Remove(status.Host.BungeePath)
                                                                If IsNothing(status) = False Then
                                                                    Try
                                                                        UnRegisterSolution(status)
                                                                    Catch ex As Exception
                                                                    End Try
                                                                    Try
                                                                        status.CloseSolution()
                                                                    Catch ex As Exception
                                                                    End Try
                                                                End If
                                                            End If
                                                        End Sub
        End If

    End Sub
    Sub RegisterSolution(host As BungeeCordHost)
        SyncLock Me
            If SolutionDirs <> "" Then
                Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(SolutionDirs)
                array.Add(New Newtonsoft.Json.Linq.JValue(host.BungeePath))
                SolutionDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
            Else
                Dim array = New Newtonsoft.Json.Linq.JArray()
                array.Add(New Newtonsoft.Json.Linq.JValue(host.BungeePath))
                SolutionDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
            End If
        End SyncLock
    End Sub
    Sub UnRegisterSolution(status As BungeeCordStatus)
        SyncLock Me
            Try
                If SolutionDirs <> "" Then
                    Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(SolutionDirs)
                    array.RemoveAt(SolutionListPanel.GetCellPosition(status).Row - 1)
                    SolutionDirs = Newtonsoft.Json.JsonConvert.SerializeObject(array)
                End If
            Catch ex As Exception
            End Try
        End SyncLock
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim create As New BungeeCordCreateDialog()
        create.Show(Me)
    End Sub

    Private Sub JavaChooseBtn_Click(sender As Object, e As EventArgs) Handles JavaChooseBtn.Click
        Static dialog As New JavaChooseForm
        If dialog IsNot Nothing AndAlso dialog.IsDisposed = False Then
        Else
            dialog = New JavaChooseForm()
        End If
        Using dialog
            If dialog.ShowDialog() = DialogResult.OK Then
                GetJava(dialog.ChoosedJava.Path)
            End If
        End Using
    End Sub

    Private Sub JavaDefaultBtn_Click(sender As Object, e As EventArgs) Handles JavaDefaultBtn.Click
        GetJava(True)
    End Sub
    Private Sub BungeeMemoryMaxBox_ValueChanged(sender As Object, e As EventArgs) Handles BungeeMemoryMaxBox.ValueChanged
        BungeeCordMemoryMax = BungeeMemoryMaxBox.Value
        If Is32BitJava = True And HasJava And BungeeMemoryMaxBox.Value > 1024 Then
            Label2.Text = "MB (記憶體過大)"
            Label2.ForeColor = Color.Red
        Else
            Label2.Text = "MB"
            Label2.ForeColor = Color.Black
        End If
    End Sub

    Private Sub BungeeMemoryMinBox_ValueChanged(sender As Object, e As EventArgs) Handles BungeeMemoryMinBox.ValueChanged
        BungeeCordMemoryMin = BungeeMemoryMinBox.Value
        If Is32BitJava = True And HasJava And BungeeMemoryMinBox.Value > 1024 Then
            Label3.Text = "MB (記憶體過大)"
            Label3.ForeColor = Color.Red
        Else
            Label3.Text = "MB"
            Label3.ForeColor = Color.Black
        End If
    End Sub
    Private Sub MemoryMaxBox_ValueChanged(sender As Object, e As EventArgs) Handles ServerMemoryMaxBox.ValueChanged
        ServerMemoryMax = ServerMemoryMaxBox.Value
        If Is32BitJava = True And HasJava And ServerMemoryMaxBox.Value > 1024 Then
            Label12.Text = "MB (記憶體過大)"
            Label12.ForeColor = Color.Red
        Else
            Label12.Text = "MB"
            Label12.ForeColor = Color.Black
        End If
    End Sub

    Private Sub MemoryMinBox_ValueChanged(sender As Object, e As EventArgs) Handles ServerMemoryMinBox.ValueChanged
        ServerMemoryMin = ServerMemoryMinBox.Value
        If Is32BitJava = True And HasJava And ServerMemoryMinBox.Value > 1024 Then
            Label14.Text = "MB (記憶體過大)"
            Label14.ForeColor = Color.Red
        Else
            Label14.Text = "MB"
            Label14.ForeColor = Color.Black
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim loginThread As New Thread(Sub()
                                          LoginNoIP(Nothing)
                                      End Sub)
        loginThread.IsBackground = True
        loginThread.Name = "No-IP Login/Logout Thread"
        loginThread.Start()
    End Sub
    Sub LoginNoIP(ByRef HostLoaded As Boolean, Optional returnFlag As Boolean = False)
        Static isLogin As Boolean = False
        If isLogin Then
            NoIPProvider = Nothing
            isLogin = False
            BeginInvoke(Sub()
                            NoIPTimer.Stop()
                            NoIPUpdateTimeSpanLabel.Text = ""
                            Button4.Text = "登入"
                            NoIPAccountBox.Enabled = True
                            NoIPPasswordBox.Enabled = True
                            Panel2.Enabled = False
                            NoIPTimer.Stop()
                        End Sub)

        Else
            If NoIPAccountBox.Text.Trim <> "" And NoIPPasswordBox.Text.Trim <> "" Then
                Try
                    NoIPProvider = New NoIPProvider(NoIPAccountBox.Text, NoIPPasswordBox.Text)
                Catch ex As NoIPProvider.HasNotRegisteredException
                    MsgBox("帳號密碼有誤!")
                    Exit Sub
                End Try
                BeginInvoke(Sub()
                                Button4.Text = "登入中..."
                                Button4.Enabled = False
                            End Sub)
                Dim zones = NoIPProvider.GetAllHosts
                Invoke(Sub()
                           HostCheckList.Tag = Nothing
                           HostCheckList.Items.Clear()
                       End Sub)
                For Each hosts In zones
                    For Each host In hosts.Value
                        Invoke(Sub() HostCheckList.Items.Add(host.Name & "." & hosts.Key.Name & " (" & hosts.Key.Type.ToString & ")"))
                    Next
                Next
                If returnFlag Then
                    HostLoaded = True
                End If
                isLogin = True
                BeginInvoke(Sub()
                                HostCheckList.Tag = zones
                                Panel2.Enabled = True
                                NoIPAccountBox.Enabled = False
                                NoIPPasswordBox.Enabled = False
                                Button4.Enabled = True
                                Button4.Text = "登出"
                            End Sub)
            End If
        End If
    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Static NoIPLinked As Boolean = False
        Dim ConnectHostThread As New Thread(Sub()
                                                If NoIPLinked Then
                                                    BeginInvoke(Sub()
                                                                    If NoIPProvider IsNot Nothing Then
                                                                        NoIPTimer.Stop()
                                                                        NoIPProvider.TargetHosts(Nothing)
                                                                        NoIPUpdateTimeSpanLabel.Text = ""
                                                                    End If
                                                                    NoIPLinked = False
                                                                    HostCheckList.Enabled = True
                                                                    Button4.Enabled = True
                                                                    Button5.Text = "連接"
                                                                End Sub)

                                                Else
                                                    If NoIPProvider IsNot Nothing Then
                                                        If HostCheckList.CheckedItems IsNot Nothing AndAlso HostCheckList.CheckedItems.Count > 0 Then
                                                            Dim hostList As New List(Of String)
                                                            BeginInvoke(Sub()
                                                                            For Each item In HostCheckList.SelectedItems
                                                                                Dim zones = CType(HostCheckList.Tag, Dictionary(Of DDNS.DTO.Zone, DDNS.DTO.Host()))
                                                                                For Each pair In zones
                                                                                    For Each host In pair.Value
                                                                                        If host.Name & "." & pair.Key.Name & " (" & pair.Key.Type.ToString & ")" = item Then
                                                                                            hostList.Add(host.Name & "." & pair.Key.Name)
                                                                                            Exit For
                                                                                        End If
                                                                                    Next
                                                                                Next
                                                                            Next
                                                                        End Sub)
                                                            NoIPProvider.TargetHosts(hostList)
                                                            NoIPProvider.UpdateHosts()
                                                            NoIPLinked = True
                                                            BeginInvoke(Sub()
                                                                            NoIPTimer.Start()
                                                                            HostCheckList.Enabled = False
                                                                            Button4.Enabled = False
                                                                            Button5.Text = "斷開連接"
                                                                        End Sub)
                                                        End If
                                                    End If
                                                End If
                                            End Sub)
        ConnectHostThread.IsBackground = True
        ConnectHostThread.Name = "Connect/Disconnect Hosts Thread"
        ConnectHostThread.Start()
    End Sub

    Private Sub NoIPTimer_Tick(sender As Object, e As EventArgs) Handles NoIPTimer.Tick
        Static LastUpdateTime As Date = Nothing
        If LastUpdateTime = Nothing Then
            Dim updateThread As New Thread(Sub()
                                               NoIPProvider.UpdateHosts()
                                           End Sub)
            updateThread.IsBackground = True
            updateThread.Name = "No-IP Auto-Update IP Thread"
            updateThread.Start()
            LastUpdateTime = Now
        ElseIf (Now - LastUpdateTime).Minutes >= 5 Then
            Dim updateThread As New Thread(Sub()
                                               NoIPProvider.UpdateHosts()
                                           End Sub)
            updateThread.IsBackground = True
            updateThread.Name = "No-IP Auto-Update IP Thread"
            updateThread.Start()
            LastUpdateTime = Now
        End If
        Dim span = (New TimeSpan(0, 5, 0) - (Now - LastUpdateTime))
        NoIPUpdateTimeSpanLabel.Text = String.Format("將於{0}分{1}秒後更新IP位址", span.Minutes, span.Seconds)

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        Select Case CheckBox2.Checked
            Case True
                NoIPPasswordBox.PasswordChar = ""
            Case False
                NoIPPasswordBox.PasswordChar = "*"
        End Select
    End Sub



    Private Sub Manager_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        'Debug Server Problem Only!
        'TestForm = (New ServerCheckingForm(0))
        'TestForm.Show()
        If IsUnixLikeSystem Then
            JavaDefaultBtn.Visible = False
            JavaChooseBtn.Visible = False
            JavaDefaultBtn.Enabled = False
            JavaChooseBtn.Enabled = False
            SettingTabControl.TabPages.Remove(TabPage3)
        End If
        Label9.Text = Label9.Text.Replace("<Version>", SERVER_MANAGER_VER)
        Dim r As New Random()
        Select Case r.Next(20)
            Case 1
                Invoke(Sub() Me.Text = Me.Text.Replace("Minecraft", "Minceraft"))
        End Select
        r = Nothing
        UpdateVersionLists()
        UpdateModpackList()
        CheckBox2_CheckedChanged(CheckBox2, New EventArgs)
        GC.Collect()
    End Sub

    Private Sub ServerSoftwareLinkList_ItemActivate(sender As Object, e As EventArgs) Handles ServerSoftwareLinkList.ItemActivate
        If ServerSoftwareLinkList.SelectedItems(0).Tag IsNot Nothing AndAlso ServerSoftwareLinkList.SelectedItems(0).Tag.ToString <> "" Then
            Process.Start(ServerSoftwareLinkList.SelectedItems(0).Tag.ToString)
        End If
    End Sub

    Private Sub SnapshotCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles SnapshotCheckBox.CheckedChanged
        'My.Settings.ShowSnapshot = SnapshotCheckBox.Checked
    End Sub

    Private Sub GitBashBrowseButton_Click(sender As Object, e As EventArgs) Handles GitBashBrowseButton.Click
        Static openFileDialog As New OpenFileDialog
        If openFileDialog Is Nothing Then openFileDialog = New OpenFileDialog
        openFileDialog.Filter = "bash.exe (Git Bash CMD)| bash.exe"
        openFileDialog.SupportMultiDottedExtensions = True
        openFileDialog.Title = "選擇 Git Bash"
        If openFileDialog.ShowDialog = DialogResult.OK Then
            GitBashPathBox.Text = openFileDialog.FileName
        End If
    End Sub

    Private Sub GitBashPathBox_TextChanged(sender As Object, e As EventArgs) Handles GitBashPathBox.TextChanged
        If IO.File.Exists(GitBashPathBox.Text) Then GitBashPath = GitBashPathBox.Text
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Static dialog As New OpenFileDialog
        dialog.Filter = "伺服器資訊(server.info)|server.info"
        If dialog.ShowDialog = DialogResult.OK Then
            If ServerPathList.Contains(New IO.FileInfo(dialog.FileName).Directory.FullName) Then
                MsgBox("匯入時發生錯誤" & vbNewLine & "原因：伺服器已經存在於伺服器列表。",, Application.ProductName)
            Else
                AddModpackServer(New IO.FileInfo(dialog.FileName).Directory.FullName, True)
            End If
        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim create As New ModPackServerCreateDialog()
        create.Show(Me)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        UpdateModpackList()
    End Sub
    Private Sub UpdateModpackList()
        If My.Computer.Network.IsAvailable Then
            If IsNothing(FeedTheBeastGetPackThread) = False AndAlso FeedTheBeastGetPackThread.IsAlive = True Then
                Try
                    FeedTheBeastGetPackThread.Abort()
                Catch ex As Exception

                End Try
                FeedTheBeastGetPackThread = Nothing
            End If
            GetFeedTheBeastPackList()
        Else
            FTBLoadingLabel.Text = "Feed The Beast：" & "(無)"
        End If
        GC.Collect()
    End Sub
    Private Sub IPLabel_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles IPLabel.LinkClicked
        If e.Button = MouseButtons.Left Then
            My.Computer.Clipboard.SetText(IPLabel.Text.Substring(e.Link.Start, e.Link.Length))
            MsgBox("已複製到剪貼簿!")
        End If
    End Sub
    Private Sub CheckingTimer_Tick(sender As Object, e As EventArgs) Handles CheckingTimer.Tick
        Static Checking As Byte = 1
        BeginInvokeIfRequired(Me, Sub()
                                      Try
                                          Label18.Text = String.Format("實體記憶體：{0} ({1}可用)", FitMemoryUnit(My.Computer.Info.TotalPhysicalMemory), FitMemoryUnit(My.Computer.Info.AvailablePhysicalMemory))
                                          Checking += 1
                                          If IsUnixLikeSystem Then
                                              Label19.Text = String.Format("虛擬記憶體：{0} ({1}可用)", FitMemoryUnit(My.Computer.Info.TotalVirtualMemory), FitMemoryUnit(My.Computer.Info.AvailableVirtualMemory))
                                          Else
                                              Dim thread1 As New Thread(Sub()
                                                                            Dim winQuery As New System.Management.ObjectQuery("SELECT * FROM Win32_OperatingSystem")
                                                                            Dim searcher As New System.Management.ManagementObjectSearcher(winQuery)
                                                                            For Each item In searcher.Get()
                                                                                Try
                                                                                    Dim total = FitMemoryUnitOnKB(item("TotalVirtualMemorySize"))
                                                                                    Dim free = FitMemoryUnitOnKB(item("FreeVirtualMemory"))
                                                                                    BeginInvokeIfRequired(Me, Sub()
                                                                                                                  Label19.Text = String.Format("虛擬記憶體：{0} ({1}可用)", total, free)
                                                                                                              End Sub)
                                                                                Catch ex As Exception
                                                                                    Continue For
                                                                                End Try
                                                                                Exit For
                                                                            Next
                                                                        End Sub)
                                              thread1.IsBackground = True
                                              thread1.Start()
                                          End If
                                          If Checking = 2 Then
                                              Checking = 0
                                              Dim thread2 As New Thread(Sub()
                                                                            BeginInvokeIfRequired(Me, Sub() Label20.Text = String.Format("CPU使用率：{0} %", CInt(PerformanceCounter1.NextValue)))
                                                                        End Sub)
                                              thread2.IsBackground = True
                                              thread2.Start()
                                          End If
                                      Catch ex As Exception

                                      End Try
                                  End Sub)
    End Sub

    Private Sub 重新載入外部IPRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 重新載入外部IPRToolStripMenuItem.Click
        Dim tooltip As New ToolTip
        Select Case My.Computer.Network.IsAvailable
            Case True
                Dim ExternalIPSeeker As New Thread(Sub()
                                                       Invoke(Sub() IPALabel.Text = "網路狀態：" & "已連接")
                                                       Invoke(Sub()
                                                                  ExternalIPLabel.Text = "外部IP位址：載入中..."
                                                                  ExternalIPLabel.LinkArea = Nothing
                                                              End Sub)
                                                       Dim exip = GetExternalIP()
                                                       Invoke(Sub() ExternalIPLabel.Text = "外部IP位址：" & exip)
                                                       BeginInvoke(Sub()
                                                                       If exip <> "(無)" Then
                                                                           ExternalIPLabel.LinkArea = New LinkArea(ExternalIPLabel.Text.IndexOf(exip), exip.Length)
                                                                           ToolTip.SetToolTip(ExternalIPLabel, "點擊連結複製外部IP位址")
                                                                           AddHandler ExternalIPLabel.LinkClicked, Sub(obj, args)
                                                                                                                       If args.Button = MouseButtons.Left Then
                                                                                                                           My.Computer.Clipboard.SetText(exip)
                                                                                                                           MsgBox("已複製到剪貼簿!")
                                                                                                                       End If
                                                                                                                   End Sub
                                                                       Else
                                                                           ConnectionTabPage.Visible = False
                                                                           TableLayoutPanel3.Enabled = False
                                                                       End If
                                                                   End Sub)
                                                   End Sub)
                ExternalIPSeeker.Name = "External IP Seeker"
                ExternalIPSeeker.IsBackground = True
                ExternalIPSeeker.Start()
            Case False
                ExternalIPLabel.Text = "外部IP位址：" & "(無)"
                ConnectionTabPage.Visible = False
                TableLayoutPanel3.Enabled = False
        End Select
    End Sub

    Private Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Invoke(Sub()
                   IPLabel.Links.Clear()
                   IPLabel.Text = "內部IP位址：載入中..."
                   IPLabel.LinkArea = Nothing
               End Sub)
        Dim tooltip As New ToolTip
        Dim IPSeeker As New Thread(Sub()
                                       Try
                                           If IsNothing(Net.Dns.GetHostAddresses(Net.Dns.GetHostName)) = False Then
                                               BeginInvokeIfRequired(Me, Sub() IPLabel.Text = "內部IP位址：")
                                               Dim ipas = Net.Dns.GetHostAddresses(Net.Dns.GetHostName)
                                               For Each i In ipas
                                                   If i.AddressFamily = Net.Sockets.AddressFamily.InterNetwork Then
                                                       Dim b = i.GetAddressBytes
                                                       Dim inip = b(0) & "." & b(1) & "." & b(2) & "." & b(3)
                                                       BeginInvokeIfRequired(Me, Sub()
                                                                                     If Array.IndexOf(ipas, i) = ipas.Count - 1 Then
                                                                                         IPLabel.Text &= inip
                                                                                     Else
                                                                                         IPLabel.Text &= inip & " , "
                                                                                     End If
                                                                                     IPLabel.Links.Add(New LinkLabel.Link(IPLabel.Text.IndexOf(inip), inip.Length))
                                                                                     tooltip.SetToolTip(IPLabel, "點擊連結複製內部IP位址")
                                                                                 End Sub)
                                                       ip.Add(inip)
                                                   End If
                                               Next
                                               If ip.Count = 0 Then
                                                   BeginInvoke(Sub() IPLabel.Text = "內部IP位址：(無)")
                                               End If
                                           Else
                                               BeginInvoke(Sub() IPLabel.Text = "內部IP位址：(無)")
                                           End If
                                       Catch ex As Exception
                                           BeginInvoke(Sub() IPLabel.Text = "內部IP位址：(錯誤)")
                                       End Try
                                   End Sub)
        IPSeeker.Name = "Internal IP Seeker"
        IPSeeker.IsBackground = True
        IPSeeker.Start()
    End Sub
End Class