require File.dirname(__FILE__) + '/../env.rb'

Capybara.current_driver = :selenium
Capybara.register_driver :selenium do |driver|
  chrome_driver_path = @drivers_path + 'chrome/chromedriver_'

  if @host_platform.linux?

  elsif @host_platform.mac?
    Selenium::WebDriver::Chrome.driver_path = chrome_driver_path + 'mac/chromedriver'
  else
    Selenium::WebDriver::Chrome.driver_path = chrome_driver_path + 'win/chromedriver.exe'
  end

  options = {
      browser: :chrome
  }

  Capybara::Selenium::Driver.new(driver, options)
end