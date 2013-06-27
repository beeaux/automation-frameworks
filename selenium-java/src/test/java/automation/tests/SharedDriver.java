package automation.tests;


import cucumber.api.Scenario;
import cucumber.api.java.After;
import cucumber.api.java.Before;
import org.openqa.selenium.Platform;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriverService;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.openqa.selenium.ie.InternetExplorerDriverService;
import org.openqa.selenium.phantomjs.PhantomJSDriverService;
import org.openqa.selenium.remote.CapabilityType;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.remote.service.DriverService;
import org.openqa.selenium.safari.SafariDriver;
import org.openqa.selenium.support.events.AbstractWebDriverEventListener;
import org.openqa.selenium.support.events.EventFiringWebDriver;
import sun.plugin2.util.BrowserType;

import java.io.File;
import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.TimeUnit;

import static automation.tests.ScreenCaptureHook.embedSnapshot;
import static automation.tests.CommandScriptExecutor.executeCommandScript;

public class SharedDriver extends EventFiringWebDriver {
    public static RemoteWebDriver WEB_DRIVER;
    private static DesiredCapabilities capabilities;
    private static DriverService service;

    private static String profile = System.getProperty("profile");
    private static String bit = System.getProperty("os.arch");

    private static String directory = System.getProperty("user.dir");
    private static String drivers = directory + File.separator + "src"
            + File.separator + "test" + File.separator + "resources" + File.separator + "drivers";
    private static String chrome_driver = "chromedriver";
    private static String ie_driver = "IEDriverServer";
    private static String headless_driver = "phantomjs";

    private static final Thread CLOSE_THREAD = new Thread() {
        @Override
        public void run() {
            WEB_DRIVER.close();
        }
    };

    public static String isPlatform() {
        String platform = null;
        Platform current = Platform.getCurrent();
        if(Platform.MAC.is(current)) {
            platform = "MAC";
        } else if(Platform.LINUX.is(current) || Platform.UNIX.is(current)) {
            platform = "LINUX";
        } else if(Platform.ANDROID.is(current)) {
            platform = "ANDROID";
        } else if(Platform.WIN8.is(current) || Platform.VISTA.is(current) || Platform.XP.is(current) || Platform.WINDOWS.is(current)) {
            platform = "WINDOWS";
        }
        return platform;
    }

    /*
        constructor
     */
    public SharedDriver() {
        super(WEB_DRIVER);
        
        if(!profile.equalsIgnoreCase("android") || !profile.startsWith("ip")) {
            manage().window().maximize();
        }
        manage().timeouts().implicitlyWait(5, TimeUnit.SECONDS);
    }

    static {
        /*
            launches selenium server standalone for firefox, iphone and ipad to ensure support for RemoteWebDriver instance
        */
        if(profile.equalsIgnoreCase("firefox") || profile.startsWith("ip") || profile.equalsIgnoreCase("android")) {
            String command = "java -jar src" + File.separator + "test"
                    + File.separator + "resources" + File.separator + "drivers" + File.separator + "selenium-server-standalone.jar";
            executeCommandScript(command);
        }
        
        if(profile.equalsIgnoreCase("chrome")){
            /*
                dynamically assigns chrome driver by identifying operating system
                and bit architecture.
             */
            if(isPlatform().equalsIgnoreCase("MAC")) {
                chrome_driver = chrome_driver + "_mac" + File.separator + chrome_driver;
            } else if(isPlatform().equalsIgnoreCase("LINUX")) {
                if(bit.contains("64")) {
                    chrome_driver = chrome_driver + "_linux64" + File.separator + chrome_driver;
                }  else {
                    chrome_driver = chrome_driver + "_linux32" + File.separator + chrome_driver;
                }
            } else if (isPlatform().equalsIgnoreCase("WINDOWS")) {
                chrome_driver = chrome_driver + "_win" + File.separator + chrome_driver + ".exe";
            }
            try {
                setWebDriverToChrome();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            }
        } else if (profile.equalsIgnoreCase("ie")) {
            /*
                dynamically assigns ie driver by identifying operating system
                and bit architecture.
                Prints out exception if operating system or platform is not Windows.
             */
            if(isPlatform().equalsIgnoreCase("WINDOWS")) {
                if(bit.contains("64")) {
                    ie_driver = ie_driver + "_x64" + File.separator + ie_driver + ".exe";
                } else {
                    ie_driver = ie_driver + "_Win32" + File.separator + ie_driver + ".exe";
                }
                
                try {
                    setWebDriverToIE();
                } catch (MalformedURLException e) {
                    e.printStackTrace();
                }
            } else {
                System.err.println(profile + "is not supported on " + isPlatform());
            }           
        }  else if(profile.equalsIgnoreCase("firefox")) {
            /*
                assigns firefox driver if user-defined value matches
             */
            try {
                setWebDriverToFirefox();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            }
        } else if(profile.equalsIgnoreCase("phantomjs") || profile.equalsIgnoreCase("headless")) {
            /*
                dynamically assigns phantomjs driver by identifying operating system
                and bit architecture.
             */
            if(isPlatform().equalsIgnoreCase("WINDOWS")) {
                headless_driver = headless_driver + "_win" + File.separator + headless_driver + ".exe";
            } else if (isPlatform().equalsIgnoreCase("MAC")) {
                headless_driver = headless_driver + "_mac" + File.separator + headless_driver;
            }  else if(isPlatform().equalsIgnoreCase("LINUX")) {
                if(bit.contains("64")) {
                    headless_driver = headless_driver + "_linux64" + File.separator + headless_driver;
                } else {
                    headless_driver = headless_driver + "_linux32" + File.separator + headless_driver;
                }
            }
            try {
                setWebDriverToHeadless();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            }
        }  else if(profile.equalsIgnoreCase("android")) {
            /*
                assigns android driver if user-defined value matches
             */
            try {
                setWebDriverToAndroid();
            } catch (MalformedURLException e) {
                e.printStackTrace();
            }
        } else if(profile.equalsIgnoreCase("safari")) {
            /*
                assigns safari driver if user-defined value matches
             */
             if(isPlatform().equalsIgnoreCase("WINDOWS") || isPlatform().equalsIgnoreCase("MAC")) {
                try {
                    setWebDriverToSafari();
                } catch (MalformedURLException e) {
                    e.printStackTrace();
                } 
            } else {
                System.err.println(profile + "is not supported on " + isPlatform());
            } 
        }

        Runtime.getRuntime().addShutdownHook(CLOSE_THREAD);
    }

