using System.Configuration;
using System.IO;

namespace Arena.EnhancedNews.Regression.Core
{
    public static class AppConfiguration
    {
        public static readonly string ProjectPath = ConfigurationManager.AppSettings["ProjectPath"];
        public static readonly string Solution = ConfigurationManager.AppSettings["Solution"];
        public static readonly string SolutionPath = ProjectPath + Path.DirectorySeparatorChar + Solution + Path.DirectorySeparatorChar + Solution;

        public static string TestResultsPath = SolutionPath + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar
            + ConfigurationManager.AppSettings["Results"];
        public static string ScreenshotPath = TestResultsPath + Path.DirectorySeparatorChar + ConfigurationManager.AppSettings["Screenshots"];
        public static string DriversPath = SolutionPath + Path.DirectorySeparatorChar + "Resources" + Path.DirectorySeparatorChar
            + ConfigurationManager.AppSettings["DriversPath"] + Path.DirectorySeparatorChar;

        public static string HostEnvironment()
        {
            var environment = ConfigurationManager.AppSettings["Environment"];
            var url = "arena/html";

            switch (environment)
            {
                case "vita1":
                    url = "https://vita1-arena.lloydsbanking.com/" + url;
                    break;
                default:
                    url = "https://localhost/ePortalStrategicMain.Web/" + url;
                    break;
            }

            return url;
        }
    }
}
