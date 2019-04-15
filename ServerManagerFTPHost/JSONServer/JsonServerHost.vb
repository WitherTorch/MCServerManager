Imports System.Net.Sockets
Imports System.Threading

Namespace JsonServer
    Public Class JsonServerHost
        Implements IDisposable
        Friend Shared homePath As String
        Friend Shared OnlineUserList As New List(Of FTPServerLib.User)
        Dim Listener As Net.Sockets.TcpListener
        Dim ConnectionList As New List(Of JsonServerConnection)
        Public ReadOnly Property Connections As JsonServerConnection()
            Get
                Return ConnectionList.ToArray
            End Get
        End Property
        Public ReadOnly Property Disposed As Boolean = False
        Public ReadOnly Property Listening As Boolean = False
        Sub New(port As Integer, path As String)
            Me.New(Net.IPAddress.Any, port, path)
        End Sub
        Sub New(address As Net.IPAddress, port As Integer, path As String)
            homePath = path
            Listener = New TcpListener(address, port)
        End Sub
        Sub Start()
            _Listening = True
            Listener.BeginAcceptTcpClient(AddressOf HandleAcceptTcpClient, Listener)
        End Sub
        Private Sub HandleAcceptTcpClient(result As IAsyncResult)

            If _Listening Then
                Listener.BeginAcceptTcpClient(AddressOf HandleAcceptTcpClient, Listener)
                Dim client As TcpClient = Listener.EndAcceptTcpClient(result)
                Dim connection As New JsonServerConnection(client)
                ConnectionList.Add(connection)
                ThreadPool.QueueUserWorkItem(AddressOf connection.HandleClient, client)
            End If
        End Sub
        Public Sub [Stop]()
            _Listening = False
            Listener.Stop()
            Listener = Nothing
        End Sub

#Region "IDisposable Support"
        Private disposedValue As Boolean ' 偵測多餘的呼叫

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 處置受控狀態 (受控物件)。
                    [Stop]()
                    For Each connection As JsonServerConnection In ConnectionList
                        connection.Dispose()
                    Next
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