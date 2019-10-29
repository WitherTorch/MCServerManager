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
End Class
