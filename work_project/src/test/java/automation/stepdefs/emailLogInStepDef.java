package automation.stepdefs;

import automation.utils.WebDriverExtension;
import automation.pages.GoogleMailPage;
import automation.pages.HotmailPage;
import automation.SharedDriver;
import cucumber.api.java.en.And;
import cucumber.api.java.en.Given;
import cucumber.api.java.en.When;
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
    WebDriverExtension webPageHelper = new WebDriverExtension();
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

              //hotmailpage.open();
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
