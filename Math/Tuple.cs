using System;

namespace Math
{
    public struct Tuple
    {
        private float _x;
        private float _y;
        private float _z;
        private float _w; // 1 for points, 0 for vectors
        
        public float X
        {
            get => _x;
            set => _x = value;
        }
        public float Y
        {
            get => _y;
            set => _y = value;
        }
        public float Z
        {
            get => _z;
            set => _z = value;
        }
        
        // 1 for points, 0 for vectors
        public float W
        {
            get => _w;
            set => _w = value;
        }

        public float R
        {
            get => _x;
            set => _x = value;
        }
        public float G
        {
            get => _y;
            set => _y = value;
        }
        public float B
        {
            get => _z;
            set => _z = value;
        }

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        public bool IsPoint => W == 1f;
        public bool IsVector => W == 0;

        public float SqrMagnitude => MathF.Pow(X, 2) + MathF.Pow(Y, 2) + MathF.Pow(Z, 2) + MathF.Pow(W, 2);
        public float Magnitude => MathF.Sqrt(SqrMagnitude);
        public Tuple Normalized => this / Magnitude;

        public static Tuple Point(float x, float y, float z) => new Tuple(x, y, z, 1f);
        public static Tuple Vector(float x, float y, float z) => new Tuple(x, y, z, 0f);
        public static Tuple Color(float x, float y, float z) => new Tuple(x, y, z, 1f); // TODO: W=1 ok? (Colors usually more "points" than "vectors")
        
        public Tuple(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }

        // Equality / Hashing
        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
        public bool Equals(Tuple other)
        {
            var x = MathF.Abs(other.X - X);
            var y = MathF.Abs(other.Y - Y);
            var z = MathF.Abs(other.Z - Z);
            
            const float epsilon3 = 0.003f;
            return x + y + z < epsilon3;
        }
        public override bool Equals(object obj) => obj is Tuple other && Equals(other);

        // String repr
        public override string ToString() => $"({X:0.00}, {Y:0.00}, {Z:0.00}, {W})";

        // Equality
        public static bool operator ==(Tuple a, Tuple b) => a.Equals(b);
        public static bool operator !=(Tuple a, Tuple b) => !(a == b);

        // Add/Subtract
        public static Tuple operator +(Tuple a, Tuple b) =>
            new Tuple(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        public static Tuple operator -(Tuple a, Tuple b) =>
            new Tuple(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        
        // Negation
        public static Tuple operator -(Tuple t) => new Tuple(-t.X, -t.Y, -t.Z, -t.W);

        // Scalar Multiplication
        public static Tuple operator *(Tuple t, float f) => ScalarMult(f, t);
        public static Tuple operator *(float f, Tuple t) => ScalarMult(f, t);
        public static Tuple operator /(Tuple t, float f) => ScalarMult(1.0f / f, t);

        // Hadamard / Schur product
        public static Tuple operator *(Tuple a, Tuple b) =>
            new Tuple(a.X * b.X, a.Y * b.Y, a.Z * b.Z, a.W * b.W);
        
        
        // Dot / cross products
        public static float Dot(Tuple a, Tuple b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z + a.W * b.W;

        public static Tuple Cross(Tuple a, Tuple b)
        {
            return new Tuple(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X,
                0f);
        }

        private static Tuple ScalarMult(float f, Tuple t) =>
            new Tuple(f * t.X, f * t.Y, f * t.Z, f * t.W);
    }
}