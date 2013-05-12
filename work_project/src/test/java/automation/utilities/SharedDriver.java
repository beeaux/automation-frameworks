package automation.utilities;

import cucumber.annotation.After;
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
import java.util.concurrent.TimeUnit;

/**
 * Created with IntelliJ IDEA.
 * User: Gabak Services
 * Date: 12/05/13
 * Time: 16:00
 * To change this template use File | Settings | File Templates.
 */
public class SharedDriver extends EventFiringWebDriver {

    protected static RemoteWebDriver WEBDRIVER;
    private static DesiredCapabilities capabilities;
    private static DriverService driver;

    public static String url = System.getProperty("url");   // gets user-defined url through Maven config.
    public static String browser = System.getProperty("browser");   // gets user-defined browser through Maven config.
    public static String bitOS = System.getProperty("os.arch");
    private static String directory = System.getProperty("user.dir");
    private static String drivers = directory
            + File.separator + "src" + File.separator + "test" + File.separator + "resources" + File.separator + "drivers";

    private static String platform = Platform.getCurrent().name();
    private static  String chromedriver = "chromedriver";
    private static String iedriverserver = "IEDriverServer";

    private static final Thread CLOSE_THREAD = new Thread() {
        @Override
        public void run() {
            WEBDRIVER.close();
        }
    };

    static {
        if(browser.equalsIgnoreCase("chrome")) {
            /*
                dynamically assigns chrome driver by identifying operating system
                and bit architecture.
             */
            if(platform.equalsIgnoreCase("mac")) {
                chromedriver = chromedriver + "_mac" + File.separator + chromedriver;
            } else if(platform.equalsIgnoreCase("linux") || platform.equalsIgnoreCase("unix")) {
                if(bitOS.contains("64")) {
                    chromedriver = chromedriver + "_linux64" + File.separator + chromedriver;
                } else {
                    chromedriver = chromedriver + "_linux32" + File.separator + chromedriver;
                }
            } else {
                chromedriver = chromedriver + "_win" + File.separator + chromedriver + ".exe";
            }

            setWebDriverToChrome();
        } else if(browser.equalsIgnoreCase("ie") || browser.equalsIgnoreCase("internetexplorer")) {
            /*
                dynamically assigns ie driver by identifying operating system
                and bit architecture.
                Prints out exception if operating system or platform is not Windows.
             */
            if(platform.contains("win") || platform.equalsIgnoreCase("xp") || platform.equalsIgnoreCase("vista")) {
                if(bitOS.contains("64")) {
                    iedriverserver = iedriverserver + "_x64" + File.separator + iedriverserver + ".exe";
                }   else {
                    iedriverserver = iedriverserver + "_Win32" + File.separator + iedriverserver + ".exe";
                }
            } else {
                System.err.println("Assigned browser {" + browser + "} is not supported on " + platform);
            }

            setWebDriverToIE();
        } else {
            // set as default browser if user-defined value does not match.
            setWebDriverToFirefox();
        }

        // shuts down web driver instance or thread.
        Runtime.getRuntime().addShutdownHook(CLOSE_THREAD);
    }

    // sets web driver instance to firefox
    private static void setWebDriverToFirefox() {
        capabilities = DesiredCapabilities.firefox();
        capabilities.setCapability(String.valueOf(FirefoxDriver.DEFAULT_ENABLE_NATIVE_EVENTS), true);

        //FirefoxProfile profile = new FirefoxProfile();
        WEBDRIVER = new FirefoxDriver(capabilities);
    }

    // sets web driver instance to ie
    private static void setWebDriverToIE() {
        String pathway = drivers + File.separator + "ie" + File.separator + iedriverserver;        // IE driver server directory.
        driver  = new InternetExplorerDriverService.Builder()
                .usingDriverExecutable(new File(pathway))
                .usingAnyFreePort()
                .build();

        try {
            driver.start();
        } catch (Exception err) {
            System.err.println(err.getMessage());
        }

        capabilities = DesiredCapabilities.internetExplorer();
        capabilities.setCapability(InternetExplorerDriver.INTRODUCE_FLAKINESS_BY_IGNORING_SECURITY_DOMAINS, true);
        WEBDRIVER = new RemoteWebDriver(driver.getUrl(), capabilities);
    }

    // sets web driver instance to chrome.
    private static void setWebDriverToChrome() {
        String pathway = drivers + File.separator + "chrome" + File.separator + chromedriver;     // Chrome driver server directory.
        driver = new ChromeDriverService.Builder()
                .usingDriverExecutable(new File(pathway))
                .usingAnyFreePort()
                .build();

        try {
            driver.start();
        } catch (Exception err) {
            System.err.println(err.getMessage());
        }

        capabilities = DesiredCapabilities.chrome();
        WEBDRIVER = new RemoteWebDriver(driver.getUrl(), capabilities);
    }

    // constructor
    public SharedDriver() {
        super(WEBDRIVER);
        manage().window().maximize();   // maximize browser window
        manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);   // sets implicit wait time to 10s
    }

    @Override
    public void close() {
        if(Thread.currentThread() != CLOSE_THREAD) {
            throw new UnsupportedOperationException("");
        }
    }

    /*
        deletes all cookies before execution
     */
    @Before
    public void deleteAllCookies() {
        manage().deleteAllCookies();
    }

    /*
        Embeds screenshot on exception or failure.
        current support issue with Chrome.
     */
    @After
    public void registerWebDriverEventListener(final ScenarioResult scenario) {
        register(new AbstractWebDriverEventListener() {
            @Override
            public void onException(Throwable throwable, WebDriver driver) {
                embedScreenshot(scenario);
            }
        });
    }

    /*
        Captures and embed screenshot as png file.
     */
    //@After
    public void embedScreenshot(ScenarioResult scenario) {
        try {
            byte[] screenshot = this.getScreenshotAs(OutputType.BYTES);
            scenario.embed(new ByteArrayInputStream(screenshot), "image/png");
        } catch (WebDriverException err) {
            System.err.println(err.getMessage());
        }
    }
}
