require File.dirname(__FILE__) + '/../env.rb'

# Register driver to use firefox
Capybara.current_driver = :selenium

Capybara.register_driver :selenium do |driver|
  options = {
      browser: :firefox
  }
  Capybara::Selenium::Driver.new(driver, options)
end
