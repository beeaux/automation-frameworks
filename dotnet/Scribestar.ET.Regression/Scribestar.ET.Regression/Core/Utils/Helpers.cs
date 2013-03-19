using System;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;
using Scribestar.ET.Regression.Core.Utils.Scenarios;

namespace Scribestar.ET.Regression.Core.Utils
{
    public static class Helpers
    {
        public static char _seperator = Path.DirectorySeparatorChar;
        public static DateTime _now = DateTime.Now;
        //public static string BrowserName = SharedDriver.DriverInstance.WebDriver.Capabilities.BrowserName;
        //public static string BrowserVersion = SharedDriver.DriverInstance.WebDriver.Capabilities.Version;

        public static string _date { get { return _now.ToString("ddMMyy"); } }

        public static string _time { get { return _now.ToString("HHmm"); } }

        public static void Debbuger(string exception)
        {
            Debug.WriteLine(exception);
        }

        public static void PrintScreen(string message)
        {
            Console.WriteLine(message);
        }

        public static void EmbedScreenshot(this IWebDriver driver)
        {
            driver = SharedDriver.DriverInstance.WebDriver;
            var capture = driver as ITakesScreenshot;
            if (capture == null) return;

            var screenshot = capture.GetScreenshot();
            var screenshotTitle = Path.Combine(GetScreenshotDirectory(), ScenarioInformation.ScenarioTitle + ".png");       // + " - " + BrowserName
            screenshot.SaveAsFile(screenshotTitle, ImageFormat.Png);
        }

        public static string GetTestResultsDirectory()
        {
            if (!Directory.Exists(ConfigurationSetting.TestResultsDirectory))
                Directory.CreateDirectory(ConfigurationSetting.TestResultsDirectory);

            var directory = Path.Combine(ConfigurationSetting.TestResultsDirectory, _date, _time);
            Directory.CreateDirectory(directory);
            return directory;
        }

        public static string GetScreenshotDirectory()
        {
            if (!Directory.Exists(ConfigurationSetting.ScreenshotDirectory))
                Directory.CreateDirectory(ConfigurationSetting.ScreenshotDirectory);
            return ConfigurationSetting.ScreenshotDirectory;
        }

        internal static void LaunchCommandPrompt(string _string)
        {
            var process = new Process();
            try
            {
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                var writer = process.StandardInput;
                writer.WriteLine(_string);
            }
            catch (Exception ex)
            {
                Debbuger(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
