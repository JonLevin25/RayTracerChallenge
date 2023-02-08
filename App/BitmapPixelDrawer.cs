using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RayTracerChallenge
{
        public class BitmapPixelDrawer : Window
    {
        private readonly WriteableBitmap _writeableBitmap;
        private readonly Image _image;
        private readonly Window _window;
        public int Width { get; }
        public int Height { get; }


        public void Fill(int r, int g, int b)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    DrawPixel(x,y,r,g,b);
                }
            }
        }
        public BitmapPixelDrawer(Window window)
        {
            _window = window;
            _image = new Image();
            RenderOptions.SetBitmapScalingMode(_image, BitmapScalingMode.NearestNeighbor);
            RenderOptions.SetEdgeMode(_image, EdgeMode.Aliased);
            
            _window.Content = _image;
            _window.Show();

            Width = (int)_window.ActualWidth;
            Height = (int)_window.ActualHeight;

            _writeableBitmap = new WriteableBitmap(
                Width,
                Height,
                96,
                96,
                PixelFormats.Bgr32,
                null);

            _image.Source = _writeableBitmap;

            _image.Stretch = Stretch.None;
            _image.HorizontalAlignment = HorizontalAlignment.Left;
            _image.VerticalAlignment = VerticalAlignment.Top;
        }

        // The DrawPixel method updates the WriteableBitmap by using
        // unsafe code to write a pixel into the back buffer.
        public void DrawPixel(int x, int y, int r, int g, int b)
        {
            try
            {
                // Reserve the back buffer for updates.
                _writeableBitmap.Lock();
                unsafe
                {
                    // Get a pointer to the back buffer.
                    IntPtr pBackBuffer = _writeableBitmap.BackBuffer;

                    // Find the address of the pixel to draw.
                    pBackBuffer += y * _writeableBitmap.BackBufferStride;
                    pBackBuffer += x * 4;

                    // Compute the pixel's color.
                    int color_data = r << 16; // R
                    color_data |= g << 8; // G
                    color_data |= b << 0; // B

                    // Assign the color data to the pixel.
                    *((int*)pBackBuffer) = color_data;
                }

                // Specify the area of the bitmap that changed.
                _writeableBitmap.AddDirtyRect(new Int32Rect(x, y, 1, 1));
            }
            finally
            {
                // Release the back buffer and make it available for display.
                _writeableBitmap.Unlock();
            }
        }
    }
}