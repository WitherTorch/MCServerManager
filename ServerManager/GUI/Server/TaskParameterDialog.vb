Imports System.ComponentModel

Public Class TaskParameterDialog
    Dim _owner As ServerTaskCreateDialog
    Sub New(owner As ServerTaskCreateDialog)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _owner = owner
    End Sub
    Private Sub TaskParameterDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShowInTaskbar = MiniState = 0
        CreatedForm.Add(Me)

    End Sub

    Private Sub ListView1_ItemActivate(sender As Object, e As EventArgs) Handles ListView1.ItemActivate
        _owner.RunCommandArgBox.Text &= ListView1.SelectedItems(0).Text
        Close()
    End Sub

    Private Sub TaskParameterDialog_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        CreatedForm.Remove(Me)

    End Sub
End Class