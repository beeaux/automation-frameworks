using System.Configuration;
using Arena.EnhancedNews.Regression.Pages;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace Arena.EnhancedNews.Regression.Stepdefs
{
    [Binding]
    public class LoginStepDef
    {
        private readonly ScreenShotRemoteWebDriver _driver = SharedDriver.DriverInstance.WebDriver;
        private static readonly Login _login = new Login();

        public LoginStepDef()
        {
            PageFactory.InitElements(_driver, _login);
        }

        public static void LogIntoArena()
        {
            var email = ConfigurationManager.AppSettings["EmailAddress"];
            var password = ConfigurationManager.AppSettings["Password"];
            _login.LoginAs(email, password);
        }
    }
}
