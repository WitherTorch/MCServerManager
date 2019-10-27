Imports D3DDevice = SharpDX.Direct3D11.Device
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
    Public Property ColumnHeaders As New List(Of DXListViewColumnHeader)
    Public Property Items As New List(Of DXListViewItem)
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
        AddHandler ContextControl.SizeChanged, AddressOf ContextControl_SizeChanged
    End Sub

    Private Sub ContextControl_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles ContextControl.Paint
        deviceContext.BeginDraw()
        deviceContext.Clear(SharpDXConverter.ConvertColor(BackColor))
        Dim CurrentDrawYCoord As Single = 0
        Dim head_X As Single = 0
        Dim startX_List As New List(Of Single)
        startX_List.Add(0)
        For Each header In ColumnHeaders
            DrawText(header.Text, New RectangleF(head_X, 1, header.Width, 20), header.ForeColor, DirectWrite.TextAlignment.Center)
            head_X += header.Width + 0.1
            startX_List.Add(head_X)
            deviceContext.DrawLine(New RawVector2(head_X, 0), New RawVector2(head_X, Height), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)), 0.175)
        Next
        CurrentDrawYCoord = 18
        deviceContext.DrawLine(New RawVector2(0, CurrentDrawYCoord + 3.2), New RawVector2(Width, CurrentDrawYCoord + 0.1), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)), 0.3)
        CurrentDrawYCoord += 0.3
        startX_List.RemoveAt(startX_List.Count - 1)
        For Each item In Items
            For i As Integer = 0 To item.subItems.Count - 1
                Dim subitem = item.subItems(i)
                DrawText(subitem.Text, subitem.Font, New RectangleF(startX_List(i) + 2, CurrentDrawYCoord + 2, ColumnHeaders(i).Width, 18), subitem.ForeColor)
            Next
            CurrentDrawYCoord += 20
            deviceContext.DrawLine(New RawVector2(0, CurrentDrawYCoord + 0.2), New RawVector2(Width, CurrentDrawYCoord + 0.1), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)), 0.175)
            CurrentDrawYCoord += 0.5
        Next
        deviceContext.EndDraw()
        sc.Present(0, PresentFlags.None)
    End Sub
    Overloads Sub DrawText(text As String, rect As RectangleF, color As Color, Optional alignment As DirectWrite.TextAlignment = DirectWrite.TextAlignment.Leading)
        Dim format = SharpDXConverter.ConvertFont(Font)
        format.TextAlignment = alignment
        deviceContext.DrawText(text, format, SharpDXConverter.ConvertRectangleF(rect), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(color)))
    End Sub
    Overloads Sub DrawText(text As String, font As Font, rect As RectangleF, color As Color, Optional alignment As DirectWrite.TextAlignment = DirectWrite.TextAlignment.Leading)
        Dim format = SharpDXConverter.ConvertFont(font)
        format.TextAlignment = alignment
        deviceContext.DrawText(text, format, SharpDXConverter.ConvertRectangleF(rect), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(color)))
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
    Public Structure DXListViewColumnHeader
        Dim Text As String
        Dim ForeColor As Color
        Dim Font As Font
        Dim Width As Single
        Sub New(text As String, width As Single)
            Me.Text = text
            Me.Width = width
            Me.ForeColor = Color.Black
            Me.Font = New Font(New FontFamily("微軟正黑體"), 18, FontStyle.Regular)
        End Sub
        Sub New(text As String, foreColor As Color, font As Font, width As Single)
            Me.Text = text
            Me.Width = width
            Me.ForeColor = foreColor
            Me.Font = font
        End Sub
    End Structure
    Public Structure DXListViewItem
        Dim subItems As List(Of DXListViewSubItem)
        Sub New(ParamArray subItems As DXListViewSubItem())
            Me.subItems = subItems.ToList
        End Sub
    End Structure

    Public Structure DXListViewSubItem
        Dim Text As String
        Dim ForeColor As Color
        Dim Font As Font
        Dim Image As Image
        Dim DisplayMode As DXTextImageDisplayMode
        Sub New(text As String)
            Me.Text = text
            Me.ForeColor = Color.Black
            Me.Font = New Font(New FontFamily("微軟正黑體"), 14, FontStyle.Regular)
            Me.DisplayMode = DXTextImageDisplayMode.Text
        End Sub
        Sub New(image As Image)
            Me.Image = image
            Me.ForeColor = Color.Black
            Me.Font = New Font(New FontFamily("微軟正黑體"), 14, FontStyle.Regular)
            Me.DisplayMode = DXTextImageDisplayMode.Image
        End Sub
        Sub New(text As String, foreColor As Color, font As Font)
            Me.Text = text
            Me.ForeColor = foreColor
            Me.Font = font
            Me.DisplayMode = DXTextImageDisplayMode.Text
        End Sub
        Sub New(image As Image, foreColor As Color, font As Font)
            Me.Image = image
            Me.ForeColor = foreColor
            Me.Font = font
            Me.DisplayMode = DXTextImageDisplayMode.Image
        End Sub
    End Structure
End Class
Public Enum DXTextImageDisplayMode
    Text
    Image
End Enum
