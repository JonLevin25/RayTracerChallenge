using Drawing;
using Math;

namespace ImageGenerator.ProjectileSimulation
{
    public readonly struct Projectile
    {
        public readonly Tuple Position;
        public readonly Tuple Velocity;

        public Projectile(Tuple position, Tuple velocity)
        {
            Position = position;
            Velocity = velocity;
        }
    }
}