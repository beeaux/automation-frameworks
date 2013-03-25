require File.dirname(__FILE__) + '/../env.rb'

Capybara.current_driver = :poltergeist

# Register driver to use phantomjs (headless javascript-enabled driver)
Capybara.register_driver :poltergeist do |driver|
  phantomjs_driver_path = @drivers_path + 'phantomjs/phantomjs_'

  if @host_platform.linux?
    if @host_platform.bitsize == 64
      Selenium::WebDriver::PhantomJS.path = phantomjs_driver_path + 'linux64/phantomjs'
    else
      Selenium::WebDriver::PhantomJS.path = phantomjs_driver_path + 'linux32/phantomjs'
    end
  elsif @host_platform.mac?
    Selenium::WebDriver::PhantomJS.path = phantomjs_driver_path + 'mac/phantomjs'
  else
    Selenium::WebDriver::PhantomJS.path = phantomjs_driver_path + 'win/phantomjs.exe'
  end

  options = {
      browser: :phantomjs #,
      #:inspector => true
  }
  Capybara::Poltergeist::Driver.new(driver, options)
end
