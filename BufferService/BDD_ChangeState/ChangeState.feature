Feature: ChangeState
	In order to make system behave different
	As a client
	I want to change system state

@mytag
Scenario Outline: Change state
	Given I have client and service started
	When I have entered <state>
	Then the result should be <result> on the screen

Examples:
| state | result |
| 0       | 0   |
| 1       | 1   |
