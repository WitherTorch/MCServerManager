Imports System.Reflection

Public Class ServerCheckingForm
    Dim index As Integer
    Sub New(index As Integer)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.index = index
    End Sub
    Friend Function check() As Server
        TextBox1.Clear()
        Dim controlProperties As PropertyInfo() = GetType(Server).GetProperties(BindingFlags.[Public] Or BindingFlags.Instance)
        Dim instance As Server = GlobalModule.Manager.ServerEntityList(index)
        If instance Is Nothing Then TextBox1.AppendText("Nothing!") : Return Nothing
        For Each propInfo As PropertyInfo In controlProperties
            If propInfo.CanRead Then
                Try
                    TextBox1.AppendText(propInfo.Name & "=" & propInfo.GetValue(instance, Nothing) & vbNewLine)
                Catch ex As Exception
                End Try
            End If
        Next
        Return instance
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        check()
    End Sub
End Class