Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports YamlDotNet.Serialization

''' <summary>
''' paper.yml 的對應.NET 類別
''' </summary>
Public Class PaperOptions
    Inherits AbstractSoftwareOptions
    Dim path As String
    Public Overrides Function GetOptionsTitle() As String
        Return "Paper 設定"
    End Function
#Region "一般"
    Dim Config_version As Integer = 13
    <DisplayName("紀錄詳細配置")> <DefaultValue(False)> <Category("一般")> <Description("是否在伺服器啟動時紀錄詳細配置。")>
    Public Property Verbose As Boolean = False
    <DisplayName("玩家互相推擠")> <DefaultValue(True)> <Category("一般")> <Description("是否允許玩家互相推擠。")>
    Public Property Enable_Player_Collisions As Boolean = True
    <DisplayName("自動儲存玩家數據延遲")> <DefaultValue(-1)> <Category("一般")> <Description("設置兩次自動儲存玩家數據之間的遊戲刻延遲。" &
                                                       vbNewLine & " -1 - 由世界的自動儲存機制調控。")>
    Public Property Player_Auto_Save_Rate As Integer = -1
    <DisplayName("每刻儲存玩家數據數量")> <DefaultValue(-1)> <Category("一般")> <Description("每個遊戲刻能夠自動儲存數據的玩家數量。" &
                                                       vbNewLine & "-1 - 使用Paper的建議值（當前為10）。")>
    Public Property Max_Player_Auto_Save_Per_Tick As Integer = -1
    <DisplayName("BungeeCord 正版驗證")> <DefaultValue(True)> <Category("一般")> <Description("設定伺服器在設定BungeeCord之後的行為模式，" &
                                                       vbNewLine & "以配合BungeeCord的正版驗證設定。")>
    Public Property Bungee_Online_Mode As Boolean = True
    <DisplayName("移除無效統計資訊")> <DefaultValue(False)> <Category("一般")> <Description("伺服器是否在加載時會從世界的保存資料中刪除無效的統計資訊。")>
    Public Property Remove_Invalid_Statistics As Boolean = False
    <DisplayName("垃圾封包閥值")> <DefaultValue(300)> <Category("一般")> <Description("伺服器將傳入的數據包視為垃圾封包並忽略它們的閾值。")>
    Public Property Incoming_Packet_Spam_Threshold As Integer = 300
    <DisplayName("使用附加幸運公式")> <DefaultValue(False)> <Category("一般")> <Description("是否使用附加的公式來計算""Luck""屬性。" &
                 vbNewLine & "此設定會影響到怪物掉落物的概率、寶箱的寶物概率等。")>
    Public Property Use_Alternative_Luck_Formula As Boolean = False
    <DisplayName("區域快取檔案大小")> <DefaultValue(256)> <Category("一般")> <Description("單個區域快取資料的大小上限。")>
    Public Property Region_File_Cache_Size As Integer = 256
    <DisplayName("將玩家列表列入預設建議")> <DefaultValue(True)> <Category("一般")> <Description("如果插件沒有自己的輸入建議，" &
                                                       vbNewLine & "是否該指示伺服器在啟動自動完成時傳回玩家列表。")>
    Public Property Suggest_Player_Names_When_Null_Tab_Completions As Boolean = True
    <DisplayName("休眠儲存區塊的執行緒")> <DefaultValue(False)> <Category("一般")> <Description("伺服器是否在每個區塊儲存後休眠該區塊的保存用執行緒。")>
    Public Property Sleep_Between_Chunk_Saves As Boolean = False
    <DisplayName("儲存空的記分板隊伍")> <DefaultValue(False)> <Category("一般")> <Description("一些記分版插件會留下數百個空的隊伍記分板，大大減慢了登入時間。" &
                                                        vbNewLine & "此設定將指示伺服器是否應自動刪除這些空隊伍。")>
    Public Property Save_Empty_Scoreboard_Teams As Boolean = False
    <DisplayName("在插件之前先加載權限檔案")> <DefaultValue(True)> <Category("一般")> <Description("在加載插件之前加載Bukkit的permission.yml(權限檔案)，" &
                                                       vbNewLine & "並允許他們在啟用時立即檢查權限。")>
    Public Property Load_Permissions_yml_Before_Plugins As Boolean = True
    <DisplayName("儲存玩家數據")> <DefaultValue(True)> <Category("一般")> <Description("是否儲存玩家數據。")>
    Public Property Save_Player_Data As Boolean = True
    <DisplayName("最小區塊加載用執行緒數量")> <DefaultValue(2)> <Category("一般")> <Description("設置用於非同步區塊加載的最小執行緒數量。")>
    Public Property Min_Chunk_Load_Threads As Integer = 2
#End Region
#Region "測時"
    <DisplayName("啟用測時")> <DefaultValue(True)> <Category("測時")> <Description("是否啟用公用測時平台。")>
    Public Property Timing_Enabled As Boolean = True
    <DisplayName("記錄詳細測時報告")> <DefaultValue(True)> <Category("測時")> <Description("讓測時平台能提供更詳細的訊息。" &
                                                       vbNewLine & "例如：顯示特定實體類型導致lag而不僅僅是"“實體”"。")>
    Public Property Timing_Verbose As Boolean = True
    <DisplayName("隱藏測時報告中的伺服器名稱")> <DefaultValue(False)> <Category("測時")> <Description("是否應該在測時報告中隱藏伺服器名稱。")>
    Public Property Timing_Server_Name_Policy As Boolean = False
    <DisplayName("測時報告的隱藏項目")> <DefaultValue({"database", "settings.bungeecord-addresses"})> <Category("測時")> <Description("在測時報告中應該隱藏的項目。")> <Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property Timing_Hidden_Config_Entries As String() = {"database", "settings.bungeecord-addresses"}
    <DisplayName("測時紀錄間隔")> <DefaultValue(300)> <Category("測時")> <Description("測時報告中各個點之間的間隔（以秒計算）")>
    Public Property Timing_History_Interval As Integer = 300
    <DisplayName("測時報告長度")> <DefaultValue(3600)> <Category("測時")> <Description("單個測時報告中要保留的資料總量。")>
    Public Property Timing_History_Length As Integer = 3600
#End Region
#Region "訊息"
    <DisplayName("伺服器斷開時的訊息")> <DefaultValue("")> <Category("訊息")> <Description("設置由於Mojang 玩家驗證服務器關閉而導致玩家斷開連線的訊息。" &
                                                     vbNewLine & "注意：空值代表伺服器會發送客戶端能翻譯的訊息。")>
    Public Property Messages_Authentication_Servers_Down As String = ""
    <DisplayName("玩家非法飛行時的訊息")> <DefaultValue("Flying is not enabled on this server")> <Category("訊息")> <Description("玩家因非法飛行而被踢出時所顯示的訊息。")>
    Public Property Messages_Kick_Flying_Player As String = "Flying is not enabled on this server"
    <DisplayName("玩家乘坐載具非法飛行時的訊息")> <DefaultValue("Flying is not enabled on this server")> <Category("訊息")> <Description("玩家因乘坐載具非法飛行而被踢出時所顯示的訊息。")>
    Public Property Messages_Kick_Flying_Vehicle As String = "Flying is not enabled on this server"
#End Region
    <DisplayName("世界設定")> <Editor(GetType(PaperWorldSettingsCollectionEditor), GetType(UITypeEditor))> <Category("世界設定")> <Description("各世界的單一設定，沒有的話將採用""default""設定。")>
    Public Property World_settings As List(Of PaperWorldSettings) = New List(Of PaperWorldSettings)
    Friend Sub CreateOptionsWithDefaultSetting(path As String)
        Me.path = path
        World_settings.Add(New PaperWorldSettings With {.Name = "default"})
    End Sub
    Friend Sub New(filepath As String)
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
                Dim timingRegion As JObject = GetJsonObject(jsonObject, "timings")
                Dim messageKickRegion As JObject = GetJsonObject(GetJsonObject(jsonObject, "messages"), "kick")
                InputPropertyValue(jsonObject, "verbose", .Verbose)
                InputPropertyValue(jsonObject, "config-version", .Config_version)
#Region "Settings"
                InputPropertyValue(settingsRegion, "enable-player-collisions", .Enable_Player_Collisions)
                InputPropertyValue(settingsRegion, "player-auto-save-rate", .Player_Auto_Save_Rate)
                InputPropertyValue(settingsRegion, "max-player-auto-save-per-tick", .Max_Player_Auto_Save_Per_Tick)
                InputPropertyValue(settingsRegion, "min-chunk-load-threads", .Min_Chunk_Load_Threads)
                InputPropertyValue(settingsRegion, "save-player-data", .Save_Player_Data)
                InputPropertyValue(settingsRegion, "bungee-online-mode", .Bungee_Online_Mode)
                InputPropertyValue(settingsRegion, "load-permissions-yml-before-plugins", .Load_Permissions_yml_Before_Plugins)
                InputPropertyValue(settingsRegion, "save-empty-scoreboard-teams", .Save_Empty_Scoreboard_Teams)
                InputPropertyValue(settingsRegion, "incoming-packet-spam-threshold", .Incoming_Packet_Spam_Threshold)
                InputPropertyValue(settingsRegion, "suggest-player-names-when-null-tab-completions", .Suggest_Player_Names_When_Null_Tab_Completions)
                InputPropertyValue(settingsRegion, "use-alternative-luck-formula", .Use_Alternative_Luck_Formula)
                InputPropertyValue(settingsRegion, "sleep-between-chunk-saves", .Sleep_Between_Chunk_Saves)
                InputPropertyValue(settingsRegion, "region-file-cache-size", .Region_File_Cache_Size)
                InputPropertyValue(settingsRegion, "remove-invalid-statistics", .Region_File_Cache_Size)
#End Region
#Region "Timing"
                InputPropertyValue(timingRegion, "enabled", .Timing_Enabled)
                InputPropertyValue(timingRegion, "verbose", .Timing_Verbose)
                InputPropertyValue(timingRegion, "server-name-privacy", .Timing_Server_Name_Policy)
                InputPropertyValue(timingRegion, "hidden-config-entries", .Timing_Hidden_Config_Entries)
                InputPropertyValue(timingRegion, "history-interval", .Timing_History_Interval)
                InputPropertyValue(timingRegion, "history-length", .Timing_History_Length)
#End Region
#Region "Messages"
                InputPropertyValue(messageKickRegion, "authentication-servers-down", .Messages_Authentication_Servers_Down)
                InputPropertyValue(messageKickRegion, "flying-player", .Messages_Kick_Flying_Player)
                InputPropertyValue(messageKickRegion, "flying-vehicle", .Messages_Kick_Flying_Vehicle)
#End Region
#Region "World Settings"
                For Each worldSettingProperty As JProperty In GetJsonObject(jsonObject, "world-settings").Children
                    Dim JSONWorldSetting As JObject = CType(worldSettingProperty.Value, JObject)
                    Dim worldSetting As New PaperWorldSettings With {.Name = worldSettingProperty.Name}
                    With worldSetting
                        InputPropertyValue(JSONWorldSetting, "keep-spawn-loaded-range", .Keep_Spawn_Loaded_Range)
                        Dim frostIceRegion As JObject = GetJsonObject(JSONWorldSetting, "frosted-ice")
                        InputPropertyValue(frostIceRegion, "enabled", .Frosted_Ice_Enabled)
                        InputPropertyValue(GetJsonObject(frostIceRegion, "delay"), "max", .Frosted_Ice_Delay_Max)
                        InputPropertyValue(GetJsonObject(frostIceRegion, "delay"), "min", .Frosted_Ice_Delay_Min)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "lava-flow-speed"), "normal", .Normal_Lava_Flow_Speed)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "lava-flow-speed"), "nether", .Nether_Lava_Flow_Speed)
                        InputPropertyValue(JSONWorldSetting, "use-chunk-inhabited-timer", .Use_Chunk_Inhabited_Timer)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "despawn-ranges"), "soft", .Soft_Despawn_Ranges)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "despawn-ranges"), "hard", .Hard_Despawn_Ranges)
                        InputPropertyValue(JSONWorldSetting, "remove-corrupt-tile-entities", .Remove_Corrupt_Tile_Entities)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "fast-drain"), "lava", .Lava_Fast_Drain)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "fast-drain"), "water", .Water_Fast_Drain)
                        InputPropertyValue(JSONWorldSetting, "falling-block-height-nerf", .Falling_Block_Height_Nerf)
                        InputPropertyValue(JSONWorldSetting, "tnt-entity-height-nerf", .TNT_Entity_Height_Nerf)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "fishing-time-range"), "MinimumTicks", .Fishing_Time_Range_MinimumTicks)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "fishing-time-range"), "MaximumTicks", .Fishing_Time_Range_MaximumTicks)
                        Dim gameMechanicsRegion As JObject = GetJsonObject(JSONWorldSetting, "game-mechanics")
                        InputPropertyValue(gameMechanicsRegion, "disable-sprint-interruption-on-attack", .GM_Disable_Sprint_Interruption_On_Attack)
                        InputPropertyValue(gameMechanicsRegion, "disable-unloaded-chunk-enderpearl-exploit", .GM_Disable_Unloaded_Chunk_Enderpearl_Exploit)
                        InputPropertyValue(gameMechanicsRegion, "disable-chest-cat-detection", .GM_Disable_Chest_Cat_Detection)
                        InputPropertyValue(gameMechanicsRegion, "disable-end-credits", .GM_Disable_End_Credits)
                        InputPropertyValue(gameMechanicsRegion, "disable-player-crits", .GM_Disable_Player_Crits)
                        InputPropertyValue(gameMechanicsRegion, "shield-blocking-delay", .GM_Shield_Blocking_Delay)
                        InputPropertyValue(gameMechanicsRegion, "scan-for-legacy-ender-dragon", .GM_Scan_For_Legacy_Ender_Dragon)
                        InputPropertyValue(gameMechanicsRegion, "allow-permanent-chunk-loaders", .GM_Allow_Permanent_Chunk_Loaders)
                        InputPropertyValue(JSONWorldSetting, "armor-stands-do-collision-entity-lookups", .Armor_Stands_Do_Collision_Entity_Lookups)
                        InputPropertyValue(JSONWorldSetting, "keep-spawn-loaded", .Keep_Spawn_Loaded)
                        InputPropertyValue(JSONWorldSetting, "disable-ice-and-snow", .Disable_Ice_And_Snow)
                        InputPropertyValue(JSONWorldSetting, "fire-physics-event-for-redstone", .Fire_Physics_Event_For_Redstone)
                        InputPropertyValue(JSONWorldSetting, "skeleton-horse-thunder-spawn-chance", .Skeleton_Horse_Thunder_Spawn_Chance)
                        InputPropertyValue(JSONWorldSetting, "baby-zombie-movement-speed", .Baby_Zombie_Movement_Speed)
                        InputPropertyValue(JSONWorldSetting, "spawner-nerfed-mobs-should-jump", .Spawner_Nerfed_Mobs_Should_Jump)
                        InputPropertyValue(JSONWorldSetting, "allow-leashing-undead-horse", .Allow_Leashing_Undead_Horse)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "squid-spawn-height"), "maximum", .Maximum_Squid_Spawn_Height)
                        InputPropertyValue(JSONWorldSetting, "mob-spawner-tick-rate", .Mob_Spawner_Tick_Rate)
                        InputPropertyValue(JSONWorldSetting, "armor-stands-tick", .Armor_Stands_Tick)
                        InputPropertyValue(JSONWorldSetting, "experience-merge-max-value", .Experience_Merge_Max_Value)
#Region "Anti-Xray"
                        Dim antiXrayRegion As JObject = GetJsonObject(JSONWorldSetting, "anti-xray")
                        InputPropertyValue(antiXrayRegion, "enabled", .Anti_Xray_Enabled)
                        Dim _engineMode As Integer = PaperAntiXrayEngineMode.Normal
                        Dim _chunkEdgeMode As Integer = PaperAntiXrayChunkEdgeMode.LoadAnother
                        InputPropertyValue(antiXrayRegion, "engine-mode", _engineMode)
                        InputPropertyValue(antiXrayRegion, "chunk-edge-mode", _chunkEdgeMode)
                        .Anti_Xray_Engine_Mode = _engineMode
                        .Anti_Xray_Chunk_Edge_Mode = _chunkEdgeMode
                        InputPropertyValue(antiXrayRegion, "max-chunk-section-index", .Anti_Xray_Max_Chunk_Section_Index)
                        InputPropertyValue(antiXrayRegion, "update-radius", .Anti_Xray_Update_Radius)
                        InputPropertyValue(antiXrayRegion, "hidden-blocks", .Anti_Xray_Hidden_Blocks)
                        InputPropertyValue(antiXrayRegion, "replacement-blocks", .Anti_Xray_Replacement_Blocks)
#End Region
                        InputPropertyValue(JSONWorldSetting, "disable-thunder", .Disable_Thunder)
                        InputPropertyValue(JSONWorldSetting, "use-alternate-fallingblock-onGround-detection", .Use_Alternate_Fallingblock_OnGround_Detection)
                        InputPropertyValue(JSONWorldSetting, "non-player-arrow-despawn-rate", .Non_Player_Arrow_Despawn_Rate)
                        InputPropertyValue(JSONWorldSetting, "prevent-tnt-from-moving-in-water", .Prevent_Tnt_From_Moving_In_Water)
                        InputPropertyValue(JSONWorldSetting, "nether-ceiling-void-damage", .Nether_Ceiling_Void_Damage)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "hopper"), "push-based", .Hopper_Push_Based)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "hopper"), "cooldown-when-full", .Hopper_Cooldown_When_Full)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "hopper"), "disable-move-event", .Hopper_Disable_Move_Event)
                        InputPropertyValue(JSONWorldSetting, "allow-non-player-entities-on-scoreboards", .Allow_Non_Player_Entities_On_Scoreboards)
                        InputPropertyValue(JSONWorldSetting, "container-update-tick-rate", .Container_Update_Tick_Rate)
                        InputPropertyValue(JSONWorldSetting, "disable-explosion-knockback", .Disable_Explosion_Knockback)
                        InputPropertyValue(JSONWorldSetting, "parrots-are-unaffected-by-player-movement", .Parrots_Are_Unaffected_by_Player_Movement)
                        InputPropertyValue(JSONWorldSetting, "elytra-hit-wall-damage", .Elytra_Hit_Wall_Damage)
                        InputPropertyValue(JSONWorldSetting, "auto-save-interval", .Auto_Save_Interval)
                        InputPropertyValue(JSONWorldSetting, "grass-spread-tick-rate", .Grass_Spread_Tick_Rate)
                        InputPropertyValue(JSONWorldSetting, "bed-search-radius", .Bed_Search_Radius)
                        InputPropertyValue(JSONWorldSetting, "enable-treasure-maps", .Enable_Treasure_Maps)
                        InputPropertyValue(JSONWorldSetting, "treasure-maps-return-already-discovered", .Treasure_Maps_Return_Already_Discovered)
#Region "Generator Settings"
                        Dim generatorSettingsRegion As JObject = GetJsonObject(JSONWorldSetting, "generator-settings")
                        InputPropertyValue(generatorSettingsRegion, "canyon", .Generate_Canyon)
                        InputPropertyValue(generatorSettingsRegion, "caves", .Generate_Caves)
                        InputPropertyValue(generatorSettingsRegion, "dungeon", .Generate_Dungeon)
                        InputPropertyValue(generatorSettingsRegion, "fortress", .Generate_Fortress)
                        InputPropertyValue(generatorSettingsRegion, "mineshaft", .Generate_Mineshaft)
                        InputPropertyValue(generatorSettingsRegion, "monument", .Generate_Monument)
                        InputPropertyValue(generatorSettingsRegion, "stronghold", .Generate_Stronghold)
                        InputPropertyValue(generatorSettingsRegion, "temple", .Generate_Temple)
                        InputPropertyValue(generatorSettingsRegion, "village", .Generate_Village)
                        InputPropertyValue(generatorSettingsRegion, "flat-bedrock", .Generate_Flat_Bedrock)
                        InputPropertyValue(generatorSettingsRegion, "disable-extreme-hills-emeralds", .Disable_Extreme_Hills_Emeralds)
                        InputPropertyValue(generatorSettingsRegion, "disable-extreme-hills-monster-eggs", .Disable_Extreme_Hills_Monster_Eggs)
                        InputPropertyValue(generatorSettingsRegion, "disable-mesa-additional-gold", .Disable_Mesa_Additional_Gold)
#End Region
                        InputPropertyValue(JSONWorldSetting, "disable-teleportation-suffocation-check", .Disable_Teleportation_Suffocation_Check)
                        InputPropertyValue(JSONWorldSetting, "skip-entity-ticking-in-chunks-scheduled-for-unload", .Skip_Entity_Ticking_In_Chunks_Scheduled_For_Unload)
                        InputPropertyValue(JSONWorldSetting, "portal-search-radius", .Portal_Search_Radius)
                        InputPropertyValue(JSONWorldSetting, "max-auto-save-chunks-per-tick", .Max_Auto_Save_Chunks_Per_Tick)
                        InputPropertyValue(JSONWorldSetting, "queue-light-updates", .Queue_Light_Updates)
                        InputPropertyValue(JSONWorldSetting, "water-over-lava-flow-speed", .Water_Over_Lava_Flow_Speed)
                        InputPropertyValue(JSONWorldSetting, "use-vanilla-world-scoreboard-name-coloring", .Use_Vanilla_World_Scoreboard_Name_Coloring)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "max-growth-height"), "cactus", .Cactus_Max_Growth_Height)
                        InputPropertyValue(GetJsonObject(JSONWorldSetting, "max-growth-height"), "reed", .Reed_Max_Growth_Height)
                        InputPropertyValue(JSONWorldSetting, "max-chunk-gens-per-tick", .Max_Chunk_Gens_Per_Tick)
                        InputPropertyValue(JSONWorldSetting, "save-queue-limit-for-auto-save", .Save_Queue_Limit_For_Auto_Save)
                        InputPropertyValue(JSONWorldSetting, "max-chunk-sends-per-tick", .Max_Chunk_Sends_Per_Tick)
                        InputPropertyValue(JSONWorldSetting, "optimize-explosions", .Ｏptimize_Explosions)
                        InputPropertyValue(JSONWorldSetting, "delay-chunk-unloads-by", .Delay_Chunk_Unloads_By)
                        InputPropertyValue(JSONWorldSetting, "disable-creeper-lingering-effect", .Disable_Creeper_Lingering_Effect)
#Region "Lootables"
                        Dim lootablesRegion As JObject = GetJsonObject(JSONWorldSetting, "lootables")
                        InputPropertyValue(lootablesRegion, "auto-replenish", .Auto_Replenish_Lootables)
                        InputPropertyValue(lootablesRegion, "restrict-player-reloot", .Restrict_Player_Reloot_Lootables)
                        InputPropertyValue(lootablesRegion, "reset-seed-on-fill", .Reset_Seed_On_Fill_Lootables)
                        InputPropertyValue(lootablesRegion, "max-refills", .Max_Refills_Lootables)
                        InputPropertyValue(lootablesRegion, "refresh-min", .Lootables_Refresh_Min)
                        InputPropertyValue(lootablesRegion, "refresh-max", .Lootables_Refresh_Max)
#End Region
                        InputPropertyValue(JSONWorldSetting, "filter-nbt-data-from-spawn-eggs-and-related", .Filter_NBT_Data_From_Spawn_Eggs_And_Related)
                        Dim resolver As String = "saferegen"
                        InputPropertyValue(JSONWorldSetting, "duplicate-uuid-resolver", resolver)
                        .Duplicate_UUID_Resolver = StringToDuplicateUUIDResolverMode(resolver)
                    End With
                    .World_settings.Add(worldSetting)
                Next
#End Region
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
        Dim timingRegion As JObject = GetJsonObject(jsonObject, "timings")
        Dim messageKickRegion As JObject = GetJsonObject(GetJsonObject(jsonObject, "messages"), "kick")
        SetPropertyValue(jsonObject, "verbose", Verbose)
        SetPropertyValue(jsonObject, "config-version", Config_version)
#Region "Settings"
        SetPropertyValue(settingsRegion, "enable-player-collisions", Enable_Player_Collisions)
        SetPropertyValue(settingsRegion, "player-auto-save-rate", Player_Auto_Save_Rate)
        SetPropertyValue(settingsRegion, "max-player-auto-save-per-tick", Max_Player_Auto_Save_Per_Tick)
        SetPropertyValue(settingsRegion, "min-chunk-load-threads", Min_Chunk_Load_Threads)
        SetPropertyValue(settingsRegion, "save-player-data", Save_Player_Data)
        SetPropertyValue(settingsRegion, "bungee-online-mode", Bungee_Online_Mode)
        SetPropertyValue(settingsRegion, "load-permissions-yml-before-plugins", Load_Permissions_yml_Before_Plugins)
        SetPropertyValue(settingsRegion, "save-empty-scoreboard-teams", Save_Empty_Scoreboard_Teams)
        SetPropertyValue(settingsRegion, "incoming-packet-spam-threshold", Incoming_Packet_Spam_Threshold)
        SetPropertyValue(settingsRegion, "suggest-player-names-when-null-tab-completions", Suggest_Player_Names_When_Null_Tab_Completions)
        SetPropertyValue(settingsRegion, "use-alternative-luck-formula", Use_Alternative_Luck_Formula)
        SetPropertyValue(settingsRegion, "sleep-between-chunk-saves", Sleep_Between_Chunk_Saves)
        SetPropertyValue(settingsRegion, "region-file-cache-size", Region_File_Cache_Size)
        SetPropertyValue(settingsRegion, "remove-invalid-statistics", Region_File_Cache_Size)
        SetPropertyValue(jsonObject, "settings", settingsRegion)
#End Region
#Region "Timing"
        SetPropertyValue(timingRegion, "enabled", Timing_Enabled)
        SetPropertyValue(timingRegion, "verbose", Timing_Verbose)
        SetPropertyValue(timingRegion, "server-name-privacy", Timing_Server_Name_Policy)
        SetPropertyValue(timingRegion, "hidden-config-entries", New JArray(Timing_Hidden_Config_Entries))
        SetPropertyValue(timingRegion, "history-interval", Timing_History_Interval)
        SetPropertyValue(timingRegion, "history-length", Timing_History_Length)
        SetPropertyValue(jsonObject, "timings", timingRegion)
#End Region
#Region "Messages"
        SetPropertyValue(messageKickRegion, "authentication-servers-down", Messages_Authentication_Servers_Down)
        SetPropertyValue(messageKickRegion, "flying-player", Messages_Kick_Flying_Player)
        SetPropertyValue(messageKickRegion, "flying-vehicle", Messages_Kick_Flying_Vehicle)
        Dim messagesRegion As JObject = GetJsonObject(jsonObject, "messages")
        SetPropertyValue(messagesRegion, "kick", messageKickRegion)
        SetPropertyValue(jsonObject, "messages", messagesRegion)
#End Region
#Region "World Settings"
        Dim worldSettingsRegion As New JObject
        For Each worldSetting As PaperWorldSettings In World_settings
            Dim JSONWorldSetting As New JObject
            With worldSetting
                SetPropertyValue(JSONWorldSetting, "keep-spawn-loaded-range", .Keep_Spawn_Loaded_Range)

                Dim frostIceRegion As New JObject
                SetPropertyValue(frostIceRegion, "enabled", .Frosted_Ice_Enabled)
                Dim f_delayRegion As New JObject
                SetPropertyValue(GetJsonObject(frostIceRegion, "delay"), "max", .Frosted_Ice_Delay_Max)
                SetPropertyValue(GetJsonObject(frostIceRegion, "delay"), "min", .Frosted_Ice_Delay_Min)
                SetPropertyValue(frostIceRegion, "delay", f_delayRegion)
                SetPropertyValue(JSONWorldSetting, "frosted-ice", frostIceRegion)

                Dim flowRegion As New JObject
                SetPropertyValue(flowRegion, "normal", .Normal_Lava_Flow_Speed)
                SetPropertyValue(flowRegion, "nether", .Nether_Lava_Flow_Speed)
                SetPropertyValue(JSONWorldSetting, "lava-flow-speed", flowRegion)

                SetPropertyValue(JSONWorldSetting, "use-chunk-inhabited-timer", .Use_Chunk_Inhabited_Timer)

                Dim despawnRegion As New JObject
                SetPropertyValue(despawnRegion, "soft", .Soft_Despawn_Ranges)
                SetPropertyValue(despawnRegion, "hard", .Hard_Despawn_Ranges)
                SetPropertyValue(JSONWorldSetting, "despawn-ranges", despawnRegion)

                SetPropertyValue(JSONWorldSetting, "remove-corrupt-tile-entities", .Remove_Corrupt_Tile_Entities)

                Dim drainRegion As New JObject
                SetPropertyValue(drainRegion, "lava", .Lava_Fast_Drain)
                SetPropertyValue(drainRegion, "water", .Water_Fast_Drain)
                SetPropertyValue(JSONWorldSetting, "fast-drain", drainRegion)

                SetPropertyValue(JSONWorldSetting, "falling-block-height-nerf", .Falling_Block_Height_Nerf)
                SetPropertyValue(JSONWorldSetting, "tnt-entity-height-nerf", .TNT_Entity_Height_Nerf)

                Dim fishRegion As New JObject
                SetPropertyValue(fishRegion, "MinimumTicks", .Fishing_Time_Range_MinimumTicks)
                SetPropertyValue(fishRegion, "MaximumTicks", .Fishing_Time_Range_MaximumTicks)
                SetPropertyValue(JSONWorldSetting, "fishing-time-range", fishRegion)

#Region "Game Mechanics"
                Dim gameMechanicsRegion As JObject = GetJsonObject(JSONWorldSetting, "game-mechanics")
                SetPropertyValue(gameMechanicsRegion, "disable-sprint-interruption-on-attack", .GM_Disable_Sprint_Interruption_On_Attack)
                SetPropertyValue(gameMechanicsRegion, "disable-unloaded-chunk-enderpearl-exploit", .GM_Disable_Unloaded_Chunk_Enderpearl_Exploit)
                SetPropertyValue(gameMechanicsRegion, "disable-chest-cat-detection", .GM_Disable_Chest_Cat_Detection)
                SetPropertyValue(gameMechanicsRegion, "disable-end-credits", .GM_Disable_End_Credits)
                SetPropertyValue(gameMechanicsRegion, "disable-player-crits", .GM_Disable_Player_Crits)
                SetPropertyValue(gameMechanicsRegion, "shield-blocking-delay", .GM_Shield_Blocking_Delay)
                SetPropertyValue(gameMechanicsRegion, "scan-for-legacy-ender-dragon", .GM_Scan_For_Legacy_Ender_Dragon)
                SetPropertyValue(gameMechanicsRegion, "allow-permanent-chunk-loaders", .GM_Allow_Permanent_Chunk_Loaders)
                SetPropertyValue(JSONWorldSetting, "game-mechanics", gameMechanicsRegion)
#End Region
                SetPropertyValue(JSONWorldSetting, "armor-stands-do-collision-entity-lookups", .Armor_Stands_Do_Collision_Entity_Lookups)
                SetPropertyValue(JSONWorldSetting, "keep-spawn-loaded", .Keep_Spawn_Loaded)
                SetPropertyValue(JSONWorldSetting, "disable-ice-and-snow", .Disable_Ice_And_Snow)
                SetPropertyValue(JSONWorldSetting, "fire-physics-event-for-redstone", .Fire_Physics_Event_For_Redstone)
                SetPropertyValue(JSONWorldSetting, "skeleton-horse-thunder-spawn-chance", .Skeleton_Horse_Thunder_Spawn_Chance)
                SetPropertyValue(JSONWorldSetting, "baby-zombie-movement-speed", .Baby_Zombie_Movement_Speed)
                SetPropertyValue(JSONWorldSetting, "spawner-nerfed-mobs-should-jump", .Spawner_Nerfed_Mobs_Should_Jump)
                SetPropertyValue(JSONWorldSetting, "allow-leashing-undead-horse", .Allow_Leashing_Undead_Horse)

                Dim squidRegion As New JObject
                SetPropertyValue(squidRegion, "maximum", .Maximum_Squid_Spawn_Height)
                SetPropertyValue(JSONWorldSetting, "squid-spawn-height", squidRegion)

                SetPropertyValue(JSONWorldSetting, "mob-spawner-tick-rate", .Mob_Spawner_Tick_Rate)
                SetPropertyValue(JSONWorldSetting, "armor-stands-tick", .Armor_Stands_Tick)
                SetPropertyValue(JSONWorldSetting, "experience-merge-max-value", .Experience_Merge_Max_Value)
#Region "Anti-Xray"
                Dim antiXrayRegion As New JObject
                SetPropertyValue(antiXrayRegion, "enabled", .Anti_Xray_Enabled)
                Dim _engineMode As Integer = .Anti_Xray_Engine_Mode
                Dim _chunkEdgeMode As Integer = .Anti_Xray_Chunk_Edge_Mode
                SetPropertyValue(antiXrayRegion, "engine-mode", _engineMode)
                SetPropertyValue(antiXrayRegion, "chunk-edge-mode", _chunkEdgeMode)
                SetPropertyValue(antiXrayRegion, "max-chunk-section-index", .Anti_Xray_Max_Chunk_Section_Index)
                SetPropertyValue(antiXrayRegion, "update-radius", .Anti_Xray_Update_Radius)
                SetPropertyValue(antiXrayRegion, "hidden-blocks", New JArray(.Anti_Xray_Hidden_Blocks))
                SetPropertyValue(antiXrayRegion, "replacement-blocks", New JArray(.Anti_Xray_Replacement_Blocks))
                SetPropertyValue(JSONWorldSetting, "anti-xray", antiXrayRegion)
#End Region
                SetPropertyValue(JSONWorldSetting, "disable-thunder", .Disable_Thunder)
                SetPropertyValue(JSONWorldSetting, "use-alternate-fallingblock-onGround-detection", .Use_Alternate_Fallingblock_OnGround_Detection)
                SetPropertyValue(JSONWorldSetting, "non-player-arrow-despawn-rate", .Non_Player_Arrow_Despawn_Rate)
                SetPropertyValue(JSONWorldSetting, "prevent-tnt-from-moving-in-water", .Prevent_Tnt_From_Moving_In_Water)
                SetPropertyValue(JSONWorldSetting, "nether-ceiling-void-damage", .Nether_Ceiling_Void_Damage)

                Dim hopperRegion As New JObject
                SetPropertyValue(hopperRegion, "push-based", .Hopper_Push_Based)
                SetPropertyValue(hopperRegion, "cooldown-when-full", .Hopper_Cooldown_When_Full)
                SetPropertyValue(hopperRegion, "disable-move-event", .Hopper_Disable_Move_Event)
                SetPropertyValue(JSONWorldSetting, "hopper", hopperRegion)

                SetPropertyValue(JSONWorldSetting, "allow-non-player-entities-on-scoreboards", .Allow_Non_Player_Entities_On_Scoreboards)
                SetPropertyValue(JSONWorldSetting, "container-update-tick-rate", .Container_Update_Tick_Rate)
                SetPropertyValue(JSONWorldSetting, "disable-explosion-knockback", .Disable_Explosion_Knockback)
                SetPropertyValue(JSONWorldSetting, "parrots-are-unaffected-by-player-movement", .Parrots_Are_Unaffected_by_Player_Movement)
                SetPropertyValue(JSONWorldSetting, "elytra-hit-wall-damage", .Elytra_Hit_Wall_Damage)
                SetPropertyValue(JSONWorldSetting, "auto-save-interval", .Auto_Save_Interval)
                SetPropertyValue(JSONWorldSetting, "grass-spread-tick-rate", .Grass_Spread_Tick_Rate)
                SetPropertyValue(JSONWorldSetting, "bed-search-radius", .Bed_Search_Radius)
                SetPropertyValue(JSONWorldSetting, "enable-treasure-maps", .Enable_Treasure_Maps)
                SetPropertyValue(JSONWorldSetting, "treasure-maps-return-already-discovered", .Treasure_Maps_Return_Already_Discovered)
#Region "Generator Settings"
                Dim generatorSettingsRegion As New JObject
                SetPropertyValue(generatorSettingsRegion, "canyon", .Generate_Canyon)
                SetPropertyValue(generatorSettingsRegion, "caves", .Generate_Caves)
                SetPropertyValue(generatorSettingsRegion, "dungeon", .Generate_Dungeon)
                SetPropertyValue(generatorSettingsRegion, "fortress", .Generate_Fortress)
                SetPropertyValue(generatorSettingsRegion, "mineshaft", .Generate_Mineshaft)
                SetPropertyValue(generatorSettingsRegion, "monument", .Generate_Monument)
                SetPropertyValue(generatorSettingsRegion, "stronghold", .Generate_Stronghold)
                SetPropertyValue(generatorSettingsRegion, "temple", .Generate_Temple)
                SetPropertyValue(generatorSettingsRegion, "village", .Generate_Village)
                SetPropertyValue(generatorSettingsRegion, "flat-bedrock", .Generate_Flat_Bedrock)
                SetPropertyValue(generatorSettingsRegion, "disable-extreme-hills-emeralds", .Disable_Extreme_Hills_Emeralds)
                SetPropertyValue(generatorSettingsRegion, "disable-extreme-hills-monster-eggs", .Disable_Extreme_Hills_Monster_Eggs)
                SetPropertyValue(generatorSettingsRegion, "disable-mesa-additional-gold", .Disable_Mesa_Additional_Gold)
                SetPropertyValue(JSONWorldSetting, "generator-settings", generatorSettingsRegion)
#End Region
                SetPropertyValue(JSONWorldSetting, "disable-teleportation-suffocation-check", .Disable_Teleportation_Suffocation_Check)
                SetPropertyValue(JSONWorldSetting, "skip-entity-ticking-in-chunks-scheduled-for-unload", .Skip_Entity_Ticking_In_Chunks_Scheduled_For_Unload)
                SetPropertyValue(JSONWorldSetting, "portal-search-radius", .Portal_Search_Radius)
                SetPropertyValue(JSONWorldSetting, "max-auto-save-chunks-per-tick", .Max_Auto_Save_Chunks_Per_Tick)
                SetPropertyValue(JSONWorldSetting, "queue-light-updates", .Queue_Light_Updates)
                SetPropertyValue(JSONWorldSetting, "water-over-lava-flow-speed", .Water_Over_Lava_Flow_Speed)
                SetPropertyValue(JSONWorldSetting, "use-vanilla-world-scoreboard-name-coloring", .Use_Vanilla_World_Scoreboard_Name_Coloring)

                Dim growthRegion As New JObject
                SetPropertyValue(growthRegion, "cactus", .Cactus_Max_Growth_Height)
                SetPropertyValue(growthRegion, "reed", .Reed_Max_Growth_Height)
                SetPropertyValue(JSONWorldSetting, "max-growth-height", growthRegion)

                SetPropertyValue(JSONWorldSetting, "max-chunk-gens-per-tick", .Max_Chunk_Gens_Per_Tick)
                SetPropertyValue(JSONWorldSetting, "save-queue-limit-for-auto-save", .Save_Queue_Limit_For_Auto_Save)
                SetPropertyValue(JSONWorldSetting, "max-chunk-sends-per-tick", .Max_Chunk_Sends_Per_Tick)
                SetPropertyValue(JSONWorldSetting, "optimize-explosions", .Ｏptimize_Explosions)
                SetPropertyValue(JSONWorldSetting, "delay-chunk-unloads-by", .Delay_Chunk_Unloads_By)
                SetPropertyValue(JSONWorldSetting, "disable-creeper-lingering-effect", .Disable_Creeper_Lingering_Effect)
#Region "Lootables"
                Dim lootablesRegion As New JObject
                SetPropertyValue(lootablesRegion, "auto-replenish", .Auto_Replenish_Lootables)
                SetPropertyValue(lootablesRegion, "restrict-player-reloot", .Restrict_Player_Reloot_Lootables)
                SetPropertyValue(lootablesRegion, "reset-seed-on-fill", .Reset_Seed_On_Fill_Lootables)
                SetPropertyValue(lootablesRegion, "max-refills", .Max_Refills_Lootables)
                SetPropertyValue(lootablesRegion, "refresh-min", .Lootables_Refresh_Min)
                SetPropertyValue(lootablesRegion, "refresh-max", .Lootables_Refresh_Max)
                SetPropertyValue(JSONWorldSetting, "lootables", lootablesRegion)
#End Region
                SetPropertyValue(JSONWorldSetting, "filter-nbt-data-from-spawn-eggs-and-related", .Filter_NBT_Data_From_Spawn_Eggs_And_Related)
                SetPropertyValue(JSONWorldSetting, "duplicate-uuid-resolver", DuplicateUUIDResolverModeToString(.Duplicate_UUID_Resolver))
            End With
            worldSettingsRegion.Add(worldSetting.Name, JSONWorldSetting)
        Next
        SetPropertyValue(jsonObject, "world-settings", worldSettingsRegion)
#End Region
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
#Region "Paper YAML Object"
    Enum PaperAntiXrayEngineMode
        Normal = 1
        Advanced = 2
    End Enum
    Enum PaperAntiXrayChunkEdgeMode
        Disabled = 1
        NotSend = 2
        LoadAnother = 3
    End Enum
    Enum PaperDuplicateUUIDResolverMode
        Regenerate_Safely
        Delete
        Slient
        Log
    End Enum
    ''' <summary>
    ''' Paper 世界設定
    ''' </summary>
    Public Class PaperWorldSettings
        <DisplayName("(名稱)")> <Category("一般")> <Description("此屬性必須設定成要設定的世界名稱或default(作為預設值)且不可重複。")>
        Public Property Name As String = ""
        <DisplayName("持續載入出生點區域範圍")> <DefaultValue(8)> <Category("一般")> <Description("在出生點附近多少區塊需持續載入。")>
        Public Property Keep_Spawn_Loaded_Range As Integer = 8
        <DisplayName("自動儲存頻率")> <DefaultValue(-1)> <Category("一般")> <Description("指定這個世界自動保存的頻率。" &
                                                         vbNewLine & "-1 - 使用Bukkit的通用設定值")>
        Public Property Auto_Save_Interval As Integer = -1
        <DisplayName("地獄頂部需承受虛空傷害")> <DefaultValue(False)> <Category("一般")> <Description("當實體位於地獄頂部時會受到虛空傷害。")>
        Public Property Nether_Ceiling_Void_Damage As Boolean = False
