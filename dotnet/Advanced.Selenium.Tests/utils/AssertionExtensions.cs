using System;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Arena.EnhancedNews.Regression.Utils
{
    public static class AssertionExtensions
    {
        private static string _exception;

        public static void ShouldContain(this IWebElement element, string expected)
        {
            var text = element.Text;
            _exception = "Expected element {" + element + "} to contain {" + text + "}";
            text.Should().Contain(expected, _exception);
        }

        public static void ThrowShouldBeAssignableToException(string exception)
        {
            var err = new Exception();
            err.Should().BeAssignableTo<ExpectedConditions>(exception);
        }
    }
}
