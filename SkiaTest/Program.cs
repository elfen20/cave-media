using Cave.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace SkiaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("no file specified to check.");
                return;
            }

            string fileName = args[0];
            if (!File.Exists(fileName))
            {
                Console.WriteLine($"file {fileName} not found!");
                return;
            }
            Console.WriteLine("starting test, loading skia...");
            Bitmap32.Loader = new SkiaBitmap32Loader();
            Console.WriteLine("loading file...");
            Bitmap32 b = Bitmap32.FromFile(fileName);
            Console.WriteLine("file loaded!");
            Console.WriteLine("Info:");
            Console.WriteLine($"Bitmap size: {b.Width},{b.Height}");
            Console.WriteLine("Detect max 4 Colors...");
            IList<ARGB> colors = b.DetectColors(4);
            foreach (var color in colors)
            {
                Console.WriteLine($"  {color}");
            }
            Console.WriteLine("finished.");
        }
    }
}
