using CommandLine;

namespace ShadowsOfInfinity
{
    public class ShadowsOfInfinity
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<MandelbrotOptions, BuddhabrotOptions, NebulabrotOptions, VisagebrotOptions>(args)
                .WithParsed((BuddhabrotOptions opts) => new Buddhabrot().RunWithOptions(opts))
                .WithParsed((MandelbrotOptions opts) => new Mandelbrot().RunWithOptions(opts))
                .WithParsed((NebulabrotOptions opts) => new Nebulabrot().RunWithOptions(opts))
                .WithParsed((VisagebrotOptions opts) => new Visagebrot().RunWithOptions(opts))
                .WithNotParsed((errs) => HandleParseError(errs));
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            // Handle errors
        }
    }
}