using System;
using System.Net;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaQuery
    {
        //Private Const MainRoot As String = "http://api.wolframalpha.com/v2/query.jsp"

        private string _apiKey;
        private bool _moreOutput;
        private int _timeLimit;
        private WolframAlphaQueryFormatEnum _format;
        private string _substitution;
        private string _assumption;
        private string _query;
        private string _podTitle;
        
        private bool _allowCached;
        private bool _asynchronous;
        

        public string APIKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        public bool MoreOutput
        {
            get { return _moreOutput; }
            set { _moreOutput = value; }
        }

        public int TimeLimit
        {
            get { return _timeLimit; }
            set { _timeLimit = value; }
        }

        public WolframAlphaQueryFormatEnum Format
        {
            get { return _format; }
            set { _format = value; }
        }

        public string Query
        {
            get { return _query; }
            set { _query = value; }
        }

        public string[] Substitutions => _substitution.Split(new[] { "&substitution=" }, StringSplitOptions.RemoveEmptyEntries);

        public string[] Assumptions => _assumption.Split(new[] { "&assumption=" }, StringSplitOptions.RemoveEmptyEntries);

        public string[] PodTitles => _podTitle.Split(new[] {"&assumption="}, StringSplitOptions.RemoveEmptyEntries);

        public bool Asynchronous
        {
            get { return _asynchronous; }
            set { _asynchronous = value; }
        }

        public bool AllowCaching
        {
            get { return _allowCached; }
            set { _allowCached = false; }
        }
        
        public void AddPodTitle(string podTitle, bool checkForDuplicates = false)
        {
            if (checkForDuplicates && _podTitle.Contains($"&PodTitle={WebUtility.UrlEncode(podTitle)}"))
                return;

            _podTitle += $"&podtitle= {WebUtility.UrlEncode(podTitle)}";
        }

        public void AddAssumption(string assumption, bool checkForDuplicates = false)
        {
            if (checkForDuplicates && _assumption.Contains($"&substitution={WebUtility.UrlEncode(assumption)}"))
                return;

            _assumption += $"&assumption= {WebUtility.UrlEncode(assumption)}";
        }

        public void AddAssumption(WolframAlphaAssumption assumption, bool checkForDuplicates = false)
        {
            if (checkForDuplicates && _assumption.Contains($"&substitution={WebUtility.UrlEncode(assumption.Word)}"))
                return;

            _assumption += $"&assumption= {WebUtility.UrlEncode(assumption.Word)}";
        }

        public void AddSubstitution(string substitution, bool checkForDuplicates = false)
        {
            if (checkForDuplicates && _substitution.Contains($"&substitution={WebUtility.UrlEncode(substitution)}"))
                return;

            _substitution += $"&substitution= {WebUtility.UrlEncode(substitution)}";
        }

        public string FullQueryString => $"?appid={_apiKey}&moreoutput={MoreOutput}&timelimit={TimeLimit}&format={_format.ToString().ToLower()}&input={_query + _assumption + _substitution}";
    }

    public enum WolframAlphaQueryFormatEnum
    {
        // Visual Representations
        Image,
        Cell,

        // Textual Representations
        PlainText,
        Html,
        MathML, // MathematicaMathMarkupLanguage
        Minput, // MathematicaInput
        Moutput, // MathematicaOutput
        
        // Audio Representations
        Sound,
        Wav
    }
}