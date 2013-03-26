require 'capybara'
require 'cucumber'
require 'capybara/poltergeist'
require 'selenium-webdriver'

include Capybara::DSL

@drivers_path = File.dirname(__FILE__) + '/drivers/'
@host_platform = Selenium::WebDriver::Platform


#include a screenshot at the point of failure if scenario fails
After do |scenario|
  if(scenario.failed?)
    page.driver.browser.save_screenshot("#{scenario.__id__}.png")
    embed("#{scenario.__id__}.png", "image/png", "SCREENSHOT")
  end
end