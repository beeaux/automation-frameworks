using Hybrid.Automation.Framework.Pages;
using Hybrid.Automation.Framework.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace Hybrid.Automation.Framework.StepDefs
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
        public void GoToOrAmOnTheHomepage()
        {
        }
    }
}
