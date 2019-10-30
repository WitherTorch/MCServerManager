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
<Serializable>
Public Class DXListView
    Private Shared ReadOnly Format As Format = Format.B8G8R8A8_UNorm
    Public Shared ReadOnly D2PixelFormat As PixelFormat = New PixelFormat(Format, SharpDX.Direct2D1.AlphaMode.Premultiplied)
    Private Shared BitmapProps1 As BitmapProperties1 = New BitmapProperties1(D2PixelFormat, 96, 96, BitmapOptions.Target)
    Dim deviceContext As DeviceContext
    Dim otherDeviceContext As DeviceContext
    Dim sc As SwapChain
    Dim backBuffer As Surface
    Dim targetBitmap As Bitmap1
    Dim ItemLength As Integer = 0
    Dim DrawnYCoord As Integer = 0
    Public ReadOnly Property IsRollingToEnd As Boolean
    Public Property ColumnHeaders As New List(Of DXListViewColumnHeader)
    Public Property Items As New List(Of DXListViewItem)
    Dim BaseYCoord As Single = 0
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
        deviceContext.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(0, 0, Width, 20)), New LinearGradientBrush(deviceContext, New LinearGradientBrushProperties() With {.StartPoint = New RawVector2(0, 0), .EndPoint = New RawVector2(0, 20)}, New GradientStopCollection(deviceContext, {New GradientStop() With {.Color = SharpDXConverter.ConvertColor(Color.FromArgb(0, 210, 105)), .Position = 0.0F}, New GradientStop() With {.Color = SharpDXConverter.ConvertColor(Color.FromArgb(161, 255, 221)), .Position = 1.0F}})))
        startX_List.Add(0)
        For Each header In ColumnHeaders
            DrawText(header.Text, New RectangleF(head_X, 1, header.Width, 20), header.ForeColor, DirectWrite.TextAlignment.Center)
            head_X += header.Width + 0.1
            startX_List.Add(head_X)
            deviceContext.DrawLine(New RawVector2(head_X, 0), New RawVector2(head_X, Height), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)), 0.175)
        Next
        CurrentDrawYCoord = 18
        deviceContext.DrawLine(New RawVector2(0, CurrentDrawYCoord + 1.4), New RawVector2(Width, CurrentDrawYCoord + 1.4), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)), 0.3)
        CurrentDrawYCoord += 1.4
        startX_List.RemoveAt(startX_List.Count - 1)
        Dim outRanged As Boolean = False
        Dim showItemLength As Integer = 0
        Dim CurrentItemsYCoord As Integer = 0
        If BaseYCoord > 0 Then
            outRanged = True
        End If
        For Each item In Items
            If CurrentDrawYCoord >= Me.Height Then
                outRanged = True
                Exit For
            ElseIf CurrentItemsYCoord < BaseYCoord Then
                ' Do Nothing
                CurrentItemsYCoord += 20
            Else
                For i As Integer = 0 To item.subItems.Count - 1
                    Dim subitem = item.subItems(i)
                    DrawText(subitem.Text, subitem.Font, New RectangleF(startX_List(i) + 2, CurrentDrawYCoord + 1, ColumnHeaders(i).Width - 2, 18), subitem.ForeColor, IIf(i < 2, DirectWrite.TextAlignment.Center, DirectWrite.TextAlignment.Leading))
                Next
                CurrentDrawYCoord += 20
                CurrentItemsYCoord += 20
                deviceContext.DrawLine(New RawVector2(0, CurrentDrawYCoord + 0.2), New RawVector2(Width, CurrentDrawYCoord + 0.1), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)), 0.175)
                showItemLength += 1
            End If
        Next
        If outRanged = True Then
            deviceContext.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(Width - 20, 0, 20, Height)), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.FromArgb(246, 246, 246))))
            deviceContext.DrawLine(New RawVector2(Width - 20, 0), New RawVector2(Width - 20, Height), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(SystemColors.ControlDarkDark)), 0.3)
            Dim pathGeo_up As New PathGeometry(deviceContext.Factory)
            Dim sink_up As GeometrySink = pathGeo_up.Open()
            sink_up.BeginFigure(New RawVector2(Width - 7, 10), FigureBegin.Filled)
            sink_up.AddLine(New RawVector2(Width - 15, 10))
            sink_up.AddLine(New RawVector2(Width - 11, 4))
            sink_up.EndFigure(FigureEnd.Closed)
            sink_up.Close()
            deviceContext.FillGeometry(pathGeo_up, New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(SystemColors.ControlDark)))
            Dim pathGeo_down As New PathGeometry(deviceContext.Factory)
            Dim sink_down As GeometrySink = pathGeo_down.Open()
            sink_down.BeginFigure(New RawVector2(Width - 7, Height - 11.5), FigureBegin.Filled)
            sink_down.AddLine(New RawVector2(Width - 15, Height - 11.5))
            sink_down.AddLine(New RawVector2(Width - 11, Height - 5.5))
            sink_down.EndFigure(FigureEnd.Closed)
            sink_down.Close()
            deviceContext.FillGeometry(pathGeo_down, New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(SystemColors.ControlDark)))
            Dim methods = GetType(DeviceContext).GetMethods()
            Dim FillRoundedRectangle = methods.Single(Function(method As Reflection.MethodInfo)
                                                          Return method.Name = "FillRoundedRectangle" _
                                                           AndAlso method.GetParameters()(0).ParameterType.IsByRef
                                                      End Function)
            If hoverPaint Then
                FillRoundedRectangle.Invoke(deviceContext, New Object() {New RoundedRectangle() With {.RadiusX = 4, .RadiusY = 4, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(Width - 16.5, CDbl(ClientSize.Height - 34) * Math.Min(BaseYCoord / 20 / ItemLength, 1) + 17, 12, CDbl(ClientSize.Height - 34) * Math.Min(showItemLength / ItemLength, 1)))}, New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(SystemColors.ControlDarkDark))})
            Else
                FillRoundedRectangle.Invoke(deviceContext, New Object() {New RoundedRectangle() With {.RadiusX = 4, .RadiusY = 4, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(Width - 16.5, CDbl(ClientSize.Height - 34) * Math.Min(BaseYCoord / 20 / ItemLength, 1) + 17, 12, CDbl(ClientSize.Height - 34) * Math.Min(showItemLength / ItemLength, 1)))}, New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(SystemColors.ControlDark))})
            End If
        End If
        deviceContext.EndDraw()
        DrawnYCoord = CurrentDrawYCoord
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
    Private Sub ContextControl_SizeChanged(ByVal sender As Object, ByVal e As EventArgs)
        deviceContext.Target = Nothing
        backBuffer.Dispose()
        targetBitmap.Dispose()
        sc.ResizeBuffers(1, 0, 0, Format, SwapChainFlags.None)
        backBuffer = Surface.FromSwapChain(sc, 0)
        targetBitmap = New Bitmap1(deviceContext, backBuffer)
        deviceContext.Target = targetBitmap
        ContextControl_Paint(Me, Nothing)
    End Sub
    <Serializable>
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
    <Serializable>
    Public Structure DXListViewItem
        Dim subItems As List(Of DXListViewSubItem)
        Sub New(ParamArray subItems As DXListViewSubItem())
            Me.subItems = subItems.ToList
        End Sub
    End Structure

    <Serializable>
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
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ItemLength = Items.Count
    End Sub
    Private Sub DXListView_Load(sender As Object, e As EventArgs) Handles Me.Load
        ItemLength = Items.Count
        InvokePaint(ContextControl, Nothing)
    End Sub
    Dim mouseDraw As Boolean = False
    Dim hoverPaint As Boolean = False
    Private Sub ContextControl_MouseDown(sender As Object, e As MouseEventArgs) Handles ContextControl.MouseDown
        Dim m_Pos = ContextControl.PointToClient(MousePosition)
        If e.Button = MouseButtons.Left AndAlso m_Pos.X > Width - 17 AndAlso m_Pos.Y > 17 AndAlso m_Pos.X < Width - 4 AndAlso m_Pos.Y < Height - 7 Then
            mouseDraw = True
        End If
    End Sub
    Private Sub ContextControl_MouseUp(sender As Object, e As MouseEventArgs) Handles ContextControl.MouseUp
        If e.Button = MouseButtons.Left Then
            If mouseDraw Then mouseDraw = False
        End If
    End Sub
    Private Sub ContextControl_MouseMove(sender As Object, e As MouseEventArgs) Handles ContextControl.MouseMove, MyBase.MouseMove
        Dim m_Pos = ContextControl.PointToClient(MousePosition)
        If m_Pos.X > Width - 17 AndAlso m_Pos.Y > 17 AndAlso m_Pos.X < Width - 4 AndAlso m_Pos.Y < Height - 7 Then
            If hoverPaint <> True Then
                hoverPaint = True
                InvokePaint(sender, Nothing)
            End If
        Else
            If hoverPaint <> False Then
                hoverPaint = False
                InvokePaint(sender, Nothing)
            End If
        End If
    End Sub
    Private Sub ContextControl_MouseWheel(sender As Object, e As MouseEventArgs) Handles ContextControl.MouseWheel
        If Height <= ItemLength * 20 + 18 AndAlso Math.Abs(Height - DrawnYCoord) < 18 Then
            BaseYCoord -= e.Delta / 5
            BaseYCoord = Math.Max(BaseYCoord, 0)
        End If
        InvokePaint(sender, Nothing)
    End Sub
End Class
Public Enum DXTextImageDisplayMode
    Text
    Image
End Enum
