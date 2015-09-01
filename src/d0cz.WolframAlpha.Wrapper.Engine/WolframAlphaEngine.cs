using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;

namespace d0cz.WolframAlpha.Wrapper.Engine
{
    public class WolframAlphaEngine
    {
        private readonly string _apiKey;
        private WolframAlphaQueryResult _queryResult;
        private WolframAlphaValidationResult _validationResult;

        public WolframAlphaEngine(string apiKey)
        {
            _apiKey = apiKey;
        }

        public string ApiKey => _apiKey;

        public WolframAlphaQueryResult QueryResult => _queryResult;

        public WolframAlphaValidationResult ValidationResult => _validationResult;

        #region "Overloads of ValidateQuery"

        public WolframAlphaValidationResult ValidateQuery(WolframAlphaQuery query)
        {
            if (string.IsNullOrEmpty(query.APIKey))
            {
                if (string.IsNullOrEmpty(ApiKey))
                    throw new Exception("To use the Wolfram Alpha API, you must specify an API key either through the parsed WolframAlphaQuery, or on the WolframAlphaEngine itself.");

                query.APIKey = ApiKey;
            }

            if (query.Asynchronous && query.Format == WolframAlphaQueryFormatEnum.Html)
                throw new Exception("Wolfram Alpha does not allow asynchronous operations while the format for the query is not set to \"HTML\".");

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://api.wolframalpha.com/v2/validatequery.jsp" + query.FullQueryString);
            webRequest.KeepAlive = true;
            string response = new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd();

            return ValidateQuery(response);
        }

        public WolframAlphaValidationResult ValidateQuery(string response)
        {
            XmlDocument document = new XmlDocument();
            WolframAlphaValidationResult result = null;

            try
            {
                document.LoadXml(response);
                result = ValidateQuery(document);
            }
            catch
            {
            }

            return result;
        }

        public WolframAlphaValidationResult ValidateQuery(XmlDocument response)
        {
            //Thread.Sleep(1);

            XmlNode mainNode = response.SelectNodes("/validatequeryresult")?.Item(0);

            if (mainNode == null) return null;

            _validationResult = new WolframAlphaValidationResult
            {
                Version = ToDecimal(mainNode.Attributes?["version"]),
                ParseTiming = ToDouble(mainNode.Attributes?["parsetiming"]),
                Timing = ToDouble(mainNode.Attributes?["timing"]),
                Error = ToBoolean(mainNode.Attributes?["error"]),
                Success = ToBoolean(mainNode.Attributes?["success"]),
                Assumptions = new List<WolframAlphaAssumption>()
            };

            foreach (XmlNode node in mainNode.SelectNodes("assumptions"))
            {
                //Thread.Sleep(1);

                var assumptionNode = node.SelectSingleNode("assumption");

                if (assumptionNode != null)
                {
                    var assumption = new WolframAlphaAssumption
                    {
                        Template = assumptionNode.Attributes?["template"].Value,
                        Word = assumptionNode.Attributes?["word"].Value,
                        Type = assumptionNode.Attributes?["type"].Value
                    };

                    if (assumptionNode.HasChildNodes)
                    {
                        assumption.Values = new List<AssumptionValue>();

                        foreach (XmlNode valueNode in assumptionNode.ChildNodes)
                        {
                            //Thread.Sleep(1);

                            assumption.Values.Add(new AssumptionValue
                            {
                                Input = valueNode.Attributes?["input"].Value,
                                Description = valueNode.Attributes?["desc"].Value,
                                Name = valueNode.Attributes?["name"].Value
                            });
                        }
                    }

                    _validationResult.Assumptions.Add(assumption);
                }
            }

            return _validationResult;
        }

        #endregion

        #region "Overloads of LoadResponse"

