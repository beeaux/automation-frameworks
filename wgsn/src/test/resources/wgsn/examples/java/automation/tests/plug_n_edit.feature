Feature: Plug N Edit
    As a tester
    I want to execute a series of tests
    In order to sign off the current release


Scenario: User can enter text into textarea
    Given I am on the homepage
    And I enter "Lorem ipsum dolor sit amet magna aliqua."
    Then I should see "Lorem ipsum dolor sit amet magna aliqua." displayed

Scenario: User can toggle (hide/show) text tab
    Given I am on the homepage
    When I click on "Hide" Menu
    Then the text tab is hidden
    When I click on "Text" Menu
    Then the text tab is displayed

@wgsn
Scenario: User can right click element on the page and select undo
    Given I am on the homepage
    And I drag and drop an element
    And I right click
    Then I should see the right click menu
    And I click on "Undo"

Scenario: User can view image displayed
    Given I am on the homepage
    Then I should see image "p1003imageShadowID"
