package automation.stepdefs;

import automation.helpers.WebDriverHelpers;
import automation.pageobjects.GoogleMailPage;
import automation.utilities.SharedDriver;
import cucumber.annotation.en.Given;
import org.openqa.selenium.support.PageFactory;

/**
 * Created with IntelliJ IDEA.
 * User: Gabak Services
 * Date: 12/05/13
 * Time: 19:29
 * To change this template use File | Settings | File Templates.
 */
public class globalStepDefs {

    private SharedDriver Driver;
    WebDriverHelpers webPageHelper = new WebDriverHelpers();
    private GoogleMailPage page;
    //private PlugNEdit page;

    // constructor
    public globalStepDefs(SharedDriver driver) {
        this.Driver = driver;
        page = PageFactory.initElements(driver, GoogleMailPage.class);   // binds driver instance with page object class.
    }



}
