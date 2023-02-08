using NUnit.Framework;

namespace Math.Tests.Tuples
{
    [TestFixture]
    public class TupleCreation
    {
        [Test]
        public void Test_Tuple_With_W_0_Is_Point()
        {
            var a = new Tuple(4.3f, -4.2f, 3.1f, 1.0f);
            Assert.AreEqual(4.3f, a.X);
            Assert.AreEqual(-4.2f, a.Y);
            Assert.AreEqual(3.1f, a.Z);
            Assert.AreEqual(1.0f, a.W);
            
            Assert.True(a.IsPoint);
            Assert.True(!a.IsVector);
        }
        
        [Test]
        public void Test_Tuple_With_W_1_Is_Vector()
        {
            var a = new Tuple(4.3f, -4.2f, 3.1f, 0.0f);
            Assert.AreEqual(4.3f, a.X);
            Assert.AreEqual(-4.2f, a.Y);
            Assert.AreEqual(3.1f, a.Z);
            Assert.AreEqual(0.0f, a.W);
            
            Assert.True(!a.IsPoint);
            Assert.True(a.IsVector);
        }

        [Test]
        public void Test_Point_Creates_Tuples_With_W_1()
        {
            var p = Tuple.Point(4, -4, 3);
            Assert.AreEqual(p, new Tuple(4f,-4f,3f,1f));
        } 
        
        [Test]
        public void Test_Vector_Creates_Tuples_With_W_0()
        {
            var p = Tuple.Vector(4, -4, 3);
            Assert.AreEqual(p, new Tuple(4f,-4f,3f,0f));
        }
    }
}