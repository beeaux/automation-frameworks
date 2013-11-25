@SampleTests

Feature: Simple log in on the MS Hotmail page

  Scenario Outline: User is able to log into the outlook site
    Given I am on the <Mail site> homepage
    And I have a <Account> email account
    When I enter a <username> and <password>
    Then I should be <logged in>

    Examples:
    |Mail site|Account|username|password|State|
    |Hotmail  |valid  |test@hotmail.com|password|logged in|
    |Gmail    |valid  |test@gmail.com  |password |logged in|
