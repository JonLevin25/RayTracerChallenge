using System;

namespace Math
{
    public struct Tuple
    {
        public float X;
        public float Y;
        public float Z;
        public float W; // 1 for points, 0 for vectors

        public bool IsPoint => W == 1;
        public bool IsVector => W == 0;

        public float SqrMagnitude => MathF.Pow(X, 2) + MathF.Pow(Y, 2) + MathF.Pow(Z, 2) + MathF.Pow(W, 2);
        public float Magnitude => MathF.Sqrt(SqrMagnitude);
        public Tuple Normalized => this / Magnitude;

        public static Tuple Point(float x, float y, float z) => new Tuple(x, y, z, 1f);
        public static Tuple Vector(float x, float y, float z) => new Tuple(x, y, z, 0f);
        public Tuple(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public bool Equals(Tuple other)
        {
            var x = MathF.Abs(other.X - X);
            var y = MathF.Abs(other.Y - Y);
            var z = MathF.Abs(other.Z - Z);
            
            const float epsilon3 = 0.0003f;
            return x + y + z < epsilon3;
        }

        public override bool Equals(object obj) => obj is Tuple other && Equals(other);

        public static bool operator ==(Tuple a, Tuple b) => a.Equals(b);
        public static bool operator !=(Tuple a, Tuple b) => !(a == b);

        public static Tuple operator +(Tuple a, Tuple b) =>
            new Tuple(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        public static Tuple operator -(Tuple a, Tuple b) =>
            new Tuple(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        
        public static Tuple operator -(Tuple t) => new Tuple(-t.X, -t.Y, -t.Z, -t.W);

        public static Tuple operator *(Tuple t, float f) => ScalarMult(f, t);
        public static Tuple operator *(float f, Tuple t) => ScalarMult(f, t);
        public static Tuple operator /(Tuple t, float f) => ScalarMult(1.0f / f, t);

        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

        public override string ToString() => $"({X:0.00}, {Y:0.00}, {Z:0.00}, {W})";
        
        private static Tuple ScalarMult(float f, Tuple t) =>
            new Tuple(f * t.X, f * t.Y, f * t.Z, f * t.W);

        public static float Dot(Tuple a, Tuple b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

        public static Tuple Cross(Tuple a, Tuple b)
        {
            return new Tuple(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X,
                0f);
        }
    }
}