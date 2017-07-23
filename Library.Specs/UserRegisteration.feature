Feature: UserRegisteration
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario: Register new user
	Given I am on the registeration page
	When I enter my data
	And Proceed with user creation
	Then The user is created and redirected to the correct page