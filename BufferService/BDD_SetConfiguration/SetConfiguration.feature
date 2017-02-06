Feature: SetSystemConfigurationFeature
	In order to make system working
	As a client
	I want to send system configuration parameters

@mytag
Scenario Outline: Set deadbend, pmin and pmax
	Given I have client and service started
	When I have entered <deadband> as a deadband, <pmin> as a pmin, <pmax> as a pmax
	Then the result should be <result> on the screen

Examples:
| deadband | pmin | pmax   | result |
| 10       | 2.0    | 15.0     | 27.0     |
| 5        | 100.0  | 120.0    | 225.0    |
| 12       | 22.9 | 110.12 | 145.02 |