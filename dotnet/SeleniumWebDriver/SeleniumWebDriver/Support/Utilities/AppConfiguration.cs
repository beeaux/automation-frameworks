using System.Configuration;

namespace SeleniumWebDriver.Support.Utilities
{
    public static class AppConfiguration
    {
        public static readonly string Browser = ConfigurationManager.AppSettings["Browser"].ToLowerInvariant();
        public static readonly string ProjectDirectory = ConfigurationManager.AppSettings["ProjectDirectory"];
        public static readonly string SolutionName = ConfigurationManager.AppSettings["SolutionName"];
        public static readonly string SolutionDirectory = ProjectDirectory + @"\" + SolutionName;
        public static readonly string BrowserDirectory = SolutionDirectory + @"\" + ConfigurationManager.AppSettings["BrowserDirectory"];
        public static string TestResultsDirectory = SolutionDirectory + @"\" + ConfigurationManager.AppSettings["TestResultsDirectory"];
        public static string ScreenCaptureDirectory = UtilityHelpers.GetTestResultsDirectory() + @"\" + ConfigurationManager.AppSettings["ScreenCaptureDirectory"];
    }
}
