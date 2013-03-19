using OpenQA.Selenium.Remote;
using Scribestar.ET.Regression.Core.Utils.Scenarios;
using System.Configuration;
using TechTalk.SpecFlow;
using Scribestar.ET.Regression.Core.Utils;

namespace Scribestar.ET.Regression.Core.Support
{
    [Binding]
    public static class WebDriverSupport
    {
        private static readonly RemoteWebDriver WebDriver = SharedDriver.DriverInstance.WebDriver;

        private static bool ReuseWebSession
        {
            get { return ConfigurationManager.AppSettings["ReuseWebSession"] == "true"; }
        }

        [Before]
        public static void BeforeWebSession()
        {
            SharedDriver.DriverInstance.SetUp();
        }

        [AfterScenario]
        public static void AfterWebScenario()
        {
            if (ScenarioInformation.FailedScenario == null)
                WebDriver.EmbedScreenshot();

            if (!ReuseWebSession)
                SharedDriver.DriverInstance.TearDown();
        }

        [AfterTestRun]
        public static void AfterWebSession()
        {
            SharedDriver.DriverInstance.TearDown();
        }
    }
}
