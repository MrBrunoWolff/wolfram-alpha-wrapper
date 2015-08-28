using System.Collections.Generic;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaValidationResult
    {
        public decimal Version { get; set; }

        public double ParseTiming { get; set; }

        public double Timing { get; set; }
        
        public bool Error { get; set; }

        public bool Success { get; set; }

        public List<WolframAlphaAssumption> Assumptions { get; set; }
    }
}