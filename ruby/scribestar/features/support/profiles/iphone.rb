require  File.dirname(__FILE__) + '/../env.rb'

# Register driver to use iphone
Capybara.register_driver :selenium do |driver|
  Capybara::Selenium::Driver.new(driver, :browser => :iphone)
end