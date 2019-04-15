Public Class MinecraftGUITestForm
    Private Sub MinecraftGUITestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CheckBox1_Load(sender As Object, e As EventArgs) Handles CheckBox1.Load

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        TextBox1.Visible = CheckBox1.Checked
    End Sub
End Class