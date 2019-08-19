Imports ServerManager

Public Class DownloadVersionList
    Friend server As Server
    Friend modpackServer As ModPackServer
    Friend modpackName As String
    Dim index As Integer
    Dim pluginName As String
    Dim website As BrowsingWebsite
    Public Property DownloadList As New List(Of String)
    Enum BrowsingWebsite
        Bukkit
        CurseForge_Plugin
        CurseForge_Mod
        Spigot_PluginDownloadList
        Nukkit_PluginDownloadList
        FeedTheBeast_Modpack
        Curse_Modpack
    End Enum
    Sub New(mServer As ModPackServer, packName As String, website As BrowsingWebsite)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        modpackServer = mServer
        modpackName = packName
        Me.website = website
    End Sub
    Sub New(index As Integer, pluginName As String, website As BrowsingWebsite)

        ' 設計工具需要此呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = GlobalModule.Manager.ServerEntityList(index)
        Me.index = index
        Me.pluginName = pluginName
        Me.website = website
    End Sub
    Private Sub VersionList_ItemActivate(sender As Object, e As EventArgs) Handles VersionList.ItemActivate
        Invoke(Sub() Call download())
    End Sub
    Sub download()
        Try
            If server Is Nothing AndAlso modpackServer Is Nothing Then
                Console.WriteLine("Server is Nothing!" & vbNewLine & "Why?")
            End If
            Dim addPath As String = ""
            Dim url As String = DownloadList(VersionList.SelectedIndices(0))
            Select Case website
                Case BrowsingWebsite.Bukkit
                    addPath = "/download"
                Case BrowsingWebsite.CurseForge_Plugin
                    addPath = "/file"
                    url = url.Replace("/files/", "/download/")
                Case BrowsingWebsite.CurseForge_Mod
                    addPath = "/file"
                    url = url.Replace("/files/", "/download/")
                Case BrowsingWebsite.Spigot_PluginDownloadList
                    addPath = ""
                Case BrowsingWebsite.Nukkit_PluginDownloadList
                    addPath = ""
            End Select
            Dim request As Net.HttpWebRequest = Net.WebRequest.Create(url & addPath)
            Dim version = System.Environment.OSVersion.Version
            request.UserAgent = "Mozilla/5.0 (Windows NT " & version.Major & "." & version.Minor & ") Charcoal/" & CharcoalEngine.CHARCOAL_VER
            Dim realURI As Uri = request.GetResponse().ResponseUri
            If website = BrowsingWebsite.Bukkit OrElse website = BrowsingWebsite.CurseForge_Plugin Then
                My.Computer.Network.DownloadFile(realURI, IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"), "", "", True, 100, True)
                For Each plugin In server.ServerPlugins
                    If plugin.Name = pluginName Then
                        IO.File.Delete(plugin.Path)
                        server.ServerPlugins.Remove(plugin)
                    End If
                Next
                Dim _plugin As New Server.ServerPlugin(pluginName, IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"), "", Date.Parse(VersionList.SelectedItems(0).SubItems(3).Text).ToString, IO.File.GetLastWriteTime(IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar")))
                Using unpatcher As New BukkitPluginUnpatcher(IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"))
                    Dim info = unpatcher.GetPluginInfo()
                    If info.IsNull = False Then
                        _plugin.Name = info.Name
                        _plugin.Version = info.Version
                        server.ServerPlugins.Add(_plugin)
                    End If
                End Using
            ElseIf website = BrowsingWebsite.CurseForge_Mod Then
                My.Computer.Network.DownloadFile(realURI, IO.Path.Combine(server.ServerPath, "mods", pluginName & ".jar"), "", "", True, 100, True)
                For Each forgeMod In server.ServerMods
                    If forgeMod.Name = pluginName Then
                        IO.File.Delete(forgeMod.Path)
                        server.ServerMods.Remove(forgeMod)
                    End If
                Next
                Dim _mod As New Server.ServerMod(pluginName, IO.Path.Combine(server.ServerPath, "mods", pluginName & ".jar"), "", Date.Parse(VersionList.SelectedItems(0).SubItems(3).Text).ToString, IO.File.GetLastWriteTime(IO.Path.Combine(server.ServerPath, "mods", pluginName & ".jar")))
                Using unpatcher As New ForgeModUnpatcher(IO.Path.Combine(server.ServerPath, "mods", pluginName & ".jar"))
                    Dim info = unpatcher.GetModInfo()
                    If info.IsNull = False Then
                        _mod.Name = info.Name
                        _mod.Version = info.Version
                        server.ServerMods.Add(_mod)
                    End If
                End Using
                server.ServerMods.Add(New Server.ServerMod(pluginName, IO.Path.Combine(server.ServerPath, "mods", pluginName & ".jar"), "", DateTime.Parse(VersionList.SelectedItems(0).SubItems(3).Text).ToString, IO.File.GetLastWriteTime(IO.Path.Combine(server.ServerPath, "mods", pluginName & ".jar"))))
            ElseIf website = BrowsingWebsite.Nukkit_PluginDownloadList Then
                My.Computer.Network.DownloadFile(realURI, IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"), "", "", True, 100, True)
                For Each plugin In server.ServerPlugins
                    If plugin.Name = pluginName Then
                        IO.File.Delete(plugin.Path)
                        server.ServerPlugins.Remove(plugin)
                    End If
                Next
                Dim t = VersionList.SelectedItems(0).SubItems(3).Text
                If t.Contains("at") Then
                    t = t.Remove(t.IndexOf("at"))
                    t = t.Trim
                End If
                Dim _plugin As New Server.ServerPlugin(pluginName, IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"), "", Date.Parse(t).ToString, IO.File.GetLastWriteTime(IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar")))
                Using unpatcher As New BukkitPluginUnpatcher(IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"))
                    Dim info = unpatcher.GetPluginInfo()
                    If info.IsNull = False Then
                        _plugin.Name = info.Name
                        _plugin.Version = info.Version
                        server.ServerPlugins.Add(_plugin)
                    End If
                End Using
                'GlobalModule.Manager.ServerEntityList(index) = server
            ElseIf website = BrowsingWebsite.Spigot_PluginDownloadList Then
                My.Computer.Network.DownloadFile(realURI, IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"), "", "", True, 100, True)
                For Each plugin In server.ServerPlugins
                    If plugin.Name = pluginName Then
                        IO.File.Delete(plugin.Path)
                        server.ServerPlugins.Remove(plugin)
                    End If
                Next
                Dim t = VersionList.SelectedItems(0).SubItems(3).Text
                If t.Contains("at") Then
                    t = t.Remove(t.IndexOf("at"))
                    t = t.Trim
                End If
                Dim _plugin As New Server.ServerPlugin(pluginName, IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"), "", Date.Parse(t).ToString, IO.File.GetLastWriteTime(IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar")))
                Using unpatcher As New BukkitPluginUnpatcher(IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"))
                    Dim info = unpatcher.GetPluginInfo()
                    If info.IsNull = False Then
                        _plugin.Name = info.Name
                        _plugin.Version = info.Version
                        server.ServerPlugins.Add(_plugin)
                    End If
                End Using
            ElseIf website = BrowsingWebsite.FeedTheBeast_Modpack Then
                modpackServer.SetPackInfo(modpackName, ModPackServer.ModPackType.FeedTheBeast)
                Dim web As New HtmlAgilityPack.HtmlWeb()
                Dim node = web.Load(url).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[2]/div[1]/section[1]/div[1]/div[3]/section[3]/div[1]/div[1]/div[2]/table[1]/tbody[1]/tr[1]/td[2]/div[1]/div[1]/a[1]")
                If node IsNot Nothing Then
                    Dim _url = node.GetAttributeValue("href", "")
                    If String.IsNullOrWhiteSpace(url) = False Then
                        Dim helper As New ModPackServerCreateHelper(modpackServer, _url)
                        helper.Show()
                        BeginInvokeIfRequired(FindForm, Sub()
                                                            FindForm().Close()
                                                        End Sub)
                    End If
                End If
            ElseIf website = BrowsingWebsite.Curse_Modpack Then
                modpackServer.SetPackInfo(modpackName, ModPackServer.ModPackType.CurseForge)
                Dim web As New HtmlAgilityPack.HtmlWeb()
                Dim node = web.Load(url).DocumentNode.SelectSingleNode("/html[1]/body[1]/div[1]/main[1]/div[1]/div[2]/section[1]/div[1]/div[1]/div[1]/section[1]/section[2]/div[1]/div[1]/div[1]/table[1]/tbody[1]/tr[1]/td[2]/a[1]")
                If node IsNot Nothing Then
                    Dim _url = node.GetAttributeValue("href", "").Replace("/files/", "/download/")
                    If String.IsNullOrWhiteSpace(url) = False Then
                        Dim helper As New ModPackServerCreateHelper(modpackServer, _url & "/file")
                        helper.Show()
                        BeginInvokeIfRequired(FindForm, Sub()
                                                            FindForm().Close()
                                                        End Sub)
                    End If
                End If
            End If
        Catch ex As OperationCanceledException

        End Try
    End Sub
    Private Sub DownloadVersionList_Load(sender As Object, e As EventArgs) Handles Me.Load
        If website = BrowsingWebsite.Nukkit_PluginDownloadList Then
            VersionList.Columns.Remove(ColumnHeader1)
            VersionList.Columns.Remove(ColumnHeader3)
            VersionList.Columns.Remove(ColumnHeader5)
            Me.Controls.Remove(NaviBar)
        End If
        VersionList.Font = New Font(New FontFamily(Drawing.Text.GenericFontFamilies.SansSerif), VersionList.Font.Size, VersionList.Font.Style)
    End Sub

    Private Sub NaviBar_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles NaviBar.ItemClicked

    End Sub
End Class
