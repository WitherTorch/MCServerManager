Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Namespace JsonServer
    Public Class JsonServerConnection
        Implements IDisposable
        Dim RemoteEndPoint As IPEndPoint
        Public ReadOnly Property ClientIP As String
        Public ReadOnly Property ControlClient As TcpClient
        Dim ClientStream As NetworkStream
        Dim ClientReader As StreamReader
        Dim ClientWriter As StreamWriter
        Sub New(client As TcpClient)
            _ControlClient = client
        End Sub
        Public Sub HandleClient(ByVal obj As Object)
            RemoteEndPoint = CType(_ControlClient.Client.RemoteEndPoint, IPEndPoint)
            _ClientIP = RemoteEndPoint.Address.ToString()
            ClientStream = _ControlClient.GetStream()
            ClientReader = New StreamReader(ClientStream)
            ClientWriter = New StreamWriter(ClientStream)
            Try
                Dim text As String = ClientReader.ReadToEnd
                If text IsNot Nothing Then
                    Dim inProtocol = JsonConvert.DeserializeObject(Of Protocol.JsonProtocol_In)(text)
                    Select Case inProtocol.MessageType
                        Case Protocol.JsonProtocol_In.MessageTypeEnum.AddServer
                            inProtocol.Message = New Messages.AddServer(GetJsonObject(JsonConvert.DeserializeObject(Of JObject)(text), "message"))
                        Case Protocol.JsonProtocol_In.MessageTypeEnum.RemoveServer
                            inProtocol.Message = New Messages.RemoveServer(GetJsonObject(JsonConvert.DeserializeObject(Of JObject)(text), "message"))
                    End Select
                    Try
                        inProtocol.Message.Start()
                    Catch ex As Exceptions.JsonServerException
                        Dim out = New Protocol.JsonProtocol_Out() With {
                                               .MessageType = Protocol.JsonProtocol_Out.MessageTypeEnum.Error,
                                               .MessageVersion = 1,
                                               .Message = New Messages.Error(ex.JsonErrorCode)}
                        Dim jsonObject = JsonConvert.DeserializeObject(Of JObject)(JsonConvert.SerializeObject(out))
                        jsonObject.Add("message", out.Message.Output)
                        ClientWriter.WriteLine(JsonConvert.SerializeObject(jsonObject))
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception
            End Try
            Dispose()
        End Sub
#Region "IDisposable Support"
        Private disposedValue As Boolean ' 偵測多餘的呼叫

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 處置受控狀態 (受控物件)。
                    If _ControlClient IsNot Nothing Then
                        _ControlClient.Close()
                    End If
                End If
                ' TODO: 釋放非受控資源 (非受控物件) 並覆寫下方的 Finalize()。
                ' TODO: 將大型欄位設為 null。
            End If
            disposedValue = True
        End Sub

        ' TODO: 只有當上方的 Dispose(disposing As Boolean) 具有要釋放非受控資源的程式碼時，才覆寫 Finalize()。
        'Protected Overrides Sub Finalize()
        '    ' 請勿變更這個程式碼。請將清除程式碼放在上方的 Dispose(disposing As Boolean) 中。
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' Visual Basic 加入這個程式碼的目的，在於能正確地實作可處置的模式。
        Public Sub Dispose() Implements IDisposable.Dispose
            ' 請勿變更這個程式碼。請將清除程式碼放在上方的 Dispose(disposing As Boolean) 中。
            Dispose(True)
            ' TODO: 覆寫上列 Finalize() 時，取消下行的註解狀態。
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
End Namespace