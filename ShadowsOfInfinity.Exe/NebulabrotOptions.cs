using CommandLine.Text;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        [Option('o', "order", Required = false, HelpText = "Channel order, which color channels get what iteration levels.")]
        public string Order { get; set; }
    }
}
