package automation.helpers;

import automation.tests.SharedDriver;
import org.openqa.selenium.Alert;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.Select;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.util.Collection;

/**
 * Created with IntelliJ IDEA.
 * User: Gabak Services
 * Date: 12/05/13
 * Time: 15:58
 * To change this template use File | Settings | File Templates.
 */
public class WebDriverHelpers extends SharedDriver{

    private static RemoteWebDriver Driver = SharedDriver.SharedRemoteWebDriver;
    private static Actions action = new Actions(current());
    private static Wait<WebDriver> wait;

    static {
        wait = new FluentWait<WebDriver>(current())
                .withTimeout(10, TimeUnit.SECONDS)
                .pollingEvery(1, TimeUnit.SECONDS)
                .ignoring(NoSuchElementException.class);
    }
    
    public static WebDriver current() {
        return Driver;
    }

    /*
        Custom page element finders.
     */
    public static WebElement findElementByCssSelector(String css) {
        try {
            WebElement element = current().findElement(By.cssSelector(css));
            if(!isElementDisplayed(element)) return null;
            return element;
        } catch (NoSuchElementException err) {
            throw new NoSuchElementException(err.getMessage());
        } catch (StaleElementReferenceException err) {
            throw new StaleElementReferenceException(err.getMessage());
        }
    }
    public static List<WebElement> findElementsByCssSelector(String css) {
        try {
            WebElement element = current().findElements(By.cssSelector(css));
            if(!isElementDisplayed(element)) return null;
            return element;
        } catch (NoSuchElementException err) {
            throw new NoSuchElementException(err.getMessage());
        } catch (StaleElementReferenceException err) {
            throw new StaleElementReferenceException(err.getMessage());
        }
    }

    public WebElement findElementByLinkText(String link) {
        try {
            WebElement element = current().findElements(By.linkText(link));
            if(!isElementDisplayed(element)) return null;
            return element;
        } catch (NoSuchElementException err) {
            throw new NoSuchElementException(err.getMessage());
        } catch (StaleElementReferenceException err) {
            throw new StaleElementReferenceException(err.getMessage());
        }
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

    public static void navigateTo(String url) {
        current().navigate().to(url);
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
    public static void waiter(WebElement _element) {
        final WebElement element = _element;
        wait.until(new Function<WebDriver, Object>() {
            @Override
            public Object apply(WebDriver _driver) {
                return element;
            }
        });
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
