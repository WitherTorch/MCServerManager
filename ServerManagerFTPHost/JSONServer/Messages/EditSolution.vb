Imports Newtonsoft.Json.Linq

Namespace JsonServer.Messages
    Public Class EditSolution
        Implements IJsonMessage
        Public Sub Input(_object As JObject) Implements IJsonMessage.Input

        End Sub

        Public Sub Start() Implements IJsonMessage.Start

        End Sub

        Public Function Output() As JObject Implements IJsonMessage.Output

        End Function
    End Class
End Namespace
