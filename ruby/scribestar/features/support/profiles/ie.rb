require File.dirname(__FILE__) + "/../env.rb"

# Register driver to use ie (internet explorer)
Capybara.register_driver :selenium do |driver|
  Capybara::Selenium::Driver.new(driver, :browser => :internet_explorer)
end
