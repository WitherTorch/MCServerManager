Public Class NewManager
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Dim value As Integer = CPUPerformanceCounter.NextValue
        CPUCircularBar.Value = value
        CPUCircularBar.Text = value
    End Sub

    Private Sub NewManager_Load(sender As Object, e As EventArgs) Handles Me.Load
        CPUPerformanceCounter.NextValue()
    End Sub
End Class