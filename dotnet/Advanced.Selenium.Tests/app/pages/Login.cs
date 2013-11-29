using Arena.EnhancedNews.Regression.Core;
using Arena.EnhancedNews.Regression.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace Arena.EnhancedNews.Regression.Pages
{
    [Binding]
    public class Login
    {
        /// <summary>
        ///     page objects
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#EmailAddress")]
        [CacheLookup] 
        private IWebElement EmailAddress;

        [FindsBy(How = How.CssSelector, Using = "#Password")]
        [CacheLookup]
        private static IWebElement Password;

        [FindsBy(How = How.CssSelector, Using = "#submit-button a img")]
        [CacheLookup]
        private static IWebElement LoginButton;

        [FindsBy(How = How.CssSelector, Using = ".workspaceManager .header .logo")]
        [CacheLookup]
        private static IWebElement Logo;

        /// <summary>
        /// 
        /// </summary>
        private static void LaunchArenaHTML()
        {
            WebDriverExtension.GoTo(AppConfiguration.HostEnvironment());
            // page title verification here.
        }

        public void LoginAs(string email, string password)
        {
            LaunchArenaHTML();

            WebDriverExtension.FindWebElementByCssSelector("#EmailAddress").SendKeys(email);
            WebDriverExtension.FindWebElementByCssSelector("#Password").SendKeys(password);
            WebDriverExtension.FindWebElementByCssSelector("#submit-button a img").ClickOn();
            WebDriverExtension.Wait(".workspaceManager .header .logo");
        }
    }
}
