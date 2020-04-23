Imports System.ComponentModel

Public Class GeneratorMapping
    Shared Function GetSolutionCode(index As Integer) As String
        Try
            Return GeneratorSolutionCodes(index)
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Close()
    End Sub

    Private Sub ChooseBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ChooseBox.SelectedIndexChanged
        Try
            Dim c = InfoPanel.Controls(0)
            InfoPanel.Controls.Clear()
            c.Dispose()
            GC.Collect()
        Catch ex As Exception
        End Try
        If ChooseBox.SelectedIndex <> -1 Then
            Dim info As New GeneratorSettingInfo
            info.Dock = DockStyle.Fill
            info.Margin = New Padding(0)
            InfoPanel.Controls.Add(info)
            Select Case ChooseBox.SelectedIndex
                Case 0
                    info.InfomationBox.Text = "生態域全部是深海的世界。"
                    info.IconBox.Image = My.Resources.Worldwater
                Case 1
                    info.InfomationBox.Text = "一個類似高山生物群落，有很多飄浮的島。"
                    info.IconBox.Image = My.Resources.Isles
                Case 2
                    info.InfomationBox.Text = "一個由漆黑巨大的洞穴以及巨大的天空島世界組成的世界。"
                    info.IconBox.Image = My.Resources.Delight
                Case 3
                    info.InfomationBox.Text = "一個由高聳陡峭的山和眾多的架空層，懸崖還有一些巨大的山洞組成的世界。。"
                    info.IconBox.Image = My.Resources.Madness
                Case 4
                    info.InfomationBox.Text = "一個低海平面和小型湖泊以及大面積的礫石組成的世界。"
                    info.IconBox.Image = My.Resources.Drought
                Case 5
                    info.InfomationBox.Text = "擁有許多泉水的巨大山洞的世界。山洞有很多延伸至地表的縫隙和孔道所以有陽光射入並因此洞內有植被存在，洞頂還有很多奇異的結構。有時在靠近地表的洞中會有很多水，儘管這是一個低海拔的世界。天空島世界也是很常見的，儘管比天空島世界世界和放大化世界出現頻率更少。"
                    info.IconBox.Image = My.Resources.Chaos
                Case 6
                    info.InfomationBox.Text = "這是個岩石、礫石的世界，擁有熔岩海以及乾枯的河床，幾乎或沒有植物和動物。"
                    info.IconBox.Image = My.Resources.Luck
            End Select
        End If
    End Sub

    Private Sub GeneratorMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CreatedForm.Add(Me)
        ShowInTaskbar = MiniState = 0

    End Sub

    Private Sub GeneratorMapping_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CreatedForm.Remove(Me)

    End Sub
End Class