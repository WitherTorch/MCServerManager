Imports System.Threading

Public Class ProcessMessageHub
    Implements IDisposable
    Public Event MessageProcessed(messages As MinecraftProcessMessage())
    Dim formatedMessages As New List(Of MinecraftProcessMessage)()
    Dim bufferedMessages As New List(Of MinecraftProcessMessage)()
    Dim originalMessages As String = "" ' 視情況，可能會變超大
    Dim threadHub As New Queue(Of Thread) '處理序列，儲存未完成的序列，並指示其依照指定的順序輸出格式化後的訊息至列表
    Dim _timer As New Timer(Sub()
                                If bufferedMessages.Count > 0 Then
                                    RaiseEvent MessageProcessed(bufferedMessages.ToArray())
                                    bufferedMessages.Clear()
                                End If
                            End Sub, Nothing, 50, 50)
    Dim parser As New MinecraftLogParser
    Dim loadedMessageCount As ULong = 0
    Dim processingMessageCount As UInteger = 0
    ''' <summary>
    ''' 加入一封訊息至非同步訊息處理序列
    ''' </summary>
    ''' <param name="processLog"></param>
    Sub AddMessage(processLog As String)
        Dim id = loadedMessageCount
        If processLog = Nothing Then Exit Sub
        originalMessages &= processLog & vbNewLine
        Dim thread As New Thread(Sub()
                                     processingMessageCount += 1
                                     Do While loadedMessageCount - id > Math.Max(Math.Min(processingMessageCount, 16) / 3, 3)
                                         Thread.Sleep(50)
                                     Loop
                                     Dim _msg = parser.ToConsoleMessage(processLog, Date.Now)
                                     Dim msg As New MinecraftProcessMessage() With {.AddtionalMessage = _msg.AddtionalMessage, .BungeeCordMessageType = _msg.BungeeCordMessageType, .Message = _msg.Message, .MessageType = _msg.ServerMessageType, .MessageTypeForEvents = _msg.MessageType, .Thread = _msg.Thread, .Time = _msg.Time}
                                     Do While True
                                         Try
                                             If threadHub.First() Is thread Then
                                                 Exit Do
                                             Else
                                                 Thread.Sleep(0)
                                             End If
                                         Catch ex As InvalidOperationException
                                             Thread.Sleep(0)
                                         End Try
                                     Loop
                                     formatedMessages.Add(msg)
                                     bufferedMessages.Add(msg)
                                     processingMessageCount -= 1
                                     threadHub.Dequeue()
                                 End Sub)
        loadedMessageCount += 1
        threadHub.Enqueue(thread)
        thread.IsBackground = True
        thread.Start()
    End Sub
    Sub AddErrorMessage(processLog As String)
        Dim id = loadedMessageCount
        If processLog = Nothing Then Exit Sub
        originalMessages &= processLog & vbNewLine
        Dim thread As New Thread(Sub()
                                     processingMessageCount += 1
                                     Do While loadedMessageCount - id > Math.Max(Math.Min(processingMessageCount, 18) / 3, 3)
                                         Thread.Sleep(50)
                                     Loop
                                     Dim _msg = parser.ToConsoleMessage(processLog, Date.Now)
                                     Dim msg As New MinecraftProcessMessage() With {.AddtionalMessage = _msg.AddtionalMessage, .BungeeCordMessageType = _msg.BungeeCordMessageType, .Message = _msg.Message, .MessageType = MinecraftProcessMessage.ProcessMessageType.Error, .MessageTypeForEvents = _msg.MessageType, .Thread = _msg.Thread, .Time = _msg.Time}
                                     Do While True
                                         Try
                                             If threadHub.First() Is thread Then
                                                 Exit Do
                                             Else
                                                 Thread.Sleep(50)
                                             End If
                                         Catch ex As InvalidOperationException
                                             Thread.Sleep(0)
                                         End Try
                                     Loop
                                     formatedMessages.Add(msg)
                                     bufferedMessages.Add(msg)
                                     processingMessageCount -= 1
                                     threadHub.Dequeue()
                                 End Sub)
        loadedMessageCount += 1
        threadHub.Enqueue(thread)
        thread.IsBackground = True
        thread.Start()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 處置 Managed 狀態 (Managed 物件)。
                _timer.Dispose()
                For Each thread In threadHub
                    If thread IsNot Nothing AndAlso thread.IsAlive Then thread.Abort()
                    thread = Nothing
                Next
                threadHub.Clear()
                GC.Collect()
            End If
            originalMessages = Nothing
            formatedMessages.Clear()
            bufferedMessages.Clear()
            ' TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的 Finalize()。
            ' TODO: 將大型欄位設為 null。
        End If
        disposedValue = True
        GC.Collect()
    End Sub

    ' TODO: 只有當上方的 Dispose(disposing As Boolean) 具有要釋放 Unmanaged 資源的程式碼時，才覆寫 Finalize()。
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
Public Class MinecraftProcessMessage
    Enum ProcessMessageType
        Info
        Warning
        [Error]
        Notify
        Debug
        Trace
    End Enum
    Enum EventMessageType
        None
        PlayerLogin
        PlayerLogout
        PlayerConnected
        PlayerLostConnected
        PlayerInputCommand
    End Enum
    Public Property MessageType As ProcessMessageType
    Public Property BungeeCordMessageType As String
    Public Property MessageTypeForEvents As EventMessageType
    Public Property AddtionalMessage As New Dictionary(Of String, String)()
    Public Property Time As Date
    Public Property Message As String
    Public Property Thread As String
    Public Function ServerMessageTypeString() As String
        Select Case MessageType
            Case ProcessMessageType.Info
                Return "訊息"
            Case ProcessMessageType.Warning
                Return "警告"
            Case ProcessMessageType.Error
                Return "錯誤"
            Case ProcessMessageType.Notify
                Return "通知"
            Case ProcessMessageType.Debug
                Return "偵錯"
            Case ProcessMessageType.Trace
                Return "程式碼追朔"
        End Select
    End Function
End Class

