using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumWebDriver.Support;
using SeleniumWebDriver.Support.Utilities;

namespace SeleniumWebDriver.Pages.Users
{
    public class SignIn
    {
        private SharedDriver _driver;

        [FindsBy(Using = "", How = How.CssSelector)] private IWebElement emailField;
        [FindsBy(Using = "", How = How.CssSelector)] private IWebElement passwordField;
        [FindsBy(Using = "", How = How.CssSelector)] public IWebElement signInButton;

        public SignIn(IWebDriver driver) {
            CustomAssertions.BrowserTitleShouldContain("");

            this._driver = (SharedDriver)driver;
        }

        public void EnterSignInCredentials(string email, string password)
        {
            WebDriverCustomMethods.TypeText(emailField, email);
            WebDriverCustomMethods.TypeText(passwordField, password);
        }
    }
}
