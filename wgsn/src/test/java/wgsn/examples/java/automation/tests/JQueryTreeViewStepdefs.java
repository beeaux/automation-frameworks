package wgsn.examples.java.automation.tests;

import cucumber.annotation.en.*;
import org.junit.Assert;
import org.openqa.selenium.support.PageFactory;
import wgsn.examples.java.automation.pageobjects.JQueryTreeView;

import static wgsn.examples.java.automation.core.WebDriverExtensions.*;

/**
 * Created with IntelliJ IDEA.
 * User: Yomi Folami
 * Date: 05/03/13
 * Time: 20:58
 * To change this template use File | Settings | File Templates.
 */
public class JQueryTreeViewStepdefs {
    private final SharedDriver Driver;
    private JQueryTreeView page;

    public JQueryTreeViewStepdefs(SharedDriver driver) {
        this.Driver = driver;
        page = PageFactory.initElements(driver, JQueryTreeView.class);
    }

    @Given("^I am on jquery homepage$")
    public void I_am_on_jquery_homepage() {
    }

    @When("^I click \"([^\"]*)\"$")
    public void I_click(String linkText) {
        clickOn(findElementByLinkText(linkText));
    }

    @Then("^I should see \"([^\"]*)\"$")
    public void I_should_see(String linkText) {
        waitForElement(page.CollapsableLinkTree);
        if(!isElementDisplayed(page.CollapsableLinkTree)) {
            System.err.println("Element is not visible on {" + current().getCurrentUrl() + "}");
        } else {
            String _text = findElementByCssSelector("#navigation .collapsable .open.collapsable a").getText();
            Assert.assertEquals(linkText, _text);
        }
    }
}
