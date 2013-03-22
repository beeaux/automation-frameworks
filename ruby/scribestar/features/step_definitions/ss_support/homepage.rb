Given(/^I am on the (.*?) homepage$/)  do  |arg1|
  if arg1.eq("support")
    visit 'http://support.sit1.scribestar/'
  else
    visit 'http://www.sit1.scribestar/'
  end
end