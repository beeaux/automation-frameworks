using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using ScribeStar.Regression.Tests.Pages;
using TechTalk.SpecFlow;

namespace ScribeStar.Regression.Tests.StepDefs
{
    [Binding]
    public class HomepageStepDef
    {
        private SharedDriver sharedDriver;
        private Homepage homepage;

        public HomepageStepDef(IWebDriver driver)
        {
            driver = (IWebDriver)sharedDriver;
            PageFactory.InitElements(driver, this);
        }

        [Given("^I (?:go to|am on) the homepage$")]
        public void GoToOrAmOnTheHomepage() { }
    }
}
