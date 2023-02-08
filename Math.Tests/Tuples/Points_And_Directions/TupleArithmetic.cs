using NUnit.Framework;

namespace Math.Tests.Tuples.Points_And_Directions
{
    [TestFixture]
    public class TupleArithmetic
    {
        [Test]
        public void Test_Adding_2_points()
        {
            var a2 = new Tuple(-2, 3, 1, 1); // Point
            var a1 = new Tuple(3, -1.33f, 1, 0); // Vector
            var res = a1 + a2;
            
            Assert.AreEqual(new Tuple(1, 1.67f, 2f, 1f), res);
            Assert.True(res.IsPoint);
        }
        
        [Test]
        public void Test_Subtracting_2_points()
        {
            var a1 = Tuple.Point(-2, 3, 1);
            var a2 = Tuple.Point(3, -1.33f, 1);
            var res = a1 - a2;
            
            Assert.AreEqual(Tuple.Vector(-5, 4.33f,0), res);
        }
        
        [Test]
        public void Test_Subtracting_Vector_From_Point()
        {
            var a1 = Tuple.Point(-2, 3, 1);
            var a2 = Tuple.Vector(3, -1.33f, 1);
            var res = a1 - a2;
            
            Assert.AreEqual(Tuple.Point(-5, 4.33f,0), res);
        }
        
        [Test]
        public void Test_Subtracting_2_Vectors()
        {
            var a1 = Tuple.Vector(-2, 3, 1);
            var a2 = Tuple.Vector(3, -1.33f, 1);
            var res = a1 - a2;
            
            Assert.AreEqual(Tuple.Vector(-5, 4.33f,0), res);
        }
        
        
        [Test]
        public void Test_Subtracting_From_Zero_Vector()
        {
            var a1 = Tuple.Vector(0, 0, 0);
            var a2 = Tuple.Vector(1, -2, -3);
            var res = a1 - a2;
            
            Assert.AreEqual(Tuple.Vector(-1, 2, 3), res);
        }

        [Test]
        public void Test_Negating_Vector()
        {
            var a = new Tuple(1, -2, 3, -4);
            var res = -a;
            
            Assert.AreEqual(new Tuple(-1, 2, -3, 4), res);
        }
        
        [Test]
        public static void Test_Scalar_Mult_Tuple()
        {
            var a = new Tuple(1, -2, 3, -4);
            var f = 3.5f;
            var expected = new Tuple(3.5f, -7f, 10.5f, -14f);
            Assert.AreEqual(expected, a*f);
            Assert.AreEqual(expected, f*a);
        }
        
        [Test]
        public static void Test_Scalar_Mult_Fraction()
        {
            var a = new Tuple(1, -2, 3, -4);
            var f = 0.5f;
            var expected = new Tuple(0.5f, -1f, 1.5f, -2f);
            Assert.AreEqual(expected, a*f);
            Assert.AreEqual(expected, f*a);
        }

    }
}