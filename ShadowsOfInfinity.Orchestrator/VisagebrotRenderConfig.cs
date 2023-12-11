namespace ShadowsOfInfinity.Orchestrator
{
    public class VisagebrotRenderConfig : IRenderConfig
    {
        public int Samples { get; set; }
        public int Band { get; set; }
        public int Cycles { get; set; }
        public int Repeats { get; set; }

        public override string ToString()
        {
            return $"Samples: {Samples} Band: {Band} Cycles: {Cycles}";
        }
    }
}
