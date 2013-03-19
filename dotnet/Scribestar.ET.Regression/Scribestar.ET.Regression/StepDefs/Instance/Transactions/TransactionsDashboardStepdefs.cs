using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using Scribestar.ET.Regression.Core.Support;
using Scribestar.ET.Regression.Pages.Instance.Transactions;
using TechTalk.SpecFlow;

namespace Scribestar.ET.Regression.StepDefs.Instance.Transactions
{
    [Binding]
    public class TransactionsDashboardStepdefs
    {
        private readonly RemoteWebDriver Driver = SharedDriver.DriverInstance.WebDriver;
        private TransactionsDashboard dashboard = new TransactionsDashboard();


        /// <summary>
        ///     binding constructor
        /// </summary>
        public TransactionsDashboardStepdefs()
        {
            PageFactory.InitElements(Driver, dashboard);
        }


        /// <summary>
        ///     step def actions.
        /// </summary>
        [Then(@"I should see the Transactions dashboard")]
        public void ThenIShouldSeeTheTransactionsDashboard()
        {
            WebDriverExtensions.WaitForElement("#transactionListDiv");

        }

    }
}
