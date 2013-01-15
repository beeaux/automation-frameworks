using OpenQA.Selenium.Remote;

namespace SeleniumWebDriver.Support
{
    public abstract class WebDriverStepsBase
    {
        protected RemoteWebDriver driver
        {
            get { return SharedDriver.DriverInstance.WebDriver; }
        }
    }
}
