# SharedDriver

module SharedDriver
  # Capybara config
  Capybara.default_driver = :selenium
  Capybara.default_selector = :css
  Capybara.default_wait_time = 5
  Capybara.app_host = 'http://www.sit1.scribestar/'

=begin
  @Driver = Selenium::WebDriver.for :firefox
  @capabilities = Selenium::WebDriver::Remote::Capabilities
  @bridge = Selenium::WebDriver::Remote::Bridge

  @Driver.manage.window.maximize
=end

  Capybara.register_driver :set_driver_to_chrome do |driver|
    Capybara::Selenium::Driver.new(driver, :browser => :chrome)
  end

  Capybara.register_driver :set_driver_to_firefox do |driver|
    Capybara::Selenium::Driver.new(driver, :browser => :firefox)
  end

  Capybara.register_driver :set_driver_to_internet_explorer do |driver|
    Capybara::Selenium::Driver.new(driver, :browser => :internet_explorer)
  end

end
World(SharedDriver)