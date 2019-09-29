Public Class ServerMaker
    Friend Shared SoftwareDictionary As New Dictionary(Of String, SoftwareInfo)
    Structure SoftwareInfo
        Public ClassType As Type
        Public InternalName As String
        Public ReadableName As String
    End Structure
    Shared Function MakeServer(Of T As ServerBase)() As T
        Return Activator.CreateInstance(GetType(T))
    End Function
    ''' <summary>
    ''' 建立伺服器物件
    ''' </summary>
    ''' <param name="softwareName">伺服器軟體的內部名稱</param>
    ''' <returns></returns>
    Shared Function MakeServer(softwareName As String) As ServerBase
        Return Activator.CreateInstance(SoftwareDictionary(softwareName).ClassType)
    End Function
    ''' <summary>
    ''' 取得伺服器物件
    ''' </summary>
    ''' <param name="path">伺服器的路徑</param>
    ''' <returns></returns>
    Shared Function GetServer(path As String) As ServerBase
        Dim instance As ServerBase = Activator.CreateInstance(SoftwareDictionary(ServerBase.GetServerTypeString(path)).ClassType)
        instance.GetServer(path)
        Return instance
    End Function
    ''' <summary>
    ''' 註冊一個伺服器軟體
    ''' </summary>
    ''' <typeparam name="T">伺服器軟體的類別</typeparam>
    ''' <param name="internalName">內部名稱(必須是唯一的)</param>
    ''' <param name="readableName">顯示名稱</param>
    Shared Sub RegisterServerSoftware(Of T As ServerBase)(internalName As String, readableName As String)
        SoftwareDictionary.Add(internalName, New SoftwareInfo() With {.ClassType = GetType(T), .InternalName = internalName, .ReadableName = readableName})
    End Sub
End Class
