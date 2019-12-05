Public Class SolutionMaker
    Public Shared SoftwareDictionary As New Dictionary(Of String, SoftwareInfo)
    Shared Function MakeSolution(Of T As SolutionTargetBase)() As T
        Return Activator.CreateInstance(GetType(T))
    End Function
    ''' <summary>
    ''' 建立方案物件
    ''' </summary>
    ''' <param name="softwareName">方案目標軟體的內部名稱</param>
    ''' <returns></returns>
    Shared Function MakeSolution(softwareName As String) As SolutionTargetBase
        Return Activator.CreateInstance(SoftwareDictionary(softwareName).ClassType)
    End Function
    ''' <summary>
    ''' 取得方案物件
    ''' </summary>
    ''' <param name="path">方案路徑</param>
    ''' <returns></returns>
    Shared Function GetSolution(path As String) As SolutionTargetBase
        Dim instance As SolutionTargetBase = Nothing
        Dim softwareName As String = SolutionTargetBase.GetServerTypeString(path)
        If softwareName IsNot Nothing Then
            For Each software In SoftwareDictionary
                If software.Key.ToLower = softwareName.ToLower Then
                    instance = Activator.CreateInstance(software.Value.ClassType)
                    Exit For
                End If
            Next
            instance.GetSolution(path)
            Return instance
        Else
            Throw New NullReferenceException("方案物件不存在!")
        End If
    End Function
    ''' <summary>
    ''' 註冊一個整合方案目標
    ''' </summary>
    ''' <typeparam name="T">方案目標軟體的類別</typeparam>
    ''' <param name="internalName">內部名稱(必須是唯一的)</param>
    ''' <param name="readableName">顯示名稱</param>
    Shared Sub RegisterSolutionTarget(Of T As SolutionTargetBase)(internalName As String, readableName As String)
        SoftwareDictionary.Add(internalName, New SoftwareInfo() With {.ClassType = GetType(T), .InternalName = internalName, .ReadableName = readableName})
    End Sub
End Class
