Imports System.ComponentModel

Public Class HybridMPExplorer
    Dim engine As CharcoalEngine
    Friend index As Integer
    Friend isStart As Boolean = True
    Friend isError As Boolean = False

    Sub New(index As Integer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.index = index
        engine = New CharcoalEngine(index)

    End Sub


    Private Sub BukkitForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        CreatedForm.Add(Me)
        ShowInTaskbar = MiniState = 0
        Dim server = GlobalModule.Manager.ServerEntityList(index)
        Me.Text = Me.Text.Replace("Cauldron", GetSimpleVersionName(server.ServerVersionType, server.ServerVersion))
        AddHandler engine.DownloadProgressChanged, Sub(obj, args)
                                                       Try
                                                           ToolStripProgressBar1.Value = args.ProgressPercentage
                                                       Catch ex As Exception
                                                       End Try
                                                   End Sub

        GoHome()
    End Sub
    Sub GoHome()

    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click
        isStart = False
        isError = False
        Try
            engine.LoadPage("https://dev.bukkit.org/bukkit-plugins", CharcoalEngine.RenderPageType.Bukkit_PluginListPage, CharcoalEnginePanel)
        Catch ex As Exception
            isError = True
            CharcoalEnginePanel.Refresh()
        End Try
    End Sub

    Private Sub ToolStripButton2_Click(sender As Object, e As EventArgs) Handles ToolStripButton2.Click
        isStart = False
        isError = False
        Try
            engine.LoadPage("https://www.curseforge.com/minecraft/bukkit-plugins", CharcoalEngine.RenderPageType.CurseForge_PluginListPage, CharcoalEnginePanel)
        Catch ex As Exception
            isError = True
            CharcoalEnginePanel.Refresh()
        End Try
    End Sub
    Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton3.Click
        isStart = False
        isError = False
        Try
            engine.LoadPage("https://www.curseforge.com/minecraft/mc-mods", CharcoalEngine.RenderPageType.CurseForge_ModListPage, CharcoalEnginePanel)
        Catch ex As Exception
            isError = True
            CharcoalEnginePanel.Refresh()
        End Try
    End Sub
    Private Sub CharcoalEnginePanel_Paint(sender As Object, e As PaintEventArgs) Handles CharcoalEnginePanel.Paint
        If isStart Then
            Try
                Dim g As Graphics = e.Graphics
                g.Clear(Color.LightGray)
                g.DrawString("請點選上方插件/模組來源來瀏覽插件/模組", New Font(SystemFonts.IconTitleFont.FontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel), New SolidBrush(Color.DimGray), New RectangleF(CharcoalEnginePanel.Location, CharcoalEnginePanel.Size), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                g.Dispose()
            Catch ex As Exception

            End Try
        ElseIf isError Then
            Try
                Dim g As Graphics = e.Graphics
                g.Clear(Color.LightGray)
                g.DrawString("加載時發生錯誤", New Font(SystemFonts.IconTitleFont.FontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel), New SolidBrush(Color.DimGray), New RectangleF(CharcoalEnginePanel.Location, CharcoalEnginePanel.Size), New StringFormat With {.Alignment = StringAlignment.Center, .LineAlignment = StringAlignment.Center})
                g.Dispose()
            Catch ex As Exception

            End Try
        End If
    End Sub


    Private Sub CharcoalEnginePanel_Resize(sender As Object, e As EventArgs) Handles CharcoalEnginePanel.Resize
        If isStart OrElse isError Then CharcoalEnginePanel.Refresh()
    End Sub

    Private Sub HybridMPExplorer_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CreatedForm.Remove(Me)

    End Sub
End Class