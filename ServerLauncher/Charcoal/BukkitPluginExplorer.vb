Public Class BukkitPluginExplorer
    Dim engine As CharcoalEngine
    Friend index As Integer


    Sub New(index As Integer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.index = index
        engine = New CharcoalEngine(index)

    End Sub


    Private Sub BukkitForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        AddHandler engine.DownloadProgressChanged, Sub(obj, args)
                                                       ToolStripProgressBar1.Value = args.ProgressPercentage
                                                   End Sub

        GoHome()
    End Sub
    Sub GoHome()

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        engine.LoadPage("https://dev.bukkit.org/bukkit-plugins", CharcoalEngine.PluginPageType.Bukkit_PluginListPage, CharcoalEnginePanel)
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        engine.LoadPage("https://www.curseforge.com/minecraft/bukkit-plugins", CharcoalEngine.PluginPageType.CurseForge_PluginListPage, CharcoalEnginePanel)
    End Sub
End Class