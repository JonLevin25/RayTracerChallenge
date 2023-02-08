using System;
using NUnit.Framework;

namespace Drawing.Tests
{
    public class Tests
    {
        [Test]
        public void Test_Create_Canvas()
        {
            var canvas = new Canvas(10, 20);
            Assert.AreEqual(10, canvas.Width);
            Assert.AreEqual(20, canvas.Height);
            foreach (var pixel in canvas.AllPixelsIterator)
            {
                Assert.AreEqual(Colors.Black, pixel);
            }
        }

        [Test]
        public void Test_Write_Pixels_To_Canvas()
        {
            var canvas = new Canvas(10, 20);
            canvas.WritePixel(0, 0, Colors.Red);
            canvas.WritePixel(2, 3, Colors.Green);
            canvas.WritePixel(9, 11, Colors.Cyan);
            Assert.AreEqual(Colors.Red, canvas.PixelAt(0,0));
            Assert.AreEqual(Colors.Green, canvas.PixelAt(2,3));
            Assert.AreEqual(Colors.Cyan, canvas.PixelAt(9, 11));
        }
        
        [Test]
        public void Test_Contract_Canvas()
        {
            var canvas = new Canvas(10, 20);
            canvas.WritePixel(4, 2, Colors.Red);
            
            Assert.DoesNotThrow(() => canvas.PixelAt(0, 3));
            Assert.DoesNotThrow(() => canvas.PixelAt(0, 3));
            
            canvas.Width = 5;
            canvas.Height = 3;
            
            Assert.AreEqual(5, canvas.Width);
            Assert.AreEqual(3, canvas.Height);

            Assert.AreEqual(Colors.Red, canvas.PixelAt(4, 2));
            Assert.Throws<ArgumentOutOfRangeException>(() => canvas.PixelAt(5, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => canvas.PixelAt(0, 3));
        }
        
        [Test]
        public void Test_Expand_Canvas()
        {
            var canvas = new Canvas(10, 20);
            canvas.WritePixel(4, 2, Colors.Red);
            canvas.Fill(Colors.Red);

            foreach (var color in canvas.AllPixelsIterator)
            {
                Assert.AreEqual(Colors.Red, color);
            }
            
            canvas.Width = 50;
            canvas.Height = 100;
            
            Assert.AreEqual(50, canvas.Width);
            Assert.AreEqual(100, canvas.Height);

            for (var x = 0; x < canvas.Width; x++)
            {
                for (var y = 0; y < canvas.Height; y++)
                {
                    var pixel = canvas.PixelAt(x, y);
                    var isNewPixel = x > 9 || y > 19;
                    
                    if (isNewPixel) Assert.AreEqual(Colors.Black, pixel);
                    else Assert.AreEqual(Colors.Red, pixel);
                }
            }
            
            Assert.DoesNotThrow(() => canvas.PixelAt(49, 99));
        }
        
    }
}