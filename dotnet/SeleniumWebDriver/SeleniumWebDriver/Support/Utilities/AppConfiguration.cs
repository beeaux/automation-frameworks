using System.Configuration;

namespace SeleniumWebDriver.Support.Utilities
{
    public static class AppConfiguration
    {
        public static readonly string Browser = ConfigurationManager.AppSettings["Browser"].ToLowerInvariant();
        public static readonly string ProjectDirectory = ConfigurationManager.AppSettings["ProjectDirectory"];
        public static readonly string SolutionName = ConfigurationManager.AppSettings["SolutionName"];
        public static readonly string SolutionDirectory = ProjectDirectory + UtilityHelpers._separator + SolutionName;
        public static readonly string BrowserDirectory = SolutionDirectory + UtilityHelpers._separator + ConfigurationManager.AppSettings["BrowserDirectory"];
        public static string TestResultsDirectory = SolutionDirectory + UtilityHelpers._separator + ConfigurationManager.AppSettings["TestResultsDirectory"];
        public static string ScreenCaptureDirectory = UtilityHelpers.GetTestResultsDirectory() + UtilityHelpers._separator + ConfigurationManager.AppSettings["ScreenCaptureDirectory"];
    }
}
