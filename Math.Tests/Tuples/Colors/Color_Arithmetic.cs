using Math.Tests.Helpers;
using NUnit.Framework;

namespace Math.Tests.Tuples.Colors
{
    [TestFixture]
    public class ColorArithmetic
    {
        [Test]
        public void Test_Adding_2_Colors()
        {
            var a = Tuple.Color(0.4f, 0.2f, 0.55f);
            var b = Tuple.Color(0.3f, 0.15f, 0.21f);
            var res = a + b;
            
            Assert.AreEqual(Tuple.Color(0.7f, 0.35f, 0.76f), res);
            // Assert.True(res.IsColor);
        }
        
        [Test]
        public void Test_Subtracting_2_Colors()
        {
            var a = Tuple.Color(0.4f, 0.2f, 0.55f);
            var b = Tuple.Color(0.3f, 0.15f, 0.21f);
            var res = a - b;
            
            Assert.AreEqual(Tuple.Color(0.1f, 0.05f, 0.34f), res);
            // Assert.True(res.IsColor);
        }

        [Test]
        public static void Test_Scalar_Mult_Color()
        {
            var a = Tuple.Color(0.4f, 0.2f, 0.55f);
            var res = 2f * a;
            
            Assert.AreEqual(Tuple.Color(0.8f, 0.4f, 1.1f), res);
            // Assert.True(res.IsColor);
        }
        
        [Test]
        public static void Test_Multiplying_Colors()
        {
            var a = Tuple.Color(0.4f, 0.2f, 0.55f);
            var b = Tuple.Color(0.3f, 0.15f, 0.21f);
            var res1 = a * b;
            var res2 = b * a;
            var expected = Tuple.Color(0.12f, 0.03f, 0.1155f);
            
            Assert.AreEqual(expected, res1);
            Assert.AreEqual(expected, res2);
        }
    }
}