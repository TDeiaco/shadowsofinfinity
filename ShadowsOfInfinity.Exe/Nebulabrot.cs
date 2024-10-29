namespace ShadowsOfInfinity.Exe
{
    public class Nebulabrot : BaseRenderer
    {
        private NebulabrotOptions _opts;

        private int _channelColorIndex = 0;
        private int _channelStartBandIndex = 1;
        private int _channelEndBandIndex = 2;

        public Nebulabrot()
        {
            Console.WriteLine("Rendering Nebulabrot");
        }

        public override string RenderFileName()
        {
            return $"nebulabrot_s_{_opts.Samples}_o_{_opts.Order}_{GetTimestamp()}.{_imageFormat.ToString().ToLower()}";
        }

        public void RunWithOptions(NebulabrotOptions opts)
        {
            _opts = opts;
            var sampleCount = opts.Samples;
            var _xRes = opts.Width;
            var _yRes = opts.Height + 1; //These adjustments are to eliminate a strip of black that I'm not sure why occures

            var updateRate = sampleCount / 100;
            if (updateRate <= 0) updateRate = 1;

            char channel0, channel1, channel2;
            if (opts.Order == null)
            {
                channel0 = 'r'; channel1 = 'g'; channel2 = 'b';
            }
            else if (opts.Order.Length != 3)
            {
                Console.WriteLine("Oder must be exactly 3 characters, and must include 'r', 'g', and 'b'");
                return;
            }
            else
            {
                channel0 = opts.Order[0];
                channel1 = opts.Order[1];
                channel2 = opts.Order[2];
            }

            var rBandSplit = opts.RBand.Split('-');
            var rStartBand = Convert.ToInt32(rBandSplit[0]);
            var rEndBand = Convert.ToInt32(rBandSplit[1]);

            var gBandSplit = opts.RBand.Split('-');
            var gStartBand = Convert.ToInt32(gBandSplit[0]);
            var gEndBand = Convert.ToInt32(gBandSplit[1]);

            var bBandSplit = opts.RBand.Split('-');
            var bStartBand = Convert.ToInt32(bBandSplit[0]);
            var bEndBand = Convert.ToInt32(bBandSplit[1]);

            var channelMap = new Dictionary<int, ChannelDefinition>();

            int startBand = 0, endBand = 0;
            switch (channel0)
            {
                case 'r': startBand = rStartBand; endBand = rEndBand; break;
                case 'g': startBand = gStartBand; endBand = gEndBand; break;
                case 'b': startBand = bStartBand; endBand = bEndBand; break;
            }
            channelMap.Add(0, new ChannelDefinition() { Color = opts.Order[0], StartBand = startBand, EndBand = endBand });

            switch (channel1)
            {
                case 'r': startBand = rStartBand; endBand = rEndBand; break;
                case 'g': startBand = gStartBand; endBand = gEndBand; break;
                case 'b': startBand = bStartBand; endBand = bEndBand; break;
            }
            channelMap.Add(1, new ChannelDefinition() { Color = opts.Order[1], StartBand = startBand, EndBand = endBand });

            switch (channel2)
            {
                case 'r': startBand = rStartBand; endBand = rEndBand; break;
                case 'g': startBand = gStartBand; endBand = gEndBand; break;
                case 'b': startBand = bStartBand; endBand = bEndBand; break;
            }

            channelMap.Add(2, new ChannelDefinition() { Color = opts.Order[2], StartBand = startBand, EndBand = endBand });

            Console.WriteLine($"Width: {opts.Width}, Height: {opts.Height}");
            Console.WriteLine($"Samples: {sampleCount}");
            Console.WriteLine($"Order: {opts.Order}");

            var _minX = -2.0;
            var _maxX = 1.0;
            var _minY = -1.0;
            var _maxY = 1.0;
            var frameWidth = _maxX - _minX;
            var frameHeight = _maxY - _minY;
            var pixelWidth = frameWidth / _xRes;
            var pixelHeight = frameHeight / _yRes;

            double[,,] histogram = new double[3, _xRes, _yRes];

            double[] maxes = new double [3];

            for (int histogramSelector = 0; histogramSelector < 3; histogramSelector++)
            {
                for (var cycles = 0; cycles < opts.Cycles; cycles++)
                {
                    //The iteration count is the endBand for the particular color
                    var iterationCount = channelMap[histogramSelector].EndBand + 1;  

                    for (var s = 0; s < sampleCount; s++)
                    {
                        var pixelX = Rand() * frameWidth + _minX;
                        var pixelY = Rand() * frameHeight + _minY;

                        // Iterate over each pixel
                        int iterations = 0;

                        double x = 0.0;
                        double y = 0.0;

                        var stops = new List<(int, int)>();

                        while (x * x + y * y <= 4.0 && iterations < iterationCount)
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

                        if (iterations > channelMap[histogramSelector].StartBand && iterations < channelMap[histogramSelector].EndBand)
                            foreach (var stop in stops)
                                histogram[histogramSelector, stop.Item1, stop.Item2] += 1;

                        // every 10k samples, log progress
                        if (s % updateRate == 0)
                        {
                            var progress = Math.Round((double)(s + 1) / sampleCount * 100);

                            Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                            Console.WriteLine($"Render progress: Channel:{histogramSelector + 1}/3 Cycles:{cycles + 1}/{opts.Cycles} {progress + 1}% ...");
                        }
                    }
                }

                for (int x = 0; x < _xRes; x++)
                    for (int y = 0; y < _yRes; y++)
                    {
                        double count = histogram[histogramSelector, x, y];
                        if (count > maxes[histogramSelector]) maxes[histogramSelector] = count;

                    }
            }

            InitBitmap(_xRes, _yRes - 1, 0, 0, 0);

            for (int x = 0; x < _xRes; x++)
            {
                for (int y = 0; y < _yRes; y++)
                {
                    if (y == 0)
                        continue;

                    var brightnessRed = 0.0;
                    var brightnessGreen = 0.0;
                    var brightnessBlue = 0.0;

                    var maxRed = 0.0;
                    var maxGreen = 0.0;
                    var maxBlue = 0.0;
                    switch (channelMap[0].Color)
                    {
                        case 'r': brightnessRed = histogram[0, x, y]; maxRed = maxes[0]; break;
                        case 'g': brightnessGreen = histogram[0, x, y]; maxGreen = maxes[0]; break;
                        case 'b': brightnessBlue = histogram[0, x, y]; maxBlue = maxes[0]; break;
                    }

                    switch (channelMap[1].Color)
                    {
                        case 'r': brightnessRed = histogram[1, x, y]; maxRed = maxes[1]; break;
                        case 'g': brightnessGreen = histogram[1, x, y]; maxGreen = maxes[1]; break;
                        case 'b': brightnessBlue = histogram[1, x, y]; maxBlue = maxes[1]; break;
                    }

                    switch (channelMap[2].Color)
                    {
                        case 'r': brightnessRed = histogram[2, x, y]; maxRed = maxes[2]; break;
                        case 'g': brightnessGreen = histogram[2, x, y]; maxGreen = maxes[2]; break;
                        case 'b': brightnessBlue = histogram[2, x, y]; maxBlue = maxes[2]; break;
                    }


                    var red = Normalize(brightnessRed, maxRed);
                    var green = Normalize(brightnessGreen, maxGreen);
                    var blue = Normalize(brightnessBlue, maxBlue);
                    BlitPixel(y - 1, x, red, green, blue);
                }
            }

            SaveBmp();
        }

        public int Normalize(double brightness, double highest)
        {
            return (int)Math.Round((brightness + 1) * 255 / (highest + 1));
        }

        public double Rand()
        {
            return Math.Round(new Random().Next(0, 1000000001) / 1000000000.0, 9);
        }
    }
}
