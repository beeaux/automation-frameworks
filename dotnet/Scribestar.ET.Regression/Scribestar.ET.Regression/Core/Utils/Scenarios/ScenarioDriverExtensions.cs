using System.Text;
using FluentAssertions;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace Scribestar.ET.Regression.Core.Utils.Scenarios
{
    public static class ScenarioDriverExtensions
    {
        public static RemoteWebDriver WebDriver(this ScenarioContext scenario)
        {
            var driver = (RemoteWebDriver)ScenarioContext.Current["driver"];
            driver.Should().NotBeNull("Web driver is not started...");
            return driver;
        }

        public static bool IsWebDriverRunning(this ScenarioContext scenario)
        {
            return ScenarioContext.Current["driver"] != null;
        }

        public static void SetWebDriver(this ScenarioContext scenario, RemoteWebDriver driver)
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
