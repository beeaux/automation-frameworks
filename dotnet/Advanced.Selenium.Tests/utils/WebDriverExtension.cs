using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace Arena.EnhancedNews.Regression.Utils
{
    public static class WebDriverExtension
    {
        private static readonly ScreenShotRemoteWebDriver WebDriver = SharedDriver.DriverInstance.WebDriver;
        private static WebDriverWait _wait = new WebDriverWait(CurrentDriver(), SharedDriver.TimeToWait);
        private static Actions _action = new Actions(CurrentDriver());

        /// <summary>
        ///     custom driver
        /// </summary>
        /// <returns></returns>
        public static ScreenShotRemoteWebDriver CurrentDriver()
        {
            return WebDriver;
        }

        /// <summary>
        ///     custom element finders
        /// </summary>
        /// <exception cref="StaleElementReferenceException"></exception>
        public static IWebElement FindWebElementByCssSelector(string css)
        {
            try
            {
                var element = CurrentDriver().FindElementByCssSelector(css);
                return !ElementIsDisplayed(element) ? null : element;
            }
            catch (NoSuchElementException err)
            {
                
                throw new NoSuchElementException(err.Message);
            }
            catch(StaleElementReferenceException err)
            {
                throw new StaleElementReferenceException(err.Message);
            }
        }

        public static ReadOnlyCollection<IWebElement> FindWebElementsByCssSelector(string css)
        {
            return CurrentDriver().FindElementsByCssSelector(css);
        }

        public static IWebElement FindWebElementById(string id)
        {
            try
            {
                var element = CurrentDriver().FindElementById(id);
                return !ElementIsDisplayed(element) ? null : element;
            }
            catch (NoSuchElementException err)
            {

                throw new NoSuchElementException(err.Message);
            }
            catch (StaleElementReferenceException err)
            {
                throw new StaleElementReferenceException(err.Message);
            }
        }

        public static IWebElement FindWebElementByLinkText(string linkText)
        {
            try
            {
                var element = CurrentDriver().FindElementByLinkText(linkText);
                return !ElementIsDisplayed(element) ? null : element;
            }
            catch (NoSuchElementException err)
            {

                throw new NoSuchElementException(err.Message);
            }
            catch (StaleElementReferenceException err)
            {
                throw new StaleElementReferenceException(err.Message);
            }
        }

        /// <summary>
        ///     fluent wait 
        /// </summary>
        /// <returns></returns>
        public static bool ElementIsDisplayed(this IWebElement element)
        {
            return element.Displayed;
        }

        public static void Wait(string selector)
        {
            if(_wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(selector))) != null) return;
            var exception = "Element: {" + selector + "} is not visible or loaded on " + CurrentDriver().Url;
            AssertionExtensions.ThrowShouldBeAssignableToException(exception);
        }

        /// <summary>
        ///     actions
        /// </summary>
        public static void GoTo(string uri)
        {
            CurrentDriver().Navigate().GoToUrl(uri);
        }

        public static void TypeText(this IWebElement element, string text)
        {
            if (!element.GetAttribute("value").Length.Equals(0)) element.Clear();
            element.SendKeys(text);
        }

        public static void ClickOn(this IWebElement element)
        {
            element.Click();
        }

        public static void Select(this IWebElement element, string text)
        {
            var select = new SelectElement(element);
            @select.SelectByText(text);
        }

        // to be refactored.
        public static void PressKey(this IWebElement element, string key)
        {
            if (key == null) throw new ArgumentNullException("key");
            _action.SendKeys(element, key).Perform();
        }
    }
}
