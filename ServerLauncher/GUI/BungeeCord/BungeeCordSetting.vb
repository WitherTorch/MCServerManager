
Public Class BungeeCordSetting
    Friend host As BungeeCordHost
    Dim chooseForm As BungeeCordServerChooseForm = New BungeeCordServerChooseForm(Me)

    Sub New(host As BungeeCordHost)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        Me.host = host
    End Sub
    Friend Sub RemoveServer(serverStatus As BungeeCordServerStatus)
        host.Servers.Remove(serverStatus.Server)
        ServerListPanel.Controls.Remove(serverStatus)
    End Sub

    Private Sub BungeeCordSetting_Load(sender As Object, e As EventArgs) Handles Me.Load
        For Each server In host.Servers
            Dim index = ServerListPanel.RowStyles.Count
            ServerListPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize))
            ServerListPanel.Controls.Add(New BungeeCordServerStatus(server, BungeeSettingMode.Remove), 0, index)
        Next
        For Each listener In host.BungeeOptions.Listeners
            Dim page As New TabPage("監聽器 " & host.BungeeOptions.Listeners.IndexOf(listener))
            Dim grid As New PropertyGrid() With {.Dock = DockStyle.Fill}
            page.Controls.Add(grid)
            grid.SelectedObject = listener
            ListenerPropertyTab.TabPages.Add(page)
        Next
        BungeeSettingGrid.SelectedObject = host.BungeeOptions
    End Sub

    Private Sub AddServerButton_Click(sender As Object, e As EventArgs) Handles AddServerButton.Click
        If IsNothing(chooseForm) = False Then
            If chooseForm.IsDisposed = False Then
                chooseForm.Show()
            Else
                chooseForm = New BungeeCordServerChooseForm(Me)
                chooseForm.Show()
            End If
        Else
            chooseForm = New BungeeCordServerChooseForm(Me)
            chooseForm.Show()
        End If
    End Sub

    Private Sub AddListenerButton_Click(sender As Object, e As EventArgs) Handles AddListenerButton.Click
        Dim listener As New BungeeCordListener()
        host.BungeeOptions.Listeners.Add(listener)
        Dim page As New TabPage("監聽器 " & host.BungeeOptions.Listeners.IndexOf(listener))
        Dim grid As New PropertyGrid() With {.Dock = DockStyle.Fill}
        page.Controls.Add(grid)
        grid.SelectedObject = listener
        ListenerPropertyTab.TabPages.Add(page)
        ListenerPropertyTab.SelectedTab = page
    End Sub

    Private Sub RemoveListenerButton_Click(sender As Object, e As EventArgs) Handles RemoveListenerButton.Click
        If ListenerPropertyTab.SelectedIndex <> -1 Then
            Dim page As TabPage = ListenerPropertyTab.SelectedTab
            Dim grid As PropertyGrid = page.Controls(0)
            Dim listener As BungeeCordListener = grid.SelectedObject
            grid.SelectedObject = Nothing
            host.BungeeOptions.Listeners.Remove(listener)
            ListenerPropertyTab.TabPages.Remove(page)
        End If
    End Sub

    Private Sub BungeeCordSetting_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        host.SaveSolution()
    End Sub
End Class