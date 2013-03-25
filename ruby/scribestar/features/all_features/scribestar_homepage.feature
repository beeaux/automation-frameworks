Feature: Go to ScribeStar homepage
  As a User
  I want to navigate to the homepage
  So that I can access my account

@support
Scenario: ScribeStar Admin goes to support homepage
  Given I am on the support homepage
  And I enter my admin login details
  #|username |password |
  #|admin    |test     |

@instance
Scenario: ScribeStar User goes to instance homepage
  Given I am on the instance homepage