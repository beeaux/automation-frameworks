@SampleTests

Feature: Simple log to email site page

  Scenario Outline: User is able to log into the outlook site
    Given I am on the <mail> homepage
    And I have a <Account> email account
    When I enter my <mail> <username> and <password>
    And I click on the sign in button
    Then I should be <State>

  Examples:
    |mail|Account|username|password|State|
    |Hotmail  |valid  |adebakre@hotmail.com|password|logged in|
    |Gmail    |valid  |adebakre@gmail.com  |password  |logged in|