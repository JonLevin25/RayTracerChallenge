using System;
using Drawing;

namespace ImageGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, red!");
            var canvas = new Canvas(600, 400);
            canvas.Fill(Colors.Red);
            FileWriter.WriteImage(canvas);
        }
    }
}