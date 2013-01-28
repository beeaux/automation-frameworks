using OpenQA.Selenium;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using Hybrid.Automation.Framework.Support;

namespace Hybrid.Automation.Framework.Core.Utils
{
    public static class AssertionHandlers
    {
        private static string reason;

        public static void ThrowShouldBeAssignableToException(string reason)
        {
            var err = new NoSuchElementException();
            err.Should().BeAssignableTo<ExpectedConditions>(reason);
        }

        public static void ShouldContain(this IWebElement element, string _string)
        {
            var _expected = element.Text;
            reason = "";

            _expected.Should().Contain(@_string, @reason);
        }

        public static void BrowserTitleShouldContain(string _string)
        {
            var wait = new WebDriverWait(WebDriverCustomMethods.CurrentPage(), SharedDriver.TimeToWait);
            var actualTitle = WebDriverCustomMethods.CurrentPage().Title;

            if (!wait.Until(ExpectedConditions.TitleContains(_string)))
            {
                reason = "Expected page title as {" + _string + "} but actual title is {" + actualTitle + "}.";
                ThrowShouldBeAssignableToException(@reason);
            }
        }

        public static void ShouldNotContain(this IWebElement element, string _msg)
        {
            var _string = element.Text;
            reason = "";

            @_string.Should().NotContain(_msg, @reason);
        }

        public static void ShouldNotBeNullOrEmpty(this IWebElement element)
        {
            var _string = element.Text;
            reason = element + "is expected to contain a value but is null";
            _string.Should().NotBeNullOrEmpty(@reason);
        }

        public static void ShouldNotExceedExecutionTimeOf(this IWebElement element, int _elapseTime)
        {
            element.ExecutionTimeOf(e => element.IsElementDisplayed()).ShouldNotExceed(_elapseTime.Milliseconds());
        }
    }
}
