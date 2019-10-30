Public Class ServerStatus
    Friend _server As ServerBase
    Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。

    End Sub
    Sub New(Server As ServerBase)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _server = Server
    End Sub
    Sub New(path As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _server = ServerMaker.GetServer(path)
    End Sub
    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs) Handles ToolTip1.Draw
        e.DrawBorder()
        e.DrawBackground()
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(100, 100, 100)), e.Bounds)
        e.DrawText()
    End Sub

    Private Sub ServerStatus_Load(sender As Object, e As EventArgs) Handles Me.Load
        PathNameLabel.Text = _server.ServerPathName
        ServerVersionLabel.Text = ServerMaker.SoftwareDictionary(_server.GetInternalName).ReadableName & " " & _server.GetSoftwareVersionString
        If _server.ServerIcon Is Nothing Then
            PictureBox1.Image = _server.ServerIcon
        Else
            PictureBox1.Image = My.Resources.ServerDefaultIcon
        End If
    End Sub

    Private Sub StartServerButton_Click(sender As Object, e As EventArgs) Handles StartServerButton.Click
        Dim console As New ServerConsole(_server)
        console.Show()
    End Sub
End Class
