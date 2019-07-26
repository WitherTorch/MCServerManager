Public Class CMDForm
    Dim _writer As IO.StreamWriter
    Sub New(ByRef outputWriter As IO.StreamWriter, Optional consoleText As String = "")

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _writer = outputWriter
        TextBox1.AppendText(consoleText)
    End Sub
    Sub AppendText(text As String)
        BeginInvokeIfRequired(Me, Sub()
                                      TextBox1.AppendText(IIf(TextBox1.Text.Trim(vbCr, vbLf) = "", text, vbNewLine & text))
                                  End Sub)
    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                _writer.WriteLine(TextBox2.Text)
                TextBox2.Text = ""
            Catch ex As Exception

            End Try
        End If
    End Sub
End Class