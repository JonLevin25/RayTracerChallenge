using System;
using System.Collections.Generic;
using System.Threading;
using Drawing;
using Math;

namespace ImageGenerator.ProjectileSimulation
{
    public class ProjectileSim
    {
        private Projectile _projectile;
        private ProjectileEnvironment _env;
        private int _tickCount;

        public ProjectileSim(Projectile initProjectile, ProjectileEnvironment env)
        {
            _projectile = initProjectile;
            _env = env;
        }

        public (int x, int y) GetCanvasPosition(Projectile projectile, Canvas canvas) =>
            _env.GetCanvasCoords(projectile.Position, canvas);

        public IEnumerable<Projectile> RunSimulation(TimeSpan timeout)
        {
            var ct = new CancellationTokenSource(timeout).Token;

            Console.WriteLine($"Running Simulation! (Size: {_env.EnvWidth:0.0}x{_env.EnvHeight:0.0}) Timeout: {timeout})");
            Console.WriteLine($"Initial pos: {_projectile.Position}");
            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    Console.Error.WriteLine("Simulation timed out!");
                    yield break;
                }

                _projectile = Tick(_projectile, _env, ref _tickCount);
                if (!_env.IsInBounds(_projectile.Position)) break;
                yield return _projectile;
                Console.WriteLine($"Tick: {_tickCount:00000}\tPos: {_projectile.Position}");
                
            }

            Console.WriteLine($"Projectile went out of bounds! Finished after {_tickCount} ticks!");
        }

        private static Projectile Tick(Projectile projectile, ProjectileEnvironment env, ref int tickCount)
        {
            var position = projectile.Position + projectile.Velocity;
            var velocity = projectile.Velocity + env.TotalForce;

            tickCount++;
            return new Projectile(position, velocity);
        }
    }
}