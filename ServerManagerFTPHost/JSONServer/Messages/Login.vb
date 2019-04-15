Imports Newtonsoft.Json.Linq

Namespace JsonServer.Messages

    Public Class Login
        Implements IJsonMessage
        Public Property Username As String
        Public Property Password As String

        Sub New(_object As JObject)
            Input(_object)
        End Sub
        Public Sub Input(_object As JObject) Implements IJsonMessage.Input
            InputPropertyValue(_object, "username", Username)
            InputPropertyValue(_object, "password", Password)
        End Sub

        Public Sub Start() Implements IJsonMessage.Start
            Dim user = FTPServerLib.UserStore.Validate(Username, Password)
            If user Is Nothing Then
                Throw New Exceptions.JsonServerException(Enums.ErrorCodes.LoginErrorByUser)
            ElseIf JsonServerHost.OnlineUserList.Contains(user) Then
                Throw New Exceptions.JsonServerException(Enums.ErrorCodes.LoginErrorByAlreadyLogin)
            Else
                JsonServerHost.OnlineUserList.Add(user)
            End If
        End Sub

        Public Function Output() As JObject Implements IJsonMessage.Output
            Dim _object As New JObject
            SetPropertyValue(_object, "username", Username)
            SetPropertyValue(_object, "password", Password)
            Return _object
        End Function
    End Class
End Namespace
