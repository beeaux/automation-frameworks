package wgsn.examples.java.automation.core;

import org.openqa.selenium.Alert;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;
import wgsn.examples.java.automation.tests.SharedDriver;

import java.util.Collection;

/**
 * Created with IntelliJ IDEA.
 * User: Yomi Folami
 * Date: 05/03/13
 * Time: 18:36
 * To change this template use File | Settings | File Templates.
 */
public class WebDriverExtensions extends SharedDriver {
    private static RemoteWebDriver WEB_DRIVER = SharedDriver.WEBDRIVER;
    private static Actions action = new Actions(current());
    private static WebDriverWait wait = new WebDriverWait(current(), 10);

    public static WebDriver current() {
        return WEB_DRIVER;
    }

    /*
        Custom page element finders.
     */
    public static WebElement findElementByCssSelector(String css) {
        return current().findElement(By.cssSelector(css));
    }

    public static Collection<WebElement> findElementsByCssSelector(String css) {
        return current().findElements(By.cssSelector(css));
    }

    public static WebElement findElementByLinkText(String link) {
        return current().findElement(By.linkText(link));
    }

    /*
        Custom actions.
     */
    public static void clickOn(WebElement element) {
        element.click();
    }

    public static void typeText(WebElement element, String _string) {
         if(!element.getAttribute("value").isEmpty()) {
             element.clear();
         }
         element.sendKeys(_string);
    }

    public static void navigateTo(WebDriver driver, String url) {
        driver.navigate().to(url);
    }

    public static void mouseOver(WebElement element) {
        action.moveToElement(element).perform();
    }

    public static void select(WebElement element, String _string) {
        Select select = new Select(element);
        select.selectByVisibleText(_string);
    }

    public static void dragAndDrop(WebElement source, WebElement target) {
         action.dragAndDrop(source, target).perform();
    }

    public static void rightClick(WebElement element) {
        action.contextClick(element).perform();
    }

    /*
        waits for page to load and verifies page title.
     */
    public static void verifyPageTitle(String title) {
        if(!wait.until(ExpectedConditions.titleContains(title))) {
            System.err.println("Page title doesn't contain {" + title + "} but {"
                    + WEBDRIVER.getTitle() + "}");
        }
    }

    /*
        waits until element is visible on screen.
     */
    public static void waitForElement(WebElement element) {
        if(wait.until(ExpectedConditions.visibilityOf(element)) != null) return;
    }

    public static boolean isElementDisplayed(WebElement element) {
        return element.isDisplayed();
    }

    // switch driver to frame
    public static void switchToFrame(int frameId) {
        current().switchTo().frame(frameId);
    }

    /*
        alert and pop-up methods
     */
    public static Alert switchToAlert() {
        Alert alert = current().switchTo().alert();
        return alert;
    }

    public static void chooseOkOnNextConfirmation() {
        Alert alert = switchToAlert();
        alert.accept();
    }

    public static void chooseCancelOnNextConfirmation() {
        Alert alert = switchToAlert();
        alert.dismiss();
    }
}
