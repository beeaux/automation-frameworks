using OpenQA.Selenium.Remote;

namespace ScribeStar.Regression.Tests.Core
{
    public abstract class WebDriverStepsBase
    {
        protected RemoteWebDriver driver
        {
            get { return SharedDriver.DriverInstance.WebDriver; }
        }
    }
}
