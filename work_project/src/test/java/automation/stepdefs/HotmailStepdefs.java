package automation.stepdefs;

import automation.pages.HotmailPage;
import automation.SharedDriver;
import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import org.openqa.selenium.support.PageFactory;

/**
 * Created with IntelliJ IDEA.
 * User: Yomi Folami
 * Date: 13/05/13
 * Time: 00:11
 * To change this template use File | Settings | File Templates.
 */
public class HotmailStepdefs {
    private SharedDriver Driver;
    private HotmailPage hotmailPage;

    public HotmailStepdefs(SharedDriver driver) {
        this.Driver = driver;
        hotmailPage = PageFactory.initElements(driver, HotmailPage.class);
    }

    @Given("^I am on Hotmail$")
    public void I_am_on_Hotmail() throws Throwable {
    }

    @Then("^I should see hotmail$")
    public void I_should_see_hotmail() throws Throwable {
    }
}
