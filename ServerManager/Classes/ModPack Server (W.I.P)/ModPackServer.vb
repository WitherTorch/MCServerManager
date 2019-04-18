Public Class ModPackServer
    Enum ModPackType
        [Error]
        FeedTheBeast
        AT
    End Enum
    Public ReadOnly Property PackName As String
    Public ReadOnly Property PackVersion As String
    Public ReadOnly Property PackType As ModPackType
    Public ReadOnly Property ServerPath As String
    Public Property ServerOptions As New Dictionary(Of String, String)
    Sub SetPackInfo(name As String, version As String, type As ModPackType)
        _PackName = name
        _PackVersion = version
        _PackType = type
    End Sub
    Sub SetPath(dir As String)
        _ServerPath = dir
    End Sub
    Shared Function CreateServer() As ModPackServer
        Return New ModPackServer
    End Function
    Shared Function GetServer(path As String) As ModPackServer
        Dim server As New ModPackServer
        With server
            ._ServerPath = path

        End With
        Return server
    End Function
End Class
