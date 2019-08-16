Imports System.IO.Compression
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class ServerCreateHelper
    Dim server As Server
    Dim client As New Net.WebClient()
    Dim path As String
    Dim forgeVer As String
    Dim downloader As ForgeUpdater
    Dim vanilla_isSnap As Boolean = False
    Dim vanilla_isPre As Boolean = False
    Dim justDownload As Boolean
    Public Sub New(server As Server, serverPath As String, Optional justDownload As Boolean = False)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        path = serverPath
        Me.justDownload = justDownload
    End Sub
    Public Sub New(server As Server, serverPath As String, targetForgeVersion As String, Optional justDownload As Boolean = False)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        path = serverPath
        forgeVer = targetForgeVersion
        Me.justDownload = justDownload
    End Sub
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        If downloader IsNot Nothing Then downloader.ForceClose()
        client.CancelAsync()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub ServerCreateHelper_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Computer.FileSystem.DirectoryExists(path) = False Then
            BeginInvoke(New Action(Sub()
                                       StatusLabel.Text = "狀態：建立目錄中……"
                                       ProgressBar.Value = 0
                                   End Sub))
            My.Computer.FileSystem.CreateDirectory(path)



        End If
        Task.Run(New Action(Sub()
                                DownloadServer()
                            End Sub))





    End Sub
    Private Function GetVanillaServerURL() As String
        Dim manifestListURL As String = ""
        If IsNothing(server.Server2ndVersion) = False OrElse server.Server2ndVersion <> "" Then
            Dim preReleaseRegex As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]*-[Pp]{1}re[0-9]{1,2}")
            Dim preReleaseRegex2 As New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*")
            Dim preReleaseRegex3 As New Regex("[0-9A-Za-z]{1,2}.[0-9A-Za-z]{1,2}[.]*[0-9]* [Pp]{1}re-[Rr]{1}elease [0-9]{1,2}")
            If preReleaseRegex.IsMatch(server.Server2ndVersion) Then
                manifestListURL = VanillaVersionDict(server.Server2ndVersion)
                vanilla_isPre = True
            ElseIf preReleaseRegex3.IsMatch(server.Server2ndVersion) Then
                manifestListURL = VanillaVersionDict(server.Server2ndVersion)
                vanilla_isPre = True
            ElseIf server.ServerVersion = "snapshot" Then
                manifestListURL = VanillaVersionDict(server.Server2ndVersion)
                vanilla_isSnap = True
            ElseIf preReleaseRegex2.IsMatch(server.Server2ndVersion) Then
                manifestListURL = VanillaVersionDict(server.Server2ndVersion)
                vanilla_isPre = True
            Else
                manifestListURL = VanillaVersionDict(server.ServerVersion)
            End If
        Else
            manifestListURL = VanillaVersionDict(server.ServerVersion)
        End If
        If manifestListURL <> "" Then
            Dim client As New Net.WebClient()
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(client.DownloadString(manifestListURL))
            If vanilla_isPre OrElse vanilla_isSnap Then
                Dim assets As String = jsonObject.GetValue("assets").ToString
                assets = New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(assets).Value
                server.SetVersion(assets, server.Server2ndVersion)
            End If
            Return jsonObject.GetValue("downloads").Item("server").Item("url").ToString
        End If
    End Function



    Sub DownloadServer()
        Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
        Select Case server.ServerVersionType
            Case Server.EServerVersionType.Vanilla

                '   My.Computer.Network.DownloadFile(
                'New Uri(String.Format("http://s3.amazonaws.com/Minecraft.Download/versions/{0}/minecraft_server.{0}.jar", server.ServerVersion)),
                'IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "minecraft_server." & server.ServerVersion & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                Dim URL = GetVanillaServerURL()
                If vanilla_isPre Then
                    DownloadFile(URL, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "minecraft_server." & server.Server2ndVersion & ".jar"), Server.EServerVersionType.Vanilla, server.Server2ndVersion)
                ElseIf vanilla_isSnap Then
                    DownloadFile(URL, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "minecraft_server." & server.Server2ndVersion & ".jar"), Server.EServerVersionType.Vanilla, server.Server2ndVersion)
                Else
                    DownloadFile(URL, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "minecraft_server." & server.ServerVersion & ".jar"), Server.EServerVersionType.Vanilla, server.ServerVersion)
                End If
            Case Server.EServerVersionType.Forge

                downloader = New ForgeUpdater(IIf(path.EndsWith(seperator), path, path & seperator), Me)
                Dim craftVersion = server.ServerVersion
                Dim v As New Version(craftVersion)
                BeginInvoke(New Action(Sub()
                                           StatusLabel.Text = "狀態：正在下載 Forge " & craftVersion & "......"
                                           ProgressBar.Value = 0
                                       End Sub))
                Dim forgeVersion As String
                If forgeVer <> "" Then
                    forgeVersion = forgeVer
                Else
                    forgeVersion = ForgeVersionDict(New Version(craftVersion)).ToString
                End If
                AddHandler downloader.ForgeDownloadStart, Sub()
                                                              Console.WriteLine("forge download start")
                                                          End Sub
                AddHandler downloader.ForgeDownloadProgressChanged, Sub(e)
                                                                        Try
                                                                            BeginInvoke(New Action(Sub()
                                                                                                       If e.TotalBytesToReceive = -1 Then
                                                                                                           ProgressBar.Style = ProgressBarStyle.Marquee
                                                                                                           ProgressBar.Value = 10
                                                                                                       Else
                                                                                                           If New Version(craftVersion) > New Version(1, 5, 1) Then
                                                                                                               ProgressBar.Value = e.ProgressPercentage * 0.5
                                                                                                           Else
                                                                                                               ProgressBar.Value = e.ProgressPercentage
                                                                                                           End If
                                                                                                       End If
                                                                                                   End Sub))
                                                                        Catch ex As Exception
                                                                        End Try
                                                                    End Sub
                AddHandler downloader.ForgeDownloadEnd, Sub()
                                                            downloader.DisposeClient()
                                                            BeginInvoke(Sub() Cancel_Button.Enabled = False)
                                                            If New Version(craftVersion) > New Version(1, 5, 1) Then
                                                                BeginInvoke(New Action(Sub()
                                                                                           StatusLabel.Text = "狀態：正在安裝 Forge " & craftVersion & "......"
                                                                                           ProgressBar.Value = 50
                                                                                       End Sub))
                                                            End If
                                                            If downloader.InstallForge(craftVersion, forgeVersion) = DialogResult.OK Then
                                                                If justDownload Then
                                                                    BeginInvoke(Sub()
                                                                                    StatusLabel.Text = "狀態：完成!"
                                                                                    ProgressBar.Value = 100
                                                                                    Close()
                                                                                End Sub)
                                                                Else
                                                                    'Try
                                                                    BeginInvoke(Sub()
                                                                                    StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                    ProgressBar.Value = 90
                                                                                End Sub)

                                                                    'downloader.DeleteForgeInstaller(craftVersion, forgeVersion)
                                                                    server.SetVersion(craftVersion, forgeVersion)
                                                                    server.SaveServer(False)
                                                                    GenerateServerEULA()
                                                                    BeginInvokeIfRequired(GlobalModule.Manager, Sub() GlobalModule.Manager.AddServer(IIf(path.EndsWith(seperator), path, path & seperator)))
                                                                    'GlobalModule.Manager.ServerPathList.Add(IIf(path.EndsWith(seperator), path, path & seperator))
                                                                    BeginInvoke(Sub()
                                                                                    StatusLabel.Text = "狀態：完成!"
                                                                                    ProgressBar.Value = 100
                                                                                    Close()
                                                                                End Sub)
                                                                    ' Catch ex As Exception
                                                                    'MsgBox(ex.StackTrace)
                                                                    ' End Try
                                                                End If
                                                            End If

                                                        End Sub

                downloader.DownloadForge(craftVersion, forgeVersion)

            Case Server.EServerVersionType.Spigot
                Dim targetURL As String = SpigotVersionDict(server.ServerVersion)
                Dim url = (New HtmlAgilityPack.HtmlWeb).Load(targetURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[1]/div[1]/div[1]/div[1]/h2[1]/a[1]").GetAttributeValue("href", "")
                'My.Computer.Network.DownloadFile(
                'New Uri(String.Format("https://cdn.getbukkit.org/spigot/spigot-{0}.jar", server.ServerVersion)),
                'IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "spigot-" & server.ServerVersion & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                DownloadFile(url, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "spigot-" & server.ServerVersion & ".jar"), Server.EServerVersionType.Spigot, server.ServerVersion)
            Case Server.EServerVersionType.Spigot_Git
                BeginInvoke(New Action(Sub()
                                           StatusLabel.Text = "狀態：正在下載 Spigot 建置工具 " & "......"
                                           ProgressBar.Value = 0
                                       End Sub))
                Dim client As New Net.WebClient()
                AddHandler client.DownloadProgressChanged, Sub(obj, args)
                                                               Try
                                                                   BeginInvoke(New Action(Sub()
                                                                                              If args.TotalBytesToReceive = -1 Then
                                                                                                  ProgressBar.Style = ProgressBarStyle.Marquee
                                                                                                  ProgressBar.Value = 10
                                                                                              Else
                                                                                                  ProgressBar.Value = args.ProgressPercentage * 0.5
                                                                                              End If
                                                                                          End Sub))
                                                               Catch ex As Exception
                                                               End Try
                                                           End Sub
                AddHandler client.DownloadFileCompleted, Sub()
                                                             client.Dispose()
                                                             BeginInvoke(Sub() Cancel_Button.Enabled = False)
                                                             Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(New Net.WebClient().DownloadString("https://hub.spigotmc.org/versions/" & server.ServerVersion & ".json"))
                                                             server.SetVersion(server.ServerVersion, jsonObject.GetValue("name"))
                                                             BeginInvoke(New Action(Sub()
                                                                                        StatusLabel.Text = "狀態：正在建置 Spigot " & server.ServerVersion & "......"
                                                                                        ProgressBar.Value = 50
                                                                                    End Sub))
                                                             Dim watcher As New SpigotGitBuildWindow()
                                                             If IsUnixLikeSystem Then
                                                                 Shell("git config --global --unset core.autocrlf", AppWinStyle.MinimizedNoFocus, True, 5000)
                                                                 watcher.Run(GetJavaPath(), "-jar BuildTools.jar --rev " & server.ServerVersion & """", IIf(path.EndsWith(seperator), path, path & seperator))
                                                             Else
                                                                 watcher.Run(GitBashPath, "--login -i -c """ & GetJavaPath() & " -jar BuildTools.jar --rev " & server.ServerVersion & """", IIf(path.EndsWith(seperator), path, path & seperator))
                                                             End If
                                                             BeginInvoke(Sub()
                                                                             If watcher.ShowDialog(Me) = DialogResult.OK Then
                                                                                 If justDownload Then
                                                                                     BeginInvoke(Sub()
                                                                                                     StatusLabel.Text = "狀態：完成!"
                                                                                                     ProgressBar.Value = 100
                                                                                                     Close()
                                                                                                 End Sub)
                                                                                 Else
                                                                                     'Try
                                                                                     BeginInvoke(Sub()
                                                                                                     StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                                     ProgressBar.Value = 90
                                                                                                 End Sub)

                                                                                     'downloader.DeleteForgeInstaller(craftVersion, forgeVersion)
                                                                                     server.SaveServer(False)
                                                                                     GenerateServerEULA()
                                                                                     BeginInvokeIfRequired(GlobalModule.Manager, Sub() GlobalModule.Manager.AddServer(IIf(path.EndsWith(seperator), path, path & seperator)))
                                                                                     'GlobalModule.Manager.ServerPathList.Add(IIf(path.EndsWith(seperator), path, path & seperator))
                                                                                     BeginInvoke(Sub()
                                                                                                     StatusLabel.Text = "狀態：完成!"
                                                                                                     ProgressBar.Value = 100
                                                                                                     Close()
                                                                                                 End Sub)
                                                                                     ' Catch ex As Exception
                                                                                     'MsgBox(ex.StackTrace)
                                                                                     ' End Try
                                                                                 End If
                                                                             End If
                                                                         End Sub)
                                                         End Sub
                client.DownloadFileAsync(New Uri("https://hub.spigotmc.org/jenkins/job/BuildTools/lastSuccessfulBuild/artifact/target/BuildTools.jar"), IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "BuildTools.jar"))

            Case Server.EServerVersionType.CraftBukkit
                Dim targetURL As String = CraftBukkitVersionDict(server.ServerVersion)
                Dim url = (New HtmlAgilityPack.HtmlWeb).Load(targetURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[1]/div[1]/div[1]/div[1]/h2[1]/a[1]").GetAttributeValue("href", "")
                ' My.Computer.Network.DownloadFile(
                'New Uri(String.Format("https://cdn.getbukkit.org/craftbukkit/craftbukkit-{0}.jar", server.ServerVersion)),
                'IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "craftbukkit-" & server.ServerVersion & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                DownloadFile(url, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "craftbukkit-" & server.ServerVersion & ".jar"), Server.EServerVersionType.CraftBukkit, server.ServerVersion)
            Case Server.EServerVersionType.SpongeVanilla
                DownloadFile(SpongeVanillaVersionList(server.ServerVersion).GetDownloadUrl, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "spongeVanilla-" & server.ServerVersion & ".jar"), Server.EServerVersionType.SpongeVanilla, server.ServerVersion)
            Case Server.EServerVersionType.Paper
                BeginInvoke(New Action(Sub()
                                           StatusLabel.Text = "狀態：正在擷取安裝檔案 ……"
                                       End Sub))
                Dim subClient As New Net.WebClient
                Dim subDocHtml = subClient.DownloadString(PaperVersionDict(Version.Parse(server.ServerVersion)))
                Dim subJsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(subDocHtml)
                Dim subJsonToken As JToken = CType(subJsonObject.GetValue("builds"), JObject).GetValue("latest")
                server.SetVersion(server.ServerVersion, subJsonToken)
                Dim targetURL As String = String.Format("https://papermc.io/api/v1/paper/{0}/{1}/download", server.ServerVersion, subJsonToken)
                'Dim request As Net.HttpWebRequest = Net.WebRequest.Create(targetURL)
                'Dim OSVersion = System.Environment.OSVersion.Version
                'request.UserAgent = "Mozilla/5.0 (Windows NT " & OSVersion.Major & "." & OSVersion.Minor & ") ServerManager/" & Application.ProductVersion
                DownloadFile(targetURL, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "paper-" & server.ServerVersion & ".jar"), Server.EServerVersionType.Paper, server.ServerVersion)
            Case Server.EServerVersionType.Akarin
                BeginInvoke(New Action(Sub()
                                           StatusLabel.Text = "狀態：正在擷取安裝檔案 ……"
                                       End Sub))
                Dim subClient As New Net.WebClient
                subClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/json")
                Dim downloadURL As String
                If server.ServerVersion = "master" Then
                    downloadURL = "https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/tree/master" & "?filter=%22successful%22&limit=1"
                Else
                    downloadURL = "https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/tree/ver/" & server.ServerVersion & "?filter=%22successful%22&limit=1"
                    server.SetVersion("ver/" & server.ServerVersion)
                End If
                Dim subDocHtml = subClient.DownloadString(downloadURL)
                Dim subJsonObject As JObject = JsonConvert.DeserializeObject(Of JArray)(subDocHtml)(0)
                Dim buildNum As Integer = subJsonObject.GetValue("build_num")
                subClient.Headers.Add(Net.HttpRequestHeader.Accept, "application/json")
                Dim anotherDocHTML = subClient.DownloadString("https://circleci.com/api/v1.1/project/github/Akarin-project/Akarin/" & buildNum & "/artifacts")
                Dim regex As New Regex("akarin-[0-9].[0-9]{1,2}.[0-9]{1,2}.jar")
                For Each anotherJSONObject As JObject In JsonConvert.DeserializeObject(Of JArray)(anotherDocHTML)
                    Dim targetURL As String = anotherJSONObject.GetValue("url")
                    If regex.IsMatch(targetURL) Then
                        Dim matchString As String = regex.Match(targetURL).Value
                        matchString = matchString.Remove(0, 7)
                        matchString = matchString.Substring(0, matchString.Length - 4)
                        If Version.TryParse(matchString, Nothing) Then
                            server.SetVersion(matchString, buildNum, server.ServerVersion)
                            DownloadFile(targetURL, IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "akarin-" & matchString & ".jar"), Server.EServerVersionType.Akarin, matchString)
                            Exit For
                        End If
                    End If
                Next
            Case Server.EServerVersionType.Nukkit
                DownloadFile(GetNukkitDownloadURL(NukkitVersionUrl), IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "nukkit-" & server.Server2ndVersion & ".jar"), Server.EServerVersionType.Nukkit, "#" & server.Server2ndVersion)
            Case Server.EServerVersionType.VanillaBedrock
                DownloadFile("https://minecraft.azureedge.net/bin-win/bedrock-server-" & VanillaBedrockVersion.ToString & ".zip", IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "bedrock-" & VanillaBedrockVersion.ToString & ".zip"), Server.EServerVersionType.VanillaBedrock, server.ServerVersion)
            Case Server.EServerVersionType.Cauldron
                Select Case server.ServerVersion
                    Case "1.7.10"
                        DownloadFile("https://www.dropbox.com/s/flfkgznkmagikrd/server-1.7.10.zip?raw=1", IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "server-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Cauldron, server.ServerVersion)
                    Case "1.7.2"
                        DownloadFile("https://www.dropbox.com/s/w730znj2y1lskt3/server-1.7.2.zip?raw=1", IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "server-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Cauldron, server.ServerVersion)
                    Case "1.6.4"
                        DownloadFile("https://www.dropbox.com/s/ey01pirj7fw0fk6/server-1.6.4.zip?raw=1", IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "server-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Cauldron, server.ServerVersion)
                    Case "1.5.2"
                        DownloadFile("https://www.dropbox.com/s/jediu69o1sgmmcg/server-1.5.2.zip?raw=1", IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "server-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Cauldron, server.ServerVersion)
                End Select
            Case Server.EServerVersionType.Thermos
                DownloadFile("https://www.dropbox.com/s/zgo0fmbm0kfkjlp/Thermos.zip?raw=1", IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "thermos-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Thermos, server.ServerVersion)
            Case Server.EServerVersionType.Contigo
                DownloadFile("https://www.dropbox.com/s/2l136lm1eesmo57/Contigo-1.7.10.zip?raw=1", IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "contigo-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Contigo, server.ServerVersion)
            Case Server.EServerVersionType.Kettle
                BeginInvoke(New Action(Sub()
                                           StatusLabel.Text = "狀態：正在下載 Kettle " & server.ServerVersion & "......"
                                           ProgressBar.Value = 0
                                       End Sub))
                Dim client As New Net.WebClient()
                AddHandler client.DownloadProgressChanged, Sub(obj, args)
                                                               Try
                                                                   BeginInvoke(New Action(Sub()
                                                                                              If args.TotalBytesToReceive = -1 Then
                                                                                                  ProgressBar.Style = ProgressBarStyle.Marquee
                                                                                                  ProgressBar.Value = 10
                                                                                              Else
                                                                                                  ProgressBar.Value = args.ProgressPercentage * 0.2
                                                                                              End If
                                                                                          End Sub))
                                                               Catch ex As Exception
                                                               End Try
                                                           End Sub
                AddHandler client.DownloadFileCompleted, Sub()
                                                             client.Dispose()
                                                             Dim vanillaClient As New Net.WebClient()
                                                             AddHandler vanillaClient.DownloadProgressChanged, Sub(obj, args)
                                                                                                                   Try
                                                                                                                       BeginInvoke(New Action(Sub()
                                                                                                                                                  If args.TotalBytesToReceive = -1 Then
                                                                                                                                                      ProgressBar.Style = ProgressBarStyle.Marquee
                                                                                                                                                      ProgressBar.Value = 30
                                                                                                                                                  Else
                                                                                                                                                      ProgressBar.Value = 20 + args.ProgressPercentage * 0.2
                                                                                                                                                  End If
                                                                                                                                              End Sub))
                                                                                                                   Catch ex As Exception
                                                                                                                   End Try
                                                                                                               End Sub
                                                             AddHandler vanillaClient.DownloadFileCompleted, Sub()
                                                                                                                 vanillaClient.Dispose()
                                                                                                                 BeginInvoke(Sub()
                                                                                                                                 Cancel_Button.Enabled = False
                                                                                                                                 StatusLabel.Text = "狀態：正在下載必需程式庫......"
                                                                                                                                 ProgressBar.Value = 40
                                                                                                                             End Sub)
                                                                                                                 For i As Integer = KettleVersionDict.Keys.ToList.IndexOf(server.ServerVersion) To KettleVersionDict.Count - 1
                                                                                                                     If String.IsNullOrEmpty(KettleVersionDict.Values.ToArray(i).Item3) = False Then
                                                                                                                         Dim subClient As New Net.WebClient
                                                                                                                         AddHandler subClient.DownloadProgressChanged, Sub(obj, args)
                                                                                                                                                                           If args.TotalBytesToReceive = -1 Then
                                                                                                                                                                               ProgressBar.Style = ProgressBarStyle.Marquee
                                                                                                                                                                               ProgressBar.Value = 50
                                                                                                                                                                           Else
                                                                                                                                                                               BeginInvokeIfRequired(Me, Sub() ProgressBar.Value = 40 + args.ProgressPercentage * 0.3)
                                                                                                                                                                           End If
                                                                                                                                                                       End Sub
                                                                                                                         AddHandler subClient.DownloadFileCompleted, Sub()
                                                                                                                                                                         subClient.Dispose()
                                                                                                                                                                         BeginInvoke(Sub()
                                                                                                                                                                                         StatusLabel.Text = "狀態：正在解壓縮必需程式庫 ......"
                                                                                                                                                                                         ProgressBar.Value = 70
                                                                                                                                                                                     End Sub)
                                                                                                                                                                         If IO.File.Exists(IIf(path.EndsWith(seperator), path, path & seperator) & "libraries.zip") Then
                                                                                                                                                                             Using archive As ZipArchive = ZipFile.OpenRead(IIf(path.EndsWith(seperator), path, path & seperator) & "libraries.zip")
                                                                                                                                                                                 Dim s As Integer = 0
                                                                                                                                                                                 For Each entry As ZipArchiveEntry In archive.Entries
                                                                                                                                                                                     s += 1
                                                                                                                                                                                     BeginInvokeIfRequired(Me, Sub() ProgressBar.Increment(s / archive.Entries.Count * 100 * 0.2))
                                                                                                                                                                                     Try
                                                                                                                                                                                         If entry.FullName.EndsWith("\") OrElse entry.FullName.EndsWith("/") Then
                                                                                                                                                                                             If New IO.DirectoryInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Exists = False Then
                                                                                                                                                                                                 IO.Directory.CreateDirectory(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                                                                                                                                             End If
                                                                                                                                                                                         Else
                                                                                                                                                                                             If New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Directory.Exists = False Then
                                                                                                                                                                                                 Dim info = New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                                                                                                                                                 info.Directory.Create()
                                                                                                                                                                                             End If
                                                                                                                                                                                             If New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Exists = False Then
                                                                                                                                                                                                 Dim info = New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                                                                                                                                                 info.Delete()
                                                                                                                                                                                             End If
                                                                                                                                                                                             entry.ExtractToFile(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName), True)
                                                                                                                                                                                         End If
                                                                                                                                                                                     Catch ex As Exception
                                                                                                                                                                                     End Try
                                                                                                                                                                                 Next
                                                                                                                                                                             End Using
                                                                                                                                                                         End If
                                                                                                                                                                         BeginInvoke(Sub()
                                                                                                                                                                                         If justDownload Then
                                                                                                                                                                                             BeginInvoke(Sub()
                                                                                                                                                                                                             StatusLabel.Text = "狀態：完成!"
                                                                                                                                                                                                             ProgressBar.Value = 100
                                                                                                                                                                                                             Close()
                                                                                                                                                                                                         End Sub)
                                                                                                                                                                                         Else
                                                                                                                                                                                             'Try
                                                                                                                                                                                             BeginInvoke(Sub()
                                                                                                                                                                                                             StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                                                                                                                                             ProgressBar.Value = 90
                                                                                                                                                                                                         End Sub)

                                                                                                                                                                                             'downloader.DeleteForgeInstaller(craftVersion, forgeVersion)
                                                                                                                                                                                             server.SaveServer(False)
                                                                                                                                                                                             GenerateServerEULA()
                                                                                                                                                                                             BeginInvokeIfRequired(GlobalModule.Manager, Sub() GlobalModule.Manager.AddServer(IIf(path.EndsWith(seperator), path, path & seperator)))
                                                                                                                                                                                             'GlobalModule.Manager.ServerPathList.Add(IIf(path.EndsWith(seperator), path, path & seperator))
                                                                                                                                                                                             BeginInvoke(Sub()
                                                                                                                                                                                                             StatusLabel.Text = "狀態：完成!"
                                                                                                                                                                                                             ProgressBar.Value = 100
                                                                                                                                                                                                             Close()
                                                                                                                                                                                                         End Sub)
                                                                                                                                                                                             ' Catch ex As Exception
                                                                                                                                                                                             'MsgBox(ex.StackTrace)
                                                                                                                                                                                             ' End Try
                                                                                                                                                                                         End If
                                                                                                                                                                                     End Sub)
                                                                                                                                                                     End Sub
                                                                                                                         subClient.DownloadFileAsync(New Uri(KettleVersionDict.Values.ToArray(i).Item3), IIf(path.EndsWith(seperator), path, path & seperator) & "libraries.zip")
                                                                                                                         Exit For
                                                                                                                     End If
                                                                                                                 Next
                                                                                                             End Sub
                                                             Dim jsonClient As New Net.WebClient()
                                                             Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(jsonClient.DownloadString(VanillaVersionDict("1.12.2")))
                                                             If vanilla_isPre OrElse vanilla_isSnap Then
                                                                 Dim assets As String = jsonObject.GetValue("assets").ToString
                                                                 assets = New Regex("[0-9]{1,2}.[0-9]{1,2}[.]*[0-9]*").Match(assets).Value
                                                                 server.SetVersion(assets, server.Server2ndVersion)
                                                             End If
                                                             vanillaClient.DownloadFileAsync(New Uri(jsonObject.GetValue("downloads").Item("server").Item("url").ToString), IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "minecraft_server.1.12.2.jar"))
                                                         End Sub
                Dim branchID As String = New Regex("[0-9a-f]{7}", RegexOptions.IgnoreCase).Match(KettleVersionDict(server.ServerVersion).Item2).Value
                server.SetVersion(server.ServerVersion, branchID)
                client.DownloadFileAsync(New Uri(KettleVersionDict(server.ServerVersion).Item1), IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), KettleVersionDict(server.ServerVersion).Item2))
            Case Server.EServerVersionType.PocketMine
                DownloadFile(PocketMineVersionDict(server.ServerVersion), IO.Path.Combine(IIf(path.EndsWith(seperator), path, path & seperator), "PocketMine-MP.phar"), Server.EServerVersionType.PocketMine, server.ServerVersion)
        End Select
    End Sub

    Sub DownloadFile(path As String, dist As String, versionType As Server.EServerVersionType, version As String)
        Dim seperator As String = IIf(IsUnixLikeSystem, "/", "\")
        BeginInvoke(New Action(Sub()
                                   StatusLabel.Text = "狀態：正在下載 " & GetSimpleVersionName(versionType, version) & " " & version & " ……"
                               End Sub))
        client.DownloadFileAsync(New Uri(path), dist)
        AddHandler client.DownloadProgressChanged, Sub(sender, e)
                                                       Try
                                                           BeginInvoke(New Action(Sub()
                                                                                      If e.TotalBytesToReceive = -1 Then
                                                                                          ProgressBar.Style = ProgressBarStyle.Marquee
                                                                                          ProgressBar.Value = 10
                                                                                      Else
                                                                                          If versionType = Server.EServerVersionType.VanillaBedrock OrElse versionType = Server.EServerVersionType.Cauldron OrElse versionType = Server.EServerVersionType.Thermos Then
                                                                                              ProgressBar.Value = e.ProgressPercentage * 0.8
                                                                                          Else
                                                                                              ProgressBar.Value = e.ProgressPercentage
                                                                                          End If
                                                                                      End If
                                                                                  End Sub))
                                                       Catch ex As Exception
                                                       End Try
                                                   End Sub
        AddHandler client.DownloadFileCompleted, Sub(sender, e)
                                                     If e.Cancelled = False Then
                                                         If versionType = Server.EServerVersionType.VanillaBedrock OrElse
                                                         versionType = Server.EServerVersionType.Cauldron OrElse
                                                         versionType = Server.EServerVersionType.Thermos OrElse
                                                         versionType = Server.EServerVersionType.Contigo Then
                                                             Invoke(Sub()
                                                                        StatusLabel.Text = "狀態：正在解壓縮伺服器軟體 ......"
                                                                        ProgressBar.Value = 80
                                                                    End Sub)
                                                             Using archive As ZipArchive = ZipFile.OpenRead(dist)
                                                                 Dim i As Integer = 0
                                                                 For Each entry As ZipArchiveEntry In archive.Entries
                                                                     i += 1
                                                                     BeginInvokeIfRequired(Me, Sub() ProgressBar.Increment(i / archive.Entries.Count * 100 * 0.2))
                                                                     Try
                                                                         If entry.FullName.EndsWith("\") OrElse entry.FullName.EndsWith("/") Then
                                                                             If New IO.DirectoryInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Exists = False Then
                                                                                 IO.Directory.CreateDirectory(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                             End If
                                                                         Else
                                                                             If New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Directory.Exists = False Then
                                                                                 Dim info = New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                                 info.Directory.Create()
                                                                             End If
                                                                             If New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName)).Exists = False Then
                                                                                 Dim info = New IO.FileInfo(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName))
                                                                                 info.Delete()
                                                                             End If
                                                                             entry.ExtractToFile(IO.Path.Combine(IIf(Me.path.EndsWith(seperator), Me.path, Me.path & seperator), entry.FullName), True)
                                                                         End If
                                                                     Catch ex As Exception
                                                                     End Try
                                                                 Next
                                                             End Using
                                                         End If
                                                         If justDownload Then
                                                             BeginInvoke(Sub()
                                                                             StatusLabel.Text = "狀態：完成!"
                                                                             ProgressBar.Value = 100
                                                                             Close()
                                                                         End Sub)
                                                         Else
                                                             BeginInvoke(New Action(Sub()
                                                                                        StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                        server.SaveServer(False)
                                                                                        GenerateServerEULA()
                                                                                        StatusLabel.Text = "狀態：完成!"
                                                                                        ProgressBar.Value = 100
                                                                                        GlobalModule.Manager.AddServer(Me.path)
                                                                                        'GlobalModule.Manager.ServerPathList.Add(Me.path)
                                                                                        Me.Close()
                                                                                    End Sub))
                                                         End If
                                                     End If
                                                 End Sub
    End Sub
    Sub GenerateServerEULA()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(path, "eula.txt"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(path, "eula.txt"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.WriteLine("#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).")
            writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy"), System.Globalization.CultureInfo.CurrentUICulture)
            writer.WriteLine("eula=true")
            writer.Flush()
            writer.Close()
        End Using
    End Sub
End Class
