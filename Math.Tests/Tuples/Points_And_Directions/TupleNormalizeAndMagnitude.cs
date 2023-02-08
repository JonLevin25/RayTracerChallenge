using NUnit.Framework;

namespace Math.Tests.Tuples.Points_And_Directions
{
    [TestFixture]
    public class TestTupleNormalizeAndMagnitude
    {
        [TestCase(0,1,0,1)]
        [TestCase(0,0,1,1)]
        [TestCase(1,2,3, 3.741657f)]
        [TestCase(-1,-2,-3, 3.741657f)]
        public static void Test_Vector_Magnitude(float x, float y, float z, float expectedMagnitude)
        {
            var v = Tuple.Vector(0, 1, 0);
            Assert.AreEqual(1f, v.Magnitude);
        }

        [TestCase(4,0,0, 1,0,0)]
        [TestCase(1,2,3, 0.26726f, 0.53452f, 0.80178f)]
        public static void Test_Vector_Normalize(float x, float y, float z,
            float expectedX, float expectedY, float expectedZ)
        {
            var v = Tuple.Vector(x, y, z);
            Assert.AreEqual(Tuple.Vector(expectedX, expectedY, expectedZ), v.Normalized);
        }

        public static void Test_Normalized_Vector_Magnitude_Is_1()
        {
            var v = Tuple.Vector(1,2,3);
            Assert.AreEqual(1f, v.Normalized.Magnitude);
        }
    }
}