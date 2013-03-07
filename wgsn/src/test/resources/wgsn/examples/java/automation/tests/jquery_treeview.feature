Feature: JQuery Treeview
    As a user
    I want to navigate to a directory tree
    In order to view the content of the directory

Scenario: User can navigate to a directory tree
    Given I am on jquery homepage
    When I click "Item 3"
    Then I should see "Item 3.0"