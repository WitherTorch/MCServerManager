Imports System.ComponentModel
Imports System.Dynamic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json.Linq
Imports YamlDotNet.Serialization
''' <summary>
''' bukkit.yml 的對應.NET 類別
''' </summary>
Public Class BukkitOptions
    Dim path As String = ""
#Region "通用設定"
    <DisplayName("允許終界")> <DefaultValue(True)> <Category("通用設定")> <Description("是否啟用終界。")>
    Public Property Allow_end As Boolean = True
    <DisplayName("過載警告")> <DefaultValue(True)> <Category("通用設定")> <Description("是否在伺服器過載(lag)時發出警告。")>
    Public Property Warn_on_overload As Boolean = True
    <DisplayName("權限設定檔位址")> <DefaultValue("permissions.yml")> <Category("通用設定")> <Description("伺服器權限設定檔的檔名。")>
    Public Property Permissions_file As String = "permissions.yml"
    <DisplayName("更新暫存資料夾位址")> <DefaultValue("update")> <Category("通用設定")> <Description("暫存插件更新檔的位置(必須是相對位置)。")>
    Public Property Update_folder As String = "update"
    <DisplayName("啟用插件測量")> <DefaultValue(False)> <Category("通用設定")> <Description("是否啟用/timings指令(用於測量插件處理事件所需的時間)。")>
    Public Property Plugin_profiling As Boolean = False
    <DisplayName("連線嘗試間隔")> <DefaultValue(4000L)> <Category("通用設定")> <Description("在最近的連接嘗試之後允許客戶端再次連接的時間，" &
                                                        vbNewLine & "以防止DoS攻擊（以毫秒為單位）")>
    Public Property Connection_throttle As Long = 4000
    <DisplayName("允許查詢插件")> <DefaultValue(True)> <Category("通用設定")> <Description("遠端查詢時，伺服器是否傳回插件清單。")>
    Public Property Query_plugins As Boolean = True
    <DisplayName("棄用事件警告")> <DefaultValue(TripleStatus.Default)> <Category("通用設定")> <Description("是否在插件註冊已棄用的事件時發出警告。" &
                                                         vbNewLine & "Default(預設) - 在註冊時發出警告。" &
                                                         vbNewLine & "True(是) - 同「預設」。" &
                                                         vbNewLine & "False(否) - 在註冊時不發出警告。")>
    Public Property Deprecated_verbose As TripleStatus = TripleStatus.Default
    <DisplayName("伺服器關閉訊息")> <DefaultValue("Server closed")> <Category("通用設定")> <Description("伺服器在關閉時向玩家顯示的訊息。")>
    Public Property Shutdown_message As String = "Server closed"
#End Region
#Region "生成限制"
    <DisplayName("最大怪物生成數")> <DefaultValue(70)> <Category("生成限制")> <Description("敵對生物(例如:殭屍、苦力怕)在一個世界中能夠生成的數量。")>
    Public Property Monsters As Integer = 70
    <DisplayName("最大動物生成數")> <DefaultValue(15)> <Category("生成限制")> <Description("動物(例如:牛、馬)在一個世界中能夠生成的數量。")>
    Public Property Animals As Integer = 15
    <DisplayName("最大水生動物數")> <DefaultValue(5)> <Category("生成限制")> <Description("水生動物(例如:魷魚、鮭魚)在一個世界中能夠生成的數量。")>
    Public Property Water_animals As Integer = 5
    <DisplayName("最大環境生物數")> <DefaultValue(15)> <Category("生成限制")> <Description("環境生物(蝙蝠)在一個世界中能夠生成的數量。")>
    Public Property Ambients As Integer = 15
#End Region
#Region "記憶體回收"
    <DisplayName("記憶體回收間隔")> <DefaultValue(600)> <Category("記憶體回收")> <Description("兩次區塊記憶體回收器(GC)嘗試回收記憶體的時間間隔。" &
                                                         vbNewLine & "如果設置為0，回收程式將被禁用。")>
    Public Property Period_in_ticks As Integer = 600
    <DisplayName("回收後載入區塊數")> <DefaultValue(0)> <Category("記憶體回收")> <Description("上次回收之後，需載入的區塊數量。" &
                                                         vbNewLine & "如果設置為0，回收程式將被禁用。")>
    Public Property Load_threshold As Integer = 0
#End Region
#Region "時間控制"
    <DisplayName("動物生成間隔")> <DefaultValue(400)> <Category("時間控制")> <Description("每次嘗試生成動物的時間間隔。(單位:遊戲刻)")>
    Public Property Animal_spawns As Integer = 400
    <DisplayName("怪物生成間隔")> <DefaultValue(1)> <Category("時間控制")> <Description("每次嘗試生成怪物的時間間隔。(單位:遊戲刻)")>
    Public Property Monster_spawns As Integer = 1
    <DisplayName("自動儲存間隔")> <DefaultValue(6000)> <Category("時間控制")> <Description("每次自動儲存伺服器的時間間隔。(單位:遊戲刻)")>
    Public Property Autosave As Integer = 6000
#End Region
    Friend ReadOnly Property Aliases As String = "now-in-commands.yml"
    Private Sub New()
    End Sub
    Friend Shared Function LoadOptions(filepath As String) As BukkitOptions
        Dim bukkitOption As New BukkitOptions
        If IO.File.Exists(filepath) Then
            With bukkitOption
                Dim reader As New IO.StreamReader(New IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                Dim deserializer = New DeserializerBuilder().Build()
                Dim yamlObject = deserializer.Deserialize(reader)
                On Error Resume Next
                Dim _serializer = New SerializerBuilder().JsonCompatible().Build()
                Dim jsonString = _serializer.Serialize(yamlObject)
                Dim jsonObject = GetDeserialisedObject(jsonString)
                Dim settingsRegion As JObject = jsonObject.GetValue("settings")
                Dim spawnLimitsRegion As JObject = jsonObject.GetValue("spawn-limits")
                Dim chunkGcRegion As JObject = jsonObject.GetValue("chunk-gc")
                Dim ticksPerRegion As JObject = jsonObject.GetValue("ticks-per")

                .Allow_end = GetBoolean(settingsRegion.GetValue("allow-end"))
                .Warn_on_overload = GetBoolean(settingsRegion.GetValue("warn-on-overload"))
                .Permissions_file = settingsRegion.GetValue("permissions-file")
                .Update_folder = settingsRegion.GetValue("update-folder")
                .Plugin_profiling = GetBoolean(settingsRegion.GetValue("plugin-profiling"))
                .Connection_throttle = settingsRegion.GetValue("connection-throttle")
                .Query_plugins = GetBoolean(settingsRegion.GetValue("query-plugins"))
                .Deprecated_verbose = GetTripleStatus(settingsRegion.GetValue("deprecated-verbose"))
                .Shutdown_message = settingsRegion.GetValue("shutdown-message")
                .Monsters = spawnLimitsRegion.GetValue("monsters")
                .Animals = spawnLimitsRegion.GetValue("animals")
                .Water_animals = spawnLimitsRegion.GetValue("water-animals")
                .Ambients = spawnLimitsRegion.GetValue("ambients")
                .Period_in_ticks = chunkGcRegion.GetValue("period-in-ticks")
                .Load_threshold = chunkGcRegion.GetValue("load-threshold")
                .Animal_spawns = ticksPerRegion.GetValue("animal-spawns")
                .Monster_spawns = ticksPerRegion.GetValue("monster-spawns")
                .Autosave = ticksPerRegion.GetValue("autosave")
                ._Aliases = jsonObject.GetValue("aliases")
                .path = filepath
                reader.Close()
            End With
            Return bukkitOption
        Else
            Return CreateOptionsWithDefaultSetting(filepath)
        End If
    End Function
    Friend Sub SaveOption()
        Dim jsonObject As JObject
        If IO.File.Exists(path) Then
            Try
                Dim reader As New IO.StreamReader(New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                Dim deserializer = New DeserializerBuilder().Build()
                Dim yamlObject = deserializer.Deserialize(reader)
                deserializer = Nothing
                Dim jsonSerializer = New SerializerBuilder().JsonCompatible().Build()
                Dim jsonString = jsonSerializer.Serialize(yamlObject)
                jsonSerializer = Nothing
                jsonObject = GetDeserialisedObject(jsonString)
                reader.Close()
            Catch ex As Exception
                jsonObject = New JObject
            End Try
        Else
            jsonObject = New JObject
        End If
        Dim settingsRegion As JObject = GetJsonObject(jsonObject, "settings")
        SetPropertyValue(settingsRegion, "allow-end", Allow_end.ToString.ToLower)
        SetPropertyValue(settingsRegion, "warn-on-overload", Warn_on_overload.ToString.ToLower)
        SetPropertyValue(settingsRegion, "permissions-file", Permissions_file)
        SetPropertyValue(settingsRegion, "update-folder", Update_folder)
        SetPropertyValue(settingsRegion, "plugin-profiling", Plugin_profiling.ToString.ToLower)
        SetPropertyValue(settingsRegion, "connection-throttle", Connection_throttle)
        SetPropertyValue(settingsRegion, "query-plugins", Query_plugins.ToString.ToLower)
        SetPropertyValue(settingsRegion, "deprecated-verbose", Deprecated_verbose.ToString.ToLower)
        SetPropertyValue(settingsRegion, "shutdown-message", Shutdown_message)
        Dim spawnLimitsRegion As New JObject()
        spawnLimitsRegion.Add("monsters", Monsters)
        spawnLimitsRegion.Add("animals", Animals)
        spawnLimitsRegion.Add("water-animals", Water_animals)
        spawnLimitsRegion.Add("ambients", Ambients)
        Dim chunkGcRegion As New JObject()
        chunkGcRegion.Add("period-in-ticks", Period_in_ticks)
        chunkGcRegion.Add("load-threshold", Load_threshold)
        Dim ticksPerRegion As New JObject()
        ticksPerRegion.Add("animal-spawns", Animal_spawns)
        ticksPerRegion.Add("monster-spawns", Monster_spawns)
        ticksPerRegion.Add("autosave", Autosave)
        SetPropertyValue(jsonObject, "settings", settingsRegion)
        SetPropertyValue(jsonObject, "spawn-limits", spawnLimitsRegion)
        SetPropertyValue(jsonObject, "chunk-gc", chunkGcRegion)
        SetPropertyValue(jsonObject, "ticks-per", ticksPerRegion)
        SetPropertyValue(jsonObject, "aliases", Aliases)
        Dim expConverter = New ExpandoObjectConverter()
        Dim deserializedObject = JsonConvert.DeserializeObject(Of ExpandoObject)(JsonConvert.SerializeObject(jsonObject), expConverter)
        Dim serializer As New YamlDotNet.Serialization.Serializer()
        Dim writer As IO.StreamWriter
        If IO.File.Exists(path) Then
            writer = New IO.StreamWriter(New IO.FileStream(path, IO.FileMode.Truncate, IO.FileAccess.Write))
        Else
            writer = New IO.StreamWriter(New IO.FileStream(path, IO.FileMode.CreateNew, IO.FileAccess.Write))
        End If
        serializer.Serialize(writer, deserializedObject)
        writer.Flush()
        writer.Close()
    End Sub
    Friend Shared Function CreateOptionsWithDefaultSetting(path As String) As BukkitOptions
        Dim op As New BukkitOptions
        op.path = path
        Return op
    End Function
End Class