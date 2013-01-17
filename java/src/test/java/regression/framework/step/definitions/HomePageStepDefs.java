package regression.framework.step.definitions;

import cucumber.annotation.en.Given;
import org.openqa.selenium.support.PageFactory;
import regression.framework.page.objects.Homepage;
import regression.framework.SharedDriver;

public class HomePageStepDefs {
    private final SharedDriver driver;
    private Homepage page;

    public HomePageStepDefs(SharedDriver driver) {
        this.driver = driver;
        page = PageFactory.initElements(driver, Homepage.class);
    }

    @Given("^I (?:am currently|am) on the homepage$")
    public void iAmOrAmCurrentlyOnTheHomepage() {

    }
}
