using System;
using System.Collections.Generic;
using System.Linq;
using Drawing;
using ImageGenerator.ProjectileSimulation;
using Math;
using Tuple = Math.Tuple;

namespace ImageGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var canvas = new Canvas(600, 400);
            // SimpleRedImage(canvas);
            Simulation1.RunProjectileSimulation(canvas, Colors.Green, 10);
        }

        private static void SimpleRedImage(Canvas canvas)
        {
            Console.WriteLine("Hello, red!");
            canvas.Fill(Colors.Red);
            FileWriter.WriteImage(canvas);
        }
    }
}