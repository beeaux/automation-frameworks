using System.Configuration;
using Hybrid.Automation.Framework.Core.Utils;

namespace Hybrid.Automation.Framework.Core
{
    public static class AppConfiguration
    {
        public static readonly string Browser = ConfigurationManager.AppSettings["Browser"].ToLowerInvariant();
        public static readonly string ProjectDirectory = ConfigurationManager.AppSettings["ProjectDirectory"];
        public static readonly string SolutionName = ConfigurationManager.AppSettings["SolutionName"];
        public static readonly string SolutionDirectory = ProjectDirectory + Helpers._separator + SolutionName;
        public static readonly string BrowserDirectory = SolutionDirectory + Helpers._separator + ConfigurationManager.AppSettings["BrowserDirectory"];
        public static string TestResultsDirectory = SolutionDirectory + Helpers._separator + ConfigurationManager.AppSettings["TestResultsDirectory"];
        public static string ScreenCaptureDirectory = Helpers.GetTestResultsDirectory() + Helpers._separator + ConfigurationManager.AppSettings["ScreenCaptureDirectory"];
        public static readonly string TestDriver = ConfigurationManager.AppSettings["TestDriver"].ToLowerInvariant();
    }
}
