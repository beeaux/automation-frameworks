using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Arena.EnhancedNews.Regression
{
    public class ScreenShotRemoteWebDriver : RemoteWebDriver, ITakesScreenshot
    {
        public ScreenShotRemoteWebDriver(Uri remoteAddress, ICapabilities capabilities) : base(remoteAddress, capabilities)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Screenshot GetScreenshot()
        {
            var screenshotResponse = Execute(DriverCommand.Screenshot, null);
            var base64 = screenshotResponse.Value.ToString();

            return new Screenshot(base64);
        }
    }
}
