using OpenQA.Selenium;

namespace Scribestar.ET.Regression.Core.Support
{
    public abstract class WebDriverStepsBase
    {
        protected IWebDriver WebDriver
        {
            get { return SharedDriver.DriverInstance.WebDriver; }
        }
    }
}
