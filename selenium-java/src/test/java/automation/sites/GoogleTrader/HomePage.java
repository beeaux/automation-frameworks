package automation.sites.GoogleTrader;

import automation.tests.SharedDriver;
import org.openqa.selenium.WebDriver;

import static automation.core.WebDriverExtensions.*;

public class HomePage {
    private final SharedDriver Driver;

    /*
        constructor
     */
    public HomePage(WebDriver driver) {
        navigateTo("http://www.google.com.ng/local/trader");
        this.Driver = (SharedDriver)driver;
    }
}
