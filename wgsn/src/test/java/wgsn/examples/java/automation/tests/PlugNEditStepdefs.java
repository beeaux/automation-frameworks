package wgsn.examples.java.automation.tests;

import cucumber.annotation.en.Given;
import cucumber.annotation.en.Then;
import cucumber.annotation.en.When;
import junit.framework.Assert;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.PageFactory;
import wgsn.examples.java.automation.pageobjects.PlugNEdit;

import java.util.Collection;

import static wgsn.examples.java.automation.core.WebDriverExtensions.*;

/**
 * Created with IntelliJ IDEA.
 * User: Yomi Folami
 * Date: 05/03/13
 * Time: 20:57
 * To change this template use File | Settings | File Templates.
 */
public class PlugNEditStepdefs {
    private SharedDriver Driver;
    private PlugNEdit page;

    public PlugNEditStepdefs(SharedDriver driver) {
        this.Driver = driver;
        page = PageFactory.initElements(driver, PlugNEdit.class);
    }

    @Then("^the text tab is hidden$")
    public void the_text_tab_is_hidden() {
        boolean displayed = page.isEditToolbarVisible();
        Assert.assertEquals(false, displayed);
    }

    @When("^I click (?:on|on the) \"([^\"]*)\" Menu$")
    public void I_click_on_menu(String label) {
        // matcher to determine which menu to click based on user's input
        Collection<WebElement> menus = findElementsByCssSelector("#NoEditupper9WordPressshowhideit img");
        for(WebElement menu : menus) {
            if(menu.getAttribute("alt").contains(label)) {
                clickOn(menu);
            }
        }
    }

    @Then("^the text tab is displayed$")
    public void the_text_tab_is_displayed() {
        boolean displayed = page.isEditToolbarVisible();
        Assert.assertEquals(true, displayed);
    }

    @Given("^I am on the homepage$")
    public void I_am_on_the_homepage() {
        switchToFrame(0);
    }

    @When("^I enter \"([^\"]*)\"$")
    public void I_enter(String text) {
        page.enterText(text);
    }

    @Then("^I should see \"([^\"]*)\" displayed$")
    public void I_should_see_displayed(String text) {
        String _text = findElementByCssSelector("#inputa2").getAttribute("value");
        Assert.assertEquals(text, _text);
    }

    @Given("^I drag and drop an element$")
    public void I_drag_and_drop_an_element() throws InterruptedException {
        WebElement element = page.MoveableElement1;
        WebElement targetElement = page.MoveableElement2;

        dragAndDrop(element, targetElement);
    }

    @Given("^I right click$")
    public void I_right_click() {
        WebElement element = page.MoveableElement1;
        rightClick(element);
    }

    @Then("^I should see the right click menu$")
    public void I_should_see_the_right_click_menu() {
        boolean displayed = page.isRightMouseClickMenuVisible();
        Assert.assertEquals(true, displayed);
    }

    /*
        set to allow user to pass in visible text for the desired right click menu option rather than
        creating various click method for each menu option.
     */
    @Then("^I click on \"([^\"]*)\"$")
    public void I_click_on(String menu) throws InterruptedException {
        Collection<WebElement> elements = findElementsByCssSelector("#RightMouseClickOptionNoEdit div");

        for (WebElement element : elements) {
            if(element.getText().contains(menu)) {
                clickOn(element);
            }
        }
        Thread.sleep(2000);
    }

    @Then("^I should see image \"([^\"]*)\"$")
    public void I_should_see_image(String image) {
        boolean visible = isElementDisplayed(findElementByCssSelector("#"+image));
        Assert.assertEquals(true, visible);
    }
}
