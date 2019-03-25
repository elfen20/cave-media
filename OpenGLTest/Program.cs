using Cave.Console;
using Cave.Media;
using Cave.Media.OpenGL;
using Cave.Media.Video;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace OpenGLTest
{

    public static class Extensions
    {

        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
    }

    class Program
    {
        Glfw3Renderer renderer;
        bool closed = false;
        bool verbose = false;

        int testNr = 0;
        int width = 800;
        int height = 400;
        int display = 0;
        RendererMode rmode = RendererMode.Window;


        List<IRenderSprite> rSprites = new List<IRenderSprite>();

        static void Main(string[] args)
        {
            var p = new Program();
            p.Run(args);
            SystemConsole.WriteLine("exit program.");
        }

        private void Run(string[] oldArgs)
        {
            var args = Arguments.FromArray(oldArgs);

            if (args.IsHelpOptionFound())
            {
                SystemConsole.WriteLine("Options:");
                SystemConsole.WriteLine("<yellow>-v<default> \t show verbose output");
                SystemConsole.WriteLine("<yellow>-s<default> \t show available displays");
                SystemConsole.WriteLine("<yellow>-d=<green><display><default> \t set display");
                SystemConsole.WriteLine("<yellow>-b=<green><w>x<h><default> \t set backbuffer size in width x height");
                SystemConsole.WriteLine("<yellow>-w, -f, -wf<green>[w][f][wf]<default> \t set window mode to window, fullscreen or windowed fullscreen");
                SystemConsole.WriteLine("<yellow>-t=<green><n><default> \t use test number n");
                return;
            }

            if (args.IsOptionPresent("v"))
            {
                verbose = true;
                SystemConsole.WriteLine("<yellow>Verbose output activated!<default>");
            }

            if (args.IsOptionPresent("w"))
            {
                rmode = RendererMode.Window;
                SystemConsole.WriteLine($"<yellow>Rendermode: <green>{rmode}<default>");
            }
            if (args.IsOptionPresent("f"))
            {
                rmode = RendererMode.FullScreen;
                SystemConsole.WriteLine($"<yellow>Rendermode: <green>{rmode}<default>");
            }
            if (args.IsOptionPresent("wf"))
            {
                rmode = RendererMode.WindowedFullScreen;
                SystemConsole.WriteLine($"<yellow>Rendermode: <green>{rmode}<default>");
            }


            if (args.IsOptionPresent("b"))
            {
                string[] dims = args.Options["b"].Value.Split('x');
                if (dims.Length != 2) throw new Exception("backbuffer size has to be in form of (width)x(height) e.g. 800x600!");
                if (!int.TryParse(dims[0], out width)) throw new Exception("error parsing backbuffer width!");
                if (!int.TryParse(dims[1], out height)) throw new Exception("error parsing backbuffer height!");
                SystemConsole.WriteLine($"<yellow>backbuffer set: {width} x {height}<default>");
            }

            Initialize(args);
            if (args.IsOptionPresent("t"))
            {
                if (!int.TryParse(args.Options["t"].Value, out testNr)) throw new Exception("error parsing test number");
                CreateSprites();
            }
            Play();
        }

        private void CreateSprites()
        {
            if (testNr > 0)
            {
                var bg = renderer.CreateSprite("1");
                bg.LoadTexture(new GdiBitmap32(TestImageGenerator.GenerateCheckerBoard(new Size(64,64),new Size(8,8),Color.Blue, Color.Gray)));
                rSprites.Add(bg);
            }
            if (testNr > 1)
            {
                var fps = renderer.CreateSprite("fps");
               // fps.Tint = Color.Red;
                fps.Position = Vector3.Create(-0.75f, -0.75f, 0f);
                fps.Scale = Vector3.Create(0.3f, 0.2f, 1);
                rSprites.Add(fps);

            }
            if (testNr > 2)
            {
                var rect = renderer.CreateSprite("rect");
                rect.Alpha = 0.6f;
                rect.Scale = Vector3.Create(0.6f, 0.6f, 1);
                rect.LoadTexture(new GdiBitmap32(TestImageGenerator.GenerateColorStripes(new Size(256, 256))));
                rSprites.Add(rect);

            }
        }

        private void Play()
        {
            SystemConsole.WriteLine("start OpenGLTest:");
            int counter = 0;
            DateTime starttime = DateTime.Now;
            DateTime runsince = starttime;
            TimeSpan onesec = TimeSpan.FromSeconds(1);
            while (!closed)
            {
                UpdateSprites(DateTime.Now - runsince);
                Render();
                counter++;
                float msecs = (float)(DateTime.Now - runsince).TotalMilliseconds;
                if ((DateTime.Now - starttime) > onesec)
                {
                    SystemConsole.WriteLine($"FPS: <yellow>{counter}<default>");
                    counter = 0;
                    starttime = DateTime.Now;
                }
                Thread.Sleep(1);
                if (SystemConsole.KeyAvailable) closed = true;
            }
        }

        private void UpdateSprites(TimeSpan timeSpan)
        {
           if (testNr > 1)
            {
                string ms = (timeSpan.TotalMilliseconds / 1000f).ToString("F2");
                rSprites[1].LoadTexture(new GdiBitmap32(TestImageGenerator.GenerateString(new Size(150, 50), Color.Goldenrod, Color.Transparent, ms,0.8f)));
            }
            if (testNr > 1)
            {
                rSprites[2].Rotation = Vector3.Create(0,0,(float)timeSpan.TotalMilliseconds / 3000);
            }
        }

        private void Render()
        {
            renderer.Clear(Color.DarkBlue);
            
            renderer.Render(rSprites);
            renderer.Present();
        }

        private void Initialize(Arguments args)
        {
            renderer = new Glfw3Renderer();
            if (!renderer.IsAvailable) throw new Exception("render not available");
            IRenderDevice[] devs = renderer.GetDevices();
            if (devs.Length < 1) throw new Exception("no devices found");
            if (args.IsOptionPresent("s"))
            {
                SystemConsole.WriteLine("Available displays:");
                for (int i=0; i< devs.Length; i++)
                {
                    SystemConsole.WriteLine($"\t<yellow>{i}: {devs[i].Name}<default>");
                }
                Environment.Exit(0);
            }

            SystemConsole.WriteLine("Initialize glfw3:");

            if (args.IsOptionPresent("d"))
            {
                if (!int.TryParse(args.Options["d"].Value, out width)) throw new Exception("error parsing display number");
                SystemConsole.WriteLine($"\t<yellow>Using {display}: {devs[display].Name}<default>");
            }
            renderer.AspectCorrection = ResizeMode.TouchFromOutside;
            renderer.Closed += WindowClosed;
            renderer.MouseButtonChanged += MBChanged;
            renderer.Initialize(devs[display], rmode, RendererFlags.WaitRetrace, width, height, "OpenGL Test");
            if (verbose) SystemConsole.WriteLine($"glfw3.Window: <yellow>{rmode}<default>, <green>{width}<default>x<green>{height}<default>");

        }

        private void MBChanged(object sender, glfw3.MouseButtonEventArgs e)
        {
            if (e.State == glfw3.InputState.Press)
            {
                SystemConsole.WriteLine($"mouse click: w[{e.Position.X}|{e.Position.Y}] s[{e.PositionNorm.X:F2}|{e.PositionNorm.Y:F2}]");

            }

            if (e.State == glfw3.InputState.Release)
            {
                if (e.PositionNorm.X < 0.1f && e.PositionNorm.Y < 0.1f) closed = true;

                if (e.Button == glfw3.MouseButton.ButtonRight)
                {
                    renderer.AspectCorrection = renderer.AspectCorrection.Next();
                }
            }
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            closed = true;
        }
    }
}
