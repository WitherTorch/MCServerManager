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
Imports System.Threading

<Serializable>
Public Class DXListView
    Private Shared ReadOnly Format As Format = Format.B8G8R8A8_UNorm
    Public Shared ReadOnly D2PixelFormat As PixelFormat = New PixelFormat(Format, SharpDX.Direct2D1.AlphaMode.Premultiplied)
    Dim deviceContext As DeviceContext
    Dim sc As SwapChain
    Dim backBuffer As Surface
    Dim targetBitmap As Bitmap1
    Dim ItemLength As Integer = 0
    Dim temped_Items As DXListViewItem()
    Public ReadOnly Property ColumnHeaders As New List(Of DXListViewColumnHeader)
    Public ReadOnly Property Items As New List(Of DXListViewItem)
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
        linBrush = New LinearGradientBrush(deviceContext, New LinearGradientBrushProperties() With {.StartPoint = New RawVector2(0, 0), .EndPoint = New RawVector2(0, 20)}, New GradientStopCollection(deviceContext, {New GradientStop() With {.Color = SharpDXConverter.ConvertColor(Color.FromArgb(0, 210, 105)), .Position = 0.0F}, New GradientStop() With {.Color = SharpDXConverter.ConvertColor(Color.FromArgb(161, 255, 221)), .Position = 1.0F}}))
    End Sub
    Dim linBrush As LinearGradientBrush
    Private Sub ContextControl_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles ContextControl.Paint
        If ContextControl.Visible = False Then Exit Sub
        deviceContext.BeginDraw()
        deviceContext.Clear(SharpDXConverter.ConvertColor(BackColor))
        Dim CurrentDrawYCoord As Single = 0
        Dim head_X As Single = 0
        Dim startX_List As New List(Of Single)
        deviceContext.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(0, 0, Width, 20)), linBrush)
        startX_List.Add(0)
        Dim y As Integer = 0
        For Each header In ColumnHeaders
            If y = ColumnHeaders.Count - 1 Then header.Width = Me.Width - head_X - 20
            y += 1
            Dim font As Font
            If header.Font Is Nothing Then
                font = Me.Font
            Else
                font = header.Font
            End If
            Dim fColor As Color
            If header.ForeColor = Nothing Then
                fColor = ForeColor
            Else
                fColor = header.ForeColor
            End If
            DrawText(deviceContext, header.Text, font, New RectangleF(head_X, 1, header.Width, 20), fColor, DirectWrite.TextAlignment.Center)
            head_X += header.Width
            startX_List.Add(head_X)
            deviceContext.DrawLine(New RawVector2(head_X, 0), New RawVector2(head_X, 21), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)), 0.175)
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
        Dim nodrawItemLength As Integer = 0
        If temped_Items IsNot Nothing Then
            For Each item In temped_Items
                If CurrentDrawYCoord >= Me.Height Then
                    outRanged = True
                    Exit For
                ElseIf CurrentItemsYCoord < BaseYCoord Then
                    ' Do Nothing
                    CurrentItemsYCoord += 20
                    nodrawItemLength += 1
                    Continue For
                Else
                    For i As Integer = 0 To item.subItems.Count - 1
                        Dim subitem = item.subItems(i)
                        Dim font As Font
                        If subitem.Font Is Nothing Then
                            font = Me.Font
                        Else
                            font = subitem.Font
                        End If
                        Dim fColor As Color
                        If subitem.ForeColor = Nothing Then
                            fColor = ForeColor
                        Else
                            fColor = subitem.ForeColor
                        End If
                        DrawText(deviceContext, subitem.Text, font, New RectangleF(startX_List(i), CurrentDrawYCoord + 1, ColumnHeaders(i).Width - 2, 18), fColor, IIf(i < 2, DirectWrite.TextAlignment.Center, DirectWrite.TextAlignment.Leading))
                    Next
                    CurrentDrawYCoord += 20
                    CurrentItemsYCoord += 20
                    '  deviceContext.DrawLine(New RawVector2(0, CurrentDrawYCoord + 0.2), New RawVector2(Width, CurrentDrawYCoord + 0.1), New SolidColorBrush(deviceContext, SharpDXConverter.ConvertColor(Color.Black)), 0.175)
                    showItemLength += 1
                End If
            Next
        End If
        If outRanged = True Then
            VScrollBar1.Enabled = True
            VScrollBar1.Maximum = ItemLength - showItemLength
            VScrollBar1.Value = nodrawItemLength
        Else
            VScrollBar1.Enabled = False
            VScrollBar1.Value = 0
            VScrollBar1.Maximum = 0
        End If
        Try
            deviceContext.EndDraw()
        Catch ex As Exception

        End Try
        sc.Present(0, PresentFlags.None)
        GC.Collect()
    End Sub
    Overloads Sub DrawText(dc As DeviceContext, text As String, rect As RectangleF, color As Color, Optional alignment As DirectWrite.TextAlignment = DirectWrite.TextAlignment.Leading)
        Dim format = SharpDXConverter.ConvertFont(Font)
        format.TextAlignment = alignment
        format.WordWrapping = DirectWrite.WordWrapping.NoWrap
        Dim brush As New SolidColorBrush(dc, SharpDXConverter.ConvertColor(color))
        dc.DrawText(text, format, SharpDXConverter.ConvertRectangleF(rect), brush)
        Utilities.Dispose(format)
        Utilities.Dispose(brush)
    End Sub
    Overloads Sub DrawText(dc As DeviceContext, text As String, font As Font, rect As RectangleF, color As Color, Optional alignment As DirectWrite.TextAlignment = DirectWrite.TextAlignment.Leading)
        Dim format = SharpDXConverter.ConvertFont(font)
        format.TextAlignment = alignment
        format.WordWrapping = DirectWrite.WordWrapping.NoWrap
        Dim brush As New SolidColorBrush(dc, SharpDXConverter.ConvertColor(color))
        dc.DrawText(text, format, SharpDXConverter.ConvertRectangleF(rect), brush)
        Utilities.Dispose(format)
        Utilities.Dispose(brush)
    End Sub
    Private Sub ContextControl_SizeChanged(ByVal sender As Object, ByVal e As EventArgs)
        deviceContext.Target = Nothing
        Utilities.Dispose(backBuffer)
        Utilities.Dispose(targetBitmap)
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
            Me.DisplayMode = DXTextImageDisplayMode.Text
        End Sub
        Sub New(image As Image)
            Me.Image = image
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
        Dim tempItemLength = ItemLength
        ItemLength = Items.Count
        If ItemLength <> tempItemLength Then
            Erase temped_Items
            temped_Items = Items.ToArray
            InvokePaint(ContextControl, Nothing)
        End If
    End Sub
    Private Sub DXListView_Load(sender As Object, e As EventArgs) Handles Me.Load
        ItemLength = Items.Count
        InvokePaint(ContextControl, Nothing)
    End Sub
    Dim mouseDraw As Boolean = False
    Dim hoverPaint As Boolean = False
    Private Sub ContextControl_MouseWheel(sender As Object, e As MouseEventArgs) Handles ContextControl.MouseWheel
        Dim wheelTick = e.Delta / SystemInformation.MouseWheelScrollDelta
        If wheelTick > 0 Then
            VScrollBar1.Value = Math.Max(VScrollBar1.Value - wheelTick, 0)
        ElseIf wheelTick < 0 Then
            VScrollBar1.Value = Math.Min(VScrollBar1.Value - wheelTick, VScrollBar1.Maximum)
        End If
    End Sub
    Private Sub DXListView_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        ColumnHeaders.Clear()
        Items.Clear()
        Utilities.Dispose(deviceContext)
        Utilities.Dispose(sc)
        Utilities.Dispose(backBuffer)
        Utilities.Dispose(targetBitmap)
        Utilities.Dispose(linBrush)
        Try
            deviceContext.Dispose()
            sc.Dispose()
            backBuffer.Dispose()
            targetBitmap.Dispose()
            linBrush.Dispose()
        Catch ex As Exception

        End Try
        GC.Collect()
    End Sub
    Private Sub VScrollBar1_ValueChanged(sender As Object, e As EventArgs) Handles VScrollBar1.ValueChanged
        BaseYCoord = VScrollBar1.Value * 20
        InvokePaint(ContextControl, Nothing)
    End Sub
End Class
Public Enum DXTextImageDisplayMode
    Text
    Image
End Enum
