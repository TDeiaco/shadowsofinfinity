namespace ShadowsOfInfinity.Orchestrator
{
    public class NebulabrotRenderConfig : IRenderConfig
    {
        public int Samples { get; set; }
        public string Order { get; set; }
        public int Repeats { get; set; }

        public override string ToString()
        {
            return $"Samples: {Samples} Order: {Order}";
        }
    }
}