        public WolframAlphaQueryResult LoadResponse(WolframAlphaQuery query)
        {
            if (string.IsNullOrEmpty(query.APIKey))
            {
                if (string.IsNullOrEmpty(_apiKey))
                    throw new Exception("To use the Wolfram Alpha API, you must specify an API key either through the parsed WolframAlphaQuery, or on the WolframAlphaEngine itself.");

                query.APIKey = _apiKey;
            }

            if (query.Asynchronous && query.Format == WolframAlphaQueryFormatEnum.Html)
                throw new Exception("Wolfram Alpha does not allow asynchronous operations while the format for the query is not set to \"HTML\".");

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://api.wolframalpha.com/v2/query.jsp" + query.FullQueryString);
            webRequest.KeepAlive = true;

            string response = new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd();

            return LoadResponse(response);
        }

        public WolframAlphaQueryResult LoadResponse(string response)
        {
            XmlDocument document = new XmlDocument();
            WolframAlphaQueryResult result = null;
            try
            {
                document.LoadXml(response);
                result = LoadResponse(document);
            }
            catch
            {
            }

            return result;
        }

        public WolframAlphaQueryResult LoadResponse(XmlDocument response)
        {
            Thread.Sleep(1);

            if (response == null)
                throw new ArgumentNullException(nameof(response));

            XmlNode mainNode = response.SelectNodes("/queryresult").Item(0);
            _queryResult = new WolframAlphaQueryResult();
            _queryResult.Success = ToBoolean(mainNode.Attributes["success"]);
            _queryResult.ErrorOccured = ToBoolean(mainNode.Attributes["error"]);
            _queryResult.NumberOfPods = ToInt32(mainNode.Attributes["numpods"]);
            _queryResult.Timing = ToDouble(mainNode.Attributes["timing"]);
            _queryResult.TimedOut = mainNode.Attributes["timedout"].Value;
            _queryResult.DataTypes = mainNode.Attributes["datatypes"].Value;
            _queryResult.Pods = new List<WolframAlphaPod>();
            
            foreach (XmlNode node in mainNode.SelectNodes("pod"))
            {
                Thread.Sleep(1);

                WolframAlphaPod pod = new WolframAlphaPod
                {
                    Title = node.Attributes["title"].Value,
                    Scanner = node.Attributes["scanner"].Value,
                    Position = ToInt32(node.Attributes["position"]),
                    ErrorOccured = ToBoolean(node.Attributes["error"]),
                    NumberOfSubPods = ToInt32(node.Attributes["numsubpods"]),
                    SubPods = new List<WolframAlphaSubPod>()
                };

                foreach (XmlNode subNode in node.SelectNodes("subpod"))
                {
                    Thread.Sleep(1);

                    WolframAlphaSubPod subPod = new WolframAlphaSubPod();
                    subPod.Title = subNode.Attributes["title"].Value;


                    foreach (XmlNode contentNode in subNode.SelectNodes("plaintext"))
                    {
                        Thread.Sleep(1);

                        subPod.PodText = contentNode.InnerText;
                    }

                    foreach (XmlNode contentNode in subNode.SelectNodes("img"))
                    {
                        Thread.Sleep(1);

                        WolframAlphaImage image = new WolframAlphaImage();
                        image.Location = new Uri(contentNode.Attributes["src"].Value);
                        image.HoverText = contentNode.Attributes["alt"].Value;
                        image.Title = contentNode.Attributes["title"].Value;
                        image.Width = ToInt32(contentNode.Attributes["width"]);
                        image.Height = ToInt32(contentNode.Attributes["height"]);
                        subPod.PodImage = image;
                    }

                    pod.SubPods.Add(subPod);
                }

                _queryResult.Pods.Add(pod);
            }

            return _queryResult;
        }

        #endregion

        private static bool ToBoolean(XmlNode xmlNode)
        {
            return Convert.ToBoolean(xmlNode.Value);
        }

        private static double ToDouble(XmlNode xmlNode)
        {
            return Convert.ToDouble(xmlNode.Value);
        }

        private static int ToInt32(XmlNode xmlNode)
        {
            return Convert.ToInt32(xmlNode.Value);
        }

        private static decimal ToDecimal(XmlNode xmlNode)
        {
            return Convert.ToDecimal(xmlNode.Value);
        }
    }
}