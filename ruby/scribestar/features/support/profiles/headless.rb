require File.dirname(__FILE__) + "/../env.rb"

# Register driver to use phantomjs (headless javascript-enabled driver)
Capybara.register_driver :poltergeist do |driver|
  Capybara::Poltergeist::Driver.new(driver, :browser => :phantomjs)
end