#Region "遊戲機制"
        <DisplayName("在攻擊時解除衝刺")> <DefaultValue(False)> <Category("遊戲機制")> <Description("是否在攻擊時解除衝刺。")>
        Public Property GM_Disable_Sprint_Interruption_On_Attack As Boolean = False
        <DisplayName("禁用結局畫面")> <DefaultValue(False)> <Category("遊戲機制")> <Description("伺服器是否在玩家離開終界時不發送遊戲結局畫面。")>
        Public Property GM_Disable_End_Credits As Boolean = False
        <DisplayName("禁用爆擊")> <DefaultValue(False)> <Category("遊戲機制")> <Description("伺服器是否在PvP中禁用暴擊，而是將它們視為正常攻擊。")>
        Public Property GM_Disable_Player_Crits As Boolean = False
        <DisplayName("盾的格檔延遲")> <DefaultValue(5)> <Category("遊戲機制")> <Description("盾的格檔時間間隔（以遊戲刻計算）。")>
        Public Property GM_Shield_Blocking_Delay As Integer = 5
        <DisplayName("玩家是否可以打開貓下的箱子")> <DefaultValue(False)> <Category("遊戲機制")> <Description("允許玩家在有貓坐著時在打開下面的箱子。")>
        Public Property GM_Disable_Chest_Cat_Detection As Boolean = False
        <DisplayName("修復終界珍珠漏洞")> <DefaultValue(True)> <Category("遊戲機制")> <Description("是否啟用對終界珍珠旅行的漏洞進行的修復。")>
        Public Property GM_Disable_Unloaded_Chunk_Enderpearl_Exploit As Boolean = True
        <DisplayName("允許使用永久區塊載入器")> <DefaultValue(False)> <Category("遊戲機制")> <Description("是否允許使用永久的區塊載入器。")>
        Public Property GM_Allow_Permanent_Chunk_Loaders As Boolean = False
        <DisplayName("檢查終界龍掉落物")> <DefaultValue(True)> <Category("遊戲機制")> <Description("是否檢查終界龍掉落物。")>
        Public Property GM_Scan_For_Legacy_Ender_Dragon As Boolean = True

