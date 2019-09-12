''' <summary>
''' 實現Bukkit 插件系統必須實作的介面
''' </summary>
Public Interface IBukkit
    ''' <summary>
    ''' 載入插件列表
    ''' </summary>
    Sub LoadPlugins()
    ''' <summary>
    ''' 建立/儲存插件列表
    ''' </summary>
    Sub SavePlugins()
    ''' <summary>
    ''' 取得插件列表的記憶體陣列形式
    ''' </summary>
    ''' <returns></returns>
    Function GetPlugins() As ServerAddons()
End Interface
