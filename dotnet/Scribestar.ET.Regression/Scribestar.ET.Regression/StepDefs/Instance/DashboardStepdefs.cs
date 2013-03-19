using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using Scribestar.ET.Regression.Pages.Instance;
using TechTalk.SpecFlow;
using Scribestar.ET.Regression.Core.Utils;
using Scribestar.ET.Regression.Core.Support;

namespace Scribestar.ET.Regression.StepDefs.Instance
{
    [Binding]
    public class DashboardStepdefs
    {
        private readonly RemoteWebDriver Driver = SharedDriver.DriverInstance.WebDriver;
        private Dashboard page = new Dashboard();


        /// <summary>
        ///     Page Object binding constructor
        /// </summary>
        public DashboardStepdefs()
        {
            PageFactory.InitElements(Driver, page);
        }

        /// <summary>
        ///     step defs
        /// </summary>
        [Then(@"I be (?:redirected to|on) the '(.*)' page")]
        public void ThenIBeRedirectedToThePage(string page)
        {
            Driver.ShouldContain("ScribeStar Dashboard");
        }

        [When(@"I click on '(.*)'")]
        public void WhenIClickOn(string link)
        {
            var element = WebDriverExtensions.FindWebElementByLinkText(link);
            element.ClickOn();
        }
    }
}
