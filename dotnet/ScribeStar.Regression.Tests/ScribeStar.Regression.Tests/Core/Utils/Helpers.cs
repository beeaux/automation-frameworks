using OpenQA.Selenium;
using ScribeStar.Regression.Tests.Core;
using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace ScribeStar.Regression.Tests.Core.Utils
{
    public static class Helpers
    {
        public static DateTime _now = DateTime.Now;
        public static string _date { get { return _now.ToString("ddMMyy"); } }
        public static string _time { get { return _now.ToString("HHmm"); } }
        public static char _separator = Path.DirectorySeparatorChar;
        public static string GetTestResultsDirectory()
        {
            if (!Directory.Exists(AppConfiguration.TestResultsDirectory))
                Directory.CreateDirectory(AppConfiguration.TestResultsDirectory);

            var resultsDirectory = Path.Combine(AppConfiguration.TestResultsDirectory, _date, _time);
            Directory.CreateDirectory(resultsDirectory);
            return resultsDirectory;
        }

        public static string GetScreenCaptureDirectory()
        {
            if (!Directory.Exists(AppConfiguration.ScreenCaptureDirectory))
                Directory.CreateDirectory(AppConfiguration.ScreenCaptureDirectory);

            return AppConfiguration.ScreenCaptureDirectory;
        }

        public static string FormatString(string _string, string _format)
        {
            string formattedString;

            switch (_format)
            {
                case "titleCase":
                    formattedString = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_string);
                    break;
                case "uppercase":
                    formattedString = CultureInfo.CurrentCulture.TextInfo.ToUpper(_string);
                    break;
                default:
                    formattedString = CultureInfo.CurrentCulture.TextInfo.ToLower(_string);
                    break;
            }

            return formattedString;
        }

        public static string ReportFileName()
        {
            var reportTitle = AppConfiguration.SolutionName.Replace('.', '_') + "_" + FormatString(ScenarioInformation.GetTestScenarioTags, "titleCase") + ".pdf";
            var reportFilePath = Path.Combine(GetTestResultsDirectory(), reportTitle);
            return reportFilePath;
        }

        public static void EmbedScreenCapture(this IWebDriver driver)
        {
            var captureScreenshot = driver as ITakesScreenshot;
            if (captureScreenshot == null) return;

            var screenshot = captureScreenshot.GetScreenshot();
            var screenCaptureFilename = Path.Combine(GetScreenCaptureDirectory(), FormatString(ScenarioInformation.ScenarioTitle, "titleCase").Replace(" ", "_") + ".png");
            screenshot.SaveAsFile(screenCaptureFilename, ImageFormat.Png);
        }
    }
}
