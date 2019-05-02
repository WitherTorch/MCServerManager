Public Class HybridMPManager
    Implements IManagerGUI
    Dim server As Server
    Sub New(index As Integer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = GlobalModule.Manager.ServerEntityList(index)
    End Sub
    Private Sub 瀏覽插件ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 瀏覽插件ToolStripMenuItem.Click
        Dim explorer As New HybridMPExplorer(GlobalModule.Manager.ServerEntityList.IndexOf(server))
        explorer.Show()
    End Sub

    Private Sub 移除插件ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 移除插件ToolStripMenuItem.Click
        Try
            Select Case PagesControl.SelectedIndex
                Case 0
                    If PluginList.SelectedItems.Count > 0 Then
                        server.ServerPlugins.RemoveAt(PluginList.SelectedIndices(0))
                        My.Computer.FileSystem.DeleteFile(PluginList.SelectedItems(0).SubItems(2).Text)
                        PluginList.Items.Remove(PluginList.SelectedItems(0))
                    End If
                Case 1
                    If ModList.SelectedItems.Count > 0 Then
                        server.ServerMods.RemoveAt(ModList.SelectedIndices(0))
                        My.Computer.FileSystem.DeleteFile(ModList.SelectedItems(0).SubItems(2).Text)
                        ModList.Items.Remove(ModList.SelectedItems(0))
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub
    Sub LoadPlugins()
        PluginList.Items.Clear()
        For Each plugin In server.ServerPlugins
            PluginList.Items.Add(New ListViewItem(New String() {plugin.Name, plugin.Version, plugin.VersionDate.ToString, plugin.Path}))
        Next
    End Sub
    Sub LoadMods()
        ModList.Items.Clear()
        For Each forgeMod In server.ServerMods
            ModList.Items.Add(New ListViewItem(New String() {forgeMod.Name, forgeMod.Version, forgeMod.VersionDate.ToString, forgeMod.Path}))
        Next
    End Sub

    Private Sub 重新整理ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 重新整理ToolStripMenuItem.Click
        LoadPlugins()
        LoadMods()
    End Sub

    Private Sub BukkitPluginManager_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Text = Me.Text.Replace("Cauldron", GetSimpleVersionName(server.ServerVersionType, server.ServerVersion))
        If My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(server.ServerPath, "plugins")) = False Then
            My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(server.ServerPath, "plugins"))
        End If
        If My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(server.ServerPath, "mods")) = False Then
            My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(server.ServerPath, "mods"))
        End If
        BeginInvoke(New Action(Sub()
                                   LoadPlugins()
                                   LoadMods()
                               End Sub))
    End Sub

End Class