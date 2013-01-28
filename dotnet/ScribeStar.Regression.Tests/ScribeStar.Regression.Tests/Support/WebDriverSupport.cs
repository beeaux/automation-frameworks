using ScribeStar.Regression.Tests.Core.Utils;
using System.Configuration;
using TechTalk.SpecFlow;

namespace ScribeStar.Regression.Tests.Core
{
    [Binding]
    public static class WebDriverSupport
    {
        private static bool ReuseWebSession
        {
            get { return ConfigurationManager.AppSettings["ReuseWebSession"] == "true"; }
        }

        private static bool MonitorWebPerformance
        {
            get { return ConfigurationManager.AppSettings["MonitorWebPerformance"] == "false"; }
        }

        [Before]
        public void BeforeWebSession()
        {
            SharedDriver.DriverInstance.SetUp();
        }

        [AfterTestRun]
        public void AfterWebSession()
        {
            SharedDriver.DriverInstance.TearDown();
        }

        [AfterScenario]
        public void AfterWebScenario()
        {
            if (!ReuseWebSession)
                SharedDriver.DriverInstance.TearDown();
        }

        [AfterScenario]
        public void LogFailedScenarios()
        {
            var err = ScenarioInformation.FailedTests;
            if (err != null)
            {
                SharedDriver.DriverInstance.WebDriver.EmbedScreenCapture();
            }
        }

        [AfterFeature]
        public void DeleteCookiesAfterWebFeature()
        {
            SharedDriver.DriverInstance.DeleteAllCookies();
        }
    }
}
