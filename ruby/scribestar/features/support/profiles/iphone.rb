require  File.dirname(__FILE__) + '/../env.rb'

# Register driver to use iphone
Capybara.register_driver :selenium do |driver|
  options = {
    :browser => :iphone
  }
  Capybara::Selenium::Driver.new(driver, options)
end