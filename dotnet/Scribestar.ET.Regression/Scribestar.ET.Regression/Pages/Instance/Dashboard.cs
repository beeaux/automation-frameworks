using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace Scribestar.ET.Regression.Pages.Instance
{
    [Binding]
    public class Dashboard
    {
        /// <summary>
        ///     Page Object Finders
        /// </summary>
        [FindsBy(How = How.Id, Using = "dashboardBodyContainer")]
        [CacheLookup]
        private IWebElement DashboardBodyContainer;

        [FindsBy(How = How.CssSelector, Using = ".dashboard-sidebar#side-bar")]
        [CacheLookup]
        private IWebElement Sidebar;

        [FindsBy(How = How.CssSelector, Using = "#DashboardMenuLink")]
        [CacheLookup]
        private IWebElement DashboardMenuLink;

        [FindsBy(How = How.CssSelector, Using = "#NewTransactionMenuLink")]
        [CacheLookup]
        private IWebElement NewTransactionMenuLink;

        [FindsBy(How = How.CssSelector, Using = "#SettingsMenuLink")]
        [CacheLookup]
        private IWebElement SettingsMenuLink;

        [FindsBy(How = How.CssSelector, Using = "#UsersMenuLink")]
        [CacheLookup]
        private IWebElement UsersMenuLink;

        [FindsBy(How = How.Id, Using = "OrganisationsMenuLink")]
        [CacheLookup]
        private IWebElement OrganisationMenuLink;

        [FindsBy(How = How.CssSelector, Using = ".logout-btn")]
        [CacheLookup]
        private IWebElement LogoutButton;

        [FindsBy(How = How.CssSelector, Using = "#sideBarOverview a")]
        [CacheLookup]
        private IWebElement SideBarOverviewLink;

        [FindsBy(How = How.CssSelector, Using = "#sideBarTransaction #transactionLink")]
        [CacheLookup]
        private IWebElement SideBarTransactionsLink;

        [FindsBy(How = How.CssSelector, Using = "#sideBarNotification #notificationLink")]
        [CacheLookup]
        private IWebElement SideBarNotificationsLink;
    }
}
