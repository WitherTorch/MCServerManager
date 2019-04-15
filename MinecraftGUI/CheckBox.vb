Imports System.ComponentModel

Public Class CheckBox
    Public Event CheckedChanged As EventHandler
    Private hover As Boolean = False
    Private _GUIEnabled As Boolean = True
    Private _checked As Boolean = False
    Private _text As String

    Public Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
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
    Public Shadows Property BackgroundImage As Image = MyBase.BackgroundImage
    <Browsable(False)>
    Public Shadows Property BackgroundImageLayout As ImageLayout = ImageLayout.Stretch
    Private Sub SetGUI()
        Label1.Text = _text
        Label1.BackColor = BackColor
        Label1.ForeColor = ForeColor
        If Enabled Then
            If _checked Then
                PictureBox1.Image = My.Resources.checkbox_checked
            Else
                PictureBox1.Image = My.Resources.checkbox_unchecked
            End If
        Else
            If _checked Then
                PictureBox1.Image = My.Resources.checkbox_dis_checked
            Else
                PictureBox1.Image = My.Resources.checkbox_dis_unchecked
            End If
        End If
    End Sub
    Public Property Checked As Boolean
        Get
            Return _checked
        End Get
        Set(value As Boolean)
            _checked = value
            SetGUI()
        End Set
    End Property

    Private Sub CheckBox_Click(sender As Object, e As EventArgs) Handles PictureBox1.MouseClick, Label1.MouseClick, Me.MouseClick
        Checked = Not Checked
        RaiseEvent CheckedChanged(sender, e)
    End Sub

    Private Sub CheckBox_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetGUI()

    End Sub

    <Browsable(True)> <Bindable(BindableSupport.Yes)> <DefaultValue("")> <EditorBrowsable(EditorBrowsableState.Always)> <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Shadows Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
            SetGUI()
        End Set
    End Property
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
