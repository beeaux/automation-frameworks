using System.Configuration;
using Arena.EnhancedNews.Regression.Utils;
using TechTalk.SpecFlow;
namespace Arena.EnhancedNews.Regression.Core
{
    [Binding]
    public static class WebDriverSupport
    {
        private static readonly ScreenShotRemoteWebDriver WebDriver = SharedDriver.DriverInstance.WebDriver;

        private static bool ReUseWebSession
        {
            get
            {
                return ConfigurationManager.AppSettings["ReUseWebSession"] == "true";
            }
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
                //WebDriver.EmbedScreenshot;

            if (!ReUseWebSession)
                SharedDriver.DriverInstance.TearDown();
        }

        [AfterTestRun]
        public static void AfterWebSession()
        {
            SharedDriver.DriverInstance.TearDown();
        }
    }
}
