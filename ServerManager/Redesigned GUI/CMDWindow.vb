Imports ServerManager

Public Class CMDWindow
    Implements ICMDWindow
    Dim process As Process
    Private Property ICMDWindow_Text As String Implements ICMDWindow.Text
        Get
            Return Text
        End Get
        Set(value As String)
            Text = value
        End Set
    End Property

    Public Sub Run(program As String, arguments As String, workingDirectory As String) Implements ICMDWindow.Run
        process = Process.Start(New ProcessStartInfo(program, arguments) With {.WorkingDirectory = workingDirectory, .CreateNoWindow = True, .RedirectStandardError = True, .RedirectStandardInput = True, .RedirectStandardOutput = True, .StandardErrorEncoding = System.Text.Encoding.UTF8, .StandardOutputEncoding = System.Text.Encoding.UTF8})
        process.EnableRaisingEvents = True
        process.BeginErrorReadLine()
        process.BeginOutputReadLine()
        AddHandler process.OutputDataReceived, Sub(obj, args)
                                                   If args.Data IsNot Nothing Then
                                                       TextBox1.AppendText(args.Data & vbNewLine)
                                                   End If
                                               End Sub
        AddHandler process.ErrorDataReceived, Sub(obj, args)
                                                  If args.Data IsNot Nothing Then
                                                      TextBox1.AppendText(args.Data & vbNewLine)
                                                  End If
                                              End Sub
        AddHandler process.Exited, Sub(obj, args)
                                       DialogResult = Windows.Forms.DialogResult.OK
                                       Close()
                                   End Sub
    End Sub
    Shadows Function ICMDWindow_ShowDialog() As DialogResult Implements ICMDWindow.ShowDialog
        Return ShowDialog()
    End Function
End Class