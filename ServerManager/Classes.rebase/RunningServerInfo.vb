Public MustInherit Class RunningServerInfo
    Public Event ServerKillled(force As Boolean)
    Public Event MessageReceived(messages As MinecraftProcessMessage())
    Public UsedMemory As Long
    Public ProcessID As Integer
    Public MessageHub As New ProcessMessageHub
    Public Players As New List(Of Player)
    Public IsRunning As Boolean
    MustOverride Sub Kill(force As Boolean)
    Sub New()
        AddHandler MessageHub.MessageProcessed, Sub(messages As MinecraftProcessMessage())
                                                    RaiseEvent MessageReceived(messages)
                                                End Sub
    End Sub
    Protected Sub OnServerKilled(force As Boolean)
        RaiseEvent ServerKillled(force)
    End Sub
End Class
Friend Class ProcessRunningServerInfo
    Inherits RunningServerInfo
    Dim _process As Process
    Dim force As Boolean = False
    Sub New()
        MyBase.New()
    End Sub
    Friend Sub ChangeWatchingProcess(process As Process)
        Try
            If process.EnableRaisingEvents = False Then process.EnableRaisingEvents = True
        Catch ex As Exception

        End Try
        If _process IsNot Nothing Then
            RemoveHandler process.ErrorDataReceived, AddressOf Process_ErrorDataReceived
            RemoveHandler process.OutputDataReceived, AddressOf Process_OutputDataReceived
            RemoveHandler process.Exited, AddressOf Process_Exited
        End If
        AddHandler process.ErrorDataReceived, AddressOf Process_ErrorDataReceived
        AddHandler process.OutputDataReceived, AddressOf Process_OutputDataReceived
        AddHandler process.Exited, AddressOf Process_Exited
        IsRunning = Not process.HasExited
        _process = process
    End Sub
    Private Sub Process_Exited(sender As Object, e As EventArgs)
        IsRunning = False
        OnServerKilled(force)
    End Sub
    Private Sub Process_OutputDataReceived(sender As Object, e As DataReceivedEventArgs)
        If e.Data IsNot Nothing Then MessageHub.AddMessage(e.Data)
    End Sub
    Private Sub Process_ErrorDataReceived(sender As Object, e As DataReceivedEventArgs)
        If e.Data IsNot Nothing Then MessageHub.AddErrorMessage(e.Data)
    End Sub
    Public Overrides Sub Kill(force As Boolean)
        Me.force = force
    End Sub
End Class
