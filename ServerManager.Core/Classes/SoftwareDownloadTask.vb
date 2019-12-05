Imports System.ComponentModel
Imports System.Net

Public Class ServerDownloadTask
    Public Event DownloadProgressChanged(percentage As Integer)
    Public Event DownloadCompleted()
    Public Event DownloadStarted()
    Public Event DownloadCanceled()
    Public Sub Cancel()
        RaiseEvent DownloadCanceled()
    End Sub
    Public Sub Download(url As String, targetFilename As String, Optional client As Net.WebClient = Nothing)
        If client Is Nothing Then client = New Net.WebClient
        AddHandler client.DownloadProgressChanged, AddressOf Client_DownloadProgressChanged
        AddHandler client.DownloadFileCompleted, AddressOf Client_DownloadFileCompleted
        RaiseEvent DownloadStarted()
        client.DownloadFileAsync(New Uri(url), targetFilename)
    End Sub
    Private Sub Client_DownloadFileCompleted(sender As Object, e As AsyncCompletedEventArgs)
        If e.Cancelled = False Then
            RaiseEvent DownloadCompleted()
        Else
            RaiseEvent DownloadCanceled()
        End If
        RemoveHandler CType(sender, WebClient).DownloadProgressChanged, AddressOf Client_DownloadProgressChanged
        RemoveHandler CType(sender, WebClient).DownloadFileCompleted, AddressOf Client_DownloadFileCompleted
    End Sub
    Private Sub Client_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        RaiseEvent DownloadProgressChanged(e.ProgressPercentage)
    End Sub
End Class
