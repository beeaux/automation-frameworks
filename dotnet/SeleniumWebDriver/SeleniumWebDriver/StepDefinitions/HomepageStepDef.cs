using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumWebDriver.Pages;
using SeleniumWebDriver.Support;
using TechTalk.SpecFlow;

namespace SeleniumWebDriver.StepDefinitions
{
    [Binding]
    public class HomepageStepDef
    {
        private SharedDriver _driver;
        private Homepage homepage;

        public HomepageStepDef(IWebDriver driver)
        {
            driver = (IWebDriver)_driver;
            PageFactory.InitElements(driver, this);
        }

        [Given("^I (?:go to|am on) the homepage$")]
        public void IGoToOrAmOnTheHomepage()
        {
        }
    }
}
