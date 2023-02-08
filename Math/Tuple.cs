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

        public static Tuple Point(float x, float y, float z) => new Tuple(x, y, z, 1f);
        public static Tuple Vector(float x, float y, float z) => new Tuple(x, y, z, 0f);
        public Tuple(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public bool Equals(Tuple other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);
        public override bool Equals(object obj) => obj is Tuple other && Equals(other);

        public static bool operator ==(Tuple a, Tuple b) => a.Equals(b);
        public static bool operator !=(Tuple a, Tuple b) => !(a == b);

        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
    }
}