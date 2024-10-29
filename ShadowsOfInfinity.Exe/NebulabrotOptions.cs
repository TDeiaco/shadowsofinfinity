using CommandLine;

namespace ShadowsOfInfinity.Exe
{
    [Verb("nebulabrot", HelpText = "Render Nebulabrot.")]
    public class NebulabrotOptions
    {
        [Option('w', "width", Required = true, HelpText = "Set the width of the image.")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "Set the height of the image.")]
        public int Height { get; set; }

        [Option('s', "samples", Required = true, HelpText = "Number of samples to capture.")]
        public int Samples { get; set; }

        [Option('o', "order", Required = true, HelpText = "Channel order, which color channels get what iteration levels.")]
        public string Order { get; set; }

        [Option('r', "rband", Required = true, HelpText = "Band of iterations for the Red channel ex: (1000-10000).")]
        public string RBand { get; set; }

        [Option('g', "gband", Required = true, HelpText = "Band of iterations for the Green channel ex: (1000-10000).")]
        public string GBand { get; set; }

        [Option('b', "bband", Required = true, HelpText = "Band of iterations for the Blue channel ex: (1000-10000)..")]
        public string BBand { get; set; }

        [Option('c', "cycles", Required = true, HelpText = "Number of cycles to run the bands.")]
        public int Cycles { get; set; }
    }
}
