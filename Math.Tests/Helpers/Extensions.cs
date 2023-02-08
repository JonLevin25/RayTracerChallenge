using System;
using NUnit.Framework;

namespace Math.Tests.Helpers
{
    public static class Extensions
    {
        public static void AssertFloatEqual(this float expected, float f, float epsilon = 0.00001f)
        {
            var delta = MathF.Abs(expected - f);
            Assert.LessOrEqual(delta, epsilon, $"Expected: {expected}\tGot: {f}\tEpsilon: {epsilon})");
        }
    }
}