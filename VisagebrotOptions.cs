using CommandLine.Text;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowsOfInfinity
{
    [Verb("visagebrot", HelpText = "Render visagebrot.")]
    public class VisagebrotOptions
    {
        [Option('w', "width", Required = true, HelpText = "Set the width of the image.")]
        public int Width { get; set; }

        [Option('h', "height", Required = true, HelpText = "Set the height of the image.")]
        public int Height { get; set; }

        [Option('s', "samples", Required = true, HelpText = "Number of samples to capture.")]
        public int Samples { get; set; }

        [Option('b', "band", Required = true, HelpText = "Sets a band of iteration counts to produce the spectrum of the Buddhabrot.")]
        public string Band { get; set; }
    }
}
