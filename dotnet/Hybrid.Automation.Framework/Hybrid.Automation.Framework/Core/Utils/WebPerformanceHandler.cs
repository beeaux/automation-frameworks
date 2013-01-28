using HttpWatch;
using Hybrid.Automation.Framework.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.IO;

namespace Hybrid.Automation.Framework.Core.Utils
{
    public class WebPerformanceHandler
    {
        static Controller controller = new Controller();
        private static Plugin plugin;
        private static RemoteWebDriver driver = SharedDriver.DriverInstance.WebDriver;
        private static readonly string browser = AppConfiguration.Browser;

        public static void AttachWebPerfPluginToBrowser()
        {
            if (!browser.Equals("firefox") && !browser.Length.Equals(0)) return;
            var guId = Guid.NewGuid().ToString();
            var js = driver as IJavaScriptExecutor;
            js.ExecuteScript("document.title = '" + guId + "';");

            //if (driver.CurrentWindowHandle != null)

            plugin = controller.AttachByTitle(guId);
            plugin.Log.EnableFilter(false);
        }

        public static void RecordWebPerf()
        {
            if (!browser.Length.Equals(0) && !browser.Equals("firefox")) return;
            switch (plugin.IsRecording)
            {
                case true:
                    break;
                default:
                    plugin.Record();
                    break;
            }
        }

        private static void StopRecording() { plugin.Stop(); }

        public static void SavePerfLogReport()
        {
            var report = Path.Combine(Helpers.GetTestResultsDirectory(), ScenarioInformation.ScenarioTitle.Replace(' ', '_'));

            if (!plugin.IsRecording) return;
            plugin.Log.Save(report + ".hwl");
            ExportReportAsCSV(report + ".csv");

            StopRecording();
        }

        private static void ExportReportAsCSV(string report)
        {
            const string columns = "Page Title,URL,Time,Sent,Received,Method,Result,Type,Content Type, Round Trips";
            plugin.Log.ExportFieldsAsCSV(report, columns);
        }
    }
}
