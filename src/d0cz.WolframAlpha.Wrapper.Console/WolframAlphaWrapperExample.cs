using System;
using System.Configuration;
using System.IO;
using System.Linq;
using d0cz.WolframAlpha.Wrapper.Engine;

namespace d0cz.WolframAlpha.Wrapper.Console
{
    public static class WolframAlphaWrapperExample
    {
        static readonly WolframAlphaEngine Engine = new WolframAlphaEngine(ConfigurationManager.AppSettings.GetValues("APP_ID")?.First());

        public static void Main()
        {
            //Try to delete the log file if it already exists.
            try
            {
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Wolfram Alpha wrapper log.log");
            }
            catch (Exception)
            {
                // ignored
            }

            //Define what our application ID is.
            string wolframAlphaApplicationId = ConfigurationManager.AppSettings.GetValues("APP_NAME")?.First();

            //Define what we want to search for.
            string WolframAlphaSearchTerms = "england";

            //Print out what we're about to do in the console.
            Output($"Getting response for the search terms \\ { WolframAlphaSearchTerms } \\ and the application ID string \\ { wolframAlphaApplicationId } \\  ...", 0, ConsoleColor.White);

            //Use the engine to get a response, from the application ID specified, and the search terms.
            //Engine.LoadResponse(WolframAlphaSearchTerms, wolframAlphaApplicationId);
            Engine.LoadResponse(new WolframAlphaQuery() {Format = "plaintext", AllowCaching = false, APIKey = Engine.ApiKey, Query = WolframAlphaSearchTerms});

            //Print out a message saying that the last task was successful.
            Output("Response injected.", 0, ConsoleColor.White);

            //Make 2 empty spaces in the console.
            Output("", 0, ConsoleColor.White);

            Output("Response details", 1, ConsoleColor.Blue);

            //Print out how many different pods that were found.
            Output($"Pods found: {Engine.QueryResult.NumberOfPods}", 1, ConsoleColor.White);
            Output($"Query pasing time: { Engine.QueryResult.ParseTiming } seconds", 1, ConsoleColor.White);
            Output($"Query execution time: { Engine.QueryResult.Timing } seconds", 1, ConsoleColor.White);

            int podNumber = 1;

            foreach (WolframAlphaPod item in Engine.QueryResult.Pods)
            {
                //Make an empty space in the console.
                Output("", 0, ConsoleColor.White);

                Output("Pod " + podNumber, 2, ConsoleColor.Red);

                Output("Sub pods found: " + item.NumberOfSubPods, 2, ConsoleColor.White);
                Output("Title: \"" + item.Title + "\"", 2, ConsoleColor.White);
                Output("Position: " + item.Position, 2, ConsoleColor.White);

                int subPodNumber = 1;

                foreach (WolframAlphaSubPod subItem in item.SubPods)
                {
                    Output("", 0, ConsoleColor.White);

                    Output("Sub pod " + subPodNumber, 3, ConsoleColor.Magenta);
                    Output("Title: \"" + subItem.Title + "\"", 3, ConsoleColor.White);
                    Output("Pod text: \"" + subItem.PodText + "\"", 3, ConsoleColor.White);
                    Output("Pod image title: \"" + subItem.PodImage?.Title + "\"", 3, ConsoleColor.White);
                    Output("Pod image width: " + subItem.PodImage?.Width, 3, ConsoleColor.White);
                    Output("Pod image height: " + subItem.PodImage?.Height, 3, ConsoleColor.White);
                    Output("Pod image location: \"" + subItem.PodImage?.Location + "\"", 3, ConsoleColor.White);
                    Output("Pod image description text: \"" + subItem.PodImage?.HoverText + "\"", 3, ConsoleColor.White);

                    subPodNumber += 1;
                }

                podNumber += 1;
            }

            //Make an empty space in the console.
            Output("", 0, ConsoleColor.White);

            //Make the application stay open until there is user interaction.
            Output($"All content has been saved to {Environment.GetFolderPath(Environment.SpecialFolder.Desktop) } \\ Wolfram Alpha wrapper log.log. Press a key to close the example.", 0, ConsoleColor.Green);
            System.Console.ReadLine();
        }

        public static void Output(string data, int indenting, ConsoleColor color)
        {
            data = new string(' ', indenting * 4) + data;

            System.Console.ForegroundColor = color;
            System.Console.WriteLine(data);
            System.Console.ForegroundColor = ConsoleColor.White;

            StreamWriter writer = new StreamWriter($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)} \\Wolfram Alpha wrapper log.log", true);
            writer.WriteLine(data);
            writer.Close();
            writer.Dispose();
        }
    }
}
