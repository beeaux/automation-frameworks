using System;
using System.Diagnostics;
using System.Globalization;
using SeleniumWebDriver.Support.Utilities;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using SeleniumWebDriver.Support;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;

namespace SeleniumWebDriver
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
            if (WebDriver != null) return;

            if("chrome".Equals(_browser)) 
            {
                if("mac".Equals(_platform)) 
                {
                    _chromeDir = _chromeDir + "_mac";
                }
                else if ("linux".Equals(_platform) || "unix".Equals(_platform))
                {
                    _chromeDir = _chromeDir + "_linux";
                }
                else
                {
                    _chromeDir = _chromeDir + "_win";
                }

                SetWebDriverToChrome();
            }
            else if("ie".Equals(_browser) || "internet explorer".Equals(_browser)) 
            {
                if(!_platform.StartsWith("m") && !_platform.StartsWith("u") && !_platform.StartsWith("l"))
                {
                    if("true".Equals(_osVersion)) 
                    {
                        _ieDir = _ieDir + "_x64";
                    }
                    else 
                    {
                        _ieDir = _ieDir + "_Win32";
                    }
                    SetWebDriverToIE();
                }
            }
            else {
                SetWebDriverToFirefox();
            }

            WebDriver.Manage().Window.Maximize();
            WebDriver.Manage().Timeouts().ImplicitlyWait(TimeToWait);

            Trace("Web driver started...");
            ScenarioContext.Current.SetWebDriver(WebDriver);
        }

        public void DeleteAllCookies()
        {
            WebDriver.Manage().Cookies.DeleteAllCookies();
        }

        private void SetWebDriverToIE()
        {
            var driverPath = AppConfiguration.BrowserDirectory + UtilityHelpers._separator + "InternetExplorer" + UtilityHelpers._separator + _ieDir;
            _service = InternetExplorerDriverService.CreateDefaultService(driverPath);
            var _settings = new InternetExplorerOptions {
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

        private void SetWebDriverToFirefox()
        {
            var _settings = FirefoxDriver.AcceptUntrustedCertificates.ToString(CultureInfo.InvariantCulture);
            _capabilities = DesiredCapabilities.Firefox();
            _capabilities.SetCapability(_settings, true);

            var _profile = (new FirefoxProfileManager()).GetProfile("default");
            WebDriver = new FirefoxDriver(_profile);
        }

        private void SetWebDriverToChrome()
        {
            var driverPath = AppConfiguration.BrowserDirectory + UtilityHelpers._separator + "Chrome" + UtilityHelpers._separator + _chromeDir;
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

        public void TearDown()
        {
            if (WebDriver == null) return;

            try
            {
                if (_service.IsRunning.Equals(true))
                {
                    _service.Dispose();
                }
                WebDriver.Quit();
                WebDriver.Dispose();
            }
            catch (Exception err)
            {
                Debug.WriteLine(err, "Web driver stop error...");
            }

            WebDriver = null;
            Trace("Web driver stopped...");
        }
    }
}
