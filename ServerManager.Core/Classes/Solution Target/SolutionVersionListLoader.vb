Public Class SolutionVersionListLoader
    Private Shared SoftwareVersionListFunction As New Dictionary(Of Type, Action)
    Public Shared Sub LoadVersionList(Of T As SolutionTargetBase)()
        If SoftwareVersionListFunction.ContainsKey(GetType(T)) Then
            SoftwareVersionListFunction(GetType(T))()
        End If
    End Sub
    Public Shared Sub LoadVersionList(type As Type)
        If SoftwareVersionListFunction.ContainsKey(type) Then
            SoftwareVersionListFunction(type)()
        End If
    End Sub
    Public Shared Function GetVersions(index As Integer) As String()
        Dim instance As ServerBase = Activator.CreateInstance(SoftwareVersionListFunction.Keys(index))
        '     If ShowVanillaSnapshot Then
        '    Return instance.GetAvailableVersions({("snapshot", True.ToString)})
        '      Else
        Return instance.GetAvailableVersions()
        '    End If
    End Function
    ''' <summary>
    ''' 註冊方案目標的版本載入程式
    ''' </summary>
    ''' <typeparam name="T">方案目標的軟體類型</typeparam>
    ''' <param name="func">版本載入程式</param>
    Public Shared Sub RegisterVersionListFunction(Of T As SolutionTargetBase)(func As Action)
        If SoftwareVersionListFunction.ContainsKey(GetType(T)) Then
            SoftwareVersionListFunction(GetType(T)) = func
        Else
            SoftwareVersionListFunction.Add(GetType(T), func)
        End If
    End Sub
    ''' <summary>
    ''' 取得是否有指定方案目標的版本載入程式
    ''' </summary>
    ''' <typeparam name="T">方案目標類型</typeparam>
    Public Shared Function HasVersionListFunction(Of T As SolutionTargetBase)() As Boolean
        Return SoftwareVersionListFunction.ContainsKey(GetType(T))
    End Function
    ''' <summary>
    ''' 取得是否有指定方案目標的版本載入程式
    ''' </summary>
    ''' <param name="type">方案目標類型</param>
    Public Shared Function HasVersionListFunction(type As Type) As Boolean
        Return SoftwareVersionListFunction.ContainsKey(type)
    End Function
End Class
