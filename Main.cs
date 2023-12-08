using CommandLine;

namespace ShadowsOfInfinity
{
    public class ShadowsOfInfinity
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<MandelbrotOptions, BuddhabrotOptions, NebulabrotOptions>(args)
                .WithParsed((BuddhabrotOptions opts) => new Buddhabrot().RunWithOptions(opts))
                .WithParsed((MandelbrotOptions opts) => new Mandelbrot().RunWithOptions(opts))
                .WithParsed((NebulabrotOptions opts) => new Nebulabrot().RunWithOptions(opts));
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            // Handle errors
        }
    }
}