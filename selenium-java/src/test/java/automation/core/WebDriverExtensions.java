package automation.core;

import automation.tests.SharedDriver;
import com.google.common.base.Function;
import org.openqa.selenium.*;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.FluentWait;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.Wait;

import java.util.Random;
import java.util.concurrent.TimeUnit;

public class WebDriverExtensions extends SharedDriver {
    private static WebDriver DRIVER = SharedDriver.WEB_DRIVER;
    private static Actions action = new Actions(current());
    private static Wait<WebDriver> wait;


    public static WebDriver current() {
        return DRIVER;
    }

    static {
        wait = new FluentWait<>(current())
                .withTimeout(3, TimeUnit.SECONDS)
                .pollingEvery(1, TimeUnit.SECONDS)
                .ignoring(NoSuchElementException.class);
    }

    /*
        custom element finders.
     */
    public  static WebElement findElementByCssSelector(String css) {
        return current().findElement(By.cssSelector(css));
    }

    public static WebElement findElementsByCssSelector(String css) {
        return current().findElement(By.cssSelector(css));
    }

    public static WebElement findElementByLinkText(String linkText) {
        return current().findElement(By.linkText(linkText));
    }

    /*
        custom wait
     */
    public static void waitForElement(WebElement element) {
        final WebElement _element = element;
        element = wait.until(new Function<WebDriver, WebElement>() {
            @Override
            public WebElement apply(WebDriver driver) {
                return _element;
            }
        });
    }

    public static boolean isElementDisplayed(WebElement element) {
        waitForElement(element);
        return element.isDisplayed();
    }

    public static boolean isElementEnabled(WebElement element) {
        waitForElement(element);
        return element.isEnabled();
    }

    public static boolean isElementSelected(WebElement element) {
        waitForElement(element);
        return element.isSelected();
    }

    /*
        custom actions
     */
    public static void clickOn(WebElement element) {
        if(!isElementDisplayed(element)) return;
        element.click();
    }

    public static void typeText(WebElement element, String value) {
        if (!isElementDisplayed(element)) return;
        if (!element.getAttribute("value").isEmpty()) {
            element.clear();
        }

        element.sendKeys(value);
    }

    public static void navigateTo(String url) {
        current().navigate().to(url);
    }

    public static void select(WebElement element, String string) {
        if (!isElementDisplayed(element)) return;

        Select select = new Select(element);
        select.selectByVisibleText(string);
    }

    public static void mouseOver(WebElement element) {
        action.moveToElement(element).perform();
    }

    public static void contextualRightClick(WebElement element) {
        action.contextClick(element).perform();
    }

    public static void releaseMouse(WebElement element) {
        action.release(element);
    }

    public static void pressKey(WebElement element, Keys key) {
        action.sendKeys(element, key).perform();
    }

    /*
        miscellaneous
     */
    public static void switchToActiveElement() {
        current().switchTo().activeElement();
    }

    public static Integer randomizer(int seed) {
        Random random = new Random();
        return random.nextInt(seed);
    }

    /*
        alerts, modal windows and popups.
     */
    public  static Alert switchToAlert() {
        if(wait.until(ExpectedConditions.alertIsPresent()) == null) {
            return null;
        }
        return current().switchTo().alert();
    }

    public static void chooseOkOnNextConfirmation() {
        switchToAlert().accept();
        switchToActiveElement();
    }

    public static void chooseCancelOnNextConfirmation() {
        switchToAlert().dismiss();
        switchToActiveElement();
    }
}
