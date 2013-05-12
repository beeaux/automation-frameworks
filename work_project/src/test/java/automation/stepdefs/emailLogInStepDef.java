package automation.stepdefs;

import automation.helpers.WebDriverHelpers;
import automation.pageobjects.GoogleMailPage;
import automation.pageobjects.HotmailPage;
import automation.tests.SharedDriver;
import cucumber.annotation.en.And;
import cucumber.annotation.en.Given;
import cucumber.annotation.en.When;
import org.openqa.selenium.support.PageFactory;

/**
 * Created with IntelliJ IDEA.
 * User: Gabak Services
 * Date: 12/05/13
 * Time: 19:52
 * To change this template use File | Settings | File Templates.
 */
public class emailLogInStepDef {

    private SharedDriver Driver;
    WebDriverHelpers webPageHelper = new WebDriverHelpers();
    private GoogleMailPage googlepage;
    private HotmailPage hotmailpage;


    // constructor
    public emailLogInStepDef(SharedDriver driver) {
        this.Driver = driver;
        googlepage = PageFactory.initElements(driver, GoogleMailPage.class);
        hotmailpage = PageFactory.initElements(driver, HotmailPage.class);
    }


    @Given("^I am on the (.*) homepage$")
    public void i_am_on_page(String url){

        if(url.equalsIgnoreCase("Gmail")){

            googlepage.open();
        }
        else if(url.equalsIgnoreCase("hotmail")){

              hotmailpage.open();
        }


    }

    @And("^I have a (.*) email account$")
    public void verify_user_email_account(){

        //No steps required
    }

    @When("^I enter my (.*) (.*) and (.*)$")
    public void i_enter_details(String mail, String username, String password){

       if(mail.equalsIgnoreCase("hotmail")){
           hotmailpage.signIn(username, password);
       }
        else if(mail.equalsIgnoreCase("gmail")){
           googlepage.signIn(username, password);
       }
    }
}
