Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Dynamic
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Converters
Imports Newtonsoft.Json.Linq
Imports YamlDotNet.Serialization
#Region "BungeeCord YAML Object"
'permissions:
'   Default:  
'  - bungeecord.command.server
'  - bungeecord.command.list
'   admin:
'  - bungeecord.command.alert
'  - bungeecord.command.end
'  - bungeecord.command.ip
'  - bungeecord.command.reload
Public Class BungeeCordPermission
    <Description("權限組的名稱")>
    Public Property Group As String
    <Description("權限組中的權限")> <Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property Permissions As String()
    Friend Sub New(group As String, perms As String())
        Me.Group = group
        Permissions = perms
    End Sub
End Class
'servers:
'   lobby:
'       motd: '&1Just another BungeeCord - Forced Host'
'       address: localhost : 25565
'       restricted: false
Public Class BungeeCordServer
    <DisplayName("伺服器名稱")> <Description("伺服器的名稱")>
    Public Property ServerName As String
    <DisplayName("MOTD")> <Description("本屬性值是玩家客戶端的多人遊戲伺服器列表中顯示的伺服器信息，顯示於名稱下方。" &
                                                              vbNewLine & vbTab & "MOTD 支持樣式代碼。" &
                                                              vbNewLine & vbTab & "MOTD 支持特殊符號， 比如「 ♥」。 然而，這些符號需要被轉換為Unicode轉義字符。" &
                                                              vbNewLine & vbTab & "如果 MOTD 超過59個字符，伺服器列表很可能會返回「通訊錯誤」。")>
    Public Property Motd As String
    <DisplayName("伺服器IP")> <Description("將伺服器與一個特定IP綁定。強烈建議你留空本屬性值！" &
                                                             vbNewLine & "留空，或是填入你想讓伺服器綁定的IP。")>
    Public Property Address As String
    <DisplayName("需要權限")> <DefaultValue(False)> <Description("玩家是否不需 ""bungeecord.server.[伺服器名稱]"" 權限即可進入伺服器")>
    Public Property Restricted As Boolean = False
    Friend Sub New(name As String, motd As String, address As String, restricted As Boolean)
        ServerName = name
        Me.Motd = motd
        Me.Address = address
        Me.Restricted = restricted
    End Sub
