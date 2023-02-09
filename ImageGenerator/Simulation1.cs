using System;
using System.Collections.Generic;
using System.Linq;
using Drawing;
using ImageGenerator.ProjectileSimulation;
using Math;
using Tuple = Math.Tuple;

namespace ImageGenerator
{
    public static class Simulation1
    {
        public static void RunProjectileSimulation(Canvas canvas, Tuple color, int projectileSize)
        {
            var initPos = Tuple.Point(0, 0, 0);
            var initVel = Tuple.Vector(1, 1, 0);
            var projectile = new Projectile(initPos, initVel);

            var gravity = Tuple.Vector(0, -0.1f, 0);
            var wind = Tuple.Vector(-0.01f, 0, 0);
         
            var environment = new ProjectileEnvironment(gravity, wind, projectile);
            var sim = new ProjectileSim(projectile, environment);
            
            RunAndSaveImage(sim, canvas, color, projectileSize);
        }

        private static void RunAndSaveImage(ProjectileSim sim, Canvas canvas, Tuple color, int projectileDrawSize)
        {

            foreach (var projectile in sim.RunSimulation(TimeSpan.FromSeconds(5)))
            {
                var (centerX, centerY) = sim.GetCanvasPosition(projectile, canvas);
                var points = GetPoints(canvas, centerX, centerY, projectileDrawSize);

                foreach (var (x, y) in points)
                {
                    canvas.WritePixel(x, y, color);
                }
            }

            FileWriter.WriteImage(canvas);
        }

        private static IEnumerable<(int x, int y)> GetPoints(Canvas canvas, int centerX, int centerY, int circleSize)
        {
            var offset = (circleSize * 0.5f).FloorToInt();
            IEnumerable<int> Range(int center) => Enumerable.Range(center - offset, circleSize);

            var allXY = Range(centerX)
                .SelectMany(x => 
                    Range(centerY).Select(y => (x, y)));
            
            var points = allXY
                .Where(tup =>
                {
                    var (x, y) = tup;
                    return canvas.IsInBounds(x, y);
                });
            
            return points;
        }
    }
}