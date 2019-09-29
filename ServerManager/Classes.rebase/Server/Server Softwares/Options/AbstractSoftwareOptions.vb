Public MustInherit Class AbstractSoftwareOptions
    ''' <summary>
    ''' 載入設定用建構函式
    ''' </summary>
    ''' <param name="path"></param>
    Public Sub New(path As String)

    End Sub
    Public MustOverride Sub SaveOption()
    Public MustOverride Function GetOptionsTitle() As String
End Class
