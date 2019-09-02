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
        client.DownloadFileAsync(New Uri(url), targetFilename)
    End Sub
    Private Sub Client_DownloadProgressChanged(sender As Object, e As DownloadProgressChangedEventArgs)
        RaiseEvent DownloadProgressChanged(e.ProgressPercentage)
    End Sub
End Class
