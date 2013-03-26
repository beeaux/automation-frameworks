require File.dirname(__FILE__) + '/../env.rb'

Capybara.current_driver = :poltergeist
Capybara.register_driver :poltergeist do |driver|
  phantomjs_driver_path = @drivers_path + 'phantomjs/phantomjs_'

  if @host_platform.linux?

  elsif @host_platform.mac?
    path_to_executable = phantomjs_driver_path + 'mac/phantomjs'
  else
    path_to_executable = phantomjs_driver_path + 'win/phantomjs.exe'
  end

  options = {
      browser: :phantomjs,
      phantomjs: path_to_executable
  }

  Capybara::Poltergeist::Driver.new(driver, options)
end