using System.Text;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Arena.EnhancedNews.Regression.Utils
{
    public static class ScenarioRemoteWebDriver
    {
        public static ScreenShotRemoteWebDriver WebDriver(this ScenarioContext scenario)
        {
            var driver = (ScreenShotRemoteWebDriver)ScenarioContext.Current["driver"];
            driver.Should().NotBeNull("Web driver is not started...");
            return driver;
        }

        public static bool IsWebDriverRunning(this ScenarioContext scenario)
        {
            return ScenarioContext.Current["driver"] != null;
        }

        public static void SetWebDriver(this ScenarioContext scenario, ScreenShotRemoteWebDriver driver)
        {
            ScenarioContext.Current["driver"] = driver;
            ScenarioContext.Current["driver-errors"] = new StringBuilder();
        }

        public static StringBuilder WebDriverErrors(this ScenarioContext scenario)
        {
            var err = (StringBuilder)ScenarioContext.Current["driver-errors"];
            err.Should().NotBeNull("Web driver is not started...");
            return err;
        }
    }
}
