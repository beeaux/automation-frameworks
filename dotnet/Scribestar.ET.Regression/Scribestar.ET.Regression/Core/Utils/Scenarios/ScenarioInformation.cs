using System;
using TechTalk.SpecFlow;

namespace Scribestar.ET.Regression.Core.Utils.Scenarios
{
    public class ScenarioInformation
    {
        public static ScenarioContext CurrentScenario = ScenarioContext.Current;
        public static Exception FailedScenario = CurrentScenario.TestError;

        public static string ScenarioTitle
        {
            get { return CurrentScenario.ScenarioInfo.Title; }
        }

        public static string ErrorMessage
        {
            get { return FailedScenario.Message; }
        }

        public static string[] ScenarioTags
        {
            get { return CurrentScenario.ScenarioInfo.Tags; }
        }
    }
}
