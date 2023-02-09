using System;

namespace Math
{
    public static class Extensions
    {
        public static bool ApproxEqual(this float a, float b)
        {
            const float epsilon = 0.00001f;
            return MathF.Abs(a - b) < epsilon;
        }
        
        public static bool ApproxEqual(this float a, float b, float epsilon)
        {
            return MathF.Abs(a - b) < epsilon;
        }

        public static int FloorToInt(this float f) => (int)MathF.Floor(f);
        public static int CeilToInt(this float f) => (int)MathF.Ceiling(f);

    }
}