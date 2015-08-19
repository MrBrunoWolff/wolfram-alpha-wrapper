using System.Collections.Generic;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaPod
    {
        private List<WolframAlphaSubPod> _subPods;
        private string _title;
        private string _scanner;
        private int _position;
        private bool _error;
        private int _numberOfSubPods;

        public List<WolframAlphaSubPod> SubPods
        {
            get { return _subPods; }
            set { _subPods = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string Scanner
        {
            get { return _scanner; }
            set { _scanner = value; }
        }

        public int Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public bool ErrorOccured
        {
            get { return _error; }
            set { _error = value; }
        }

        public int NumberOfSubPods
        {
            get { return _numberOfSubPods; }
            set { _numberOfSubPods = value; }
        }
    }
}
