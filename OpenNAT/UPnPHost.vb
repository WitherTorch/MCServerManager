Imports System.Net
Imports System.Threading.Tasks

Public Class UPnPHost
    Public CanUPnP As Boolean = False
    Dim _device As Open.Nat.NatDevice
    Dim discoverer As New Open.Nat.NatDiscoverer()
    Sub New()
        _device = discoverer.DiscoverDeviceAsync(Open.Nat.PortMapper.Upnp, Nothing).Result
    End Sub
    Function CheckPort(port As Integer) As Boolean
        Dim result As Boolean = False
        For Each mapping In _device.GetAllMappingsAsync().Result
            result = (mapping.PrivatePort = port)
            If result = False Then
                result = (mapping.PublicPort = port)
            End If
            If result = True Then Exit For
        Next
        Return result
    End Function

End Class

