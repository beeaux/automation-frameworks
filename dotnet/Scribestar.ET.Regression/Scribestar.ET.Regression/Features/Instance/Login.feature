Feature: Login
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Add two numbers
	Given I log in with my account credentials
	| Email Address              | Password |
	| sanityrobot@sanitytest.com | test     |
	Then I be redirected to the 'ScribeStar Dashboard' page
	When I click on 'Transactions'
	Then I should see the Transactions dashboard
