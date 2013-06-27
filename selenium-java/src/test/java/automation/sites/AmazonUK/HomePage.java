package automation.sites.AmazonUK;


import automation.tests.SharedDriver;
import org.openqa.selenium.WebDriver;

import static automation.core.WebDriverExtensions.*;

public class HomePage {
  private final SharedDriver Driver;
  private static String url = System.getProperty("url");
  
  public HomePage(WebDriver driver) {
    /*
      note: url is configurable through system property configuration, as shown above.
    */
    navigateTo(url);
    //waitUntilPageTitle("Amazon.co.uk");
    this.Driver = (SharedDriver)driver;
  }
}
  
  
