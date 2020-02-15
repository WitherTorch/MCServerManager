Imports System.Diagnostics
Imports SharpDX.Mathematics.Interop
Imports D3DDevice = SharpDX.Direct3D11.Device
Imports DXGIDevice = SharpDX.DXGI.Device
Imports D2DDevice = SharpDX.Direct2D1.Device
Imports SharpDX.DXGI
Imports SharpDX.Direct2D1
Imports SharpDX.Direct3D
Imports SharpDX.Direct3D11
Imports DeviceContext = SharpDX.Direct2D1.DeviceContext
Imports SharpDX
Imports System.Runtime.InteropServices

Public Class DXManager
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(ByVal nLeftRect As Integer, ByVal nTopRect As Integer, ByVal nRightRect As Integer, ByVal nBottomRect As Integer, ByVal nWidthEllipse As Integer, ByVal nHeightEllipse As Integer) As IntPtr
    End Function
#Region "DirectX Variants"
    Private Shared ReadOnly Format As Format = Format.B8G8R8A8_UNorm
    Public Shared ReadOnly D2PixelFormat As PixelFormat = New PixelFormat(Format, SharpDX.Direct2D1.AlphaMode.Premultiplied)
    Dim deviceContext As DeviceContext
    Dim sc As SwapChain
    Dim backBuffer As Surface
    Dim targetBitmap As Bitmap1
    Dim d3DDevice As D3DDevice
    Dim dxgiDevice As DXGIDevice
    Dim d2DDevice As D2DDevice
#End Region
#Region "Event Variants"
    Dim hasTopMessages As Boolean = False
    Dim minButtonState As ControlButtonStatus = ControlButtonStatus.Unchecked
    Dim maxButtonState As ControlButtonStatus = ControlButtonStatus.Unchecked
    Dim closeButtonState As ControlButtonStatus = ControlButtonStatus.Unchecked
    Dim isEdgeDragging As Boolean = False
    Dim CurrentCursorPosition As Point
    Dim dragTop As Boolean = False
    Dim dragLeft As Boolean = False
    Dim dragRight As Boolean = False
    Dim dragBottom As Boolean = False
    Dim hoveredMenuItemIndex As Integer = -1
    Dim checkedMenuItemIndex As Integer = 0
    Dim MenuItems As New List(Of DXMenuItem)
    Dim isClickedMenuItem As Boolean = False
    Dim isShowInnerWindow As Boolean = True
    Dim UICPUvalue As Integer = -1
    Dim UIRAMvalue As Integer = -1
    Dim UIVirtualRAMvalue As Integer = -1
    Dim UINetworkValue As Integer = -1
    Dim CPUvalue As Integer = -1
    Dim RAMvalue As Integer = -1
    Dim VirtualRAMvalue As Integer = -1
    Dim networkValue As Integer = -1
    Dim searcher As New Management.ManagementObjectSearcher(New Management.ObjectQuery("SELECT * FROM CIM_OperatingSystem"))
    Dim PCStatusCheckingTimer As New Timer() With {.Enabled = False, .Interval = 10}
    Dim requestFlags As FormEventFlags = FormEventFlags.Empty
    Dim VersionLoadTabIndex As Integer = 0
#End Region
#Region "Drawing Variants"
    Dim AppIcon As Bitmap
#End Region
    Dim minBtnRect As RectangleF
    Dim maxBtnRect As RectangleF
    Dim closeBtnRect As RectangleF
    Dim VersionLoadTabRects As Rectangle() = New Rectangle() {New Rectangle(400, 45, 100, 25), New Rectangle(500, 45, 100, 25), New Rectangle(600, 45, 100, 25)}
    Const RegionSize As Integer = 4
    Dim innerWindowRect As RectangleF
#Region "Enums"
    Enum ControlButtonStatus
        Unchecked = 0
        Entered = 1
        Pressed = 2
    End Enum
    <Flags>
    Enum FormEventFlags
        Empty = 0
        RefreshLayer1 = 1
        RefreshLayer2 = 2
        RefreshLayer3 = 4
        ShadowLayer = 8
        MouseMove = 16
        RefreshAll = RefreshLayer1 Or FormEventFlags.RefreshLayer2 Or FormEventFlags.RefreshLayer3
    End Enum
#End Region
#Region "Classes"
    Class DXMenuItem
        Implements IDisposable
        Public Property Icon As Bitmap1
        Public Property Text As String
        Public Property RenderFunc As Func(Of Boolean, Bitmap1)

#Region "IDisposable Support"
        Private disposedValue As Boolean ' 偵測多餘的呼叫

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not disposedValue Then
                If disposing Then
                    ' TODO: 處置 Managed 狀態 (Managed 物件)。
                End If
                Utilities.Dispose(Icon)
                ' TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的 Finalize()。
                ' TODO: 將大型欄位設為 null。
            End If
            disposedValue = True
        End Sub

        ' TODO: 只有當上方的 Dispose(disposing As Boolean) 具有要釋放 Unmanaged 資源的程式碼時，才覆寫 Finalize()。
        'Protected Overrides Sub Finalize()
        '    ' 請勿變更這個程式碼。請將清除程式碼放在上方的 Dispose(disposing As Boolean) 中。
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' Visual Basic 加入這個程式碼的目的，在於能正確地實作可處置的模式。
        Public Sub Dispose() Implements IDisposable.Dispose
            ' 請勿變更這個程式碼。請將清除程式碼放在上方的 Dispose(disposing As Boolean) 中。
            Dispose(True)
            ' TODO: 覆寫上列 Finalize() 時，取消下行的註解狀態。
            ' GC.SuppressFinalize(Me)
        End Sub
#End Region
    End Class
