﻿Imports ServerManager

Public Class DownloadVersionList
    Friend server As Server
    Dim index As Integer
    Dim pluginName As String
    Dim website As BrowsingWebsite
    Public Property DownloadList As New List(Of String)
    Enum BrowsingWebsite
        Bukkit
        CurseForge_Plugin
        CurseForge_Mod
        Nukkit_PluginDownloadList
    End Enum
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
            If server Is Nothing Then
                Console.WriteLine("Server is Nothing!" & vbNewLine & "Why?")
                server = TestForm.check()
            End If
            Dim addPath As String = ""
            Select Case website
                Case BrowsingWebsite.Bukkit
                    addPath = "/download"
                Case BrowsingWebsite.CurseForge_Plugin
                    addPath = "/file"
                Case BrowsingWebsite.CurseForge_Mod
                    addPath = "/file"
                Case BrowsingWebsite.Nukkit_PluginDownloadList
                    addPath = ""
            End Select
            Dim request As Net.HttpWebRequest = Net.WebRequest.Create(DownloadList(VersionList.SelectedIndices(0)) & addPath)
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
                server.ServerPlugins.Add(New Server.BukkitPlugin(pluginName, IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"), DateTime.Parse(VersionList.SelectedItems(0).SubItems(3).Text).ToString))
            ElseIf website = BrowsingWebsite.CurseForge_Mod Then
                My.Computer.Network.DownloadFile(realURI, IO.Path.Combine(server.ServerPath, "mods", pluginName & ".jar"), "", "", True, 100, True)
                For Each forgeMod In server.ServerMods
                    If forgeMod.Name = pluginName Then
                        IO.File.Delete(forgeMod.Path)
                        server.ServerMods.Remove(forgeMod)
                    End If
                Next
                server.ServerMods.Add(New Server.ForgeMod(pluginName, IO.Path.Combine(server.ServerPath, "mods", pluginName & ".jar"), DateTime.Parse(VersionList.SelectedItems(0).SubItems(3).Text).ToString))
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
                server.ServerPlugins.Add(New Server.BukkitPlugin(pluginName, IO.Path.Combine(server.ServerPath, "plugins", pluginName & ".jar"), DateTime.Parse(t).ToString))
                'GlobalModule.Manager.ServerEntityList(index) = server
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