using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowsOfInfinity
{
    public class Nebulabrot : BaseRenderer
    {
        public Nebulabrot()
        {
            Console.WriteLine("Rendering Nebulabrot");
        }

        public void RunWithOptions(NebulabrotOptions opts)
        {
            var sampleCount = opts.Samples;
            var _xRes = opts.Width;
            var _yRes = opts.Height + 1; //These adjustments are to eliminate a strip of black that I'm not sure why occures

            var updateRate = sampleCount / 100;
            if (updateRate <= 0) updateRate = 1;

            char a, b, c;
            if (opts.Order == null)
            {
                a = 'b'; b = 'g'; c = 'r';
            }
            else if (opts.Order.Length != 3)
            {
                Console.WriteLine("Oder must be exactly 3 characters, and must include 'r', 'g', and 'b'");
                return;
            }
            else
            {
                a = opts.Order[0];
                b = opts.Order[1];
                c = opts.Order[2];
            }

            Console.WriteLine($"Width: {opts.Width}, Height: {opts.Height}");
            Console.WriteLine($"Samples {sampleCount}");

            var _minX = -2.0;
            var _maxX = 1.0;
            var _minY = -1.0;
            var _maxY = 1.0;
            var frameWidth = _maxX - _minX;
            var frameHeight = _maxY - _minY;
            var pixelWidth = frameWidth / _xRes;
            var pixelHeight = frameHeight / _yRes;

            double[,] histogramA = new double[_xRes, _yRes];
            double[,] histogramB = new double[_xRes, _yRes];
            double[,] histogramC = new double[_xRes, _yRes];

            var maxA = 0.0;
            var maxB = 0.0;
            var maxC = 0.0;

            for (int colorFactor = 0; colorFactor < 3; colorFactor++)
            {
                var iterationCount = Math.Pow(10, (colorFactor + 2));
                for (var s = 0; s < sampleCount; s++)
                {
                    var pixelX = (Rand() * frameWidth) + _minX;
                    var pixelY = (Rand() * frameHeight) + _minY;

                    // Iterate over each pixel
                    int iterations = 0;

                    double x = 0.0;
                    double y = 0.0;

                    var stops = new List<(int, int)>();

                    while (((x * x) + (y * y) <= 4.0) && iterations < iterationCount)
                    {
                        var xTemp = (x * x) - (y * y) + pixelX;
                        y = (2.0 * x * y) + pixelY;
                        x = xTemp;

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
                        {
                            switch (colorFactor)
                            {
                                case 0:
                                    histogramA[stop.Item1, stop.Item2] += 1;
                                    break;
                                case 1:
                                    histogramB[stop.Item1, stop.Item2] += 1;
                                    break;
                                case 2:
                                    histogramC[stop.Item1, stop.Item2] += 1;
                                    break;
                            }
                        }

                    // every 10k samples, log progress
                    if (s % updateRate == 0)
                    {
                        var progress = Math.Round((double)(s + 1) / sampleCount * 100);

                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                        Console.WriteLine($"Render progress: {colorFactor + 1}/3 {progress + 1}% ...");
                    }
                }

                var count = 0.0;
                for (int x = 0; x < _xRes; x++)
                    for (int y = 0; y < _yRes; y++)
                    {
                        switch (colorFactor)
                        {
                            case 0:
                                count = histogramA[x, y];
                                if (count > maxA) maxA = count;
                                break;
                            case 1:
                                count = histogramB[x, y];
                                if (count > maxB) maxB = count;
                                break;
                            case 2:
                                count = histogramC[x, y];
                                if (count > maxC) maxC = count;
                                break;
                        }
                    }
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

                    var brightnessA = histogramA[x, y];
                    var brightnessB = histogramB[x, y];
                    var brightnessC = histogramC[x, y];

                    var brightnessRed = 0.0;
                    var brightnessGreen = 0.0;
                    var brightnessBlue = 0.0;

                    var maxRed = 0.0;
                    var maxGreen = 0.0;
                    var maxBlue = 0.0;
                    switch (a)
                    {
                        case 'r': brightnessRed = brightnessA; maxRed = maxA; break;
                        case 'g': brightnessGreen = brightnessA; maxGreen = maxA; break;
                        case 'b': brightnessBlue = brightnessA; maxBlue = maxA; break;
                    }

                    switch (b)
                    {
                        case 'r': brightnessRed = brightnessB; maxRed = maxB; break;
                        case 'g': brightnessGreen = brightnessB; maxGreen = maxB; break;
                        case 'b': brightnessBlue = brightnessB; maxBlue = maxB; break;
                    }

                    switch (c)
                    {
                        case 'r': brightnessRed = brightnessC; maxRed = maxC; break;
                        case 'g': brightnessGreen = brightnessC; maxGreen = maxC; break;
                        case 'b': brightnessBlue = brightnessC; maxBlue = maxC; break;
                    }


                    var red = Normalize(brightnessRed, maxRed);
                    var green = Normalize(brightnessGreen, maxGreen);
                    var blue = Normalize(brightnessBlue, maxBlue);
                    Color color = Color.FromArgb(red, green, blue);
                    bmp.SetPixel(y - 1, x, color);
                }
            }

            bmp.Save($"nebulabrot{Guid.NewGuid()}.png", ImageFormat.Png);
        }

        public int Normalize(double brightness, double highest)
        {
            return (int)Math.Round(((brightness + 1) * 255) / (highest + 1));
        }

        public double Rand()
        {
            return Math.Round(new Random().Next(0, 1000000001) / 1000000000.0, 9);
        }
    }
}
