When(/^I enter my admin login details$/) do #|username, password|
  fill_in('UserName', :with => 'admin')
  fill_in('Password', :with => 'test')
  click_button('Log in')
end