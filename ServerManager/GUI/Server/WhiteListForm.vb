Public Class WhiteListForm
    Dim sPath As String
    Dim server As Server
    Sub New(server As Server)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        sPath = server.ServerPath
        Me.server = server
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim respond = InputBox("請輸入要加入白名單的玩家ID", Application.ProductName)
        If respond.Trim <> "" Then
            PlayerBox.Items.Add(respond)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        For Each item In PlayerBox.CheckedItems
            PlayerBox.Items.Remove(item)
        Next
    End Sub

    Private Sub WhiteListForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        ShowInTaskbar = MiniState = 0
        Try
            Dim array = Newtonsoft.Json.JsonConvert.DeserializeObject(Of Newtonsoft.Json.Linq.JArray)(ReadAllText(IO.Path.Combine(sPath, "whitelist.json")))
            For Each token In array
                Dim jsonObject As Newtonsoft.Json.Linq.JObject = token
                PlayerBox.Items.Add(jsonObject.GetValue("name").ToString)
            Next
        Catch ex As Exception

        End Try
    End Sub

    Private Sub WhiteListForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim array As New Newtonsoft.Json.Linq.JArray()
        If server.ServerVersionType = Server.EServerVersionType.VanillaBedrock Then
            For Each itm In PlayerBox.Items
                Dim uuid = MinecraftUUIDProvider.GetUUID(itm.ToString)
                If uuid = Guid.Empty Then
                Else
                    Dim jsonObject As New Newtonsoft.Json.Linq.JObject
                    jsonObject.Add("uuid", uuid)
                    jsonObject.Add("name", itm.ToString)
                    array.Add(jsonObject)
                End If
            Next
            WriteAllText(IO.Path.Combine(sPath, "whitelist.json"), Newtonsoft.Json.JsonConvert.SerializeObject(array))
        Else
            For Each itm In PlayerBox.Items
                Dim jsonObject As New Newtonsoft.Json.Linq.JObject
                jsonObject.Add("ignoresPlayerLimit", False)
                jsonObject.Add("name", itm.ToString)
                array.Add(jsonObject)
            Next
            WriteAllText(IO.Path.Combine(sPath, "whitelist.json"), Newtonsoft.Json.JsonConvert.SerializeObject(array))
        End If
    End Sub
End Class