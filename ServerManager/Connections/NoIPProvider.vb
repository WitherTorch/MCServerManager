Imports NoIP
Public Class NoIPProvider
    Dim client As DDNS.Client
    Friend _username As String
    Friend _password As String
    Friend _targets As IList(Of String)
    Sub New(username As String, password As String)
        Me._username = username
        Me._password = password
        client = New DDNS.Client(New DDNS.UserAgent("Minecraft Server Manager"))
        Try
            client.Register(username, password)
        Catch ex As Exception

        End Try
        If client.IsRegistered = False Then Throw New HasNotRegisteredException()
    End Sub
    Function GetAllHosts() As Dictionary(Of DDNS.DTO.Zone, DDNS.DTO.Host())
        Dim hostDict As New Dictionary(Of DDNS.DTO.Zone, DDNS.DTO.Host())
        Try
            If client.GetZones IsNot Nothing Then
                For Each zone In client.GetZones
                    hostDict.Add(zone, client.GetHosts(zone).ToArray)
                Next
            End If
        Catch ex As Exception
            Console.WriteLine("An Exception Catched!")
            Console.WriteLine("Type=" & ex.GetType.ToString)
            Console.WriteLine("Message=" & ex.Message)
            Console.WriteLine("StackTrace=" & ex.StackTrace)
        End Try
        Return hostDict
    End Function
    Sub TargetHosts(hosts As IList(Of String))
        _targets = hosts
    End Sub
    Sub UpdateHosts()
        If _targets IsNot Nothing Then
            For Each hostname In _targets
                Dim url = String.Format("https://dynupdate.no-ip.com/nic/update?hostname={0}&myip={1}", hostname, Manager.GetExternalIP)
                Dim request As Net.HttpWebRequest = Net.HttpWebRequest.Create(url)
                request.Host = "dynupdate.no-ip.com"
                request.Credentials = New Net.NetworkCredential(_username, _password)
                request.UserAgent = client.UserAgent.ToString & " ramdom" & New Random().Next & "@email.com"
                request.Method = "GET"
                request.ProtocolVersion = New Version(1, 0)
                request.Headers.Add("Authorization: Basic " & Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_username & ":" & _password)))
                Try
                    Using response As Net.HttpWebResponse = request.GetResponse
                        Console.WriteLine(New IO.StreamReader(response.GetResponseStream).ReadToEnd)
                    End Using
                Catch ex As Exception
                    Console.WriteLine("An Exception Catched!")
                    Console.WriteLine("Type=" & ex.GetType.ToString)
                    Console.WriteLine("Message=" & ex.Message)
                    Console.WriteLine("StackTrace=" & ex.StackTrace)
                End Try
            Next
        End If
    End Sub
    Class HasNotRegisteredException
        Inherits Exception
    End Class
End Class
