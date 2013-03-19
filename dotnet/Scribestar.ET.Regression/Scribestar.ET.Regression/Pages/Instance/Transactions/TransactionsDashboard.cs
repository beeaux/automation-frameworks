using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using Scribestar.ET.Regression.Core.Support;
using System;
using System.Collections.ObjectModel;
using Scribestar.ET.Regression.Core.Utils;
using TechTalk.SpecFlow;

namespace Scribestar.ET.Regression.Pages.Instance.Transactions
{
    [Binding]
    public class TransactionsDashboard
    {
        /// <summary>
        ///     page object finders.
        /// </summary>
        [FindsBy(How = How.CssSelector, Using = "#transactionListDiv")]
        private IWebElement TransactionList;

        [FindsBy(How = How.CssSelector, Using = "#transactionList tr")]
        private ReadOnlyCollection<IWebElement> TransactionRows;

        [FindsBy(How = How.Id, Using = "userTransactionsContainer")]
        private IWebElement TransactionPreviewBoard;

        [FindsBy(How = How.CssSelector, Using = ".tab-icon-tx-active a")]
        [CacheLookup]
        private IWebElement ActiveTab;
        [FindsBy(How = How.CssSelector, Using = ".tab-icon-tx-all a")]
        [CacheLookup]
        private IWebElement AllTab;

        [FindsBy(How = How.LinkText, Using = "View")]
        [CacheLookup]
        private IWebElement ViewButton;
        [FindsBy(How = How.LinkText, Using = "Actions")]
        [CacheLookup]
        private IWebElement ActionsButton;


        /// <summary>
        ///     actions
        /// </summary>
        public string GetTransactionId()
        {
            var transaction = SelectTransaction();
            var id = TransactionRows[transaction].GetAttribute("id").Split('_');
            return id[2];
        }

        public int SelectTransaction()
        {
            WebDriverExtensions.WaitForElement("#transactionListDiv");
            if (!TransactionList.IsElementDisplayed()) return 0;

            var count = TransactionRows.Count;
            var rand = new Random();
            var row = rand.Next(0, count);

            return row;
        }

        public void ViewTransaction()
        {
            var id = GetTransactionId();
            var view = WebDriverExtensions.FindWebElementByCssSelector("#viewTransaction_" + id);

            view.ClickOn();
        }

        public void PreviewTransaction() {
            var _transaction = SelectTransaction();
            var transaction = TransactionRows[_transaction];

            transaction.ClickOn();

            if(!TransactionPreviewBoard.IsElementDisplayed()) return;

            var previewHeadingTitle = WebDriverExtensions.FindWebElementByCssSelector("#userTransactionsContainer .properties-header-small h2");
            previewHeadingTitle.ShouldContain("Transaction Properties");
    }
}