#End Region
    Sub New()

        ' 設計工具需要此呼叫。
        InitializeComponent()
        ' 在 InitializeComponent() 呼叫之後加入所有初始設定。
        InitDeviceContext()
        AppIcon = SharpDXConverter.ConvertBitmap(My.Resources.IconTemplate, d2DDevice)
        MenuItems.Add(New DXMenuItem() With {.Icon = SharpDXConverter.ConvertBitmap(My.Resources.home, d2DDevice), .Text = "主頁", .RenderFunc = AddressOf RenderMainPage})
        Dim serverBMPDC As New DeviceContext(d2DDevice, DeviceContextOptions.None)
        Dim serverBitmap As New Bitmap1(serverBMPDC, New Size2(40, 40), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
        Dim lineBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.White), serverBMPDC)
        Dim lineStrokeStyle As New StrokeStyle(d2DDevice.Factory, New StrokeStyleProperties() With {.StartCap = CapStyle.Round, .EndCap = CapStyle.Round})
        serverBMPDC.Target = serverBitmap
        serverBMPDC.BeginDraw()
        serverBMPDC.DrawLine(New RawVector2(5, 10), New RawVector2(19, 3), lineBrush, 3.5, lineStrokeStyle)
        serverBMPDC.DrawLine(New RawVector2(19, 3), New RawVector2(35, 10), lineBrush, 3.5, lineStrokeStyle)
        serverBMPDC.DrawLine(New RawVector2(35, 10), New RawVector2(35, 29), lineBrush, 3.5, lineStrokeStyle)
        serverBMPDC.DrawLine(New RawVector2(35, 29), New RawVector2(19, 36), lineBrush, 3.5, lineStrokeStyle)
        serverBMPDC.DrawLine(New RawVector2(19, 36), New RawVector2(5, 29), lineBrush, 3.5, lineStrokeStyle)
        serverBMPDC.DrawLine(New RawVector2(5, 29), New RawVector2(5, 10), lineBrush, 3.5, lineStrokeStyle)

        serverBMPDC.DrawLine(New RawVector2(6, 11), New RawVector2(19, 18), lineBrush, 3.5, lineStrokeStyle)
        serverBMPDC.DrawLine(New RawVector2(19, 18), New RawVector2(34, 11), lineBrush, 3.5, lineStrokeStyle)
        serverBMPDC.DrawLine(New RawVector2(19, 18), New RawVector2(19, 35), lineBrush, 3.5, lineStrokeStyle)
        serverBMPDC.EndDraw()
        MenuItems.Add(New DXMenuItem() With {.Icon = serverBitmap, .Text = "伺服器列表", .RenderFunc = AddressOf RenderServerListPage})
        Utilities.Dispose(serverBMPDC)
        Utilities.Dispose(lineBrush)
        Utilities.Dispose(lineStrokeStyle)
        MenuItems.Add(New DXMenuItem() With {.Icon = SharpDXConverter.ConvertBitmap(My.Resources.modServerListICO, d2DDevice), .Text = "主頁", .RenderFunc = AddressOf RenderModpackServerListPage})
        minBtnRect = New RectangleF(Width - 43, 5, 10, 10)
        maxBtnRect = New RectangleF(Width - 29, 5, 10, 10)
        closeBtnRect = New RectangleF(Width - 15, 5, 10, 10)
        innerWindowRect = New RectangleF(40, 37, Width - 43, Height - 40)
        requestFlags = requestFlags Or FormEventFlags.RefreshAll
        Timer1.Enabled = True
        OnPageChanged(0)
    End Sub
    ''' <summary>
    ''' 設置並啟動 DirectX SwapChain
    ''' </summary>
    Private Sub InitDeviceContext()
        d3DDevice = New D3DDevice(DriverType.Hardware, DeviceCreationFlags.BgraSupport)
        dxgiDevice = d3DDevice.QueryInterface(Of SharpDX.Direct3D11.Device1)().QueryInterface(Of DXGIDevice)()
        d2DDevice = New D2DDevice(dxgiDevice)
        Me.deviceContext = New DeviceContext(d2DDevice, DeviceContextOptions.None)
        Dim swapChainDesc As SwapChainDescription = New SwapChainDescription() With {
                .BufferCount = 1,
                .Usage = Usage.RenderTargetOutput,
                .OutputHandle = RenderControl1.Handle,
                .IsWindowed = True,
                .ModeDescription = New ModeDescription(0, 0, New Rational(60, 1), Format),
                .SampleDescription = New SampleDescription(1, 0),
                .SwapEffect = SwapEffect.Discard
            }
        sc = New SwapChain(dxgiDevice.GetParent(Of Adapter)().GetParent(Of SharpDX.DXGI.Factory)(), d3DDevice, swapChainDesc)
        Me.backBuffer = Surface.FromSwapChain(sc, 0)
        Me.targetBitmap = New Bitmap1(Me.deviceContext, backBuffer)
        Me.deviceContext.Target = targetBitmap
    End Sub
    Public Function getNetworkUtilization(networkCard As String) As Double
        Dim bandwidthCounter As New PerformanceCounter("Network Interface", "Current Bandwidth", networkCard)
        bandwidthCounter.NextValue()
        Dim bandwidth As Single = bandwidthCounter.NextValue()
        Dim dataSentCounter As New PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCard)
        Dim dataReceivedCounter As New PerformanceCounter("Network Interface", "Bytes Received/sec", networkCard)
        Dim sendSum As Single = 0
        Dim receiveSum As Single = 0
        For i As Integer = 1 To 10
            sendSum += dataSentCounter.NextValue()
            receiveSum += dataReceivedCounter.NextValue()
        Next
        Dim utilization As Double = (8 * (sendSum + receiveSum)) / (bandwidth * 10) * 100
        Return utilization
    End Function
    Public Sub getPCStatusData()
        Static waitTime As SByte = 0
        Static thread1 As Threading.Thread, thread2 As Threading.Thread, thread3 As Threading.Thread
        requestFlags = requestFlags Or FormEventFlags.RefreshLayer3
        If waitTime = 0 Then
            If thread1 IsNot Nothing AndAlso thread1.IsAlive Then thread1.Abort()
            thread1 = New Threading.Thread(Sub()
                                               Try
                                                   CPUvalue = CPUPerformanceCounter.NextValue
                                               Catch ex As Exception

                                               End Try
                                           End Sub)
            thread1.IsBackground = True
            thread1.Start()
            If thread2 IsNot Nothing AndAlso thread2.IsAlive Then thread2.Abort()
            thread2 = New Threading.Thread(Sub()
                                               For Each item In searcher.Get()
                                                   Try
                                                       RAMvalue = 100 - item("FreePhysicalMemory") / item("TotalVisibleMemorySize") * 100
                                                   Catch ex As Exception
                                                       Continue For
                                                   End Try
                                                   Try
                                                       VirtualRAMvalue = 100 - item("FreeVirtualMemory") / item("TotalVirtualMemorySize") * 100
                                                   Catch ex As Exception
                                                       Continue For
                                                   End Try
                                                   Exit For
                                               Next
                                           End Sub)
            thread2.IsBackground = True
            thread2.Start()
            If thread3 IsNot Nothing AndAlso thread3.IsAlive Then thread3.Abort()
            thread3 = New Threading.Thread(Sub()
                                               Try
                                                   Dim category As New PerformanceCounterCategory("Network Interface")
                                                   Dim names As String() = category.GetInstanceNames()
                                                   Dim totalValue As Double = 0
                                                   For Each name As String In names
                                                       totalValue += getNetworkUtilization(name)
                                                   Next
                                                   networkValue = totalValue / names.Count
                                               Catch ex As Exception

                                               End Try
                                           End Sub)
            thread3.IsBackground = True
            thread3.Start()
            waitTime += 1
        Else
            If CPUvalue - UICPUvalue <> 0 Then
                UICPUvalue += (CPUvalue - UICPUvalue) / (15 - waitTime)
            End If
            If RAMvalue - UIRAMvalue <> 0 Then
                UIRAMvalue += (RAMvalue - UIRAMvalue) / (15 - waitTime)
            End If
            If VirtualRAMvalue - UIVirtualRAMvalue <> 0 Then
                UIVirtualRAMvalue += (VirtualRAMvalue - UIVirtualRAMvalue) / (15 - waitTime)
            End If
            If networkValue - UINetworkValue <> 0 Then
                UINetworkValue += (networkValue - UINetworkValue) / (15 - waitTime)
            End If
            If waitTime >= 35 Then
                waitTime = 0
            Else
                waitTime += 1
            End If
        End If
    End Sub
    Protected Overrides Sub OnClosed(e As EventArgs)
        MyBase.OnClosed(e)
        Utilities.Dispose(deviceContext)
        Utilities.Dispose(sc)
        Utilities.Dispose(d3DDevice)
        Utilities.Dispose(d2DDevice)
        Utilities.Dispose(dxgiDevice)
        Utilities.Dispose(backBuffer)
        Utilities.Dispose(targetBitmap)
        Utilities.Dispose(AppIcon)
        If IsNothing(Layer1DC) = False Then Utilities.Dispose(Layer1DC)
        If IsNothing(Layer1Bitmap) = False Then Utilities.Dispose(Layer1Bitmap)
    End Sub
    ''' <summary>
    ''' 主繪圖程式
    ''' </summary>
    Public Sub XPaint(ByRef flags As FormEventFlags, Optional args As Object = Nothing)
        If deviceContext Is Nothing Then Exit Sub
        deviceContext.BeginDraw()
        Select Case WindowState
            Case FormWindowState.Maximized
                deviceContext.Clear(SharpDXConverter.ConvertColor(Color.White))
            Case FormWindowState.Normal
                deviceContext.Clear(SharpDXConverter.ConvertColor(Color.White))
            Case FormWindowState.Minimized
                deviceContext.EndDraw()
                sc.Present(0, PresentFlags.None)
                Return
        End Select
        If flags.HasFlag(FormEventFlags.RefreshLayer1) Then
            Layer1Paint(True)
        Else
            Layer1Paint(False)
        End If
        If flags.HasFlag(FormEventFlags.RefreshLayer2) Then
            Layer2Paint(True)
        Else
            Layer2Paint(False)
        End If
        Dim Layer3Bitmap As Bitmap1
        If flags.HasFlag(FormEventFlags.RefreshLayer3) Then
            Layer3Bitmap = Layer3Paint(True)
        Else
            Layer3Bitmap = Layer3Paint(False)
        End If
        If Layer1Bitmap IsNot Nothing Then deviceContext.DrawBitmap(Layer1Bitmap, 1.0F, BitmapInterpolationMode.Linear)
        If Layer2Bitmap IsNot Nothing Then deviceContext.DrawBitmap(Layer2Bitmap, 1.0F, BitmapInterpolationMode.Linear)
        If Layer3Bitmap IsNot Nothing Then deviceContext.DrawBitmap(Layer3Bitmap, SharpDXConverter.ConvertRectangleF(innerWindowRect), 1.0F, BitmapInterpolationMode.Linear)
        deviceContext.EndDraw()
        sc.Present(0, PresentFlags.None)
        flags = FormEventFlags.Empty
    End Sub
