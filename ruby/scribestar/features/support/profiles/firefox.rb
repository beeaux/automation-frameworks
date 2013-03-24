require File.dirname(__FILE__) + "/../env.rb"

# Register driver to use firefox
Capybara.register_driver :selenium do |driver|
  options = {
      :browser => :firefox
  }
  Capybara::Selenium::Driver.new(driver, options)
end
