Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Friend Class JavaServerCreator
    Friend _owner As CreateJavaServerDialog
    Private serverDir As String

    Private Sub ServerCreator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Computer.FileSystem.DirectoryExists(_owner.ServerDirBox.Text) = False Then
            BeginInvoke(New Action(Sub()
                                       ProgressText.Text = "建立目錄中……"
                                       Progress.Value = 0
                                   End Sub))
            My.Computer.FileSystem.CreateDirectory(_owner.ServerDirBox.Text)



        End If
        Task.Factory.StartNew(New Action(Sub()
                                             DownloadServer()
                                         End Sub))





    End Sub
    Sub New(owner As CreateJavaServerDialog, serverDir As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _owner = owner
        Me.serverDir = serverDir
    End Sub
    Private Function GetVanillaServerURL() As String
        Dim manifestListURL As String = _owner._owner.VanillaManifestURLs(_owner.VersionBox.Text)
        Dim client As New Net.WebClient()
        Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(client.DownloadString(manifestListURL))
        Return jsonObject.GetValue("downloads").Item("server").Item("url").ToString
    End Function


    Sub DownloadServer()

        Select Case _owner.ServerTypeBox.SelectedIndex
            Case 0 'Vanilla

                '   My.Computer.Network.DownloadFile(
                'New Uri(String.Format("http://s3.amazonaws.com/Minecraft.Download/versions/{0}/minecraft_server.{0}.jar", _owner.VersionBox.Text)),
                'IO.Path.Combine(_owner.ServerDirBox.Text, "minecraft_server." & _owner.VersionBox.Text & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                DownloadFile(GetVanillaServerURL(), IO.Path.Combine(_owner.ServerDirBox.Text, "minecraft_server." & _owner.VersionBox.Text & ".jar"))
            Case 1 'Forge

                Dim downloader As New ForgeUtil(_owner.ServerDirBox.Text, Nothing)
                Dim craftVersion = _owner.VersionBox.Text
                Dim v As New Version(craftVersion)
                If v > New Version(1, 5, 1) Then
                    BeginInvoke(New Action(Sub()
                                               ProgressText.Text = "下載 Forge 安裝程式中……"
                                               Progress.Value = 0
                                           End Sub))
                Else
                    BeginInvoke(New Action(Sub()
                                               ProgressText.Text = "下載 Forge 主程式中……"
                                               Progress.Value = 0
                                           End Sub))
                End If
                Dim forgeVersion = ForgeNewestBranchVersionList(craftVersion)
                AddHandler downloader.ForgeDownloadStart, Sub()
                                                              Console.WriteLine("forge download start")
                                                          End Sub
                AddHandler downloader.ForgeDownloadProgressChanged, Sub(e)
                                                                        BeginInvoke(New Action(Sub()
                                                                                                   If New Version(craftVersion) > New Version(1, 5, 1) Then
                                                                                                       Progress.Value = e.ProgressPercentage * 0.5
                                                                                                   Else
                                                                                                       Progress.Value = e.ProgressPercentage
                                                                                                   End If
                                                                                               End Sub))
                                                                    End Sub
                AddHandler downloader.ForgeDownloadEnd, Sub()
                                                            If New Version(craftVersion) > New Version(1, 5, 1) Then
                                                                BeginInvoke(New Action(Sub()
                                                                                           ProgressText.Text = "正在安裝 Forge ......"
                                                                                           Progress.Value = 50
                                                                                       End Sub))
                                                            End If
                                                            If downloader.InstallForge(craftVersion, forgeVersion) = DialogResult.OK Then
                                                                Try
                                                                    ProgressText.Text = "正在配置 Forge ......"
                                                                    Progress.Value = 90
                                                                    downloader.DeleteForgeInstaller(craftVersion, forgeVersion)
                                                                    GenerateServerProperties()
                                                                    GenerateServerInfo()
                                                                    _owner._owner.RegisterJavaServer(serverDir)
                                                                    _owner._owner.AddJavaServer(serverDir)
                                                                    _owner._owner.servers.Add(serverDir)

                                                                    ProgressText.Text = "完成!"
                                                                    Progress.Value = 100
                                                                Catch ex As Exception
                                                                    'MsgBox(ex.StackTrace)
                                                                End Try
                                                            End If

                                                        End Sub

                downloader.DownloadForge(craftVersion, forgeVersion)

            Case 2 'Spigot

                'My.Computer.Network.DownloadFile(
                'New Uri(String.Format("https://cdn.getbukkit.org/spigot/spigot-{0}.jar", _owner.VersionBox.Text)),
                'IO.Path.Combine(_owner.ServerDirBox.Text, "spigot-" & _owner.VersionBox.Text & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                DownloadFile(String.Format("https://cdn.getbukkit.org/spigot/spigot-{0}.jar", _owner.VersionBox.Text), IO.Path.Combine(_owner.ServerDirBox.Text, "spigot-" & _owner.VersionBox.Text & ".jar"))
            Case 3 'CraftBukkit

                ' My.Computer.Network.DownloadFile(
                'New Uri(String.Format("https://cdn.getbukkit.org/craftbukkit/craftbukkit-{0}.jar", _owner.VersionBox.Text)),
                'IO.Path.Combine(_owner.ServerDirBox.Text, "craftbukkit-" & _owner.VersionBox.Text & ".jar"), "", "", True, 100000, True, FileIO.UICancelOption.DoNothing)
                DownloadFile(String.Format("https://cdn.getbukkit.org/craftbukkit/craftbukkit-{0}.jar", _owner.VersionBox.Text), IO.Path.Combine(_owner.ServerDirBox.Text, "craftbukkit-" & _owner.VersionBox.Text & ".jar"))
        End Select
    End Sub

    Sub DownloadFile(path As String, dist As String)

        BeginInvoke(New Action(Sub()
                                   ProgressText.Text = "下載伺服器主程式中……"
                               End Sub))
        Dim client As New Net.WebClient()
        client.DownloadFileAsync(New Uri(path), dist)
        AddHandler client.DownloadProgressChanged, Sub(sender, e)
                                                       BeginInvoke(New Action(Sub()
                                                                                  Progress.Value = e.ProgressPercentage
                                                                              End Sub))
                                                   End Sub
        AddHandler client.DownloadFileCompleted, Sub(sender, e)
                                                     BeginInvoke(New Action(Sub()
                                                                                GenerateServerProperties()
                                                                                GenerateServerInfo()
                                                                                ProgressText.Text = "完成!"
                                                                                Progress.Value = 100
                                                                                _owner._owner.RegisterJavaServer(serverDir)
                                                                                _owner._owner.AddJavaServer(serverDir)
                                                                                _owner._owner.servers.Add(serverDir)
                                                                                Me.Close()
                                                                            End Sub))
                                                 End Sub

    End Sub
    Sub GenerateServerProperties()
        BeginInvoke(New Action(Sub()
                                   ProgressText.Text = "正在生成伺服器設定……"
                               End Sub))
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(_owner.ServerDirBox.Text, "server.properties"), "", False)
        Dim writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(_owner.ServerDirBox.Text, "server.properties"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
        writer.WriteLine("#Minecraft server properties")
        writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy"))
        Dim optionDict As New Dictionary(Of Integer, Boolean)
        For i = 0 To _owner.ServerOptionBox.RowCount - 1
            writer.WriteLine(String.Format("{0}={1}", _owner.ToOptionName(i), _owner.GetOptionValue(i)))
        Next
        writer.WriteLine("query.port" & "=" & _owner.PortBox.Value)
        Select Case _owner.ipType
            Case CreateJavaServerDialog.ServerIPType.Float
                writer.WriteLine("server-ip" & "=" & "")
            Case CreateJavaServerDialog.ServerIPType.Default
                writer.WriteLine("server-ip" & "=" & _owner.IPBox.Text)
            Case CreateJavaServerDialog.ServerIPType.Custom
                writer.WriteLine("server-ip" & "=" & _owner.IPBox.Text)
        End Select
        writer.WriteLine("server-port" & "=" & _owner.PortBox.Value)
        writer.WriteLine("resource-pack" & "=" & "")
        writer.WriteLine("resource-pack-sha1" & "=" & "")
        writer.WriteLine("generate-structures" & "=" & _owner.CheckBox1.Checked.ToString.ToLower)
        writer.WriteLine("generator-settings" & "=" & _owner.GeneratorSettingBox.Text)
        writer.WriteLine("level-name" & "=" & _owner.LevelNameBox.Text)
        writer.WriteLine("level-seed" & "=" & _owner.LevelSeedBox.Text)
        writer.WriteLine("level-type" & "=" & GetLevelType(_owner.LevelTypeBox.SelectedIndex))
        writer.Flush()
        writer.Close()
    End Sub

    Private Function GetLevelType(Index As Integer) As String
        Select Case Index
            Case 0 : Return "DEFAULT"
            Case 1 : Return "FLAT"
            Case 2 : Return "LARGEBIOMES"
            Case 3 : Return "AMPLIFIED"
            Case 4 : Return "CUSTOMIZED"
            Case Else : Return ""
        End Select
    End Function

    Sub GenerateServerInfo()
        BeginInvoke(New Action(Sub()
                                   ProgressText.Text = "正在簽署EULA……"
                               End Sub))
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(_owner.ServerDirBox.Text, "eula.txt"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(_owner.ServerDirBox.Text, "eula.txt"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.WriteLine("#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).")
            writer.WriteLine("#" & Now.ToString("ddd MMM dd HH:mm:ss K yyyy"))
            writer.WriteLine("eula=true")
            writer.Flush()
            writer.Close()
        End Using
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(_owner.ServerDirBox.Text, "server.info"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(_owner.ServerDirBox.Text, "server.info"), IO.FileMode.OpenOrCreate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.WriteLine("server-version=" & _owner.VersionBox.Text)
            writer.WriteLine("server-version-type=" & GetServerType())
            If _owner.ServerTypeBox.SelectedIndex = 1 Then
                writer.WriteLine("forge-build-version=" & ForgeNewestBranchVersionList(_owner.VersionBox.Text))
            End If
            writer.Flush()
            writer.Close()
        End Using
    End Sub
    Function GetServerType() As String
        Select Case _owner.ServerTypeBox.SelectedIndex
            Case 0
                Return "Vanilla"
            Case 1
                Return "Forge"
            Case 2
                Return "Spigot"
            Case 3
                Return "CraftBukkit"
            Case Else
                Return ""
        End Select
    End Function
    Private Sub ServerCreator_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _owner.Close()
    End Sub
End Class