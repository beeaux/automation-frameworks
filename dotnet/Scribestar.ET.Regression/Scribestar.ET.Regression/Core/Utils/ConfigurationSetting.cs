using System.Configuration;

namespace Scribestar.ET.Regression.Core.Utils
{
    public class ConfigurationSetting
    {
        public static readonly string Browser = ConfigurationManager.AppSettings["Browser"].ToLowerInvariant();

        public static string ProjectDirectory = ConfigurationManager.AppSettings["Project"];

        public static string SolutionName = ConfigurationManager.AppSettings["SolutionName"];

        public static string SolutionDirectory = ProjectDirectory + Helpers._seperator + SolutionName +
                                                 Helpers._seperator + SolutionName;

        public static string TestResultsDirectory = SolutionDirectory + Helpers._seperator +
                                                    ConfigurationManager.AppSettings["Results"];

        public static string ScreenshotDirectory = Helpers.GetTestResultsDirectory() + Helpers._seperator +
                                                   ConfigurationManager.AppSettings["Screenshots"];

        public static string Browsers = ConfigurationSetting.SolutionDirectory + Helpers._seperator + ConfigurationManager.AppSettings["Browsers"] + Helpers._seperator;

        public static string HostEnvironment()
        {
            var environment = ConfigurationManager.AppSettings["HostUrl"].ToLowerInvariant();
            var url = ".scribestar/";

            switch (environment)
            {
                case "sit1":
                    url = "http://" + environment + url;
                    break;
                default:
                    url = "http://" + environment + url;
                    break;
            }
            return url;
        }
    }
}
