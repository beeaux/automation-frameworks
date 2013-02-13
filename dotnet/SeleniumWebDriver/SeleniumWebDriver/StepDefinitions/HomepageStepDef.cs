using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumWebDriver.Pages;
using SeleniumWebDriver.Support;
using SeleniumWebDriver.Support.Utilities;
using TechTalk.SpecFlow;

namespace SeleniumWebDriver.StepDefinitions
{
    [Binding]
    public class HomepageStepDef
    {
        private RemoteWebDriver WebDriver = SharedDriver.DriverInstance.WebDriver;
        private readonly Homepage _homepage = new Homepage();

        public HomepageStepDef()
        {
            PageFactory.InitElements(WebDriver, _homepage);
        }

        [Given("^I (?:go to|am on) the homepage$")]
        public void GoToOrAmOnTheHomepage()
        {
            _homepage.NavigateToHomepage();
            AssertionHandlers.BrowserTitleShouldContain("");
        }
    }
}
