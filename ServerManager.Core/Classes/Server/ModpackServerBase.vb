Imports System.Drawing
Imports ServerManager

Public MustInherit Class ModpackServerBase
    Implements Taskable, IMemoryChange
    Public Property MemoryMax As Integer Implements IMemoryChange.MemoryMax
    Public Property MemoryMin As Integer Implements IMemoryChange.MemoryMin
#Region "Events"
    Public Event Initallised()
    Public Event ServerInfoUpdated()
    Public Event ServerIconUpdated()
    Public Event ServerDownloadStart()
    Public Event ServerDownloading(percent As Integer)
    Public Event ServerDownloadEnd(isCanceled As Boolean)
    Protected Sub OnInitallised()
        RaiseEvent Initallised()
    End Sub
    Protected Sub OnServerInfoUpdated()
        RaiseEvent ServerInfoUpdated()
    End Sub
    Protected Sub OnServerIconUpdated()
        RaiseEvent ServerIconUpdated()
    End Sub
    Protected Sub OnServerDownloadStart()
        RaiseEvent ServerDownloadStart()
    End Sub
    Protected Sub OnServerDownloading(percent As Integer)
        RaiseEvent ServerDownloading(percent)
    End Sub
    Protected Sub OnServerDownloadEnd(isCanceled As Boolean)
        RaiseEvent ServerDownloadEnd(isCanceled)
    End Sub
#End Region
#Region "Properties"
    Dim _ServerVersion As String
    Dim _ServerPath As String
    ''' <summary>
    ''' 伺服器的資料夾路徑
    ''' </summary>
    ''' <returns></returns>
    Public Property ServerPath As String
        Get
            Return _ServerPath
        End Get
        Protected Set(value As String)
            _ServerPath = value
        End Set
    End Property
    Dim _ServerPathName As String
    ''' <summary>
    ''' 伺服器的資料夾名稱
    ''' </summary>
    ''' <returns></returns>
    Public Property ServerPathName As String
        Get
            Return _ServerPathName
        End Get
        Protected Set(value As String)
            _ServerPathName = value
        End Set
    End Property
    Dim _ServerIcon As Image = New Bitmap(64, 64)
    ''' <summary>
    ''' 伺服器的圖示
    ''' </summary>
    ''' <returns></returns>
    Public Property ServerIcon As Image
        Get
            Return _ServerIcon
        End Get
        Protected Set(value As Image)
            _ServerIcon = value
        End Set
    End Property
    Public Property ServerTasks As ServerTask() Implements Taskable.ServerTasks
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

End Class