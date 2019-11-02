Imports ServerManager

Public Class MyOpenFileDIalog
    Implements IOpenFileDialog
    Dim dialog As New OpenFileDialog
    Public Property Filter As String Implements IOpenFileDialog.Filter
        Get
            Return dialog.Filter
        End Get
        Set(value As String)
            dialog.Filter = value
        End Set
    End Property

    Public Property FileName As String Implements IOpenFileDialog.FileName
        Get
            Return dialog.FileName
        End Get
        Set(value As String)
            dialog.FileName = value
        End Set
    End Property

    Public Property Title As String Implements IOpenFileDialog.Title
        Get
            Return dialog.Title
        End Get
        Set(value As String)
            dialog.Title = value
        End Set
    End Property

    Public Function ShowDialog() As DialogResult Implements IOpenFileDialog.ShowDialog
        Return dialog.ShowDialog()
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 偵測多餘的呼叫

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 處置 Managed 狀態 (Managed 物件)。
                dialog.Dispose()
            End If
            ' TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的 Finalize()。
            ' TODO: 將大型欄位設為 null。
        End If
        disposedValue = True
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