End Class
'listeners:
'- query_port: 25577
'   motd: '&1Another Bungee server'
'   tab_list: GLOBAL_PING
'   query_enabled: false
'   proxy_protocol: false
'   forced_hosts:
'       pvp.md-5.net: pvp
'   ping_passthrough: false
'   priorities:
'       - lobby
'   bind_local_address: true
'   host: 0.0.0.0:25577
'   max_players: 1
'   tab_size: 60
'   force_default_server: false
Public Class BungeeCordListener
    <DisplayName("遠端監聽埠")> <DefaultValue(25577)> <Description("設置監聽伺服器的埠號")>
    Public Property Query_port As Integer = 25577
    <DisplayName("MOTD")> <DefaultValue("Another Bungee Server")> <Description("本屬性值是玩家客戶端的多人遊戲伺服器列表中顯示的伺服器信息，顯示於名稱下方。" &
                                                              vbNewLine & vbTab & "MOTD 支持樣式代碼。" &
                                                              vbNewLine & vbTab & "MOTD 支持特殊符號， 比如「 ♥」。 然而，這些符號需要被轉換為Unicode轉義字符。" &
                                                              vbNewLine & vbTab & "如果 MOTD 超過59個字符，伺服器列表很可能會返回「通訊錯誤」。")>
    Public Property Motd As String = "Another Bungee Server"
    <DisplayName("[TAB]列表顯示風格")> <DefaultValue(Tab_list.GLOBAL_PING)> <Description("[Tab]列表有3個選項可供選擇。" &
                                                      vbNewLine & "GLOBAL_PING：顯示連接到BungeeCord 的所有玩家，並更新連接時間。" &
                                                      vbNewLine & "GLOBAL：如上所述，但沒有更新連接時間。" &
                                                      vbNewLine & "SERVER：僅顯示伺服器上的玩家。")>
    Public Property Tab_list As Tab_list = Tab_list.GLOBAL_PING
    <DisplayName("允許遠端監聽")> <DefaultValue(False)> <Description("允許使用GameSpy4協議的伺服器監聽器。它被用於收集伺服器信息。")>
    Public Property Query_enabled As Boolean = False
    <DisplayName("啟用HAProxy")> <DefaultValue(False)> <Description("這個選項允許伺服器支援 HAProxy PROXY 協定。" &
                 vbNewLine & "大多數使用者不須啟用這個。")>
    Public Property Proxy_protocol As Boolean = False
    <DisplayName("直連伺服器列表")> <Description("直連伺服器列表（無須重導向至預設伺服器）")>
    Public ReadOnly Property Forced_hosts As Dictionary(Of String, String) = New Dictionary(Of String, String)()
    <DisplayName("ping-passthrough")> <DefaultValue(False)>
    Public Property Ping_passthrough As Boolean = False
    <DisplayName("優先伺服器列表")> <Description("伺服器將嘗試把玩家連結至""優先伺服器列表""中設置的伺服器")> <Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property Priorities As String() = {}
    <DisplayName("綁定IP位址")> <DefaultValue(True)> <Description("Bungee用於連接伺服器的地址是否會明確設置為BungeeCord正在偵聽的地址。 " &
                 vbNewLine & "除非您的系統具有多個IP地址，否則這沒有用處。")>
    Public Property Bind_local_address As Boolean = True
    <DisplayName("主機IP")> <DefaultValue("0.0.0.0")> <Description("監聽器將託管的IP和Port。 建議使用""0.0.0.0""監聽所有IP。")>
    Public Property Host As String = "0.0.0.0"
    <DisplayName("最大玩家數(表面)")> <DefaultValue(1)> <Description("設定顯示在Minecraft 多人遊戲選單上的玩家上限。")>
    Public Property Max_players As Integer = 1
    <DisplayName("[TAB]列表大小")> <DefaultValue(60)> <Description("[Tab]列表的大小。")>
    Public Property Tab_size As Integer = 60
    <DisplayName("強制連結預設伺服器(大廳)")> <DefaultValue(False)> <Description("如果為true， 則在加入伺服器時， 播放器將始終連接到默認伺服器。 " &
                                    vbNewLine & "如果為false，則播放器將加入他們上次連接的伺服器。 " &
                                    vbNewLine & "注意：“"直連伺服器列表”"不會覆蓋它。 要使"“直連伺服器列表”"設置生效，請將其設置為false。")>
    Public Property Force_default_server As Boolean = False
End Class
Public Enum Tab_list
    GLOBAL_PING
    [GLOBAL]
    SERVER
