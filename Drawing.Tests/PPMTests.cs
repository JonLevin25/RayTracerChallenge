using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Utils;
using Tuple = Math.Tuple;

namespace Drawing.Tests
{
    public class PPMTests
    {
        private const int MetadataLineCount = 3;

        [Test]
        public void Test_PPM_Serialize_Empty_Canvas()
        {
            const int maxColorValue = 128;
            var canvas = new Canvas(20, 30);
            
            var str = PPMConverter.SerializeCanvas(canvas, maxColorValue);
            var lines = GetPPMLines(str);

            Assert.AreEqual("P3", lines[0]);
            Assert.AreEqual("20 30", lines[1]);
            Assert.AreEqual("128", lines[2]);
        }

        [Test]
        public void Test_PPM_Serialize_Canvas()
        {
            const int maxColorValue = 128;
            var canvas = new Canvas(20, 30);

            canvas.WritePixel(0, 0, Colors.Red);
            canvas.WritePixel(1, 1, Colors.Black);
            canvas.WritePixel(2, 1, Colors.Green);
            canvas.WritePixel(19, 29, Colors.Blue); // last pixel

            var str = PPMConverter.SerializeCanvas(canvas, maxColorValue);
            var lines = GetPPMLines(str);

            Assert.AreEqual("P3", lines[0]);
            Assert.AreEqual("20 30", lines[1]);
            Assert.AreEqual("128", lines[2]);
        }

        [Test]
        public void Test_PPM_Lines_Max_70_Lines()
        {
            const int maxColorValue = 255;
            var canvas1 = new Canvas(20, 30);
            var canvas2 = new Canvas(20, 30);
            canvas1.Fill(Colors.White);
            canvas2.Fill(Colors.Black);

            var ppm1 = PPMConverter.SerializeCanvas(canvas1, maxColorValue);
            var ppm2 = PPMConverter.SerializeCanvas(canvas2, maxColorValue);

            var linesLength1 = ppm1.Split(Environment.NewLine).Select(line => line.Length).ToArray();
            var linesLength2 = ppm2.Split(Environment.NewLine).Select(line => line.Length).ToArray();
            
            void AssertLengths(int[] lineLengths, string canvasName)
            {
                for (var i = 0; i < lineLengths.Length; i++)
                {
                    var lineLen = lineLengths[i];
                    Assert.LessOrEqual(70, lineLen, $"PPM Line is too long on {canvasName} (lineIdx: {i}. Length: {lineLen})");
                }
            }
            
            AssertLengths(linesLength1, nameof(canvas1));
            AssertLengths(linesLength2, nameof(canvas2));
        }

        [Test]
        public void Test_PPM_EndsWith_Newline()
        {
            const int maxColorValue = 255;
            var canvas = new Canvas(20, 30);
            var ppm = PPMConverter.SerializeCanvas(canvas, maxColorValue);
            Assert.True(ppm.EndsWith(Environment.NewLine));
        }

        [Test]
        public void Test_Canvas_To_PPM_Explicit()
        {
            const int maxColorValue = 255;
            var canvas = new Canvas(4, 8);
            canvas.WritePixel(0, 0, Colors.Red);
            canvas.WritePixel(1, 1, Colors.White);
            canvas.WritePixel(2, 2, Tuple.Color(1.5f, -1f, 0f));
            canvas.WritePixel(3, 7, Colors.Cyan);

            var str = PPMConverter.SerializeCanvas(canvas, maxColorValue);
            var lines = GetPPMLines(str);
            
            // assert metadata
            Assert.AreEqual("P3", lines[0]);
            Assert.AreEqual("4 8", lines[1]);
            Assert.AreEqual("255", lines[2]);

            // assert pixel data
            var pixelLines = GetPPMColorValues(str);
            Assert.AreEqual(
                new[]
                {
                    new[] {"255", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", },
                    new[] {"0", "0", "0", "255", "255", "255", "0", "0", "0", "0", "0", "0", },
                    new[] {"0", "0", "0", "0", "0", "0", "255", "0", "0", "0", "0", "0", },
                    new[] {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", },
                    new[] {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", },
                    new[] {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", },
                    new[] {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", },
                    new[] {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "255", "255", },
                },
                pixelLines);
        }

        [Test]
        public void Test_Serialize_Max_Color()
        {
            const int maxColorValue = 50;
            var canvas = new Canvas(2, 1);
            canvas.WritePixel(0, 0, Tuple.Color(1f, 1f, 1f));
            canvas.WritePixel(1, 0, Tuple.Color(0.25f, 0.5f, 0.66f));

            var str = PPMConverter.SerializeCanvas(canvas, maxColorValue);
            var lines = GetPPMLines(str);
            Assert.AreEqual(MetadataLineCount + 1, lines.Length);
            var pixelsLine = lines[MetadataLineCount];

            Assert.AreEqual("50 50 50 12 25 33", pixelsLine);
        }

        private static string[] GetPPMLines(string str)
        {
            // Ignore newline
            var lines = str
                .Split(Environment.NewLine);
            
            var endsWithEmptyLine = lines[^1].IsNullOrWhitespace();
            return endsWithEmptyLine 
                ? lines.SkipLast(1).ToArray()
                : lines;
        }

        private static string[] GetPPMColorValues(string ppm)
        {
            var lines = ppm
                .Split(Environment.NewLine);
            return lines
                .Skip(MetadataLineCount)
                .SelectMany(line => line.Split(' '))
                .ToArray();
        }
    }
}