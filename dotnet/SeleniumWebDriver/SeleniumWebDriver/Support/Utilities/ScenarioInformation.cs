using System;
using System.Globalization;
using TechTalk.SpecFlow;

namespace SeleniumWebDriver.Support.Utilities
{
    public class ScenarioInformation
    {
        public static string GetTestScenarioTags { 
            get 
            {
                string _tagsAsString = null;
                var tags = ScenarioTags;
                foreach (var tag in tags)
                {
                    _tagsAsString = tag + " ";
                }
                return _tagsAsString;
            } 
        }

        public static string ScenarioTitle { get { return ScenarioContext.Current.ScenarioInfo.Title; } }
        public static Exception FailedTests = ScenarioContext.Current.TestError;
        public static string[] ScenarioTags = ScenarioContext.Current.ScenarioInfo.Tags;
        public static string ErrMsg { get { return FailedTests.Message; } }

        public static string GetTotalNumberOfScenarios()
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
