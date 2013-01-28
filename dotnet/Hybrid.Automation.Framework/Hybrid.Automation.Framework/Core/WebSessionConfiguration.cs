using Coypu;
using Hybrid.Automation.Framework.Support;
using System;

namespace Hybrid.Automation.Framework.Core
{
    public class WebSessionConfiguration
    {
        SessionConfiguration session = new SessionConfiguration
        {
            AppHost = EnvironmentConfiguration.GetEnvironment(),
            Port = 5555,
            SSL = true|false,
            Timeout = SharedDriver.TimeToWait,
            WaitBeforeClick = TimeSpan.FromMilliseconds(50)
        };

        public void ConfigCoypu()
        {
            var driver = "Coypu.Drivers." + GetDriver() + ", Coypu";
            session.Driver = Type.GetType(driver);
            session.Browser = Coypu.Drivers.Browser.Parse(AppConfiguration.Browser);
        }

        public static string GetDriver()
        {
            string _driver;
            var driver = AppConfiguration.TestDriver;
            switch (driver)
            {
                case "watin":
                    _driver = "Watin";
                    break;
                default:
                    _driver = "Selenium.SeleniumWebDriver";
                    break;
            }
            return _driver;
        }
    }
}
