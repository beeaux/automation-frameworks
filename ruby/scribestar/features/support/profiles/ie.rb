require File.dirname(__FILE__) + "/../env.rb"

# Register driver to use ie (internet explorer)
Capybara.register_driver :selenium do |driver|
  ie_driver_path = @drivers_path + 'ie/IEDriverServer_'
  
  if @host_platform.windows?
    if @host_platform.bitsize == 64
      Selenium::WebDriver::Firefox.driver_path = ie_driver_path + 'x64/IEDriverServer.exe'
    else
      Selenium::WebDriver::Firefox.driver_path = ie_driver_path + 'Win32/IEDriverServer.exe'
    end
  else
    STDERR ''
  end

  options = {
      :browser => :remote,
      :url => "http://192.168.1.127:4444/wd/hub",
      :desired_capabilities => :internet_explorer
  }
  Capybara::Driver::Selenium.new(driver, options)
end
