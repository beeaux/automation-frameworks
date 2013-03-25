require 'capybara'
require 'cucumber'
require 'webrat'
require 'rspec'
require 'capybara/poltergeist'
require 'selenium-webdriver'

include Capybara::DSL

# Capybara.current_driver = :selenium

@drivers_path = File.dirname(__FILE__) + '/drivers/'        # set global drivers path
@host_platform = Selenium::WebDriver::Platform              # get host operating system


#include a screenshot at the point of failure if scenario fails
After do |scenario|
  if(scenario.failed?)
    page.driver.browser.save_screenshot("#{scenario.__id__}.png")
    embed("#{scenario.__id__}.png", "image/png", "SCREENSHOT")
  end
end