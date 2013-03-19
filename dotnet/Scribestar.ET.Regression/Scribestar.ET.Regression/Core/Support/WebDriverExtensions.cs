using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Scribestar.ET.Regression.Core.Support
{
    public static class WebDriverExtensions
    {
        private static readonly RemoteWebDriver WebDriver = SharedDriver.DriverInstance.WebDriver;
        private static readonly WebDriverWait Wait = new WebDriverWait(CurrentPage(), SharedDriver.TimeToWait);
        private static IAlert Alert;

        public static RemoteWebDriver CurrentPage()
        {
            return WebDriver;
        }

        /// <summary>
        ///     Custom page element finders.
        /// </summary>
        public static IWebElement FindWebElementByLinkText(string linkText)
        {
            return WebDriver.FindElementByLinkText(linkText);
        }

        public static IWebElement FindWebElementByCssSelector(string css)
        {
            return WebDriver.FindElementByCssSelector(css);
        }

        public static ReadOnlyCollection<IWebElement> FindWebElementsByCssSelector(string css)
        {
            return WebDriver.FindElementsByCssSelector(css);
        }

        public static ReadOnlyCollection<IWebElement> FindWebElementsByTagName(string tagName)
        {
            return WebDriver.FindElementsByTagName(tagName);
        }

        /// <summary>
        ///     Custom interaction handlers
        /// </summary>
        public static void NavigateTo(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void TypeText(this IWebElement element, string _string)
        {
            if (!element.GetAttribute("value").Length.Equals(0))
                element.Clear();

            element.SendKeys(_string);
        }

        public static void ClickOn(this IWebElement element)
        {
            if (element != null) element.Click();
        }

        public static void Select(this IWebElement element, string _string)
        {
            var select = new SelectElement(element);
            @select.SelectByText(_string);
        }

        public static void MouseOver(this IWebElement element)
        {
            var action = new Actions(CurrentPage());
            @action.MoveToElement(element).Perform();
        }

        public static bool IsElementDisplayed(this IWebElement element)
        {
            return element.Displayed;
        }

        public static void ClickButton(string css)
        {
            var element = FindWebElementByCssSelector(css);
            element.ClickOn();
        }


        /// <summary>
        ///     Custom wait handler
        /// </summary>
        public static void WaitForElement(string _string)
        {
            if (Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(_string))) != null) return;

            var reason = "Element: {" + _string + "} is not visible or loaded on " + CurrentPage().Url;
            var ex = new NoSuchElementException();
            //ex.Should().BeAssignableTo<ExpectedConditions>(@reason);
        }

        public static void WaitForAlert()
        {
            var alert = (IWebElement)Alert;
            if (alert.IsElementDisplayed()) return;

            var reason = "Alert popup failed to display on " + CurrentPage().Url;
        }


        /// <summary>
        ///     Custom alert and pop up handler
        /// </summary>
        public static IAlert SwitchToAlert()
        {
            var alert = CurrentPage().SwitchTo().Alert();
            return @alert;
        }

        public static void ChooseOkOnNextConfirmation()
        {
            var accept = SwitchToAlert();
            @accept.Accept();
        }

        public static void ChooseCancelOnNextConfirmation()
        {
            var cancel = SwitchToAlert();
            @cancel.Dismiss();
        }


    }
}
