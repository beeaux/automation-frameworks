package automation;

import cucumber.api.Scenario;
import cucumber.api.java.After;
import cucumber.api.java.Before;
import org.openqa.selenium.OutputType;
import org.openqa.selenium.TakesScreenshot;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebDriverException;
import org.openqa.selenium.remote.Augmenter;

import static automation.SharedDriver.SharedRemoteWebDriver;

public class ScenarioExtension {
    public static Scenario scenario;
    public static String capture_screenshots = System.getProperty("captureScreenshots");

    @Before
    public void before(Scenario _scenario) {
        this.scenario = _scenario;
    }

    @After
    public void embedScreenshot(Scenario _scenario) {
        WebDriver driver = new Augmenter().augment(SharedRemoteWebDriver);
        try {
            byte[] screenshot = ((TakesScreenshot) driver).getScreenshotAs(OutputType.BYTES);
            scenario.embed(screenshot, "image/png");
        } catch (WebDriverException platformDoesNotSupportScreenshots) {
            System.err.println(platformDoesNotSupportScreenshots.getMessage());
        }
    }

    /*

     */
    //@After
    public void setEmbedScreenshot(Scenario _scenario) {
        if (!capture_screenshots.equalsIgnoreCase("failedTests") && !capture_screenshots.equalsIgnoreCase("errors")) {
            if (capture_screenshots.equalsIgnoreCase("debug"))
                embedScreenshot(_scenario);
        } else {
           if (_scenario.isFailed())
               embedScreenshot(_scenario);
        }
    }
}
