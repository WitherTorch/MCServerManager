''' <summary>
''' 實現Forge 模組系統必須實作的介面
''' </summary>
Public Interface IForge
    ''' <summary>
    ''' 載入模組列表
    ''' </summary>
    Sub LoadMods()
    ''' <summary>
    ''' 建立/儲存模組列表
    ''' </summary>
    Sub SaveMods()
    ''' <summary>
    ''' 取得模組列表的記憶體陣列形式
    ''' </summary>
    ''' <returns></returns>
    Function GetMods() As ServerAddons()
End Interface