#Region "分層繪圖"
    Dim Layer1Bitmap As Bitmap1 = Nothing
    Dim Layer1DC As DeviceContext = Nothing
    Dim Layer2Bitmap As Bitmap1 = Nothing
    Dim Layer2DC As DeviceContext = Nothing
    Private Sub Layer1Paint(forceUpdate As Boolean)
        If IsNothing(Layer1Bitmap) OrElse forceUpdate Then
            If IsNothing(Layer1DC) = False Then Utilities.Dispose(Layer1DC)
            If IsNothing(Layer1Bitmap) = False Then Utilities.Dispose(Layer1Bitmap)
            Layer1DC = New DeviceContext(d2DDevice, DeviceContextOptions.None)
            Layer1Bitmap = New Bitmap1(Layer1DC, New Size2(RenderControl1.Width, RenderControl1.Height), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
            Layer1DC.Target = Layer1Bitmap
            Layer1DC.BeginDraw()
            Dim backBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.White), Layer1DC)
            Dim menuBackBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.FromArgb(64, 64, 64)), Layer1DC)
            Layer1DC.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(0, 36, 36, Height - 36)), menuBackBrush)
            Layer1DC.DrawBitmap(AppIcon, SharpDXConverter.ConvertRectangleF(New RectangleF(0, 0, 36, 36)), 1.0F, BitmapInterpolationMode.Linear)
#Region "繪製最小化圖示"
            Dim MinICODC As New DeviceContext(d2DDevice, DeviceContextOptions.None)
            Dim MinICOBitmap As New Bitmap1(MinICODC, New Size2(10, 10), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
            Dim MinICOBrush As SolidColorBrush
            Try
                Select Case minButtonState
                    Case ControlButtonStatus.Unchecked
                        MinICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), MinICODC)
                    Case ControlButtonStatus.Entered
                        MinICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.FromArgb(0, 197, 99)), MinICODC)
                    Case ControlButtonStatus.Pressed
                        MinICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.FromArgb(0, 177, 89)), MinICODC)
                    Case Else
                        MinICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), MinICODC)
                End Select
            Catch ex As Exception
                MinICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), MinICODC)
            End Try
            Dim MinICOStrokeStyle As New StrokeStyle(d2DDevice.Factory, New StrokeStyleProperties() With {.StartCap = CapStyle.Round, .EndCap = CapStyle.Round})
            MinICODC.Target = MinICOBitmap
            MinICODC.BeginDraw()
            MinICODC.DrawLine(New RawVector2(0.5, 9.5), New RawVector2(9.5, 9.5), MinICOBrush, 1.5, MinICOStrokeStyle)
            MinICODC.EndDraw()
            Layer1DC.DrawBitmap(MinICOBitmap, SharpDXConverter.ConvertRectangleF(minBtnRect), 1.0F, BitmapInterpolationMode.Linear)
            Utilities.Dispose(MinICOBitmap)
            Utilities.Dispose(MinICOBrush)
            Utilities.Dispose(MinICODC)
            Utilities.Dispose(MinICOStrokeStyle)
#End Region
#Region "繪製最大化圖示"
            Dim MaxICODC As New DeviceContext(d2DDevice, DeviceContextOptions.None)
            Dim MaxICOBitmap As New Bitmap1(MaxICODC, New Size2(10, 10), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
            Dim MaxICOBrush As SolidColorBrush
            Try
                Select Case maxButtonState
                    Case ControlButtonStatus.Unchecked
                        MaxICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), MaxICODC)
                    Case ControlButtonStatus.Entered
                        MaxICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.FromArgb(0, 197, 99)), MaxICODC)
                    Case ControlButtonStatus.Pressed
                        MaxICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.FromArgb(0, 177, 89)), MaxICODC)
                    Case Else
                        MaxICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), MaxICODC)
                End Select
            Catch ex As Exception
                MaxICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), MaxICODC)
            End Try
            Dim MaxICOStrokeStyle As New StrokeStyle(d2DDevice.Factory, New StrokeStyleProperties() With {.StartCap = CapStyle.Round, .EndCap = CapStyle.Round})
            MaxICODC.Target = MaxICOBitmap
            MaxICODC.BeginDraw()
            Select Case WindowState
                Case FormWindowState.Maximized
                    MaxICODC.DrawLine(New RawVector2(0.5, 3), New RawVector2(7, 3), MaxICOBrush, 1.2, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(7, 3), New RawVector2(7, 9.5), MaxICOBrush, 1.2, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(7, 9.5), New RawVector2(0.5, 9.5), MaxICOBrush, 1.2, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(0.5, 9.5), New RawVector2(0.5, 3), MaxICOBrush, 1.2, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(3, 0.5), New RawVector2(9.5, 0.5), MaxICOBrush, 1.2, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(9.5, 0.5), New RawVector2(9.5, 7.5), MaxICOBrush, 1.2, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(9.5, 7.5), New RawVector2(7, 7.5), MaxICOBrush, 1.2, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(3, 3), New RawVector2(3, 0.5), MaxICOBrush, 1.2, MaxICOStrokeStyle)
                Case FormWindowState.Normal
                    MaxICODC.DrawLine(New RawVector2(0.5, 0.5), New RawVector2(0.5, 9.5), MaxICOBrush, 1.5, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(0.5, 9.5), New RawVector2(9.5, 9.5), MaxICOBrush, 1.5, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(9.5, 9.5), New RawVector2(9.5, 0.5), MaxICOBrush, 1.5, MaxICOStrokeStyle)
                    MaxICODC.DrawLine(New RawVector2(9.5, 0.5), New RawVector2(0.5, 0.5), MaxICOBrush, 1.5, MaxICOStrokeStyle)
            End Select
            MaxICODC.EndDraw()
            Layer1DC.DrawBitmap(MaxICOBitmap, SharpDXConverter.ConvertRectangleF(maxBtnRect), 1.0F, BitmapInterpolationMode.Linear)
            Utilities.Dispose(MaxICOBitmap)
            Utilities.Dispose(MaxICOBrush)
            Utilities.Dispose(MaxICODC)
            Utilities.Dispose(MaxICOStrokeStyle)
