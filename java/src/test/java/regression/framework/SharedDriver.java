package regression.framework;

import cucumber.annotation.Before;
import cucumber.runtime.ScenarioResult;
import org.openqa.selenium.OutputType;
import org.openqa.selenium.Platform;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebDriverException;
import org.openqa.selenium.chrome.ChromeDriverService;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.ie.InternetExplorerDriverService;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.remote.service.DriverService;
import org.openqa.selenium.support.events.AbstractWebDriverEventListener;
import org.openqa.selenium.support.events.EventFiringWebDriver;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.util.concurrent.TimeUnit;

public class SharedDriver extends EventFiringWebDriver {
    private static RemoteWebDriver WEBDRIVER;
    private static DesiredCapabilities _capabilities;
    private static DriverService _service;

    public static String url = System.getProperty("url");
    public static String browser = System.getProperty("browser");
    public static String osArch = System.getProperty("os.arch");
    public static String platform = Platform.getCurrent().name();

    private static String directory = System.getProperty("user.dir");
    private static String resource_path = directory + File.separator + "src" + File.separator + "test" + File.separator + "resources";
    private static String driver_path = resource_path + File.separator + "driver";
    private static String chrome_driver = "chrome_driver";
    private static String ie_driver = "ie_driver";

    static {
        if(browser.equalsIgnoreCase("chrome")) {
            if(platform.equalsIgnoreCase("mac")) {
                chrome_driver = chrome_driver + "_mac";
            } else if(platform.equalsIgnoreCase("linux") || platform.equalsIgnoreCase("unix")) {
                chrome_driver = chrome_driver + "_linux";
            } else {
                chrome_driver = chrome_driver + "_win.exe";
            }
            setWebDriverToChrome();
        } else if(browser.equalsIgnoreCase("ie") || browser.equalsIgnoreCase("internet explorer")) {
            if(!platform.startsWith("m") && !platform.startsWith("u") && !platform.startsWith("l")) {
                if(osArch.contains("64")) {
                    ie_driver = ie_driver + "_x64.exe";
                } else {
                    ie_driver = ie_driver + "_win32.exe";
                }
                setWebDriverToIE();
            } else {
                System.err.println("Selected browser {" + browser + "} is not supported on " + platform);
            }
        } else {
             setWebDriverToFirefox();
        }

        Runtime.getRuntime().addShutdownHook(new Thread() {
            @Override
            public void run() {
                WEBDRIVER.close();
            }
        });
    }

    public static void setWebDriverToFirefox() {
        _capabilities = DesiredCapabilities.firefox();
        _capabilities.setCapability(String.valueOf(FirefoxDriver.DEFAULT_ENABLE_NATIVE_EVENTS), true);

        WEBDRIVER = new FirefoxDriver(_capabilities);
    }

    public static void setWebDriverToIE() {
        driver_path = driver_path + File.separator + "ie" + File.separator + ie_driver;
        _service = new InternetExplorerDriverService.Builder()
                .usingDriverExecutable(new File(driver_path))
                .usingAnyFreePort()
                .build();

        try {
            _service.start();
        }  catch (IOException err)  {
            throw new IllegalStateException(err.getMessage());
        }

        _capabilities = DesiredCapabilities.internetExplorer();
        _capabilities.setCapability(InternetExplorerDriver.INTRODUCE_FLAKINESS_BY_IGNORING_SECURITY_DOMAINS, true);

        WEBDRIVER = new RemoteWebDriver(_service.getUrl(), _capabilities);
    }

    private static void setWebDriverToChrome() {
        driver_path = driver_path + File.separator + "chrome" + File.separator + chrome_driver;
        _service = new ChromeDriverService.Builder()
                .usingDriverExecutable(new File(driver_path))
                .usingAnyFreePort()
                .build();

        try {
            _service.start();
        }  catch (IOException err)  {
            throw new IllegalStateException(err.getMessage());
        }

        _capabilities = DesiredCapabilities.chrome();

        WEBDRIVER = new RemoteWebDriver(_service.getUrl(), _capabilities);
    }

    public SharedDriver() {
        super(WEBDRIVER);
        manage().window().maximize();
        manage().timeouts().implicitlyWait(20, TimeUnit.SECONDS);
    }

    @Before
    public void deleteAllCookies() {
        manage().deleteAllCookies();
    }

    @Before
    public void registerWebDriverEventListener(final ScenarioResult result) {
        register(new AbstractWebDriverEventListener() {
            @Override
            public void onException(Throwable throwable, WebDriver driver) {
                embedScreenshot(result);
            }
        });
    }

    public void embedScreenshot(ScenarioResult result) {
        try {
            byte [] screenshot = this.getScreenshotAs(OutputType.BYTES);
            result.embed(new ByteArrayInputStream(screenshot), "image/png");
        } catch (WebDriverException screenshotsNotSupported) {
            throw new IllegalStateException(screenshotsNotSupported.getMessage());
        }
    }
}
