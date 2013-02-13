using System.Configuration;
using SeleniumWebDriver.Support.Utilities;
using TechTalk.SpecFlow;

namespace SeleniumWebDriver.Support
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
        public static void BeforeWebSession()
        {
            SharedDriver.DriverInstance.SetUp();
        }

        [AfterTestRun]
        public static void AfterWebSession()
        {
            SharedDriver.DriverInstance.TearDown();
        }

        [AfterScenario]
        public static void AfterWebScenario()
        {
            if (!ReuseWebSession)
                SharedDriver.DriverInstance.TearDown();
        }

        [AfterScenario]
        public static void LogFailedScenarios()
        {
            var err = ScenarioInformation.FailedTests;
            if (err != null)
            {
                SharedDriver.DriverInstance.WebDriver.EmbedScreenCapture();
            }
        }

        [AfterFeature]
        public static void DeleteCookiesAfterWebFeature()
        {
            SharedDriver.DriverInstance.DeleteAllCookies();
        }
    }
}