#End Region
#Region "繪製關閉圖示"
            Dim CloseICODC As New DeviceContext(d2DDevice, DeviceContextOptions.None)
            Dim CloseICOBitmap As New Bitmap1(CloseICODC, New Size2(10, 10), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
            Dim CloseICOBrush As SolidColorBrush
            Try
                Select Case closeButtonState
                    Case ControlButtonStatus.Unchecked
                        CloseICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), CloseICODC)
                    Case ControlButtonStatus.Entered
                        CloseICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.Red), CloseICODC)
                    Case ControlButtonStatus.Pressed
                        CloseICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DarkRed), CloseICODC)
                    Case Else
                        CloseICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), CloseICODC)
                End Select
            Catch ex As Exception
                CloseICOBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.DimGray), CloseICODC)
            End Try
            Dim CloseICOStrokeStyle As New StrokeStyle(d2DDevice.Factory, New StrokeStyleProperties() With {.StartCap = CapStyle.Round, .EndCap = CapStyle.Round})
            CloseICODC.Target = CloseICOBitmap
            CloseICODC.BeginDraw()
            CloseICODC.DrawLine(New RawVector2(0, 0), New RawVector2(10, 10), CloseICOBrush, 1.5, CloseICOStrokeStyle)
            CloseICODC.DrawLine(New RawVector2(0, 10), New RawVector2(10, 0), CloseICOBrush, 1.5, CloseICOStrokeStyle)
            CloseICODC.EndDraw()
            Layer1DC.DrawBitmap(CloseICOBitmap, SharpDXConverter.ConvertRectangleF(closeBtnRect), 1.0F, BitmapInterpolationMode.Linear)
            Utilities.Dispose(CloseICOBitmap)
            Utilities.Dispose(CloseICOBrush)
            Utilities.Dispose(CloseICODC)
            Utilities.Dispose(CloseICOStrokeStyle)
#End Region
            Layer1DC.EndDraw()
            Utilities.Dispose(backBrush)
            Utilities.Dispose(menuBackBrush)
            'Utilities.Dispose(Layer1DC)
        End If
    End Sub
    Private Sub Layer2Paint(forceUpdate As Boolean)
        If IsNothing(Layer2Bitmap) OrElse forceUpdate Then
            If IsNothing(Layer2DC) = False Then Utilities.Dispose(Layer2DC)
            If IsNothing(Layer2Bitmap) = False Then Utilities.Dispose(Layer2Bitmap)
            Layer2DC = New DeviceContext(d2DDevice, DeviceContextOptions.None)
            Layer2Bitmap = New Bitmap1(Layer2DC, New Size2(RenderControl1.Width, RenderControl1.Height), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
            Layer2DC.Target = Layer2Bitmap
            Layer2DC.BeginDraw()
            Dim clickedBackBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.FromArgb(0, 177, 89)), Layer2DC)
            Dim choosedBackBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.FromArgb(0, 197, 99)), Layer2DC)
            Dim noChoosedBackBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.FromArgb(64, 64, 64)), Layer2DC)
            Dim innerBorderBrush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(Color.LightGray), Layer2DC)
            If hoveredMenuItemIndex > -1 OrElse checkedMenuItemIndex > -1 Then
                Dim i As Integer = 0
                For Each item In MenuItems
                    If i = hoveredMenuItemIndex Then
                        If isClickedMenuItem Then
                            Layer2DC.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(0, 36 * (i + 1), 36, 36)), clickedBackBrush)
                        Else
                            Layer2DC.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(0, 36 * (i + 1), 36, 36)), choosedBackBrush)
                        End If
                    ElseIf i = checkedMenuItemIndex Then
                        Layer2DC.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(0, 36 * (i + 1), 36, 36)), choosedBackBrush)
                    Else
                        Layer2DC.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(0, 36 * (i + 1), 36, 36)), noChoosedBackBrush)
                    End If
                    Layer2DC.DrawBitmap(item.Icon, SharpDXConverter.ConvertRectangleF(New RectangleF(0, 36 * (i + 1), 36, 36)), 1.0F, BitmapInterpolationMode.Linear)
                    i += 1
                Next
            Else
                Dim i As Integer = 0
                For Each item In MenuItems
                    Layer2DC.FillRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(0, 36 * (i + 1), 36, 36)), noChoosedBackBrush)
                    Layer2DC.DrawBitmap(item.Icon, SharpDXConverter.ConvertRectangleF(New RectangleF(0, 36 * (i + 1), 36, 36)), 1.0F, BitmapInterpolationMode.Linear)
                    i += 1
                Next
            End If
            Layer2DC.DrawRoundedRectangle(New RoundedRectangle() With {.RadiusX = 3, .RadiusY = 3, .Rect = SharpDXConverter.ConvertRectangleF(innerWindowRect)}, innerBorderBrush)
            Layer2DC.EndDraw()
            Utilities.Dispose(clickedBackBrush)
            Utilities.Dispose(choosedBackBrush)
            Utilities.Dispose(noChoosedBackBrush)
            Utilities.Dispose(innerBorderBrush)
        End If
    End Sub
