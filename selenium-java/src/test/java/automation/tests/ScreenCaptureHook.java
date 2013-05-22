package automation.tests;


import cucumber.api.Scenario;
import org.openqa.selenium.OutputType;
import org.openqa.selenium.TakesScreenshot;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebDriverException;
import org.openqa.selenium.remote.Augmenter;

import static automation.tests.SharedDriver.WEB_DRIVER;

public class ScreenCaptureHook {

    public static void embedSnapshot(Scenario scenario) {
        WebDriver driver = new Augmenter().augment(WEB_DRIVER);
        try {
            byte[] snapshot = ((TakesScreenshot)driver).getScreenshotAs(OutputType.BYTES);
            scenario.embed(snapshot, "image/png");
        } catch (WebDriverException err) {
            System.err.println(err.getMessage());
        }
    }
}
