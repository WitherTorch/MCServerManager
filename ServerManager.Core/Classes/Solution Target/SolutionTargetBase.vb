Imports System.Drawing
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public MustInherit Class SolutionTargetBase
#Region "Events"
    Public Event Initallised()
    Public Event SolutionInfoUpdated()
    Public Event TargetDownloadStart()
    Public Event TargetDownloading(percent As Integer)
    Public Event TargetDownloadEnd(isCanceled As Boolean)
    Protected Sub OnInitallised()
        RaiseEvent Initallised()
    End Sub
    Protected Sub OnSolutionInfoUpdated()
        RaiseEvent SolutionInfoUpdated()
    End Sub
    Protected Sub OnTargetDownloadStart()
        RaiseEvent TargetDownloadStart()
    End Sub
    Protected Sub OnTargetDownloading(percent As Integer)
        RaiseEvent TargetDownloading(percent)
    End Sub
    Protected Sub OnTargetDownloadEnd(isCanceled As Boolean)
        RaiseEvent TargetDownloadEnd(isCanceled)
    End Sub
#End Region
#Region "Properties"
    Dim _SolutionVersion As String
    ''' <summary>
    ''' 方案的目標整合器版本
    ''' </summary>
    ''' <returns></returns>
    Public Property SolutionTargetVersion As String
        Get
            Return _SolutionVersion
        End Get
        Protected Set(value As String)
            _SolutionVersion = value
        End Set
    End Property
    Dim _SolutionPath As String
    ''' <summary>
    ''' 方案的資料夾路徑
    ''' </summary>
    ''' <returns></returns>
    Public Property SolutionPath As String
        Get
            Return _SolutionPath
        End Get
        Set(value As String)
            _SolutionPath = value
        End Set
    End Property
    Dim _SolutionPathName As String
    ''' <summary>
    ''' 方案的資料夾名稱
    ''' </summary>
    ''' <returns></returns>
    Public Property SolutionPathName As String
        Get
            Return _SolutionPathName
        End Get
        Protected Set(value As String)
            _SolutionPathName = value
        End Set
    End Property
    Dim _servers As New List(Of ServerBase)
    Public ReadOnly Property Servers As ServerBase()
        Get
            Return _servers.ToArray()
        End Get
    End Property
    Private _IsRunning As Boolean
    Public Property IsRunning As Boolean
        Get
            Return _IsRunning
        End Get
        Set(value As Boolean)
            _IsRunning = value
            RaiseEvent SolutionInfoUpdated()
        End Set
    End Property
    Public Property ProcessID As Long = 0
#End Region
#Region "Must Override Function/Sub"
    ''' <summary>
    ''' 決定方案目標有無可用的軟體更新。
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function CanUpdate() As Boolean
    ''' <summary>
    ''' 傳回目標軟體的執行檔案名稱
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetTargetFileName() As String
    ''' <summary>
    ''' 傳回目標軟體的內部名稱
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetInternalName() As String
    ''' <summary>
    ''' 取得目標軟體版本的字串形式
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetSoftwareVersionString() As String
    ''' <summary>
    ''' 取得目標軟體所有的可用設定物件
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetOptionObjects() As AbstractSoftwareOptions()
    ''' <summary>
    ''' 取得目標軟體的最新可用版本
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetAvailableVersion() As String
    Public MustOverride Function GetAdditionalSolutionInfo() As String()
    ''' <summary>
    ''' 在啟動方案前要做的事(通常是檢查項目)
    ''' </summary>
    Public Overridable Function BeforeStartSoluton() As Boolean
    End Function
    ''' <summary>
    ''' 啟動方案
    ''' </summary>
    Public MustOverride Function StartSoluton() As Process
    ''' <summary>
    ''' 生成方案資訊文件(solution.info)
    ''' </summary>
    Public Sub GenerateServerInfo()
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(SolutionPath, "solution.info"), IO.FileMode.Truncate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.AutoFlush = True
            writer.WriteLine("solution-target-version=" & SolutionTargetVersion)
            writer.WriteLine("solution-target=" & GetInternalName())
            Try
                Dim additionalInfo As String() = GetAdditionalSolutionInfo()
                If additionalInfo IsNot Nothing Then
                    For Each item In additionalInfo
                        writer.WriteLine(item)
                    Next
                End If
            Catch ex As Exception

            End Try
            Dim jsonArray As New JArray
            writer.Flush()
            writer.Close()
        End Using
    End Sub
    ''' <summary>
    ''' 儲存方案
    ''' </summary>
    Public MustOverride Sub SaveSolution()
    ''' <summary>
    ''' 關閉方案
    ''' </summary>
    Public MustOverride Sub ShutdownSolution()
    ''' <summary>
    ''' 下載方案目標
    ''' </summary>
    Public MustOverride Function DownloadAndInstallTarget(targetServerVersion As String) As ServerDownloadTask
    ''' <summary>
    ''' 更新方案目標
    ''' </summary>
    Public MustOverride Function UpdateTarget() As ServerDownloadTask
    Public Sub New()
    End Sub
    Public Sub GetSolution(solutionPath As String)
        _SolutionPath = solutionPath
        _SolutionPathName = IO.Path.GetDirectoryName(solutionPath)
        GetSolution()
    End Sub
    Overridable Sub ReloadSolution()
        GetSolution()
    End Sub
    Protected MustOverride Sub OnReadSolutionInfo(key As String, value As String)
    Protected Overridable Sub GetSolution()
        If SolutionPath <> "" Then
            Try
                Dim taskList As New List(Of ServerTask)
                If IO.File.Exists(IO.Path.Combine(SolutionPath, "solution.info")) Then
                    Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(SolutionPath, "solution.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                        Do Until reader.EndOfStream
                            Dim infoText As String = reader.ReadLine
                            Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                            If info.Length >= 2 Then
                                Select Case info(0)
                                    Case "solution-target-version"
                                        SolutionTargetVersion = info(1)
                                    Case Else
                                        OnReadSolutionInfo(info(0), info(1))
                                End Select
                            End If
                        Loop
                    End Using
                End If
            Catch ex As IO.FileNotFoundException
                Throw ex
            End Try
            SolutionPathName = (New IO.DirectoryInfo(SolutionPath)).Name
        Else
            Throw New GetServerException
        End If
    End Sub
    Friend Shared Function GetServerTypeString(path As String) As String
        If path <> "" Then
            Try
                Dim taskList As New List(Of ServerTask)
                If IO.File.Exists(IO.Path.Combine(path, "solution.info")) Then
                    Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(path, "solution.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                        Do Until reader.EndOfStream
                            Dim infoText As String = reader.ReadLine
                            If infoText.StartsWith("solution-target") Then
                                Return infoText.Substring(16)
                            End If
                        Loop
                    End Using
                End If
            Catch ex As IO.FileNotFoundException
                Throw New GetServerException
            End Try
        Else
            Throw New GetServerException
        End If
    End Function
    Protected Overridable Sub AddServer(ByRef server As ServerBase)
        _servers.Add(server)
    End Sub
    Protected Overridable Sub RemoveServer(ByRef server As ServerBase)
        _servers.Remove(server)
    End Sub
    Protected Overridable Function GetServerFilter(server As ServerBase) As Boolean
        Return True
    End Function
#End Region
End Class
