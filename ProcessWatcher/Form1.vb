Public Class Form1
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim processInfo As New ProcessStartInfo(TextBox2.Text, TextBox3.Text)
        processInfo.UseShellExecute = False
        processInfo.CreateNoWindow = True
        processInfo.RedirectStandardOutput = True
        processInfo.WorkingDirectory = TextBox4.Text
        Dim Process As Process = Process.Start(processInfo)
        Process.BeginOutputReadLine()
        Process.EnableRaisingEvents = True
        AddHandler Process.OutputDataReceived, Sub(obj, args)
                                                   TextBox1.Text &= vbCrLf & args.Data
                                               End Sub
    End Sub
End Class
