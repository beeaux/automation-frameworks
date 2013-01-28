using System.Configuration;

namespace Hybrid.Automation.Framework.Core
{
    public static class EnvironmentConfiguration
    {
        public static string GetEnvironment()
        {
            string environment;
            var _environment = ConfigurationManager.AppSettings["AppHost"].ToLowerInvariant();

            switch (_environment)
            {
                case "integration":
                    environment = "";
                    break;
                default:
                    environment = "";
                    break;
            }
            return environment;
        }
    }
}
