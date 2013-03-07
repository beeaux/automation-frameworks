package wgsn.examples.java.automation.pageobjects;

import com.thoughtworks.selenium.Selenium;
import org.openqa.selenium.Alert;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import wgsn.examples.java.automation.tests.SharedDriver;

import java.util.List;
import java.util.Random;

import static wgsn.examples.java.automation.core.WebDriverExtensions.*;

/**
 * Created with IntelliJ IDEA.
 * User: Yomi Folami
 * Date: 05/03/13
 * Time: 20:55
 * To change this template use File | Settings | File Templates.
 */
public class PlugNEdit {
    private final SharedDriver Driver;

    @FindBy(css = "#inputa2") private WebElement TextEntry;
    @FindBy(id = "NoEdittool1") private WebElement EditToolbar;
    @FindBy(css = "#Upper2MovableDrawing span div") private List<WebElement> MoveableElements;
    @FindBy(id = "p1002image") public  WebElement MoveableElement1;
    @FindBy(id = "p1025Movable-drawingmoveDiv") public WebElement MoveableElement2;
    @FindBy(id = "RightMouseClickOptionNoEdit") private WebElement RightMouseClickMenu;

    private Alert alert;
    private Selenium selenium;

    public PlugNEdit(WebDriver driver) {
        driver.navigate().to(url);

        /*
            this code was added to fix the 'Confirm Navigation' alert when page is been reloaded with unsaved changes
            or data when running group of tests but didn't seem to fix the issue.
         */
        /*if(alert != null) {
            switchToAlert();
            selenium.getConfirmation();
            chooseOkOnNextConfirmation();
        }*/

        verifyPageTitle("Drag And Drop HTML Editor Online WYSIWYG Visual Editor, Free HTML Editor Online Web Page Builder, Web Based WYSIWYG Editor");
        this.Driver = (SharedDriver) driver;
    }

    public void enterText(String text) {
        typeText(TextEntry, text);
    }

    public boolean isEditToolbarVisible() {
        boolean visibility = true;
        if(!isElementDisplayed(EditToolbar)) {
            visibility = false;
        }
        return visibility;
    }

    /*
        selects a moveable element at random
     */
    public WebElement MoveableElement() {
        int counter = MoveableElements.size();
        Random random = new Random();
        int e = random.nextInt(counter);

        WebElement element = MoveableElements.get(e);
        return element;
    }

    public boolean isRightMouseClickMenuVisible() {
        boolean visibility = true;
        if(!isElementDisplayed(RightMouseClickMenu)) {
            visibility = false;
        }
        return visibility;
    }
}
