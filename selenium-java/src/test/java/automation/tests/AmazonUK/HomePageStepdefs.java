package automation.tests.AmazonUK;

import automation.sites.AmazonUK.HomePage;
import automation.tests.SharedDriver;
import cucumber.api.java.en.Given;
import org.openqa.selenium.support.PageFactory;

public class HomePageStepdefs {
  private SharedDriver Driver;
  private HomePage homepage;
  
  public HomePageStepdefs(SharedDriver driver) {
    this.Driver = driver;
    homepage = PageFactory.initElements(driver, HomePage.class);
  }
  
  @Given("^I am on Amazon UK$")
  public void am_on_Amazon_UK() throws Throwable {
  }
}
