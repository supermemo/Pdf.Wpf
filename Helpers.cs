#region License & Metadata

// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// 
// 
// Created On:   2018/06/09 01:39
// Modified On:  2018/06/10 16:40
// Modified By:  Alexis

#endregion




using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Patagames.Pdf.Net.Controls.Wpf
{
  public class Helpers
  {
    #region Constructors

    static Helpers()
    {
      // This will require restart the app if DPI is changed
      // but it is too much overhead to check it on each conversion
      var flags = BindingFlags.NonPublic | BindingFlags.Static;
      var dpiProperty = typeof(SystemParameters).GetProperty("Dpi",
                                                             flags);

      Dpi = (int)dpiProperty.GetValue(null,
                                      null);
    }

    #endregion




    public struct Int32Size
    {
      public int Width;
      public int Height;

      public Int32Size(int width,
                       int height)
      {
        Width  = width;
        Height = height;
      }

      public override bool Equals(object obj)
      {
        if (!(obj is Int32Size))
          return false;

        var pt = (Int32Size)obj;
        return Equals(pt);
      }

      public bool Equals(Int32Size obj)
      {
        if (!obj.Width.Equals(Width))
          return false;
        if (!obj.Height.Equals(Height))
          return false;

        return true;
      }

      public static bool operator ==(Int32Size left,
                                     Int32Size right)
      {
        return left.Equals(right);
      }

      public static bool operator !=(Int32Size left,
                                     Int32Size right)
      {
        return !(left == right);
      }

      public override int GetHashCode()
      {
        unchecked // Overflow is fine, just wrap
        {
          int hash = 17;
          // Suitable nullity checks etc, of course :)
          hash = hash * 23 + Width.GetHashCode();
          hash = hash * 23 + Height.GetHashCode();
          return hash;
        }
      }
    }

    public struct Int32Point
    {
      public int X;
      public int Y;

      public Int32Point(int x,
                        int y)
      {
        X = x;
        Y = y;
      }

      public override bool Equals(object obj)
      {
        if (!(obj is Int32Point))
          return false;

        var pt = (Int32Point)obj;
        return Equals(pt);
      }

      public bool Equals(Int32Point obj)
      {
        if (!obj.X.Equals(X))
          return false;
        if (!obj.Y.Equals(Y))
          return false;

        return true;
      }

      public static bool operator ==(Int32Point left,
                                     Int32Point right)
      {
        return left.Equals(right);
      }

      public static bool operator !=(Int32Point left,
                                     Int32Point right)
      {
        return !(left == right);
      }

      public override int GetHashCode()
      {
        unchecked // Overflow is fine, just wrap
        {
          int hash = 17;
          // Suitable nullity checks etc, of course :)
          hash = hash * 23 + X.GetHashCode();
          hash = hash * 23 + Y.GetHashCode();
          return hash;
        }
      }
    }




    #region DPIhandling

    public static int Dpi { get; private set; }

    public static int PointsToPixels(double points,
                                     int    dpi)
    {
      return (int)(points * dpi / 72.0);
    }

    public static int PointsToPixels(double points)
    {
      return PointsToPixels(points,
                            Dpi);
    }

    public static double PixelsToPoints(int pixels,
                                        int dpi)
    {
      return pixels * 72.0 / dpi;
    }

    public static double PixelsToPoints(int pixels)
    {
      return PixelsToPoints(pixels,
                            Dpi);
    }

    #endregion DPIhandling




    #region Colors, pens, brushes, Rects and Sizes, etc

    private static readonly Color _emptyColor = Color.FromArgb(0,
                                                               0,
                                                               0,
                                                               0);
    public static Color ColorEmpty { get { return _emptyColor; } }

    public static Pen CreatePen(Brush  brush,
                                double thick = 1.0)
    {
      return new Pen(brush,
                     thick);
    }

    public static Pen CreatePen(Color  color,
                                double thick = 1.0)
    {
      return CreatePen(CreateBrush(color),
                       thick);
    }

    public static Brush CreateBrush(Color color)
    {
      return new SolidColorBrush(color);
    }

    public static int ToArgb(Color color)
    {
      return (color.A << 24) | (color.R << 16) | (color.G << 8) | color.B;
    }

    public static Size CreateSize(double nw,
                                  double nh)
    {
      if (nw < 0)
        nw = 0;
      if (nh < 0)
        nh = 0;
      return new Size(nw,
                      nh);
    }

    public static RenderRect CreateRenderRect(double x,
                                              double y,
                                              double w,
                                              double h,
                                              bool   isChecked)
    {
      if (w < 0)
        w = 0;
      if (h < 0)
        h = 0;
      return new RenderRect(x,
                            y,
                            w,
                            h,
                            isChecked);
    }

    public static Rect CreateRect(double x,
                                  double y,
                                  double w,
                                  double h)
    {
      if (w < 0)
        w = 0;
      if (h < 0)
        h = 0;
      return new Rect(x,
                      y,
                      w,
                      h);
    }

    public static Rect CreateRect(Point location,
                                  Size  size)
    {
      if (size.Width < 0)
        size.Width = 0;
      if (size.Height < 0)
        size.Height = 0;
      return new Rect(location,
                      size);
    }

    public static double ThicknessHorizontal(Thickness pageMargin)
    {
      return pageMargin.Left + pageMargin.Right;
    }

    public static double ThicknessVertical(Thickness pageMargin)
    {
      return pageMargin.Top + pageMargin.Bottom;
    }

    #endregion




    #region Render

    public static void DrawImageUnscaled(DrawingContext  drawingContext,
                                         WriteableBitmap wpfBmp,
                                         double          x,
                                         double          y)
    {
      drawingContext.DrawImage(wpfBmp,
                               new Rect(x,
                                        y,
                                        /*PixelsToPoints*/(wpfBmp.PixelWidth*96/Dpi),
                                        /*PixelsToPoints*/(wpfBmp.PixelHeight*96/Dpi)));
    }

    public static void FillRectangle(DrawingContext drawingContext,
                                     Brush          brush,
                                     Rect           rect)
    {
      drawingContext.DrawRectangle(brush,
                                   null,
                                   rect);
    }

    public static void DrawRectangle(DrawingContext drawingContext,
                                     Pen            pen,
                                     Rect           rect)
    {
      drawingContext.DrawRectangle(null,
                                   pen,
                                   rect);
    }

    #endregion
  }
}
