using System.Drawing;
using System.Drawing.Imaging;

namespace ShadowsOfInfinity
{
    public class Mandelbrot : BaseRenderer
    {
        public Mandelbrot()
        {
            Console.WriteLine("Rendering Mandelbrot");
        }

        public void RunWithOptions(MandelbrotOptions opts)
        {
            Console.WriteLine($"Width: {opts.Width}, Height: {opts.Height}");
            Console.WriteLine($"Iterations: {opts.Iterations}");

            int width = opts.Width;
            int height = opts.Height;
            Bitmap bmp = new Bitmap(width, height);  //I DO probably want this cross platform
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    bmp.SetPixel(i, j, Color.Black);
                }
            }

            var _xRes = opts.Width;
            var _yRes = opts.Height;
            var _minX = -2.0;
            var _maxX = 1.0;
            var _minY = -1.0;
            var _maxY = 1.0;

            var frameWidth = _maxX - _minX;
            var frameHeight = _maxY - _minY;
            var pixelWidth = frameWidth / _xRes;
            var pixelHeight = frameHeight / _yRes;

            for (int ix = 0; ix < _xRes; ix++)
            {
                for (int iy = 0; iy < _yRes; iy++)
                {
                    var pixelX = _minX + ((pixelWidth * ix) + (0.5 * pixelWidth));
                    var pixelY = _maxY - ((pixelHeight * iy) - (0.5 * pixelHeight));

                    double x = 0.0;
                    double y = 0.0;
                    var iterations = 0;

                    while ((x * x) + (y * y) <= 4.0 && iterations < opts.Iterations)
                    {
                        var xTemp = (x * x) - (y * y) + pixelX;
                        y = (2.0 * x * y) + pixelY;
                        x = xTemp;

                        iterations++;
                    }

                    Color color = Color.FromArgb(iterations % 255, iterations % 255, iterations % 255);
                    bmp.SetPixel(ix, iy, color);

                }
                Console.WriteLine($"X: {ix}");

            }

            bmp.Save($"mandelbrot{DateTime.Now.Ticks}.png", ImageFormat.Png);

        }

        public double Rand()
        {
            return Math.Round(new Random().Next(0, 1000000001) / 1000000000.0, 9);
        }

        // this normalizes the full iteration count on each pixel within a range of 0 - 255
        // and then pushes this to an image that can be sent to the HTML UI
        public void RenderPathDensity(int xRes, int yRes, Bitmap bmp, double[] pathDensitySpace)
        {
            var highest = 0.0;
            var lowest = 0.0;
            foreach (var d in pathDensitySpace)
            {
                if (highest < d) highest = d;
                if (lowest > d) lowest = d;
            }

            var index = 0;
            foreach (var d in pathDensitySpace)
            {
                var x = index % xRes;
                var y = (int)Math.Truncate((double)(index / xRes));

                var val = (int)((d * 255) / highest);
                bmp.SetPixel(x, y, Color.FromArgb(255, val, val, val));
                index += 1;
            }
        }
    }

}
