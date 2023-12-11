namespace ShadowsOfInfinity.Orchestrator
{
    public class MandelbrotRenderConfig : IRenderConfig
    {
        public int Iterations { get; set; }
        public int Repeats { get; set; }

        public override string ToString()
        {
            return $"Iterations: {Iterations}";
        }
    }
}
