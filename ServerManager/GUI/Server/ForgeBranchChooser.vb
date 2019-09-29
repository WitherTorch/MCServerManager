Imports System.Xml

Public Class ForgeBranchChooser
    Const manifestListURL As String = "https://files.minecraftforge.net/maven/net/minecraftforge/forge/maven-metadata.xml"
    Dim server As ServerBase
    Dim serverPath As String
    Public Sub New(server As ServerBase, dir As String)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.server = server
        serverPath = dir
    End Sub

    Private Sub ForgeBranchChooser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Enabled = False
        Threading.Tasks.Task.Run(Sub()
                                     Try
                                         BeginInvokeIfRequired(Me, Sub() Label3.Text = server.ServerVersion)
                                         Dim client As New Net.WebClient()
                                         Dim docHtml = client.DownloadString(manifestListURL)
                                         Dim xmlDocument = New XmlDocument()
                                         xmlDocument.Load(New IO.StringReader(docHtml))
                                         Dim versionNodes = xmlDocument.SelectNodes("//metadata/versioning/versions/*")
                                         Dim versionList As New List(Of String)
                                         For Each versionNode As XmlNode In versionNodes
                                             Dim version As String = versionNode.InnerText
                                             Try
                                                 Dim mcVersion As Version = New Version(version.Split(New Char() {"-"})(0))
                                                 Dim forgeVersion As Version = New Version(version.Split(New Char() {"-"})(1))
                                                 If mcVersion.ToString = server.ServerVersion Then versionList.Add(forgeVersion.ToString)
                                             Catch ex As Exception
                                             End Try
                                         Next
                                         versionList.Reverse()
                                         BeginInvokeIfRequired(Me, Sub()
                                                                       ComboBox1.Items.AddRange(versionList.ToArray)
                                                                       ComboBox1.Enabled = True
                                                                   End Sub)
                                     Catch ex As Exception

                                     End Try
                                 End Sub)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedIndex >= 0 Then
            Dim helper As New ServerCreateHelper(server, serverPath, ComboBox1.SelectedItem.ToString)
            helper.Show()
            Close()
        End If
    End Sub
End Class