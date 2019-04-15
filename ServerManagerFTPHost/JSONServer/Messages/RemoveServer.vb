Imports Newtonsoft.Json.Linq
Namespace JsonServer.Messages
    Public Class RemoveServer
        Implements IJsonMessage
        Public Property ServerName As String

        Sub New(_object As JObject)
            Input(_object)
        End Sub
        Public Sub Input(_object As JObject) Implements IJsonMessage.Input
            InputPropertyValue(_object, "name", ServerName)
        End Sub

        Public Sub Start() Implements IJsonMessage.Start

        End Sub

        Public Function Output() As JObject Implements IJsonMessage.Output
            Dim _object As New JObject
            SetPropertyValue(_object, "name", ServerName)
            Return _object
        End Function

    End Class
End Namespace
