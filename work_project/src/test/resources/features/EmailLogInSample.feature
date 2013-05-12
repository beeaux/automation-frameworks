@SampleTests

Feature: Simple log to email site page

  Scenario Outline: User is able to log into the outlook site
    Given I am on the <mail> homepage
    And I have a <Account> email account
    When I enter my <mail> <username> and <password>
    And I click on the sign in button
    Then I should be <logged in>

  Examples:
    |mail|Account|username|password|State|
    |Hotmail  |valid  |adebakre@hotmail.com|simisade81|logged in|
    |Gmail    |valid  |adebakre@gmail.com  |mother07  |logged in|