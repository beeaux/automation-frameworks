using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using SeleniumWebDriver.Pages.Users;
using SeleniumWebDriver.Support;
using SeleniumWebDriver.Support.Utilities;
using TechTalk.SpecFlow;
using FluentAssertions;
using log4net.Repository.Hierarchy;
using System;

namespace SeleniumWebDriver.StepDefinitions.Users
{
    [Binding]
    public class SignInStepDef
    {
        private SharedDriver _driver;
        private SignIn signInPage;

        public SignInStepDef(IWebDriver driver)
        {
            driver = (IWebDriver)_driver;
            PageFactory.InitElements(driver, this);
        }

        [Given("^I (?:am already|currently) signed in as (?:a|an) (?:Administrator|Editor|Lawyer|Reviewer)")]
        [Given("^I enter my signin details")]
        public void AmAlreadyOrCurrentlySignedInAs(Table dataset)
        {
            foreach (var data in dataset.Rows)
            {
                var email = data["Email"];
                var password = data["Password"];

                if (!email.Length.Equals(0) && !password.Length.Equals(0))
                {
                    signInPage.EnterSignInCredentials(email, password);
                    WebDriverCustomMethods.ClickOn(signInPage.signInButton);
                }
                else
                {
                    var reason = "Both email and password cannot be null or empty";
                    Console.WriteLine(@reason);
                }
            }
        }

    }
}
