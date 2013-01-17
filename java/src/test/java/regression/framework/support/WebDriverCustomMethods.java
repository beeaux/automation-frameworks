package regression.framework.support;

import org.openqa.selenium.Alert;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import regression.framework.SharedDriver;

import java.util.Collection;
import java.util.List;

public class WebDriverCustomMethods {
    private final SharedDriver _SD;
    private static RemoteWebDriver driver;
    private static WebDriverWait wait = new WebDriverWait(currentPage(), 10);;

    public WebDriverCustomMethods(WebDriver driver) {
        _SD = (SharedDriver) driver;
    }

    public static WebDriver currentPage() {
        return driver;
    }

    /*
        Web Element Finders
     */
    public static WebElement getWebElementByCssSelector(String selector) {
        return driver.findElementByClassName(selector);
    }

    public static WebElement getWebElementById(String id) {
        return driver.findElementById(id);
    }

    public static WebElement getWebElementByLinkText(String linkText) {
        return driver.findElementByLinkText(linkText);
    }

    public static WebElement getWebElementByXPath(String xpath) {
        return driver.findElementByXPath(xpath);
    }

    public static List<WebElement> getWebElementsByCssSelector(String selector) {
        return driver.findElementsByClassName(selector);
    }

    public static List<WebElement> getWebElementsByTagName(String tagName) {
        return driver.findElementsByTagName(tagName);
    }

    /*
        Action Handlers
     */
    public static void navigateTo(String url) {
       driver.navigate().to(url);
    }

    public static void typeText(WebElement element, String value) {
        element.sendKeys(value);
    }

    public static void clickOn(WebElement element) {
        element.click();
    }

    public static void selectAValue(WebElement element, String value) {
        Select select = new Select(element);
        select.selectByVisibleText(value);
    }

    public static void mouseOver(WebElement element) {
        Actions action = new Actions(currentPage());
        action.moveToElement(element).perform();
    }

    /*
       Custom Wait Handlers
     */
    public static void verifyPageTitle(String title) {
        //wait = new WebDriverWait(currentPage(), 10);
        if(!wait.until(ExpectedConditions.titleIs(title))) {
            throw new IllegalStateException(("Expected page title to be {" + title + "} instead of {" + currentPage().getTitle() + "}"));
        }
    }

    public static void waitForElement(WebElement element) {
        //wait = new WebDriverWait(currentPage(), 10);
        if(wait.until(ExpectedConditions.visibilityOf(element)) != null) return;

        System.err.println(element + " is not visible or located on " + currentPage().getCurrentUrl());
    }

    public static boolean isElementDisplayed(WebElement element) {
        return element.isDisplayed();
    }



    /*
        Alerts & Pop ups
     */
    public static Alert switchToAlert(WebDriver driver) {
        Alert alert = driver.switchTo().alert();
        return alert;
    }

    public static void chooseOkOnNextConfirmation() {
        Alert alert = switchToAlert(currentPage());
        alert.accept();
    }

    public static void chooseCancelOnNextConfirmation() {
        Alert alert = switchToAlert(currentPage());
        alert.dismiss();
    }
}
