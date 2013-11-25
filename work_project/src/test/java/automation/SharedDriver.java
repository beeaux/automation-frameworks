package automation;

import cucumber.api.Scenario;
import cucumber.api.java.After;
import cucumber.api.java.Before;
import org.openqa.selenium.Platform;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriverService;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.ie.InternetExplorerDriverService;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.remote.service.DriverService;
import org.openqa.selenium.safari.SafariOptions;
import org.openqa.selenium.support.events.AbstractWebDriverEventListener;
import org.openqa.selenium.support.events.EventFiringWebDriver;

import java.io.File;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.TimeUnit;

import static automation.utils.CMDExecutor.runScript;
import static java.lang.String.valueOf;
import static org.openqa.selenium.firefox.FirefoxDriver.*;
import static org.openqa.selenium.ie.InternetExplorerDriver.*;
import static org.openqa.selenium.remote.CapabilityType.*;

public class SharedDriver extends EventFiringWebDriver {
    public static RemoteWebDriver SharedRemoteWebDriver;
    private static DesiredCapabilities capabilities;
    private static DriverService driver_service;

    //configurable parameters
    public static String browser = System.getProperty("browser");
    private static String os_arch = System.getProperty("os.arch");
    public static Platform current_platform = Platform.getCurrent();
    private static String localhost = "http://localhost:5555/wd/hub";

    // directories & drivers
    private static String user_directory = System.getProperty("user.dir");
    private static String resources_directory = user_directory + File.separator +"src" + File.separator
            + "test" + File.separator + "resources";
    private static String drivers_directory = resources_directory + File.separator + "drivers";
    private static String chrome_driver = "chromedriver";
    private static String ie_driver = "IEDriverServer";
    private static String phantomjs_driver = "phantomjs";
    private static String standalone_server_jar = "java -jar " + drivers_directory + File.separator + "selenium_server_standalone.jar";

    /*
        app urls
        vita-1 | https://vita1-arena.lloydsbanking.com/arena/html
        localhost | https://localhost/ePortalStrategicMain.Web/arena/html

     */
    // constructor
    public SharedDriver() {
        super(SharedRemoteWebDriver);
        manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        manage().window().maximize();
    }

    private static final Thread CloseThread = new Thread() {
        @Override
        public void run() {
            SharedRemoteWebDriver.close();
        }
    };

    @Override
    public void close() {
        if(Thread.currentThread() != CloseThread)
            throw new UnsupportedOperationException("Warning - don't close WebDriver. It's shared and will close when JVM exits.");
        super.close();
    }

    @Before
    public void deleteAllCookies() {
        manage().deleteAllCookies();
    }

    @After
    public void registerWebDriverEventListener(final Scenario scenario) {
        register(new AbstractWebDriverEventListener() {
            @Override
            public void onException(Throwable throwable, WebDriver driver) {
                super.onException(throwable, driver);
            }
        });
    }

    public static String getCurrentOS() {
        String os = null;
        if (Platform.MAC.is(current_platform)) {
            os = "MAC";
        }  else if (Platform.LINUX.is(current_platform) || Platform.UNIX.is(current_platform)) {
            os = "LINUX";
        }  else if (Platform.WIN8.is(current_platform) || Platform.WINDOWS.is(current_platform) || Platform.XP.is(current_platform) || Platform.VISTA.is(current_platform)) {
            os = "WIN";
        }   else if (Platform.ANDROID.is(current_platform)) {
            os = "ANDROID";
        }
        return os;
    }

