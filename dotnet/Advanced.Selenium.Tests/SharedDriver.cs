using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using Arena.EnhancedNews.Regression.Core;
using Arena.EnhancedNews.Regression.Utils;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using TechTalk.SpecFlow;

namespace Arena.EnhancedNews.Regression
{
    public class SharedDriver
    {
        public static SharedDriver DriverInstance = new SharedDriver();
        public ScreenShotRemoteWebDriver WebDriver { get; private set; }
        public static readonly TimeSpan TimeToWait = TimeSpan.FromSeconds(25);
        private static DesiredCapabilities _capabilities;
        private DriverService _driverservice;

        //configurable parameters
        private readonly string _driverPaths = AppConfiguration.DriversPath;
        private static readonly Platform _platform = Platform.CurrentPlatform;
        private readonly string _browser = ConfigurationManager.AppSettings["Browser"];
        private readonly bool _is64BitOS = Environment.Is64BitOperatingSystem;
        private const string Localhost = "http://localhost:4444/wd/hub";

        // directories & drivers
        private static string _chromedriver = "chromedriver";
        private static string _ieDriverServer = "IEDriverServer";
        private static string _phantomjs = "phantomjs";

        private static void Trace(string _string)
        {
            Console.WriteLine("-> {0}", _string);
        }

        public static string GetCurrentPlatform()
        {
            string platform = null;

            if (_platform.IsPlatformType(PlatformType.Mac))
            {
                platform = "Mac";
            }
            else if (_platform.IsPlatformType(PlatformType.Linux) || _platform.IsPlatformType(PlatformType.Unix))
            {
                platform = "Linux";
            }
            else if (_platform.IsPlatformType(PlatformType.Vista) || _platform.IsPlatformType(PlatformType.Windows) || _platform.IsPlatformType(PlatformType.WinNT) || _platform.IsPlatformType(PlatformType.XP))
            {
                platform = "Windows";
            }
            else if (_platform.IsPlatformType(PlatformType.Android))
            {
                platform = "Android";
            }

            return platform;
        }

        public void SetUp()
        {
            if (WebDriver != null) return;

            if (_browser.Equals("firefox"))
            {
                SetWebDriverToFirefox();
            }
            else if (_browser.Equals("ie"))
            {
                if (GetCurrentPlatform().Equals("Windows"))
                {
                    if (_is64BitOS)
                        _ieDriverServer = _ieDriverServer + "_x64";
                    else
                        _ieDriverServer = _ieDriverServer + "_Win32";
                }
                else
                {
                    Trace("Assigned browser {" + _browser + "} is not supported on " + _platform);
                    var err = new WebDriverException();
                    err.Should().BeAssignableTo<WebDriverException>();
                    TearDown();
                }
                SetWebDriverToIE();
            }
            else if (_browser.Equals("chrome"))
            {
                if (GetCurrentPlatform().Equals("Mac"))
                {
                    _chromedriver = _chromedriver + "_mac";
                }
                else if (GetCurrentPlatform().Equals("Linux"))
                {
                    if (!_is64BitOS)
                        _chromedriver = _chromedriver + "_linux32";
                    else
                        _chromedriver = _chromedriver + "_linux64";
                }
                else
                {
                    _chromedriver = _chromedriver + "_win";
                }

                SetWebDriverToChrome();
            }
            else if (_browser.Equals("safari"))
            {
                SetWebDriverToSafari();
            }
            else
            {
                if (GetCurrentPlatform().Equals("Mac"))
                {
                    _phantomjs = _phantomjs + "_mac";
                }
                else if (GetCurrentPlatform().Equals("Windows"))
                {
                    _phantomjs = _phantomjs + "_win";
                }
                else if (GetCurrentPlatform().Equals("Linux"))
                {
                    if (!_is64BitOS)
                        _phantomjs = _phantomjs + "_linux32";
                    else
                        _phantomjs = _phantomjs + "_linux64";
                }
                SetWebDriverToPhantomJs();
            }

            if (WebDriver != null)
            {
                WebDriver.Manage().Window.Maximize();
                WebDriver.Manage().Timeouts().ImplicitlyWait(TimeToWait);
            }

            Trace("Web driver {" + _browser + "} successfully started...");
            ScenarioContext.Current.SetWebDriver(WebDriver);
        }

        private void SetWebDriverToPhantomJs()
        {
            try
            {
                _driverservice.Start();
            }
            catch (Exception err)
            {
                throw new DriverServiceNotFoundException(err.Message);
            }

            _capabilities = DesiredCapabilities.PhantomJS();
            SetAdditionalCapabilities();

            WebDriver = new ScreenShotRemoteWebDriver(_driverservice.ServiceUrl, _capabilities);
        }

        private void SetWebDriverToSafari()
        {
            var settings = new SafariOptions
            {
                SafariLocation = "",
                SkipExtensionInstallation = true
            };

            _capabilities = DesiredCapabilities.Safari();
            SetAdditionalCapabilities();

            WebDriver = new ScreenShotRemoteWebDriver(new Uri(Localhost), _capabilities);
        }


        private void SetWebDriverToChrome()
        {
            var driverExePath = _driverPaths + "chrome" + Path.DirectorySeparatorChar + _chromedriver;
            _driverservice = ChromeDriverService.CreateDefaultService(driverExePath);
            var settings = new ChromeOptions
            {
                BinaryLocation = "",
                LeaveBrowserRunning = true
            };

            try
            {
                _driverservice.Start();
            }
            catch (Exception err)
            {
                throw new DriverServiceNotFoundException(err.Message);
            }

            _capabilities = DesiredCapabilities.Chrome();
            SetAdditionalCapabilities();

            WebDriver = new ScreenShotRemoteWebDriver(_driverservice.ServiceUrl, _capabilities);
        }

        private void SetWebDriverToIE()
        {
            var driverExePath = _driverPaths + "ie" + Path.DirectorySeparatorChar + _ieDriverServer;
            _driverservice = InternetExplorerDriverService.CreateDefaultService(driverExePath);
            var settings = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                EnsureCleanSession = true
            };

            try
            {
                _driverservice.Start();
            }
            catch (Exception err)
            {
                throw new DriverServiceNotFoundException(err.Message);
            }

            _capabilities = DesiredCapabilities.InternetExplorer();
            SetAdditionalCapabilities();

            WebDriver = new ScreenShotRemoteWebDriver(_driverservice.ServiceUrl, _capabilities);
        }

        private void SetWebDriverToFirefox()
        {
            _capabilities = DesiredCapabilities.Firefox();
            _capabilities.SetCapability(FirefoxDriver.AcceptUntrustedCertificates.ToString(), true);
            _capabilities.SetCapability(FirefoxDriver.BinaryCapabilityName, "webdriver.firefox.bin");
            SetAdditionalCapabilities();

            WebDriver = new ScreenShotRemoteWebDriver(new Uri(Localhost), _capabilities);
        }

        public void TearDown()
        {
            if (WebDriver == null) return;
            try
            {
                WebDriver.Quit();
                WebDriver.Dispose();
            }
            catch (Exception err)
            {
                //WebDriver.EmbedScreenshot();
                Debug.WriteLine(err, "Web driver couldn't be stopped...");
            }
            WebDriver = null;
            Trace("Web driver {" + _browser + "} successfully stopped...");
        }

        private static void SetAdditionalCapabilities()
        {
            _capabilities.SetCapability(CapabilityType.AcceptSslCertificates, true);
            _capabilities.SetCapability(CapabilityType.TakesScreenshot, true);
            _capabilities.SetCapability(CapabilityType.SupportsFindingByCss, true);
        }
    }
}
