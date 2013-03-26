Given(/^I am on (.*?) homepage$/) do  |search_engine|
  visit(search_engine + '.com')
end