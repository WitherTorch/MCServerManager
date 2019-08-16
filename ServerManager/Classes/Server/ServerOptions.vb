#Region "ServerOptionEnums"
Imports System.ComponentModel
Imports System.Dynamic
Imports ServerManager
''' <summary>
''' 遊戲難度
''' </summary>
Enum Difficulty
    ''' <summary>
    ''' 和平
    ''' </summary>
    Peaceful
    ''' <summary>
    ''' 簡單
    ''' </summary>
    Easy
    ''' <summary>
    ''' 普通
    ''' </summary>
    Normal
    ''' <summary>
    ''' 困難
    ''' </summary>
    Hard
End Enum
''' <summary>
''' 遊戲模式
''' </summary>
Enum Gamemode
    ''' <summary>
    ''' 生存模式
    ''' </summary>
    Survival
    ''' <summary>
    ''' 創造模式
    ''' </summary>
    Creative
    ''' <summary>
    ''' 冒險模式
    ''' </summary>
    Adventure
    ''' <summary>
    ''' 旁觀者模式
    ''' </summary>
    Spectator
End Enum
''' <summary>
''' OP 權限等級
''' </summary>
Enum Op_Permission_Level
    ''' <summary>
    ''' 最低
    ''' </summary>
    Lowest = 1
    ''' <summary>
    ''' 低
    ''' </summary>
    Low = 2
    ''' <summary>
    ''' 高
    ''' </summary>
    High = 3
    ''' <summary>
    ''' 最高
    ''' </summary>
    Highest = 4
End Enum
Enum Function_Permission_Level
    ''' <summary>
    ''' 預設(與命令方塊同等級)
    ''' </summary>
    [Default] = 2
    ''' <summary>
    ''' 正常OP權限
    ''' </summary>
    AsNormalOp = 3
    ''' <summary>
    ''' 控制台
    ''' </summary>
    Console = 4
