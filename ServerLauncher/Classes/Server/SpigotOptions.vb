Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports YamlDotNet.Serialization

''' <summary>
''' spigot.yml 的對應.NET 類別
''' </summary>
Public Class SpigotOptions
    Dim path As String = ""
#Region "通用設定"
    Dim Config_version As Integer = 13
    Dim Debug As Boolean = False
    <DisplayName("僅停止時儲存快取")> <DefaultValue(False)> <Category("通用設定 - 一般")> <Description("是否只在伺服器停止時保存玩家快取。")>
    Public Property Save_user_cache_on_stop_only As Boolean = False
#Region "屬性最大值"
    <DisplayName("血量上限")> <DefaultValue(2048.0!)> <Category("通用設定 - 屬性")> <Description("設定實體血量的最高上限。")>
    Public Property MaxHealth As Single = 2048.0!
    <DisplayName("移動速度上限")> <DefaultValue(2048.0!)> <Category("通用設定 - 屬性")> <Description("設定實體速度的最高上限。")>
    Public Property MovementSpeed As Single = 2048.0!
    <DisplayName("攻擊傷害上限")> <DefaultValue(2048.0!)> <Category("通用設定 - 屬性")> <Description("設定實體傷害的最高上限。")>
    Public Property AttackDamage As Single = 2048.0!
#End Region
    <DisplayName("啟用BungeeCord 模式")> <DefaultValue(False)> <Category("通用設定 - 一般")> <Description("是否啟用BungeeCord 獨有功能。")>
    Public Property Bungeecord As Boolean = False
    <DisplayName("物品檢查間隔")> <DefaultValue(20)> <Category("通用設定 - 一般")> <Description("控制兩次物品資料變更偵測間的時間(以遊戲刻計算)。" &
                                   vbNewLine & "Minecraft 在每個遊戲刻都會偵測一次物品是否發生變化，這可能會造成效能低下(因為要偵測所有NBT標籤)。" &
                                   vbNewLine & "Spigot 在每刻只會檢查基礎的資料(如數量、損害值、物品類型等)，深層檢查只會依照""物品檢查間隔""所指定的間隔定期執行。")>
    Public Property Item_dirty_ticks As Integer = 20
    <DisplayName("物品檢查間隔")> <DefaultValue(1024)> <Category("通用設定 - 一般")> <Description("這個設定將整數快取的數量限制在""Int_cache_limit""所指定的值內，以抑制整數快取的無限量增長(特別是在世界生成器內)‎。")>
    Public Property Int_cache_limit As Integer = 1024
    <DisplayName("用戶快取大小")> <DefaultValue(1000)> <Category("通用設定 - 一般")> <Description("限制""usercache.json""的大小。")>
    Public Property User_cache_size As Integer = 1000
    <DisplayName("最大延遲時間")> <DefaultValue(60)> <Category("通用設定 - 一般")> <Description("伺服器需要進入""沒有回應""狀態多久(以秒計算)才會自動終止伺服器。")>
    Public Property Timeout_time As Integer = 60
#Region "不支援"
    <DisplayName("崩潰後重啟")> <DefaultValue(False)> <Category("不支援")> <Description("請注意，伺服器管理員不支援此設定及設定後的作用。" &
                                                               vbNewLine & "描述：伺服器是否會在發生崩潰時嘗試‎‎重開伺服器。")>
    Public Property Restart_on_crash As Boolean = False
    <DisplayName("重啟指令位置")> <DefaultValue("")> <Category("不支援")> <Description("請注意，伺服器管理員不支援此設定及設定後的作用。" &
                                                           vbNewLine & "描述：伺服器將在伺服器重啟時執行此路徑上的腳本。")>
    Public Property Restart_script As String = ""
#End Region
    <DisplayName("Netty 執行緒數")> <DefaultValue(4)> <Category("通用設定 - 一般")> <Description("Netty用於操作網路的執行緒數量。")>
    Public Property Netty_threads As Integer = 4
    <DisplayName("延遲連線")> <DefaultValue(False)> <Category("通用設定 - 一般")> <Description("是否推遲玩家登入伺服器直到所有插件載入完畢。")>
    Public Property Late_bind As Boolean = False
    <DisplayName("展示ID數量")> <DefaultValue(12)> <Category("通用設定 - 一般")> <Description("懸停在用戶端的伺服器線上人數時所顯示的玩家ID數量")>
    Public Property Sample_count As Integer = 12
    <DisplayName("玩家列表洗牌間隔")> <DefaultValue(0)> <Category("通用設定 - 一般")> <Description("玩家列表需要間隔多久(以遊戲刻計算)才會隨機排序一次。(0 為停用)")>
    Public Property Player_shuffle As Integer = 0
    <DisplayName("錯誤移動檢查閥值")> <DefaultValue(GetType(Decimal), "0.0625")> <Category("通用設定 - 一般")> <Description("移動錯誤檢查的門檻。")>
    Public Property Moved_wrongly_threshold As Decimal = 0.0625D
    <DisplayName("移動過快檢查閥值")> <DefaultValue(GetType(Decimal), "10")> <Category("通用設定 - 一般")> <Description("移動過快檢查的倍數。")>
    Public Property Moved_too_quickly_multiplier As Decimal = 10D
    <DisplayName("創造模式過濾物品")> <DefaultValue(True)> <Category("通用設定 - 一般")> <Description("是否讓處於創造模式下的玩家不能生成在黑名單內的物品。")>
    Public Property Filter_creative_items As Boolean = True
#End Region
#Region "指令相關"
    <DisplayName("自動完成字數(1.12 以後)")> <DefaultValue(0)> <Category("指令相關")> <Description("需要輸入多少字才能啟動指令的自動完成功能。" &
                                                              vbNewLine & "0 - 不需輸入任何字，即可瀏覽指令清單" &
                                                              vbNewLine & "-1 - 停用自動完成" &
                                                              vbNewLine & "注意：此項目只在1.12以上版本有用。")>
    Public Property Tab_complete As Integer = 0
    <DisplayName("自動完成字數(1.11.2 以前)")> <DefaultValue(0)> <Category("指令相關")> <Description("是否允許玩家按[TAB]鍵自動完成指令。" &
                                                              vbNewLine & "True - 啟用自動完成" &
                                                              vbNewLine & "False - 停用自動完成" &
                                                              vbNewLine & "注意：此項目只在1.11.2以下版本有用。")>
    Public Property Tab_complete_old As Boolean = True
    <DisplayName("還原原版指令實現列表")> <DefaultValue(New String() {"setblock", "summon", "testforblock"})> <Category("指令相關")> <Description("禁用Bukkit對下列指令的實現(使用原版的指令實現)")> <Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property Replace_commands As String() = New String() {"setblock", "summon", "testforblock"}
    <DisplayName("紀錄指令")> <DefaultValue(True)> <Category("指令相關")> <Description("是否紀錄玩家使用的指令。")>
    Public Property Log As Boolean = True
    <DisplayName("垃圾訊息偵測排除指令")> <DefaultValue(New String() {"/skill"})> <Category("指令相關")> <Description("從原版的垃圾訊息偵測機制中排除下列指令。" &
                                                               vbNewLine & "這功能對大多數插件來說沒有必要。")>
    Public Property Spam_exclusions As String() = {"/skill"}
    <DisplayName("紀錄指令方塊的指令")> <DefaultValue(False)> <Category("指令相關")> <Description("‎‎是否禁止指令方塊輸出執行訊息到伺服器主控台。‎")>
    Public Property Silent_commandblock_console As Boolean = False
#End Region
#Region "伺服器訊息"
    <DisplayName("不在白名單的訊息")> <DefaultValue("You are not whitelisted on this server!")> <Category("伺服器訊息")> <Description("‎‎玩家因為不在伺服器白名單內而無法進入時所顯示的訊息。‎")>
    Public Property Whitelist As String = "You are not whitelisted on this server!"
    <DisplayName("輸入錯誤指令的訊息")> <DefaultValue("Unknown command. Type ""help"" for help.")> <Category("伺服器訊息")> <Description("‎‎玩家輸入不存在的指令時所顯示的訊息。‎")>
    Public Property Unknown_command As String = "Unknown command. Type ""help"" for help."
    <DisplayName("伺服器滿人的訊息")> <DefaultValue("The server is full!")> <Category("伺服器訊息")> <Description("‎‎玩家因為伺服器已滿而無法進入時所顯示的訊息。‎")>
    Public Property Server_full As String = "The server is full!"
    <DisplayName("個人端過舊的訊息")> <DefaultValue("Outdated client! Please use {0}")> <Category("伺服器訊息")> <Description("‎‎玩家因為用戶端版本過舊而無法進入時所顯示的訊息。‎")>
    Public Property Outdated_client As String = "Outdated client! Please use {0}"
    <DisplayName("伺服端過舊的訊息")> <DefaultValue("Outdated server! I'm still on {0}")> <Category("伺服器訊息")> <Description("‎‎玩家因為伺服器版本過舊而無法進入時所顯示的訊息。‎")>
    Public Property Outdated_server As String = "Outdated server! I'm still on {0}"
    <DisplayName("伺服端重啟的訊息")> <DefaultValue("Server is restarting")> <Category("伺服器訊息")> <Description("‎‎玩家因為伺服器重啟而被踢出時所顯示的訊息。‎")>
    Public Property Restart As String = "Server is restarting"
#End Region
#Region "統計與進度"
    <DisplayName("停用保存統計資訊及成就(1.11 以下)")> <DefaultValue(False)> <Category("統計與進度")> <Description("‎‎伺服器是否不保存玩家的統計及成就(1.12以前)。‎")>
    Public Property Disable_saving_stats As Boolean = False
    Friend Property Forced_stats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    <DisplayName("停用保存成就(1.12 以上)")> <DefaultValue(False)> <Category("統計與進度")> <Description("‎‎伺服器是否不保存玩家的進度。‎")>
    Public Property Disable_saving_advancements As Boolean = False
    Friend Property Disabled_advancements As List(Of String) = New List(Of String)
#End Region
    <DisplayName("世界設定")> <Editor(GetType(SpigotWorldSettingsCollectionEditor), GetType(UITypeEditor))> <Category("世界設定")> <Description("各世界的單一設定，沒有的話將採用""default""設定。")>
    Public Property World_settings As List(Of SpigotWorldSettings) = New List(Of SpigotWorldSettings)
    Private Sub New()
    End Sub
    Friend Shared Function CreateOptionsWithDefaultSetting(path As String) As SpigotOptions
        Dim op As New SpigotOptions
        op.path = path
        op.World_settings.Add(New SpigotWorldSettings With {.Name = "default"})
        Return op
    End Function
    Friend Shared Function LoadOptions(filepath As String) As SpigotOptions
        Dim spigotOption As New SpigotOptions
        If IO.File.Exists(filepath) Then
            With spigotOption
                Dim reader As New IO.StreamReader(New IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                Dim deserializer = New DeserializerBuilder().Build()
                Dim yamlObject = deserializer.Deserialize(reader)
                deserializer = Nothing
                Dim serializer = New SerializerBuilder().JsonCompatible().Build()
                Dim jsonString = serializer.Serialize(yamlObject)
                serializer = Nothing
                Dim jsonObject = GetDeserialisedObject(jsonString)
                Dim settingsRegion As JObject = GetJsonObject(jsonObject, "settings")
                Dim commandsRegion As JObject = GetJsonObject(jsonObject, "commands")
                Dim messagesRegion As JObject = GetJsonObject(jsonObject, "messages")
                Dim statsRegion As JObject = GetJsonObject(jsonObject, "stats")
                Dim advancementsRegion As JObject = GetJsonObject(jsonObject, "advancements")
                Dim worldSettingsRegion As JObject = GetJsonObject(jsonObject, "world-settings")
                InputPropertyValue(jsonObject, "config-version", .Config_version)
#Region "Settings"
                InputPropertyValue(settingsRegion, "debug", .Debug)
                InputPropertyValue(settingsRegion, "save-user-cache-on-stop-only", .Save_user_cache_on_stop_only)
                Dim attributeRegion As JObject = GetJsonObject(settingsRegion, "attribute")
                InputPropertyValue(GetJsonObject(attributeRegion, "maxHealth"), "max", .MaxHealth)
                InputPropertyValue(GetJsonObject(attributeRegion, "movementSpeed"), "max", .MovementSpeed)
                InputPropertyValue(GetJsonObject(attributeRegion, "attackDamage"), "max", .AttackDamage)
                attributeRegion = Nothing
                InputPropertyValue(settingsRegion, "bungeecord", .Bungeecord)
                InputPropertyValue(settingsRegion, "item-dirty-ticks", .Item_dirty_ticks)
                InputPropertyValue(settingsRegion, "int-cache-limit", .Int_cache_limit)
                InputPropertyValue(settingsRegion, "user-cache-size", .User_cache_size)
                InputPropertyValue(settingsRegion, "timeout-time", .Timeout_time)
                InputPropertyValue(settingsRegion, "restart-on-crash", .Restart_on_crash)
                InputPropertyValue(settingsRegion, "restart-script", .Restart_script)
                InputPropertyValue(settingsRegion, "netty-threads", .Netty_threads)
                InputPropertyValue(settingsRegion, "late-bind", .Late_bind)
                InputPropertyValue(settingsRegion, "sample-count", .Sample_count)
                InputPropertyValue(settingsRegion, "player-shuffle", .Player_shuffle)
                InputPropertyValue(settingsRegion, "moved-wrongly-threshold", .Moved_wrongly_threshold)
                InputPropertyValue(settingsRegion, "moved-too-quickly-multiplier", .Moved_too_quickly_multiplier)
                InputPropertyValue(settingsRegion, "filter-creative-items", .Filter_creative_items)
                settingsRegion = Nothing
#End Region
#Region "Commands"
                InputPropertyValue(commandsRegion, "tab-complete", .Tab_complete)
                InputPropertyValue(commandsRegion, "tab-complete", .Tab_complete_old)
                InputPropertyValue(commandsRegion, "replace-commands", .Replace_commands)
                InputPropertyValue(commandsRegion, "log", .Log)
                InputPropertyValue(commandsRegion, "spam-exclusions", .Spam_exclusions)
                InputPropertyValue(commandsRegion, "silent-commandblock-console", .Silent_commandblock_console)
                commandsRegion = Nothing
#End Region
#Region "Messages"
                InputPropertyValue(messagesRegion, "whitelist", .Whitelist)
                InputPropertyValue(messagesRegion, "unknown-command", .Unknown_command)
                InputPropertyValue(messagesRegion, "server-full", .Server_full)
                InputPropertyValue(messagesRegion, "outdated-client", .Outdated_client)
                InputPropertyValue(messagesRegion, "outdated-server", .Outdated_server)
                InputPropertyValue(messagesRegion, "restart", .Restart)
                messagesRegion = Nothing
#End Region
#Region "Stats"
                InputPropertyValue(statsRegion, "disable-saving", .Disable_saving_stats)
                For Each jsonPropertyPair In GetJsonObject(statsRegion, "forced-stats")
                    .Forced_stats.Add(jsonPropertyPair.Key, jsonPropertyPair.Value)
                Next
                statsRegion = Nothing
#End Region
#Region "Advancements"
                If advancementsRegion IsNot Nothing Then
                    InputPropertyValue(advancementsRegion, "disable-saving", .Disable_saving_advancements)
                    InputPropertyValue(advancementsRegion, "disabled", .Disabled_advancements)
                    advancementsRegion = Nothing
                End If
#End Region
#Region "World Settings"
                For Each jsonPropertyPair In worldSettingsRegion
                    Dim settings As New SpigotWorldSettings
                    settings.Name = jsonPropertyPair.Key
                    Dim settingsObject As JObject = jsonPropertyPair.Value
                    With settings
                        InputPropertyValue(settingsObject, "verbose", .Verbose)
                        InputPropertyValue(settingsObject, "item-despawn-rate", .Item_despawn_rate)
                        InputPropertyValue(GetJsonObject(settingsObject, "merge-radius"), "exp", .Verbose)
                        InputPropertyValue(GetJsonObject(settingsObject, "merge-radius"), "item", .Verbose)
                        InputPropertyValue(settingsObject, "arrow-despawn-rate", .Arrow_despawn_rate)
                        InputPropertyValue(settingsObject, "view-distance", .View_distance)
                        InputPropertyValue(settingsObject, "enable-zombie-pigmen-portal-spawns", .Enable_zombie_pigmen_portal_spawns)
                        InputPropertyValue(settingsObject, "wither-spawn-sound-radius", .Wither_spawn_sound_radius)
                        InputPropertyValue(settingsObject, "hanging-tick-frequency", .Hanging_tick_frequency)
                        InputPropertyValue(settingsObject, "mob-spawn-range", .Mob_spawn_range)
                        InputPropertyValue(settingsObject, "random-light-updates", .Random_light_updates)
#Region "Hunger"
                        Dim hungerRegion = GetJsonObject(settingsObject, "hunger")
                        InputPropertyValue(hungerRegion, "jump-walk-exhaustion", .Hunger_jump_walk_exhaustion)
                        InputPropertyValue(hungerRegion, "jump-sprint-exhaustion", .Hunger_jump_sprint_exhaustion)
                        InputPropertyValue(hungerRegion, "combat-exhaustion", .Hunger_combat_exhaustion)
                        InputPropertyValue(hungerRegion, "regen-exhaustion", .Hunger_regen_exhaustion)
                        InputPropertyValue(hungerRegion, "swim-multiplier", .Hunger_swim_multiplier)
                        InputPropertyValue(hungerRegion, "sprint-multiplier", .Hunger_sprint_multiplier)
                        InputPropertyValue(hungerRegion, "other-multiplier", .Hunger_other_multiplier)
                        hungerRegion = Nothing
#End Region
#Region "Growth"
                        Dim growthRegion = GetJsonObject(settingsObject, "growth")
                        InputPropertyValue(growthRegion, "cactus-modifier", .Growth_cactus_modifier)
                        InputPropertyValue(growthRegion, "cane-modifier", .Growth_cane_modifier)
                        InputPropertyValue(growthRegion, "melon-modifier", .Growth_melon_modifier)
                        InputPropertyValue(growthRegion, "mushroom-modifier", .Growth_mushroom_modifier)
                        InputPropertyValue(growthRegion, "pumpkin-modifier", .Growth_pumpkin_modifier)
                        InputPropertyValue(growthRegion, "sapling-modifier", .Growth_sapling_modifier)
                        InputPropertyValue(growthRegion, "wheat-modifier", .Growth_wheat_modifier)
                        InputPropertyValue(growthRegion, "netherwart-modifier", .Growth_netherwart_modifier)
                        InputPropertyValue(growthRegion, "vine-modifier", .Growth_vine_modifier)
                        InputPropertyValue(growthRegion, "cocoa-modifier", .Growth_cocoa_modifier)
                        growthRegion = Nothing
#End Region
                        InputPropertyValue(GetJsonObject(settingsObject, "squid-spawn-range"), "min", .Squid_spawn_range_min)
                        InputPropertyValue(growthRegion, "max-tnt-per-tick", .Max_tnt_per_tick)
#Region "Tracking Range"
                        Dim trackingRegion = GetJsonObject(settingsObject, "entity-tracking-range")
                        InputPropertyValue(growthRegion, "players", .Players_tracking_range)
                        InputPropertyValue(growthRegion, "animals", .Animals_tracking_range)
                        InputPropertyValue(growthRegion, "monsters", .Monsters_tracking_range)
                        InputPropertyValue(growthRegion, "misc", .Misc_tracking_range)
                        InputPropertyValue(growthRegion, "other", .Other_tracking_range)
                        trackingRegion = Nothing
#End Region
                        InputPropertyValue(GetJsonObject(settingsObject, "ticks-per"), "hopper-tranfer", .Ticks_per_hopper_transfer)
                        InputPropertyValue(GetJsonObject(settingsObject, "ticks-per"), "hopper-check", .Ticks_per_hopper_check)
                        InputPropertyValue(growthRegion, "hopper-amount", .Hopper_amount)
                        .EditSaveStructureInfo(growthRegion, "save-structure-info")
                        InputPropertyValue(GetJsonObject(settingsObject, "max-tick-time"), "tile", .Tile_max_tick_time)
                        InputPropertyValue(GetJsonObject(settingsObject, "max-tick-time"), "entity", .Entity_max_tick_time)
#Region "Activation Range"
                        Dim aRangeRegion = GetJsonObject(settingsObject, "entity-activation-range")
                        InputPropertyValue(growthRegion, "animals", .Animals_activation_range)
                        InputPropertyValue(growthRegion, "monsters", .Monsters_activation_range)
                        InputPropertyValue(growthRegion, "misc", .Misc_activation_range)
                        InputPropertyValue(growthRegion, "tick-inactive-villagers", .Tick_inactive_villagers)
                        aRangeRegion = Nothing
#End Region
                        InputPropertyValue(growthRegion, "nerf-spawner-mobs", .Nerf_spawner_mobs)
                        InputPropertyValue(growthRegion, "zombie-aggressive-towards-villager", .Zombie_aggressive_towards_villager)
                        InputPropertyValue(growthRegion, "dragon-death-sound-radius", .Dragon_death_sound_radius)
                        InputPropertyValue(growthRegion, "seed-village", .seed_village)
                        InputPropertyValue(growthRegion, "seed-feature", .seed_feature)
                        InputPropertyValue(growthRegion, "seed-monument", .seed_monument)
                        InputPropertyValue(growthRegion, "seed-slime", .seed_slime)
                    End With
                    .World_settings.Add(settings)
                Next

#End Region
                .path = filepath
                reader.Close()
                GC.Collect()
            End With
            Return spigotOption
        Else
            CreateOptionsWithDefaultSetting(filepath)
        End If
    End Function
    Friend Sub SaveOption(Optional isBefore1120 As Boolean = False)
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
        SetPropertyValue(jsonObject, "config-version", Config_version)
#Region "Settings"
        Dim configsRegion = GetJsonObject(jsonObject, "settings")
        SetPropertyValue(jsonObject, "debug", Debug)
        SetPropertyValue(jsonObject, "save-user-cache-on-stop-only", Save_user_cache_on_stop_only)
        Dim attributeRegion = GetJsonObject(configsRegion, "attribute")
        SetPropertyValue(GetJsonObject(attributeRegion, "maxHealth"), "max", MaxHealth)
        SetPropertyValue(GetJsonObject(attributeRegion, "movementSpeed"), "max", MovementSpeed)
        SetPropertyValue(GetJsonObject(attributeRegion, "attackDamage"), "max", AttackDamage)
        SetPropertyValue(configsRegion, "attribute", attributeRegion)
        SetPropertyValue(configsRegion, "bungeecord", Bungeecord)
        SetPropertyValue(configsRegion, "item-dirty-ticks", Item_dirty_ticks)
        SetPropertyValue(configsRegion, "int-cache-limit", Int_cache_limit)
        SetPropertyValue(configsRegion, "user-cache-size", User_cache_size)
        SetPropertyValue(configsRegion, "timeout-time", Timeout_time)
        SetPropertyValue(configsRegion, "restart-on-crash", Restart_on_crash)
        SetPropertyValue(configsRegion, "restart-script", Restart_script)
        SetPropertyValue(configsRegion, "netty-threads", Netty_threads)
        SetPropertyValue(configsRegion, "late-bind", Late_bind)
        SetPropertyValue(configsRegion, "sample-count", Sample_count)
        SetPropertyValue(configsRegion, "player-shuffle", Player_shuffle)
        SetPropertyValue(configsRegion, "moved-wrongly-threshold", Moved_wrongly_threshold)
        SetPropertyValue(configsRegion, "moved-too-quickly-multiplier", Moved_too_quickly_multiplier)
        SetPropertyValue(configsRegion, "filter-creative-items", Filter_creative_items)
        SetPropertyValue(jsonObject, "settings", configsRegion)
#End Region
#Region "Commands"
        Dim commandsRegion = GetJsonObject(jsonObject, "commands")
        If isBefore1120 Then
            SetPropertyValue(commandsRegion, "tab-complete", Tab_complete_old)
        Else
            SetPropertyValue(commandsRegion, "tab-complete", Tab_complete)
        End If
        SetPropertyValue(commandsRegion, "replace-commands", New JArray(Replace_commands))
        SetPropertyValue(commandsRegion, "log", Log)
        SetPropertyValue(commandsRegion, "spam-exclusions", New JArray(Spam_exclusions))
        SetPropertyValue(commandsRegion, "silent-commandblock-console", Silent_commandblock_console)
        SetPropertyValue(jsonObject, "commands", commandsRegion)
#End Region
#Region "Messages"
        Dim messagesRegion = GetJsonObject(jsonObject, "messages")
        SetPropertyValue(messagesRegion, "whitelist", Whitelist)
        SetPropertyValue(messagesRegion, "unknown-command", Unknown_command)
        SetPropertyValue(messagesRegion, "server-full", Server_full)
        SetPropertyValue(messagesRegion, "outdated-client", Outdated_client)
        SetPropertyValue(messagesRegion, "outdated-server", Outdated_server)
        SetPropertyValue(messagesRegion, "restart", Restart)
        SetPropertyValue(jsonObject, "messages", messagesRegion)
#End Region
#Region "Stats"
        Dim statsRegion = GetJsonObject(jsonObject, "stats")
        SetPropertyValue(statsRegion, "disable-saving", Disable_saving_stats)
        Dim forcedStatsRegion As New JObject
        For Each pair In Forced_stats
            forcedStatsRegion.Add(pair.Key, pair.Value)
        Next
        SetPropertyValue(statsRegion, "forced-stats", forcedStatsRegion)
        SetPropertyValue(jsonObject, "stats", statsRegion)
#End Region
#Region "Advancements (1.12 up)"
        Dim advancementsRegion = GetJsonObject(jsonObject, "advancements")
        SetPropertyValue(advancementsRegion, "disable-saving", Disable_saving_advancements)
        SetPropertyValue(advancementsRegion, "disabled", New JArray(Disabled_advancements))
        SetPropertyValue(jsonObject, "advancements", advancementsRegion)
#End Region
#Region "World Settings"
        Dim worldSettingsRegion = GetJsonObject(jsonObject, "world-settings")
        For Each worldSetting In World_settings
            Dim region As New JObject
            With region
                .Add("verbose", worldSetting.Verbose)
                .Add("item-despawn-rate", worldSetting.Item_despawn_rate)
                Dim mergeRadiusRegion As New JObject
                mergeRadiusRegion.Add("item", worldSetting.Merge_radius_item)
                mergeRadiusRegion.Add("exp", worldSetting.Merge_radius_exp)
                .Add("merge-radius", mergeRadiusRegion)
                .Add("arrow-despawn-rate", worldSetting.Arrow_despawn_rate)
                .Add("view-distance", worldSetting.View_distance)
                .Add("enable-zombie-pigmen-portal-spawns", worldSetting.Enable_zombie_pigmen_portal_spawns)
                .Add("wither-spawn-sound-radius", worldSetting.Wither_spawn_sound_radius)
                .Add("hanging-tick-frequency", worldSetting.Hanging_tick_frequency)
                .Add("mob-spawn-range", worldSetting.Mob_spawn_range)
                .Add("random-light-updates", worldSetting.Random_light_updates)
                Dim hungerRegion As New JObject
                With hungerRegion
                    .Add("jump-walk-exhaustion", worldSetting.Hunger_jump_walk_exhaustion)
                    .Add("jump-sprint-exhaustion", worldSetting.Hunger_jump_sprint_exhaustion)
                    .Add("combat-exhaustion", worldSetting.Hunger_combat_exhaustion)
                    .Add("regen-exhaustion", worldSetting.Hunger_regen_exhaustion)
                    .Add("swim-multiplier", worldSetting.Hunger_swim_multiplier)
                    .Add("sprint-multiplier", worldSetting.Hunger_sprint_multiplier)
                    .Add("other-multiplier", worldSetting.Hunger_other_multiplier)
                End With
                .Add("hunger", hungerRegion)
                Dim growthRegion As New JObject
                With growthRegion
                    .Add("cactus-modifier", worldSetting.Growth_cactus_modifier)
                    .Add("cane-modifier", worldSetting.Growth_cane_modifier)
                    .Add("melon-modifier", worldSetting.Growth_melon_modifier)
                    .Add("mushroom-modifier", worldSetting.Growth_mushroom_modifier)
                    .Add("pumpkin-modifier", worldSetting.Growth_pumpkin_modifier)
                    .Add("sapling-modifier", worldSetting.Growth_sapling_modifier)
                    .Add("wheat-modifier", worldSetting.Growth_wheat_modifier)
                    .Add("netherwart-modifier", worldSetting.Growth_netherwart_modifier)
                    .Add("vine-modifier", worldSetting.Growth_vine_modifier)
                    .Add("cocoa-modifier", worldSetting.Growth_cocoa_modifier)
                End With
                .Add("growth", growthRegion)
                Dim squidRegion As New JObject
                squidRegion.Add("min", worldSetting.Squid_spawn_range_min)
                .Add("squid-spawn-range", squidRegion)
                .Add("max-tnt-per-tick", worldSetting.Max_tnt_per_tick)
                Dim entityTrackingRegion As New JObject
                With entityTrackingRegion
                    .Add("players", worldSetting.Players_tracking_range)
                    .Add("animals", worldSetting.Animals_tracking_range)
                    .Add("monsters", worldSetting.Monsters_tracking_range)
                    .Add("misc", worldSetting.Misc_tracking_range)
                    .Add("other", worldSetting.Other_tracking_range)
                End With
                .Add("entity-tracking-range", entityTrackingRegion)
                Dim ticksPerRegion As New JObject
                ticksPerRegion.Add("hopper-transfer", worldSetting.Ticks_per_hopper_transfer)
                ticksPerRegion.Add("hopper-check", worldSetting.Ticks_per_hopper_check)
                .Add("ticks-per", ticksPerRegion)
                .Add("hopper-amount", worldSetting.Hopper_amount)
                .Add("save-structure-info", worldSetting.Save_structure_info)
                Dim maxTickTimeRegion As New JObject
                maxTickTimeRegion.Add("tile", worldSetting.Tile_max_tick_time)
                maxTickTimeRegion.Add("entity", worldSetting.Entity_max_tick_time)
                .Add("max-tick-time", maxTickTimeRegion)
                Dim entityActivationRegion As New JObject
                With entityActivationRegion
                    .Add("animals", worldSetting.Animals_activation_range)
                    .Add("monsters", worldSetting.Monsters_activation_range)
                    .Add("misc", worldSetting.Misc_activation_range)
                    .Add("tick-inactive-villagers", worldSetting.Tick_inactive_villagers)
                End With
                .Add("entity-activation-range", entityActivationRegion)
                .Add("nerf-spawner-mobs", worldSetting.Nerf_spawner_mobs)
                .Add("zombie-aggressive-towards-villager", worldSetting.Zombie_aggressive_towards_villager)
                .Add("dragon-death-sound-radius", worldSetting.Dragon_death_sound_radius)
                .Add("seed-village", worldSetting.seed_village)
                .Add("seed-feature", worldSetting.seed_feature)
                .Add("seed-monument", worldSetting.seed_monument)
                .Add("seed-slime", worldSetting.seed_slime)
            End With
            SetPropertyValue(worldSettingsRegion, worldSetting.Name, region)
        Next
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
End Class
#Region "Spigot YAML Object"
''' <summary>
''' Spigot 世界設定
''' </summary>
Public Class SpigotWorldSettings
    <DisplayName("(名稱)")> <Category("一般")> <Description("此屬性必須設定成要設定的世界名稱或default(作為預設值)且不可重複。")>
    Public Property Name As String = ""
    <DisplayName("紀錄詳細配置")> <DefaultValue(True)> <Category("一般")> <Description("是否在伺服器啟動時紀錄每個世界的詳細報告和配置。")>
    Public Property Verbose As Boolean = True
    <DisplayName("物品消失的時間")> <DefaultValue(6000)> <Category("一般")> <Description("在地面上的物品消失之前所需的時間(以遊戲刻計算)。")>
    Public Property Item_despawn_rate As Integer = 6000
    <DisplayName("經驗球合併距離")> <DefaultValue(3.0!)> <Category("一般")> <Description("‎將在地面上的經驗球合併成一個的範圍(以方塊計算)。")>
    Public Property Merge_radius_exp As Single = 3.0!
    <DisplayName("同類物品合併距離")> <DefaultValue(2.5!)> <Category("一般")> <Description("將在地面上的相同物品堆積成單一實體的範圍(以方塊計算)。")>
    Public Property Merge_radius_item As Single = 2.5!
    <DisplayName("箭矢消失的時間")> <DefaultValue(1200)> <Category("一般")> <Description("‎在靜止狀態的箭矢消失之前所需的時間(以遊戲刻計算)。")>
    Public Property Arrow_despawn_rate As Integer = 1200
    Dim _View_distance As Integer = 10
    <DisplayName("視野距離")> <DefaultValue(10)> <Category("視野")> <Description("玩家周圍載入的區塊數量(只能在1~15之間)。")>
    Public Property View_distance As Integer
        Get
            Return _View_distance
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 15 Then
                _View_distance = value
            Else
            End If
        End Set
    End Property
    <DisplayName("地獄傳送門生成殭屍豬人")> <DefaultValue(True)> <Category("一般")> <Description("‎是否允許殭屍豬人在地獄傳送門上生成。")>
    Public Property Enable_zombie_pigmen_portal_spawns As Boolean = True
    <DisplayName("凋零生成時的聲音範圍")> <DefaultValue(0)> <Category("一般")> <Description("‎凋靈怪生成時所發出的聲音的傳播範圍。")>
    Public Property Wither_spawn_sound_radius As Integer = 0
    <DisplayName("懸掛型實體刷新時間")> <DefaultValue(100)> <Category("一般")> <Description("‎懸掛型實體(如畫、物品展示框、栓繩結等)的狀態更新間隔(以遊戲刻計算)。")>
    Public Property Hanging_tick_frequency As Integer = 100
    <DisplayName("生物生成範圍")> <DefaultValue(4)> <Category("一般")> <Description("‎生物可以在玩家附近多少區塊內生成。")>
    Public Property Mob_spawn_range As Integer = 4
    <DisplayName("隨機光照更新")> <DefaultValue(False)> <Category("視野")> <Description("伺服器是否會隨機採樣區塊進行檢測，以便驗證及修復照明問題。")>
    Public Property Random_light_updates As Boolean = False
    <DisplayName("跳躍的飢餓消耗速率")> <DefaultValue(0.05#)> <Category("飢餓等級")> <Description("每次跳躍增加多少飢餓等級。")>
    Public Property Hunger_jump_walk_exhaustion As Double = 0.05#
    <DisplayName("疾跑跳躍的飢餓消耗速率")> <DefaultValue(0.2#)> <Category("飢餓等級")> <Description("疾跑時，每次跳躍增加多少飢餓等級。")>
    Public Property Hunger_jump_sprint_exhaustion As Double = 0.2#
    <DisplayName("攻擊的飢餓消耗速率")> <DefaultValue(0.1#)> <Category("飢餓等級")> <Description("每次攻擊增加多少飢餓等級。")>
    Public Property Hunger_combat_exhaustion As Double = 0.1#
    <DisplayName("自然回復的飢餓消耗速率")> <DefaultValue(6.0#)> <Category("飢餓等級")> <Description("自然回復下，每滴血增加多少飢餓等級。")>
    Public Property Hunger_regen_exhaustion As Double = 6.0#
    <DisplayName("游泳的飢餓消耗速率")> <DefaultValue(0.01#)> <Category("飢餓等級")> <Description("游泳時，每公尺增加多少飢餓等級。")>
    Public Property Hunger_swim_multiplier As Double = 0.01#
    <DisplayName("疾跑的飢餓消耗速率")> <DefaultValue(0.1#)> <Category("飢餓等級")> <Description("疾跑時，每公尺增加多少飢餓等級。")>
    Public Property Hunger_sprint_multiplier As Double = 0.1#
    <DisplayName("其他情況的飢餓消耗速率")> <DefaultValue(0.0#)> <Category("飢餓等級")> <Description("其他情況下，每單位增加多少飢餓等級。")>
    Public Property Hunger_other_multiplier As Double = 0.0#
    <DisplayName("仙人掌的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制仙人掌的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_cactus_modifier As Integer = 100
    <DisplayName("甘蔗的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制甘蔗的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_cane_modifier As Integer = 100
    <DisplayName("西瓜的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制西瓜的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_melon_modifier As Integer = 100
    <DisplayName("蘑菇的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制蘑菇的傳播速度(區塊/遊戲刻)。")>
    Public Property Growth_mushroom_modifier As Integer = 100
    <DisplayName("南瓜的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制南瓜的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_pumpkin_modifier As Integer = 100
    <DisplayName("樹苗的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制樹苗的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_sapling_modifier As Integer = 100
    <DisplayName("小麥的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制小麥的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_wheat_modifier As Integer = 100
    <DisplayName("地獄疙瘩的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制地獄疙瘩的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_netherwart_modifier As Integer = 100
    <DisplayName("藤蔓的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制藤蔓的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_vine_modifier As Integer = 100
    <DisplayName("可可豆的生長速度")> <DefaultValue(100)> <Category("生長速度")> <Description("控制可可豆的生長速度(區塊/遊戲刻)。")>
    Public Property Growth_cocoa_modifier As Integer = 100
    <DisplayName("烏賊最小生成範圍")> <DefaultValue(45.0!)> <Category("一般")> <Description("烏賊生成範圍的最小值。")>
    Public Property Squid_spawn_range_min As Single = 45.0!
    <DisplayName("每刻能計算的TNT爆炸量")> <DefaultValue(100)> <Category("一般")> <Description("每遊戲刻能計算多少TNT的爆炸。")>
    Public Property Max_tnt_per_tick As Integer = 100
    <DisplayName("玩家追蹤範圍")> <DefaultValue(48)> <Category("視野")> <Description("其他玩家於視野中可見或以其他方式追蹤到玩家的區塊範圍。")>
    Public Property Players_tracking_range As Integer = 48
    <DisplayName("動物追蹤範圍")> <DefaultValue(48)> <Category("視野")> <Description("動物於視野忠可見或以其他方式追蹤到動物的區塊範圍。")>
    Public Property Animals_tracking_range As Integer = 48
    <DisplayName("怪物追蹤範圍")> <DefaultValue(48)> <Category("視野")> <Description("怪物於視野中可見或以其他方式追蹤到怪物的區塊範圍。")>
    Public Property Monsters_tracking_range As Integer = 48
    <DisplayName("雜項追蹤範圍")> <DefaultValue(32)> <Category("視野")> <Description("雜項實體(如物品展示框、畫、掉落物、經驗球等)於視野中可見或以其他方式追蹤到雜項實體的區塊範圍。")>
    Public Property Misc_tracking_range As Integer = 32
    <DisplayName("其他的追蹤範圍")> <DefaultValue(64)> <Category("視野")> <Description("實體於視野中可見或以其他方式追蹤到實體的區塊範圍。")>
    Public Property Other_tracking_range As Integer = 64
    <DisplayName("漏斗傳輸物品的間隔")> <DefaultValue(8)> <Category("效能")> <Description("兩次漏斗送出/抽取物品之間的時間(以遊戲刻計算)。")>
    Public Property Ticks_per_hopper_transfer As Integer = 8
    <DisplayName("漏斗嘗試收發物品的間隔")> <DefaultValue(1)> <Category("效能")> <Description("漏斗自上次嘗試以來嘗試送出/抽取物品的時間(以遊戲刻計算)。")>
    Public Property Ticks_per_hopper_check As Integer = 1
    <DisplayName("漏斗最大傳輸物品數量")> <DefaultValue(1)> <Category("一般")> <Description("每次漏斗送出/抽取物品時的最大數量。")>
    Public Property Hopper_amount As Integer = 1
    <DisplayName("使用1.6結構儲存機制")> <DefaultValue(True)> <Category("一般")> <Description("是否使用1.6.3引進的結構儲存機制。" &
                                                       vbNewLine & "伺服器管理員禁止使用者編輯此設定。" &
                                                       vbNewLine & "因為此設定如果關閉的話會讓一些結構可能發生異常，")>
    Public ReadOnly Property Save_structure_info As Boolean = True
    <DisplayName("""Tile""實體最大操作耗時")> <DefaultValue(50)> <Category("效能")> <Description("在伺服器跳到下一個行程之前，方塊實體的操作可以消耗的時間（以毫秒計算）。")>
    Public Property Tile_max_tick_time As Integer = 50
    <DisplayName("掉落物最大操作耗時")> <DefaultValue(50)> <Category("效能")> <Description("在伺服器跳到下一個行程之前，掉落物的操作可以消耗的時間（以毫秒計算）。")>
    Public Property Entity_max_tick_time As Integer = 50
    <DisplayName("動物激活AI的範圍")> <DefaultValue(32)> <Category("效能")> <Description("控制動物被""啟動AI""的範圍(以方塊數計算) - " &
                                                     vbNewLine & "超出此範圍的動物將在降低的速率下運行以防止伺服器lag。")>
    Public Property Animals_activation_range As Integer = 32
    <DisplayName("怪物激活AI的範圍")> <DefaultValue(32)> <Category("效能")> <Description("控制怪物被""啟動AI""的範圍(以方塊數計算) - " &
                                                 vbNewLine & "超出此範圍的怪物將在降低的速率下運行以防止伺服器lag。")>
    Public Property Monsters_activation_range As Integer = 32
    <DisplayName("其他實體激活功能的範圍")> <DefaultValue(16)> <Category("效能")> <Description("控制雜項實體(如物品展示框、畫、掉落物、經驗球等)被""啟動偵測""的範圍(以方塊數計算) - " &
                                             vbNewLine & "超出此範圍的雜項實體將在降低的速率下運行以防止伺服器lag。")>
    Public Property Misc_activation_range As Integer = 16
    <DisplayName("降速運行非互動中村民的AI")> <DefaultValue(True)> <Category("效能")> <Description("是否不要將在待機的村民以低速率運行AI。")>
    Public Property Tick_inactive_villagers As Boolean = True
    <DisplayName("生怪磚怪物無AI")> <DefaultValue(False)> <Category("一般")> <Description("是否讓生怪磚生成的怪物沒有AI。")>
    Public Property Nerf_spawner_mobs As Boolean = False
    <DisplayName("殭屍追殺村民")> <DefaultValue(True)> <Category("一般")> <Description("是否讓殭屍追殺村民。")>
    Public Property Zombie_aggressive_towards_villager As Boolean = True
    <DisplayName("終界龍死亡時的聲音範圍")> <DefaultValue(0)> <Category("一般")> <Description("‎終界龍死亡時所發出的聲音的傳播範圍。")>
    Public Property Dragon_death_sound_radius As Integer = 0
    Friend Property seed_village As Integer = 10387312
    Friend Property seed_feature As Integer = 14357617
    Friend Property seed_monument As Integer = 10387313
    Friend Property seed_slime As Integer = 987234911
    Friend Sub EditSaveStructureInfo(jsonObject As JObject, key As String)
        Try
            _Save_structure_info = jsonObject.GetValue(key)
        Catch ex As Exception

        End Try
    End Sub
End Class
#End Region
Public Class SpigotWorldSettingsCollectionEditor
    Inherits CollectionEditor
    Dim itemCount As Integer = 1
    Public Sub New()
        MyBase.New(type:=GetType(List(Of SpigotWorldSettings)))
    End Sub
    Protected Overrides Function CreateCollectionForm() As CollectionForm
        Dim form = MyBase.CreateCollectionForm()
        form.Text = "Spigot 世界設定編輯器"
        AddHandler form.Shown, Sub()
                                   ShowDescription(form)
                               End Sub
        Return form
    End Function
    Protected Overrides Function CreateInstance(ByVal itemType As Type) As Object
        Dim settings = New SpigotWorldSettings
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
        Dim settings As SpigotWorldSettings = value
        Return settings.Name.ToLower <> "default"
    End Function
    Protected Overrides Function GetDisplayText(value As Object) As String
        Dim settings As SpigotWorldSettings = value
        Return settings.Name
    End Function
End Class
