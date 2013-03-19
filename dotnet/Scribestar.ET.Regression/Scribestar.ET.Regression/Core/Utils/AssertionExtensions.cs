using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Scribestar.ET.Regression.Core.Utils
{
    public static class AssertionExtensions
    {
        private static string reason;


        /// <summary>
        /// 
        /// </summary>
        public static void ThrowShouldBeAssignableToException(string reason)
        {
            var err = new Exception();
            err.Should().BeAssignableTo<ExpectedConditions>(reason);
        }

        public static void ShouldContain(this IWebDriver driver, string _string)
        {
            var wait = new WebDriverWait(driver, SharedDriver.TimeToWait);
            var title = driver.Title;

            if (!wait.Until(ExpectedConditions.TitleContains(_string)))
            {
                reason = "Expected page title as {" + _string + "} but actual title is {" + title + "}.";
                ThrowShouldBeAssignableToException(@reason);
            }
        }

        public static void ShouldContain(this IWebElement element, string _string)
        {
            var text = element.Text;
            reason = "Expected element {" + element + "} to contain {" + text + "}";
            text.Should().Contain(_string, reason);
        }
    }
}
