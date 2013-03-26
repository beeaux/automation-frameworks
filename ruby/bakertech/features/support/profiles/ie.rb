require File.dirname(__FILE__) + '/../env.rb'

Capybara.current_driver = :selenium
Capybara.register_driver :selenium do |driver|
  ie_driver_path = @drivers_path + 'ie/IEDriverServer_'

  if @host_platform.bitsize == 32
    Selenium::WebDriver::IE.driver_path = ie_driver_path + 'Win32/IEDriverServer.exe'
  else
    Selenium::WebDriver::IE.driver_path = ie_driver_path + 'x64/IEDriverServer.exe'
  end

  options = {
      browser: :internet_explorer
  }

  Capybara::Selenium::Driver.new(driver, options)
end