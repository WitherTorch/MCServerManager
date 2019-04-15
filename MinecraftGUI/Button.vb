Imports System.ComponentModel

Public Class Button
    Private hover As Boolean = False
    Private _GUIEnabled As Boolean = True

    Public Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        SetGUI()
    End Sub

    ''' <summary>
    ''' 是否啟用 Minecraft GUI
    ''' </summary>
    ''' <returns></returns>
    <Browsable(True)> <DefaultValue(True)>
    Public Property GUIEnabled As Boolean
        Get
            Return _GUIEnabled
        End Get
        Set(value As Boolean)
            _GUIEnabled = value
            SetGUI()
        End Set
    End Property
    <Browsable(False)>
    Public Shadows Property Image As Image
    <Browsable(False)>
    Public Shadows Property BackgroundImageLayout As ImageLayout
    Private Sub SetGUI()
        MyBase.BackgroundImageLayout = ImageLayout.Stretch
        MyBase.FlatStyle = FlatStyle.Flat
        MyBase.ForeColor = Color.White
        MyBase.FlatAppearance.BorderSize = 0
        If _GUIEnabled Then
            If MyBase.Enabled Then
                If hover Then
                    MyBase.BackgroundImage = My.Resources.button_hover_border
                Else
                    MyBase.BackgroundImage = My.Resources.button_border
                End If
            Else
                MyBase.BackgroundImage = My.Resources.button_not_important_border
            End If
        Else
            MyBase.BackgroundImage = Nothing
        End If
    End Sub
    Private Sub Button_MouseHover(sender As Object, e As EventArgs) Handles Me.MouseHover
        hover = True
        If GUIEnabled Then SetGUI()
    End Sub
    Private Sub Button_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave
        hover = False
        If GUIEnabled Then SetGUI()
    End Sub
    <Browsable(True)>
    Public Shadows Property Enabled As Boolean
        Get
            Return MyBase.Enabled
        End Get
        Set(value As Boolean)
            MyBase.Enabled = value
            SetGUI()
        End Set
    End Property
End Class
