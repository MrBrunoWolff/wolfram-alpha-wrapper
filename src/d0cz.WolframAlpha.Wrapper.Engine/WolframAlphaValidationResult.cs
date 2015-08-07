using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaValidationResult
    {
        private string WA_ParseData;
        private List<WolframAlphaAssumption> WA_Assumptions;
        private bool WA_Success;
        private bool WA_Error;

        private double WA_Timing;
        public bool Success
        {
            get { return WA_Success; }
            set { WA_Success = value; }
        }

        public string ParseData
        {
            get { return WA_ParseData; }
            set { WA_ParseData = value; }
        }

        public List<WolframAlphaAssumption> Assumptions
        {
            get { return WA_Assumptions; }
            set { WA_Assumptions = value; }
        }

        public bool ErrorOccured
        {
            get { return WA_Error; }
            set { WA_Error = value; }
        }

        public double Timing
        {
            get { return WA_Timing; }
            set { WA_Timing = value; }
        }

    }
}
