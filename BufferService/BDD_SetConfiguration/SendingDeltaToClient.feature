Feature: SendingDeltaToClient
	In order to send delta
	As a service
	I want to send delta in remote state after 10 values has come

@mytag
Scenario: Send delta to historical
	Given I have my client and service started and their historical should be started
	When I have prepared bufferModel.deltaCd
	Then the result should be empty deltaCd collections and not throwing exceptions except when historical is not working

