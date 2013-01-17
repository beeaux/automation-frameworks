package regression.framework.page.objects;

import org.openqa.selenium.WebDriver;
import regression.framework.SharedDriver;
import regression.framework.support.WebDriverCustomMethods;
import static regression.framework.SharedDriver.*;

public class Homepage {
    private final SharedDriver driver;

    public Homepage(WebDriver driver) {
        WebDriverCustomMethods.navigateTo(url);
        WebDriverCustomMethods.verifyPageTitle("");

        this.driver = (SharedDriver)driver;
    }
}
