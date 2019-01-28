#region CopyRight 2018
/*
    Copyright (c) 2019 Gernot Lenkner (andreas@rohleder.cc)
    All rights reserved
*/
#endregion

using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace OpenGLCheck
{
    /// <summary>
    /// static class that generates TestImages.
    /// </summary>
    public static class TestImageGenerator
    {
        /// <summary>
        /// draws an ellipse on a bitmap.
        /// </summary>
        /// <param name="size">size of the returned bitmap in pixels</param>
        /// <param name="foreground">color of the ellipse</param>
        /// <param name="background">color of the background</param>
        /// <param name="sizeratio">ratio of the size of the ellipse to the size of the bitmap. a ratio of 0.5 is default</param>
        /// <param name="filled">if set the ellipse will be filled</param>
        /// <returns><see cref="Bitmap"/></returns>
        public static Bitmap GenerateEllipse(Size size, Color foreground, Color background, float sizeratio = 0.5f, bool filled = true)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Brush bg = new SolidBrush(background);
                g.FillRectangle(bg, new Rectangle(Point.Empty, size));
                RectangleF rec = new RectangleF((size.Width / 2) * (1 - sizeratio), (size.Height / 2) * (1 - sizeratio), (size.Width / 2) * (1 + sizeratio), (size.Height / 2) * (1 + sizeratio));
                if (filled)
                {
                    Brush fg = new SolidBrush(foreground);
                    g.FillEllipse(fg, rec.X - 0.5f, rec.Y - 0.5f, rec.Width, rec.Height);
                }
                else
                {
                    Pen fg = new Pen(foreground);
                    g.DrawEllipse(fg, rec.X, rec.Y, rec.Width - 0.5f, rec.Height - 0.5f);
                }
            }
            return bmp;
        }

        /// <summary>
        /// draws a rectangle on a bitmap.
        /// </summary>
        /// <param name="size">size of the returned bitmap in pixels</param>
        /// <param name="foreground">color of the rectangle</param>
        /// <param name="background">color of the background</param>
        /// <param name="sizeratio">ratio of the size of the rectangle to the size of the bitmap. a ratio of 0.5 is default</param>
        /// <param name="filled">if set the rectangle will be filled</param>
        /// <returns><see cref="Bitmap"/></returns>
        public static Bitmap GenerateRectangle(Size size, Color foreground, Color background, float sizeratio = 0.5f, bool filled = true)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Brush bg = new SolidBrush(background);
                g.FillRectangle(bg, new Rectangle(Point.Empty, size));
                RectangleF rec = new RectangleF((size.Width / 2) * (1 - sizeratio), (size.Height / 2) * (1 - sizeratio), (size.Width) * (sizeratio), (size.Height) * (sizeratio));
                if (filled)
                {
                    Brush fg = new SolidBrush(foreground);
                    g.FillRectangle(fg, rec);
                }
                else
                {
                    Pen fg = new Pen(foreground);
                    g.DrawRectangle(fg, rec.X, rec.Y, rec.Width - 0.5f, rec.Height - 0.5f);
                }
            }
            return bmp;
        }

        /// <summary>
        /// draws a text on a bitmap with FontFamily <see cref="FontFamily.GenericSansSerif"/>
        /// </summary>
        /// <param name="size">size of the returned bitmap in pixels</param>
        /// <param name="foreground">color of the text</param>
        /// <param name="background">color of the background</param>
        /// <param name="text">the text</param>
        /// <param name="sizeratio">ratio of the size of the rectangle to the size of the bitmap. a ratio of 0.5 is default</param>
        /// <returns><see cref="Bitmap"/></returns>
        public static Bitmap GenerateString(Size size, Color foreground, Color background, string text, float sizeratio = 0.5f)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Brush bg = new SolidBrush(background);
                g.FillRectangle(bg, new Rectangle(Point.Empty, size));
                RectangleF rec = new RectangleF((size.Width / 2) * (1 - sizeratio), (size.Height / 2) * (1 - sizeratio), (size.Width) * (sizeratio), (size.Height) * (sizeratio));
                Pen fgp = new Pen(foreground);
                g.DrawRectangle(fgp, rec.X, rec.Y, rec.Width - 0.5f, rec.Height - 0.5f);
                Brush fgb = new SolidBrush(foreground);
                g.DrawString(text, new Font(FontFamily.GenericSansSerif, rec.Height * 0.72f), fgb, rec);
            }
            return bmp;
        }

        /// <summary>
        /// generates a bitmap with some default color stripes.
        /// </summary>
        /// <param name="size">the size of the bitmap in pixels</param>
        /// <param name="horizontal">when set horizontal stripes will be generated</param>
        /// <returns><see cref="Bitmap"/></returns>
        public static Bitmap GenerateColorStripes(Size size, bool horizontal = false)
        {
            Color[] colortable = new Color[8]
            {
                Color.Red, Color.Green, Color.Blue, Color.White, Color.Magenta, Color.Cyan, Color.Yellow, Color.Black,
            };
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < 8; i++)
                {
                    Brush b = new SolidBrush(colortable[i]);
                    PointF loc;
                    SizeF rectsize;
                    if (horizontal)
                    {
                        loc = new PointF(0, i * size.Height / 8f);
                        rectsize = new SizeF(size.Width, size.Height / 8f);
                    }
                    else
                    {
                        loc = new PointF(i * size.Width / 8f, 0);
                        rectsize = new SizeF(size.Width / 8f, size.Height);
                    }
                    RectangleF rect = new RectangleF(loc, rectsize);
                    g.FillRectangle(b, rect);
                }
            }
            return bmp;
        }

        /// <summary>
        /// generates a bitmap with eight grey stripes in ascending brightness.
        /// </summary>
        /// <param name="size">the size of the bitmap in pixels</param>
        /// <param name="horizontal">when set horizontal stripes will be generated</param>
        /// <returns><see cref="Bitmap"/></returns>
        public static Bitmap GenerateGrayStripes(Size size, bool horizontal = false)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < 8; i++)
                {
                    Brush b = new SolidBrush(Color.FromArgb(i * 32, i * 32, i * 32));
                    PointF loc;
                    SizeF rectsize;
                    if (horizontal)
                    {
                        loc = new PointF(0, i * size.Height / 8f);
                        rectsize = new SizeF(size.Width, size.Height / 8f);
                    }
                    else
                    {
                        loc = new PointF(i * size.Width / 8f, 0);
                        rectsize = new SizeF(size.Width / 8f, size.Height);
                    }
                    RectangleF rect = new RectangleF(loc, rectsize);
                    g.FillRectangle(b, rect);
                }
            }
            return bmp;
        }

        /// <summary>
        /// generates a bitmap with a checkerboard texture
        /// </summary>
        /// <param name="size">the size of the bitmap in pixels</param>
        /// <param name="fieldsize">the size of one field in pixels</param>
        /// <param name="foreground">the first field color</param>
        /// <param name="background">the secon field color</param>
        /// <returns><see cref="Bitmap"/></returns>
        public static Bitmap GenerateCheckerBoard(Size size, Size fieldsize, Color foreground, Color background)
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Brush[] colors = new Brush[2]
                {
               new SolidBrush(foreground),
                new SolidBrush(background),
                };
                for (int y = 0; y < size.Height; y += fieldsize.Height)
                {
                    for (int x = 0; x < size.Width; x += fieldsize.Width)
                    {
                        Rectangle rect = new Rectangle(new Point(x, y), fieldsize);
                        g.FillRectangle(colors[(y / fieldsize.Height + x / fieldsize.Width) % 2], rect);
                    }
                }
            }
            return bmp;
        }

    }

}
