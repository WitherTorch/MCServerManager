Public Class VersionListLoader
    Private Shared SoftwareVersionListFunction As New Dictionary(Of Type, Action)
    Friend Shared Sub LoadVersionList(Of T As ServerBase)()
        If SoftwareVersionListFunction.ContainsKey(GetType(T)) Then
            SoftwareVersionListFunction(GetType(T))()
        End If
    End Sub
    Friend Shared Sub LoadVersionList(type As Type)
        If SoftwareVersionListFunction.ContainsKey(type) Then
            SoftwareVersionListFunction(type)()
        End If
    End Sub
    ''' <summary>
    ''' 註冊伺服器軟體的版本載入程式
    ''' </summary>
    ''' <typeparam name="T">伺服器軟體類型</typeparam>
    ''' <param name="func">版本載入程式</param>
    Public Shared Sub RegisterVersionListFunction(Of T As ServerBase)(func As Action)
        If SoftwareVersionListFunction.ContainsKey(GetType(T)) Then
            SoftwareVersionListFunction(GetType(T)) = func
        Else
            SoftwareVersionListFunction.Add(GetType(T), func)
        End If
    End Sub
    ''' <summary>
    ''' 取得是否有指定伺服器軟體的版本載入程式
    ''' </summary>
    ''' <typeparam name="T">伺服器軟體類型</typeparam>
    Public Shared Function HasVersionListFunction(Of T As ServerBase)() As Boolean
        Return SoftwareVersionListFunction.ContainsKey(GetType(T))
    End Function
    ''' <summary>
    ''' 取得是否有指定伺服器軟體的版本載入程式
    ''' </summary>
    ''' <param name="type">伺服器軟體類型</param>
    Public Shared Function HasVersionListFunction(type As Type) As Boolean
        Return SoftwareVersionListFunction.ContainsKey(type)
    End Function
End Class
