Imports SharpDX

Public Class SharpDXConverter
    Public Shared Function ConvertFont(font As Font) As DirectWrite.TextFormat
        Dim fw As DirectWrite.FontWeight = DirectWrite.FontWeight.Normal
        If font.Bold Then
            fw = DirectWrite.FontWeight.Bold
        End If
        Dim fs As DirectWrite.FontStyle = DirectWrite.FontStyle.Normal
        If font.Italic Then
            fs = DirectWrite.FontStyle.Italic
        End If
        Return New DirectWrite.TextFormat(DirectWriteFactory, font.FontFamily.Name, fw, fs, font.SizeInPoints)
    End Function
    Public Shared Function ConvertSolidBrush(brush As Drawing.SolidBrush, renderTarget As Direct2D1.RenderTarget) As Direct2D1.SolidColorBrush
        Return New Direct2D1.SolidColorBrush(renderTarget, ConvertColor(brush.Color))
    End Function
    Public Shared Function ConvertColor(color As Color) As Mathematics.Interop.RawColor4
        Return New Mathematics.Interop.RawColor4(color.R / 255, color.G / 255, color.B / 255, color.A / 255)
    End Function

    Public Shared Function ConvertPoint(point As Point) As Mathematics.Interop.RawVector2
        Return New Mathematics.Interop.RawVector2(point.X, point.Y)
    End Function
    Public Shared Function ConvertSize(size As Size) As Size2
        Return New Size2(size.Width, size.Height)
    End Function
    Public Shared Function ConvertRectangle(rectangle As Rectangle) As Mathematics.Interop.RawRectangle
        Return New Mathematics.Interop.RawRectangle(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
    End Function
    Public Shared Function ConvertRectangleF(rectangle As RectangleF) As Mathematics.Interop.RawRectangleF
        Return New Mathematics.Interop.RawRectangleF(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom)
    End Function
    Public Shared Function ConvertBitmap(image As Bitmap, device As Direct2D1.Device) As Direct2D1.Bitmap
        Dim deviceContext As New Direct2D1.DeviceContext(device, Direct2D1.DeviceContextOptions.None)
        Dim bit As New Direct2D1.Bitmap1(deviceContext, SharpDXConverter.ConvertSize(image.Size), New Direct2D1.BitmapProperties1() With {.PixelFormat = New Direct2D1.PixelFormat(DXGI.Format.B8G8R8A8_UNorm, Direct2D1.AlphaMode.Premultiplied), .BitmapOptions = Direct2D1.BitmapOptions.Target})
        deviceContext.Target = bit
        deviceContext.BeginDraw()
        For x As Integer = 0 To image.Width - 1
            For y As Integer = 0 To image.Height - 1
                Dim brush = SharpDXConverter.ConvertSolidBrush(New SolidBrush(image.GetPixel(x, y)), deviceContext)
                deviceContext.DrawRectangle(SharpDXConverter.ConvertRectangleF(New RectangleF(x, y, 1, 1)), brush)
                Utilities.Dispose(brush)
            Next
        Next
        deviceContext.EndDraw()
        Utilities.Dispose(deviceContext)
        Return bit
    End Function
End Class
