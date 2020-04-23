Imports System.ComponentModel
Imports System.Threading

Public Class ModpackExplorer
    Dim engine As CharcoalEngine
    Dim spongeThread As Thread
    Friend _server As ModPackServer
    Friend isStart As Boolean = True


    Sub New(modpackServer As ModPackServer)

        ' 設計工具需要此呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        ' Me._server = GlobalModule.Manager.ServerEntityList(Index)
        _server = modpackServer
        engine = New CharcoalEngine(_server)
    End Sub

    Private Sub BukkitForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        CreatedForm.Add(Me)
        ShowInTaskbar = MiniState = 0

        AddHandler engine.DownloadProgressChanged, Sub(obj, args)
                                                       ToolStripProgressBar1.Value = args.ProgressPercentage
                                                   End Sub
    End Sub
    Sub GoHome()

    End Sub

    Private Sub CharcoalEnginePanel_Paint(sender As Object, e As PaintEventArgs) Handles CharcoalEnginePanel.Paint
        If isStart Then
            Try
                Dim g As Graphics = e.Graphics
                g.Clear(Color.LightGray)
                g.DrawString("請點選上方模組包來源來選擇模組包", New Font(SystemFonts.IconTitleFont.FontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel), New SolidBrush(Color.DimGray), New RectangleF(CharcoalEnginePanel.Location, CharcoalEnginePanel.Size), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                g.Dispose()
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub CharcoalEnginePanel_Resize(sender As Object, e As EventArgs) Handles CharcoalEnginePanel.Resize
        If isStart Then CharcoalEnginePanel.Refresh()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        Try
            isStart = False
            engine.LoadPage("https://www.curseforge.com/minecraft/modpacks", CharcoalEngine.RenderPageType.CurseForge_ModpackListPage, CharcoalEnginePanel)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        Try
            isStart = False
            engine.LoadPage("https://www.feed-the-beast.com/modpacks?filter-server-packs=y&filter-game-version=&filter-sort=4", CharcoalEngine.RenderPageType.FeedTheBeast_ModpackListPage, CharcoalEnginePanel)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ModpackExplorer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CreatedForm.Remove(Me)

    End Sub
End Class