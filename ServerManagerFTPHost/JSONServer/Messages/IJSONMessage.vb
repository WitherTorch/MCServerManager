Imports Newtonsoft.Json.Linq

Namespace JsonServer.Messages
    Public Interface IJsonMessage
        Sub Input(_object As JObject)
        Function Output() As JObject
        Sub Start()
    End Interface
End Namespace
