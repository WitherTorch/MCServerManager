Imports System.Drawing
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
        Set(value As String)
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
#Region "Must Override Function/Sub"
    ''' <summary>
    ''' 決定伺服器有無可用的軟體更新。
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
    ''' 取得伺服器軟體版本的字串形式
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetSoftwareVersionString() As String
    ''' <summary>
    ''' 取得伺服器的server.properties
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetServerProperties() As IServerProperties
    ''' <summary>
    ''' 取得伺服器所有的可用設定物件
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetOptionObjects() As AbstractSoftwareOptions()
    ''' <summary>
    ''' 取得伺服器軟體的可用版本列表
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetAvailableVersions() As String()
    ''' <summary>
    ''' 取得伺服器軟體在指定參數下的可用版本列表
    ''' </summary>
    ''' <returns></returns>
    Public MustOverride Function GetAvailableVersions(ParamArray args As (String, String)()) As String()
    Public MustOverride Function GetAdditionalServerInfo() As String()
    ''' <summary>
    ''' 在啟動伺服器要做的事(通常是檢查項目)
    ''' </summary>
    Public Overridable Function BeforeRunServer() As Boolean
    End Function
    ''' <summary>
    ''' 啟動伺服器
    ''' </summary>
    Public MustOverride Function RunServer() As Process
    ''' <summary>
    ''' 生成伺服器資訊文件(server.info)
    ''' </summary>
    Public Sub GenerateServerInfo()
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
            If ServerTasks IsNot Nothing Then
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
    Public MustOverride Function DownloadAndInstallServer(targetServerVersion As String) As ServerDownloadTask
    ''' <summary>
    ''' 更新伺服器
    ''' </summary>
    Public MustOverride Function UpdateServer() As ServerDownloadTask
    Public Sub New()
    End Sub
    Public Sub GetServer(serverPath As String)
        _ServerPath = serverPath
        _ServerPathName = IO.Path.GetDirectoryName(serverPath)
        GetServer()
    End Sub
    Overridable Sub ReloadServer()
        GetServer()
    End Sub
    Overridable Sub ReloadServerIcon()
        ServerIcon = Image.FromFile(IO.Path.Combine(ServerPath, "server-icon.png"))
    End Sub
    Protected MustOverride Sub OnReadServerInfo(key As String, value As String)
    Protected Overridable Sub GetServer()
        If ServerPath <> "" Then
            Try
                Dim taskList As New List(Of ServerTask)
                If IO.File.Exists(IO.Path.Combine(ServerPath, "server.info")) Then
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
            If IO.File.Exists(IO.Path.Combine(ServerPath, "server-icon.png")) Then
                ServerIcon = Image.FromFile(IO.Path.Combine(ServerPath, "server-icon.png"))
            Else
                ServerIcon = Nothing
            End If
            ServerPathName = (New IO.DirectoryInfo(ServerPath)).Name
        Else
            Throw New GetServerException
        End If
    End Sub
    Friend Shared Function GetServerTypeString(path As String) As String
        If path <> "" Then
            Try
                Dim taskList As New List(Of ServerTask)
                If IO.File.Exists(IO.Path.Combine(path, "server.info")) Then
                    Using reader As New IO.StreamReader(New IO.FileStream(IO.Path.Combine(path, "server.info"), IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                        Do Until reader.EndOfStream
                            Dim infoText As String = reader.ReadLine
                            If infoText.StartsWith("server-version-type") Then
                                Return infoText.Substring(20)
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
    Protected Sub GenerateServerEULA()
        Dim fs As IO.FileStream
        If IO.File.Exists(IO.Path.Combine(ServerPath, "eula.txt")) Then
            fs = New IO.FileStream(IO.Path.Combine(ServerPath, "eula.txt"), IO.FileMode.Truncate, IO.FileAccess.Write)
        Else
            fs = New IO.FileStream(IO.Path.Combine(ServerPath, "eula.txt"), IO.FileMode.CreateNew, IO.FileAccess.Write)
        End If
        Using writer As New IO.StreamWriter(fs, System.Text.Encoding.UTF8)
            writer.WriteLine("#By changing the setting below to TRUE you are indicating your agreement to our EULA (https://account.mojang.com/documents/minecraft_eula).")
            writer.WriteLine("#" & Date.Now.ToString("ddd MMM dd HH:mm:ss K yyyy"), System.Globalization.CultureInfo.CurrentUICulture)
            writer.WriteLine("eula=true")
            writer.Flush()
            writer.Close()
        End Using
    End Sub
#End Region
End Class
