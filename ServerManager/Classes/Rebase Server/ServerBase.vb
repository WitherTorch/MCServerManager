Public MustInherit Class ServerBase
#Region "Events"
    Public Event Initallised()
    Public Event ServerInfoUpdated()
    Public Event ServerIconUpdated()
    Public Event ServerUpdateStart()
    Public Event ServerUpdating(percent As Integer)
    Public Event ServerUpdateEnd()
#End Region
#Region "Properties"
    Public ReadOnly Property ServerOptions As New Dictionary(Of String, String) ' main server options 
    Public ReadOnly Property ServerPath As String
    Public ReadOnly Property ServerPathName As String
    Public ReadOnly Property ServerIcon As Image = New Bitmap(64, 64)
    Private _IsRunning As Boolean
    Public Property IsRunning As Boolean
        Get
            Return _IsRunning
        End Get
        Set(value As Boolean)
            _IsRunning = value
            RaiseEvent ServerInfoUpdated()
        End Set
    End Property
    Public Property ProcessID As Long = 0
#End Region
#Region "Must Override Function/Sub"
    ''' <summary>
    ''' 決定伺服器有有無可用的軟體更新。
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function CanUpdate() As Boolean
    ''' <summary>
    ''' 傳回伺服器的執行檔案名稱
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetServerFileName() As String
    ''' <summary>
    ''' 啟動伺服器
    ''' </summary>
    Public MustOverride Function RunServer() As Process
    Protected MustOverride Sub SetMainServerOption()
    Public Sub New()

    End Sub
    Public Sub New(serverPath As String)
        _ServerPath = serverPath
        _ServerPathName = IO.Path.GetDirectoryName(serverPath)
        SetMainServerOption()
    End Sub
#End Region
End Class
