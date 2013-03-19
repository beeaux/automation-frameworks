using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Scribestar.ET.Regression.Core.Utils;
using System;
using System.Configuration;
using System.Diagnostics;
using TechTalk.SpecFlow;
using Scribestar.ET.Regression.Core.Utils.Scenarios;

namespace Scribestar.ET.Regression
{
    public class SharedDriver
    {
        public static SharedDriver DriverInstance = new SharedDriver();
        public RemoteWebDriver WebDriver { get; private set; }
        public static readonly TimeSpan TimeToWait = TimeSpan.FromSeconds(5);

        private DesiredCapabilities _capabilities;
        private readonly string Browsers = ConfigurationSetting.Browsers;
        private static readonly string _platform = Platform.CurrentPlatform.PlatformType.ToString().ToLowerInvariant();
        private readonly string _browser = ConfigurationSetting.Browser;
        private readonly bool _is64bitOS = Environment.Is64BitOperatingSystem;
        private static string _chromedriver = "chromedriver";
        private static string _iedriverserver = "IEDriverServer";
        private static void Trace(string _string)
        {
            Console.WriteLine("-> {0}", _string);
        }

        public void SetUp()
        {
            if (WebDriver != null) return;

            if ("firefox".Equals(_browser))
            {
                SetWebDriverToFirefox();
            }
            else if ("ie".Equals(_browser) || "internetexplorer".Equals(_browser))
            {
                if (_platform.Contains("win") || "xp".Equals(_platform) || "vista".Equals(_platform))
                {
                    if (_is64bitOS)
                        _iedriverserver = _iedriverserver + "_x64";
                    else
                        _iedriverserver = _iedriverserver + "_Win32";
                }
                else
                {
                    Trace("Assigned browser {" + _browser + "} is not supported on " + _platform);
                    var err = new WebDriverException();
                    //err.Should().BeAssignableTo<WebDriverException>();
                    TearDown();
                }

                SetWebDriverToIE();
            }
            else if ("htmlunit".Equals(_browser))
            {
                LaunchSeleniumStandaloneServer();
                SetWebDriverToHtmlUnit();
            }
            else
            {
                if ("mac".Equals(_platform))
                {
                    _chromedriver = _chromedriver + "_mac";
                }
                else if ("linux".Equals(_platform) || "unix".Equals(_platform))
                {
                    if (_is64bitOS == false)
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

            if (!"htmlunit".Equals(_browser))
                WebDriver.Manage().Window.Maximize();

            WebDriver.Manage().Timeouts().ImplicitlyWait(TimeToWait);
            Trace("Web driver {" + _browser + "} successfully started...");
            ScenarioContext.Current.SetWebDriver(WebDriver);
        }

        private void LaunchSeleniumStandaloneServer()
        {
            const string script = @"java -jar C:\Dev\scribestar\lib\Selenium\selenium-server-standalone-2.30.0.jar";
            Helpers.LaunchCommandPrompt(script);
        }

        private void SetWebDriverToHtmlUnit()
        {
            _capabilities = DesiredCapabilities.HtmlUnitWithJavaScript();
            _capabilities.SetCapability(CapabilityType.IsJavaScriptEnabled, true);
            _capabilities.SetCapability(CapabilityType.TakesScreenshot, true);

            WebDriver = new RemoteWebDriver(_capabilities);
        }

        private void SetWebDriverToIE()
        {
            var driver = InternetExplorerDriverService.CreateDefaultService(Browsers + "IE" + Helpers._seperator + _iedriverserver);
            var settings = new InternetExplorerOptions
            {
                IntroduceInstabilityByIgnoringProtectedModeSettings = true
            };

            try
            {
                WebDriver = new InternetExplorerDriver(driver, settings);
            }
            catch (DriverServiceNotFoundException ex)
            {
                throw new DriverServiceNotFoundException(ex.Message);
            }
        }

        private void SetWebDriverToFirefox()
        {
            var settings = FirefoxDriver.AcceptUntrustedCertificates.ToString();
            _capabilities = DesiredCapabilities.Firefox();
            _capabilities.SetCapability(settings, true);

            var customprofile = (new FirefoxProfileManager().GetProfile("firefox_profile"));
            WebDriver = new FirefoxDriver(customprofile);
        }

        private void SetWebDriverToChrome()
        {
            var driver = ChromeDriverService.CreateDefaultService(Browsers + "Chrome" + Helpers._seperator + _chromedriver);

            var settings = new ChromeOptions();
            settings.AddAdditionalCapability(CapabilityType.SupportsFindingByCss, true);
            settings.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);

            try
            {
                WebDriver = new ChromeDriver(driver, settings);
            }
            catch (DriverServiceNotFoundException ex)
            {
                throw new DriverServiceNotFoundException(ex.Message);
            }
        }

        public void TearDown()
        {
            if (WebDriver == null) return;

            try
            {
                WebDriver.Quit();
                WebDriver.Dispose();
            }
            catch (Exception ex)
            {
                WebDriver.EmbedScreenshot();
                Debug.WriteLine(ex, "Web driver couldn't be stopped...");
            }
            WebDriver = null;
            Trace("Web driver {" + _browser + "} successfully stopped...");
        }

    }
}
