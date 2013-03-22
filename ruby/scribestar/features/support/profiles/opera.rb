require File.dirname(__FILE__) + "/../env.rb"

# Register driver to use opera (see README.txt on more info on Opera usage and configuration)
Capybara.register_driver :selenium do |driver|
  Capybara::Selenium::Driver.new(driver, :browser => :opera)
end

