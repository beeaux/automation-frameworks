using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumWebDriver.Support;
using SeleniumWebDriver.Support.Utilities;

namespace SeleniumWebDriver.Pages
{
    public class Homepage
    {
        private RemoteWebDriver WebDriver = SharedDriver.DriverInstance.WebDriver;

        [FindsBy(Using = "", How = How.CssSelector), CacheLookup] private IWebElement logo { get; set; }

        public void NavigateToHomepage()
        {
            WebDriver.NavigateTo(EnvironmentConfiguration.GetEnvironment());
        }
    }
}
