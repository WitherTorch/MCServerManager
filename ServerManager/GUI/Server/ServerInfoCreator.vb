Public Class ServerInfoCreator

    Private Sub VersionBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles VersionBox.SelectedIndexChanged
        Select Case VersionTypeBox.SelectedIndex
            Case 1 'Forge
                ComboBox1.Items.Clear()
                Label1.Text = "Forge 分支："
                ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
                For Each item In ForgeVersionDict(VersionBox.Text)
                    ComboBox1.Items.Add(item)
                Next
                Label1.Enabled = True
                ComboBox1.Enabled = True
            Case 4 'SpongeVanilla
                ComboBox1.Items.Clear()
                Label1.Text = "SpongeVanilla 分支："
                ComboBox1.DropDownStyle = ComboBoxStyle.Simple
                Label1.Enabled = True
                ComboBox1.Enabled = True
        End Select
    End Sub

    Private Sub ServerInfoCreator_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class