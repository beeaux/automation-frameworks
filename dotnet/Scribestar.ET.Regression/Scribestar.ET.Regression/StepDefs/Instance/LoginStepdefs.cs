using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using Scribestar.ET.Regression.Pages.Instance;
using TechTalk.SpecFlow;
using Scribestar.ET.Regression.Core.Support;
using Scribestar.ET.Regression.Core.Utils;
using System.Configuration;

namespace Scribestar.ET.Regression.StepDefs.Instance
{
    [Binding]
    public class LoginStepdefs
    {
        private Login page = new Login();
        private readonly RemoteWebDriver Driver = SharedDriver.DriverInstance.WebDriver;

        /// <summary>
        ///     Page Object binding constructor
        /// </summary>
        public LoginStepdefs()
        {
            PageFactory.InitElements(Driver, page);
        }

        public void GoToScribeStarHomepage()
        {
            var environment = ConfigurationManager.AppSettings["Environment"];
            var url = "http://www."+ environment +".scribestar/";
            
            Driver.NavigateTo(url);
            Driver.ShouldContain("ScribeStar Login");
        }


        /// <summary>
        ///     step defs
        /// </summary>
        [Given(@"I log in with my account credentials")]
        public void GivenILogInWithMyAccountCredentials(Table dataset)
        {
            GoToScribeStarHomepage();

            foreach (var data in dataset.Rows)
            {
                var email = data["Email Address"];
                var password = data["Password"];

                page.EnterEmailAddressAndPassword(email, password);

                page.ClickLogin();
            }
        }
    }
}
