Imports System.Threading.Tasks
Public Class CloudFlareProvider
    Const CloudFlare_Base_URL As String = "https://api.cloudflare.com/client/v4/"
    Sub test()
        Try
            Dim request As Net.HttpWebRequest = Net.WebRequest.Create(CloudFlare_Base_URL & "zones")
            request.Headers.Add("X-Auth-Email", "new1271@outlook.com")
            request.Headers.Add("X-Auth-Key", "f68f72483c275cd5e58eae3edea35a3395705")
            request.ContentType = "application/json"
            Dim response As Net.HttpWebResponse = request.GetResponse()
            If response.StatusCode = Net.HttpStatusCode.OK Then
                Dim reader As New IO.StreamReader(response.GetResponseStream)
                Console.WriteLine(reader.ReadToEnd)
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
