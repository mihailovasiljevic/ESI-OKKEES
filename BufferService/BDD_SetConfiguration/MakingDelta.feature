Feature: MakingDelta
	In order send delta
	As a service
	I want to create delta

@mytag
Scenario: Add elements for addition and deletion
	Given I have service object and data received from client
	When I have added element for addition and element for update
	Then the collection should be updated
