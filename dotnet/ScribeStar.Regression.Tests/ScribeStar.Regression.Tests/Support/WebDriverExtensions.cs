using NUnit.Framework;
using OpenQA.Selenium.Remote;
using System.Text;
using TechTalk.SpecFlow;

namespace ScribeStar.Regression.Tests.Core
{
    public static class WebDriverExtensions
    {
        public static RemoteWebDriver WebDriver(this ScenarioContext sc)
        {
            var result = (RemoteWebDriver)ScenarioContext.Current["driver"];
            Assert.IsNotNull(result, "driver is not started");
            return result;
        }

        public static bool IsWebDriverRunning(this ScenarioContext sc)
        {
            return ScenarioContext.Current["driver"] != null;
        }

        public static void SetWebDriver(this ScenarioContext sc, RemoteWebDriver driver)
        {
            ScenarioContext.Current["driver"] = driver;
            ScenarioContext.Current["driver-errors"] = new StringBuilder();
        }

        public static StringBuilder WebDriverErrors(this ScenarioContext sc)
        {
            var result = (StringBuilder)ScenarioContext.Current["driver-errors"];
            Assert.IsNotNull(result, "driver is not started");
            return result;
        }
    }
}
