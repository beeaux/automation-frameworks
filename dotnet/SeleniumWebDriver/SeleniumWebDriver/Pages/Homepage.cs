using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumWebDriver.Support;
using SeleniumWebDriver.Support.Utilities;

namespace SeleniumWebDriver.Pages
{
    public class Homepage
    {
        private SharedDriver _driver;

        [FindsBy(Using = "", How = How.CssSelector), CacheLookup] private IWebElement logo { get; set; }

        public Homepage(IWebDriver driver)
        {
            WebDriverCustomMethods.NavigateTo(driver, EnvironmentConfiguration.GetEnvironment());
            CustomAssertions.BrowserTitleShouldContain("");

            this._driver = (SharedDriver)driver;
        }
    }
}
