require File.dirname(__FILE__) + '/../env.rb'

# Register driver to use opera (see README.txt on more info on Opera usage and configuration)
Capybara.current_driver = :selenium

Capybara.register_driver :selenium do |driver|

  options = {
      browser: :opera
  }

  Capybara::Selenium::Driver.new(driver, options)
end

