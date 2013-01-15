using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumWebDriver.Support.Utilities;
using System.Collections.ObjectModel;

namespace SeleniumWebDriver.Support
{
    public static class WebDriverCustomMethods
    {
        private static RemoteWebDriver driver = SharedDriver.DriverInstance.WebDriver;
        public static IAlert Alert;

        public static IWebDriver CurrentPage()
        {
            return driver;
        }

        /// <summary>
        ///     Web Element Finders
        /// </summary>
        public static IWebElement GetWebElementByCssSelector(string selector)
        {
            return driver.FindElementByCssSelector(selector);
        }

        public static IWebElement GetElementById(string id)
        {
            return driver.FindElementById(id);
        }

        public static IWebElement GetElementByLinkText(string linkText)
        {
            return driver.FindElementByLinkText(linkText);
        }

        public static ReadOnlyCollection<IWebElement> GetElementsByTagName(string tagName)
        {
            return driver.FindElementsByName(tagName);
        }

        public static ReadOnlyCollection<IWebElement> GetElementsByCssSelector(string selector)
        {
            return driver.FindElementsByCssSelector(selector);
        }

        public static IWebElement GetElementByXPath(string xpath)
        {
            return driver.FindElementByXPath(xpath);
        }

        /// <summary>
        ///     Action Handlers
        /// </summary>
        public static void NavigateTo(this RemoteWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public static void TypeText(this IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        public static void ClickOn(this IWebElement element)
        {
            element.Click();
        }

        public static void SelectAValue(this IWebElement element, string value)
        {
            var select = new SelectElement(element);
            @select.SelectByText(value);
        }

        public static void MouseOver(this IWebElement element)
        {
            var action = new Actions(CurrentPage());
            @action.MoveToElement(element).Perform();
        }

        public static void ClearField(this IWebElement element)
        {
            element.Clear();
        }

        /// <summary>
        ///     Custom Wait Handler
        /// </summary>
        public static void WaitForElement(string locator)
        {
            var wait = new WebDriverWait(CurrentPage(), SharedDriver.TimeToWait);
            if (wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(locator))) != null) return;

            var errMsg = "Element: {" + locator + "} is not visible or loaded on " + CurrentPage().Url;
            CustomAssertions.ThrowShouldBeAssignableToException(@errMsg);
        }

        public static void WaitForAlertElement(this IWebElement element)
        {
            var alert = (IWebElement)Alert;
            if (!IsElementDisplayed(@alert).Equals(false)) return;

            var errMsg = "Expected popup alert failed to display on " + CurrentPage().Url;
            CustomAssertions.ThrowShouldBeAssignableToException(@errMsg);
        }

        public static bool IsElementDisplayed(this IWebElement element)
        {
            return element.Displayed;
        }

        public static bool IsTextPresent(string text)
        {
            return CurrentPage().PageSource.Contains(text);
        }

        public static IAlert SwitchToAlert(this IWebDriver driver)
        {
            var alert = driver.SwitchTo().Alert();
            return @alert;
        }

        public static void ChooseOkOnNextConfirmation()
        {
            var accept = SwitchToAlert(CurrentPage());
            @accept.Accept();
        }

        public static void ChooseCancelOnNextConfirmation()
        {
            var cancel = SwitchToAlert(CurrentPage());
            cancel.Dismiss();
        }
    }
}
