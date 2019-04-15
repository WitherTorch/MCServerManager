Imports System.ComponentModel

Public Class SpigotGitBuildWindow
    Friend Process As Process
    Friend Sub Run(program As String, args As String, directory As String)
        Console.WriteLine(program)
        Console.WriteLine(args)
        Console.WriteLine(directory)
        Dim processInfo As New ProcessStartInfo(program, args)
        processInfo.UseShellExecute = False
        processInfo.CreateNoWindow = True
        processInfo.RedirectStandardOutput = True
        processInfo.RedirectStandardInput = True
        processInfo.RedirectStandardError = True
        processInfo.WorkingDirectory = directory
        Process = Process.Start(processInfo)
        Process.BeginOutputReadLine()
        Process.BeginErrorReadLine()
        Process.EnableRaisingEvents = True
        AddHandler Process.ErrorDataReceived, Sub(sender, e)
                                                  Try
                                                      If IsNothing(Process) = False AndAlso Process.HasExited = False Then
                                                          If IsNothing(e.Data) = False Then
                                                              Invoke(Sub()
                                                                         If TextBox1.Text = "" Then
                                                                             TextBox1.AppendText(e.Data)
                                                                         Else
                                                                             TextBox1.AppendText(vbCrLf & e.Data)
                                                                         End If
                                                                     End Sub)
                                                              Console.WriteLine("[Git Error Message] " & e.Data)
                                                          End If
                                                      End If
                                                  Catch ex As Exception
                                                  End Try
                                              End Sub
        AddHandler Process.OutputDataReceived, Sub(sender, e)
                                                   Try
                                                       If IsNothing(Process) = False AndAlso Process.HasExited = False Then
                                                           If IsNothing(e.Data) = False Then
                                                               Invoke(Sub()
                                                                          If TextBox1.Text = "" Then
                                                                              TextBox1.AppendText(e.Data)
                                                                          Else
                                                                              TextBox1.AppendText(vbCrLf & e.Data)
                                                                          End If
                                                                      End Sub)
                                                               Console.WriteLine("[Git Output Message] " & e.Data)
                                                           End If
                                                       End If
                                                   Catch ex As Exception
                                                   End Try
                                               End Sub
        AddHandler Process.Exited, Sub(sender, e)
                                       Try

                                           Process = Nothing
                                           Console.WriteLine("Process Exited")
                                           DialogResult = DialogResult.OK
                                           Close()

                                       Catch ex As Exception
                                       End Try
                                   End Sub
    End Sub
    Private Sub Commander_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try
            If IsNothing(Process) = False OrElse Process.HasExited = False Then
                DialogResult = DialogResult.OK
                Process.Kill()
            End If
        Catch ex As Exception
        End Try
    End Sub
End Class
