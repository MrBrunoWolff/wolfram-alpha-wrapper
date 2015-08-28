using System.Collections.Generic;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaAssumption
    {
        public string Template { get; set; }

        public string Word { get; set; }

        public string Type { get; set; }

        public List<AssumptionValue> Values { get; set; }
    }

    public class AssumptionValue
    {
        public string Input { get; set; }

        public string Description { get; set; }

        public string Name { get; set; }
    }
}