End Enum
#End Region
''' <summary>
''' config.yml 的對應.NET 類別
''' </summary>
Public Class BungeeCordOptions
    Dim path As String
    <DisplayName("inject-commands")>
    Public Property Inject_commands As Boolean = False
    <DisplayName("Forge 支援")> <DefaultValue(False)> <Category("玩家")> <Description("是否允許Forge玩家進入。")>
    Public Property Forge_support As Boolean = False
    <DisplayName("玩家數量限制")> <DefaultValue(-1)> <Category("玩家")> <Description("BungeeCord 同時能容納的最大玩家數量。 " &
                                                     vbNewLine & "如果設成小於等於0的話，將不限制玩家數量")>
    Public Property Player_limit As Integer = -1
    <DisplayName("權限組")> <Category("伺服器")> <Description("BungeeCord 的權限組")> <[ReadOnly](True)>
    Public ReadOnly Property Permissions As BungeeCordPermission() = {
        New BungeeCordPermission("default", {"bungeecord.command.server", "bungeecord.command.list"}),
        New BungeeCordPermission("admin", {"bungeecord.command.alert", "bungeecord.command.end", "bungeecord.command.ip", "bungeecord.command.reload"})
    }
    <DisplayName("最大延遲時間")> <DefaultValue(30000)> <Category("伺服器")> <Description("在關閉所有連接之前，BungeeCord 應該多久沒有回應。")>
    Public Property Timeout As Integer = 30000
    <DisplayName("記錄指令")> <DefaultValue(False)> <Category("伺服器")> <Description("BungeeCord 是否紀錄玩家使用的指令。")>
    Public Property Log_commands As Boolean = False
    <DisplayName("正版驗證")> <DefaultValue(True)> <Category("玩家")> <Description("是否開啟在線驗證。" &
                                                              vbNewLine & "伺服器會與 Minecraft 的帳戶資料庫對比檢查連入玩家。" &
                                                             vbNewLine & "如果你的伺服器並未與 Internet 連接，則將這個值設為 false ，" &
                                                              vbNewLine & "然而這樣的話破壞者也能夠使用任意假帳戶登錄伺服器。" &
                                                               vbNewLine & "如果 Minecraft.net 伺服器下線，那麼開啟在線驗證的伺服器會因為無法驗證玩家身份而拒絕所有玩家加入。")>
    Public Property Online_mode As Boolean = True
    <DisplayName("禁用指令")> <DefaultValue(New String() {})> <Category("伺服器")> <Description("禁用的指令")> <Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", GetType(UITypeEditor))>
    Public Property Disabled_commands As String() = {}
    <DisplayName("伺服器")> <Category("伺服器")> <Description("BungeeCord 內的伺服器")>
    Friend ReadOnly Property Servers As List(Of BungeeCordServer) = New List(Of BungeeCordServer)
    <DisplayName("監聽器")> <Category("伺服器")> <Description("BungeeCord 的監聽器")>
    Friend ReadOnly Property Listeners As List(Of BungeeCordListener) = New List(Of BungeeCordListener)
    <DisplayName("IP轉發")> <DefaultValue(False)> <Category("玩家")> <Description("描述：是否啟用IP（將玩家真正的IP轉發給Bukkit，而不是代理IP）" &
                                                         vbNewLine & "和UUID轉發（將玩家的UUID轉發給Bukkit，而不是離線ID雜湊）" &
                                                         vbNewLine & "如果有開正版驗證，建議設為 true")>
    Public Property Ip_forward As Boolean = False
    <DisplayName("封包壓縮閥值")> <DefaultValue(256L)> <Category("技術性")> <Description("默認會允許n-1字節的資料包正常發送, 如果資料包為 n 字節或更大時會進行壓縮。" &
                                                              vbNewLine & "所以，更低的數值會使得更多的資料包被壓縮，但是如果被壓縮的資料包字節太小將會得不償失。" &
                                                              vbNewLine & "-1 - 永久禁用資料包壓縮" &
                                                              vbNewLine & "0 - 壓縮全部資料包")>
    Public Property Network_compression_threshold As Long = 256
    <DisplayName("禁用VPN")> <DefaultValue(False)> <Category("技術性")> <Description("如果伺服器發送的和Mojang的驗證伺服器的ISP/AS不一樣,玩家將會被踢出 " &
                                                             vbNewLine & "True - 伺服器將會禁止玩家使用VPN或代理" &
                                                             vbNewLine & "False - 伺服器將不會禁止玩家使用VPN或代理")>
    Public Property Prevent_proxy_connections As Boolean = False
    <DisplayName("玩家權限組")> <DefaultValue(False)> <Category("伺服器")> <Description("設定玩家的權限組（未設定的就是""default"" 權限組）")>
    Public Property Groups As Dictionary(Of String, String()) = New Dictionary(Of String, String())()
    <DisplayName("連線嘗試間隔")> <DefaultValue(4000)> <Category("伺服器")> <Description("在最近的連接嘗試之後允許客戶端再次連接的時間，" &
                                                        vbNewLine & "以防止DoS攻擊（以毫秒為單位）")>
    Public Property Connection_throttle As Long = 4000
    <DisplayName("統計數據編號")> <Category("技術性")> <Description("這是一個隨機生成的UUID，用於提供統計數據給MCStats。")>
    Public ReadOnly Property Stats As String = Guid.NewGuid.ToString("D")
    <DisplayName("連線嘗試次數")> <DefaultValue(3)> <Category("伺服器")>
    Public Property Connection_throttle_limit As Integer = 3
    <DisplayName("紀錄Ping")> <DefaultValue(True)> <Category("伺服器")>
    Public Property Log_pings As Boolean = True

    Friend Shared Function LoadOptions(filepath As String) As BungeeCordOptions
        Dim bungeeOption As New BungeeCordOptions
        If IO.File.Exists(filepath) Then
            With bungeeOption
                Dim reader As New IO.StreamReader(New IO.FileStream(filepath, IO.FileMode.Open, IO.FileAccess.Read), System.Text.Encoding.UTF8)
                Dim deserializer = New DeserializerBuilder().Build()
                Dim yamlObject = deserializer.Deserialize(reader)
                Dim _serializer = New SerializerBuilder().JsonCompatible().Build()
                Dim json = _serializer.Serialize(yamlObject)
                Dim jsonObject = JsonConvert.DeserializeObject(Of JObject)(json)
                On Error Resume Next
                .Inject_commands = GetBoolean(jsonObject.GetValue("inject_commands").ToString())
                .Forge_support = GetBoolean(jsonObject.GetValue("forge_support").ToString())
                .Player_limit = CInt(jsonObject.GetValue("player_limit").ToString())
                Dim _permissions As New List(Of BungeeCordPermission)
                For Each child As JProperty In jsonObject.GetValue("permissions").Children
                    Dim permissionValues As New List(Of String)
                    For Each value In child.Value.ToArray
                        permissionValues.Add(value.ToString)
                    Next
                    _permissions.Add(New BungeeCordPermission(child.Name, permissionValues.ToArray))
                Next
                ._Permissions = _permissions.ToArray
                .Timeout = CInt(jsonObject.GetValue("timeout").ToString())
                .Log_commands = GetBoolean(jsonObject.GetValue("log_commands").ToString())
                .Online_mode = GetBoolean(jsonObject.GetValue("online_mode").ToString())
                Dim commands As New List(Of String)
                For Each value In jsonObject.GetValue("disabled_commands").ToArray
                    commands.Add(value.ToString)
                Next
                .Disabled_commands = commands.ToArray
                For Each child As JProperty In jsonObject.GetValue("servers").Children
                    Dim server As New BungeeCordServer(child.Name, "", "", False)
                    For Each value As JProperty In child.Value.Children
                        Select Case value.Name.ToLower
                            Case "motd"
                                server.Motd = value.Value
                            Case "address"
                                server.Address = value.Value
                            Case "restricted"
                                server.Restricted = GetBoolean(value.Value)
                        End Select
                    Next
                    .Servers.Add(server)
                Next
                For Each child As JObject In jsonObject.GetValue("listeners").ToArray
                    Dim listener As New BungeeCordListener
                    For Each value As JProperty In child.Children
                        Select Case value.Name.ToLower
                            Case "query_port"
                                listener.Query_port = CInt(value.Value)
                            Case "motd"
                                listener.Motd = value.Value
                            Case "tab_list"
                                Select Case value.Value.ToString.ToUpper
                                    Case "GLOBAL_PING"
                                        listener.Tab_list = Tab_list.GLOBAL_PING
                                    Case "GLOBAL"
                                        listener.Tab_list = Tab_list.GLOBAL
                                    Case "SERVER"
                                        listener.Tab_list = Tab_list.SERVER
                                End Select
                            Case "query_enabled"
                                listener.Query_enabled = GetBoolean(value.Value)
                            Case "proxy_protocol"
                                listener.Proxy_protocol = GetBoolean(value.Value)
                            Case "forced_hosts"
                                For Each hashPair As JProperty In value.Value.ToArray
                                    listener.Forced_hosts.Add(hashPair.Name, hashPair.Value.ToString)
                                Next
                            Case "ping_passthrough"
                                listener.Ping_passthrough = GetBoolean(value.Value)
                            Case "priorities"
                                Dim priorityList As New List(Of String)
                                For Each hashPair As JValue In value.Value.ToArray
                                    priorityList.Add(hashPair.Value)
                                Next
                                listener.Priorities = priorityList.ToArray
                            Case "bind_local_address"
                                listener.Bind_local_address = GetBoolean(value.Value)
                            Case "host"
                                listener.Host = value.Value
                            Case "max_players"
                                listener.Max_players = CInt(value.Value)
                            Case "tab_size"
                                listener.Tab_size = CInt(value.Value)
                            Case "force_default_server"
                                listener.Force_default_server = GetBoolean(value.Value)
                        End Select
                    Next
                    .Listeners.Add(listener)
                Next
                .Ip_forward = GetBoolean(jsonObject.GetValue("Ip_forward").ToString)
                .Network_compression_threshold = CInt(jsonObject.GetValue("network_compression_threshold").ToString)
                .Prevent_proxy_connections = GetBoolean(jsonObject.GetValue("prevent_proxy_connections").ToString)
                For Each child As JProperty In jsonObject.GetValue("groups").Children
                    Dim permissionList As New List(Of String)
                    For Each value As JValue In child.Value
                        permissionList.Add(value.Value)
                    Next
                    .Groups.Add(child.Name, permissionList.ToArray)
                Next
                .Connection_throttle = CInt(jsonObject.GetValue("connection_throttle").ToString)
                ._Stats = jsonObject.GetValue("stats").ToString
                .Connection_throttle_limit = CInt(jsonObject.GetValue("connection_throttle_limit").ToString)
                .Log_pings = GetBoolean(jsonObject.GetValue("log_pings").ToString)

                .path = filepath
                reader.Close()
            End With
            Return bungeeOption
        Else
            Return CreateOptionsWithDefaultSetting(filepath)
        End If

    End Function
    Friend Sub SaveOption()
        Dim jsonObject As New JObject()
        jsonObject.Add("inject_commands", Inject_commands)
        jsonObject.Add("forge_support", Forge_support)
        jsonObject.Add("player_limit", Player_limit)
        Dim jsonPermissions As New JObject
        For Each permission In Permissions
            jsonPermissions.Add(permission.Group, New JArray(permission.Permissions.ToArray))
        Next
        jsonObject.Add("permissions", jsonPermissions)
        jsonObject.Add("timeout", Timeout)
        jsonObject.Add("log_commands", Log_commands)
        jsonObject.Add("online_mode", Online_mode)
        jsonObject.Add("disabled_commands", New JArray(Disabled_commands.ToArray))
        Dim jsonServers As New JObject
        For Each server In Servers
            Dim jsonServer As New JObject
            jsonServer.Add("motd", server.Motd)
            jsonServer.Add("address", server.Address)
            jsonServer.Add("restricted", server.Restricted)
            jsonServers.Add(server.ServerName, jsonServer)
        Next
        jsonObject.Add("servers", jsonServers)
        Dim jsonListeners As New JArray
        For Each listener In Listeners
            Dim jsonListener As New JObject
            jsonListener.Add("query_port", listener.Query_port)
            jsonListener.Add("motd", listener.Motd)
            jsonListener.Add("tab_list", listener.Tab_list.ToString)
            jsonListener.Add("query_enabled", listener.Query_enabled)
            jsonListener.Add("proxy_protocol", listener.Proxy_protocol)
            Dim jsonHosts As New JObject
            For Each host In listener.Forced_hosts
                jsonHosts.Add(host.Key, host.Value)
            Next
            jsonListener.Add("forced_hosts", jsonHosts)
            jsonListener.Add("ping_passthrough", listener.Ping_passthrough)
            jsonListener.Add("priorities", New JArray(listener.Priorities))
            jsonListener.Add("bind_local_address", listener.Bind_local_address)
            jsonListener.Add("host", listener.Host)
            jsonListener.Add("max_players", listener.Max_players)
            jsonListener.Add("tab_size", listener.Tab_size)
            jsonListener.Add("force_default_server", listener.Force_default_server)
            jsonListeners.Add(jsonListener)
        Next
        jsonObject.Add("listeners", jsonListeners)
        jsonObject.Add("ip_forward", Ip_forward)
        jsonObject.Add("network_compression_threshold", Network_compression_threshold)
        jsonObject.Add("prevent_proxy_connections", Prevent_proxy_connections)
        Dim jsonGroup As New JObject
        For Each group In Groups
            jsonGroup.Add(group.Key, New JArray(group.Value))
        Next
        jsonObject.Add("groups", jsonGroup)
        jsonObject.Add("connection_throttle", Connection_throttle)
        jsonObject.Add("stats", Stats)
        jsonObject.Add("connection_throttle_limit", Connection_throttle_limit)
        jsonObject.Add("log_pings", Log_pings)
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
    Friend Shared Function CreateOptionsWithDefaultSetting(path As String) As BungeeCordOptions
        Dim op As New BungeeCordOptions
        op.path = path
        Return op
    End Function
End Class
