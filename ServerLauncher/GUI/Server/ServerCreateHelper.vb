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
    Dim downloader As ForgeUpdater
    Dim vanilla_isSnap As Boolean = False
    Dim vanilla_isPre As Boolean = False
    Public Sub New(server As Server, serverPath As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        path = serverPath
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
            If preReleaseRegex.IsMatch(server.Server2ndVersion) Then
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

        Select Case server.ServerVersionType
            Case Server.EServerVersionType.Vanilla

                '   My.Computer.Network.DownloadFile(
                'New Uri(String.Format("http://s3.amazonaws.com/Minecraft.Download/versions/{0}/minecraft_server.{0}.jar", server.ServerVersion)),
                'IO.Path.Combine(path, "minecraft_server." & server.ServerVersion & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                Dim URL = GetVanillaServerURL()
                If vanilla_isPre Then
                    DownloadFile(URL, IO.Path.Combine(path, "minecraft_server." & server.Server2ndVersion & ".jar"), Server.EServerVersionType.Vanilla, server.Server2ndVersion)
                ElseIf vanilla_isSnap Then
                    DownloadFile(URL, IO.Path.Combine(path, "minecraft_server." & server.Server2ndVersion & ".jar"), Server.EServerVersionType.Vanilla, server.Server2ndVersion)
                Else
                    DownloadFile(URL, IO.Path.Combine(path, "minecraft_server." & server.ServerVersion & ".jar"), Server.EServerVersionType.Vanilla, server.ServerVersion)
                End If
            Case Server.EServerVersionType.Forge

                downloader = New ForgeUpdater(path, Me)
                Dim craftVersion = server.ServerVersion
                Dim v As New Version(craftVersion)
                BeginInvoke(New Action(Sub()
                                           StatusLabel.Text = "狀態：正在下載 Forge " & craftVersion & "......"
                                           ProgressBar.Value = 0
                                       End Sub))
                Dim forgeVersion = ForgeVersionDict(New Version(craftVersion)).ToString
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
                                                                'Try
                                                                BeginInvoke(Sub()
                                                                                StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                ProgressBar.Value = 90
                                                                            End Sub)

                                                                'downloader.DeleteForgeInstaller(craftVersion, forgeVersion)
                                                                server.SaveServer(False)
                                                                GenerateServerEULA()
                                                                GlobalModule.Manager.BeginInvoke(Sub() GlobalModule.Manager.AddServer(Me.path, True))
                                                                'GlobalModule.Manager.ServerPathList.Add(path)
                                                                BeginInvoke(Sub()
                                                                                StatusLabel.Text = "狀態：完成!"
                                                                                ProgressBar.Value = 100
                                                                                Close()
                                                                            End Sub)
                                                                ' Catch ex As Exception
                                                                'MsgBox(ex.StackTrace)
                                                                ' End Try
                                                            End If

                                                        End Sub

                downloader.DownloadForge(craftVersion, forgeVersion)

            Case Server.EServerVersionType.Spigot
                Dim targetURL As String = SpigotVersionDict(server.ServerVersion)
                Dim url = (New HtmlAgilityPack.HtmlWeb).Load(targetURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[1]/div[1]/div[1]/div[1]/h2[1]/a[1]").GetAttributeValue("href", "")
                'My.Computer.Network.DownloadFile(
                'New Uri(String.Format("https://cdn.getbukkit.org/spigot/spigot-{0}.jar", server.ServerVersion)),
                'IO.Path.Combine(path, "spigot-" & server.ServerVersion & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                DownloadFile(url, IO.Path.Combine(path, "spigot-" & server.ServerVersion & ".jar"), Server.EServerVersionType.Spigot, server.ServerVersion)
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
                                                             watcher.Run(GitBashPath, "--login -i -c """ & IO.Path.Combine(JavaPath, "java.exe") & " -jar BuildTools.jar --rev " & server.ServerVersion & """", path)
                                                             BeginInvoke(Sub()
                                                                             If watcher.ShowDialog(Me) = DialogResult.OK Then
                                                                                 'Try
                                                                                 BeginInvoke(Sub()
                                                                                                 StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                                 ProgressBar.Value = 90
                                                                                             End Sub)

                                                                                 'downloader.DeleteForgeInstaller(craftVersion, forgeVersion)
                                                                                 server.SaveServer(False)
                                                                                 GenerateServerEULA()
                                                                                 GlobalModule.Manager.BeginInvoke(Sub() GlobalModule.Manager.AddServer(Me.path, True))
                                                                                 'GlobalModule.Manager.ServerPathList.Add(path)
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
                client.DownloadFileAsync(New Uri("https://hub.spigotmc.org/jenkins/job/BuildTools/lastSuccessfulBuild/artifact/target/BuildTools.jar"), IO.Path.Combine(path, "BuildTools.jar"))

            Case Server.EServerVersionType.CraftBukkit
                Dim targetURL As String = CraftBukkitVersionDict(server.ServerVersion)
                Dim url = (New HtmlAgilityPack.HtmlWeb).Load(targetURL).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[4]/div[1]/div[1]/div[1]/div[1]/h2[1]/a[1]").GetAttributeValue("href", "")
                ' My.Computer.Network.DownloadFile(
                'New Uri(String.Format("https://cdn.getbukkit.org/craftbukkit/craftbukkit-{0}.jar", server.ServerVersion)),
                'IO.Path.Combine(path, "craftbukkit-" & server.ServerVersion & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                DownloadFile(url, IO.Path.Combine(path, "craftbukkit-" & server.ServerVersion & ".jar"), Server.EServerVersionType.CraftBukkit, server.ServerVersion)
            Case Server.EServerVersionType.SpongeVanilla
                DownloadFile(SpongeVanillaVersionList(server.ServerVersion).GetDownloadUrl, IO.Path.Combine(path, "spongeVanilla-" & server.ServerVersion & ".jar"), Server.EServerVersionType.SpongeVanilla, server.ServerVersion)
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
                DownloadFile(targetURL, IO.Path.Combine(path, "paper-" & server.ServerVersion & ".jar"), Server.EServerVersionType.Paper, server.ServerVersion)
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
                            server.SetVersion(matchString, buildNum)
                            DownloadFile(targetURL, IO.Path.Combine(path, "akarin-" & matchString & ".jar"), Server.EServerVersionType.Akarin, matchString)
                            Exit For
                        End If
                    End If
                Next
            Case Server.EServerVersionType.Nukkit
                DownloadFile(GetNukkitDownloadURL(NukkitVersionUrl), IO.Path.Combine(path, "nukkit-" & server.Server2ndVersion & ".jar"), Server.EServerVersionType.Nukkit, "#" & server.Server2ndVersion)
            Case Server.EServerVersionType.VanillaBedrock
                DownloadFile("https://minecraft.azureedge.net/bin-win/bedrock-server-" & VanillaBedrockVersion.ToString & ".zip", IO.Path.Combine(path, "bedrock-" & VanillaBedrockVersion.ToString & ".zip"), Server.EServerVersionType.VanillaBedrock, server.ServerVersion)
            Case Server.EServerVersionType.Cauldron
                Select Case server.ServerVersion
                    Case "1.7.10"
                        DownloadFile("https://www.dropbox.com/s/flfkgznkmagikrd/server-1.7.10.zip?raw=1", IO.Path.Combine(path, "server-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Cauldron, server.ServerVersion)
                    Case "1.7.2"
                        DownloadFile("https://www.dropbox.com/s/w730znj2y1lskt3/server-1.7.2.zip?raw=1", IO.Path.Combine(path, "server-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Cauldron, server.ServerVersion)
                    Case "1.6.4"
                        DownloadFile("https://www.dropbox.com/s/ey01pirj7fw0fk6/server-1.6.4.zip?raw=1", IO.Path.Combine(path, "server-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Cauldron, server.ServerVersion)
                    Case "1.5.2"
                        DownloadFile("https://www.dropbox.com/s/jediu69o1sgmmcg/server-1.5.2.zip?raw=1", IO.Path.Combine(path, "server-" & server.ServerVersion & ".zip"), Server.EServerVersionType.Cauldron, server.ServerVersion)
                End Select
        End Select
    End Sub

    Sub DownloadFile(path As String, dist As String, versionType As Server.EServerVersionType, version As String)
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
                                                                                          If versionType = Server.EServerVersionType.VanillaBedrock Then
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
                                                         If versionType = Server.EServerVersionType.VanillaBedrock OrElse versionType = Server.EServerVersionType.Cauldron Then
                                                             Invoke(Sub()
                                                                        StatusLabel.Text = "狀態：正在解壓縮伺服器軟體 ......"
                                                                        ProgressBar.Value = 80
                                                                    End Sub)
                                                             Using archive As ZipArchive = ZipFile.OpenRead(dist)
                                                                 For Each entry As ZipArchiveEntry In archive.Entries
                                                                     If entry.FullName.EndsWith("\") OrElse entry.FullName.EndsWith("/") Then
                                                                         If New IO.DirectoryInfo(IO.Path.Combine(Me.path, entry.FullName)).Exists = False Then
                                                                             IO.Directory.CreateDirectory(IO.Path.Combine(Me.path, entry.FullName))
                                                                         End If
                                                                     Else
                                                                         If New IO.FileInfo(IO.Path.Combine(Me.path, entry.FullName)).Directory.Exists = False Then
                                                                             Dim info = New IO.FileInfo(IO.Path.Combine(Me.path, entry.FullName))
                                                                             info.Directory.Create()
                                                                             entry.ExtractToFile(IO.Path.Combine(Me.path, entry.FullName), True)
                                                                         End If
                                                                     End If
                                                                 Next
                                                             End Using
                                                         End If
                                                         BeginInvoke(New Action(Sub()
                                                                                    StatusLabel.Text = "狀態：正在配置伺服器 ......"
                                                                                    server.SaveServer(False)
                                                                                    GenerateServerEULA()
                                                                                    StatusLabel.Text = "狀態：完成!"
                                                                                    ProgressBar.Value = 100
                                                                                    GlobalModule.Manager.AddServer(Me.path, True)
                                                                                    'GlobalModule.Manager.ServerPathList.Add(Me.path)
                                                                                    Me.Close()
                                                                                End Sub))
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
