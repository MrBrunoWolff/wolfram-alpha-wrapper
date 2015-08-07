using System.Collections.Generic;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaQueryResult
    {
        public List<WolframAlphaPod> Pods { get; set; }

        public bool Success { get; set; }

        public bool ErrorOccured { get; set; }

        public int NumberOfPods { get; set; }

        public string DataTypes { get; set; }

        public string TimedOut { get; set; }

        public double Timing { get; set; }

        public double ParseTiming { get; set; }
    }
}