#Region "子視窗繪圖程式 (Layer 3)"
    Dim MainPageBitmap As Bitmap1
    Dim ServerListPageBitmap As Bitmap1
    Dim ModpackServerListPageBitmap As Bitmap1
    Private Function Layer3Paint(forceUpdate As Boolean) As Bitmap1
        If checkedMenuItemIndex > -1 AndAlso isShowInnerWindow Then
            Return MenuItems(checkedMenuItemIndex).RenderFunc().Invoke(forceUpdate)
        Else
            Return Nothing
        End If
    End Function
    Private Function RenderMainPage(forceUpdate As Boolean) As Bitmap1
        If IsNothing(MainPageBitmap) OrElse forceUpdate Then
            Dim MainPageDC As New DeviceContext(d2DDevice, DeviceContextOptions.None)
            If IsNothing(MainPageBitmap) = False Then Utilities.Dispose(MainPageBitmap)
            MainPageBitmap = New Bitmap1(MainPageDC, New Size2(innerWindowRect.Width, innerWindowRect.Height), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
            MainPageDC.Target = MainPageBitmap
            MainPageDC.BeginDraw()
            Dim backBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.DimGray))
            Dim foreBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.LightGray))
            Dim foreBrush2 As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.White))
            Dim textBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(80, 80, 80)))
            Dim percentTextBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.Gray))
            Dim textFormat = SharpDXConverter.ConvertFont(New Font("Segoe UI", 30, FontStyle.Bold))
            Dim percentTextFormat = SharpDXConverter.ConvertFont(New Font("Segoe UI", 14))
            Dim geo As Geometry = Nothing
#Region "CPU 儀表繪製"
            Dim cpuBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(185, 29, 71)))
            If (UICPUvalue >= 100) Then
                MainPageDC.FillEllipse(New Ellipse(New RawVector2(80, 80), 75, 75), cpuBrush)
            Else
                MainPageDC.FillEllipse(New Ellipse(New RawVector2(80, 80), 75, 75), backBrush)
                geo = DrawArc(MainPageDC.Factory, UICPUvalue * 3.6, New RawVector2(80, 80), 75)
                MainPageDC.FillGeometry(geo, cpuBrush)
            End If
            MainPageDC.FillEllipse(New Ellipse(New RawVector2(80, 80), 60, 60), foreBrush)
            textFormat.WordWrapping = False
            textFormat.TextAlignment = DirectWrite.TextAlignment.Center
            percentTextFormat.WordWrapping = False
            MainPageDC.DrawText(CPUvalue, textFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(40, 56), New Size(80, 24))), textBrush)
            MainPageDC.DrawText("%", percentTextFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(66 + (TextRenderer.MeasureText(CPUvalue, New Font("Segoe UI", 30, FontStyle.Bold)).Width) / 2, 75), New Size(16, 16))), percentTextBrush)
            percentTextFormat.TextAlignment = DirectWrite.TextAlignment.Center
            MainPageDC.DrawText("CPU", percentTextFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(40, 100), New Size(80, 24))), percentTextBrush)
#End Region
#Region "RAM 儀表繪製"
            Dim ramBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(153, 180, 51)))
            If (UIRAMvalue >= 100) Then
                MainPageDC.FillEllipse(New Ellipse(New RawVector2(80, 240), 75, 75), ramBrush)
            Else
                MainPageDC.FillEllipse(New Ellipse(New RawVector2(80, 240), 75, 75), backBrush)
                If IsNothing(geo) = False AndAlso geo.IsDisposed = False Then geo.Dispose()
                geo = DrawArc(MainPageDC.Factory, UIRAMvalue * 3.6, New RawVector2(80, 240), 75)
                MainPageDC.FillGeometry(geo, ramBrush)
            End If
            MainPageDC.FillEllipse(New Ellipse(New RawVector2(80, 240), 60, 60), foreBrush)
            textFormat.WordWrapping = False
            textFormat.TextAlignment = DirectWrite.TextAlignment.Center
            percentTextFormat.WordWrapping = False
            MainPageDC.DrawText(RAMvalue, textFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(40, 216), New Size(80, 24))), textBrush)
            MainPageDC.DrawText("%", percentTextFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(66 + (TextRenderer.MeasureText(RAMvalue, New Font("Segoe UI", 30, FontStyle.Bold)).Width) / 2, 235), New Size(16, 16))), percentTextBrush)
            percentTextFormat.TextAlignment = DirectWrite.TextAlignment.Center
            MainPageDC.DrawText("記憶體", percentTextFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(40, 260), New Size(80, 24))), percentTextBrush)
#End Region
#Region "Virtual RAM 儀表繪製"
            Dim VirtualRAMBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(255, 196, 13)))
            If (UIVirtualRAMvalue >= 100) Then
                MainPageDC.FillEllipse(New Ellipse(New RawVector2(240, 80), 75, 75), VirtualRAMBrush)
            Else
                MainPageDC.FillEllipse(New Ellipse(New RawVector2(240, 80), 75, 75), backBrush)
                If IsNothing(geo) = False AndAlso geo.IsDisposed = False Then geo.Dispose()
                geo = DrawArc(MainPageDC.Factory, UIVirtualRAMvalue * 3.6, New RawVector2(240, 80), 75)
                MainPageDC.FillGeometry(geo, VirtualRAMBrush)
            End If
            MainPageDC.FillEllipse(New Ellipse(New RawVector2(240, 80), 60, 60), foreBrush)
            textFormat.WordWrapping = False
            textFormat.TextAlignment = DirectWrite.TextAlignment.Center
            percentTextFormat.WordWrapping = False
            MainPageDC.DrawText(VirtualRAMvalue, textFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(200, 56), New Size(80, 24))), textBrush)
            MainPageDC.DrawText("%", percentTextFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(226 + (TextRenderer.MeasureText(VirtualRAMvalue, New Font("Segoe UI", 30, FontStyle.Bold)).Width) / 2, 75), New Size(16, 16))), percentTextBrush)
            percentTextFormat.TextAlignment = DirectWrite.TextAlignment.Center
            MainPageDC.DrawText("虛擬記憶體", percentTextFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(200, 100), New Size(80, 24))), percentTextBrush)
#End Region
#Region "網路儀表繪製"
            Dim NetworkBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(45, 137, 239)))
            If (UINetworkValue >= 100) Then
                MainPageDC.FillEllipse(New Ellipse(New RawVector2(240, 240), 75, 75), NetworkBrush)
            Else
                MainPageDC.FillEllipse(New Ellipse(New RawVector2(240, 240), 75, 75), backBrush)
                If IsNothing(geo) = False AndAlso geo.IsDisposed = False Then geo.Dispose()
                geo = DrawArc(MainPageDC.Factory, UINetworkValue * 3.6, New RawVector2(240, 240), 75)
                MainPageDC.FillGeometry(geo, NetworkBrush)
            End If
            MainPageDC.FillEllipse(New Ellipse(New RawVector2(240, 240), 60, 60), foreBrush)
            textFormat.WordWrapping = False
            textFormat.TextAlignment = DirectWrite.TextAlignment.Center
            percentTextFormat.WordWrapping = False
            MainPageDC.DrawText(networkValue, textFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(200, 216), New Size(80, 24))), textBrush)
            MainPageDC.DrawText("%", percentTextFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(226 + (TextRenderer.MeasureText(networkValue, New Font("Segoe UI", 30, FontStyle.Bold)).Width) / 2, 235), New Size(16, 16))), percentTextBrush)
            percentTextFormat.TextAlignment = DirectWrite.TextAlignment.Center
            MainPageDC.DrawText("網路", percentTextFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(New Point(200, 260), New Size(80, 24))), percentTextBrush)
