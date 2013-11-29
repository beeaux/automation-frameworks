using Arena.EnhancedNews.Regression.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace Arena.EnhancedNews.Regression.Pages
{
    [Binding]
    public class Workspace
    {
        private static string _navIcon;

        [FindsBy(How = How.CssSelector, Using = ".workspace .panel .dockableHeader .title")] 
        private static readonly IWebElement _workspaceTitle;
 
        private static string SetNavIcon(string workspace)
        {
            switch (workspace)
            {
                case    "FX Price Ladder":
                    _navIcon = "fxLadder";
                    break;
                case "MM Price Ladder":
                    _navIcon = "mmLadder";
                    break;
                case "Economic Calendar":
                    _navIcon = "calendar";
                    break;
                case "Help & Settings":
                    _navIcon = "helpAndSettings";
                    break;
                case "Diagnostic Tools":
                    _navIcon = "settings";
                    break;
                default:
                    _navIcon = "content";
                    break;
            }
            return _navIcon;
        }

        public void LaunchWorkspace(string workspace)
        {
            _navIcon = ".workspaceManager .header .workspaceButtons .glyph.icon-" + SetNavIcon(workspace);
            var element = WebDriverExtension.FindWebElementByCssSelector(_navIcon);
            element.ClickOn();

            _workspaceTitle.ShouldContain(workspace);
        }
    }
}
