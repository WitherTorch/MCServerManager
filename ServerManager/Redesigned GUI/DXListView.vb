﻿Imports D3DDevice = SharpDX.Direct3D11.Device
Imports DXGIDevice = SharpDX.DXGI.Device
Imports D2DDevice = SharpDX.Direct2D1.Device
Imports SharpDX.DXGI
Imports SharpDX.Direct2D1
Imports SharpDX.Direct3D
Imports SharpDX.Direct3D11
Imports DeviceContext = SharpDX.Direct2D1.DeviceContext
Imports SharpDX
Imports SharpDX.Mathematics.Interop

Public Class DXListView
    Private Shared ReadOnly Format As Format = Format.B8G8R8A8_UNorm
    Public Shared ReadOnly D2PixelFormat As PixelFormat = New PixelFormat(Format, SharpDX.Direct2D1.AlphaMode.Premultiplied)
    Private Shared BitmapProps1 As BitmapProperties1 = New BitmapProperties1(D2PixelFormat, 96, 96, BitmapOptions.Target)
    Dim deviceContext As DeviceContext
    Dim otherDeviceContext As DeviceContext
    Dim sc As SwapChain
    Dim backBuffer As Surface
    Dim targetBitmap As Bitmap1
    Dim dcBrush As Brush
    Public Property Columns As ListView.ColumnHeaderCollection

    Public Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()

        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        InitDeviceContext()
    End Sub

    Private Sub InitDeviceContext()
        Dim d3DDevice As D3DDevice = New D3DDevice(DriverType.Hardware, DeviceCreationFlags.BgraSupport)
        Dim dxgiDevice As DXGIDevice = d3DDevice.QueryInterface(Of SharpDX.Direct3D11.Device1)().QueryInterface(Of DXGIDevice)()
        Dim d2DDevice As D2DDevice = New D2DDevice(dxgiDevice)
        Me.deviceContext = New DeviceContext(d2DDevice, DeviceContextOptions.None)
        Me.otherDeviceContext = New DeviceContext(d2DDevice, DeviceContextOptions.None)
        Dim swapChainDesc As SwapChainDescription = New SwapChainDescription() With {
                .BufferCount = 1,
                .Usage = Usage.RenderTargetOutput,
                .OutputHandle = ContextControl.Handle,
                .IsWindowed = True,
                .ModeDescription = New ModeDescription(0, 0, New Rational(60, 1), Format),
                .SampleDescription = New SampleDescription(1, 0),
                .SwapEffect = SwapEffect.Discard
            }
        sc = New SwapChain(dxgiDevice.GetParent(Of Adapter)().GetParent(Of SharpDX.DXGI.Factory)(), d3DDevice, swapChainDesc)
        Me.backBuffer = Surface.FromSwapChain(sc, 0)
        Me.targetBitmap = New Bitmap1(Me.deviceContext, backBuffer)
        Me.deviceContext.Target = targetBitmap
        dcBrush = New SolidColorBrush(Me.deviceContext, New SharpDX.Mathematics.Interop.RawColor4(255, 255, 255, 0))
        AddHandler ContextControl.SizeChanged, AddressOf ContextControl_SizeChanged
    End Sub

    Private Sub ContextControl_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles ContextControl.Paint
        deviceContext.BeginDraw()
        deviceContext.Clear(SharpDXConverter.ConvertColor(Color.White))
        deviceContext.DrawLine(New RawVector2(100, 0), New RawVector2(100.5, Me.Height), SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.Black), deviceContext), 0.5!)
        deviceContext.DrawLine(New RawVector2(150, 0), New RawVector2(150.5, Me.Height), SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.Black), deviceContext), 0.5!)
        deviceContext.DrawLine(New RawVector2(400, 0), New RawVector2(400.5, Me.Height), SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.Black), deviceContext), 0.5!)
        For i As Integer = 1 To Me.Height \ 24
            If i * 24 <= Me.Height Then
                deviceContext.DrawText("Hi " & i, SharpDXConverter.ConvertFont(Font), New RawRectangleF(1.5, (i - 1) * 24 + 1.5, 60, (i - 1) * 24 + 61.5), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)))
                deviceContext.DrawLine(New RawVector2(0, i * 24 + 0.5), New RawVector2(Me.Width, i * 24 + 0.5), SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.Black), deviceContext), 0.5!)
            Else
                Exit For
            End If
        Next
        deviceContext.EndDraw()
        sc.Present(0, PresentFlags.None)
    End Sub
    Sub DrawGrids()

    End Sub
    Private Sub ContextControl_SizeChanged(ByVal sender As Object, ByVal e As EventArgs)
        deviceContext.Target = Nothing
        backBuffer.Dispose()
        targetBitmap.Dispose()
        sc.ResizeBuffers(1, 0, 0, Format, SwapChainFlags.None)
        backBuffer = Surface.FromSwapChain(sc, 0)
        targetBitmap = New Bitmap1(deviceContext, backBuffer)
        deviceContext.Target = targetBitmap
    End Sub
End Class
