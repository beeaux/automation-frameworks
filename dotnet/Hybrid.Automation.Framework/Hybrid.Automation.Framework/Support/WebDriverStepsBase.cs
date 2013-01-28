using OpenQA.Selenium.Remote;

namespace Hybrid.Automation.Framework.Support
{
    public abstract class WebDriverStepsBase
    {
        protected RemoteWebDriver driver
        {
            get { return SharedDriver.DriverInstance.WebDriver; }
        }
    }
}
