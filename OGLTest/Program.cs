using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cave;
using Cave.Media;
using Cave.Media.OpenGL;
using Cave.Media.Video;

namespace OGLTest
{
    class Program
    {
        Glfw3Renderer renderer;
        bool closed = false;
        Dictionary<string, IRenderSprite> sprites = new Dictionary<string, IRenderSprite>();
        Bitmap32[] circles;

        static void Main(string[] args)
        {
            var p = new Program();
            p.Start();
        }

        private void Initialize()
        {
            renderer = new Glfw3Renderer();
            if (!renderer.IsAvailable) throw new Exception("render not available");
            IRenderDevice[] devs = renderer.GetDevices();
            if (devs.Length < 1) throw new Exception("no devices found");
            renderer.Closed += WindowClosed;
            renderer.MouseButtonChanged += MBChanged;
            renderer.Initialize(devs[0], RendererMode.Window, RendererFlags.WaitRetrace, 1024, 768, "OpenGL Test");
            circles = new Bitmap32[5];
            for (int i = 0; i < circles.Length; i++)
            {
                circles[i] = Circle(ARGB.Random);
            }
        }

        private void MBChanged(object sender, glfw3.MouseButtonEventArgs e)
        {
            Console.WriteLine($"Button {e.Button.ToString()} was {e.State.ToString()} at ({e.Position.X},{e.Position.Y})");
        }

        private Bitmap32 Circle(Color color)
        {
            Bitmap b = new Bitmap(100,100);
            using (Graphics g = Graphics.FromImage(b))
            {
              //  g.Clear(ARGB.Random);
                SolidBrush br = new SolidBrush(color);
                Rectangle bounds = new Rectangle(Point.Empty, b.Size);
                g.FillEllipse(br, bounds);
                g.DrawString(DateTime.Now.ToString(), SystemFonts.DialogFont, Brushes.LawnGreen, bounds);
            }

            return new GdiBitmap32(b);
        }

        private void CreateSprites()
        {
            Bitmap32 b = Bitmap32.FromFile(@"..\..\Assets\checker.png");
            IRenderSprite sp = renderer.CreateSprite("bg");
            sp.Tint = ARGB.FromColor(100, 0, 0, 100);
            sp.LoadTexture(b);
            sprites.Add("bg", sp);

            sp = renderer.CreateSprite("s1");
            sp.LoadTexture(circles[0]);
            sp.Scale = Vector3.Create(0.5f, 0.5f, 1);                        
            sprites.Add("s1", sp);

        }


        private void WindowClosed(object sender, EventArgs e)
        {
            closed = true;
        }

        private void Start()
        {
            Initialize();
            CreateSprites();

            int counter = 0;
            DateTime starttime = DateTime.Now;
            DateTime runsince = starttime;
            TimeSpan onesec = TimeSpan.FromSeconds(1);
            while (!closed)
            {
                float msecs = (float)(DateTime.Now - runsince).TotalMilliseconds;
                sprites["s1"].Rotation = Vector3.Create(msecs / 5000f,0,0);
                sprites["s1"].LoadTexture(circles[counter % 5]);
                Render();
                renderer.Present();
                counter++;
                if ((DateTime.Now - starttime) > onesec)
                {
                    System.Console.WriteLine($"FPS: {counter}");
                    counter = 0;
                    starttime = DateTime.Now;
                }
                Thread.Sleep(10);

            }
        }

        private void Render()
        {
            renderer.Clear(Color.Red);
            renderer.Render(sprites.Values.ToArray());
        }
    }
}
