Public Class ServerConsole
    Private Sub ServerConsole_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub ServerConsole_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Y < 30 OrElse e.X = Width - Margin.Right Then
            WinAPI.MoveForm(Handle)
        End If
    End Sub
End Class