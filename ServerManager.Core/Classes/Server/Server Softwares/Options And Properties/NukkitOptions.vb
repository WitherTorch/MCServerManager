Imports System.ComponentModel
Imports System.Drawing.Design
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports YamlDotNet.Serialization
Public Class NukkitOptions
    Inherits AbstractSoftwareOptions
    Dim path As String
    Public Overrides Function GetOptionsTitle() As String
        Return "Nukkit 設定"
    End Function
    Enum NukkitLanguageEnum
        English
        Chinese_Simplified
        Chinese_Traditional
        Japanese
        Russian
        Spanish
        Polish
        Portuguese_Brazil
        Korean
        Ukrainian
        German
        Lithuanian
        Indonesia
        Czech
        Turkish
        Finland
    End Enum
    <DisplayName("介面語言")> <DefaultValue(NukkitLanguageEnum.Chinese_Traditional)> <Category("一般")> <Description("多語言設定" &
                                                                        vbNewLine & "English - 英文" &
                                                                        vbNewLine & "Chinese_Simplified - 簡體中文" &
                                                                        vbNewLine & "Chinese_Traditional - 繁體中文" &
                                                                        vbNewLine & "Japanese - 日文" &
                                                                        vbNewLine & "Russian - 俄羅斯文" &
                                                                        vbNewLine & "Spanish - 西班牙語" &
                                                                        vbNewLine & "Polish - 波蘭語" &
                                                                        vbNewLine & "Portuguese_Brazil - 巴西式葡萄牙語" &
                                                                        vbNewLine & "Korean - 韓文" &
                                                                        vbNewLine & "Ukrainian - 烏克蘭語" &
                                                                        vbNewLine & "German - 德文" &
                                                                        vbNewLine & "Lithuanian - 立陶宛語" &
                                                                        vbNewLine & "Indonesia - 印尼語" &
                                                                        vbNewLine & "Czech - 捷克語" &
                                                                        vbNewLine & "Turkish - 土耳其文" &
                                                                        vbNewLine & "Finland - 芬蘭語"
                                                                        )>
    Public Property Language As NukkitLanguageEnum = NukkitLanguageEnum.Chinese_Traditional
    <DisplayName("是否強制使用語言")> <DefaultValue(True)> <Category("一般")> <Description("伺服器是否強制使用特定語言介面")>
    Public Property Force_Language As Boolean = True
    <DisplayName("伺服器關閉訊息")> <DefaultValue("伺服器已關閉")> <Category("一般")> <Description("伺服器關閉訊息")>
    Public Property Shutdown_Message As String = "伺服器已關閉"
    <DisplayName("遠端查詢插件")> <DefaultValue(True)> <Category("一般")> <Description("允許使用Query協定查詢您的插件")>
    Public Property Query_Plugins As Boolean = True
    <DisplayName("紀錄使用棄用API")> <DefaultValue(True)> <Category("一般")> <Description("當某插件使用已棄用的API時，在後台提醒")>
    Public Property Deprecated_Verbose As Boolean = True
    <DisplayName("非同步執行緒數量")> <DefaultValue(0)> <Category("一般")> <Description("非同步執行緒數量，" &
                                   vbNewLine & "0 - 自動識別CPU核心數量（最少4個執行緒）")>
    Public Property Async_Workers As Integer = 0
    <DisplayName("數據包大小閥值")> <DefaultValue(256)> <Category("一般")> <Description("數據包大小閥值（單位：位元組），這些包將被壓縮" &
                                   vbNewLine & "0 - 壓縮全部。" &
                                   vbNewLine & "-1 - 停用功能")>
    Public Property Batch_Threshold As Integer = 256
    <DisplayName("資料包壓縮等級")> <DefaultValue(7)> <Category("一般")> <Description("壓縮等級，等級越高，CPU佔用越高，佔用頻寬越少")>
    Public Property Compression_Level As Integer = 7
    <DisplayName("非同步資料包壓縮")> <DefaultValue(False)> <Category("一般")> <Description("非同步壓縮封包資料，緩解主線程CPU負載")>
    Public Property Async_Compression As Boolean = False
    <DisplayName("偵錯等級")> <DefaultValue(1)> <Category("偵錯")> <Description("當偵錯級別 > 1 時，將在控制台顯示偵錯資訊")>
    Public Property Debug_Level As Integer = 1
    <DisplayName("啟用偵錯用指令")> <DefaultValue(False)> <Category("偵錯")> <Description("啟用指令""/status""、""/gc""等偵錯用指令")>
    Public Property Debug_Commands As Boolean = False
    <DisplayName("啟用計時")> <DefaultValue(False)> <Category("計時")> <Description("預設啟用對核心與插件的計時")>
    Public Property Timings_Enabled As Boolean = False
    <DisplayName("詳細記錄計時報告")> <DefaultValue(False)> <Category("計時")> <Description("啟用詳細監控,包含高頻計時")>
    Public Property Timings_Verbose As Boolean = False
    <DisplayName("計時報告間隔")> <DefaultValue(6000)> <Category("計時")> <Description("""歷史""幀的間隔" &
                                       vbNewLine & "預設 5 分鐘 (60000 遊戲刻)")>
    Public Property Timings_History_Interval As Integer = 6000
    <DisplayName("最大計時報告長度")> <DefaultValue(72000)> <Category("計時")> <Description("整個""歷史""時間的長度" &
                                   vbNewLine & "預設 1 小時 (72000 遊戲刻)" &
                                    vbNewLine & "此值最大為 [""歷史""幀的間隔] * 12")>
    Public Property Timings_History_Length As Integer = 72000
    <DisplayName("特例下繞過計時最大值")> <DefaultValue(False)> <Category("計時")> <Description("對於特殊情況將繞過最大值" &
                               vbNewLine & "這個最大值可以確保檔案大小合理，以便在Aikar的時間解析器進行處理" &
                                vbNewLine & "設置此項將不會幫你繞過最大值，除非在Aikar API中增加了特例")>
    Public Property Timings_Bypass_Max As Boolean = False
    <DisplayName("計時時紀錄伺服器名稱")> <DefaultValue(False)> <Category("計時")> <Description("是否不會在計時報告中記錄伺服器名稱")>
    Public Property Timings_Privacy As Boolean = False
    <DisplayName("計時報告忽略項目")> <DefaultValue(New String() {})> <Category("計時")> <Description("設置要忽略的部分。這些部分將不會傳送到Aikar的時間解析器")> '<Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property Timings_Ignore As String() = {}
    <DisplayName("每刻區塊傳送數量")> <DefaultValue(4)> <Category("區塊")> <Description("每遊戲刻發送給玩家區塊的數量")>
    Public Property Chunk_Sending_Per_Tick As Integer = 4
    <DisplayName("最大區塊傳送數量")> <DefaultValue(192)> <Category("區塊")> <Description("玩家附近發送的區塊數量")>
    Public Property Chunk_Sending_Max_Chunks As Integer = 192
    <DisplayName("玩家初始加載區塊數量")> <DefaultValue(56)> <Category("區塊")> <Description("玩家進入伺服器需要的區塊數量")>
    Public Property Chunk_Sending_Spawn_Threshold As Integer = 56
    <DisplayName("使用區塊緩存")> <DefaultValue(False)> <Category("區塊")> <Description("使用區塊緩存來緩解多玩家同時加入時所造成的伺服器壓力")>
    Public Property Chunk_Sending_Cache_Chunks As Boolean = False
    <DisplayName("每刻處理區塊數量")> <DefaultValue(40)> <Category("區塊")> <Description("每遊戲刻處理的區塊數量")>
    Public Property Chunk_Ticking_Per_Tick As Integer = 40
    <DisplayName("區塊處理半徑")> <DefaultValue(3)> <Category("區塊")> <Description("玩家周圍區塊處理的半徑大小")>
    Public Property Chunk_Ticking_Tick_Radius As Integer = 3
    <DisplayName("區塊光照更新")> <DefaultValue(False)> <Category("區塊")> <Description("是否隨時更新光照")>
    Public Property Chunk_Ticking_Light_Updates As Boolean = False
    <DisplayName("區塊生成序列數量上限")> <DefaultValue(8)> <Category("區塊")> <Description("等待序列中，將生成的區塊數量上限")>
    Public Property Chunk_Generation_Queue_Size As Integer = 8
    <DisplayName("區塊填充序列數量上限")> <DefaultValue(8)> <Category("區塊")> <Description("等待序列中，被填充的區塊的數量上限")>
    Public Property Chunk_Generation_Population_Queue_Size As Integer = 8
    <DisplayName("動物生成週期")> <DefaultValue(400)> <Category("時間控制")> <Description("動物生成的週期（單位:遊戲刻）")>
    Public Property Ticks_Per_Animal_Spawns As Integer = 400
    <DisplayName("怪物生成週期")> <DefaultValue(1)> <Category("時間控制")> <Description("怪物生成的週期（單位:遊戲刻）")>
    Public Property Ticks_Per_Monster_Spawns As Integer = 1
    <DisplayName("自動儲存週期")> <DefaultValue(6000)> <Category("時間控制")> <Description("自動儲存的週期（單位:遊戲刻）")>
    Public Property Ticks_Per_Autosave As Integer = 6000
    <DisplayName("緩存清理週期")> <DefaultValue(900)> <Category("時間控制")> <Description("緩存清理的週期（單位:遊戲刻）")>
    Public Property Ticks_Per_Cache_Cleanup As Integer = 900
    <DisplayName("最大怪物生成數量")> <DefaultValue(70)> <Category("生成限制")> <Description("怪物生成的數量限制")>
    Public Property Monsters_Spawn_Limits As Integer = 70
    <DisplayName("最大動物生成數量")> <DefaultValue(15)> <Category("生成限制")> <Description("動物生成的數量限制")>
    Public Property Animals_Spawn_Limits As Integer = 15
    <DisplayName("最大水生動物生成數量")> <DefaultValue(5)> <Category("生成限制")> <Description("水生動物生成的數量限制")>
    Public Property Water_Animals_Spawn_Limits As Integer = 5
    <DisplayName("最大環境生物生成數量")> <DefaultValue(15)> <Category("生成限制")> <Description("環境生物生成的數量限制")>
    Public Property Ambient_Spawn_Limits As Integer = 15
    <DisplayName("儲存玩家資料")> <DefaultValue(True)> <Category("一般")> <Description("玩家資料是否將儲存為 player/<玩家ID>.dat")>
    Public Property Save_Player_Data As Boolean = True
    Private Sub CreateOptionsWithDefaultSetting(path As String)
        Me.path = path
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
                Dim settingsRegion As JObject = GetJsonObject(jsonObject, "settings")
                Dim networkRegion As JObject = GetJsonObject(jsonObject, "network")
                Dim debugRegion As JObject = GetJsonObject(jsonObject, "debug")
                Dim timingRegion As JObject = GetJsonObject(jsonObject, "timings")
                Dim chunkSendingRegion As JObject = GetJsonObject(jsonObject, "chunk-sending")
                Dim chunkTickingRegion As JObject = GetJsonObject(jsonObject, "chunk-ticking")
                Dim chunkGenerationRegion As JObject = GetJsonObject(jsonObject, "chunk-generation")
                Dim ticksPerRegion As JObject = GetJsonObject(jsonObject, "ticks-per")
                Dim spawnLimitsRegion As JObject = GetJsonObject(jsonObject, "spawn-limits")
                Dim languageCode As Integer = NukkitLanguageEnum.Chinese_Traditional
                InputPropertyValue(settingsRegion, "language", languageCode)
                .Language = GetLanguageByLanguageCode(languageCode)
                InputPropertyValue(settingsRegion, "force-language", .Force_Language)
                InputPropertyValue(settingsRegion, "shutdown-message", .Shutdown_Message)
                InputPropertyValue(settingsRegion, "query-plugins", .Query_Plugins)
                InputPropertyValue(settingsRegion, "deprecated-verbose", .Deprecated_Verbose)
                Dim workerCount As String = "auto"
                InputPropertyValue(settingsRegion, "async-workers", workerCount)
                Select Case workerCount
                    Case "auto"
                        .Async_Workers = 0
                    Case Else
                        If Integer.TryParse(workerCount, Nothing) Then
                            .Async_Workers = workerCount
                        Else
                            .Async_Workers = 0
                        End If
                End Select
                InputPropertyValue(networkRegion, "batch-threshold", .Batch_Threshold)
                InputPropertyValue(networkRegion, "compression-level", .Compression_Level)
                InputPropertyValue(networkRegion, "async-compression", .Async_Compression)
                InputPropertyValue(debugRegion, "level", .Debug_Level)
                InputPropertyValue(debugRegion, "commands", .Debug_Commands)
                InputPropertyValue(timingRegion, "enabled", .Timings_Enabled)
                InputPropertyValue(timingRegion, "verbose", .Timings_Verbose)
                InputPropertyValue(timingRegion, "history-interval", .Timings_History_Interval)
                InputPropertyValue(timingRegion, "history-length", .Timings_History_Length)
                InputPropertyValue(timingRegion, "bypass-max", .Timings_Bypass_Max)
                InputPropertyValue(timingRegion, "ignore", .Timings_Ignore)
                InputPropertyValue(chunkSendingRegion, "per-tick", .Chunk_Sending_Per_Tick)
                InputPropertyValue(chunkSendingRegion, "max-chunks", .Chunk_Sending_Max_Chunks)
                InputPropertyValue(chunkSendingRegion, "spawn-threshold", .Chunk_Sending_Spawn_Threshold)
                InputPropertyValue(chunkSendingRegion, "cache-chunks", .Chunk_Sending_Cache_Chunks)
                InputPropertyValue(chunkTickingRegion, "per-tick", .Chunk_Ticking_Per_Tick)
                InputPropertyValue(chunkTickingRegion, "tick-radius", .Chunk_Ticking_Tick_Radius)
                InputPropertyValue(chunkTickingRegion, "light-updates", .Chunk_Ticking_Light_Updates)
                InputPropertyValue(chunkGenerationRegion, "queue-size", .Chunk_Generation_Queue_Size)
                InputPropertyValue(chunkGenerationRegion, "population-queue-size", .Chunk_Generation_Population_Queue_Size)
                InputPropertyValue(ticksPerRegion, "animal-spawns", .Ticks_Per_Animal_Spawns)
                InputPropertyValue(ticksPerRegion, "monster-spawns", .Ticks_Per_Monster_Spawns)
                InputPropertyValue(ticksPerRegion, "autosave", .Ticks_Per_Autosave)
                InputPropertyValue(ticksPerRegion, "cache-cleanup", .Ticks_Per_Cache_Cleanup)
                InputPropertyValue(spawnLimitsRegion, "monsters", .Monsters_Spawn_Limits)
                InputPropertyValue(spawnLimitsRegion, "animals", .Animals_Spawn_Limits)
                InputPropertyValue(spawnLimitsRegion, "water-animals", .Water_Animals_Spawn_Limits)
                InputPropertyValue(spawnLimitsRegion, "ambient", .Ambient_Spawn_Limits)
                InputPropertyValue(GetJsonObject(jsonObject, "player"), "save-player-data", .Save_Player_Data)
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
        Dim settingsRegion As JObject = GetJsonObject(jsonObject, "settings")
        Dim networkRegion As JObject = GetJsonObject(jsonObject, "network")
        Dim debugRegion As JObject = GetJsonObject(jsonObject, "debug")
        Dim timingRegion As JObject = GetJsonObject(jsonObject, "timings")
        Dim chunkSendingRegion As JObject = GetJsonObject(jsonObject, "chunk-sending")
        Dim chunkTickingRegion As JObject = GetJsonObject(jsonObject, "chunk-ticking")
        Dim chunkGenerationRegion As JObject = GetJsonObject(jsonObject, "chunk-generation")
        Dim ticksPerRegion As JObject = GetJsonObject(jsonObject, "ticks-per")
        Dim spawnLimitsRegion As JObject = GetJsonObject(jsonObject, "spawn-limits")
        SetPropertyValue(settingsRegion, "language", GetLanguageCodeByLanguage(Language))
        SetPropertyValue(settingsRegion, "force-language", Force_Language)
        SetPropertyValue(settingsRegion, "shutdown-message", Shutdown_Message)
        SetPropertyValue(settingsRegion, "query-plugins", Query_Plugins)
        SetPropertyValue(settingsRegion, "deprecated-verbose", Deprecated_Verbose)
        Select Case Async_Workers
            Case 0
                SetPropertyValue(settingsRegion, "async-workers", "auto")
            Case Else
                SetPropertyValue(settingsRegion, "async-workers", Async_Workers)
        End Select
        SetPropertyValue(networkRegion, "batch-threshold", Batch_Threshold)
        SetPropertyValue(networkRegion, "compression-level", Compression_Level)
        SetPropertyValue(networkRegion, "async-compression", Async_Compression)
        SetPropertyValue(debugRegion, "level", Debug_Level)
        SetPropertyValue(debugRegion, "commands", Debug_Commands)
        SetPropertyValue(timingRegion, "enabled", Timings_Enabled)
        SetPropertyValue(timingRegion, "verbose", Timings_Verbose)
        SetPropertyValue(timingRegion, "history-interval", Timings_History_Interval)
        SetPropertyValue(timingRegion, "history-length", Timings_History_Length)
        SetPropertyValue(timingRegion, "bypass-max", Timings_Bypass_Max)
        SetPropertyValue(timingRegion, "ignore", New JArray(Timings_Ignore))
        SetPropertyValue(chunkSendingRegion, "per-tick", Chunk_Sending_Per_Tick)
        SetPropertyValue(chunkSendingRegion, "max-chunks", Chunk_Sending_Max_Chunks)
        SetPropertyValue(chunkSendingRegion, "spawn-threshold", Chunk_Sending_Spawn_Threshold)
        SetPropertyValue(chunkSendingRegion, "cache-chunks", Chunk_Sending_Cache_Chunks)
        SetPropertyValue(chunkTickingRegion, "per-tick", Chunk_Ticking_Per_Tick)
        SetPropertyValue(chunkTickingRegion, "tick-radius", Chunk_Ticking_Tick_Radius)
        SetPropertyValue(chunkTickingRegion, "light-updates", Chunk_Ticking_Light_Updates)
        SetPropertyValue(chunkGenerationRegion, "queue-size", Chunk_Generation_Queue_Size)
        SetPropertyValue(chunkGenerationRegion, "population-queue-size", Chunk_Generation_Population_Queue_Size)
        SetPropertyValue(ticksPerRegion, "animal-spawns", Ticks_Per_Animal_Spawns)
        SetPropertyValue(ticksPerRegion, "monster-spawns", Ticks_Per_Monster_Spawns)
        SetPropertyValue(ticksPerRegion, "autosave", Ticks_Per_Autosave)
        SetPropertyValue(ticksPerRegion, "cache-cleanup", Ticks_Per_Cache_Cleanup)
        SetPropertyValue(spawnLimitsRegion, "monsters", Monsters_Spawn_Limits)
        SetPropertyValue(spawnLimitsRegion, "animals", Animals_Spawn_Limits)
        SetPropertyValue(spawnLimitsRegion, "water-animals", Water_Animals_Spawn_Limits)
        SetPropertyValue(spawnLimitsRegion, "ambient", Ambient_Spawn_Limits)
        Dim playerRegion As JObject = GetJsonObject(jsonObject, "player")
        SetPropertyValue(playerRegion, "save-player-data", Save_Player_Data)
        SetPropertyValue(jsonObject, "settings", settingsRegion)
        SetPropertyValue(jsonObject, "network", networkRegion)
        SetPropertyValue(jsonObject, "debug", debugRegion)
        SetPropertyValue(jsonObject, "timings", timingRegion)
        SetPropertyValue(jsonObject, "chunk-sending", chunkSendingRegion)
        SetPropertyValue(jsonObject, "chunk-ticking", chunkTickingRegion)
        SetPropertyValue(jsonObject, "chunk-generation", chunkGenerationRegion)
        SetPropertyValue(jsonObject, "ticks-per", ticksPerRegion)
        SetPropertyValue(jsonObject, "spawn-limits", spawnLimitsRegion)
        SetPropertyValue(jsonObject, "player", playerRegion)
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

    Private Shared Function GetLanguageByLanguageCode(code As String) As NukkitLanguageEnum
        Select Case code
            Case "eng"
                Return NukkitLanguageEnum.English
            Case "chs"
                Return NukkitLanguageEnum.Chinese_Simplified
            Case "cht"
                Return NukkitLanguageEnum.Chinese_Traditional
            Case "jpn"
                Return NukkitLanguageEnum.Japanese
            Case "rus"
                Return NukkitLanguageEnum.Russian
            Case "spa"
                Return NukkitLanguageEnum.Spanish
            Case "pol"
                Return NukkitLanguageEnum.Polish
            Case "bra"
                Return NukkitLanguageEnum.Portuguese_Brazil
            Case "kor"
                Return NukkitLanguageEnum.Korean
            Case "ukr"
                Return NukkitLanguageEnum.Ukrainian
            Case "deu"
                Return NukkitLanguageEnum.German
            Case "ltu"
                Return NukkitLanguageEnum.Lithuanian
            Case "cze"
                Return NukkitLanguageEnum.Czech
            Case Else
                Return NukkitLanguageEnum.Chinese_Traditional
        End Select
    End Function
    Private Shared Function GetLanguageCodeByLanguage(lang As NukkitLanguageEnum) As String
        Select Case lang
            Case NukkitLanguageEnum.English
                Return "eng"
            Case NukkitLanguageEnum.Chinese_Simplified
                Return "chs"
            Case NukkitLanguageEnum.Chinese_Traditional
                Return "cht"
            Case NukkitLanguageEnum.Japanese
                Return "jpn"
            Case NukkitLanguageEnum.Russian
                Return "rus"
            Case NukkitLanguageEnum.Spanish
                Return "spa"
            Case NukkitLanguageEnum.Polish
                Return "pol"
            Case NukkitLanguageEnum.Portuguese_Brazil
                Return "bra"
            Case NukkitLanguageEnum.Korean
                Return "kor"
            Case NukkitLanguageEnum.Ukrainian
                Return "ukr"
            Case NukkitLanguageEnum.German
                Return "deu"
            Case NukkitLanguageEnum.Lithuanian
                Return "ltu"
            Case NukkitLanguageEnum.Czech
                Return "cze"
            Case Else
                Return "cht"
        End Select
    End Function
End Class
