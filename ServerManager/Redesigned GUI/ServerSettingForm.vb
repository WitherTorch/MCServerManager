Imports System.ComponentModel
Imports SharpDX.Direct3D11
Imports SharpDX.DXGI
Imports Device = SharpDX.Direct3D11.Device

Public Class ServerSettingForm
    Dim _server As ServerBase
    Dim navIndex As Integer = 0
#Region "DirectX Variants"
    Dim d As Device
    Dim sc As SwapChain
    Dim target As Texture2D
    Dim targetView As RenderTargetView
#End Region
    Public Sub New(ByRef server As ServerBase)

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        _server = server
        Dim scd As New SwapChainDescription() With {
        .BufferCount = 1,
        .Flags = SwapChainFlags.None,
        .IsWindowed = True,
        .ModeDescription = New ModeDescription(ClientSize.Width, ClientSize.Height, New Rational(60, 1), Format.R8G8B8A8_UNorm),
        .OutputHandle = Handle,
        .SampleDescription = New SampleDescription(1, 0),
        .SwapEffect = SwapEffect.Discard,
        .Usage = Usage.RenderTargetOutput
        }
        Try
            Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, scd, d, sc)
        Catch ex As Exception
            scd.ModeDescription.RefreshRate = New Rational(30, 1)
            Device.CreateWithSwapChain(SharpDX.Direct3D.DriverType.Hardware, DeviceCreationFlags.None, scd, d, sc)
        End Try
        target = Texture2D.FromSwapChain(Of Texture2D)(sc, 0)
        targetView = New RenderTargetView(d, target)
        d.ImmediateContext.OutputMerger.SetRenderTargets(targetView)
    End Sub

    Private Sub ServerSettingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ServerSettingFormBindings.ContainsKey(_server) Then
            Close()
        Else
            ServerSettingFormBindings.Add(_server, Me)
            If TypeOf _server Is IMemoryChange Then
                Dim memoryServer As IMemoryChange = _server
                NumericUpDown1.Value = memoryServer.MemoryMax
                NumericUpDown2.Value = memoryServer.MemoryMin
            End If
        End If
    End Sub

    Private Sub ServerSettingForm_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If TypeOf _server Is IMemoryChange Then
            Dim memoryServer As IMemoryChange = _server
            memoryServer.MemoryMax = NumericUpDown1.Value
            memoryServer.MemoryMin = NumericUpDown2.Value
        End If
        ServerSettingFormBindings.Remove(_server)
    End Sub

    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click
        navIndex -= 1
        UpdatePropertiesViewer()
        PropertyGrid1.Focus()
    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click
        navIndex += 1
        UpdatePropertiesViewer()
        PropertyGrid1.Focus()
    End Sub

    Private Sub UpdatePropertiesViewer()
        Dim optionObjects As AbstractSoftwareOptions() = _server.GetOptionObjects
        Select Case navIndex
            Case 0
                PropertyGrid1.SelectedObject = _server.GetServerProperties
                MetroButton1.Enabled = optionObjects.Count > 0
                MetroButton2.Enabled = optionObjects.Count > 0
                Label5.Text = "主設定檔(server.properties)"
            Case Is > _server.GetOptionObjects.Length
                navIndex = 0
                UpdatePropertiesViewer()
            Case Is >= 1
                PropertyGrid1.SelectedObject = optionObjects(navIndex - 1)
                MetroButton1.Enabled = True
                MetroButton2.Enabled = True
                Label5.Text = optionObjects(navIndex - 1).GetOptionsTitle
            Case Is <= -1
                navIndex = optionObjects.Length
                UpdatePropertiesViewer()
        End Select
    End Sub

    Private Sub MetroTabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles MetroTabControl1.SelectedIndexChanged
        If MetroTabControl1.SelectedIndex = 1 Then
            UpdatePropertiesViewer()
        Else
            Try
                PropertyGrid1.SelectedObject = Nothing
                MetroButton1.Enabled = False
                MetroButton2.Enabled = False
                Label5.Text = String.Empty
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ServerSettingForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        d.ImmediateContext.ClearRenderTargetView(targetView, SharpDXConverter.ConvertColor(Color.White))
        sc.Present(0, PresentFlags.None)
    End Sub

    Private Sub ServerSettingForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        d.Dispose()
        sc.Dispose()
        target.Dispose()
        targetView.Dispose()
    End Sub
End Class