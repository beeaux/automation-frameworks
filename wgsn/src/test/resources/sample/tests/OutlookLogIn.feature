@SampleTests

Feature: Simple log in on the MS Hotmail page

  Scenario Outline: User is able to log into the outlook site
    Given I am on the <Mail site> homepage
    And I have a <Account> email account
    When I enter a <username> and <password>
    Then I should be <logged in>

    Examples:
    |Mail site|Account|username|password|State|
    |Hotmail  |valid  |adebakre@hotmail.com|simisade81|logged in|
    |Gmail    |valid  |adebakre@gmail.com  |mother07  |logged in|
