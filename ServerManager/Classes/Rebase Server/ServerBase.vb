Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public MustInherit Class ServerBase
    Implements Taskable
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
    ''' <summary>
    ''' 伺服器的軟體版本
    ''' </summary>
    ''' <returns></returns>
    Public Property ServerVersion As String
        Get
            Return _ServerVersion
        End Get
        Protected Set(value As String)
            _ServerVersion = value
        End Set
    End Property
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
    ''' <summary>
    ''' 伺服器的圖示
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ServerIcon As Image = New Bitmap(64, 64)
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
    ''' 傳回伺服器軟體的內部名稱
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetInternalName() As String
    ''' <summary>
    ''' 傳回傳回可讀性高的伺服器軟體名稱
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetReadableName() As String
    ''' <summary>
    ''' 取得伺服器軟體版本的字串形式
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetSoftwareVersionString() As String
    Public MustOverride Function GetAdditionalServerInfo() As String()
    ''' <summary>
    ''' 啟動伺服器
    ''' </summary>
    Public MustOverride Function RunServer() As Process
    ''' <summary>
    ''' 生成伺服器資訊文件(server.info)
    ''' </summary>
    Public Sub GenerateServerInfo()
        My.Computer.FileSystem.WriteAllText(IO.Path.Combine(ServerPath, "server.info"), "", False)
        Using writer As New IO.StreamWriter(New IO.FileStream(IO.Path.Combine(ServerPath, "server.info"), IO.FileMode.Truncate, IO.FileAccess.Write), System.Text.Encoding.UTF8)
            writer.AutoFlush = True
            writer.WriteLine("server-version=" & ServerVersion)
            writer.WriteLine("server-version-type=" & GetInternalName())
            Try
                Dim additionalInfo As String() = GetAdditionalServerInfo()
                If additionalInfo IsNot Nothing Then
                    For Each item In additionalInfo
                        writer.WriteLine(item)
                    Next
                End If
            Catch ex As Exception

            End Try
            Dim jsonArray As New JArray
            If IsNothing(ServerTasks) = False Then
                For Each task As ServerTask In ServerTasks
                    Dim jsonObject As New JObject
                    jsonObject.Add("mode", task.Mode)
                    jsonObject.Add("name", task.Name)
                    jsonObject.Add("enabled", task.Enabled)
                    Select Case task.Mode
                        Case ServerTask.TaskMode.Repeating
                            jsonObject.Add("period", task.RepeatingPeriod)
                            jsonObject.Add("periodUnit", task.RepeatingPeriodUnit)
                        Case ServerTask.TaskMode.Trigger
                            jsonObject.Add("event", task.TriggerEvent)
                            Select Case task.TriggerEvent
                                Case ServerTask.TaskTriggerEvent.PlayerInputCommand
                                    jsonObject.Add("regex", task.CheckRegex)
                            End Select
                    End Select
                    Dim command As New JObject
                    Dim taskCommand As ServerTask.TaskCommand = task.Command
                    command.Add("action", taskCommand.Action)
                    command.Add("data", taskCommand.Data)
                    jsonObject.Add("command", command)
                    jsonArray.Add(jsonObject)
                Next
                writer.WriteLine("tasks=" & JsonConvert.SerializeObject(jsonArray))
            Else
                writer.WriteLine("tasks=" & "[]")
            End If
            writer.Flush()
            writer.Close()
        End Using
    End Sub
    ''' <summary>
    ''' 儲存伺服器
    ''' </summary>
    Public MustOverride Sub SaveServer()
    ''' <summary>
    ''' 關閉伺服器
    ''' </summary>
    Public MustOverride Sub ShutdownServer()
    ''' <summary>
    ''' 下載伺服器
    ''' </summary>
    Public MustOverride Function DownloadServer() As ServerDownloadTask
    ''' <summary>
    ''' 更新伺服器
    ''' </summary>
    Public MustOverride Sub UpdateServer()
    Public Sub New()
    End Sub
    Public Sub New(serverPath As String)
        _ServerPath = serverPath
        _ServerPathName = IO.Path.GetDirectoryName(serverPath)
    End Sub
    Protected Sub OnReadServerInfo(key As String, value As String)

    End Sub
    Friend Overloads Sub GetServer()
        If ServerPath <> "" Then
            Try
                Dim taskList As New List(Of ServerTask)
                If My.Computer.FileSystem.FileExists(IO.Path.Combine(ServerPath, "server.info")) Then
                    Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(ServerPath, "server.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                        Do Until reader.EndOfStream
                            Dim infoText As String = reader.ReadLine
                            Dim info = infoText.Split("=", 2, StringSplitOptions.None)
                            If info.Length >= 2 Then
                                Select Case info(0)
                                    Case "server-version"
                                        ServerVersion = info(1)
                                    Case "tasks"
                                        Dim jsonArray As JArray = JsonConvert.DeserializeObject(Of JArray)(info(1))
                                        For Each jsonObject As JObject In jsonArray
                                            Dim task As New ServerTask(CInt(jsonObject.GetValue("mode")), jsonObject.GetValue("name"))
                                            task.Enabled = jsonObject.GetValue("enabled")
                                            Select Case task.Mode
                                                Case ServerTask.TaskMode.Repeating
                                                    task.RepeatingPeriod = jsonObject.GetValue("period")
                                                    task.RepeatingPeriodUnit = CInt(jsonObject.GetValue("periodUnit"))
                                                Case ServerTask.TaskMode.Trigger
                                                    task.TriggerEvent = CInt(jsonObject.GetValue("event"))
                                                    task.CheckRegex = IIf(jsonObject.ContainsKey("regex"), jsonObject.GetValue("regex"), "")
                                            End Select
                                            Dim command As JObject = jsonObject.GetValue("command")
                                            Dim taskCommand As New ServerTask.TaskCommand()
                                            taskCommand.Action = CInt(command.GetValue("action"))
                                            taskCommand.Data = command.GetValue("data")
                                            task.Command = taskCommand
                                            taskList.Add(task)
                                        Next
                                    Case Else
                                        OnReadServerInfo(info(0), info(1))
                                End Select
                            End If
                        Loop
                    End Using
                End If
                ServerTasks = taskList.ToArray
            Catch ex As IO.FileNotFoundException
                Throw ex
            End Try
            If My.Computer.FileSystem.FileExists(IO.Path.Combine(ServerPath, "server-icon.png")) Then
                ServerIcon = Image.FromFile(IO.Path.Combine(ServerPath, "server-icon.png"))
            Else
                ServerIcon = My.Resources.ServerDefaultIcon
            End If
            ServerPathName = (New IO.DirectoryInfo(ServerPath)).Name
        Else
            Throw
        End If
    End Sub

#End Region
End Class
