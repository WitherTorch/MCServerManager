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
        Dim desBitmap As System.Drawing.Bitmap

        If image.PixelFormat <> System.Drawing.Imaging.PixelFormat.Format32bppPArgb Then
            desBitmap = New System.Drawing.Bitmap(image.Width, image.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb)

            Using g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(desBitmap)
                g.DrawImage(image, 0, 0)
            End Using
        Else
            desBitmap = image
        End If

        Dim bmpData As System.Drawing.Imaging.BitmapData = desBitmap.LockBits(New System.Drawing.Rectangle(0, 0, desBitmap.Width, desBitmap.Height), System.Drawing.Imaging.ImageLockMode.[ReadOnly], desBitmap.PixelFormat)
        Dim numBytes As Integer = bmpData.Stride * desBitmap.Height
        Dim byteData As Byte() = New Byte(numBytes - 1) {}
        Dim ptr As IntPtr = bmpData.Scan0
        System.Runtime.InteropServices.Marshal.Copy(ptr, byteData, 0, numBytes)
        desBitmap.UnlockBits(bmpData)
        Dim bp As Direct2D1.BitmapProperties1
        Dim pixelFormat As Direct2D1.PixelFormat = New Direct2D1.PixelFormat(DXGI.Format.B8G8R8A8_UNorm, Direct2D1.AlphaMode.Premultiplied)
        bp = New Direct2D1.BitmapProperties1(pixelFormat, desBitmap.HorizontalResolution, desBitmap.VerticalResolution)
        Dim dc As New Direct2D1.DeviceContext(device, Direct2D1.DeviceContextOptions.None)
        Dim tempBitmap As Direct2D1.Bitmap = New Direct2D1.Bitmap1(dc, New Size2(desBitmap.Width, desBitmap.Height), bp)
        tempBitmap.CopyFromMemory(byteData, bmpData.Stride)
        dc.Dispose()
        Return tempBitmap
    End Function
End Class
