Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports YamlDotNet.Serialization

' Caudron Options (Base on Caudron 1.7.10)
Public Class CauldronOptions
    Inherits AbstractSoftwareOptions
    Dim path As String
    Public Overrides Function GetOptionsTitle() As String
        Return "Cauldron 設定"
    End Function
    Dim Config_Version As Integer = 1
#Region "紀錄"
    <DisplayName("記錄實體碰撞")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄實體碰撞/記量檢查。")>
    Public Property Entity_Collision_Checks As Boolean = False
    <DisplayName("記錄連線")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄連線。")>
    Public Property Connection As Boolean = False
    <DisplayName("記錄跳過遊戲刻")> <DefaultValue(False)> <Category("紀錄")> <Description("是否在伺服器跳過某些遊戲刻時（通常是因為 lag）紀錄。")>
    Public Property Tick_Intervals As Boolean = False
    <DisplayName("死當警告時傾印執行緒")> <DefaultValue(False)> <Category("紀錄")> <Description("是否在發出死當警告時傾印伺服器執行緒（偵錯造成死當的程式碼）。")>
    Public Property Dump_Threads_On_Warn As Boolean = False
    <DisplayName("記錄速度導致的實體移除")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄由於速度導致的實體移除。")>
    Public Property Entity_Speed_Removal As Boolean = False
    <DisplayName("停用警告訊息")> <DefaultValue(False)> <Category("紀錄")> <Description("是否不記錄警告訊息。")>
    Public Property Disabled_Warnings As Boolean = False
    <DisplayName("記錄世界記憶體洩漏")> <DefaultValue(False)> <Category("紀錄")> <Description("是否紀錄那些記憶體洩漏的世界（通常是因為Bug）。")>
    Public Property World_Leak_Debug As Boolean = False
    <DisplayName("實體碰撞警告閥值")> <DefaultValue(200)> <Category("紀錄")> <Description("紀錄實體碰撞警告的閥值（單點內的碰撞實體數）。" &
                                                                                vbNewLine & "0 _ 禁用此設定")>
    Public Property Collision_Warn_Size As Integer = 200
    <DisplayName("實體數警告閥值")> <DefaultValue(0)> <Category("紀錄")> <Description("紀錄在單個維度中實體數警告的閥值。" &
                                                                                vbNewLine & "0 - 禁用此設定")>
    Public Property Entity_Count_Warn_Size As Integer = 0
    <DisplayName("記錄實體死亡")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄實體死亡或被摧毀。")>
    Public Property Entity_Death As Boolean = False
    <DisplayName("記錄實體消失")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄實體自然消失（被伺服器的實體存在規則給消除）。")>
    Public Property Entity_Despawn As Boolean = False
    <DisplayName("記錄區塊卸載")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄區塊卸除。")>
    Public Property Chunk_Unload As Boolean = False
    <DisplayName("記錄實體生成")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄實體生成。")>
    Public Property Entity_Spawn As Boolean = False
    <DisplayName("死當時傾印區塊")> <DefaultValue(False)> <Category("紀錄")> <Description("是否在發出死當警告時傾印伺服器區塊（可協助偵錯造成死當的原因）。")>
    Public Property Dump_Chunks_On_Deadlock As Boolean = False
    <DisplayName("記錄區塊加載")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄區塊加載。")>
    Public Property Chunk_Load As Boolean = False
    <DisplayName("紀錄程式碼堆疊紀錄")> <DefaultValue(False)> <Category("紀錄")> <Description("是否記錄程式碼的堆疊紀錄(StackTrace)")>
    Public Property Detailed_Logging As Boolean = False
    <DisplayName("死當時傾印堆疊")> <DefaultValue(False)> <Category("紀錄")> <Description("是否在發出死當警告時傾印伺服器的記憶體堆疊（可協助偵錯造成死當的原因）。")>
    Public Property Dump_Heap_On_Deadlock As Boolean = False
