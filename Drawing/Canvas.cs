using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using Tuple = Math.Tuple;

namespace Drawing
{
    // TODO: Connect this to actual BitmapPixelDrawer
    public class Canvas
    {
        private int _w;
        private int _h;
        
        public int Width
        {
            get => _w;
            set
            {
                _w = value;
                Pixels.SetLength(value, _ => ListGenerator(Height));
            }
        }

        public int Height
        {
            get => _h;
            set
            {
                if (_h == value) return;
                _h = value;
                for (var x = 0; x < Width; x++)
                {
                    var column = Pixels[x];
                    column.SetLength(value);
                }
            }
        }
        
        private List<List<Tuple>> Pixels { get; }
        
        public IEnumerable<Tuple> AllPixelsIterator
        {
            get
            {
                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        yield return PixelAt(x, y);
                    }
                }
            }
        }

        public Tuple PixelAt(int x, int y)
        {
            return Pixels[x][y];
        }

        public Canvas(int width, int height)
        {
            _w = width;
            _h = height;
            
            Pixels = Enumerable.Repeat(0, width)
                .Select(_ => ListGenerator(Height))
                .ToList();
        }

        public void WritePixel(int x, int y, Tuple color) => Pixels[x][y] = color;

        private static List<Tuple> ListGenerator(int count) =>
            Enumerable.Repeat(Colors.Black, count).ToList();

        public void Fill(Tuple color)
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    Pixels[x][y] = color;
                }
            }
        }
    }
}