Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json
Public Class MinecraftUUIDProvider
    Const UUIDApiRoot As String = "http://tools.glowingmines.eu/convertor/nick/"
    Shared Function GetUUID(username As String) As Guid
        Try
            Dim client As New Net.WebClient()
            Dim jsonObject As JObject = JsonConvert.DeserializeObject(Of JObject)(client.DownloadString(UUIDApiRoot & username))
            Return New Guid(jsonObject.GetValue("offlinesplitteduuid").ToString)
        Catch ex As Exception
            Return Guid.Empty
        End Try
    End Function
End Class
