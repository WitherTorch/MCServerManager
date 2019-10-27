Public Class Form1
    Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("訊息類型", 120))
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("時間", 80))
        DxListView1.ColumnHeaders.Add(New DXListView.DXListViewColumnHeader("訊息", 400))
        DxListView1.Items.Add(New DXListView.DXListViewItem(New DXListView.DXListViewSubItem("訊息"), New DXListView.DXListViewSubItem("00:00"), New DXListView.DXListViewSubItem("Starting Minecraft Server at '0.0.0.0:25565' ...")))
        DxListView1.Items.Add(New DXListView.DXListViewItem(New DXListView.DXListViewSubItem("訊息"), New DXListView.DXListViewSubItem("00:20"), New DXListView.DXListViewSubItem("Reading server properties ...")))
        DxListView1.Items.Add(New DXListView.DXListViewItem(New DXListView.DXListViewSubItem("訊息"), New DXListView.DXListViewSubItem("00:21"), New DXListView.DXListViewSubItem("Generating 'world'")))
        DxListView1.Items.Add(New DXListView.DXListViewItem(New DXListView.DXListViewSubItem("訊息"), New DXListView.DXListViewSubItem("00:35"), New DXListView.DXListViewSubItem("OK, hello")))
    End Sub
End Class