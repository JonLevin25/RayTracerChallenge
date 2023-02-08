using System;
using NUnit.Framework;

namespace Math.Tests.Tuples
{
    [TestFixture]
    public class TupleDotAndCrossProducts
    {
        [Test]
        public void Test_Dot_Of_Two_Tuples()
        {
            var a = Tuple.Vector(1, 2, 3);
            var b = Tuple.Vector(2,3,4);
            Assert.AreEqual(20f, Tuple.Dot(a, b));
        }
        
        [Test]
        public void Test_Cross_Of_Two_Tuples()
        {
            var a = Tuple.Vector(1, 2, 3);
            var b = Tuple.Vector(2,3,4);
            Assert.AreEqual(Tuple.Vector(-1, 2, -1), Tuple.Cross(a, b));
            Assert.AreEqual(Tuple.Vector(1,-2, 1), Tuple.Cross(b, a));
        } 
    }
}