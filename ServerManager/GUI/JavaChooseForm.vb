Public Class JavaChooseForm
    Private JavaList As List(Of JavaPathsProvider.Java)
    Friend Property ChoosedJava As JavaPathsProvider.Java = Nothing
    Private Sub JavaChooseForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        JavaList = JavaPathsProvider.GetJavaList()
        For Each java In JavaList
            Dim item As New ListViewItem(java.Name)
            item.SubItems.Add(java.Path)
            JavaListView.Items.Add(item)
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Static DirDialog As New FolderBrowserDialog
        DirDialog.Description = "選擇Java 所在的資料夾"
        If DirDialog.ShowDialog = DialogResult.OK Then
            Dim dirPath As String = DirDialog.SelectedPath
            If IO.File.Exists(IO.Path.Combine(dirPath, "java.exe")) And
                IO.File.Exists(IO.Path.Combine(dirPath, "javaw.exe")) Then
                Dim item As New ListViewItem(New IO.DirectoryInfo(dirPath).Name & " (外部 Java)")
                item.SubItems.Add(dirPath)
                JavaListView.Items.Add(item)
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If JavaListView.SelectedItems.Count > 0 Then
            Dim item = JavaListView.SelectedItems.Item(0)
            If item IsNot Nothing Then
                ChoosedJava = New JavaPathsProvider.Java() With {.Name = item.Text, .Path = item.SubItems(item.SubItems.Count - 1).Text}
                DialogResult = DialogResult.OK
                Close()
            End If
        End If
    End Sub

    Private Sub JavaChooseForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If DialogResult <> DialogResult.OK Then DialogResult = DialogResult.Cancel
    End Sub
End Class