#End Region
        <DisplayName("霜冰是否融化")> <DefaultValue(True)> <Category("行為")> <Description("是否啟用霜冰的溶化機制。")>
        Public Property Frosted_Ice_Enabled As Boolean = True
        <DisplayName("霜冰融化最小延遲")> <DefaultValue(20)> <Category("行為")> <Description("應用在霜冰效果的隨機數產生器(RNG)的最底值。")>
        Public Property Frosted_Ice_Delay_Min As Integer = 20
        <DisplayName("霜冰融化最大延遲")> <DefaultValue(40)> <Category("行為")> <Description("應用在霜冰效果的隨機數產生器(RNG)的最高值。")>
        Public Property Frosted_Ice_Delay_Max As Integer = 40
        <DisplayName("允許非玩家實體在計分板上")> <DefaultValue(False)> <Category("一般")> <Description("是否允許將非玩家實體放在記分板上。")>
        Public Property Allow_Non_Player_Entities_On_Scoreboards As Boolean = False
        <DisplayName("替代的漏斗傳輸機制")> <DefaultValue(False)> <Category("行為")> <Description("是否使用替代的漏斗傳輸機制。")>
        Public Property Hopper_Push_Based As Boolean = False
        <DisplayName("漏斗裝滿時暫停吸取")> <DefaultValue(True)> <Category("行為")> <Description("是否在漏斗裝滿時進行冷卻，而不是一直嘗試拉取新物品。")>
        Public Property Hopper_Cooldown_When_Full As Boolean = True
        <DisplayName("禁止漏斗傳輸時產生事件")> <DefaultValue(False)> <Category("行為")> <Description("是否不讓漏斗產生""InventoryMoveItemEvent""事件。 可顯著地改善漏斗性能，" &
                                                            vbNewLine & "但會使所有依賴此事件插件（包含領地保護插件）故障。")>
        Public Property Hopper_Disable_Move_Event As Boolean = False
        <DisplayName("容器更新速率")> <DefaultValue(1)> <Category("一般")> <Description("更新容器（箱子、發射器等）和物品欄的速率。（以遊戲刻計算）")>
        Public Property Container_Update_Tick_Rate As Integer = 1
        <DisplayName("禁止爆炸擊退")> <DefaultValue(False)> <Category("一般")> <Description("是否阻止因爆炸而發生的任何擊退。")>
        Public Property Disable_Explosion_Knockback As Boolean = False
        <DisplayName("鸚鵡不因移動而掉下")> <DefaultValue(False)> <Category("一般")> <Description("是否使鸚鵡不會因玩家的任何移動而掉下來。（取而代之的是用[Shift]）")>
        Public Property Parrots_Are_Unaffected_by_Player_Movement As Boolean = False
        <DisplayName("滑翔撞牆傷害")> <DefaultValue(True)> <Category("一般")> <Description("使用鞘翅滑翔的玩家是否會因撞牆而受傷。")>
        Public Property Elytra_Hit_Wall_Damage As Boolean = True
        <DisplayName("安全重生範圍")> <DefaultValue(1)> <Category("一般")> <Description("床被占用或阻擋時，在附近多少格內嘗試使玩家安全的重生。")>
        Public Property Bed_Search_Radius As Integer = 1
        <DisplayName("草地蔓延速率")> <DefaultValue(1)> <Category("一般")> <Description("嘗試傳播草地的延遲。（以遊戲刻來計算）")>
        Public Property Grass_Spread_Tick_Rate As Integer = 1
        <DisplayName("額外的方塊落地檢測")> <DefaultValue(False)> <Category("行為")> <Description("是否使用額外的檢測系統來更好地處理落在物體上的墜落方塊（沙、礫石等）。")>
        Public Property Use_Alternate_Fallingblock_OnGround_Detection As Boolean = False
        <DisplayName("非玩家發射之箭矢消失時間")> <DefaultValue(-1)> <Category("一般")> <Description("非玩家實體射出的箭矢的消失時間。" &
                                                         vbNewLine & "-1 - 使用""Spigot 設定""中的""箭矢消失時間""的值")>
        Public Property Non_Player_Arrow_Despawn_Rate As Integer = -1
        <DisplayName("TNT 能被水流推動")> <DefaultValue(False)> <Category("行為")> <Description("被點燃的TNT是否能被水流推動。")>
        Public Property Prevent_Tnt_From_Moving_In_Water As Boolean = False
        <DisplayName("持續加載出生點區塊")> <DefaultValue(True)> <Category("一般")> <Description("是否持續加載出生點區塊。")>
        Public Property Keep_Spawn_Loaded As Boolean = True
        <DisplayName("充能紅石時是否觸發事件")> <DefaultValue(False)> <Category("一般")> <Description("在充能紅石時是否觸發""BlockPhysicsEvent""事件。")>
        Public Property Fire_Physics_Event_For_Redstone As Boolean = False
        <DisplayName("盔甲架的實體碰撞檢查")> <DefaultValue(True)> <Category("行為")> <Description("是否需要在盔甲架上進行實體碰撞檢查。")>
        Public Property Armor_Stands_Do_Collision_Entity_Lookups As Boolean = True
        <DisplayName("骷髏陷阱馬的生成機率")> <DefaultValue(0.01)> <Category("一般")> <Description("骷髏陷阱馬在雷雨中生成的機率。")>
        Public Property Skeleton_Horse_Thunder_Spawn_Chance As Double = 0.01
        <DisplayName("禁止冰雪產生")> <DefaultValue(False)> <Category("一般")> <Description("是否禁止冰與雪的產生。")>
        Public Property Disable_Ice_And_Snow As Boolean = False
        <DisplayName("檢查盔甲架")> <DefaultValue(True)> <Category("一般")> <Description("盔甲架是否進行每遊戲刻的檢查。")>
        Public Property Armor_Stands_Tick As Boolean = True
        <DisplayName("經驗合併最大值")> <DefaultValue(-1)> <Category("一般")> <Description("設定經驗球經驗上限，限制經驗球的合併次數。" &
                                                         vbNewLine & "-1 - 無限制，在一定範圍內的經驗球皆會合併。")>
        Public Property Experience_Merge_Max_Value As Integer = -1
