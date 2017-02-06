Feature: BufferStatisticsFeature
	In order to see buffer statistics
	As a user
	I want to be told data from database for specific dates

@mytag
Scenario Outline: Show empty buffer statistics
	Given I have client and service started
	When I have entered <startDate> as a start date and <endDate> as a end date for no result
	Then the result should be <result>, there are no data

Examples:
| startDate  | endDate     | result  |
| 2001-01-01 | 2001-02-02  | 0       |

@mytag
Scenario Outline: Show buffer statistics
	Given I have client and service started
	When I have entered <startDate> as a start date and <endDate> as a end date for result
	Then the result should be <result> on the screen

Examples:
| startDate  | endDate     | result  |
| 2001-01-01 | 2020-02-02  | 1       |
