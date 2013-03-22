require 'capybara'
require 'cucumber'
require 'webrat'
require 'rspec'
require 'selenium-webdriver'
require 'capybara/poltergeist'

include Capybara::DSL

# Define Capybara configuration
Capybara.default_driver = :selenium
Capybara.default_selector = :css
Capybara.default_wait_time = 5

# SELENIUM_SERVER_JAR file required to run Opera and Selenium Grid
#export SELENIUM_SERVER_JAR = "/../drivers/selenium-server-standalone.jar"


After do |scenario|
  if(scenario.failed?)
    page.driver.browser.save_screenshot("#{scenario.__id__}.png")
    embed("#{scenario.__id__}.png", "image/png", "SCREENSHOT")
  end
end