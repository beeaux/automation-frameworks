# START:module
require "selenium-webdriver"
#require ""

module SharedDriver
  @@driver = Selenium::WebDriver.for    :firefox
  @capabilities = Selenium::WebDriver::Remote::Capabilities
  @bridge = Selenium::WebDriver::Remote::Bridge
  #@defaultTimeout = Time.sec(10)

  @@driver.manage.window.maximize
  @@driver.manage.timeouts.implicit_wait=10

  at_exit { @@driver.close }
  # END:module

  # START:accessor
  def driver
    @@driver
  end
  # END:accessor

  def SetWebDriverToFirefox
    firefoxOpts = Bridge::Firefox::DEFAULT_ASSUME_UNTRUSTED_ISSUER.to_string
    @capabilities = Capabilities.firefox
    @capabilities.for(firefoxOpts, true)

    @@driver = new.Bridge::Firefox(@capabilities)
  end

  def SetWebDriverToChrome
    chromeDriverExeFile = Selenium::WebDriver::Remote::Chrome.driver_path=("chromeDriverExeFile")

    #@bridge = Chrome::Service::default_service(chromeDriverExeFile)
    #chromeOpts = Bridge::Chrome::

    begin
      @bridge.start
    rescue
      STDERR.puts("Error")
    end
  end

=begin
  [Before]
  def SetUp
    SetWebDriverToFirefox()

    @@driver.manage.window.maximize
    @@driver.manage.timeouts.implicit_wait=(10)
  end

  [After]
  def TearDown
    at_exit { @@driver.close }
  end
=end

end
# END:module

# START:world
World(SharedDriver)
# END:world