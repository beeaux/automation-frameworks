# START:module
module Screenshots
  if Cucumber::OS_X
    def embed_screenshot(id)
      'screencapture - t png #{id}.png'
      embed("#{id}.png", "image/png")
    end
  elsif Cucumber::WINDOWS
    require "selenium-webdriver"
    include Selenium::WebDriver::DriverExtensions::TakesScreenshot
    def embed_screenshot(id)
      save_screenshot("#{id}.png")
      embed("#{id}.png", "image/png")
    end
  else
    def embed_screenshot(id)
      STDERR.puts "Sorry - no screenshots on your platform yet."
    end
  end
end
# END:module
World(Screenshots)

After do
  embed_screenshot("screenshot_#{Time.new.to_i}")  if scenario.failed?
end
