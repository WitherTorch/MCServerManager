Public Class CustomComboBox
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        MyBase.OnPaint(e)

        '請在此處加入您自訂的繪製程式碼
    End Sub
    Public Property OutputValues As String()
    Public Property OutputMode As OutputMode
    Function GetSelectedValue()
        Select Case OutputMode
            Case OutputMode.Integer
                Return SelectedIndex
            Case OutputMode.String
                Return OutputValues(SelectedIndex)
            Case Else
                Return SelectedIndex
        End Select

    End Function
End Class
Public Enum OutputMode
    [Integer]
    [String]
End Enum

