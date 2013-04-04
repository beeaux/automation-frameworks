require File.dirname(__FILE__) + '/../env.rb'

# Register driver to use ie (internet explorer)
Capybara.current_driver = :selenium

Capybara.register_driver :selenium do |driver|
  ie_driver_path = @drivers_path + 'ie/IEDriverServer_'
  
  if @host_platform.windows?
    if @host_platform.bitsize == 32
      Selenium::WebDriver::IE.path = ie_driver_path + 'Win32/IEDriverServer.exe'
    else
      Selenium::WebDriver::IE.path = ie_driver_path + 'x64/IEDriverServer.exe'
    end
  else
    STDERR 'Sorry!!!'
  end

  options = {
      browser: :remote
      desired_capabilities: :internet_explorer
  }
  Capybara::Driver::Selenium.new(driver, options)
end
