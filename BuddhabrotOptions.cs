using CommandLine;

namespace ShadowsOfInfinity
{
    [Verb("buddhabrot", HelpText = "Render Buddhabrot.")]
    public class BuddhabrotOptions
    {
        [Option('w', "width", Required = true, HelpText = "Set the width of the image.")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "Set the height of the image.")]
        public int Height { get; set; }

        [Option('s', "samples", Required = true, HelpText = "Number of samples to capture.")]
        public int Samples { get; set; }

        [Option('r', "range", Required = true, HelpText = "Sets a range of iteration counts to generate images from.")]
        public string Range { get; set; }

        [Option('d', "details", Required = false, HelpText = "Uses a log function to render the details of the Buddhabrot.")]
        public bool Details { get; set; }
    }
}
