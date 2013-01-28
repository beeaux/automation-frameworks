using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ScribeStar.Regression.Tests.Core;
using ScribeStar.Regression.Tests.Core.Utils;

namespace ScribeStar.Regression.Tests.Pages
{
    public class Homepage
    {
        private SharedDriver sharedDriver;

        [FindsBy(Using = "", How = How.CssSelector), CacheLookup]
        private IWebElement logo { get; set; }

        public Homepage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(EnvironmentConfiguration.GetEnvironment());
            AssertionHandlers.BrowserTitleShouldContain("");

            this.sharedDriver = (SharedDriver)driver;
        }
    }
}
