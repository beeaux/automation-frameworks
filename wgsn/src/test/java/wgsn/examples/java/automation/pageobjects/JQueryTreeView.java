package wgsn.examples.java.automation.pageobjects;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import wgsn.examples.java.automation.tests.SharedDriver;

import static wgsn.examples.java.automation.core.WebDriverExtensions.*;
import static wgsn.examples.java.automation.tests.SharedDriver.url;

/**
 * Created with IntelliJ IDEA.
 * User: Yomi Folami
 * Date: 05/03/13
 * Time: 20:55
 * To change this template use File | Settings | File Templates.
 */
public class JQueryTreeView {
    private final SharedDriver Driver;

    // page objects.
    @FindBy(css = "#navigation .collapsable") public WebElement CollapsableLinkTree;

    // constructor
    public JQueryTreeView(WebDriver driver) {
        driver.navigate().to(url);

        verifyPageTitle("jQuery treeview");
        this.Driver = (SharedDriver) driver;
    }
}
