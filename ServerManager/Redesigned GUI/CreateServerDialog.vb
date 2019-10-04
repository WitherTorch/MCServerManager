Public Class CreateServerDialog
    Dim ViewIndex As Integer = 0
    Dim softwares As Dictionary(Of String, ServerMaker.SoftwareInfo) = ServerMaker.SoftwareDictionary
    Dim server As ServerBase
    Dim targetVersion As String
    Private Sub CreateServerDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each software In softwares.Values
            MetroComboBox1.Items.Add(software.ReadableName)
        Next
        ChangeView(ViewIndex)
    End Sub
    Sub ChangeView(index As Integer)
        Select Case index
            Case 0 'Step 1
                Panel1.Visible = True
                Panel2.Visible = False
                Panel3.Visible = False
                Panel1.Focus()
            Case 1 'Step 2
                server = ServerMaker.MakeServer(softwares.Keys(MetroComboBox1.SelectedIndex))
                server.ServerPath = MetroTextBox1.Text
                MetroComboBox2.Items.AddRange(server.GetAvailableVersions)
                Panel1.Visible = False
                Panel2.Visible = True
                Panel3.Visible = False
                Panel2.Focus()
            Case 2 'Step 3
                targetVersion = MetroComboBox2.SelectedItem.ToString()
                Select Case MetroComboBox3.SelectedIndex
                    Case 0
                        server.GetServerProperties.SetValue("server-ip", "")
                    Case 1
                        server.GetServerProperties.SetValue("server-ip", MetroComboBox4.SelectedItem.ToString())
                    Case 2
                        server.GetServerProperties.SetValue("server-ip", MetroTextBox2.Text)
                End Select
                Panel1.Visible = False
                Panel2.Visible = False
                Panel3.Visible = True
                Panel3.Focus()
        End Select
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        If ViewIndex >= 3 Then 'Create finished

        Else 'Creating
            Dim checkingOK As Boolean = False 'Check settings are OK
            Select Case ViewIndex
                Case 0
                    If String.IsNullOrWhiteSpace(MetroTextBox1.Text) = False Then
                        If IO.Directory.Exists(MetroTextBox1.Text) = False Then
                            Try
                                IO.Directory.CreateDirectory(MetroTextBox1.Text)
                                If MetroComboBox1.SelectedIndex >= 0 Then
                                    checkingOK = True
                                End If
                            Catch ex As Exception

                            End Try
                        Else
                            checkingOK = True
                        End If
                    End If
                Case 1
                    If MetroComboBox2.SelectedIndex >= 0 Then
                        If MetroComboBox3.SelectedIndex >= 0 Then
                            Select Case MetroComboBox3.SelectedIndex
                                Case 0
                                    checkingOK = True
                                Case 1
                                    If MetroComboBox4.SelectedIndex >= 0 Then
                                        checkingOK = True
                                    End If
                                Case 2
                                    Dim address As Net.IPAddress = Nothing
                                    If Net.IPAddress.TryParse(MetroTextBox2.Text, address) Then
                                        address = Nothing
                                        checkingOK = True
                                    End If
                            End Select
                        End If
                    End If
            End Select
            If checkingOK Then
                ViewIndex += 1
                ChangeView(ViewIndex)
            Else
                MsgBox("有些項目尚未填妥或填寫有誤!",, Application.ProductName)
            End If
        End If
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            MetroTextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub MetroComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MetroComboBox3.SelectedIndexChanged
        Select Case MetroComboBox3.SelectedIndex
            Case 1
                MetroTextBox2.Visible = False
                MetroComboBox4.Visible = True
            Case 2
                MetroTextBox2.Visible = True
                MetroComboBox4.Visible = False
            Case Else
                MetroTextBox2.Visible = False
                MetroComboBox4.Visible = False
        End Select
    End Sub
End Class