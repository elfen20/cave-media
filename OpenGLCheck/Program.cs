using Cave.Media.OpenGL;
using Cave.Media.Video;
using Cave.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using Cave.Media;

namespace OpenGLCheck
{
    class Program
    {
        Glfw3Renderer renderer;
        bool closed = false;
        bool verbose = false;

        int width = 800;
        int height = 600;
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
                CreateSprites(args.Options["t"].Value);
            }
            Play();
        }

        private void CreateSprites(string value)
        {
            switch (value)
            {
                case "1":
                    var s1 = renderer.CreateSprite("1");
                    s1.LoadTexture(new GdiBitmap32(TestImageGenerator.GenerateCheckerBoard(new Size(64,64),new Size(8,8),Color.Blue, Color.Gray)));
                    rSprites.Add(s1);
                    break;
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
                float msecs = (float)(DateTime.Now - runsince).TotalMilliseconds;
                Render();
                counter++;
                if ((DateTime.Now - starttime) > onesec)
                {
                    SystemConsole.WriteLine($"FPS: <yellow>{counter}<default>");
                    counter = 0;
                    starttime = DateTime.Now;
                }
                Thread.Sleep(10);
                if (SystemConsole.KeyAvailable) closed = true;
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

            renderer.Closed += WindowClosed;
            renderer.MouseButtonChanged += MBChanged;
            renderer.Initialize(devs[display], rmode, RendererFlags.WaitRetrace, width, height, "OpenGL Test");
            if (verbose) SystemConsole.WriteLine($"glfw3.Window: <yellow>{rmode}<default>, <green>{width}<default>x<green>{height}<default>");

        }

        private void MBChanged(object sender, glfw3.MouseButtonEventArgs e)
        {
            if (e.State == glfw3.InputState.Press) closed = true;
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            closed = true;
        }
    }
}
