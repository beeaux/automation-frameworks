require "selenium-webdriver"

module WebDriverCustomMethods

  # HTML Finders
  def CurrentPage
    return driver
  end

  def GetWebElementByCssSelector(selector)
    return driver.find_element(:css, selector)
  end

  def GetWebElementByLinkText(linkText)
    return driver.find_element(:link_text, linkText)
  end

  def GetWebElementById(identifier)
    return driver.find_element(:id, identifier)
  end

  def GetWebElementByXPath(locator)
    return driver.find_element(:xpath, locator)
  end

  def GetWebElementsByTagName(tagName)
    return driver.find_elements(tagName)
  end

  def GetWebElementsByCssSelector(selector)
    return driver.find_elements(selector)
  end



 # Action
 def NavigateTo(relativeUrl)
   driver.navigate.to(relativeUrl)
 end

  def TypeText(element, value)
    element.send_keys(value)
  end

  def ClickOn(element)
    element.click
  end

  def SelectAValue(element, value)
    select = Selenium::WebDriver::Support.new(element)
    select.select_by_text(value)
  end

  def MouseOver(element)
    actions = Selenium::WebDriver::ActionBuilder
    actions.move_to(element).perform
  end

  # Wait
  def WaitForElement(element)
    wait = Selenium::WebDriver::Wait.new(:timeout => 10)
    if(wait.until{element.displayed?} == nil)
      STDERR("No such element displayed on" + driver.url)
    end
  end



end
World(WebDriverCustomMethods)