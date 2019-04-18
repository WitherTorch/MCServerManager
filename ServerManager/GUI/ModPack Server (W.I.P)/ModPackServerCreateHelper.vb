Public Class ModPackServerCreateHelper
    Dim server As ModPackServer
    Dim client As New Net.WebClient()
    Dim path As String
    Public Sub New(server As ModPackServer, serverPath As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        path = serverPath
    End Sub
    Private Sub ModPackServerCreateHelper_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
End Class