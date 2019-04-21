Public Class Form1
    Dim host As ServiceHost
    Dim isStarted As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If isStarted Then
            host.Stop()
            host = Nothing
            isStarted = False
            Me.Text = "啟動服務"
        Else
            If TextBox1.Text.Trim <> "" AndAlso IO.Directory.Exists(TextBox1.Text) Then
                AddHandler host.OnMessage, Sub(msg)
                                               ListBox2.Items.Add(msg)
                                           End Sub
                host = New ServiceHost(TextBox1.Text)
                host.Start()
                isStarted = True
                Me.Text = "停止服務"
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If host IsNot Nothing Then
            Dim username As String = InputBox("使用者的名稱?", Me.Text)
            If ListBox1.Items.Contains(username) = False Then
                Dim password As String = InputBox("使用者的密碼?", Me.Text)
                host.AddUser(username, password)
            Else
                MsgBox("使用者已存在!", Me.Text)
            End If
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If host IsNot Nothing Then
            If ListBox1.SelectedIndex > -1 Then
                Dim username As String = ListBox1.SelectedItem
                host.RemoveUser(username)
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub
End Class
