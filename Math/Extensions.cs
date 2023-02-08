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
    }
}