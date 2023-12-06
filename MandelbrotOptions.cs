using CommandLine;

namespace ShadowsOfInfinity
{

    [Verb("mandelbrot", HelpText = "Render Mandelbrot.")]
    public class MandelbrotOptions
    {
        [Option('w', "width", Required = true, HelpText = "Set the width of the image.")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "Set the height of the image.")]
        public int Height { get; set; }

        [Option('i', "iterations", Required = true, HelpText = "Iteration count.")]
        public int Iterations { get; set; }
    }
}