#End Region
#Region "設定"
    <DisplayName("強制在Forge 遊戲刻內加載區塊")> <DefaultValue(False)> <Category("設定")> <Description("是否強制在Forge 伺服器的遊戲刻內加載區塊。")>
    Public Property Load_Chunk_On_Forge_Tick As Boolean = False
    <DisplayName("移除過快的實體")> <DefaultValue(False)> <Category("設定")> <Description("是否移除任何超過最大速度的實體。")>
    Public Property Check_Entity_Mas_Speeds As Boolean = False
    <DisplayName("移除過大的實體")> <DefaultValue(True)> <Category("設定")> <Description("是否移除任何大小超出最大邊框尺寸的實體。")>
    Public Property Check_Entity_Bounding_Boxes As Boolean = True
    <DisplayName("提供要求時強制使區塊加載")> <DefaultValue(True)> <Category("設定")> <Description("受否在提供請求時強制加載區塊。（可使不檢查是否加載了區塊的模組提高效能）")>
    Public Property Load_Chunk_On_Request As Boolean = True
    <DisplayName("實體最大邊框尺寸")> <DefaultValue(1000)> <Category("設定")> <Description("實體邊框的最大大小。")>
    Public Property Entity_Bounding_Box_Max_Size As Integer = 1000
    <DisplayName("傾印方塊和物品資訊")> <DefaultValue(False)> <Category("設定")> <Description("是否傾印方塊和物品的資訊（包含ID）。")>
    Public Property Dump_Materials As Boolean = False
#End Region
#Region "假玩家"
    <DisplayName("假玩家是否能觸發登入事件")> <DefaultValue(False)> <Category("假玩家")> <Description("假玩家是否能觸發伺服器的登入事件。")>
    Public Property Do_Login As Boolean = False
#End Region
#Region "偵錯"
    <DisplayName("使用執行緒競爭監視")> <DefaultValue(False)> <Category("偵錯")> <Description("是否使用Java 的執行緒競爭監視以執行執行緒傾印。")>
    Public Property Thread_Contention_Monitoring As Boolean = False
