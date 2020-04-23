Imports System.ComponentModel

Public Class NukkitPluginExplorer
    Dim engine As CharcoalEngine
    Friend index As Integer


    Sub New(index As Integer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.index = index
        engine = New CharcoalEngine(index)
    End Sub


    Private Sub NukkitForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        CreatedForm.Add(Me)
        ShowInTaskbar = MiniState = 0

        AddHandler engine.DownloadProgressChanged, Sub(obj, args)
                                                       ToolStripProgressBar1.Value = args.ProgressPercentage
                                                   End Sub

        GoHome()
    End Sub
    Sub GoHome()
        engine.LoadPage("https://nukkitx.com/resources/categories/nukkit-plugins.1/", CharcoalEngine.RenderPageType.Nukkit_PluginListPage, CharcoalEnginePanel)
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        GoHome()
    End Sub

    Private Sub NukkitPluginExplorer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CreatedForm.Remove(Me)

    End Sub
End Class