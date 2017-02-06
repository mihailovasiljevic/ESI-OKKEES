Feature: SendingMeasurements
	In order to complete system
	As a client
	I want to send to server dumping values

@mytag
Scenario Outline: Send dumping values to server
	Given I have my client and service started
	When I have entered <code> and <value> 
	Then the result of <code> should be <lastValue>

Examples:
| code | value | lastValue |
| 0    | 2.0   | 2.0       |
| 1    | 22.0  | 22.0      |
| 0    | 2.1   | 2.1       |

Scenario Outline: Send dumping values to server bad deadband
	Given I have my client and service started
	When I have entered <code> and <value> 
	Then the result of <code> should be <lastValue>

Examples:
| code | value | lastValue |
| 1    | 22.9   | 22.0       |
| 0    | 2.2   | 2.2       |

Scenario Outline: Send dumping values to server good deadband
	Given I have my client and service started
	When I have entered <code> and <value> 
	Then the result of <code> should be <lastValue>

Examples:
| code | value | lastValue |
| 1    | 29.0   | 29.0      |
| 0    | 2.3   | 2.3       |