Imports ServerManager.BungeeCordHost
Public Enum BungeeSettingMode
    Remove
    Add
End Enum
Public Class BungeeCordServerStatus
    Public ReadOnly Property Server As BungeeServer
    Dim _mode As BungeeSettingMode
    Sub New(server As BungeeServer, mode As BungeeSettingMode)

        ' 設計工具需要此呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _Server = server
        _mode = mode
    End Sub
    Private Sub ServerStatus_Load(sender As Object, e As EventArgs) Handles Me.Load
        UpdateComponent()
    End Sub
    Friend Sub SetSettingMode(mode As BungeeSettingMode)
        _mode = mode
        UpdateComponent(True)
    End Sub
    Friend Sub SetAlias(AliasName As String)
        Server.ServerAlias = AliasName
        ServerAlias.Text = AliasName
        UpdateComponent(True)
    End Sub
    Private Sub UpdateComponent(Optional ExceptVersionLabel As Boolean = False)
        BeginInvoke(New Action(Sub()
                                   If _mode = BungeeSettingMode.Remove Then
                                       RestrictedCheckBox.Visible = True
                                       RestrictedCheckBox.Enabled = True
                                       RestrictedCheckBox.Checked = Server.Restricted
                                   Else
                                       RestrictedCheckBox.Visible = False
                                       RestrictedCheckBox.Enabled = False
                                   End If
                                   ServerIcon.Image = Server.Server.ServerIcon
                                   If Server.ServerAlias = "" Or Server.ServerAlias.Trim.Replace("　", "") = "" Then
                                       ServerAlias.Text = "伺服器別名：無"
                                   Else
                                       ServerAlias.Text = "伺服器別名：" & Server.ServerAlias
                                   End If
                                   ServerName.Text = Server.Server.ServerPathName
                                   If ExceptVersionLabel = False Then SetVersionLabel()
                                   Select Case _mode
                                       Case BungeeSettingMode.Remove
                                           RemoveOrAddButton.Text = "移除"
                                       Case BungeeSettingMode.Add
                                           RemoveOrAddButton.Text = "新增"
                                   End Select

                               End Sub))
    End Sub
    Friend Overloads Sub SetVersionLabel(Optional updating As Boolean = False, Optional updatingPercent As Integer = 0)
        ServerVersion.Text = "伺服器版本：" & ServerMaker.SoftwareDictionary(Server.Server.GetInternalName).ReadableName
        If updating Then
            ServerVersion.Text &= " [更新進度：" & updatingPercent & " %]"
        End If
    End Sub
    Friend Overloads Sub SetVersionLabel(addtionText As String)
        ServerVersion.Text = "伺服器版本：" & ServerMaker.SoftwareDictionary(Server.Server.GetInternalName).ReadableName
        If addtionText <> "" Then
            ServerVersion.Text &= (" " & addtionText)
        End If
    End Sub

    Private Sub RemoveButton_Click(sender As Object, e As EventArgs) Handles RemoveOrAddButton.Click
        Select Case _mode
            Case BungeeSettingMode.Remove
                CType(FindForm(), BungeeCordSetting).RemoveServer(Me)
            Case BungeeSettingMode.Add
                CType(FindForm(), BungeeCordServerChooseForm).AddServer(Me)
        End Select
    End Sub

    Private Sub RestrictedCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles RestrictedCheckBox.CheckedChanged
        Server.Restricted = RestrictedCheckBox.Checked
    End Sub
End Class
