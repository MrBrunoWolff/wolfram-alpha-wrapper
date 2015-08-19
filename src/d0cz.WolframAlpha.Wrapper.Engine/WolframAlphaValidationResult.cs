using System.Collections.Generic;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaValidationResult
    {
        private string _parseData;
        private List<WolframAlphaAssumption> _assumptions;
        private bool _success;
        private bool _error;
        private double _timing;

        public bool Success
        {
            get { return _success; }
            set { _success = value; }
        }

        public string ParseData
        {
            get { return _parseData; }
            set { _parseData = value; }
        }

        public List<WolframAlphaAssumption> Assumptions
        {
            get { return _assumptions; }
            set { _assumptions = value; }
        }

        public bool ErrorOccured
        {
            get { return _error; }
            set { _error = value; }
        }

        public double Timing
        {
            get { return _timing; }
            set { _timing = value; }
        }

    }
}
