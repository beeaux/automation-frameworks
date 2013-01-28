using System;
using System.Globalization;
using TechTalk.SpecFlow;

namespace ScribeStar.Regression.Tests.Core.Utils
{
    public class ScenarioInformation
    {
        public static string GetTestScenarioTags
        {
            get
            {
                string stringOfTags = null;
                var tags = ScenarioTags;
                foreach (var tag in tags)
                {
                    stringOfTags = tag + " ";
                }
                return stringOfTags;
            }
        }

        public static ScenarioInfo scenario = ScenarioContext.Current.ScenarioInfo; 
        public static string ScenarioTitle { get { return scenario.Title; } }
        public static Exception FailedTests = ScenarioContext.Current.TestError;
        public static string[] ScenarioTags = scenario.Tags;
        public static string ErrMsg { get { return FailedTests.Message; } }

        public static string TotalNoOfScenarios()
        {
            var noOfScenarios = 0;
            while (ScenarioTitle != null)
            {
                noOfScenarios++;
                Console.WriteLine("Current value of n is {0}", noOfScenarios++);
                return noOfScenarios.ToString(CultureInfo.InvariantCulture);
            }
            return noOfScenarios.ToString(CultureInfo.InvariantCulture);
        }
    }
}
