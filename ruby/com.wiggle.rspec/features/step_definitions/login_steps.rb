# START:given
Given /^I on the login page$/ do
  NavigateTo('https://www.wiggle.co.uk/secure/myaccount/logon')

  loginInRadio = GetWebElementById('LogInOption')
  WaitForElement(loginInRadio)
  ClickOn(loginInRadio)
end
#END: given

# START:when
When /^I sign in with my login credentials$/ do  #|Email, Password|
  TypeText(GetWebElementByCssSelector('#LogOnModel_UserName'), "yomi@yomifolami.com")
  TypeText(GetWebElementByCssSelector('#LogOnModel_Password'), "Password")
  ClickOn(GetWebElementByCssSelector('.cta.primary'))
end
# END:when

# START:then
Then /^I should see an error message "(.*?)"$/ do |errMsg|
  validator = GetWebElementByCssSelector("#login div:first-child h4").text     #notification negative area shadow
  errMsg.should eq(validator)
end
# END:then