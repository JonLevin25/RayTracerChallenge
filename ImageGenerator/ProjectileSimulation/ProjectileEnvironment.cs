using System;
using Drawing;
using Math;
using Tuple = Math.Tuple;

namespace ImageGenerator.ProjectileSimulation
{
    public readonly struct ProjectileEnvironment
    {
        public readonly Tuple Gravity;
        public readonly Tuple Wind;

        public readonly float EnvHeight;
        public readonly float EnvWidth;

        public Tuple TotalForce => Gravity + Wind;


        public ProjectileEnvironment(Tuple gravity, Tuple wind, Projectile initProjectile)
        {
            Gravity = gravity;
            Wind = wind;

            var (maxX, maxY) = GetMaxSimSizeDiscrete(gravity, wind, initProjectile.Position, initProjectile.Velocity);
            EnvWidth = maxX + 0.1f;
            EnvHeight = maxY + 0.1f;
        }
        
        public ProjectileEnvironment(Tuple gravity, Tuple wind, float width, float height)
        {
            Gravity = gravity;
            Wind = wind;

            EnvWidth = width;
            EnvHeight = height;
        }

        public (int x, int y) GetCanvasCoords(Tuple position, Canvas canvas)
        {
            var x = (position.X / EnvWidth * canvas.Width).FloorToInt();
            var y = canvas.Height - (position.Y / EnvHeight * canvas.Height).FloorToInt();

            var clampedX = System.Math.Clamp(x, 0, canvas.Width - 1);
            var clampedY = System.Math.Clamp(y, 0, canvas.Height - 1);
            
            return (clampedX, clampedY);
        }
        
        public bool IsInBounds(Tuple position)
        {
            if (position.X < 0 || position.X >= EnvWidth) return false;
            if (position.Y < 0 || position.Y >= EnvHeight) return false;
            return true;
        }

        public static (float maxX, float maxY) GetMaxSimSizeDiscrete(Tuple gravity, Tuple wind, Tuple initPosition,
            Tuple initVelocity)
        {
            // Math Variables (Arithmetic sum)
            var a = gravity + wind;
            var dy = a.Y;
            var dx = a.X;

            var vx0 = initVelocity.X;
            var vy0 = initVelocity.Y;

            var x0 = initPosition.X;
            var y0 = initPosition.Y;

            var peakHeightTicks = -vy0 / dy;
            var finalTicks = 2 * peakHeightTicks;

            var ySumDeltasAtPeak = 0.5f * peakHeightTicks * (2 * vy0 + (peakHeightTicks - 1) * dy);
            var xSumDeltasAtEnd = 0.5f * finalTicks * (2 * vx0 + (finalTicks - 1) * dx);
            
            var yMax = y0 + ySumDeltasAtPeak;
            var xMax = x0 + xSumDeltasAtEnd;

            return (xMax, yMax);
        }

        public static (float maxX, float maxY) GetMaxSimSizeContinuous(Tuple gravity, Tuple wind, Tuple initPosition,
            Tuple initVelocity)
        {
            // Math Variables
            var a = gravity + wind;
            var ay = a.Y;
            var ax = a.X;
            
            var vx0 = initVelocity.X;
            var vy0 = initVelocity.Y;
            
            var x0 = initPosition.X;
            var y0 = initPosition.Y;
            
            // Parabola peak
            var peakHeightTicks = -vy0 / ay;
            var finalTicks = 2 * peakHeightTicks;

            // kinematic formula
            var maxY = 0.5f * ay * MathF.Pow(peakHeightTicks, 2) + vy0 * peakHeightTicks + y0;
            var maxX = 0.5f * ax * MathF.Pow(finalTicks, 2) + vx0 * finalTicks + x0;
            return (maxX, maxY);
        }
    }
}