End Enum
''' <summary>
''' 世界類型
''' </summary>
Enum Java_Level_Type
    ''' <summary>
    ''' 預設
    ''' </summary>
    [DEFAULT]
    ''' <summary>
    ''' 超平坦
    ''' </summary>
    FLAT
    ''' <summary>
    ''' 巨大生態系
    ''' </summary>
    LARGEBIOMES
    ''' <summary>
    ''' 巨大化世界
    ''' </summary>
    AMPLIFIED
    ''' <summary>
    ''' 自定義地形
    ''' </summary>
    CUSTOMIZED
    ''' <summary>
    ''' 自定義生物群系
    ''' </summary>
    BUFFET
End Enum
''' <summary>
''' 世界類型
''' </summary>
Enum Bedrock_Level_Type
    ''' <summary>
    ''' 無限
    ''' </summary>
    INFINITE
    ''' <summary>
    ''' 超平坦
    ''' </summary>
    FLAT
    ''' <summary>
    ''' 舊世界類型(256*256)
    ''' </summary>
    OLD
End Enum
''' <summary>
''' 
''' </summary>
Enum Bedrock_Player_Permission_Level
    ''' <summary>
    ''' 訪客
    ''' </summary>
    Visitor
    ''' <summary>
    ''' 成員
    ''' </summary>
    Member
    ''' <summary>
    ''' 管理員
    ''' </summary>
    [Operator]
End Enum

#End Region
Interface IServerOptions
    Sub InputOption(serverOption As IDictionary(Of String, String))
    Sub SetValue(optionName As String, value As String)
    Function OutputOption() As IDictionary(Of String, String)
End Interface
''' <summary>
''' server.properties 的對應.NET 類別(Java 版)
''' </summary>
Class JavaServerOptions
    Inherits Dynamic.DynamicObject
    Implements IServerOptions, ICustomTypeDescriptor, INotifyPropertyChanged
    Dim dictionary As New Dictionary(Of String, String)
    <DisplayName("允許玩家飛行")> <DefaultValue(False)> <Category("玩家")> <Description("允許玩家在安裝添加飛行功能的 mod 前提下在生存模式下飛行。" &
                                                               vbNewLine & "允許飛行可能會使作弊者更加常見，因為此設定會使他們更容易達成目的。" &
                                                               vbNewLine & "在創造模式下本屬性不會有任何作用。" &
                                                               vbNewLine & "false - 不允許飛行。懸空超過5秒的玩家會被踢出伺服器。" &
                                                               vbNewLine & "true - 允許飛行。玩家得以使用飛行MOD飛行。")>
    Public Property Allow_Flight As Boolean = False
    <DisplayName("允許進入地獄")> <DefaultValue(True)> <Category("地圖")> <Description("允許玩家進入地獄。 " &
                                                              vbNewLine & "False - 地獄傳送門不會生效。" &
                                                              vbNewLine & "True - 玩家可以通過地獄傳送門前往地獄。")>
    Public Property Allow_Nether As Boolean = True
    <DisplayName("顯示玩家獲得成就")> <DefaultValue(True)> <Category("玩家")> <Description("玩家獲得 成就/進度 時是否在伺服器中進行顯示。  " &
                                                              vbNewLine & "False - 玩家獲得 成就/進度 時的提示僅自己可見，不會向其他玩家進行顯示。" &
                                                              vbNewLine & "True - 玩家獲得 成就/進度 時將在其他在線玩家的聊天欄進行提示。")>
    Public Property Announce_Player_Achievements As Boolean = True
    <DisplayName("難度")> <DefaultValue(Difficulty.Easy)> <Category("玩家")> <Description("定義伺服器的遊戲難度（例如生物對玩家造成的傷害，飢餓與中毒對玩家的影響方式等）。  " &
                                                              vbNewLine & "Peaceful - 和平" &
                                                              vbNewLine & "Easy - 簡單" &
                                                              vbNewLine & "Normal - 普通" &
                                                              vbNewLine & "Hard - 困難")>
    Public Property Difficulty As Difficulty = Difficulty.Easy
    <DisplayName("允許收集信息")> <DefaultValue(False)> <Category("技術性")> <Description("允許使用GameSpy4協議的伺服器監聽器。它被用於收集伺服器信息。")>
    Public Property Enable_Query As Boolean = False
    <DisplayName("允許遠程訪問")> <DefaultValue(False)> <Category("技術性")> <Description("是否允許遠程訪問伺服器控制台。")>
    Public Property Enable_Rcon As Boolean = False
    <DisplayName("強制遊戲模式")> <DefaultValue(False)> <Category("玩家")> <Description("強制玩家加入時為默認遊戲模式" &
                                                              vbNewLine & "False - 玩家將以退出前的遊戲模式加入" &
                                                              vbNewLine & "True - 玩家總是以默認遊戲模式加入")>
    Public Property Force_Gamemode As Boolean = False
    <DisplayName("預設遊戲模式")> <DefaultValue(Gamemode.Survival)> <Category("玩家")> <Description("定義默認遊戲模式" &
                                                              vbNewLine & "Survival - 生存模式" &
                                                              vbNewLine & "Creative - 創造模式" &
                                                              vbNewLine & "Adventure - 冒險模式（僅在12w22a之後，或正式版1.3之後可用）" &
                                                              vbNewLine & "Spectator - 旁觀模式（僅在14w05a之後，或正式版1.8之後可用）")>
    Public Property Gamemode As Gamemode = Gamemode.Survival
    <DisplayName("生成結構")> <DefaultValue(True)> <Category("地圖")> <Description("定義是否在生成世界時生成結構（例如村莊）" &
                                                              vbNewLine & "False - 新生成的區塊中將不包含結構。" &
                                                              vbNewLine & "True - 新生成的區塊中將包含結構。")>
    Public Property Generate_Structures As Boolean = True
    <DisplayName("世界生成器設置")> <DefaultValue("")> <Category("地圖")> <Description("本屬性只用於自訂超平坦世界的生成。")>
    Public ReadOnly Property Generator_Settings As String = ""
    <DisplayName("極限模式")> <DefaultValue(False)> <Category("玩家")> <Description("一旦啟用，玩家在死後會自動被伺服器封禁（即開啟極限模式）。")>
    Public Property Hardcore As Boolean = False
    <DisplayName("地圖名稱")> <DefaultValue("world")> <Category("地圖")> <Description("""地圖名稱""的值將作為世界名稱及其資料夾名。")>
    Public ReadOnly Property Level_Name As String = "world"
    <DisplayName("地圖種子")> <DefaultValue("")> <Category("地圖")> <Description("與單人遊戲類似，為世界定義一個種子。")>
    Public ReadOnly Property Level_Seed As String = ""
    <DefaultValue(Java_Level_Type.DEFAULT)> <Category("地圖")> <Description("確定地圖所生成的類型" &
                                                              vbNewLine & "DEFAULT - 標準的世界，帶有丘陵，河谷，海洋等" &
                                                              vbNewLine & "FLAT - 一個沒有特色的平坦世界，適合用於建設（1.13 之後可以使用""世界生成器設置""自訂地形(同之前的 CUSTOMIZED)。）" &
                                                              vbNewLine & "LARGEBIOMES - 同預設世界，但所有生態系都更大（僅快照12w19a，或正式版1.3之後可用）" &
                                                              vbNewLine & "AMPLIFIED - 同預設世界，但世界生成高度提高（僅快照13w36a，或正式版1.7.2之後可用）" &
                                                              vbNewLine & "CUSTOMIZED - 自訂式地形，透過使用 Generator_Settings 來自訂地形（僅快照14w21b，或正式版1.8到1.12.2可用）" &
                                                              vbNewLine & "BUFFET - 自選世界類型，如果沒有在 Generator_Settings 輸入代碼,將會生成標準的世界(即 DEFAULT)（僅快照18w16a，或正式版1.13之後可用）")>
    <DisplayName("地圖類型")> Public ReadOnly Property Level_Type As Java_Level_Type = Java_Level_Type.DEFAULT
    Dim _Max_Build_Height As Integer = 256
    <DisplayName("最高建築高度")> <DefaultValue(256)> <Category("地圖")> <Description("玩家在遊戲中能夠建造的最大高度。地形生成算法並不會受這個值的影響。")>
    Public Property Max_Build_Height As Integer
        Get
            Return _Max_Build_Height
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 256 Then
                _Max_Build_Height = value
            Else
                MsgBox("最大建築高度只能在1~256之間", , Application.ProductName)
            End If
        End Set
    End Property
    Dim _Max_Players As Integer = 20
    <DisplayName("玩家最大數量")> <DefaultValue(20)> <Category("玩家")> <Description("伺服器同時能容納的最大玩家數量。但請注意在線玩家越多，對伺服器造成的負擔也就越大。" &
                                                              vbNewLine & "伺服器的OP具有在人滿的情況下強行進入伺服器的權力，找到在伺服器根目錄下叫ops.json的文件並打開，" &
                                                              vbNewLine & "設置你要突破人數限制的OP下的bypassesPlayerLimit選項為true即可（默認值為false），" &
                                                              vbNewLine & "這意味著OP將不需要在伺服器人滿時等待玩家離開再加入，過大的數值會使客戶端顯示的玩家列表崩壞。")>
    Public Property Max_Players As Integer
        Get
            Return _Max_Players
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 2147483647 Then
                _Max_Players = value
            Else
                MsgBox("最大玩家人數只能在1~2147483647之間", , Application.ProductName)
            End If
        End Set
    End Property
    Dim _Max_Tick_Time As Integer = 60000
    <DisplayName("最大遊戲刻延遲時間")> <DefaultValue(60000)> <Category("技術性")> <Description("設置每個tick花費的最大毫秒數，超過該最大毫秒數看門狗將停止伺服器程序並附帶上一些信息。" &
                                                              vbNewLine & "正常情況下伺服器的一個tick最大會花費60.00秒（最小應該為0.05秒）；" &
                                                              vbNewLine & "如果伺服器程序被判定為崩潰了，它將被強制終止運行。遇到這種情況的時候，它會調用System.exit(1)。 " &
                                                              vbNewLine & "-1 - 完全停用看門狗（這個停用選項在14w32a快照中被添加）")>
    Public Property Max_Tick_Time As Integer
        Get
            Return _Max_Tick_Time
        End Get
        Set(value As Integer)
            If value >= -1 And value <= ((2 ^ 63) - 1) Then
                _Max_Tick_Time = value
            End If
        End Set
    End Property
    Dim _Max_World_Size As Integer = 29999984
    <DisplayName("世界邊界大小")> <DefaultValue(29999984)> <Category("地圖")> <Description("設置世界邊界的最大半徑值，單位為方塊。" &
                                                              vbNewLine & "通過成功執行的指令能把世界邊界設置得更大，但不會超過這裡設置的最大方塊限制。" &
                                                              vbNewLine & "如果設置的 Max_World_Size 超過默認值的大小，那將不會起任何效果。 ")>
    Public Property Max_World_Size As Integer
        Get
            Return _Max_World_Size
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 29999984 Then
                _Max_World_Size = value
            Else
                MsgBox("最大世界大小只能在1~29999984之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("MOTD")> <DefaultValue("A Minecraft Server")> <Category("技術性")> <Description("本屬性值是玩家客戶端的多人遊戲伺服器列表中顯示的伺服器信息，顯示於名稱下方。" &
                                                              vbNewLine & vbTab & "MOTD 支持樣式代碼。" &
                                                              vbNewLine & vbTab & "MOTD 支持特殊符號， 比如「 ♥」。 然而，這些符號需要被轉換為Unicode轉義字符。" &
                                                              vbNewLine & vbTab & "如果 MOTD 超過59個字符，伺服器列表很可能會返回「通訊錯誤」。")>
    Public Property Motd As String = ""
    Dim _Network_Compression_Threshold As Long = 256
    <DisplayName("封包壓縮閥值")> <DefaultValue(256L)> <Category("技術性")> <Description("默認會允許n-1字節的資料包正常發送, 如果資料包為 n 字節或更大時會進行壓縮。" &
                                                              vbNewLine & "所以，更低的數值會使得更多的資料包被壓縮，但是如果被壓縮的資料包字節太小將會得不償失。" &
                                                              vbNewLine & "-1 - 永久禁用資料包壓縮" &
                                                              vbNewLine & "0 - 壓縮全部資料包")>
    Public Property Network_Compression_Threshold As Long
        Get
            Return _Network_Compression_Threshold
        End Get
        Set(value As Long)
            If value >= -1 And value <= Long.MaxValue Then
                _Network_Compression_Threshold = value
            End If
        End Set
    End Property

    <DisplayName("正版驗證")> <DefaultValue(True)> <Category("玩家")> <Description("是否開啟在線驗證。" &
                                                              vbNewLine & "伺服器會與 Minecraft 的帳戶資料庫對比檢查連入玩家。" &
                                                             vbNewLine & "如果你的伺服器並未與 Internet 連接，則將這個值設為 false ，" &
                                                              vbNewLine & "然而這樣的話破壞者也能夠使用任意假帳戶登錄伺服器。" &
                                                               vbNewLine & "如果 Minecraft.net 伺服器下線，那麼開啟在線驗證的伺服器會因為無法驗證玩家身份而拒絕所有玩家加入。" &
                                                              vbNewLine & "通常，這個值設為 true 的伺服器被稱為「正版伺服器」，設為 false 的被稱為「離線伺服器」或「盜版伺服器」。 " &
                                                             vbNewLine & "True - 啟用。伺服器會認為自己具有 Internet 連接，並檢查每一位連入的玩家。" &
                                                             vbNewLine & "False - 禁用。伺服器不會檢查玩家。")>
    Public Property Online_Mode As Boolean = True
    <DisplayName("OP權限等級")> <DefaultValue(Op_Permission_Level.Highest)> <Category("玩家")> <Description("設定OP的權限等級" &
                                                              vbNewLine & "Lowest - OP可以無視重生點保護。" &
                                                             vbNewLine & "Low - OP可以使用單人遊戲作弊指令（除了/publish，因為不能在伺服器上使用，/debug也是）並使用指令方塊。指令方塊和Realm服主/管理員有此等級權限。" &
                                                              vbNewLine & "High - OP可以使用幾乎所有多人遊戲限定的指令（除Highest專用指令）" &
                                                             vbNewLine & "Highest - OP可以使用所有指令，包括/stop、/save-all、/save-on和/save-off。")>
    Public Property Op_Permission_Level As Op_Permission_Level = Op_Permission_Level.Highest
    <DisplayName("玩家空閒時間")> <DefaultValue(0)> <Category("玩家")> <Description("如果不為0，伺服器將在玩家的空閒時間達到設置的時間（單位為分鐘）時將玩家踢出伺服器 " &
                                                             vbNewLine & "例如：把Player_Idle_Timeout設置為3，玩家空閒時間達到3分鐘就會被踢出伺服器。")>
    Public Property Player_Idle_Timeout As ULong = 0
    <DisplayName("禁用VPN")> <DefaultValue(False)> <Category("技術性")> <Description("如果伺服器發送的和Mojang的驗證伺服器的ISP/AS不一樣,玩家將會被踢出 " &
                                                             vbNewLine & "True - 伺服器將會禁止玩家使用VPN或代理" &
                                                             vbNewLine & "False - 伺服器將不會禁止玩家使用VPN或代理")>
    Public Property Prevent_Proxy_Connections As Boolean = False
    <DisplayName("允許PVP")> <DefaultValue(True)> <Category("玩家")> <Description("是否允許PvP。玩家自己的箭也只有在允許PvP時才可能傷害到自己。 " &
                                                             vbNewLine & "註： 來源於玩家的間接傷害，例如熔岩，火，TNT等，還是會造成傷害。" &
                                                             vbNewLine & "True - 玩家可以互相殘殺。" &
                                                             vbNewLine & "False - 玩家無法互相造成傷害。")>
    Public Property PvP As Boolean = True
    Dim _Query_Port As Integer = 25565
    <DisplayName("收集信息埠")> <DefaultValue(25565)> <Category("技術性")> <Description("設置監聽伺服器的埠號")>
    Public Property Query_Port As Integer
        Get
            Return _Query_Port
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Query_Port = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("遠端訪問密碼")> <DefaultValue("")> <Category("技術性")> <Description("設置遠程訪問的密碼")>
    Public Property Rcon_Password As String = ""
    Dim _Rcon_Port As Integer = 25575
    <DisplayName("遠端訪問埠")> <DefaultValue(25575)> <Category("技術性")> <Description("設置遠程訪問的埠號")>
    Public Property Rcon_Port As Integer
        Get
            Return _Rcon_Port
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Rcon_Port = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("資源包位址")> <DefaultValue("")> <Category("技術性")> <Description("輸入指向一個資源包的URI。玩家可選擇是否使用該資源包。")>
    Public Property Resource_Pack As String = ""
    <DisplayName("資源包驗證碼")> <DefaultValue("")> <Category("技術性")> <Description("資源包的SHA-1值，必須為小寫十六進位，建議填寫它，" &
                                                          vbNewLine & "這還沒有用於驗證資源包的完整性，但是它提高了資源包緩存的有效性和可靠性。 ")>
    Public Property Resource_Pack_Sha1 As String = ""
    <DisplayName("伺服器IP")> <DefaultValue("")> <Category("技術性")> <Description("將伺服器與一個特定IP綁定。強烈建議你留空本屬性值！" &
                                                             vbNewLine & "      留空，或是填入你想讓伺服器綁定的IP。")>
    Public Property Server_Ip As String = ""
    Dim _Server_Port As Integer = 25565
    <DisplayName("伺服器埠")> <DefaultValue(25565)> <Category("技術性")> <Description("改變伺服器埠號。如果伺服器通過路由器與外界連接的話，該埠必須也能夠通過路由器。 ")>
    Public Property Server_Port As Integer
        Get
            Return _Server_Port
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Server_Port = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("允許數據採集")> <DefaultValue(True)> <Category("技術性")> <Description("自1.3正式版之後，一旦啟用，將允許服務端定期發送統計數據到http://snoop.minecraft.net. " &
                                                             vbNewLine & "True - 啟用數據採集" &
                                                             vbNewLine & "False - 禁用數據採集")>
    Public Property Snooper_Enabled As Boolean = True
    <DefaultValue(True)> <Category("生成")> <Description("決定動物是否可以生成。 " &
                                                             vbNewLine & "True - 動物可以生成。" &
                                                             vbNewLine & "False - 動物生成後會立即消失。")>
    <DisplayName("允許生成動物")> Public Property Spawn_Animals As Boolean = True
    <DefaultValue(True)> <Category("生成")> <Description("決定攻擊型生物（怪物）是否可以生成。  " &
                                                             vbNewLine & "True - 可以。只要滿足條件的話怪物就會生成。" &
                                                             vbNewLine & "False - 不會有任何怪物。" &
                                                             vbNewLine & "如果Difficulty = Peaceful（和平）的話，本屬性值不會有任何影響。")>
    <DisplayName("允許生成怪物")> Public Property Spawn_Monsters As Boolean = True
    <DefaultValue(True)> <Category("生成")> <Description("決定是否生成村民。 " &
                                                             vbNewLine & "True - 生成村民。" &
                                                             vbNewLine & "False - 不會有村民。")>
    <DisplayName("允許生成村民")> Public Property Spawn_NPCs As Boolean = True
    <DefaultValue(16)> <Category("技術性")> <Description("通過將該值進行(x*2)+1的運算來決定出生點的保護半徑。" &
                                                              vbNewLine & "設置為0將不會禁用出生點保護。設置為0將會保護位於出生點的1x1方塊區域，" &
                                                             vbNewLine & "設置為1將會保護以出生點為中心的3x3方塊區域。" &
                                                              vbNewLine & "設置為2會保護5x5的方塊區域，設置為3將會保護7x7的方塊區域，以此類推。 " &
                                                             vbNewLine & "這個選項只會在第一個玩家進行伺服器時生成。如果伺服器沒有設置OP，這個選項將會自動禁用。")>
    <DisplayName("出生點保護距離")> Public Property Spawn_Protection As Integer = 16
    Dim _View_Distance As Integer = 10
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    <DisplayName("視野距離")> <DefaultValue(10)> <Category("地圖")> <Description("設置服務端傳送給客戶端的數據量，也就是設置玩家各個方向上的區塊數量 (是以玩家為中心的半徑，不是直徑)。 " &
                                                              vbNewLine & "它決定了服務端的可視距離。 " &
                                                             vbNewLine & "默認/推薦設置為10，如果很卡的話，減少該數值。")>
    Public Property View_Distance As Integer
        Get
            Return _View_Distance
        End Get
        Set(value As Integer)
            If value >= 3 And value <= 15 Then
                _View_Distance = value
            Else
                MsgBox("渲染距離必須在3~15之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("啟用白名單")> <DefaultValue(False)> <Category("玩家")> <Description("伺服器的白名單 " &
                                                            vbNewLine & "當啟用時，只有白名單上的用戶才能連接伺服器。" &
                                                            vbNewLine & "白名單主要用於私人伺服器，例如相識的朋友等。" &
                                                             vbNewLine & "True - 從 whitelist.json 文件加載白名單。" &
                                                             vbNewLine & "False - 不使用白名單。")>
    Public Property White_List As Boolean = False
    <DisplayName("啟用指令方塊")> <DefaultValue(False)> <Category("技術性")> <Description("是否啟用指令方塊" &
                                                             vbNewLine & "True - 啟用" &
                                                             vbNewLine & "False - 禁用")>
    Public Property Enable_Command_Block As Boolean = False
    <DisplayName("強制使用白名單")> <DefaultValue(False)> <Category("玩家")> <Description("在伺服器上強制使用白名單。" &
                                                            vbNewLine & "啟用此選項後, 在伺服器重新載入 whitelist.json 後," &
                                                            vbNewLine & "在白名單上不存在的使用者將會被踢出伺服器(如果有啟用白名單)。" &
                                                             vbNewLine & "True - 不在白名單上的使用者將會被踢出伺服器。" &
                                                             vbNewLine & "False - 沒有任何使用者會被踢出伺服器。")>
    Public Property Enforce_Whitelist As Boolean = False
    <DisplayName("函數權限等級")> <DefaultValue(Function_Permission_Level.Default)> <Category("技術性")> <Description("在伺服器上函數的權限等級。" &
                                                            vbNewLine & "Default - 與命令方塊同等級" &
                                                            vbNewLine & "AsNormalOP - 可使用一般OP所使用的指令（如/ban, /op 之類的）" &
                                                             vbNewLine & "Console - 能使用所有指令。")>
    Public Property Function_Permission_Level As Function_Permission_Level = Function_Permission_Level.Default
    Public Overloads Function TryGetMember(propertyName As String, ByRef result As Object) As Boolean
        If dictionary IsNot Nothing AndAlso propertyName.StartsWith("otherVar") AndAlso IsNumeric(propertyName.Substring(8)) AndAlso dictionary.Count > propertyName.Substring(8) Then
            result = dictionary.Values(CInt(propertyName.Substring(8)))
            Return True
        Else
            result = Nothing
            Return False
        End If
    End Function
    Public Overloads Overrides Function TryGetMember(binder As GetMemberBinder, ByRef result As Object) As Boolean
        If dictionary IsNot Nothing AndAlso binder.Name.StartsWith("otherVar") AndAlso IsNumeric(binder.Name.Substring(8)) AndAlso dictionary.Count > binder.Name.Substring(8) Then
            result = dictionary.Values(CInt(binder.Name.Substring(8)))
            Return True
        Else
            result = Nothing
            Return False
        End If
    End Function
    Public Overloads Function TrySetMember(propertyName As String, value As Object) As Boolean
        If dictionary IsNot Nothing AndAlso propertyName.StartsWith("otherVar") AndAlso IsNumeric(propertyName.Substring(8)) AndAlso dictionary.Count > propertyName.Substring(8) Then
            dictionary.Item(dictionary.Keys(CInt(propertyName.Substring(8)))) = value
            Return True
        Else
            Return False
        End If
    End Function
    Public Overloads Overrides Function TrySetMember(binder As SetMemberBinder, value As Object) As Boolean
        If dictionary IsNot Nothing AndAlso binder.Name.StartsWith("otherVar") AndAlso IsNumeric(binder.Name.Substring(8)) AndAlso dictionary.Count > binder.Name.Substring(8) Then
            dictionary.Item(dictionary.Keys(CInt(binder.Name.Substring(8)))) = value
            Return True
        Else
            Return False
        End If
    End Function
    Public Sub InputOption(serverOption As IDictionary(Of String, String)) Implements IServerOptions.InputOption
        If serverOption IsNot Nothing Then
            For Each [option] In serverOption
                Try
                    Select Case [option].Key.Trim
                        Case "allow-flight"
                            Allow_Flight = [option].Value
                        Case "allow-nether"
                            Allow_Nether = [option].Value
                        Case "announce-player-achievements"
                            Announce_Player_Achievements = [option].Value
                        Case "difficulty"
                            Difficulty = [Enum].Parse(GetType(Difficulty), [option].Value)
                        Case "enable-query"
                            Enable_Query = [option].Value
                        Case "enable-rcon"
                            Enable_Rcon = [option].Value
                        Case "force-gamemode"
                            Force_Gamemode = [option].Value
                        Case "gamemode"
                            Gamemode = [Enum].Parse(GetType(Gamemode), [option].Value)
                        Case "generate-structures"
                            Generate_Structures = [option].Value
                        Case "generator-settings"
                            _Generator_Settings = [option].Value
                        Case "hardcore"
                            Hardcore = [option].Value
                        Case "level-name"
                            _Level_Name = [option].Value
                        Case "level-seed"
                            _Level_Seed = [option].Value
                        Case "level-type"
                            _Level_Type = [Enum].Parse(GetType(Java_Level_Type), [option].Value.ToUpper)
                        Case "max-build-height"
                            Max_Build_Height = [option].Value
                        Case "max-players"
                            Max_Players = [option].Value
                        Case "max-tick-time"
                            Max_Tick_Time = [option].Value
                        Case "max-world-size"
                            Max_World_Size = [option].Value
                        Case "motd"
                            Motd = [option].Value
                        Case "network-compression-threshold"
                            Network_Compression_Threshold = [option].Value
                        Case "online-mode"
                            Online_Mode = [option].Value
                        Case "op-permission-level"
                            Op_Permission_Level = [Enum].Parse(GetType(Op_Permission_Level), [option].Value)
                        Case "player-idle-timeout"
                            Player_Idle_Timeout = ULong.Parse([option].Value)
                        Case "prevent-proxy-connections"
                            Prevent_Proxy_Connections = [option].Value
                        Case "pvp"
                            PvP = [option].Value
                        Case "query.port"
                            Query_Port = serverOption("query.port")
                        Case "rcon.password"
                            Rcon_Password = serverOption("rcon.password")
                        Case "rcon.port"
                            Rcon_Port = Integer.Parse(serverOption("rcon.port"))
                        Case "resource-pack"
                            _Resource_Pack = [option].Value
                        Case "resource-pack-sha1"
                            _Resource_Pack_Sha1 = [option].Value
                        Case "server-ip"
                            Server_Ip = [option].Value
                        Case "server-port"
                            Server_Port = [option].Value
                        Case "snooper-enabled"
                            Snooper_Enabled = [option].Value
                        Case "spawn-animals"
                            Spawn_Animals = [option].Value
                        Case "spawn-monsters"
                            Spawn_Monsters = [option].Value
                        Case "spawn-npcs"
                            Spawn_NPCs = [option].Value
                        Case "spawn-protection"
                            Spawn_Protection = Integer.Parse([option].Value)
                        Case "view-distance"
                            View_Distance = Integer.Parse([option].Value)
                        Case "white-list"
                            White_List = [option].Value
                        Case "enable-command-block"
                            Enable_Command_Block = [option].Value
                        Case "enforce-whitelist"
                            Enforce_Whitelist = [option].Value
                        Case "function-permission-level"
                            Function_Permission_Level = [Enum].Parse(GetType(Function_Permission_Level), [option].Value)
                        Case ""

                        Case Else
                            If dictionary Is Nothing Then dictionary = New Dictionary(Of String, String)()
                            If dictionary.ContainsKey([option].Key) Then
                                dictionary([option].Key) = [option].Value
                            Else
                                dictionary.Add([option].Key, [option].Value)
                            End If
                    End Select
                Catch ex As Exception

                End Try
            Next
        End If
    End Sub
    Public Sub SetValue(optionName As String, value As String) Implements IServerOptions.SetValue
        Select Case optionName
            Case "allow-flight"
                Allow_Flight = Boolean.Parse(value)
            Case "allow-nether"
                Allow_Nether = Boolean.Parse(value)
            Case "announce-player-achievements"
                Announce_Player_Achievements = Boolean.Parse(value)
            Case "difficulty"
                Difficulty = [Enum].Parse(GetType(Difficulty), value)
            Case "enable-query"
                Enable_Query = Boolean.Parse(value)
            Case "enable-rcon"
                Enable_Rcon = Boolean.Parse(value)
            Case "force-gamemode"
                Force_Gamemode = Boolean.Parse(value)
            Case "gamemode"
                Gamemode = [Enum].Parse(GetType(Gamemode), value)
            Case "generate-structures"
                Generate_Structures = Boolean.Parse(value)
            Case "generator-settings"
                _Generator_Settings = value
            Case "hardcore"
                Hardcore = Boolean.Parse(value)
            Case "level-name"
                _Level_Name = value
            Case "level-seed"
                _Level_Seed = value
            Case "level-type"
                _Level_Type = [Enum].Parse(GetType(Java_Level_Type), value.ToUpper)
            Case "max-build-height"
                Max_Build_Height = value
            Case "max-players"
                Max_Players = value
            Case "max-tick-time"
                Max_Tick_Time = value
            Case "max-world-size"
                Max_World_Size = value
            Case "motd"
                Motd = value
            Case "network-compression-threshold"
                Network_Compression_Threshold = value
            Case "online-mode"
                Online_Mode = Boolean.Parse(value)
            Case "op-permission-level"
                Op_Permission_Level = [Enum].Parse(GetType(Op_Permission_Level), value)
            Case "player-idle-timeout"
                Player_Idle_Timeout = ULong.Parse(value)
            Case "prevent-proxy-connections"
                Prevent_Proxy_Connections = Boolean.Parse(value)
            Case "pvp"
                PvP = Boolean.Parse(value)
            Case "query.port"
                Query_Port = value
            Case "rcon.password"
                Rcon_Password = value
            Case "rcon.port"
                Rcon_Port = Integer.Parse(value)
            Case "resource-pack"
                _Resource_Pack = value
            Case "resource-pack-sha1"
                _Resource_Pack_Sha1 = value
            Case "server-ip"
                Server_Ip = value
            Case "server-port"
                Server_Port = value
            Case "snooper-enabled"
                Snooper_Enabled = Boolean.Parse(value)
            Case "spawn-animals"
                Spawn_Animals = Boolean.Parse(value)
            Case "spawn-monsters"
                Spawn_Monsters = Boolean.Parse(value)
            Case "spawn-npcs"
                Spawn_NPCs = Boolean.Parse(value)
            Case "spawn-protection"
                Spawn_Protection = Integer.Parse(value)
            Case "view-distance"
                View_Distance = Integer.Parse(value)
            Case "white-list"
                White_List = Boolean.Parse(value)
            Case "enable-command-block"
                Enable_Command_Block = Boolean.Parse(value)
            Case "enforce-whitelist"
                Enforce_Whitelist = Boolean.Parse(value)
            Case "function-permission-level"
                Function_Permission_Level = [Enum].Parse(GetType(Function_Permission_Level), value)
            Case Else
                If dictionary Is Nothing Then dictionary = New Dictionary(Of String, String)()
                If dictionary.ContainsKey(optionName) Then
                    dictionary(optionName) = value
                Else
                    dictionary.Add(optionName, value)
                End If
        End Select
    End Sub
    Public Function OutputOption() As IDictionary(Of String, String) Implements IServerOptions.OutputOption
        Dim options As New Dictionary(Of String, String)
        options.Add("allow-flight", Allow_Flight.ToString.ToLower)
        options.Add("allow-nether", Allow_Nether.ToString.ToLower)
        options.Add("announce-player-achievements", Announce_Player_Achievements.ToString.ToLower)
        options.Add("difficulty", Difficulty)
        options.Add("enable-query", Enable_Query.ToString.ToLower)
        options.Add("enable-rcon", Enable_Rcon.ToString.ToLower)
        options.Add("force-gamemode", Force_Gamemode.ToString.ToLower)
        options.Add("gamemode", Gamemode)
        options.Add("generate-structures", Generate_Structures)
        options.Add("generator-settings", Generator_Settings)
        options.Add("hardcore", Hardcore.ToString.ToLower)
        options.Add("level-name", Level_Name)
        options.Add("level-seed", Level_Seed)
        options.Add("level-type", Level_Type.ToString)
        options.Add("max-build-height", Max_Build_Height)
        options.Add("max-players", Max_Players)
        options.Add("max-tick-time", Max_Tick_Time)
        options.Add("max-world-size", Max_World_Size)
        options.Add("motd", Motd)
        options.Add("network-compression-threshold", Network_Compression_Threshold)
        options.Add("online-mode", Online_Mode.ToString.ToLower)
        options.Add("op-permission-level", Op_Permission_Level)
        options.Add("player-idle-timeout", Player_Idle_Timeout)
        options.Add("prevent-proxy-connections", Prevent_Proxy_Connections.ToString.ToLower)
        options.Add("pvp", PvP.ToString.ToLower)
        options.Add("query.port", Query_Port)
        options.Add("rcon.password", Rcon_Password)
        options.Add("rcon.port", Rcon_Port)
        options.Add("resource-pack", Resource_Pack)
        options.Add("resource-pack-sha1", Resource_Pack_Sha1)
        options.Add("server-ip", Server_Ip)
        options.Add("server-port", Server_Port)
        options.Add("snooper-enabled", Snooper_Enabled.ToString.ToLower)
        options.Add("spawn-animals", Spawn_Animals.ToString.ToLower)
        options.Add("spawn-monsters", Spawn_Monsters.ToString.ToLower)
        options.Add("spawn-npcs", Spawn_NPCs.ToString.ToLower)
        options.Add("spawn-protection", Spawn_Protection)
        options.Add("view-distance", View_Distance)
        options.Add("white-list", White_List.ToString.ToLower)
        options.Add("enable-command-block", Enable_Command_Block.ToString.ToLower)
        options.Add("enforce-whitelist", Enforce_Whitelist.ToString.ToLower)
        options.Add("function-permission-level", Function_Permission_Level)
        If dictionary IsNot Nothing AndAlso dictionary.Count > 0 Then
            For Each item In dictionary
                If options.ContainsKey(item.Key) Then
                    options(item.Key) = item.Value
                Else
                    options.Add(item.Key, item.Value)
                End If
            Next
        End If
        Return options
    End Function
#Region "Implements ICustomTypeDescriptor"
    Public Function GetAttributes() As AttributeCollection Implements ICustomTypeDescriptor.GetAttributes
        Return New AttributeCollection({})
    End Function

    Public Function GetClassName() As String Implements ICustomTypeDescriptor.GetClassName
        Return [GetType]().Name
    End Function

    Public Function GetComponentName() As String Implements ICustomTypeDescriptor.GetComponentName
        Return Application.ProductName
    End Function

    Public Function GetConverter() As TypeConverter Implements ICustomTypeDescriptor.GetConverter
        Return Nothing
    End Function

    Public Function GetDefaultEvent() As EventDescriptor Implements ICustomTypeDescriptor.GetDefaultEvent
        Return Nothing
    End Function

    Public Function GetDefaultProperty() As PropertyDescriptor Implements ICustomTypeDescriptor.GetDefaultProperty
        Return Nothing
    End Function

    Public Function GetEditor(editorBaseType As Type) As Object Implements ICustomTypeDescriptor.GetEditor
        Return TypeDescriptor.GetEditor(GetType(Object), editorBaseType)
    End Function

    Public Function GetEvents() As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return New EventDescriptorCollection({})
    End Function

    Public Function GetEvents(attributes() As Attribute) As EventDescriptorCollection Implements ICustomTypeDescriptor.GetEvents
        Return New EventDescriptorCollection({})
    End Function

    Public Function GetProperties() As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Dim collect As New List(Of PropertyDescriptor)
        For Each _property In [GetType]().GetProperties()
            Dim _attributes As New List(Of Attribute)
            For Each attr In _property.GetCustomAttributes(True)
                _attributes.Add(attr)
            Next
            collect.Add(TypeDescriptor.CreateProperty([GetType](), _property.Name, _property.PropertyType, _attributes.ToArray))
        Next
        Dim i As Integer = 0
        For Each _property In dictionary
            collect.Add(New JavaServerOptionsPropertyDescriptor(Me, "otherVar" & i.ToString, New DisplayNameAttribute(_property.Key), New CategoryAttribute("其他")))
            i += 1
        Next
        Return New PropertyDescriptorCollection(collect.ToArray)
    End Function

    Public Function GetProperties(attributes() As Attribute) As PropertyDescriptorCollection Implements ICustomTypeDescriptor.GetProperties
        Dim collect As New List(Of PropertyDescriptor)
        For Each _property In [GetType]().GetProperties()
            Dim _attributes As New List(Of Attribute)
            For Each attr In _property.GetCustomAttributes(True)
                _attributes.Add(attr)
            Next
            collect.Add(TypeDescriptor.CreateProperty([GetType](), _property.Name, _property.PropertyType, _attributes.ToArray))
        Next
        Dim i As Integer = 0
        For Each _property In dictionary
            collect.Add(New JavaServerOptionsPropertyDescriptor(Me, "otherVar" & i.ToString, New DisplayNameAttribute(_property.Key), New CategoryAttribute("其他")))
            i += 1
        Next
        Return New PropertyDescriptorCollection(collect.ToArray)
    End Function

    Public Function GetPropertyOwner(pd As PropertyDescriptor) As Object Implements ICustomTypeDescriptor.GetPropertyOwner
        Return Me
    End Function
    Class JavaServerOptionsPropertyDescriptor
        Inherits PropertyDescriptor
        Sub New([option] As JavaServerOptions, name As String, ParamArray attributes As Attribute())
            MyBase.New(name, attributes)
        End Sub
        Public Overrides ReadOnly Property ComponentType As Type
            Get
                Return GetType(JavaServerOptions)
            End Get
        End Property

        Public Overrides ReadOnly Property IsReadOnly As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides ReadOnly Property PropertyType As Type
            Get
                Return GetType(String)
            End Get
        End Property

        Public Overrides Sub ResetValue(component As Object)

        End Sub

        Public Overrides Sub SetValue(component As Object, value As Object)
            CType(component, JavaServerOptions).TrySetMember(MyBase.Name, value)
        End Sub

        Public Overrides Function CanResetValue(component As Object) As Boolean
            Return False
        End Function

        Public Overrides Function GetValue(component As Object) As Object
            Dim value As Object = Nothing
            CType(component, JavaServerOptions).TryGetMember(MyBase.Name, value)
            Return value
        End Function

        Public Overrides Function ShouldSerializeValue(component As Object) As Boolean
            Return False
        End Function
    End Class
#End Region
End Class
''' <summary>
''' server.properties 的對應.NET 類別(Nukkit 專用)
''' </summary>
Class NukkitServerOptions
    Implements IServerOptions

    <DisplayName("允許玩家飛行")> <DefaultValue(False)> <Category("玩家")> <Description("允許玩家在安裝添加飛行功能的 mod 前提下在生存模式下飛行。" &
                                                               vbNewLine & "允許飛行可能會使作弊者更加常見，因為此設定會使他們更容易達成目的。" &
                                                               vbNewLine & "在創造模式下本屬性不會有任何作用。" &
                                                               vbNewLine & "false - 不允許飛行。懸空超過5秒的玩家會被踢出伺服器。" &
                                                               vbNewLine & "true - 允許飛行。玩家得以使用飛行MOD飛行。")>
    Public Property Allow_Flight As Boolean = False
    <DisplayName("顯示玩家獲得成就")> <DefaultValue(True)> <Category("玩家")> <Description("玩家獲得成就時是否在伺服器中進行顯示。  " &
                                                              vbNewLine & "False - 玩家獲得成就時的提示僅自己可見，不會向其他玩家進行顯示。" &
                                                              vbNewLine & "True - 玩家獲得成就時將在其他在線玩家的聊天欄進行提示。")>
    Public Property Announce_Player_Achievements As Boolean = True
    <DisplayName("啟用自動儲存")> <DefaultValue(True)> <Category("遊戲")> <Description("自動儲存伺服器資料。" &
                                                              vbNewLine & "False - 允許自動儲存伺服器資料。" &
                                                              vbNewLine & "True - 伺服器關閉時儲存資料。")>
    Public Property Auto_Save As Boolean = True
    <DisplayName("難度")> <DefaultValue(Difficulty.Easy)> <Category("玩家")> <Description("定義伺服器的遊戲難度（例如生物對玩家造成的傷害，飢餓與中毒對玩家的影響方式等）。  " &
                                                              vbNewLine & "Peaceful - 和平" &
                                                              vbNewLine & "Easy - 簡單" &
                                                              vbNewLine & "Normal - 普通" &
                                                              vbNewLine & "Hard - 困難")>
    Public Property Difficulty As Difficulty = Difficulty.Easy
    <DisplayName("允許收集信息")> <DefaultValue(False)> <Category("技術性")> <Description("允許使用GameSpy4協議的伺服器監聽器。它被用於收集伺服器信息。")>
    Public Property Enable_Query As Boolean = False
    <DisplayName("允許遠程訪問")> <DefaultValue(False)> <Category("技術性")> <Description("是否允許遠程訪問伺服器控制台。")>
    Public Property Enable_Rcon As Boolean = False
    <DisplayName("強制遊戲模式")> <DefaultValue(False)> <Category("玩家")> <Description("強制玩家加入時為默認遊戲模式" &
                                                              vbNewLine & "False - 玩家將以退出前的遊戲模式加入" &
                                                              vbNewLine & "True - 玩家總是以默認遊戲模式加入")>
    Public Property Force_Gamemode As Boolean = False
    <DisplayName("預設遊戲模式")> <DefaultValue(Gamemode.Survival)> <Category("玩家")> <Description("定義默認遊戲模式" &
                                                              vbNewLine & "Survival - 生存模式" &
                                                              vbNewLine & "Creative - 創造模式" &
                                                              vbNewLine & "Adventure - 冒險模式")>
    Public Property Gamemode As Gamemode = Gamemode.Survival
    <DisplayName("生成器設定")> <DefaultValue("")> <Category("地圖")> <Description("本屬性只用於自訂平坦世界的生成。")>
    Public ReadOnly Property Generator_Settings As String = ""
    <DisplayName("極限模式")> <DefaultValue(False)> <Category("玩家")> <Description("一旦啟用，玩家在死後會自動被伺服器封禁（即開啟極限模式）。")>
    Public Property Hardcore As Boolean = False
    <DisplayName("地圖名稱")> <DefaultValue("world")> <Category("地圖")> <Description("""地圖名稱""的值將作為世界名稱及其資料夾名。")>
    Public ReadOnly Property Level_Name As String = "world"
    <DisplayName("地圖種子")> <DefaultValue("")> <Category("地圖")> <Description("與單人遊戲類似，為世界定義一個種子。")>
    Public ReadOnly Property Level_Seed As String = ""
    <DisplayName("地圖類型")> <DefaultValue(Bedrock_Level_Type.INFINITE)> <Category("地圖")> <Description("確定地圖所生成的類型" &
                                                              vbNewLine & "INFINITE - 標準的世界，帶有丘陵，河谷，海洋等" &
                                                              vbNewLine & "FLAT - 一個沒有特色的平坦世界，適合用於建設" &
                                                              vbNewLine & "OLD - 同無限世界，但世界大小限制為256×256，且被隱形的基岩屏障所包圍。")>
    Public ReadOnly Property Level_Type As Bedrock_Level_Type = Bedrock_Level_Type.INFINITE
    Dim _Max_Players As Integer = 20
    <DisplayName("玩家最大數量")> <DefaultValue(20)> <Category("玩家")> <Description("伺服器同時能容納的最大玩家數量。但請注意在線玩家越多，對伺服器造成的負擔也就越大。" &
                                                              vbNewLine & "伺服器的OP具有在人滿的情況下強行進入伺服器的權力，找到在伺服器根目錄下叫ops.json的文件並打開，" &
                                                              vbNewLine & "設置你要突破人數限制的OP下的bypassesPlayerLimit選項為true即可（默認值為false），" &
                                                              vbNewLine & "這意味著OP將不需要在伺服器人滿時等待玩家離開再加入，過大的數值會使客戶端顯示的玩家列表崩壞。")>
    Public Property Max_Players As Integer
        Get
            Return _Max_Players
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 2147483647 Then
                _Max_Players = value
            Else
                MsgBox("最大玩家人數只能在1~2147483647之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("MOTD")> <DefaultValue("A Minecraft PE Server")> <Category("技術性")> <Description("本屬性值是玩家客戶端的多人遊戲伺服器列表中顯示的伺服器主信息，顯示於名稱下方。" &
                                                              vbNewLine & vbTab & "MOTD 支持樣式代碼。" &
                                                              vbNewLine & vbTab & "MOTD 支持特殊符號， 比如「♥」。 然而，這些符號需要被轉換為Unicode轉義字符。")>
    Public Property Motd As String = "A Minecraft PE Server"
    <DisplayName("允許PVP")> <DefaultValue(True)> <Category("玩家")> <Description("是否允許PvP。玩家自己的箭也只有在允許PvP時才可能傷害到自己。 " &
                                                             vbNewLine & "註： 來源於玩家的間接傷害，例如熔岩，火，TNT等，還是會造成傷害。" &
                                                             vbNewLine & "True - 玩家可以互相殘殺。" &
                                                             vbNewLine & "False - 玩家無法互相造成傷害。")>
    Public Property PvP As Boolean = True
    Dim _Rcon_Port As Integer = 19132
    <DisplayName("遠程訪問埠")> <DefaultValue(19162)> <Category("技術性")> <Description("設置遠程訪問的埠號")>
    Public Property Rcon_Port As Integer
        Get
            Return _Rcon_Port
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Rcon_Port = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("遠程訪問密碼")> <DefaultValue("")> <Category("技術性")> <Description("設置遠程訪問的密碼")>
    Public Property Rcon_Password As String = ""
    <DisplayName("伺服器IP")> <DefaultValue("0.0.0.0")> <Category("技術性")> <Description("將伺服器與一個特定IP綁定。強烈建議你留空本屬性值！" &
                                                             vbNewLine & "      留空，或是填入你想讓伺服器綁定的IP。")>
    Public Property Server_Ip As String = "0.0.0.0"
    Dim _Server_Port As Integer = 19132
    <DisplayName("伺服器埠")> <DefaultValue(19132)> <Category("技術性")> <Description("改變伺服器埠號。如果伺服器通過路由器與外界連接的話，該埠必須也能夠通過路由器。 ")>
    Public Property Server_Port As Integer
        Get
            Return _Server_Port
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Server_Port = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("允許生成動物")> <DefaultValue(True)> <Category("生成")> <Description("決定動物是否可以生成。 " &
                                                             vbNewLine & "True - 動物可以生成。" &
                                                             vbNewLine & "False - 動物生成後會立即消失。")>
    Public Property Spawn_Animals As Boolean = True
    <DisplayName("允許生成怪物")> <DefaultValue(True)> <Category("生成")> <Description("決定攻擊型生物（怪物）是否可以生成。  " &
                                                             vbNewLine & "True - 可以。只要滿足條件的話怪物就會生成。" &
                                                             vbNewLine & "False - 不會有任何怪物。" &
                                                             vbNewLine & "如果Difficulty = Peaceful（和平）的話，本屬性值不會有任何影響。")>
    Public Property Spawn_Monsters As Boolean = True
    <DisplayName("出生點保護距離")> <DefaultValue(16)> <Category("技術性")> <Description("通過將該值進行(x*2)+1的運算來決定出生點的保護半徑。" &
                                                              vbNewLine & "設置為0將不會禁用出生點保護。設置為0將會保護位於出生點的1x1方塊區域，" &
                                                             vbNewLine & "設置為1將會保護以出生點為中心的3x3方塊區域。" &
                                                              vbNewLine & "設置為2會保護5x5的方塊區域，設置為3將會保護7x7的方塊區域，以此類推。 " &
                                                             vbNewLine & "這個選項只會在第一個玩家進行伺服器時生成。如果伺服器沒有設置OP，這個選項將會自動禁用。")>
    Public Property Spawn_Protection As Integer = 16
    <DisplayName("第二MOTD")> <DefaultValue("")> <Category("技術性")> <Description("本屬性值是玩家客戶端的多人遊戲伺服器列表中顯示的伺服器次信息，顯示於主信息下方。" &
                                                              vbNewLine & vbTab & "Sub-MOTD 的特性與 MOTD 相同。")>
    Public Property Sub_Motd As String = ""
    Dim _View_Distance As Integer = 10
    <DisplayName("視野距離")> <DefaultValue(10)> <Category("地圖")> <Description("設置服務端傳送給客戶端的數據量，也就是設置玩家各個方向上的區塊數量 (是以玩家為中心的半徑，不是直徑)。 " &
                                                              vbNewLine & "它決定了服務端的可視距離。 " &
                                                             vbNewLine & "默認/推薦設置為10，如果很卡的話，減少該數值。")>
    Public Property View_Distance As Integer
        Get
            Return _View_Distance
        End Get
        Set(value As Integer)
            If value >= 3 And value <= 15 Then
                _View_Distance = value
            Else
                MsgBox("渲染距離必須在3~15之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("允許白名單")> <DefaultValue(False)> <Category("玩家")> <Description("伺服器的白名單 " &
                                                            vbNewLine & "當啟用時，只有白名單上的用戶才能連接伺服器。" &
                                                            vbNewLine & "白名單主要用於私人伺服器，例如相識的朋友等。" &
                                                             vbNewLine & "True - 從 whitelist.json 文件加載白名單。" &
                                                             vbNewLine & "False - 不使用白名單。")>
    Public Property White_List As Boolean = False
    <DisplayName("正版驗證")> <DefaultValue(True)> <Category("玩家")> <Description("伺服器是否需要玩家用Xbox 帳戶連線(即正版驗證)。" &
                                                             vbNewLine & "True - 啟用。伺服器需要玩家有Xbox 帳戶才能連線。" &
                                                             vbNewLine & "False - 禁用。伺服器不需要玩家有Xbox 帳戶。")>
    Public Property Xbox_Auth As Boolean = True
    Public Sub InputOption(serverOption As IDictionary(Of String, String)) Implements IServerOptions.InputOption
        On Error Resume Next
        Allow_Flight = ToStandardBoolean(serverOption("allow-flight"))
        Announce_Player_Achievements = ToStandardBoolean(serverOption("announce-player-achievements"))
        Difficulty = [Enum].Parse(GetType(Difficulty), serverOption("difficulty"))
        Enable_Query = ToStandardBoolean(serverOption("enable-query"))
        Enable_Rcon = ToStandardBoolean(serverOption("enable-rcon"))
        Force_Gamemode = ToStandardBoolean(serverOption("force-gamemode"))
        Gamemode = [Enum].Parse(GetType(Gamemode), serverOption("gamemode"))
        _Generator_Settings = serverOption("generator-settings")
        Hardcore = ToStandardBoolean(serverOption("hardcore"))
        _Level_Name = serverOption("level-name")
        _Level_Seed = serverOption("level-seed")
        _Level_Type = [Enum].Parse(GetType(Bedrock_Level_Type), serverOption("level-type").ToUpper)
        Max_Players = serverOption("max-players")
        Motd = serverOption("motd")
        PvP = ToStandardBoolean(serverOption("pvp"))
        Rcon_Port = serverOption("rcon.port")
        Rcon_Password = serverOption("rcon.password")
        Server_Ip = serverOption("server-ip")
        Server_Port = serverOption("server-port")
        Spawn_Animals = ToStandardBoolean(serverOption("spawn-animals"))
        Spawn_Monsters = ToStandardBoolean(serverOption("spawn-monsters"))
        Spawn_Protection = Integer.Parse(serverOption("spawn-protection"))
        Sub_Motd = serverOption("sub-motd")
        View_Distance = Integer.Parse(serverOption("view-distance"))
        White_List = ToStandardBoolean(serverOption("white-list"))
        Xbox_Auth = ToStandardBoolean(serverOption("xbox-auth"))
    End Sub
    Public Sub SetValue(optionName As String, value As String) Implements IServerOptions.SetValue
        Select Case optionName
            Case "allow-flight"
                Allow_Flight = ToStandardBoolean(value)
            Case "announce-player-achievements"
                Announce_Player_Achievements = ToStandardBoolean(value)
            Case "difficulty"
                Difficulty = [Enum].Parse(GetType(Difficulty), value)
            Case "enable-query"
                Enable_Query = ToStandardBoolean(value)
            Case "enable-rcon"
                Enable_Rcon = ToStandardBoolean(value)
            Case "force-gamemode"
                Force_Gamemode = ToStandardBoolean(value)
            Case "gamemode"
                Gamemode = [Enum].Parse(GetType(Gamemode), value)
            Case "generator-settings"
                _Generator_Settings = value
            Case "hardcore"
                Hardcore = ToStandardBoolean(value)
            Case "level-name"
                _Level_Name = value
            Case "level-seed"
                _Level_Seed = value
            Case "level-type"
                _Level_Type = [Enum].Parse(GetType(Bedrock_Level_Type), value.ToUpper)
            Case "motd"
                Motd = value
            Case "pvp"
                PvP = ToStandardBoolean(value)
            Case "rcon.password"
                Rcon_Password = value
            Case "rcon.port"
                Rcon_Port = value
            Case "server-ip"
                Server_Ip = value
            Case "server-port"
                Server_Port = value
            Case "spawn-animals"
                Spawn_Animals = ToStandardBoolean(value)
            Case "spawn-monsters"
                Spawn_Monsters = ToStandardBoolean(value)
            Case "spawn-protection"
                Spawn_Protection = Integer.Parse(value)
            Case "sub-motd"
                Sub_Motd = value
            Case "view-distance"
                View_Distance = Integer.Parse(value)
            Case "white-list"
                White_List = ToStandardBoolean(value)
            Case "xbox-auth"
                Xbox_Auth = ToStandardBoolean(value)
        End Select
    End Sub
    Public Function OutputOption() As IDictionary(Of String, String) Implements IServerOptions.OutputOption
        Dim options As New Dictionary(Of String, String)
        options.Add("allow-flight", ToStandardOnOff(Allow_Flight))
        options.Add("announce-player-achievements", ToStandardOnOff(Announce_Player_Achievements))
        options.Add("difficulty", Difficulty)
        options.Add("enable-query", ToStandardOnOff(Enable_Query))
        options.Add("enable-rcon", ToStandardOnOff(Enable_Rcon))
        options.Add("force-gamemode", ToStandardOnOff(Force_Gamemode))
        options.Add("gamemode", Gamemode)
        options.Add("generator-settings", Generator_Settings)
        options.Add("hardcore", ToStandardOnOff(Hardcore))
        options.Add("level-name", Level_Name)
        options.Add("level-seed", Level_Seed)
        options.Add("level-type", Level_Type.ToString)
        options.Add("max-players", Max_Players)
        options.Add("motd", Motd)
        options.Add("pvp", ToStandardOnOff(PvP))
        options.Add("rcon.port", Rcon_Port)
        options.Add("rcon.password", Rcon_Password)
        options.Add("server-ip", Server_Ip)
        options.Add("server-port", Server_Port)
        options.Add("spawn-animals", ToStandardOnOff(Spawn_Animals))
        options.Add("spawn-monsters", ToStandardOnOff(Spawn_Monsters))
        options.Add("spawn-protection", Spawn_Protection)
        options.Add("sub-motd", Sub_Motd)
        options.Add("view-distance", View_Distance)
        options.Add("white-list", ToStandardOnOff(White_List))
        options.Add("xbox-auth", ToStandardOnOff(Xbox_Auth))
        Return options
    End Function
    Private Function ToStandardOnOff([boolean] As Boolean) As String
        Select Case [boolean]
            Case True
                Return "on"
            Case False
                Return "off"
            Case Else
                Return ""
        End Select
    End Function
    Private Function ToStandardBoolean(OnOff As String) As Boolean
        Select Case OnOff
            Case "on"
                Return True
            Case "off"
                Return False
            Case Else
                Throw New InvalidCastException()
        End Select
    End Function
End Class
''' <summary>
''' server.properties 的對應.NET 類別(Vanilla(基岩)專用)
''' </summary>
Class VanillaBedrockServerOptions
    Implements IServerOptions
    <DisplayName("最大執行緒量")> <DefaultValue(8)> <Category("伺服器")> <Description("設定伺服器能使用的執行緒數量。")>
    Public Property Max_Threads As Integer = 8
    <DisplayName("玩家閒置時間")> <DefaultValue(30UI)> <Category("玩家")> <Description("如果不為0，伺服器將在玩家的空閒時間達到設置的時間（單位為分鐘）時將玩家踢出伺服器 " &
                                                             vbNewLine & "例如：把Player_Idle_Timeout設置為3，玩家空閒時間達到3分鐘就會被踢出伺服器。")>
    Public Property Player_Idle_Timeout As UInteger = 30UI
    <DisplayName("伺服器名稱")> <DefaultValue("Dedicated Server")> <Category("伺服器")> <Description("設定顯示在伺服器列表上的名稱。")>
    Public Property Server_Name As String = "Dedicated Server"
    <DisplayName("難度")> <DefaultValue(Difficulty.Easy)> <Category("玩家")> <Description("定義伺服器的遊戲難度（例如生物對玩家造成的傷害，飢餓與中毒對玩家的影響方式等）。  " &
                                                              vbNewLine & "Peaceful - 和平" &
                                                              vbNewLine & "Easy - 簡單" &
                                                              vbNewLine & "Normal - 普通" &
                                                              vbNewLine & "Hard - 困難")>
    Public Property Difficulty As Difficulty = Difficulty.Easy
    <DisplayName("正版驗證")> <DefaultValue(True)> <Category("玩家")> <Description("伺服器是否需要玩家用Xbox 帳戶連線(即正版驗證)。" &
                                                             vbNewLine & "True - 啟用。伺服器需要玩家有Xbox 帳戶才能連線。" &
                                                             vbNewLine & "False - 禁用。伺服器不需要玩家有Xbox 帳戶。")>
    Public Property Online_Mode As Boolean = True
    <DisplayName("允許作弊")> <DefaultValue(False)> <Category("玩家")> <Description("玩家是否可以作弊(如輸入指令)。")>
    Public Property Allow_Cheats As Boolean = False
    <DisplayName("預設遊戲模式")> <DefaultValue(Gamemode.Survival)> <Category("玩家")> <Description("定義默認遊戲模式" &
                                                              vbNewLine & "Survival - 生存模式" &
                                                              vbNewLine & "Creative - 創造模式" &
                                                              vbNewLine & "Adventure - 冒險模式（僅在12w22a之後，或正式版1.3之後可用）" &
                                                              vbNewLine & "Spectator - 旁觀模式（僅在14w05a之後，或正式版1.8之後可用）")>
    Public Property Gamemode As Gamemode = Gamemode.Survival
    <DisplayName("地圖名稱")> <DefaultValue("world")> <Category("地圖")> <Description("""地圖名稱""的值將作為世界名稱及其資料夾名。")>
    Public ReadOnly Property Level_Name As String = "world"
    <DisplayName("地圖種子")> <DefaultValue("")> <Category("地圖")> <Description("與單人遊戲類似，為世界定義一個種子。")>
    Public ReadOnly Property Level_Seed As String = ""
    <DisplayName("地圖類型")> <DefaultValue(Bedrock_Level_Type.INFINITE)> <Category("地圖")> <Description("確定地圖所生成的類型" &
                                                              vbNewLine & "INFINITE - 標準的世界，帶有丘陵，河谷，海洋等" &
                                                              vbNewLine & "FLAT - 一個沒有特色的平坦世界，適合用於建設" &
                                                              vbNewLine & "OLD - 同無限世界，但世界大小限制為256×256，且被隱形的基岩屏障所包圍。")>
    Public ReadOnly Property Level_Type As Bedrock_Level_Type = Bedrock_Level_Type.INFINITE
    Dim _Max_Players As Integer = 20
    <DisplayName("玩家最大數量")> <DefaultValue(20)> <Category("玩家")> <Description("伺服器同時能容納的最大玩家數量。但請注意在線玩家越多，對伺服器造成的負擔也就越大。" &
                                                              vbNewLine & "伺服器的OP具有在人滿的情況下強行進入伺服器的權力，找到在伺服器根目錄下叫ops.json的文件並打開，" &
                                                              vbNewLine & "設置你要突破人數限制的OP下的bypassesPlayerLimit選項為true即可（默認值為false），" &
                                                              vbNewLine & "這意味著OP將不需要在伺服器人滿時等待玩家離開再加入，過大的數值會使客戶端顯示的玩家列表崩壞。")>
    Public Property Max_Players As Integer
        Get
            Return _Max_Players
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 2147483647 Then
                _Max_Players = value
            Else
                MsgBox("最大玩家人數只能在1~2147483647之間",, Application.ProductName)
            End If
        End Set
    End Property
    Dim _Server_Port As Integer = 19132
    <DisplayName("伺服器埠")> <DefaultValue(19132)> <Category("技術性")> <Description("改變伺服器埠號(IPv4)。如果伺服器通過路由器與外界連接的話，該埠必須也能夠通過路由器。 ")>
    Public Property Server_Port As Integer
        Get
            Return _Server_Port
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Server_Port = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    Dim _Server_PortV6 As Integer = 19133
    <DisplayName("伺服器埠(IPv6)")> <DefaultValue(19133)> <Category("技術性")> <Description("改變伺服器埠號(IPv6)。如果伺服器通過路由器與外界連接的話，該埠必須也能夠通過路由器。 ")>
    Public Property Server_PortV6 As Integer
        Get
            Return _Server_PortV6
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Server_PortV6 = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    Dim _View_Distance As Integer = 10
    <DisplayName("視野距離")> <DefaultValue(10)> <Category("地圖")> <Description("設置服務端傳送給客戶端的數據量，也就是設置玩家各個方向上的區塊數量 (是以玩家為中心的半徑，不是直徑)。 " &
                                                              vbNewLine & "它決定了服務端的可視距離。 " &
                                                             vbNewLine & "默認/推薦設置為10，如果很卡的話，減少該數值。")>
    Public Property View_Distance As Integer
        Get
            Return _View_Distance
        End Get
        Set(value As Integer)
            If value >= 3 And value <= 15 Then
                _View_Distance = value
            Else
                MsgBox("渲染距離必須在3~15之間",, Application.ProductName)
            End If
        End Set
    End Property
    Dim _Tick_Distance As Integer = 10
    <DisplayName("刷新距離")> <DefaultValue(10)> <Category("地圖")> <Description("伺服器能在玩家附近多少區塊內刷新遊戲刻。")>
    Public Property Tick_Distance As Integer
        Get
            Return _Tick_Distance
        End Get
        Set(value As Integer)
            If value >= 4 And value <= 12 Then
                _Tick_Distance = value
            Else
                MsgBox("區塊數必須在4~12之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("預設玩家權限等級")> <DefaultValue(Bedrock_Player_Permission_Level.Member)> <Category("玩家")> <Description("玩家初次登入時能得到的權限等級。" &
                                                                                         vbNewLine & "Visitor - 訪客" &
                                                                                         vbNewLine & "Member - 成員" &
                                                                                         vbNewLine & "Operator - 管理員")>
    Public Property Default_Player_Permission_Level As Bedrock_Player_Permission_Level = Bedrock_Player_Permission_Level.Member
    <DisplayName("強制使用材質包")> <DefaultValue(False)> <Category("玩家")> <Description("需不需要強制玩家使用伺服器的材質包。")>
    Public Property Texturepack_Required As Boolean = False
    <DisplayName("使用白名單")> <DefaultValue(False)> <Category("玩家")> <Description("伺服器的白名單 " &
                                                            vbNewLine & "當啟用時，只有白名單上的用戶才能連接伺服器。" &
                                                            vbNewLine & "白名單主要用於私人伺服器，例如相識的朋友等。" &
                                                             vbNewLine & "True - 從 whitelist.json 文件加載白名單。" &
                                                             vbNewLine & "False - 不使用白名單。")>
    Public Property White_List As Boolean = False
    Public Sub InputOption(serverOption As IDictionary(Of String, String)) Implements IServerOptions.InputOption
        On Error Resume Next
        Difficulty = [Enum].Parse(GetType(Difficulty), serverOption("difficulty"))
        Gamemode = [Enum].Parse(GetType(Gamemode), serverOption("gamemode"))
        _Level_Name = serverOption("level-name")
        _Level_Seed = serverOption("level-seed")
        Server_Name = serverOption("server-name")
        Server_PortV6 = serverOption("server-portv6")
        Online_Mode = serverOption("online-mode")
        Allow_Cheats = Boolean.Parse(serverOption("allow-cheats"))
        Player_Idle_Timeout = serverOption("player-idle-timeout")
        Max_Threads = serverOption("max-threads")
        Tick_Distance = serverOption("tick-distance")
        Select Case serverOption("default-player-permission-level").ToLower
            Case "visitor"
                Default_Player_Permission_Level = Bedrock_Player_Permission_Level.Visitor
            Case "member"
                Default_Player_Permission_Level = Bedrock_Player_Permission_Level.Member
            Case "operator"
                Default_Player_Permission_Level = Bedrock_Player_Permission_Level.Operator
        End Select
        Texturepack_Required = Boolean.Parse(serverOption("texturepack-required"))
        Select Case serverOption("level-type").ToUpper
            Case "FLAT"
                _Level_Type = Bedrock_Level_Type.FLAT
            Case "LEGACY"
                _Level_Type = Bedrock_Level_Type.OLD
            Case "DEFAULT"
                _Level_Type = Bedrock_Level_Type.INFINITE
        End Select
        Max_Players = serverOption("max-players")
        Server_Port = serverOption("server-port")
        View_Distance = Integer.Parse(serverOption("view-distance"))
        White_List = Boolean.Parse(serverOption("white-list"))
    End Sub
    Public Sub SetValue(optionName As String, value As String) Implements IServerOptions.SetValue
        Select Case optionName
            Case "server-name"
                Server_Name = value
            Case "server-portv6"
                Server_PortV6 = value
            Case "online-mode"
                Online_Mode = Boolean.Parse(value)
            Case "allow-cheats"
                Allow_Cheats = Boolean.Parse(value)
            Case "player-idle-timeout"
                Player_Idle_Timeout = value
            Case "max-threads"
                Max_Threads = value
            Case "tick-distance"
                Tick_Distance = value
            Case "default-player-permission-level"
                Select Case value.ToLower
                    Case "visitor"
                        Default_Player_Permission_Level = Bedrock_Player_Permission_Level.Visitor
                    Case "member"
                        Default_Player_Permission_Level = Bedrock_Player_Permission_Level.Member
                    Case "operator"
                        Default_Player_Permission_Level = Bedrock_Player_Permission_Level.Operator
                End Select
            Case "texturepack-required"
                Texturepack_Required = Boolean.Parse(value)
            Case "difficulty"
                Difficulty = [Enum].Parse(GetType(Difficulty), value)
            Case "gamemode"
                Gamemode = [Enum].Parse(GetType(Gamemode), value)
            Case "level-name"
                _Level_Name = value
            Case "level-seed"
                _Level_Seed = value
            Case "level-type"
                Select Case value.ToUpper
                    Case "FLAT"
                        _Level_Type = Bedrock_Level_Type.FLAT
                    Case "LEGACY"
                        _Level_Type = Bedrock_Level_Type.OLD
                    Case "DEFAULT"
                        _Level_Type = Bedrock_Level_Type.INFINITE
                End Select
            Case "server-port"
                Server_Port = value
            Case "view-distance"
                View_Distance = Integer.Parse(value)
            Case "white-list"
                White_List = Boolean.Parse(value)
        End Select
    End Sub
    Public Function OutputOption() As IDictionary(Of String, String) Implements IServerOptions.OutputOption
        Dim options As New Dictionary(Of String, String)
        options.Add("server-name", Server_Name)
        options.Add("difficulty", Difficulty)
        options.Add("gamemode", Gamemode)
        options.Add("level-name", Level_Name)
        options.Add("level-seed", Level_Seed)
        Select Case Level_Type
            Case Bedrock_Level_Type.FLAT
                options.Add("level-type", "FLAT")
            Case Bedrock_Level_Type.OLD
                options.Add("level-type", "LEGACY")
            Case Bedrock_Level_Type.INFINITE
                options.Add("level-type", "DEFAULT")
        End Select
        options.Add("max-players", Max_Players)
        options.Add("server-port", Server_Port)
        options.Add("server-portv6", Server_PortV6)
        options.Add("online-mode", Online_Mode.ToString.ToLower)
        options.Add("allow-cheats", Allow_Cheats.ToString.ToLower)
        options.Add("player-idle-timeout", Player_Idle_Timeout)
        options.Add("max-threads", Max_Threads)
        options.Add("view-distance", View_Distance)
        options.Add("tick-distance", Tick_Distance)
        options.Add("default-player-permission-level", Default_Player_Permission_Level.ToString.ToLower)
        options.Add("texturepack-required", Texturepack_Required.ToString.ToLower)
        options.Add("white-list", White_List.ToString.ToLower)
        Return options
    End Function
End Class
''' <summary>
''' server.properties 的對應.NET 類別(PocketMine 專用)
''' </summary>
Class PocketMineServerOptions
    Implements IServerOptions

    <DisplayName("啟用自動儲存")> <DefaultValue(True)> <Category("遊戲")> <Description("自動儲存伺服器資料。" &
                                                              vbNewLine & "False - 允許自動儲存伺服器資料。" &
                                                              vbNewLine & "True - 伺服器關閉時儲存資料。")>
    Public Property Auto_Save As Boolean = True
    <DisplayName("難度")> <DefaultValue(Difficulty.Easy)> <Category("玩家")> <Description("定義伺服器的遊戲難度（例如生物對玩家造成的傷害，飢餓與中毒對玩家的影響方式等）。  " &
                                                              vbNewLine & "Peaceful - 和平" &
                                                              vbNewLine & "Easy - 簡單" &
                                                              vbNewLine & "Normal - 普通" &
                                                              vbNewLine & "Hard - 困難")>
    Public Property Difficulty As Difficulty = Difficulty.Easy
    <DisplayName("允許收集信息")> <DefaultValue(False)> <Category("技術性")> <Description("允許使用GameSpy4協議的伺服器監聽器。它被用於收集伺服器信息。")>
    Public Property Enable_Query As Boolean = False
    <DisplayName("允許遠程訪問")> <DefaultValue(False)> <Category("技術性")> <Description("是否允許遠程訪問伺服器控制台。")>
    Public Property Enable_Rcon As Boolean = False
    <DisplayName("強制遊戲模式")> <DefaultValue(False)> <Category("玩家")> <Description("強制玩家加入時為默認遊戲模式" &
                                                              vbNewLine & "False - 玩家將以退出前的遊戲模式加入" &
                                                              vbNewLine & "True - 玩家總是以默認遊戲模式加入")>
    Public Property Force_Gamemode As Boolean = False
    <DisplayName("預設遊戲模式")> <DefaultValue(Gamemode.Survival)> <Category("玩家")> <Description("定義默認遊戲模式" &
                                                              vbNewLine & "Survival - 生存模式" &
                                                              vbNewLine & "Creative - 創造模式" &
                                                              vbNewLine & "Adventure - 冒險模式")>
    Public Property Gamemode As Gamemode = Gamemode.Survival
    <DisplayName("生成器設定")> <DefaultValue("")> <Category("地圖")> <Description("本屬性只用於自訂平坦世界的生成。")>
    Public ReadOnly Property Generator_Settings As String = ""
    <DisplayName("極限模式")> <DefaultValue(False)> <Category("玩家")> <Description("一旦啟用，玩家在死後會自動被伺服器封禁（即開啟極限模式）。")>
    Public Property Hardcore As Boolean = False
    <DisplayName("地圖名稱")> <DefaultValue("world")> <Category("地圖")> <Description("""地圖名稱""的值將作為世界名稱及其資料夾名。")>
    Public ReadOnly Property Level_Name As String = "world"
    <DisplayName("地圖種子")> <DefaultValue("")> <Category("地圖")> <Description("與單人遊戲類似，為世界定義一個種子。")>
    Public ReadOnly Property Level_Seed As String = ""
    <DisplayName("地圖類型")> <DefaultValue(Bedrock_Level_Type.INFINITE)> <Category("地圖")> <Description("確定地圖所生成的類型" &
                                                              vbNewLine & "INFINITE - 標準的世界，帶有丘陵，河谷，海洋等" &
                                                              vbNewLine & "FLAT - 一個沒有特色的平坦世界，適合用於建設" &
                                                              vbNewLine & "OLD - 同無限世界，但世界大小限制為256×256，且被隱形的基岩屏障所包圍。")>
    Public ReadOnly Property Level_Type As Bedrock_Level_Type = Bedrock_Level_Type.INFINITE
    Dim _Max_Players As Integer = 20
    <DisplayName("玩家最大數量")> <DefaultValue(20)> <Category("玩家")> <Description("伺服器同時能容納的最大玩家數量。但請注意在線玩家越多，對伺服器造成的負擔也就越大。" &
                                                              vbNewLine & "伺服器的OP具有在人滿的情況下強行進入伺服器的權力，找到在伺服器根目錄下叫ops.json的文件並打開，" &
                                                              vbNewLine & "設置你要突破人數限制的OP下的bypassesPlayerLimit選項為true即可（默認值為false），" &
                                                              vbNewLine & "這意味著OP將不需要在伺服器人滿時等待玩家離開再加入，過大的數值會使客戶端顯示的玩家列表崩壞。")>
    Public Property Max_Players As Integer
        Get
            Return _Max_Players
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 2147483647 Then
                _Max_Players = value
            Else
                MsgBox("最大玩家人數只能在1~2147483647之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("MOTD")> <DefaultValue("A Minecraft PE Server")> <Category("技術性")> <Description("本屬性值是玩家客戶端的多人遊戲伺服器列表中顯示的伺服器主信息，顯示於名稱下方。" &
                                                              vbNewLine & vbTab & "MOTD 支持樣式代碼。" &
                                                              vbNewLine & vbTab & "MOTD 支持特殊符號， 比如「♥」。 然而，這些符號需要被轉換為Unicode轉義字符。")>
    Public Property Motd As String = "A Minecraft PE Server"
    <DisplayName("允許PVP")> <DefaultValue(True)> <Category("玩家")> <Description("是否允許PvP。玩家自己的箭也只有在允許PvP時才可能傷害到自己。 " &
                                                             vbNewLine & "註： 來源於玩家的間接傷害，例如熔岩，火，TNT等，還是會造成傷害。" &
                                                             vbNewLine & "True - 玩家可以互相殘殺。" &
                                                             vbNewLine & "False - 玩家無法互相造成傷害。")>
    Public Property PvP As Boolean = True
    Dim _Rcon_Port As Integer = 19132
    <DisplayName("遠程訪問埠")> <DefaultValue(19132)> <Category("技術性")> <Description("設置遠程訪問的埠號")>
    Public Property Rcon_Port As Integer
        Get
            Return _Rcon_Port
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Rcon_Port = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("遠程訪問密碼")> <DefaultValue("")> <Category("技術性")> <Description("設置遠程訪問的密碼")>
    Public Property Rcon_Password As String = ""
    <DisplayName("伺服器IP")> <DefaultValue("0.0.0.0")> <Category("技術性")> <Description("將伺服器與一個特定IP綁定。強烈建議你留空本屬性值！" &
                                                             vbNewLine & "      留空，或是填入你想讓伺服器綁定的IP。")>
    Public Property Server_Ip As String = "0.0.0.0"
    Dim _Server_Port As Integer = 19132
    <DisplayName("伺服器埠")> <DefaultValue(19132)> <Category("技術性")> <Description("改變伺服器埠號。如果伺服器通過路由器與外界連接的話，該埠必須也能夠通過路由器。 ")>
    Public Property Server_Port As Integer
        Get
            Return _Server_Port
        End Get
        Set(value As Integer)
            If value >= 1 And value <= 65534 Then
                _Server_Port = value
            Else
                MsgBox("埠號只能在1~65534之間",, Application.ProductName)
            End If
        End Set
    End Property
    Public Property Spawn_Protection As Integer = 16
    Dim _View_Distance As Integer = 10
    <DisplayName("視野距離")> <DefaultValue(10)> <Category("地圖")> <Description("設置服務端傳送給客戶端的數據量，也就是設置玩家各個方向上的區塊數量 (是以玩家為中心的半徑，不是直徑)。 " &
                                                              vbNewLine & "它決定了服務端的可視距離。 " &
                                                             vbNewLine & "默認/推薦設置為10，如果很卡的話，減少該數值。")>
    Public Property View_Distance As Integer
        Get
            Return _View_Distance
        End Get
        Set(value As Integer)
            If value >= 3 And value <= 15 Then
                _View_Distance = value
            Else
                MsgBox("渲染距離必須在3~15之間",, Application.ProductName)
            End If
        End Set
    End Property
    <DisplayName("允許白名單")> <DefaultValue(False)> <Category("玩家")> <Description("伺服器的白名單 " &
                                                            vbNewLine & "當啟用時，只有白名單上的用戶才能連接伺服器。" &
                                                            vbNewLine & "白名單主要用於私人伺服器，例如相識的朋友等。" &
                                                             vbNewLine & "True - 從 whitelist.json 文件加載白名單。" &
                                                             vbNewLine & "False - 不使用白名單。")>
    Public Property White_List As Boolean = False
    <DisplayName("正版驗證")> <DefaultValue(True)> <Category("玩家")> <Description("伺服器是否需要玩家用Xbox 帳戶連線(即正版驗證)。" &
                                                             vbNewLine & "True - 啟用。伺服器需要玩家有Xbox 帳戶才能連線。" &
                                                             vbNewLine & "False - 禁用。伺服器不需要玩家有Xbox 帳戶。")>
    Public Property Xbox_Auth As Boolean = True
    Public Sub InputOption(serverOption As IDictionary(Of String, String)) Implements IServerOptions.InputOption
        On Error Resume Next
        Difficulty = [Enum].Parse(GetType(Difficulty), serverOption("difficulty"))
        Enable_Query = ToStandardBoolean(serverOption("enable-query"))
        Enable_Rcon = ToStandardBoolean(serverOption("enable-rcon"))
        Force_Gamemode = ToStandardBoolean(serverOption("force-gamemode"))
        Gamemode = [Enum].Parse(GetType(Gamemode), serverOption("gamemode"))
        _Generator_Settings = serverOption("generator-settings")
        Hardcore = ToStandardBoolean(serverOption("hardcore"))
        _Level_Name = serverOption("level-name")
        _Level_Seed = serverOption("level-seed")
        _Level_Type = [Enum].Parse(GetType(Bedrock_Level_Type), serverOption("level-type").ToUpper)
        Max_Players = serverOption("max-players")
        Motd = serverOption("motd")
        PvP = ToStandardBoolean(serverOption("pvp"))
        Rcon_Port = serverOption("rcon.port")
        Rcon_Password = serverOption("rcon.password")
        Server_Ip = serverOption("server-ip")
        Server_Port = serverOption("server-port")
        Spawn_Protection = Integer.Parse(serverOption("spawn-protection"))
        View_Distance = Integer.Parse(serverOption("view-distance"))
        White_List = ToStandardBoolean(serverOption("white-list"))
        Xbox_Auth = ToStandardBoolean(serverOption("xbox-auth"))
    End Sub
    Public Sub SetValue(optionName As String, value As String) Implements IServerOptions.SetValue
        Select Case optionName
            Case "difficulty"
                Difficulty = [Enum].Parse(GetType(Difficulty), value)
            Case "enable-query"
                Enable_Query = ToStandardBoolean(value)
            Case "enable-rcon"
                Enable_Rcon = ToStandardBoolean(value)
            Case "force-gamemode"
                Force_Gamemode = ToStandardBoolean(value)
            Case "gamemode"
                Gamemode = [Enum].Parse(GetType(Gamemode), value)
            Case "generator-settings"
                _Generator_Settings = value
            Case "hardcore"
                Hardcore = ToStandardBoolean(value)
            Case "level-name"
                _Level_Name = value
            Case "level-seed"
                _Level_Seed = value
            Case "level-type"
                _Level_Type = [Enum].Parse(GetType(Bedrock_Level_Type), value.ToUpper)
            Case "motd"
                Motd = value
            Case "pvp"
                PvP = ToStandardBoolean(value)
            Case "rcon.port"
                Rcon_Port = value
            Case "rcon.password"
                Rcon_Password = value
            Case "server-ip"
                Server_Ip = value
            Case "server-port"
                Server_Port = value
            Case "spawn-protection"
                Spawn_Protection = Integer.Parse(value)
            Case "view-distance"
                View_Distance = Integer.Parse(value)
            Case "white-list"
                White_List = ToStandardBoolean(value)
            Case "xbox-auth"
                Xbox_Auth = ToStandardBoolean(value)
        End Select
    End Sub
    Public Function OutputOption() As IDictionary(Of String, String) Implements IServerOptions.OutputOption
        Dim options As New Dictionary(Of String, String)
        options.Add("difficulty", Difficulty)
        options.Add("enable-query", ToStandardOnOff(Enable_Query))
        options.Add("enable-rcon", ToStandardOnOff(Enable_Rcon))
        options.Add("force-gamemode", ToStandardOnOff(Force_Gamemode))
        options.Add("gamemode", Gamemode)
        options.Add("generator-settings", Generator_Settings)
        options.Add("hardcore", ToStandardOnOff(Hardcore))
        options.Add("level-name", Level_Name)
        options.Add("level-seed", Level_Seed)
        options.Add("level-type", Level_Type.ToString)
        options.Add("max-players", Max_Players)
        options.Add("motd", Motd)
        options.Add("pvp", ToStandardOnOff(PvP))
        options.Add("rcon.port", Rcon_Port)
        options.Add("rcon.password", Rcon_Password)
        options.Add("server-ip", Server_Ip)
        options.Add("server-port", Server_Port)
        options.Add("spawn-protection", Spawn_Protection)
        options.Add("view-distance", View_Distance)
        options.Add("white-list", ToStandardOnOff(White_List))
        options.Add("xbox-auth", ToStandardOnOff(Xbox_Auth))
        Return options
    End Function
    Private Function ToStandardOnOff([boolean] As Boolean) As String
        Select Case [boolean]
            Case True
                Return "on"
            Case False
                Return "off"
            Case Else
                Return ""
        End Select
    End Function
    Private Function ToStandardBoolean(OnOff As String) As Boolean
        Select Case OnOff
            Case "on"
                Return True
            Case "off"
                Return False
            Case Else
                Throw New InvalidCastException()
        End Select
    End Function
End Class

