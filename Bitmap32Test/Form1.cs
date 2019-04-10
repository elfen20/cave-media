using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Cave.Media;

namespace Bitmap32Test
{
    public partial class Form1 : Form
    {
        Bitmap background;
        Bitmap32 background32;

        IBitmap32Loader skialoader;
        IBitmap32Loader gdiloader;

        public Form1()
        {
            InitializeComponent();

            skialoader = new SkiaBitmap32Loader();
            gdiloader = new GdiBitmap32Loader();
            Bitmap32.Loader = gdiloader;

            background = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(background))
            {
                g.Clear(Color.Black);
                g.FillRectangle(Brushes.Red, 50, 0, 50, 50);
                g.FillRectangle(Brushes.Green, 0, 50, 50, 50);
                g.FillRectangle(Brushes.Blue, 50, 50, 50, 50);
            }

            using (MemoryStream ms = new MemoryStream())
            {
                background.Save(ms, ImageFormat.Bmp);
                background32 = Bitmap32.FromStream(ms);
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(background, 0, 0);
        }

        private void bCopy_Click(object sender, EventArgs e)
        {
            using (var g = splitContainer1.Panel2.CreateGraphics())
            {
                g.Clear(Color.Gray);
                g.DrawImage(background32.ToGdiBitmap(), 0, 0);
            }

        }

        private void bScale_Click(object sender, EventArgs e)
        {
            using (var g = splitContainer1.Panel2.CreateGraphics())
            {
                g.Clear(Color.Gray);
                var b32 = background32.Resize(background32.Width * 2, background32.Height * 2, ResizeMode.None);
                g.DrawImage(b32.ToGdiBitmap(), 0, 0);
            }
        }

        private void bRotate_Click(object sender, EventArgs e)
        {
            using (var g = splitContainer1.Panel2.CreateGraphics())
            {
                g.Clear(Color.Gray);
                var b32 = new Bitmap32(background32.Width, background32.Height);
                b32.Draw(background32, 0, 0, new Translation() { Rotation = 4f });
                g.DrawImage(b32.ToGdiBitmap(), 0, 0);
            }
        }

        private void bFlipX_Click(object sender, EventArgs e)
        {
            using (var g = splitContainer1.Panel2.CreateGraphics())
            {
                g.Clear(Color.Gray);
                var b32 = new Bitmap32(background32.Width, background32.Height);
                b32.Draw(background32, 0, 0, new Translation() { FlipHorizontally = true });
                g.DrawImage(b32.ToGdiBitmap(), 0, 0);
            }
        }

        private void bFlipY_Click(object sender, EventArgs e)
        {
            using (var g = splitContainer1.Panel2.CreateGraphics())
            {
                g.Clear(Color.Gray);
                var b32 = new Bitmap32(background32.Width, background32.Height);
                b32.Draw(background32, 0, 0, new Translation() { FlipVertically = true });
                g.DrawImage(b32.ToGdiBitmap(), 0, 0);
            }
        }

        private void cbUseSkia_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUseSkia.Checked)
            {
                Bitmap32.Loader = skialoader;
            }
            else
            {
                Bitmap32.Loader = gdiloader;
            }
        }
    }
}
