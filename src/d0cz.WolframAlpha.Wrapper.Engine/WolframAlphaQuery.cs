using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaQuery
    {
        //Private Const MainRoot As String = "http://api.wolframalpha.com/v1/query.jsp"

        private string _waApiKey;
        public string APIKey
        {
            get { return _waApiKey; }
            set { _waApiKey = value; }
        }

        private string _waFormat;
        private string _waSubstitution;
        private string _waAssumption;
        private string _waQuery;
        private string _waPodTitle;
        private int _waTimeLimit;
        private bool _waAllowCached;
        private bool _waAsynchronous;
        private bool _waMoreOutput;
        public bool MoreOutput
        {
            get { return _waMoreOutput; }
            set { _waMoreOutput = value; }
        }

        public string Format
        {
            get { return _waFormat; }
            set { _waFormat = value; }
        }

        public bool Asynchronous
        {
            get { return _waAsynchronous; }
            set { _waAsynchronous = value; }
        }

        public bool AllowCaching
        {
            get { return _waAllowCached; }
            set { _waAllowCached = false; }
        }

        public string Query
        {
            get { return _waQuery; }
            set { _waQuery = value; }
        }

        public int TimeLimit
        {
            get { return _waTimeLimit; }
            set { _waTimeLimit = value; }
        }

        public void AddPodTitle(string podTitle, bool checkForDuplicates = false)
        {
            if (checkForDuplicates == true && _waPodTitle.Contains("&PodTitle=" + WebUtility.UrlEncode(podTitle)))
            {
                return;
            }
            _waPodTitle += $"&podtitle= {WebUtility.UrlEncode(podTitle)}";
        }

        public void AddSubstitution(string substitution, bool checkForDuplicates = false)
        {
            if (checkForDuplicates == true && _waSubstitution.Contains("&substitution=" + WebUtility.UrlEncode(substitution)))
            {
                return;
            }
            _waSubstitution += $"&substitution= {WebUtility.UrlEncode(substitution)}";
        }

        public void AddAssumption(string assumption, bool checkForDuplicates = false)
        {
            if (checkForDuplicates == true && _waAssumption.Contains("&substitution=" + WebUtility.UrlEncode(assumption)))
            {
                return;
            }

            _waAssumption += $"&assumption= {WebUtility.UrlEncode(assumption)}";
        }

        public void AddAssumption(WolframAlphaAssumption assumption, bool checkForDuplicates = false)
        {
            if (checkForDuplicates && _waAssumption.Contains($"&substitution= { WebUtility.UrlEncode(assumption.Word)}"))
            {
                return;
            }
            _waAssumption += $"&assumption= {WebUtility.UrlEncode(assumption.Word)}";
        }

        public string[] Substitutions => _waSubstitution.Split(new[] { "&substitution=" }, StringSplitOptions.RemoveEmptyEntries);

        public string[] Assumptions => _waAssumption.Split(new[] { "&assumption=" }, StringSplitOptions.RemoveEmptyEntries);

        public string[] PodTitles => _waPodTitle.Split(new[] { "&assumption=" }, StringSplitOptions.RemoveEmptyEntries);

        public string FullQueryString => $"?appid={_waApiKey}&moreoutput={MoreOutput}&timelimit={TimeLimit}&format={_waFormat}&input={_waQuery + _waAssumption + _waSubstitution}";

        public class WolframAlphaQueryFormat
        {
            public static string Image = "image";
            public static string Html = "html";
            public static string Pdf = "pdf";
            public static string PlainText = "plaintext";
            public static string MathematicaInput = "minput";
            public static string MathematicaOutput = "moutput";
            public static string MathematicaMathMarkupLanguage = "mathml";
            public static string MathematicaExpressionMarkupLanguage = "expressionml";
            public static string ExtensibleMarkupLanguage = "xml";
        }
    }
}
