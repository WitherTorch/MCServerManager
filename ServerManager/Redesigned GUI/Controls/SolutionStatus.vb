Public Class SolutionStatus
    Friend _target As SolutionTargetBase
    Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。

    End Sub
    Sub New(Target As SolutionTargetBase)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _target = Target
    End Sub
    Sub New(path As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _target = SolutionMaker.GetSolution(path)
    End Sub
    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs) Handles ToolTip1.Draw
        e.DrawBorder()
        e.DrawBackground()
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(100, 100, 100)), e.Bounds)
        e.DrawText()
    End Sub

    Private Sub ServerStatus_Load(sender As Object, e As EventArgs) Handles Me.Load
        PathNameLabel.Text = _target.SolutionPathName
        ServerVersionLabel.Text = ServerMaker.SoftwareDictionary(_target.GetInternalName).ReadableName & " " & _target.GetSoftwareVersionString
    End Sub

    Private Sub StartServerButton_Click(sender As Object, e As EventArgs) Handles StartServerButton.Click
        If ServerConsoleBindings.ContainsKey(_target) = False Then
            Dim console As New ServerConsole(_target)
            console.Show()
        End If
    End Sub

    Private Sub SettingServerButton_Click(sender As Object, e As EventArgs) Handles SettingServerButton.Click
        If ServerSettingFormBindings.ContainsKey(_target) = False Then
            Dim setting As New ServerSettingForm(_target)
            setting.Show()
        End If
    End Sub

    Private Sub ServerStatus_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Y < 25 OrElse e.X = Width - Margin.Right Then
            WinAPI.MoveForm(Handle)
        End If
    End Sub

    Private Sub ShowFolderButton_Click(sender As Object, e As EventArgs) Handles ShowFolderButton.Click
        Process.Start(_target.ServerPath)
    End Sub
End Class