#Region "防作弊(Anti-Xray)"
        <DisplayName("啟用 Anti-Xray")> <DefaultValue(False)> <Category("防作弊")> <Description("是否啟用""Anti-Xray""（會使透視外掛失效）。")>
        Public Property Anti_Xray_Enabled As Boolean = False
        <DisplayName("Anti-Xray 的防護模式")> <DefaultValue(PaperAntiXrayEngineMode.Normal)> <Category("防作弊")> <Description("""Anti-Xray""的防護模式。" &
                                                             vbNewLine & "Normal - 用石頭代替未露出的方塊" &
                                                             vbNewLine & "Advanced - 用隨機的方塊數據代替未露出的方塊")>
        Public Property Anti_Xray_Engine_Mode As PaperAntiXrayEngineMode = PaperAntiXrayEngineMode.Normal
        <DisplayName("Anti-Xray 的區塊邊界處理模式")> <DefaultValue(PaperAntiXrayChunkEdgeMode.LoadAnother)> <Category("防作弊")> <Description("""Anti-Xray""在處理未載入的區塊邊緣時的防護模式。" &
                                                     vbNewLine & "Disabled - 不處理" &
                                                     vbNewLine & "NotSend - 不傳送該區塊（將使玩家的視野距離減少）" &
                                                     vbNewLine & "LoadAnother - 加載相鄰的區塊（將使玩家的視野距離增廣）")>
        Public Property Anti_Xray_Chunk_Edge_Mode As PaperAntiXrayChunkEdgeMode = PaperAntiXrayChunkEdgeMode.LoadAnother
        <DisplayName("Anti-Xray 處理的高度索引")> <DefaultValue(3)> <Category("防作弊")> <Description("""Anti-Xray""在處理時應處理的Y軸索引最大值。" &
                                                     vbNewLine & "在Paper 內部將會以(<此值> +1) * 16 來定義處理的總高度。")>
        Public Property Anti_Xray_Max_Chunk_Section_Index As Integer = 3
        <DisplayName("Anti-Xray 的更新範圍")> <DefaultValue(2)> <Category("防作弊")> <Description("""Anti-Xray""在阻止透視時將顯示方塊後多少距離的所有方塊（玩家眼前的方塊也算一個）。")>
        Public Property Anti_Xray_Update_Radius As Integer = 2
        <DisplayName("Anti-Xray 需要隱藏的方塊")> <DefaultValue({
            "gold_ore",
            "iron_ore",
            "coal_ore",
            "lapis_ore",
            "mossy_cobblestone",
            "obsidian",
            "chest",
            "diamond_ore",
            "redstone_ore",
            "lit_redstone_ore",
            "clay",
            "emerald_ore",
            "ender_chest"
        })> <Category("防作弊")> <Description("要隱藏的方塊（要輸入方塊ID）。")> <Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
        Public Property Anti_Xray_Hidden_Blocks As String() = {
            "gold_ore",
            "iron_ore",
            "coal_ore",
            "lapis_ore",
            "mossy_cobblestone",
            "obsidian",
            "chest",
            "diamond_ore",
            "redstone_ore",
            "lit_redstone_ore",
            "clay",
            "emerald_ore",
            "ender_chest"
        }
        <DisplayName("Anti-Xray 用於隱藏的方塊")> <DefaultValue({
         "stone",
         "planks"
        })> <Category("防作弊")> <Description("在""Advanced""防護模式下要拿來偽裝的方塊（要輸入方塊ID）。")> <Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
        Public Property Anti_Xray_Replacement_Blocks As String() = {
         "stone",
         "planks"
        }
#End Region
        <DisplayName("禁止雷暴天氣")> <DefaultValue(False)> <Category("一般")> <Description("是否禁止雷暴的產生。")>
        Public Property Disable_Thunder As Boolean = False
        <DisplayName("不卸載玩家周圍區塊")> <DefaultValue(True)> <Category("一般")> <Description("是否讓玩家身旁的區塊不要被卸載，" &
                                                           vbNewLine & "以減少區塊的頻繁重複加載並減少CPU的運行週期。" &
                                                           vbNewLine & "此功能將會使區塊垃圾回收機制沒有用處。")>
        Public Property Skip_Entity_Ticking_In_Chunks_Scheduled_For_Unload As Boolean = True
        <DisplayName("使用區塊占用計時器")> <DefaultValue(True)> <Category("一般")> <Description("是否將區塊占用計時器納入計算當中。")>
        Public Property Use_Chunk_Inhabited_Timer As Boolean = True
        <DisplayName("岩漿流速(正常世界)")> <DefaultValue(30)> <Category("一般")> <Description("在正常世界中岩漿的流速（以每格幾遊戲刻計算）。")>
        Public Property Normal_Lava_Flow_Speed As Integer = 30
        <DisplayName("岩漿流速(地獄)")> <DefaultValue(10)> <Category("一般")> <Description("在地獄中岩漿的流速（以每格幾遊戲刻計算）。")>
        Public Property Nether_Lava_Flow_Speed As Integer = 10
        <DisplayName("移除損壞的方塊實體")> <DefaultValue(False)> <Category("一般")> <Description("是否移除損壞的方塊實體。")>
        Public Property Remove_Corrupt_Tile_Entities As Boolean = False
        <DisplayName("實體隨機移除範圍")> <DefaultValue(32)> <Category("一般")> <Description("隨機移除多少格外的實體。")>
        Public Property Soft_Despawn_Ranges As Integer = 32
        <DisplayName("實體強制移除範圍")> <DefaultValue(128)> <Category("一般")> <Description("強制移除多少格外的實體。")>
        Public Property Hard_Despawn_Ranges As Integer = 128
        <DisplayName("刪除指定高度以下的掉落方塊")> <DefaultValue(0)> <Category("一般")> <Description("刪除距離地面多少格內的掉落方塊實體（沙子、礫石等）。" &
                                                        vbNewLine & "如果此值小於1的話將會禁用此設定")>
        Public Property Falling_Block_Height_Nerf As Integer = 0
        <DisplayName("刪除指定高度以下的掉落TNT")> <DefaultValue(0)> <Category("一般")> <Description("刪除距離地面多少格內的點燃TNT。" &
                                                vbNewLine & "如果此值小於1的話將會禁用此設定")>
        Public Property TNT_Entity_Height_Nerf As Integer = 0
        <DisplayName("釣魚最小所需時間")> <DefaultValue(100)> <Category("行為")> <Description("上鉤時間的隨機數產生器(RNG)的最底值（以遊戲刻計算）。")>
        Public Property Fishing_Time_Range_MinimumTicks As Integer = 100
        <DisplayName("釣魚最大所需時間")> <DefaultValue(600)> <Category("行為")> <Description("上鉤時間的隨機數產生器(RNG)的最高值（以遊戲刻計算）。")>
        Public Property Fishing_Time_Range_MaximumTicks As Integer = 600
        <DisplayName("失去源頭的岩漿迅速消退")> <DefaultValue(False)> <Category("一般")> <Description("岩漿源移除後附近的岩漿流是否迅速的消退。")>
        Public Property Lava_Fast_Drain As Boolean = False
        <DisplayName("失去源頭的水迅速消退")> <DefaultValue(False)> <Category("一般")> <Description("水源移除後附近的水流是否迅速的消退。")>
        Public Property Water_Fast_Drain As Boolean = False
        <DisplayName("小殭屍的速度")> <DefaultValue(0.5#)> <Category("一般")> <Description("小殭屍的移動速度。")>
        Public Property Baby_Zombie_Movement_Speed As Double = 0.5#
        <DisplayName("生怪磚無AI生物是否會跳")> <DefaultValue(False)> <Category("行為")> <Description("啟用Spigot 設定的""生怪磚怪物無AI""後，那些生物是否會跳躍。")>
        Public Property Spawner_Nerfed_Mobs_Should_Jump As Boolean = False
        <DisplayName("可以牽引不死馬")> <DefaultValue(False)> <Category("一般")> <Description("玩家是否能用韁繩牽引殭屍馬或骷髏馬。")>
        Public Property Allow_Leashing_Undead_Horse As Boolean = False
        <DisplayName("生怪磚生成頻率")> <DefaultValue(1)> <Category("一般")> <Description("生怪磚應該多久（以遊戲刻計算）檢查一次可用的生怪區域並將新實體生成到世界上。")>
        Public Property Mob_Spawner_Tick_Rate As Integer = 1
        <DisplayName("魷魚最大生成高度")> <DefaultValue(0)> <Category("一般")> <Description("魷魚的最大生成高度。" &
                                                          vbNewLine & "0 - 使用Minecraft 的預設值（在1.12為64）")>
        Public Property Maximum_Squid_Spawn_Height As Integer = 0
        <DisplayName("停用傳送卡牆檢測")> <DefaultValue(False)> <Category("一般")> <Description("是否停用傳送時的卡牆檢查。")>
        Public Property Disable_Teleportation_Suffocation_Check As Boolean = False
        <DisplayName("啟用探險家地圖")> <DefaultValue(True)> <Category("行為")> <Description("村民是否可以販售""探險家""地圖。")>
        Public Property Enable_Treasure_Maps As Boolean = True
        <DisplayName("探險家地圖將指引至最近的遺跡")> <DefaultValue(False)> <Category("行為")> <Description("初次開啟一張""探險家""地圖時是否指引到最近的遺跡（不管有沒有人探索過）。")>
        Public Property Treasure_Maps_Return_Already_Discovered As Boolean = False
#Region "地圖生成器設定"
        <DisplayName("生成峽谷")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成峽谷。")>
        Public Property Generate_Canyon As Boolean = True
        <DisplayName("生成洞穴")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成洞穴。")>
        Public Property Generate_Caves As Boolean = True
        <DisplayName("生成地牢")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成地牢。")>
        Public Property Generate_Dungeon As Boolean = True
        <DisplayName("生成要塞")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成地獄要塞。")>
        Public Property Generate_Fortress As Boolean = True
        <DisplayName("生成礦坑")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成廢棄礦坑。")>
        Public Property Generate_Mineshaft As Boolean = True
        <DisplayName("生成深海遺跡")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成深海遺跡。")>
        Public Property Generate_Monument As Boolean = True
        <DisplayName("生成祭壇")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成終界祭壇。")>
        Public Property Generate_Stronghold As Boolean = True
        <DisplayName("生成神廟")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成叢林神廟。")>
        Public Property Generate_Temple As Boolean = True
        <DisplayName("生成村莊")> <DefaultValue(True)> <Category("地圖生成器設定")> <Description("是否生成村莊。")>
        Public Property Generate_Village As Boolean = True
        <DisplayName("生成平坦基岩地形")> <DefaultValue(False)> <Category("地圖生成器設定")> <Description("是否生成平坦單層基岩地形。")>
        Public Property Generate_Flat_Bedrock As Boolean = False
        <DisplayName("禁止在峭壁上生成綠寶石")> <DefaultValue(False)> <Category("地圖生成器設定")> <Description("是否不要在峭壁上生成綠寶石礦。")>
        Public Property Disable_Extreme_Hills_Emeralds As Boolean = False
        <DisplayName("禁止在峭壁上生成怪物蛋")> <DefaultValue(False)> <Category("地圖生成器設定")> <Description("是否不要在峭壁上生成被蛀蝕的石頭（舊稱怪物蛋）。")>
        Public Property Disable_Extreme_Hills_Monster_Eggs As Boolean = False
        <DisplayName("禁止在惡地上生成更多金")> <DefaultValue(False)> <Category("地圖生成器設定")> <Description("是否不要在惡地（舊稱平頂山）上生成更多的金礦。")>
        Public Property Disable_Mesa_Additional_Gold As Boolean = False
#End Region
        <DisplayName("傳送門搜尋範圍")> <DefaultValue(128)> <Category("行為")> <Description("用地獄傳送門傳送時要在目標地附近多少格內嘗試搜尋一個現有的傳送門（找不到的話會建立一個新的）。")>
        Public Property Portal_Search_Radius As Integer = 128
        <DisplayName("優化爆炸")> <DefaultValue(False)> <Category("一般")> <Description("在爆炸期間是否儲存實體查找資料，而不是在整個過程中持續重新計算。")>
        Public Property Ｏptimize_Explosions As Boolean = False
        <DisplayName("最大同時儲存區塊量")> <DefaultValue(24)> <Category("一般")> <Description("在單個遊戲刻內最多能儲存多少個區塊。")>
        Public Property Max_Auto_Save_Chunks_Per_Tick As Integer = 24
        <DisplayName("使用原版世界的記分板名稱著色")> <DefaultValue(False)> <Category("一般")> <Description("是否使用原版的記分版系統來為玩家的暱稱著色。")>
        Public Property Use_Vanilla_World_Scoreboard_Name_Coloring As Boolean = False
        <DisplayName("最大同時傳送區塊量")> <DefaultValue(81)> <Category("一般")> <Description("在單個遊戲刻內最多能傳送多少個區塊給玩家。")>
        Public Property Max_Chunk_Sends_Per_Tick As Integer = 81
        <DisplayName("區塊卸載延遲")> <DefaultValue("10s")> <Category("一般")> <Description("要延遲多久時間（要指定單位，單位在第二段）才將一個未使用區塊卸載。" &
                                                            vbNewLine & "單位：" &
                                                            vbNewLine & "秒=s" &
                                                            vbNewLine & "分=m" &
                                                            vbNewLine & "時=h" &
                                                            vbNewLine & "日=d")>
        Public Property Delay_Chunk_Unloads_By As String = "10s"
        <DisplayName("自動儲存區塊序列限制")> <DefaultValue(50)> <Category("一般")> <Description("如果自動儲存區塊的序列中的區塊數高於此值，將不再添加更多區塊。")>
        Public Property Save_Queue_Limit_For_Auto_Save As Integer = 50
        <DisplayName("最大同時生成區塊量")> <DefaultValue(10)> <Category("一般")> <Description("在單個遊戲刻內最多能生成多少個區塊。")>
        Public Property Max_Chunk_Gens_Per_Tick As Integer = 10
        <DisplayName("仙人掌最大生長高度")> <DefaultValue(3)> <Category("一般")> <Description("仙人掌的最大生長高度。")>
        Public Property Cactus_Max_Growth_Height As Integer = 3
        <DisplayName("甘蔗最大生長高度")> <DefaultValue(3)> <Category("一般")> <Description("甘蔗的最大生長高度。")>
        Public Property Reed_Max_Growth_Height As Integer = 3
        <DisplayName("將光照計算加入排程")> <DefaultValue(False)> <Category("一般")> <Description("是否將光照計算放入每遊戲刻的排程運算中。")>
        Public Property Queue_Light_Updates As Boolean = False
        <DisplayName("重新裝填寶箱")> <DefaultValue(False)> <Category("一般")> <Description("是否自動重新填裝具有可掠奪物品表的箱子（如寶箱）。")>
        Public Property Auto_Replenish_Lootables As Boolean = False
        <DisplayName("阻止玩家刷寶箱資源")> <DefaultValue(True)> <Category("一般")> <Description("是否阻止同個玩家利用可重新填裝的寶箱來刷資源。")>
        Public Property Restrict_Player_Reloot_Lootables As Boolean = True
        <DisplayName("隨機裝填寶箱")> <DefaultValue(True)> <Category("一般")> <Description("是否在重填充寶箱時更換提取種子（這將使每次的內容物與前次不同）。")>
        Public Property Reset_Seed_On_Fill_Lootables As Boolean = True
        <DisplayName("重裝填寶箱次數")> <DefaultValue(-1)> <Category("一般")> <Description("重填充寶箱的次數。" &
                                                         vbNewLine & "-1 - 無限制填裝次數")>
        Public Property Max_Refills_Lootables As Integer = -1
        <DisplayName("最小重裝填寶箱時間")> <DefaultValue("12h")> <Category("一般")> <Description("最少多久（要指定單位，單位在第二段）填裝一次寶箱。" &
                                                    vbNewLine & "單位：" &
                                                    vbNewLine & "秒=s" &
                                                    vbNewLine & "分=m" &
                                                    vbNewLine & "時=h" &
                                                    vbNewLine & "日=d")>
        Public Property Lootables_Refresh_Min As String = "12h"
        <DisplayName("最大重裝填寶箱時間")> <DefaultValue("2d")> <Category("一般")> <Description("最多多久（要指定單位，單位在第二段）填裝一次寶箱。" &
                                                            vbNewLine & "單位：" &
                                                            vbNewLine & "秒=s" &
                                                            vbNewLine & "分=m" &
                                                            vbNewLine & "時=h" &
                                                            vbNewLine & "日=d")>
        Public Property Lootables_Refresh_Max As String = "2d"
        <DisplayName("水流過岩漿時的速度")> <DefaultValue(5)> <Category("一般")> <Description("水流過岩漿時的速度。")>
        Public Property Water_Over_Lava_Flow_Speed As Integer = 5
        <DisplayName("過濾NBT標籤")> <DefaultValue(True)> <Category("一般")> <Description("過濾生怪蛋及一些常被用作作弊手段的實體的NBT。")>
        Public Property Filter_NBT_Data_From_Spawn_Eggs_And_Related As Boolean = True
        <DisplayName("阻止苦力怕留下藥水雲")> <DefaultValue(False)> <Category("一般")> <Description("是否阻止苦力怕留下滯留藥水雲。")>
        Public Property Disable_Creeper_Lingering_Effect As Boolean = False
        <DisplayName("實體碰撞最大數量")> <DefaultValue(8)> <Category("行為")> <Description("實體碰撞的最大數量。（超過這個數量伺服器將不會處理任何實體碰撞）")>
        Public Property Max_Entity_Collisions As Integer = 8
        <DisplayName("重複UUID實體處置方式")> <DefaultValue(PaperDuplicateUUIDResolverMode.Regenerate_Safely)> <Category("一般")> <Description("該如何處理有重複UUID的實體。" &
                                                                                                       vbNewLine & "Regenerate_Safely - 以安全的方式重新生成UUID給此實體" &
                                                                                                       vbNewLine & "Delete - 刪除實體" &
                                                                                                       vbNewLine & "Slient - 不處理" &
                                                                                                       vbNewLine & "Log - 紀錄該實體資訊")>
        Public Property Duplicate_UUID_Resolver As PaperDuplicateUUIDResolverMode = PaperDuplicateUUIDResolverMode.Regenerate_Safely
    End Class

#End Region
    Public Class PaperWorldSettingsCollectionEditor
        Inherits CollectionEditor
        Dim itemCount As Integer = 1
        Public Sub New()
            MyBase.New(type:=GetType(List(Of PaperWorldSettings)))
        End Sub
        Protected Overrides Function CreateCollectionForm() As CollectionForm
            Dim form = MyBase.CreateCollectionForm()
            form.Text = "Paper 世界設定編輯器"
            AddHandler form.Shown, Sub()
                                       ShowDescription(form)
                                   End Sub
            Return form
        End Function
        Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
            Dim settings = New PaperWorldSettings
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
            Dim settings As PaperWorldSettings = value
            Return settings.Name.ToLower <> "default"
        End Function
        Protected Overrides Function GetDisplayText(value As Object) As String
            Dim settings As PaperWorldSettings = value
            Return settings.Name
        End Function
    End Class
    Shared Function DuplicateUUIDResolverModeToString(mode As PaperDuplicateUUIDResolverMode) As String
        Select Case mode
            Case PaperDuplicateUUIDResolverMode.Regenerate_Safely
                Return "saferegen"
            Case PaperDuplicateUUIDResolverMode.Delete
                Return "remove"
            Case PaperDuplicateUUIDResolverMode.Slient
                Return "nothing"
            Case PaperDuplicateUUIDResolverMode.Log
                Return "warn"
            Case Else
                Return "saferegen"
        End Select
    End Function
    Shared Function StringToDuplicateUUIDResolverMode(mode As String) As PaperDuplicateUUIDResolverMode
        Select Case mode.ToLower
            Case "saferegen"
                Return PaperDuplicateUUIDResolverMode.Regenerate_Safely
            Case "saferegenerate"
                Return PaperDuplicateUUIDResolverMode.Regenerate_Safely
            Case "regenerate"
                Return PaperDuplicateUUIDResolverMode.Regenerate_Safely
            Case "regen"
                Return PaperDuplicateUUIDResolverMode.Regenerate_Safely
            Case "remove"
                Return PaperDuplicateUUIDResolverMode.Delete
            Case "delete"
                Return PaperDuplicateUUIDResolverMode.Delete
            Case "slient"
                Return PaperDuplicateUUIDResolverMode.Slient
            Case "nothing"
                Return PaperDuplicateUUIDResolverMode.Slient
            Case "log"
                Return PaperDuplicateUUIDResolverMode.Log
            Case "warn"
                Return PaperDuplicateUUIDResolverMode.Log
            Case Else
                Return PaperDuplicateUUIDResolverMode.Regenerate_Safely
        End Select
    End Function
End Class