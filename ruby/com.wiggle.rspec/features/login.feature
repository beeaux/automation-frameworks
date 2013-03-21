Feature: Login
  In order to shop on wiggle.com
  As a customer
  I want to sign into my account

@login
Scenario: User signs in with invalid credentials
  Given I on the login page
  When I sign in with my login credentials
  #|Email|Password|
  #|yomi@yomifolami.com|123Password|
  Then I should see an error message "Sorry we could not log you in. Please try entering your account details again."


