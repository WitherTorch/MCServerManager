Imports System.ComponentModel
''' <summary>
''' pocketmine.yml 的對應.NET 類別
''' </summary>
Public Class PocketMineOptions
    Dim path As String
#Region "Setting"
    <DisplayName("是否強制使用語言")> <DefaultValue(False)> <Category("一般")> <Description("伺服器是否強制使用特定語言介面")>
    Public Property Force_Language As Boolean = False
    <DisplayName("伺服器關閉訊息")> <DefaultValue("伺服器已關閉")> <Category("一般")> <Description("伺服器關閉訊息")>
    Public Property Shutdown_Message As String = "伺服器已關閉"
    <DisplayName("遠端查詢插件")> <DefaultValue(True)> <Category("一般")> <Description("允許使用Query協定查詢您的插件")>
    Public Property Query_Plugins As Boolean = True
    <DisplayName("紀錄使用棄用API")> <DefaultValue(True)> <Category("一般")> <Description("當某插件使用已棄用的API時，在後台提醒")>
    Public Property Deprecated_Verbose As Boolean = True
    <DisplayName("啟用插件和核心分析")> <DefaultValue(True)> <Category("一般")> <Description("是否啟用插件和核心分析")>
    Public Property Enable_Profiling As Boolean = True
    <DisplayName("低效能檢測")> <DefaultValue(20)> <Category("一般")> <Description("在伺服器運行時的每秒遊戲刻數低於指定的值時紀錄在伺服器訊息中")>
    Public Property Profile_Report_Trigger As Integer = 20
    <DisplayName("非同步執行緒數量")> <DefaultValue(0)> <Category("一般")> <Description("非同步執行緒數量，" &
                               vbNewLine & "0 - 自動識別CPU核心數量（最少4個執行緒）")>
    Public Property Async_Workers As Integer = 0
    <DisplayName("是否允許使用開發版本")> <DefaultValue(False)> <Category("一般")> <Description("是否允許使用開發版本" &
                                                                                vbNewLine & "注意：使用開發版本有可能損毀插件、世界或整個遊戲")>
    Public Property Enable_Dev_Builds As Boolean = False
#End Region
#Region "Memory"
    <DisplayName("總記憶體限制")> <DefaultValue(0)> <Category("記憶體")> <Description("伺服器的記憶體限制（單位：MB），達限制時將會啟動垃圾回收器回收所有多餘的記憶體" &
                               vbNewLine & "0 - 不限制")>
    Public Property Global_Limit As Integer = 0
    <DisplayName("主執行緒記憶體限制(軟性)")> <DefaultValue(0)> <Category("記憶體")> <Description("伺服器主執行緒的記憶體限制（單位：MB），達限制時將會啟動垃圾回收器回收所有多餘的記憶體" &
                               vbNewLine & "0 - 不限制")>
    Public Property Main_Limit As Integer = 0
    <DisplayName("主執行緒記憶體限制(硬性)")> <DefaultValue(1024)> <Category("記憶體")> <Description("伺服器主執行緒的記憶體限制（單位：MB），達限制時將會關閉伺服器" &
                           vbNewLine & "0 - 不限制")>
    Public Property Main_Hard_Limit As Integer = 1024
    <DisplayName("非同步執行緒記憶體限制(硬性)")> <DefaultValue(256)> <Category("記憶體")> <Description("伺服器非同步執行緒的記憶體限制（單位：MB），達限制時將會關閉超出的執行緒上的工作" &
                       vbNewLine & "0 - 不限制")>
    Public Property Async_Worker_Hard_Limit As Integer = 256
    <DisplayName("記憶體檢查頻率")> <DefaultValue(20)> <Category("記憶體")> <Description("決定要多少遊戲刻檢查一次記憶體")>
    Public Property Check_Rate As Integer = 20
    <DisplayName("達記憶體限制時是否持續回收記憶體")> <DefaultValue(True)> <Category("記憶體")> <Description("在達記憶體限制時是否持續回收記憶體直到低於限制")>
    Public Property Continuous_Trigger As Boolean = True
    <DisplayName("達記憶體限制時持續回收記憶體速率")> <DefaultValue(30)> <Category("記憶體")> <Description("決定要間隔多少次檢查記憶體排程後回收一次記憶體")>
    Public Property Continuous_Trigger_Rate As Integer = 30
#Region "Garbage Collection (GC)"
    <DisplayName("正常回收記憶體的速率")> <DefaultValue(36000)> <Category("記憶體")> <Description("決定在正常情況下要多少遊戲刻回收一次記憶體")>
    Public Property Garbage_Collection_Period As Integer = 36000
    <DisplayName("回收非同步執行緒的棄用記憶體")> <DefaultValue(True)> <Category("記憶體")> <Description("是否要在回收時回收非同步執行緒的棄用記憶體")>
    Public Property GC_Collect_Async_Worker As Boolean = True
    <DisplayName("啟用達記憶體限制時的回收機制")> <DefaultValue(True)> <Category("記憶體")> <Description("是否要啟用達記憶體限制時的回收機制")>
    Public Property GC_Low_Memory_Trigger As Boolean = True
#End Region
    <DisplayName("轉儲執行緒記憶體")> <DefaultValue(True)> <Category("記憶體")> <Description("是否從非同步執行緒及主執行緒轉儲內存")>
    Public Property Dump_Async_Worker As Boolean = True
    <DisplayName("達記憶體限制時的視野距離")> <DefaultValue(3)> <Category("記憶體")> <Description("決定在達記憶體限制時玩家的視野距離")>
    Public Property Max_Chunk_Radius As Integer = 3
    <DisplayName("啟用達記憶體限制時的區塊回收機制")> <DefaultValue(True)> <Category("記憶體")> <Description("是否要啟用達記憶體限制時的區塊回收機制")>
    Public Property Max_Chunk_Trigger_Chunk_Collect As Boolean = True
    <DisplayName("在達記憶體限制時禁止增加區塊快取")> <DefaultValue(True)> <Category("記憶體")> <Description("是否在達記憶體限制時禁止增加區塊快取")>
    Public Property Disable_Chunk_Cache As Boolean = True
    <DisplayName("啟用達記憶體限制時的世界快取清除機制")> <DefaultValue(True)> <Category("記憶體")> <Description("是否在達記憶體限制時清除世界快取")>
    Public Property World_Low_Memory_Trigger As Boolean = True
#End Region
#Region "Network"
    <DisplayName("數據包大小閥值")> <DefaultValue(256)> <Category("網路")> <Description("數據包大小閥值（單位：位元組），這些包將被壓縮" &
                                   vbNewLine & "0 - 壓縮全部。" &
                                   vbNewLine & "-1 - 停用功能")>
    Public Property Batch_Threshold As Integer = 256
    <DisplayName("資料包壓縮等級")> <DefaultValue(7)> <Category("網路")> <Description("壓縮等級，等級越高，CPU佔用越高，佔用頻寬越少")>
    Public Property Compression_Level As Integer = 7
    <DisplayName("非同步資料包壓縮")> <DefaultValue(False)> <Category("網路")> <Description("非同步壓縮封包資料，緩解主線程CPU負載")>
    Public Property Async_Compression As Boolean = False
    <DisplayName("最大封包大小")> <DefaultValue(1492)> <Category("網路")> <Description("限制所有封包大小為指定位元組，過大的封包將會被切割成合適大小的幾個小封包")>
    Public Property Max_Mtu_Size As Integer = 1492
#End Region
#Region "Debug"
    <DisplayName("偵錯等級")> <DefaultValue(1)> <Category("偵錯")> <Description("當偵錯級別 > 1 時，將在控制台顯示偵錯資訊")>
    Public Property Debug_Level As Integer = 1
#End Region
#Region "Players"
    <DisplayName("儲存玩家資料")> <DefaultValue(True)> <Category("玩家")> <Description("玩家資料是否將在關閉伺服器時儲存")>
    Public Property Save_Player_Data As Boolean = True
    <DisplayName("允許以作弊的方式移動")> <DefaultValue(True)> <Category("玩家")> <Description("玩家是否可以用作弊的方式對移動速度作修改")>
    Public Property Allow_Movement_Cheats As Boolean = True
#End Region
#Region "Chunk"
    <DisplayName("每刻區塊傳送數量")> <DefaultValue(4)> <Category("區塊")> <Description("每遊戲刻發送給玩家區塊的數量")>
    Public Property Chunk_Sending_Per_Tick As Integer = 4
    <DisplayName("玩家初始加載區塊數量")> <DefaultValue(4)> <Category("區塊")> <Description("玩家進入伺服器需要的區塊數量")>
    Public Property Chunk_Sending_Spawn_Radius As Integer = 4
    <DisplayName("每刻處理區塊數量")> <DefaultValue(40)> <Category("區塊")> <Description("每遊戲刻處理的區塊數量")>
    Public Property Chunk_Ticking_Per_Tick As Integer = 40
    <DisplayName("區塊處理半徑")> <DefaultValue(3)> <Category("區塊")> <Description("玩家周圍區塊處理的半徑大小")>
    Public Property Chunk_Ticking_Tick_Radius As Integer = 3
    <DisplayName("區塊光照更新")> <DefaultValue(False)> <Category("區塊")> <Description("是否隨時更新光照")>
    Public Property Chunk_Ticking_Light_Updates As Boolean = False
    <DisplayName("區塊填充序列數量上限")> <DefaultValue(8)> <Category("區塊")> <Description("等待序列中，被填充的區塊的數量上限")>
    Public Property Chunk_Generation_Population_Queue_Size As Integer = 8
#End Region
#Region "Ticks-Per"
    <DisplayName("自動儲存週期")> <DefaultValue(6000)> <Category("時間控制")> <Description("自動儲存的週期（單位:遊戲刻）")>
    Public Property Ticks_Per_Autosave As Integer = 6000
#End Region
#Region "Auto-Report"
    <DisplayName("啟用崩潰時自動回報")> <DefaultValue(True)> <Category("崩潰回報")> <Description("是否啟用崩潰時自動回報給PocketMine")>
    Public Property Auto_Report_Enabled As Boolean = True
    <DisplayName("崩潰回報時是否傳送程式碼")> <DefaultValue(True)> <Category("崩潰回報")> <Description("是否在自動回報時傳送PocketMine 程式碼")>
    Public Property Auto_Report_Send_Code As Boolean = True
    <DisplayName("崩潰回報時是否傳送設定")> <DefaultValue(True)> <Category("崩潰回報")> <Description("是否在自動回報時傳送PocketMine 設定")>
    Public Property Auto_Report_Send_Settings As Boolean = True
    <DisplayName("崩潰回報時是否傳送PHP 資訊")> <DefaultValue(False)> <Category("崩潰回報")> <Description("是否在自動回報時傳送用來啟動的PHP的資訊")>
    Public Property Auto_Report_Send_PHPInfo As Boolean = False
    <DisplayName("崩潰回報時是否使用HTTPS協定")> <DefaultValue(True)> <Category("崩潰回報")> <Description("是否在自動回報時使用HTTPS協定傳送")>
    Public Property Auto_Report_Use_HTTPS As Boolean = True
#End Region
#Region "Auto-Updater"
    <DisplayName("啟用自動更新系統")> <DefaultValue(False)> <Category("自動更新(不支援)")> <Description("是否啟用內部自動更新" &
                                                                                    vbNewLine & " 警告，這會使伺服器管理員的版本資訊檔案出錯，不建議啟用")>
    Public Property Auto_Updater_Enabled As Boolean = False
    <DisplayName("更新時提醒主控台")> <DefaultValue(True)> <Category("自動更新(不支援)")> <Description("是否在自動更新時在主控台上顯示訊息")>
    Public Property On_Update_Warn_Console As Boolean = True
    <DisplayName("更新時提醒管理員")> <DefaultValue(True)> <Category("自動更新(不支援)")> <Description("是否在自動更新時通知所有擁有OP權限的管理員")>
    Public Property On_Update_Warn_Ops As Boolean = True
    <DisplayName("更新頻道")> <DefaultValue(PocketMineAutoUpdaterChannel.Stable)> <Category("自動更新(不支援)")> <Description("自動更新所使用的頻道")>
    Public Property Preferred_Channel As PocketMineAutoUpdaterChannel = PocketMineAutoUpdaterChannel.Stable
#End Region
#Region "PocketMine YAML Object"
    Enum PocketMineAutoUpdaterChannel
        Development
        Alpha
        Beta
        Stable
    End Enum
#End Region
End Class
