Imports Newtonsoft.Json.Linq

Namespace JsonServer.Messages
    Class AddServer
        Implements IJsonMessage
        Public Property ServerVersionType As Enums.ServerVersionType
        Public Property ServerName As String
        Public Property ServerVersion As String
        Public Property Server2ndVersion As String
        Public Property SpongeVersionType As String
        Public Property Server3rdVersion As String
        Public Property ServerOptions As New Dictionary(Of String, String)
        Sub New(_object As JObject)
            Input(_object)
        End Sub
        Public Sub Input(_object As JObject) Implements IJsonMessage.Input
            InputPropertyValue(_object, "name", ServerName)
            InputPropertyValue(_object, "version", ServerVersion)
            InputPropertyValue(_object, "version2", Server2ndVersion)
            InputPropertyValue(_object, "version3", Server3rdVersion)
            InputPropertyValue(_object, "spongeType", SpongeVersionType)
            Dim type As Integer = 0
            InputPropertyValue(_object, "versionType", type)
            ServerVersionType = type
            Dim optionJSONObject = GetJsonObject(_object, "options")
            For Each optionProperty In optionJSONObject.Properties
                ServerOptions.Add(optionProperty.Name, optionProperty.Value)
            Next
        End Sub
        Public Function Output() As JObject Implements IJsonMessage.Output
            Dim _object As New JObject
            SetPropertyValue(_object, "name", ServerName)
            SetPropertyValue(_object, "version", ServerVersion)
            SetPropertyValue(_object, "version2", Server2ndVersion)
            SetPropertyValue(_object, "version3", Server3rdVersion)
            SetPropertyValue(_object, "spongeType", SpongeVersionType)
            Dim type As Integer = 0
            SetPropertyValue(_object, "versionType", type)
            ServerVersionType = type
            Dim optionJSONObject As New JObject
            For Each [option] In ServerOptions
                optionJSONObject.Add([option].Key, [option].Value)
            Next
            _object.Add("options", optionJSONObject)
            Return _object
        End Function

        Public Sub Start() Implements IJsonMessage.Start

        End Sub
    End Class
End Namespace
