﻿namespace ShadowsOfInfinity.Exe
{
    public class Visagebrot : BaseRenderer
    {
        private VisagebrotOptions _opts;

        public Visagebrot()
        {
            Console.WriteLine("Rendering Visagebrot");
        }

        public override string RenderFileName()
        {
            return $"visagebrot_s_{_opts.Samples}_b_{_opts.Band}_c_{_opts.Cycles}_{GetTimestamp()}.{_imageFormat.ToString().ToLower()}";
        }

        public void RunWithOptions(VisagebrotOptions opts)
        {
            _opts = opts;
            var sampleCount = opts.Samples;
            var _xRes = opts.Width;
            var _yRes = opts.Height + 1; //These adjustments are to eliminate a strip of black that I'm not sure why occures

            var split = opts.Band.Split('-');
            var startBand = Convert.ToInt32(split[0]);
            var endBand = Convert.ToInt32(split[1]);

            if (startBand > endBand)
            {
                Console.WriteLine($"Cannot have a backwards band.  Use as follows: '-b 1000-100000'.  Quitting...");
                return;
            }
            if (startBand < 2)
            {
                Console.WriteLine($"Cannot have a start band lower than 2.  Quitting...");
                return;
            }

            var updateRate = sampleCount / 100;
            if (updateRate <= 0) updateRate = 1;

            Console.WriteLine($"Width: {opts.Width}, Height: {opts.Height}");
            Console.WriteLine($"Samples: {sampleCount}");
            Console.WriteLine($"Iteration Band: {opts.Band}");
            Console.WriteLine($"Cycles: {opts.Cycles}");

            Console.WriteLine($"");  //This is important to render the progress correctly

            var _minX = -2.0;
            var _maxX = 1.0;
            var _minY = -1.0;
            var _maxY = 1.0;
            var frameWidth = _maxX - _minX;
            var frameHeight = _maxY - _minY;
            var pixelWidth = frameWidth / _xRes;
            var pixelHeight = frameHeight / _yRes;

            double[,] histogram = new double[_xRes, _yRes];

            for (int x = 0; x < _xRes; x++)
                for (int y = 0; y < _yRes; y++)
                    histogram[x, y] = 1;

            InitBitmap(_xRes, _yRes - 1, 0, 0, 0);

            for (var cycles = 0; cycles < opts.Cycles; cycles++)
            {
                for (var s = 0; s < sampleCount; s++)
                {
                    var pixelX = Rand() * frameWidth + _minX;
                    var pixelY = Rand() * frameHeight + _minY;

                    // Iterate over each pixel
                    int iterations = 0;

                    double x = 0.0;
                    double y = 0.0;

                    var stops = new List<(int, int)>();

                    while (x * x + y * y <= 4.0 && iterations < endBand)
                    {
                        var xTemp = x * x - y * y + pixelX;
                        y = 2.0 * x * y + pixelY;
                        x = xTemp;

                        var zx = (x - _minX - 0.5 * pixelWidth) / pixelWidth;
                        zx = Math.Round(zx, 0);

                        var zy = (_maxY - y + 0.5 * pixelHeight) / pixelHeight;
                        zy = Math.Round(zy, 0);

                        if (zx >= 0 && zx < _xRes && zy >= 0 && zy < _yRes)
                            stops.Add(((int)zx, (int)zy));

                        iterations++;
                    }

                    //Only capture trajectories that escape 
                    if (iterations > startBand && iterations < endBand)
                        foreach (var stop in stops)
                            histogram[stop.Item1, stop.Item2] += 1;

                    // every 10k samples, log progress
                    if (s % updateRate == 0)
                    {
                        var progress = Math.Round((double)(s + 1) / sampleCount * 100);

                        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                        Console.WriteLine($"Render progress: {cycles + 1}/{opts.Cycles} {progress + 1}% ...");
                    }
                }
            }

            var max = 0.0;
            for (int x = 0; x < _xRes; x++)
                for (int y = 0; y < _yRes; y++)
                {
                    var count = histogram[x, y];
                    if (count > max) max = count;
                }

            Console.WriteLine($"Max: {max}");

            for (int x = 0; x < _xRes; x++)
            {
                for (int y = 0; y < _yRes; y++)
                {
                    if (y == 0)
                        continue;

                    var brightness = histogram[x, y];
                    var rgb = Normalize(brightness, max);
                    BlitPixel(y - 1, x, rgb, rgb, rgb);
                }
            }

            SaveBmp();
        }

        public int Normalize(double brightness, double highest)
        {
            return (int)Math.Round((double)(brightness * 255 / highest));
        }

        public double Rand()
        {
            return Math.Round(new Random().Next(0, 1000000001) / 1000000000.0, 9);
        }
    }
}
