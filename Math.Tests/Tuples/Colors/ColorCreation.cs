using Math.Tests.Helpers;
using NUnit.Framework;

namespace Math.Tests.Tuples.Colors
{
    [TestFixture]
    public class ColorCreation
    {
        [Test]
        public void Test_Colors_Are_RGB_Tuples()
        {
            var a = Tuple.Color(-0.5f, 1.7f, 0.4f);
            (-0.5f).AssertFloatEqual(a.R);
            (1.7f).AssertFloatEqual(a.G);
            (0.4f).AssertFloatEqual(a.B);
            // Assert.True(a.IsColor);
        }
    }
}