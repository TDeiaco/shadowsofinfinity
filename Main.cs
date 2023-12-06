using CommandLine;

namespace ShadowsOfInfinity
{
    public class ShadowsOfInfinity
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<MandelbrotOptions, BuddhabrotOptions>(args)
                .WithParsed((BuddhabrotOptions opts) => new Buddhabrot().RunWithOptions(opts))
                .WithParsed((MandelbrotOptions opts) => new Mandelbrot().RunWithOptions(opts));
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            // Handle errors
        }
    }
}