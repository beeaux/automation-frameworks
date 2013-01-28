using Coypu;
using Hybrid.Automation.Framework.Core;
using Hybrid.Automation.Framework.Core.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Hybrid.Automation.Framework.Support
{
    public class SharedDriver
    {
        public static SharedDriver DriverInstance = new SharedDriver();
        private DesiredCapabilities _capabilities;
        private DriverService _service;
        public RemoteWebDriver WebDriver { get; private set; }
        public static readonly TimeSpan TimeToWait = TimeSpan.FromSeconds(10);

        private static readonly string _platform = Platform.CurrentPlatform.PlatformType.ToString().ToLowerInvariant();
        private readonly string _browser = AppConfiguration.Browser;
        private static readonly string _osVersion = Environment.Is64BitOperatingSystem.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
        private static string _chromeDir = "chromedriver";
        private static string _ieDir = "IEDriverServer";

        private static void Trace(string message)
        {
            Console.WriteLine("-> {0}", message);
        }


        public void SetUp()
        {
            if(WebDriver != null) return;

            if ("chrome".Equals(_browser))
            {
                SetWebDriverToChrome();
            }
            else if ("ie".Equals(_browser) || "".Equals(_browser)) 
            {
                SetWebDriverToIE();
            }
            else if ("htmlunit".Equals(_browser))
            {
                SetWebDriverToHtmlUnit();
            }
            else if ("android".Equals(_browser))
            {
                SetWebDriverToAndroid();
            }
            else
            {
                SetWebDriverToFirefox();
            }

            if (!_browser.Equals("android") && !_browser.Equals("htmlunit"))
            {
                WebDriver.Manage().Window.Maximize();
            }
            WebDriver.Manage().Timeouts().ImplicitlyWait(TimeToWait);
        }

        private void SetWebDriverToChrome()
        {
            var driverPath = AppConfiguration.BrowserDirectory + Helpers._separator + "Chrome" + Helpers._separator + _chromeDir;
            _service = ChromeDriverService.CreateDefaultService(driverPath);

            try
            {
                _service.Start();
            }
            catch (DriverServiceNotFoundException err)
            {
                throw new DriverServiceNotFoundException(err.Message);
            }

            _capabilities = DesiredCapabilities.Chrome();
            WebDriver = new RemoteWebDriver(_service.ServiceUrl, _capabilities);
        }

        private void SetWebDriverToIE()
        {
            var driverPath = AppConfiguration.BrowserDirectory + Helpers._separator + "IE" + Helpers._separator + _ieDir;

            _service = InternetExplorerDriverService.CreateDefaultService(driverPath);
            var settings = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                EnableNativeEvents = true,
                IgnoreZoomLevel = true
            };

            try
            {
                _service.Start();
            }
            catch (DriverServiceNotFoundException err)
            {
                throw new DriverServiceNotFoundException(err.Message);
            }

            _capabilities = DesiredCapabilities.InternetExplorer();
            WebDriver = new RemoteWebDriver(_service.ServiceUrl, _capabilities);
        }

        private void SetWebDriverToHtmlUnit()
        {
            _capabilities = DesiredCapabilities.HtmlUnitWithJavaScript();
            //_capabilities.SetCapability("", true);

            WebDriver = new RemoteWebDriver(new Uri("http://127.­0.­0.­1:4444/­wd/­hub"), _capabilities);
        }

        private void SetWebDriverToAndroid()
        {
            _capabilities = DesiredCapabilities.Android();

            WebDriver = new RemoteWebDriver(_capabilities);
        }

        private void SetWebDriverToFirefox()
        {
            var setting = FirefoxDriver.AcceptUntrustedCertificates.ToString(CultureInfo.InvariantCulture);
            _capabilities = DesiredCapabilities.Firefox();
            _capabilities.SetCapability(setting, true);

            var profile = (new FirefoxProfileManager()).GetProfile("default");
            WebDriver = new FirefoxDriver(profile);
        }

        public void TearDown()
        {
            if(WebDriver == null) return;

            try
            {
                if (_service.IsRunning.Equals(true))
                {
                    _service.Dispose();
                }
                WebDriver.Quit();
                WebDriver.Dispose();
            }
            catch (WebDriverException err)
            {
                Debug.WriteLine(err, "Web driver stop error...");
            }
            WebDriver = null;
            Trace("Web driver stopped...");
        }

        public void DeleteAllCookies()
        {
            WebDriver.Manage().Cookies.DeleteAllCookies();
        }
    }
}
