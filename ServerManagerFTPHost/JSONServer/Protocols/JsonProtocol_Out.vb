Imports Newtonsoft

Namespace JsonServer.Protocol
    <Serializable> <Json.JsonObject(Json.MemberSerialization.OptIn)>
    Public Class JsonProtocol_Out
        <Serializable>
        Enum MessageTypeEnum
            None
            [Error]
            Infomation
            Data
        End Enum
        <Json.JsonProperty("type")>
        Public Property MessageType As MessageTypeEnum = MessageTypeEnum.None
        <Json.JsonProperty("version")>
        Public Property MessageVersion As Integer = 1
        Public Property Message As Messages.IJsonMessage
    End Class
End Namespace