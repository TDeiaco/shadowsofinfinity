using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowsOfInfinity.Orchestrator
{
    public class BuddhabrotRenderConfig : IRenderConfig
    {
        public int Samples { get; set; }
        public int Iterations { get; set; }
        public int Repeats { get; set; }

        public override string ToString()
        {
            return $"Samples: {Samples} Iterations: {Iterations}";
        }
    }
}
