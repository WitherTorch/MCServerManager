Imports System.ComponentModel

Public Class ForgeModManager
    Implements IManagerGUI
    Dim server As Server

    Sub New(index As Integer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = GlobalModule.Manager.ServerEntityList(index)
    End Sub
    Private Sub 瀏覽插件ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 瀏覽模組ToolStripMenuItem.Click
        Dim explorer As New ForgeModExplorer(GlobalModule.Manager.ServerEntityList.IndexOf(server))
        explorer.Show()
    End Sub

    Private Sub 移除插件ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 移除模組ToolStripMenuItem.Click
        Try
            server.ServerMods.RemoveAt(ModList.SelectedIndices(0))
            My.Computer.FileSystem.DeleteFile(ModList.SelectedItems(0).SubItems(2).Text)
            ModList.Items.Remove(ModList.SelectedItems(0))
        Catch ex As Exception
        End Try
    End Sub
    Sub LoadMods()
        ModList.Items.Clear()
        For Each forgeMod In server.ServerMods
            ModList.Items.Add(New ListViewItem(New String() {forgeMod.Name, forgeMod.Version, forgeMod.VersionDate.ToString, forgeMod.Path}))
        Next
    End Sub

    Private Sub 重新整理ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 重新整理ToolStripMenuItem.Click
        LoadMods()
    End Sub

    Private Sub ForgeModManager_Load(sender As Object, e As EventArgs) Handles Me.Load
        CreatedForm.Add(Me)
        ShowInTaskbar = MiniState = 0
        If My.Computer.FileSystem.DirectoryExists(IO.Path.Combine(server.ServerPath, "mods")) = False Then
            My.Computer.FileSystem.CreateDirectory(IO.Path.Combine(server.ServerPath, "mods"))
        End If
        BeginInvoke(New Action(Sub() LoadMods()))
    End Sub

    Private Sub ForgeModManager_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CreatedForm.Remove(Me)

    End Sub
End Class