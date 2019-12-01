Public Class SolutionTargetBase
#Region "Properties"
    Dim _SolutionVersion As String
    ''' <summary>
    ''' 方案的目標整合器版本
    ''' </summary>
    ''' <returns></returns>
    Public Property SolutionVersion As String
        Get
            Return _SolutionVersion
        End Get
        Protected Set(value As String)
            _SolutionVersion = value
        End Set
    End Property
    Dim _SolutionPath As String
    ''' <summary>
    ''' 方案的資料夾路徑
    ''' </summary>
    ''' <returns></returns>
    Public Property SolutionPath As String
        Get
            Return _SolutionPath
        End Get
        Set(value As String)
            _SolutionPath = value
        End Set
    End Property
    Dim _SolutionPathName As String
    ''' <summary>
    ''' 方案的資料夾名稱
    ''' </summary>
    ''' <returns></returns>
    Public Property SolutionPathName As String
        Get
            Return _SolutionPathName
        End Get
        Protected Set(value As String)
            _SolutionPathName = value
        End Set
    End Property
#End Region

End Class
