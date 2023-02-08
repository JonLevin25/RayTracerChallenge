using System;
using System.Linq;
using System.Text;
using Math;
using Utils;
using Tuple = Math.Tuple;

namespace Drawing
{
    public static class PPMConverter
    {
        private const string Header = "P3";
        
        // public static Canvas ParseToCanvas(string serializedPPM)
        // {
        //     var lines = serializedPPM.Replace("\r", "").Split('\n');
        //     var header = lines[0];
        //
        //     if (lines[0] != Header)
        //     {
        //         Console.Error.WriteLine($"PPM: Wrong header detected! ({header}, expected: {Header})");
        //     }
        //
        //     var size = lines[1].Split(' ');
        //     var w = int.Parse(size[0]);
        //     var h = int.Parse(size[1]);
        //
        //     var maxColorVal = int.Parse(lines[2]);
        //     // TODO: parse colors
        //     return new Canvas(w, h);
        // }

        public static string SerializeCanvas(Canvas canvas, int maxColorValue)
        {
            var sb = new StringBuilder();
            sb.AppendLine(Header);
            sb.Append(canvas.Width);
            sb.Append(' ');
            sb.Append(canvas.Height);
            sb.AppendLine();
            sb.AppendLine(maxColorValue.ToString());
            
            var unwrappedPixelsStr = Enumerable.Range(0, canvas.Height)
                .Select(y => SerializeLine(canvas, maxColorValue, y))
                .JoinStr(' ');
            
            var pixelStr = WrapStr(unwrappedPixelsStr, 70);
            sb.Append(pixelStr);
            
            return sb.ToString();
        }

        private static string WrapStr(string str, int maxChars, char validReplacementChar = ' ')
        {
            string GetSubstrInclusive(int startInclusive, int endInclusive)
            {
                var len = endInclusive - startInclusive + 1;
                return str.Substring(startInclusive, len);
            }
            
            var lastIdxReached = 0;
            var sb = new StringBuilder(str.Length + str.Length / maxChars + 1);
            
            for (var i = maxChars; i < str.Length; i+= maxChars)
            {
                // FindReplacementCharBefore
                while (str[i] != validReplacementChar)
                {
                    i--;
                    if (lastIdxReached == i)
                    {
                        throw new ArgumentException(
                            $"Cannot Wrap string! could not find valid replacement char ('{validReplacementChar}') such that {maxChars}char wrapping is possible.");
                    }
                }
                
                sb.AppendLine(GetSubstrInclusive(lastIdxReached, i));
                
                lastIdxReached = i + 1;
            }

            if (lastIdxReached < str.Length - 1)
            {
                sb.AppendLine(GetSubstrInclusive(lastIdxReached, str.Length - 1));
            }

            return sb.ToString();
        }

        private static string SerializeLine(Canvas canvas, int maxColorValue, int y)
        {
            return Enumerable.Range(0, canvas.Width)
                .Select(x => canvas.PixelAt(x, y))
                .Select(pixel => ColorStr(pixel, maxColorValue))
                .JoinStr(' ');
        }

        private static string ColorStr(Tuple pixel, int maxColorValue)
        {
            string Str(float colorVal) => GetColor(colorVal, maxColorValue).ToString();
            return $"{Str(pixel.R)} {Str(pixel.G)} {Str(pixel.B)}";
        }
        
        private static int GetColor(float floatValue, int maxColorValue)
        {
            var intVal = (floatValue * maxColorValue).FloorToInt();
            return System.Math.Clamp(intVal, 0, maxColorValue);
        }
    }
}