    private static void setWebDriverToAndroid() throws MalformedURLException {
        capabilities = DesiredCapabilities.android();
        setAdditionalCapabilities();

        WEB_DRIVER = new RemoteWebDriver(new URL("http://localhost:8080/wd/hub"), capabilities);

    }
    
     private static void setWebDriverToSafari() throws MalformedURLException {
        capabilities = DesiredCapabilities.safari();
        /*
            important system settings to ensure safari runs smoothly.
            note: screenshot is captured using SafariDriver instead of RemoteWebDriver.
         */
        capabilities.setCapability(SafariDriver.CLEAN_SESSION_CAPABILITY, true);
        capabilities.setCapability(SafariDriver.DATA_DIR_CAPABILITY, System.getProperty("webdriver.safari.driver"));
        capabilities.setCapability(SafariDriver.NO_INSTALL_EXTENSION_CAPABILITY, false);
        setAdditionalCapabilities();

        WEB_DRIVER = new SafariDriver(capabilities);
    }

    private static void setWebDriverToHeadless() throws MalformedURLException {
        String headless_driver_path = drivers + File.separator + "phantomjs" + File.separator + headless_driver;
        service = new PhantomJSDriverService.Builder()
                .usingPhantomJSExecutable(new File(headless_driver_path))
                .usingAnyFreePort()
                .build();

        try {
            service.start();
        } catch (IOException e) {
            e.printStackTrace();
        }

        capabilities = DesiredCapabilities.phantomjs();
        capabilities.setCapability(CapabilityType.TAKES_SCREENSHOT, true);
        setAdditionalCapabilities();

        if(isPlatform().equalsIgnoreCase("WINDOWS")) {
            capabilities.setCapability(CapabilityType.BROWSER_NAME, BrowserType.INTERNET_EXPLORER);
            capabilities.setCapability(CapabilityType.VERSION, "8");
        }

        WEB_DRIVER = new RemoteWebDriver(service.getUrl(), capabilities);
    }

    private static void setWebDriverToFirefox() throws MalformedURLException {
        capabilities = DesiredCapabilities.firefox();
        capabilities.setCapability(FirefoxDriver.BINARY, System.getProperty("webdriver.firefox.bin"));
        capabilities.setCapability(String.valueOf(FirefoxDriver.DEFAULT_ENABLE_NATIVE_EVENTS), true);
        setAdditionalCapabilities();

        WEB_DRIVER = new RemoteWebDriver(new URL("http://localhost:4444/wd/hub"), capabilities);
    }

    private static void setWebDriverToIE() throws MalformedURLException {
        String ie_driver_path = drivers + File.separator + "ie" + File.separator + ie_driver;
        service = new InternetExplorerDriverService.Builder()
                .usingDriverExecutable(new File(ie_driver_path))
                .usingAnyFreePort()
                .build();

        try {
            service.start();
        } catch (IOException e) {
            e.printStackTrace();
        }

        capabilities = DesiredCapabilities.internetExplorer();
        capabilities.setCapability(InternetExplorerDriver.INTRODUCE_FLAKINESS_BY_IGNORING_SECURITY_DOMAINS, true);
        capabilities.setCapability(InternetExplorerDriver.REQUIRE_WINDOW_FOCUS, true);
        setAdditionalCapabilities();
        
        WEB_DRIVER = new RemoteWebDriver(service.getUrl(), capabilities);
    }

    private static void setWebDriverToChrome() throws MalformedURLException {
        String chrome_driver_path = drivers + File.separator + "chrome" + File.separator + chrome_driver;
        service = new ChromeDriverService.Builder()
                .usingDriverExecutable(new File(chrome_driver_path))
                .usingAnyFreePort()
                .build();

        try {
            service.start();
        } catch (IOException e) {
            e.printStackTrace();
        }

        capabilities = DesiredCapabilities.chrome();
        setAdditionalCapabilities();

        WEB_DRIVER = new RemoteWebDriver(service.getUrl(), capabilities);
    }
    
    private static void setAdditionalCapabilities() {
        capabilities.setJavascriptEnabled(true);
        capabilities.setCapability(CapabilityType.SUPPORTS_ALERTS, true);
        capabilities.setCapability(CapabilityType.ACCEPT_SSL_CERTS, true);
        capabilities.setCapability(CapabilityType.UNEXPECTED_ALERT_BEHAVIOUR, "ignore");
    }
    
    @Override
    public void close() {
        if(Thread.currentThread() != CLOSE_THREAD) {
            throw new UnsupportedOperationException("You shouldn't close this WebDriver. It's shared and will close when the JVM exits.");
        }
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
                embedSnapshot(scenario);
            }
        });
    }
}
