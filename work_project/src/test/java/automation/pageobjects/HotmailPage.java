package automation.pageobjects;

import automation.helpers.WebDriverHelpers;
import automation.tests.SharedDriver;
import com.thoughtworks.selenium.Selenium;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;

import static automation.helpers.WebDriverHelpers.verifyPageTitle;

/**
 * Created with IntelliJ IDEA.
 * User: Gabak Services
 * Date: 12/05/13
 * Time: 16:03
 * To change this template use File | Settings | File Templates.
 */
public class HotmailPage {

    WebDriverHelpers webHelpers = new WebDriverHelpers();
    private Selenium selenium;
    private SharedDriver Driver;
    String url = "http://www.hotmail.com";
    private WebDriver driver;

    public void open() {
        driver.navigate().to(url);

        verifyPageTitle("Drag And Drop HTML Editor Online WYSIWYG Visual Editor, Free HTML Editor Online Web Page Builder, Web Based WYSIWYG Editor");
        this.Driver = (SharedDriver) driver;
    }

    @FindBy (css = "#idDiv_PWD_UsernameTb") private WebElement emailField;
    @FindBy (css = "#idDiv_PWD_PasswordTb") private WebElement passwordField;
    @FindBy (css = "#idSIButton9") private WebElement signInButton;

    //#################################Page object methods#####################################//

    public void enterEmailAddress(String email){

        webHelpers.typeText(emailField,email);
    }

    public void enterPassword(String password){

        webHelpers.typeText(passwordField,password);
    }

    public void clickSignIn(){

        webHelpers.clickOn(signInButton);
    }

    public void signIn(String email, String password){

        enterEmailAddress(email);
        enterPassword(password);
        clickSignIn();

    }
}
