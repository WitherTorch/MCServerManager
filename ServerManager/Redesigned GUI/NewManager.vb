Public Class NewManager
    Dim winQuery As Management.ObjectQuery = New Management.ObjectQuery("SELECT * FROM CIM_OperatingSystem")
    Dim searcher As New Management.ManagementObjectSearcher(winQuery)
    Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick
        Dim CPUvalue As Integer = CPUPerformanceCounter.NextValue
        CPUCircularBar.Value = CPUvalue
        CPUCircularBar.Text = CPUvalue
        Select Case CPUvalue
            Case 100
                CPUCircularBar.SubscriptMargin = New Padding(-32, 10, 0, 0)
            Case 10 To 99
                CPUCircularBar.SubscriptMargin = New Padding(-22, 10, 0, 0)
            Case 0 To 9
                CPUCircularBar.SubscriptMargin = New Padding(-12, 10, 0, 0)
        End Select
        For Each item In searcher.Get()
            Try
                Dim RAMvalue As Integer = 100 - item("FreePhysicalMemory") / item("TotalVisibleMemorySize") * 100
                RAMCircularBar.Value = RAMvalue
                RAMCircularBar.Text = RAMvalue
                Select Case RAMvalue
                    Case 100
                        RAMCircularBar.SubscriptMargin = New Padding(-30, 10, 0, 0)
                    Case 10 To 99
                        RAMCircularBar.SubscriptMargin = New Padding(-20, 10, 0, 0)
                    Case 0 To 9
                        RAMCircularBar.SubscriptMargin = New Padding(-10, 10, 0, 0)
                End Select
            Catch ex As Exception
                Continue For
            End Try
            Try
                Dim VirtualRAMvalue As Integer = 100 - item("FreeVirtualMemory") / item("TotalVirtualMemorySize") * 100
                VRAMCircularBar.Value = VirtualRAMvalue
                VRAMCircularBar.Text = VirtualRAMvalue
                Select Case VirtualRAMvalue
                    Case 100
                        VRAMCircularBar.SubscriptMargin = New Padding(-30, 10, 0, 0)
                    Case 10 To 99
                        VRAMCircularBar.SubscriptMargin = New Padding(-20, 10, 0, 0)
                    Case 0 To 9
                        VRAMCircularBar.SubscriptMargin = New Padding(-10, 10, 0, 0)
                End Select
            Catch ex As Exception
                Continue For
            End Try
            Exit For
        Next
        Dim category As New PerformanceCounterCategory("Network Interface")
        Dim names As String() = category.GetInstanceNames()
        Dim totalValue As Double = 0
        For Each name As String In names
            totalValue += getNetworkUtilization(name)
        Next
        Dim networkValue As Integer = totalValue / names.Count
        NetworkCircularBar.Value = networkValue
        NetworkCircularBar.Text = networkValue
        Select Case networkValue
            Case 100
                NetworkCircularBar.SubscriptMargin = New Padding(-32, 10, 0, 0)
            Case 10 To 99
                NetworkCircularBar.SubscriptMargin = New Padding(-22, 10, 0, 0)
            Case 0 To 9
                NetworkCircularBar.SubscriptMargin = New Padding(-12, 10, 0, 0)
        End Select
    End Sub
    Public Function getNetworkUtilization(networkCard As String) As Double
        Dim bandwidthCounter As New PerformanceCounter("Network Interface", "Current Bandwidth", networkCard)
        bandwidthCounter.NextValue()
        Dim bandwidth As Single = bandwidthCounter.NextValue()
        Dim dataSentCounter As New PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCard)
        Dim dataReceivedCounter As New PerformanceCounter("Network Interface", "Bytes Received/sec", networkCard)
        Dim sendSum As Single = 0
        Dim receiveSum As Single = 0
        For i As Integer = 1 To 10
            sendSum += dataSentCounter.NextValue()
            receiveSum += dataReceivedCounter.NextValue()
        Next
        Dim utilization As Double = (8 * (sendSum + receiveSum)) / (bandwidth * 10) * 100
        Return utilization
    End Function
    Private Sub NewManager_Load(sender As Object, e As EventArgs) Handles Me.Load
        CPUPerformanceCounter.NextValue()
    End Sub

    Private Sub ToolTip1_Draw(sender As Object, e As DrawToolTipEventArgs) Handles ToolTip1.Draw
        e.DrawBorder()
        e.DrawBackground()
        e.Graphics.DrawRectangle(New Pen(Color.FromArgb(100, 100, 100)), e.Bounds)
        e.DrawText()
    End Sub
End Class