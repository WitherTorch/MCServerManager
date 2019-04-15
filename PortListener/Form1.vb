Imports System.Net.Sockets
Imports System.Text
Imports PcapDotNet.Core
Imports PcapDotNet.Packets
Public Class Form1
    Dim tcpListener As TcpListener
    Dim tcpClient As TcpClient
    Dim ClientClosed As Boolean = False
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Task.Run(New Action(Sub()
                                Dim allDevices As IList(Of LivePacketDevice) = LivePacketDevice.AllLocalMachine

                                If allDevices.Count = 0 Then
                                    AddText("No interfaces found! Make sure WinPcap is installed.")
                                    Return
                                End If

                                Dim i As Integer = 0

                                While i <> allDevices.Count
                                    Dim device As LivePacketDevice = allDevices(i)
                                    Console.Write((i + 1) & ". " & device.Name)

                                    If device.Description IsNot Nothing Then
                                        AddText(" (" & device.Description & ")")
                                    Else
                                        AddText(" (No description available)")
                                    End If

                                    i += 1
                                End While

                                Dim deviceIndex As Integer = 0

                                Do
                                    AddText("Enter the interface number (1-" & allDevices.Count & "):")
                                    Dim deviceIndexString As String = 1

                                    If Not Integer.TryParse(deviceIndexString, deviceIndex) OrElse deviceIndex < 1 OrElse deviceIndex > allDevices.Count Then
                                        deviceIndex = 0
                                    End If
                                Loop While deviceIndex = 0

                                Dim selectedDevice As PacketDevice = allDevices(deviceIndex - 1)

                                Using communicator As PacketCommunicator = selectedDevice.Open(25565, PacketDeviceOpenAttributes.Promiscuous, 1000)
                                    AddText("Listening on " & selectedDevice.Description & "...")
                                    Dim packet As Packet

                                    Do
                                        Dim result As PacketCommunicatorReceiveResult = communicator.ReceivePacket(packet)

                                        Select Case result
                                            Case PacketCommunicatorReceiveResult.Timeout
                                                Continue Do
                                            Case PacketCommunicatorReceiveResult.Ok
                                                Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") & " length:" + packet.Length)
                                                AddText(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") & " length:" + packet.Length)
                                            Case Else
                                                Throw New InvalidOperationException("The result " & result & " shoudl never be reached here")
                                        End Select
                                        Threading.Thread.Sleep(500)
                                    Loop While True
                                End Using
                            End Sub))
    End Sub
    Sub closec()
        TcpClient.Close()
        tcpListener.Stop()
        ClientClosed = True
        ListBox1.Items.Clear()
    End Sub
    Sub AddText(str As String)
        BeginInvoke(New Action(Sub() ListBox1.Items.Add(str)))
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        closec()
    End Sub
End Class
