Imports System.Threading.Tasks

Public Class CreateServerWizard
    Private Sub CreateMapWizard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WizardPanel1.Hide()
        WizardPanel2.Hide()
        WizardPanel1.Show()
        NextButton.Enabled = False
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles CreateProgress.Click

    End Sub

    Private Sub CreateStatusLabel_Click(sender As Object, e As EventArgs) Handles CreateStatusLabel.Click

    End Sub

    Private Sub NextButton_Click(sender As Object, e As EventArgs) Handles NextButton.Click
        WizardPanel1.Hide()
        WizardPanel2.Show()
        NextButton.Text = "建立(&C)"
        CreateProgress.Increment(1)
        AddHandler NextButton.Click, Sub()
                                         Task.Run(New Action(Sub()
                                                                              Dim s As Server = Server.CreateServer
                                                                              s.SetPath(ServerDirBox.Text)
                                                                              If VersionTypeBox.SelectedIndex + 1 = Server.EServerVersionType.Forge Then
                                                                                  s.SetVersion(Me.VersionBox.Text, ForgeNewestBranchVersionList(Me.VersionTypeBox.Text))
                                                                              ElseIf VersionTypeBox.SelectedIndex + 1 = Server.EServerVersionType.Nukkit Then
                                                                                  s.SetVersion("1.0", NukkitVersion)
                                                                              Else
                                                                                  s.SetVersion(Me.VersionBox.Text)
                                                                              End If
                                                                              If VersionTypeBox.SelectedIndex + 1 = Server.EServerVersionType.Nukkit Then
                                                                                  s.SetVersionType(Server.EServerType.Bedrock, VersionTypeBox.SelectedIndex + 1)
                                                                              Else
                                                                                  s.SetVersionType(Server.EServerType.Java, VersionTypeBox.SelectedIndex + 1)
                                                                              End If
                                                                              s.ServerOptions = CType(AdvancedPropertyGrid.SelectedObject, IServerOptions).OutputOption
                                                                              Dim serverBuilder As New ServerCreateHelper(s, Me.ServerDirBox.Text)
                                                                              If serverBuilder.ShowDialog() <> Nothing Then
                                                                                  Me.Close()
                                                                              End If
                                                                          End Sub))
                                         Me.Enabled = False
                                     End Sub
    End Sub
End Class