#End Region
#Region "版本載入分頁繪製"
            Dim javaTabColorBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(227, 162, 26)))
            Dim javaTabColorBrushD As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.Gray))
            Dim bedrockTabColorBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(255, 196, 13)))
            Dim bedrockTabColorBrushD As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(117, 117, 117)))
            Dim solutionTabColorBrush As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.FromArgb(45, 137, 239)))
            Dim solutionTabColorBrushD As New SolidColorBrush(MainPageDC, SharpDXConverter.ConvertColor(Color.DimGray))
            Dim DCType As Type = GetType(DeviceContext)
            DCType.InvokeMember("FillRoundedRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {New RoundedRectangle() With {.RadiusX = 10, .RadiusY = 7, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(355, 5, 396, 355))}, foreBrush})
            If VersionLoadTabIndex = 0 Then
                DCType.InvokeMember("FillRoundedRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {New RoundedRectangle() With {.RadiusX = 10, .RadiusY = 7, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(360, 8, 120, 75))}, javaTabColorBrush})
            Else
                DCType.InvokeMember("FillRoundedRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {New RoundedRectangle() With {.RadiusX = 10, .RadiusY = 7, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(360, 8, 120, 75))}, javaTabColorBrushD})
            End If
            If VersionLoadTabIndex = 1 Then
                DCType.InvokeMember("FillRoundedRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {New RoundedRectangle() With {.RadiusX = 10, .RadiusY = 7, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(460, 8, 120, 75))}, bedrockTabColorBrush})
            Else
                DCType.InvokeMember("FillRoundedRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {New RoundedRectangle() With {.RadiusX = 10, .RadiusY = 7, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(460, 8, 120, 75))}, bedrockTabColorBrushD})
            End If
            If VersionLoadTabIndex = 2 Then
                DCType.InvokeMember("FillRoundedRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {New RoundedRectangle() With {.RadiusX = 10, .RadiusY = 7, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(560, 8, 120, 75))}, solutionTabColorBrush})
            Else
                DCType.InvokeMember("FillRoundedRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {New RoundedRectangle() With {.RadiusX = 10, .RadiusY = 7, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(560, 8, 120, 75))}, solutionTabColorBrushD})
            End If
            DCType.InvokeMember("FillRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {SharpDXConverter.ConvertRectangleF(New RectangleF(660, 8, 50, 75)), foreBrush})
            DCType.InvokeMember("FillRoundedRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {New RoundedRectangle() With {.RadiusX = 10, .RadiusY = 7, .Rect = SharpDXConverter.ConvertRectangleF(New RectangleF(360, 33, 386, 322))}, foreBrush2})
            DCType.InvokeMember("FillRectangle", Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.InvokeMethod, Nothing, MainPageDC, New Object() {SharpDXConverter.ConvertRectangleF(New RectangleF(360, 33, 350, 7)), foreBrush2})
            textFormat = SharpDXConverter.ConvertFont(New Font("微軟正黑體", 16))
            textFormat.TextAlignment = DirectWrite.TextAlignment.Center
            MainPageDC.DrawText("Java", textFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(360, 9, 100, 25)), foreBrush2)
            MainPageDC.DrawText("Bedrock", textFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(460, 9, 100, 25)), foreBrush2)
            MainPageDC.DrawText("方案", textFormat, SharpDXConverter.ConvertRectangleF(New RectangleF(560, 9, 100, 25)), foreBrush2)
            javaTabColorBrush.Dispose()
            bedrockTabColorBrush.Dispose()
            solutionTabColorBrush.Dispose()
            javaTabColorBrushD.Dispose()
            bedrockTabColorBrushD.Dispose()
            solutionTabColorBrushD.Dispose()
#End Region
            If IsNothing(geo) = False AndAlso geo.IsDisposed = False Then geo.Dispose()
            MainPageDC.EndDraw()
            cpuBrush.Dispose()
            ramBrush.Dispose()
            VirtualRAMBrush.Dispose()
            NetworkBrush.Dispose()
            backBrush.Dispose()
            foreBrush.Dispose()
            foreBrush2.Dispose()
            textBrush.Dispose()
            MainPageDC.Dispose()
        End If
        Return MainPageBitmap
    End Function
    Private Function RenderServerListPage(forceUpdate As Boolean) As Bitmap1
        If IsNothing(ServerListPageBitmap) OrElse forceUpdate Then
            Dim ServerListPageDC As New DeviceContext(d2DDevice, DeviceContextOptions.None)
            If IsNothing(ServerListPageBitmap) = False Then Utilities.Dispose(ServerListPageBitmap)
            ServerListPageBitmap = New Bitmap1(ServerListPageDC, New Size2(innerWindowRect.Width, innerWindowRect.Height), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
        End If
        Return ServerListPageBitmap
    End Function
    Private Function RenderModpackServerListPage(forceUpdate As Boolean) As Bitmap1
        If IsNothing(ModpackServerListPageBitmap) OrElse forceUpdate Then
            Dim ModpackServerListPageDC As New DeviceContext(d2DDevice, DeviceContextOptions.None)
            If IsNothing(ModpackServerListPageBitmap) = False Then Utilities.Dispose(ModpackServerListPageBitmap)
            ModpackServerListPageBitmap = New Bitmap1(ModpackServerListPageDC, New Size2(innerWindowRect.Width, innerWindowRect.Height), New BitmapProperties1() With {.PixelFormat = D2PixelFormat, .BitmapOptions = BitmapOptions.Target})
        End If
        Return ModpackServerListPageBitmap
    End Function
#End Region

    Private Sub RenderControl1_Paint(sender As Object, e As PaintEventArgs) Handles RenderControl1.Paint
        requestFlags = FormEventFlags.RefreshAll
    End Sub
    Dim PCStatusDataEventHandler As New EventHandler(AddressOf getPCStatusData)
    Private Sub OnPageChanged(pageIndex As Integer)
        Select Case pageIndex
            Case 0
                PCStatusCheckingTimer.Enabled = True
                AddHandler PCStatusCheckingTimer.Tick, PCStatusDataEventHandler
            Case 1
                PCStatusCheckingTimer.Enabled = False
                RemoveHandler PCStatusCheckingTimer.Tick, PCStatusDataEventHandler
            Case 2
                PCStatusCheckingTimer.Enabled = False
                RemoveHandler PCStatusCheckingTimer.Tick, PCStatusDataEventHandler
        End Select
    End Sub

    Private Sub RenderControl1_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged, RenderControl1.SizeChanged
        minBtnRect = New RectangleF(Width - 43, 5, 10, 10)
        maxBtnRect = New RectangleF(Width - 29, 5, 10, 10)
        closeBtnRect = New RectangleF(Width - 15, 5, 10, 10)
        innerWindowRect = New RectangleF(40, 37, Width - 43, Height - 40)
        If WindowState = FormWindowState.Normal Then
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 6, 6))
        Else
            Region = New Region(New Rectangle(0, 0, Width, Height))
        End If
        If deviceContext IsNot Nothing AndAlso backBuffer IsNot Nothing AndAlso targetBitmap IsNot Nothing Then
            Try
                deviceContext.Target = Nothing
                backBuffer.Dispose()
                targetBitmap.Dispose()
                sc.ResizeBuffers(1, 0, 0, Format, SwapChainFlags.None)
                backBuffer = Surface.FromSwapChain(sc, 0)
                targetBitmap = New Bitmap1(deviceContext, backBuffer)
                deviceContext.Target = targetBitmap
            Catch ex As Exception
                Exit Sub
            End Try
            requestFlags = requestFlags Or FormEventFlags.RefreshAll
        End If
    End Sub
    Function DrawArc(Factory As Direct2D1.Factory, degree As Single, centerPoint As RawVector2, radius As Single) As Geometry
        Dim geo As New PathGeometry(Factory)
        Dim sink As GeometrySink = geo.Open()
        sink.BeginFigure(New RawVector2(centerPoint.X, centerPoint.Y - radius), FigureBegin.Filled)
        sink.AddLine(centerPoint)
        sink.AddLine(New RawVector2(centerPoint.X + radius * Math.Sin(degree * Math.PI / 180), centerPoint.Y - radius * Math.Cos(degree * Math.PI / 180)))
        sink.AddArc(New ArcSegment() With {.ArcSize = IIf(degree > 180, ArcSize.Large, ArcSize.Small), .Point = New RawVector2(centerPoint.X, centerPoint.Y - radius), .RotationAngle = degree, .Size = New Size2F(radius, radius), .SweepDirection = SweepDirection.CounterClockwise})
        sink.EndFigure(FigureEnd.Closed)
        sink.Close()
        sink.Dispose()
        Return geo
    End Function
#End Region
#Region "功能實作"
    Private Sub RenderControl1_MouseDown(sender As Object, e As MouseEventArgs) Handles RenderControl1.MouseDown
        If e.Button = MouseButtons.Left Then
            Select Case e.Y
                Case Is <= RegionSize
                    isEdgeDragging = True
                    dragTop = True
                    Select Case e.X
                        Case Is <= RegionSize 'TopLeft
                            dragLeft = True
                        Case Is >= Width - RegionSize 'TopRight
                            dragRight = True
                    End Select
                Case Is >= Height - RegionSize
                    isEdgeDragging = True
                    dragBottom = True
                    Select Case e.X
                        Case Is <= RegionSize 'BottomLeft
                            dragLeft = True
                        Case Is >= Width - RegionSize 'BottomRight
                            dragRight = True
                    End Select
                Case Else
                    Select Case e.X
                        Case Is <= RegionSize 'Left
                            isEdgeDragging = True
                            dragLeft = True
                        Case Is >= Width - RegionSize 'Right
                            isEdgeDragging = True
                            dragRight = True
                        Case Else
                            isEdgeDragging = False
                            dragLeft = False
                            dragRight = False
                            dragTop = False
                            dragBottom = False
                            Cursor = Cursors.Default
                    End Select
            End Select
            If e.Y <= 36 AndAlso (e.X > RegionSize AndAlso e.Y > RegionSize) Then '標題列
                If e.X < Width - 43 Then
                    WinAPI.MoveForm(Handle)
                Else
                    If closeBtnRect.Contains(e.Location) Then
                        closeButtonState = ControlButtonStatus.Pressed
                        requestFlags = requestFlags Or FormEventFlags.MouseMove Or FormEventFlags.RefreshLayer1
                    End If
                    If minBtnRect.Contains(e.Location) Then
                        minButtonState = ControlButtonStatus.Pressed
                        requestFlags = requestFlags Or FormEventFlags.MouseMove Or FormEventFlags.RefreshLayer1
                        SyncLock Me
                            WindowState = FormWindowState.Minimized
                        End SyncLock
                    End If
                    If maxBtnRect.Contains(e.Location) Then
                        maxButtonState = ControlButtonStatus.Pressed
                        requestFlags = requestFlags Or FormEventFlags.MouseMove Or FormEventFlags.RefreshLayer1
                        SyncLock Me
                            Select Case WindowState
                                Case FormWindowState.Normal
                                    WindowState = FormWindowState.Maximized
                                Case FormWindowState.Maximized
                                    WindowState = FormWindowState.Normal
                            End Select
                        End SyncLock
                        minBtnRect = New RectangleF(Width - 43, 5, 10, 10)
                        maxBtnRect = New RectangleF(Width - 29, 5, 10, 10)
                        closeBtnRect = New RectangleF(Width - 15, 5, 10, 10)
                        innerWindowRect = New RectangleF(40, 37, Width - 43, Height - 40)
                    End If
                End If
            End If
            If e.X <= 36 AndAlso e.Y > 36 AndAlso hoveredMenuItemIndex > -1 Then
                isClickedMenuItem = True
                If checkedMenuItemIndex <> hoveredMenuItemIndex Then
                    OnPageChanged(checkedMenuItemIndex)
                    checkedMenuItemIndex = hoveredMenuItemIndex
                End If
                requestFlags = requestFlags Or FormEventFlags.MouseMove Or FormEventFlags.RefreshLayer2 Or FormEventFlags.RefreshLayer3
            End If
            If innerWindowRect.Contains(e.Location) Then
                Select Case checkedMenuItemIndex
                    Case 0
                        Dim checkVersionLoadingTabHeader As Boolean = False
                        Dim i As Integer = 0
                        For Each rect In VersionLoadTabRects
                            If rect.Contains(e.Location) Then
                                checkVersionLoadingTabHeader = True
                                VersionLoadTabIndex = i
                                Exit For
                            End If
                            i += 1
                        Next
                        If checkVersionLoadingTabHeader Then requestFlags = requestFlags Or FormEventFlags.MouseMove Or FormEventFlags.RefreshLayer3
                End Select
            End If
        End If
    End Sub

    Private Sub RenderControl1_MouseMove(sender As Object, e As MouseEventArgs) Handles RenderControl1.MouseMove
        CurrentCursorPosition = RenderControl1.PointToScreen(e.Location)
        If isEdgeDragging Then
            Dim scrFormEndLoc As Point = PointToScreen(New Point(Width, Height))
            Try
                If e.Button = MouseButtons.Left Then
                    If Width > 10 OrElse Height > 10 Then
                        If dragLeft And dragTop Then
                            Location = CurrentCursorPosition
                            Size = New Size(scrFormEndLoc.X - CurrentCursorPosition.X, scrFormEndLoc.Y - CurrentCursorPosition.Y)
                        ElseIf dragLeft And dragBottom Then
                            Location = New Point(CurrentCursorPosition.X, Location.Y)
                            Size = New Size(scrFormEndLoc.X - CurrentCursorPosition.X, CurrentCursorPosition.Y - Location.Y)
                        ElseIf dragRight And dragTop Then
                            Location = New Point(Location.X, CurrentCursorPosition.Y)
                            Size = New Size(CurrentCursorPosition.X - Location.X, scrFormEndLoc.Y - CurrentCursorPosition.Y)
                        ElseIf dragRight And dragBottom Then
                            Size = PointToClient(CurrentCursorPosition)
                        ElseIf dragTop Then
                            Location = New Point(Location.X, CurrentCursorPosition.Y)
                            Size = New Size(Width, scrFormEndLoc.Y - CurrentCursorPosition.Y)
                        ElseIf dragLeft Then
                            Location = New Point(CurrentCursorPosition.X, Location.Y)
                            Size = New Size(scrFormEndLoc.X - CurrentCursorPosition.X, Height)
                        ElseIf dragRight Then
                            Size = New Size(CurrentCursorPosition.X - Location.X, Height)
                        ElseIf dragBottom Then
                            Size = New Size(Width, CurrentCursorPosition.Y - Location.Y)
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        Else
            If (e.Y <= RegionSize OrElse e.Y >= Height - RegionSize) AndAlso e.X > RegionSize AndAlso e.X < Width - RegionSize Then
                Cursor = Cursors.SizeNS
            ElseIf (e.X <= RegionSize OrElse e.X >= Width - RegionSize) AndAlso e.Y > RegionSize AndAlso e.Y < Height - RegionSize Then
                Cursor = Cursors.SizeWE
            ElseIf (e.Y <= RegionSize AndAlso e.X <= RegionSize) OrElse (e.Y >= Height - RegionSize AndAlso e.X >= Width - RegionSize) Then
                Cursor = Cursors.SizeNWSE
            ElseIf (e.Y <= RegionSize AndAlso e.X >= Width - RegionSize) OrElse (e.Y >= Height - RegionSize AndAlso e.X <= RegionSize) Then
                Cursor = Cursors.SizeNESW
            Else
                Cursor = Cursors.Default
            End If
            Dim eventFlags = FormEventFlags.MouseMove
            If e.Y < 25 AndAlso e.X >= Width - 43 Then '標題列
                Dim isChanged As Boolean = False
                If closeBtnRect.Contains(e.Location) Then
                    If closeButtonState <> ControlButtonStatus.Pressed Then
                        isChanged = (closeButtonState = ControlButtonStatus.Entered)
                        closeButtonState = ControlButtonStatus.Entered
                    End If
                Else
                    isChanged = (closeButtonState = ControlButtonStatus.Unchecked)
                    closeButtonState = ControlButtonStatus.Unchecked
                End If
                If minBtnRect.Contains(e.Location) Then
                    If minButtonState <> ControlButtonStatus.Pressed Then
                        isChanged = isChanged OrElse (minButtonState = ControlButtonStatus.Entered)
                        minButtonState = ControlButtonStatus.Entered
                    End If
                Else
                    isChanged = isChanged OrElse (minButtonState = ControlButtonStatus.Unchecked)
                    minButtonState = ControlButtonStatus.Unchecked
                End If
                If maxBtnRect.Contains(e.Location) Then
                    If maxButtonState <> ControlButtonStatus.Pressed Then
                        isChanged = isChanged OrElse (maxButtonState = ControlButtonStatus.Entered)
                        maxButtonState = ControlButtonStatus.Entered
                    End If
                Else
                    isChanged = isChanged OrElse (maxButtonState = ControlButtonStatus.Unchecked)
                    maxButtonState = ControlButtonStatus.Unchecked
                End If
                If isChanged = True Then eventFlags = eventFlags Or FormEventFlags.RefreshLayer1
            End If
            Dim containMenuBound As Boolean = False
            If e.X <= 36 AndAlso e.Y > 36 Then
                For i As Integer = 0 To MenuItems.Count - 1
                    Dim itemBound As New Rectangle(0, 36 * (i + 1), 36, 36)
                    If itemBound.Contains(e.Location) Then
                        If hoveredMenuItemIndex <> i Then
                            eventFlags = eventFlags Or FormEventFlags.RefreshLayer2
                            hoveredMenuItemIndex = i
                        End If
                        containMenuBound = True
                        Exit For
                    End If
                Next
            End If
            If containMenuBound = False Then
                If hoveredMenuItemIndex <> -1 Then
                    eventFlags = eventFlags Or FormEventFlags.RefreshLayer2
                    hoveredMenuItemIndex = -1
                End If
            End If
            requestFlags = requestFlags Or eventFlags
        End If
    End Sub

    Private Sub RenderControl1_MouseUp(sender As Object, e As MouseEventArgs) Handles RenderControl1.MouseUp
        Dim isChanged As Boolean = False
        Dim eventFlags = FormEventFlags.MouseMove
        isChanged = isClickedMenuItem = True
        isClickedMenuItem = False
        If isChanged Then eventFlags = eventFlags Or FormEventFlags.RefreshLayer2
        If isEdgeDragging Then
            Cursor = Cursors.Default
            isEdgeDragging = False
            dragLeft = False
            dragRight = False
            dragTop = False
            dragBottom = False
        End If
        If closeButtonState <> ControlButtonStatus.Unchecked Then
            If closeBtnRect.Contains(e.Location) Then
                closeButtonState = ControlButtonStatus.Entered
            Else
                closeButtonState = ControlButtonStatus.Unchecked
            End If
            eventFlags = eventFlags Or FormEventFlags.RefreshLayer1
            isChanged = True
            SyncLock Me
                Close()
            End SyncLock
        End If
        If minButtonState <> ControlButtonStatus.Unchecked Then
            If minBtnRect.Contains(e.Location) Then
                minButtonState = ControlButtonStatus.Entered
            Else
                minButtonState = ControlButtonStatus.Unchecked
            End If
            eventFlags = eventFlags Or FormEventFlags.RefreshLayer1
            isChanged = True
        End If
        If maxButtonState <> ControlButtonStatus.Unchecked Then
            If maxBtnRect.Contains(e.Location) Then
                maxButtonState = ControlButtonStatus.Entered
            Else
                maxButtonState = ControlButtonStatus.Unchecked
            End If
            eventFlags = eventFlags Or FormEventFlags.RefreshLayer1
            isChanged = True
        End If
        If isChanged Then requestFlags = requestFlags Or eventFlags
    End Sub

    Private Sub RenderControl1_MouseLeave(sender As Object, e As EventArgs) Handles RenderControl1.MouseLeave, MyBase.MouseLeave
        Dim eventFlags = FormEventFlags.MouseMove
        Dim isChanged As Boolean = False
        If closeButtonState <> ControlButtonStatus.Unchecked Then
            isChanged = (closeButtonState = ControlButtonStatus.Unchecked)
            closeButtonState = ControlButtonStatus.Unchecked
        End If
        If minButtonState <> ControlButtonStatus.Unchecked Then
            isChanged = isChanged OrElse (minButtonState = ControlButtonStatus.Unchecked)
            minButtonState = ControlButtonStatus.Unchecked
        End If
        If maxButtonState <> ControlButtonStatus.Unchecked Then
            isChanged = isChanged OrElse (maxButtonState = ControlButtonStatus.Unchecked)
            maxButtonState = ControlButtonStatus.Unchecked
        End If
        If isChanged = True Then eventFlags = eventFlags Or FormEventFlags.RefreshLayer1
        requestFlags = requestFlags Or eventFlags
    End Sub

    Private Sub RenderControl1_MouseEnter(sender As Object, e As EventArgs) Handles RenderControl1.MouseEnter
        Dim eventFlags = FormEventFlags.MouseMove
        requestFlags = requestFlags Or eventFlags
    End Sub

    Private Sub RenderControl1_MouseClick(sender As Object, e As MouseEventArgs) Handles RenderControl1.MouseClick

    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Const CS_DROPSHADOW As Integer = &H20000
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ClassStyle = cp.ClassStyle Or CS_DROPSHADOW
            Return cp
        End Get
    End Property

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        XPaint(requestFlags)
    End Sub
#End Region
End Class