using Hybrid.Automation.Framework.Core;
using Hybrid.Automation.Framework.Core.Utils;
using Hybrid.Automation.Framework.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Hybrid.Automation.Framework.Pages
{
    public class Homepage
    {
        private SharedDriver sharedDriver;

        [FindsBy(Using = "", How = How.CssSelector), CacheLookup] private IWebElement logo { get; set; }

        public Homepage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(EnvironmentConfiguration.GetEnvironment());
            AssertionHandlers.BrowserTitleShouldContain("");

            this.sharedDriver = (SharedDriver)driver;
        }
    }
}
