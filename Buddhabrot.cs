using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace ShadowsOfInfinity
{
    public class Buddhabrot
    {
        public void RunWithOptions(BuddhabrotOptions opts)
        {
            var details = opts.Details;
            var sampleCount = opts.Samples;
            var _xRes = opts.Width;
            var _yRes = opts.Height + 1; //These adjustments are to eliminate a strip of black that I'm not sure why occures

            var updateRate = sampleCount / 100;
            if (updateRate <= 0) updateRate = 1;

            var split = opts.Range.Split('-');
            var startRange = Convert.ToInt32(split[0]);
            var endRange = Convert.ToInt32(split[1]);

            if (startRange > endRange)
            {
                Console.WriteLine($"Cannot have a backwards range.  Use as follows: '-r 3-10'.  Quitting...");
                return;
            }
            if (startRange < 2)
            {
                Console.WriteLine($"Cannot have a start range lower than 2.  Quitting...");
                return;
            }

            Console.WriteLine($"Width: {opts.Width}, Height: {opts.Height}");
            Console.WriteLine($"Samples {sampleCount}");

            for (int iterationCount = startRange; iterationCount <= endRange; iterationCount++)
            {
                Console.WriteLine($"Rendering Buddhabrot with iteration count of: {iterationCount}");
                Console.WriteLine($"");  //This is important to render the progress correctly

                var _minX = -2.0;
                var _maxX = 1.0;
                var _minY = -1.0;
                var _maxY = 1.0;
                var frameWidth = _maxX - _minX;
                var frameHeight = _maxY - _minY;
                var pixelWidth = frameWidth / _xRes;
                var pixelHeight = frameHeight / _yRes;

                int[,] histogram = new int[_xRes, _yRes];

                for (var s = 0; s < sampleCount; s++)
                {
                    //Console.WriteLine($"Sample: {s}");

                    //var asdf = Rand(2);
                    var pixelX = (Rand() * frameWidth) + _minX;
                    var pixelY = (Rand() * frameHeight) + _minY;

                    // Iterate over each pixel
                    Complex c = new Complex(pixelX, pixelY);
                    Complex z = 0;
                    int iterations = 0;

                    double x = 0.0;
                    double y = 0.0;

                    var stops = new List<(int, int)>();

                    while (((x * x) + (y * y) <= 4.0) && iterations < iterationCount)
                    {
                        var xTemp = (x * x) - (y * y) + pixelX;
                        y = (2.0 * x * y) + pixelY;
                        x = xTemp;

                        // Convert z to pixel coordinates
                        // int zx = (int)((z.Real + 2) * scale);
                        //int zy = (int)((z.Imaginary + 2) * scale);

                        var zx = (x - _minX - (0.5 * pixelWidth)) / pixelWidth;
                        zx = Math.Round(zx, 0);

                        var zy = (_maxY - y + (0.5 * pixelHeight)) / pixelHeight;
                        zy = Math.Round(zy, 0);

                        if (zx >= 0 && zx < _xRes && zy >= 0 && zy < _yRes)
                            stops.Add(((int)zx, (int)zy));

                        iterations++;
                    }

                    //DO NOT INCLUDE 
                    if (iterations != iterationCount)
                        foreach (var stop in stops)
                            histogram[stop.Item1, stop.Item2] += 1;

                    // every 10k samples, log progress
                    if (s % updateRate == 0)
                    {
                        var progress = Math.Round((double)(s + 1) / sampleCount * 100);

                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                        Console.WriteLine($"Render progress: {progress + 1}% ...");
                    }
                }

                var highest = 0.0;
                for (int x = 0; x < _xRes; x++)
                    for (int y = 0; y < _yRes; y++)
                    {
                        var count = histogram[x, y];
                        if (count > highest) highest = count;
                    }

                var bmp = new Bitmap(_xRes, _yRes - 1);  //I DO probably want this cross platform
                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {

                        bmp.SetPixel(i, j, Color.Black);
                    }
                }

                for (int x = 0; x < _xRes; x++)
                {
                    for (int y = 0; y < _yRes; y++)
                    {
                        if (y == 0)
                            continue;

                        var brightness = histogram[x, y];
                        var rgb = (int)Math.Round((double)((brightness * 255) / highest));
                        Color color = Color.FromArgb(rgb, rgb, rgb);
                        // Color color = Color.FromArgb(valR, valG, valB);
                        bmp.SetPixel(y - 1, x, color);
                    }
                }

                bmp.Save($"buddhabrot_iters_{iterationCount.ToString("0.##E+00")}_smpls_{sampleCount.ToString("0.##E+00")}.png", ImageFormat.Png);
            }
        }

        public double Rand()
        {
            return Math.Round(new Random().Next(0, 1000000001) / 1000000000.0, 9);
        }
    }
}
