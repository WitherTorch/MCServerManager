Public Class UPnPMapper
    Dim registerPort As New List(Of Integer)
    Friend CanUPnP As Boolean = False
    Public Function Init() As Boolean
        Try
            CanUPnP = UPnP.NAT.Discover
        Catch ex As Exception
            CanUPnP = False
        End Try
        Return CanUPnP
    End Function
    Public Function PortForward(port As Integer, description As String) As Boolean
        If registerPort.Contains(port) Then
            Return False
        Else
            Try
                UPnP.NAT.ForwardPort(port, Net.Sockets.ProtocolType.Tcp, description)
                registerPort.Add(port)
                Return True
            Catch ex As Net.WebException
                If CType(ex.Response, Net.HttpWebResponse).StatusCode = Net.HttpStatusCode.InternalServerError Then
                    registerPort.Add(port)
                    Return True
                Else
                    Throw ex
                End If
            End Try

        End If

    End Function
    Public Function ExternalIP() As String
        Return UPnP.NAT.GetExternalIP().ToString
    End Function
    Public Sub DestroyPort(port As Integer)
        If registerPort.Contains(port) Then
            UPnP.NAT.DeleteForwardingRule(port, Net.Sockets.ProtocolType.Tcp)
            registerPort.Remove(port)
        End If
    End Sub
    Protected Overrides Sub Finalize()
        For Each port In registerPort
            DestroyPort(port)
        Next
        MyBase.Finalize()
    End Sub
End Class
