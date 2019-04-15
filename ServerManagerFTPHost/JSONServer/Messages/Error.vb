Imports Newtonsoft.Json.Linq
Namespace JsonServer.Messages
    Public Class [Error]
        Implements IJsonMessage
        Public Property ErrorCode As Enums.ErrorCodes = Enums.ErrorCodes.Unknown
        Sub New(Optional code As Enums.ErrorCodes = Enums.ErrorCodes.Unknown)
            ErrorCode = code
        End Sub
        Public Sub Input(_object As JObject) Implements IJsonMessage.Input

        End Sub

        Public Sub Start() Implements IJsonMessage.Start

        End Sub

        Public Function Output() As JObject Implements IJsonMessage.Output
            Dim _object As New JObject
            _object.Add("code", CInt(ErrorCode))
            Return _object
        End Function
    End Class
End Namespace
