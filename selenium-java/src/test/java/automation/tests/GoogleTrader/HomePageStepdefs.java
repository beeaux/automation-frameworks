package automation.tests.GoogleTrader;

import automation.sites.GoogleTrader.HomePage;
import automation.tests.SharedDriver;
import cucumber.api.java.en.Given;
import org.openqa.selenium.support.PageFactory;

public class HomePageStepdefs {
    private SharedDriver Driver;
    private HomePage homePage;

    public HomePageStepdefs(SharedDriver driver) {
        this.Driver = driver;
        homePage = PageFactory.initElements(driver, HomePage.class);
    }

    @Given("^I am on Google Homepage$")
    public void I_am_on_Google_Homepage() throws Throwable {
    }
}
