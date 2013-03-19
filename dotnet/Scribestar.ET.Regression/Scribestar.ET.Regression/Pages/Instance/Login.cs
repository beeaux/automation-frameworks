using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Scribestar.ET.Regression.Core.Support;
using TechTalk.SpecFlow;

namespace Scribestar.ET.Regression.Pages.Instance
{
    [Binding]
    public class Login
    {
        /// <summary>
        ///     Page Object Finders.
        /// </summary>
        [FindsBy(How = How.Id, Using = "emailAddressInput")]
        [CacheLookup]
        private IWebElement EmailAddressField;

        [FindsBy(How = How.Id, Using = "passwordInput")]
        [CacheLookup]
        private IWebElement PasswordField;

        [FindsBy(How = How.Id, Using = "loginButton")]
        [CacheLookup]
        private IWebElement LoginButton;


        /// <summary>
        ///     actions.
        /// </summary>
        public void EnterEmailAddressAndPassword(string email, string password)
        {
            EmailAddressField.TypeText(email);
            PasswordField.TypeText(password);
        }

        public void ClickLogin()
        {
            LoginButton.ClickOn();
        }
    }
}