    /*

     */
     static {
          /*

           */
        if (browser.equalsIgnoreCase("firefox") || browser.equalsIgnoreCase("safari")) {
            String _string = standalone_server_jar + " -role hub";
            runScript(_string);

            _string = standalone_server_jar + " -role node -hub http://localhost:4444/grid/register";
            runScript(_string);
        }

        if (browser.equalsIgnoreCase("firefox")) {
            try {
                setSharedRemoteWebDriverToFirefox();
            } catch (MalformedURLException err) {
                err.printStackTrace();
            }
        } else if (browser.equalsIgnoreCase("ie")) {
            if (getCurrentOS().equalsIgnoreCase("win")) {
                if (os_arch.contains("64"))
                    ie_driver = ie_driver + "_x64" + File.separator + ie_driver + ".exe";
                else
                    ie_driver = ie_driver + "_Win32" + File.separator + ie_driver + ".exe";

                try {
                    setSharedRemoteWebDriverToIE();
                } catch (MalformedURLException err) {
                    err.printStackTrace();
                }
            } else {
                throw new IllegalStateException(browser + " is not supported on " + getCurrentOS());
            }
        } else if (browser.equalsIgnoreCase("chrome")){
            if(getCurrentOS().equalsIgnoreCase("MAC")) {
                chrome_driver = chrome_driver + "_mac" + File.separator + chrome_driver;
            } else if(getCurrentOS().equalsIgnoreCase("LINUX")) {
                if(os_arch.contains("64")) {
                    chrome_driver = chrome_driver + "_linux64" + File.separator + chrome_driver;
                } else {
                    chrome_driver = chrome_driver + "_linux32" + File.separator + chrome_driver;
                }
            } else {
                chrome_driver = chrome_driver + "_win" + File.separator + chrome_driver + ".exe";
            }

            try {
                setSharedRemoteWebDriverToChrome();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            }
        } else if (browser.equalsIgnoreCase("safari")) {
            try {
                setSharedRemoteWebDriverToSafari();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            }
        } else if (browser.isEmpty() || browser.equalsIgnoreCase("phantomjs")){
            if (getCurrentOS().equalsIgnoreCase("win")) {
                phantomjs_driver = phantomjs_driver + "_win" + File.separator + phantomjs_driver + ".exe";
            } else if(getCurrentOS().equalsIgnoreCase("mac")) {
                phantomjs_driver = phantomjs_driver + "_mac" + File.separator + phantomjs_driver;
            }  else {
                if (os_arch.contains("64"))
                    phantomjs_driver = phantomjs_driver + "_linux64" + File.separator + phantomjs_driver;
                else
                    phantomjs_driver = phantomjs_driver + "_linux32" + File.separator + phantomjs_driver;
            }

            try {
                setSharedRemoteWebDriverToPhantomJS();
            } catch (MalformedURLException err) {
                err.printStackTrace();
            }
        }

        //  shuts down remote web driver thread.
        Runtime.getRuntime().addShutdownHook(CloseThread);
    }

    private static void setSharedRemoteWebDriverToSafari() throws MalformedURLException {
        capabilities = DesiredCapabilities.safari();
        //capabilities.setCapability(SafariOptions.CAPABILITY, );
        setAdditionalCapabilities();

        SharedRemoteWebDriver = new RemoteWebDriver(new URL(localhost), capabilities);
    }

    private static void setSharedRemoteWebDriverToPhantomJS() throws MalformedURLException {
    }

    private static void setSharedRemoteWebDriverToChrome() throws MalformedURLException {
        driver_service = new ChromeDriverService.Builder()
                .usingDriverExecutable(new File(drivers_directory + File.separator + "chrome" + File.separator + chrome_driver))
                .usingAnyFreePort()
                .build();
        try {
            driver_service.start();
        } catch (Exception err) {
            throw new IllegalStateException(err.getMessage());
        }

        capabilities = DesiredCapabilities.chrome();
        setAdditionalCapabilities();

        SharedRemoteWebDriver = new RemoteWebDriver(driver_service.getUrl(), capabilities);
    }

    private static void setSharedRemoteWebDriverToIE() throws MalformedURLException {
        driver_service = new InternetExplorerDriverService.Builder()
                .usingDriverExecutable(new File(drivers_directory + File.separator + "ie" + File.separator + ie_driver))
                .usingAnyFreePort()
                .build();
        try {
            driver_service.start();
        } catch (Exception err) {
            throw new IllegalStateException(err.getMessage());
        }

        capabilities = DesiredCapabilities.internetExplorer();
        capabilities.setCapability(INTRODUCE_FLAKINESS_BY_IGNORING_SECURITY_DOMAINS, true);
        setAdditionalCapabilities();

        SharedRemoteWebDriver = new RemoteWebDriver(driver_service.getUrl(), capabilities);
    }

    private static void setSharedRemoteWebDriverToFirefox() throws MalformedURLException {
        capabilities = DesiredCapabilities.firefox();
        capabilities.setCapability(valueOf(DEFAULT_ENABLE_NATIVE_EVENTS), true);
        capabilities.setCapability(BINARY, System.getProperty("webdriver.firefox.bin"));
        setAdditionalCapabilities();

        SharedRemoteWebDriver = new RemoteWebDriver(new URL("http://localhost:5555/wd/hub/"), capabilities);
    }

    private static void setAdditionalCapabilities() {
        capabilities.setJavascriptEnabled(true);
        capabilities.setCapability(ACCEPT_SSL_CERTS, true);
        capabilities.setCapability(TAKES_SCREENSHOT, true);
    }
}
