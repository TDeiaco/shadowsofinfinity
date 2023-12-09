namespace ShadowsOfInfinity
{
    public class Mandelbrot : BaseRenderer
    {
        private MandelbrotOptions _opts;
        public Mandelbrot()
        {
            Console.WriteLine("Rendering Mandelbrot");
        }

        public override string RenderFileName()
        {
            return $"mandelbrot_i_{_opts.Iterations}_{GetTimestamp()}.{_imageFormat.ToString().ToLower()}";
        }

        public void RunWithOptions(MandelbrotOptions opts)
        {
            _opts = opts;
            Console.WriteLine($"Width: {opts.Width}, Height: {opts.Height}");
            Console.WriteLine($"Iterations: {opts.Iterations}");

            Console.WriteLine($"...");

            int width = opts.Width;
            int height = opts.Height;
            InitBitmap(width, height, 0, 0, 0);

            var _xRes = opts.Width;
            var _yRes = opts.Height;
            var _minX = -1.75;
            var _maxX = 0.5;
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

                    BlitPixel(ix, iy, iterations % 127, iterations % 255, iterations % 132);

                }
                Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                var progress = Math.Round((((double)ix + 1) / _xRes) * 100);
                Console.WriteLine($"Render progress: {progress}%...");

            }

            SaveBmp();
        }

        public double Rand()
        {
            return Math.Round(new Random().Next(0, 1000000001) / 1000000000.0, 9);
        }
    }
}
