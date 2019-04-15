Imports Newtonsoft.Json.Linq
Namespace JsonServer.Messages
    Public Class Logout
        Implements IJsonMessage
        Public Property Username As String

        Sub New(_object As JObject)
            Input(_object)
        End Sub
        Public Sub Input(_object As JObject) Implements IJsonMessage.Input
            InputPropertyValue(_object, "username", Username)
        End Sub

        Public Sub Start() Implements IJsonMessage.Start
            For Each User In JsonServerHost.OnlineUserList
                If User.Username = Username Then
                    JsonServerHost.OnlineUserList.Remove(User)
                End If
            Next
        End Sub

        Public Function Output() As JObject Implements IJsonMessage.Output
            Dim _object As New JObject
            SetPropertyValue(_object, "username", Username)
            Return _object
        End Function

    End Class
End Namespace
