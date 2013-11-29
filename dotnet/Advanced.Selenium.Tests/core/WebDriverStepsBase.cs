using OpenQA.Selenium;

namespace Arena.EnhancedNews.Regression.Core
{
    public abstract class WebDriverStepsBase
    {
        protected IWebDriver WebDriver
        {
            get
            {
                return SharedDriver.DriverInstance.WebDriver;
            }
        }
    }
}