#End Region
    <DisplayName("世界設定")> <Editor(GetType(CauldronWorldSettingsCollectionEditor), GetType(UITypeEditor))> <Category("世界設定")> <Description("各世界的單一設定，沒有的話將採用""default""設定。")>
    Public Property World_settings As List(Of CauldronWorldSettings) = New List(Of CauldronWorldSettings)
    <DisplayName("插件設定")> <Editor(GetType(CauldronPluginSettingsCollectionEditor), GetType(UITypeEditor))> <Category("插件設定")> <Description("各插件的單一設定，沒有的話將採用""default""設定。")>
    Public Property Plugin_settings As List(Of CauldronPluginSettings) = New List(Of CauldronPluginSettings)
    Friend Sub CreateOptionsWithDefaultSetting(path As String)
        Me.path = path
        World_settings.Add(New CauldronWorldSettings With {.Name = "default"})
        Plugin_settings.Add(New CauldronPluginSettings With {.Name = "default"})
    End Sub
    Public Sub New(filepath As String)
        MyBase.New(filepath)
        If IO.File.Exists(filepath) Then
            With Me
                Dim jsonObject As JObject
                Try
                    Dim reader As New IO.StreamReader(New IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                    Dim deserializer = New DeserializerBuilder().Build()
                    Dim yamlObject = deserializer.Deserialize(reader)
                    deserializer = Nothing
                    Dim serializer = New SerializerBuilder().JsonCompatible().Build()
                    Dim jsonString = serializer.Serialize(yamlObject)
                    serializer = Nothing
                    jsonObject = GetDeserialisedObject(jsonString)
                    reader.Close()
                Catch ex As Exception
                    jsonObject = New JObject
                End Try
                Dim loggingRegion As JObject = GetJsonObject(jsonObject, "logging")
                Dim settingRegion As JObject = GetJsonObject(jsonObject, "setting")
                Dim fakePlayersRegion As JObject = GetJsonObject(jsonObject, "fake-players")
                Dim debugRegion As JObject = GetJsonObject(jsonObject, "debug")
                InputPropertyValue(jsonObject, "config-version", .Config_Version)
                InputPropertyValue(loggingRegion, "entity-collision-checks", .Entity_Collision_Checks)
                InputPropertyValue(loggingRegion, "connection", .Connection)
                InputPropertyValue(loggingRegion, "tick-intervals", .Tick_Intervals)
                InputPropertyValue(loggingRegion, "dump-threads-on-warn", .Dump_Threads_On_Warn)
                InputPropertyValue(loggingRegion, "entity-speed-removal", .Entity_Speed_Removal)
                InputPropertyValue(loggingRegion, "disabled-warnings", .Disabled_Warnings)
                InputPropertyValue(loggingRegion, "world-leak-debug", .World_Leak_Debug)
                InputPropertyValue(loggingRegion, "collision-warn-size", .Collision_Warn_Size)
                InputPropertyValue(loggingRegion, "entity-count-warn-size", .Entity_Count_Warn_Size)
                InputPropertyValue(loggingRegion, "entity-death", .Entity_Death)
                InputPropertyValue(loggingRegion, "entity-despawn", .Entity_Despawn)
                InputPropertyValue(loggingRegion, "chunk-unload", .Chunk_Unload)
                InputPropertyValue(loggingRegion, "entity-spawn", .Entity_Spawn)
                InputPropertyValue(loggingRegion, "dump-chunks-on-deadlock", .Dump_Chunks_On_Deadlock)
                InputPropertyValue(loggingRegion, "chunk-load", .Chunk_Load)
                InputPropertyValue(loggingRegion, "detailed-logging", .Detailed_Logging)
                InputPropertyValue(loggingRegion, "dump-heap-on-deadlock", .Dump_Heap_On_Deadlock)
                InputPropertyValue(settingRegion, "load-chunk-on-forge-tick", .Load_Chunk_On_Forge_Tick)
                InputPropertyValue(settingRegion, "check-entity-max-speeds", .Check_Entity_Mas_Speeds)
                InputPropertyValue(settingRegion, "check-entity-bounding-boxes", .Check_Entity_Bounding_Boxes)
                InputPropertyValue(settingRegion, "load-chunk-on-request", .Load_Chunk_On_Request)
                InputPropertyValue(settingRegion, "entity-bounding-box-max-size", .Entity_Bounding_Box_Max_Size)
                InputPropertyValue(settingRegion, "dump-materials", .Dump_Materials)
                InputPropertyValue(fakePlayersRegion, "do-login", .Do_Login)
                InputPropertyValue(debugRegion, "thread-contention-monitoring", .Thread_Contention_Monitoring)
                For Each worldSettingProperty As JProperty In GetJsonObject(jsonObject, "world-settings").Children
                    Try
                        Dim JSONWorldSetting As JObject = CType(worldSettingProperty.Value, JObject)
                        Dim worldSetting As New CauldronWorldSettings With {.Name = worldSettingProperty.Name}
                        With worldSetting
                            InputPropertyValue(JSONWorldSetting, "flowing-lava-decay", .Flowing_Lava_Decay)
                            InputPropertyValue(JSONWorldSetting, "infinite-water-source", .Infinite_Water_Source)
                        End With
                        .World_settings.Add(worldSetting)
                    Catch ex As Exception

                    End Try
                Next
                For Each pluginSettingProperty As JProperty In GetJsonObject(jsonObject, "plugin-settings").Children
                    Try
                        Dim JSONPluginSetting As JObject = CType(pluginSettingProperty.Value, JObject)
                        Dim pluginSetting As New CauldronPluginSettings With {.Name = pluginSettingProperty.Name}
                        With pluginSetting
                            InputPropertyValue(JSONPluginSetting, "remap-plugin-file", .Remap_Plugin_File)
                        End With
                        .Plugin_settings.Add(pluginSetting)
                    Catch ex As Exception

                    End Try
                Next
                .path = filepath
                GC.Collect()
            End With
        Else
            CreateOptionsWithDefaultSetting(filepath)
        End If
    End Sub
    Public Overrides Sub SaveOption()
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
        Dim loggingRegion As JObject = GetJsonObject(jsonObject, "logging")
        Dim settingRegion As JObject = GetJsonObject(jsonObject, "setting")
        Dim fakePlayersRegion As JObject = GetJsonObject(jsonObject, "fake-players")
        Dim debugRegion As JObject = GetJsonObject(jsonObject, "debug")
        SetPropertyValue(jsonObject, "config-version", Config_Version)
        SetPropertyValue(loggingRegion, "entity-collision-checks", Entity_Collision_Checks)
        SetPropertyValue(loggingRegion, "connection", Connection)
        SetPropertyValue(loggingRegion, "tick-intervals", Tick_Intervals)
        SetPropertyValue(loggingRegion, "dump-threads-on-warn", Dump_Threads_On_Warn)
        SetPropertyValue(loggingRegion, "entity-speed-removal", Entity_Speed_Removal)
        SetPropertyValue(loggingRegion, "disabled-warnings", Disabled_Warnings)
        SetPropertyValue(loggingRegion, "world-leak-debug", World_Leak_Debug)
        SetPropertyValue(loggingRegion, "collision-warn-size", Collision_Warn_Size)
        SetPropertyValue(loggingRegion, "entity-count-warn-size", Entity_Count_Warn_Size)
        SetPropertyValue(loggingRegion, "entity-death", Entity_Death)
        SetPropertyValue(loggingRegion, "entity-despawn", Entity_Despawn)
        SetPropertyValue(loggingRegion, "chunk-unload", Chunk_Unload)
        SetPropertyValue(loggingRegion, "entity-spawn", Entity_Spawn)
        SetPropertyValue(loggingRegion, "dump-chunks-on-deadlock", Dump_Chunks_On_Deadlock)
        SetPropertyValue(loggingRegion, "chunk-load", Chunk_Load)
        SetPropertyValue(loggingRegion, "detailed-logging", Detailed_Logging)
        SetPropertyValue(loggingRegion, "dump-heap-on-deadlock", Dump_Heap_On_Deadlock)
        SetPropertyValue(settingRegion, "load-chunk-on-forge-tick", Load_Chunk_On_Forge_Tick)
        SetPropertyValue(settingRegion, "check-entity-max-speeds", Check_Entity_Mas_Speeds)
        SetPropertyValue(settingRegion, "check-entity-bounding-boxes", Check_Entity_Bounding_Boxes)
        SetPropertyValue(settingRegion, "load-chunk-on-request", Load_Chunk_On_Request)
        SetPropertyValue(settingRegion, "entity-bounding-box-max-size", Entity_Bounding_Box_Max_Size)
        SetPropertyValue(settingRegion, "dump-materials", Dump_Materials)
        SetPropertyValue(fakePlayersRegion, "do-login", Do_Login)
        SetPropertyValue(debugRegion, "thread-contention-monitoring", Thread_Contention_Monitoring)
        SetPropertyValue(jsonObject, "logging", loggingRegion)
        SetPropertyValue(jsonObject, "setting", settingRegion)
        SetPropertyValue(jsonObject, "fake-players", fakePlayersRegion)
        SetPropertyValue(jsonObject, "debug", debugRegion)
        Dim worldSettingsRegion As New JObject
        For Each worldSetting As CauldronWorldSettings In World_settings
            Dim JSONWorldSetting As New JObject
            With worldSetting
                SetPropertyValue(JSONWorldSetting, "flowing-lava-decay", .Flowing_Lava_Decay)
                SetPropertyValue(JSONWorldSetting, "infinite-water-source", .Infinite_Water_Source)
            End With
            worldSettingsRegion.Add(worldSetting.Name, JSONWorldSetting)
        Next
        SetPropertyValue(jsonObject, "world-settings", worldSettingsRegion)
        Dim pluginSettingsRegion As New JObject
        For Each pluginSetting As CauldronPluginSettings In Plugin_settings
            Dim JSONPluginSetting As New JObject
            With pluginSetting
                SetPropertyValue(JSONPluginSetting, "remap-plugin-file", .Remap_Plugin_File)
            End With
            pluginSettingsRegion.Add(pluginSetting.Name, JSONPluginSetting)
        Next
        SetPropertyValue(jsonObject, "plugin-settings", pluginSettingsRegion)
        Dim expConverter = New Newtonsoft.Json.Converters.ExpandoObjectConverter()
        Dim deserializedObject = JsonConvert.DeserializeObject(Of Dynamic.ExpandoObject)(JsonConvert.SerializeObject(jsonObject), expConverter)
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

#Region "Cauldron YAML Object"
    ''' <summary>
    ''' Cauldron 世界設定
    ''' </summary>
    Public Class CauldronWorldSettings
        <DisplayName("(名稱)")> <Category("一般")> <Description("此屬性必須設定成要設定的世界名稱或default(作為預設值)且不可重複。")>
        Public Property Name As String = ""
        <DisplayName("岩漿是否比照水的行為")> <DefaultValue(False)> <Description("當移除岩漿源時，岩漿是否表現得和水一樣。")>
        Public Property Flowing_Lava_Decay As Boolean = False
        <DisplayName("是否能生成無限水源")> <DefaultValue(True)> <Description("是否能以原版方式生成無限水源。")>
        Public Property Infinite_Water_Source As Boolean = True
    End Class
    ''' <summary>
    ''' Cauldron 插件設定
    ''' </summary>
    Public Class CauldronPluginSettings
        <DisplayName("(名稱)")> <Category("一般")> <Description("此屬性必須設定成要設定的插件名稱或default(作為預設值)且不可重複。")>
        Public Property Name As String = ""
        <DisplayName("重新映射插件檔案")> <DefaultValue(False)> <Description("是否重新映射插件檔案。")>
        Public Property Remap_Plugin_File As Boolean = False
    End Class
#End Region
    Public Class CauldronWorldSettingsCollectionEditor
        Inherits CollectionEditor
        Dim itemCount As Integer = 1
        Public Sub New()
            MyBase.New(type:=GetType(List(Of CauldronWorldSettings)))
        End Sub
        Protected Overrides Function CreateCollectionForm() As CollectionForm
            Dim form = MyBase.CreateCollectionForm()
            form.Text = "Cauldron 世界設定編輯器"
            AddHandler form.Shown, Sub()
                                       ShowDescription(form)
                                   End Sub
            Return form
        End Function
        Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
            Dim settings = New CauldronWorldSettings
            settings.Name = "world-" & itemCount
            itemCount += 1
            Return settings
        End Function
        Shared Sub ShowDescription(control As Control)
            Dim grid As PropertyGrid = TryCast(control, PropertyGrid)
            If grid IsNot Nothing Then grid.HelpVisible = True
            For Each child As Control In control.Controls
                ShowDescription(child)
            Next
        End Sub
        Protected Overrides Function CanRemoveInstance(value As Object) As Boolean
            Dim settings As CauldronWorldSettings = value
            Return settings.Name.ToLower <> "default"
        End Function
        Protected Overrides Function GetDisplayText(value As Object) As String
            Dim settings As CauldronWorldSettings = value
            Return settings.Name
        End Function
    End Class
    Public Class CauldronPluginSettingsCollectionEditor
        Inherits CollectionEditor
        Dim itemCount As Integer = 1
        Public Sub New()
            MyBase.New(type:=GetType(List(Of CauldronPluginSettings)))
        End Sub
        Protected Overrides Function CreateCollectionForm() As CollectionForm
            Dim form = MyBase.CreateCollectionForm()
            form.Text = "Cauldron 插件設定編輯器"
            AddHandler form.Shown, Sub()
                                       ShowDescription(form)
                                   End Sub
            Return form
        End Function
        Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
            Dim settings = New CauldronPluginSettings
            settings.Name = "world-" & itemCount
            itemCount += 1
            Return settings
        End Function
        Shared Sub ShowDescription(control As Control)
            Dim grid As PropertyGrid = TryCast(control, PropertyGrid)
            If grid IsNot Nothing Then grid.HelpVisible = True
            For Each child As Control In control.Controls
                ShowDescription(child)
            Next
        End Sub
        Protected Overrides Function CanRemoveInstance(value As Object) As Boolean
            Dim settings As CauldronPluginSettings = value
            Return settings.Name.ToLower <> "default"
        End Function
        Protected Overrides Function GetDisplayText(value As Object) As String
            Dim settings As CauldronPluginSettings = value
            Return settings.Name
        End Function
    End Class

End Class
