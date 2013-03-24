Given(/^I am on the (.*?) homepage$/)  do  |env|
  if env == 'support'
    visit('http://support.sit1.scribestar/')
  else
    visit('http://www.sit1.scribestar/')
  end
end