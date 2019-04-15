Public Class BungeeCordCreateDialog
    Private Sub SolutionDirBrowseBtn_Click(sender As Object, e As EventArgs) Handles SolutionDirBrowseBtn.Click
        Static dir As New FolderBrowserDialog
        dir = New FolderBrowserDialog
        dir.ShowNewFolderButton = True
        dir.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
        dir.Description = "選擇建立方案的資料夾位置"
        If dir.ShowDialog = DialogResult.OK Then
            SolutionDirBox.Text = dir.SelectedPath
        End If
    End Sub

    Private Sub BungeeCordCreateDialog_Load(sender As Object, e As EventArgs) Handles Me.Load
        Label10.Text = "BungeeCord 版本：build #" & BungeeCordUpdater.GetLatestVersionNumber
    End Sub

    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click

        If SolutionDirBox.Text.Trim <> "" Then
            Dim _host As BungeeCordHost = BungeeCordHost.GetEmptyBungeeCordHost(SolutionDirBox.Text)
            _host.SetVersion(BungeeCordUpdater.GetLatestVersionNumber)
            Dim u As New BungeeCordCreateHelper(_host)
            u.ShowDialog(Owner)
            Close()
        End If
    End Sub

    Private Sub SolutionDirBox_TextChanged(sender As Object, e As EventArgs) Handles SolutionDirBox.TextChanged

    End Sub


End Class