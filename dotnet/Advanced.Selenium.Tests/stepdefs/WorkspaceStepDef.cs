using Arena.EnhancedNews.Regression.Pages;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace Arena.EnhancedNews.Regression.Stepdefs
{
    [Binding]
    public class WorkspaceStepDef
    {
        private readonly ScreenShotRemoteWebDriver _driver = SharedDriver.DriverInstance.WebDriver;
        private readonly Workspace _workspace = new Workspace();

        public WorkspaceStepDef()
        {
            PageFactory.InitElements(_driver, _workspace);
        }

        [Given(@"I am on (.*) workspace")]
        public void GivenIAmOnWorkspace(string workspace)
        {
            LoginStepDef.LogIntoArena();
            _workspace.LaunchWorkspace(workspace);
        }
    }
}
