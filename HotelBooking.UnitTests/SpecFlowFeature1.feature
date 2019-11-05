Feature: CreateBooking
	I want to book a room if there is any free room between the chosen dates.
	Fully occpuied range is between 2019,12,9 and 2019,12,15
@SDinO
Scenario: Start date is in fully occupied range
	Given I have entered 2019,12,10 as start date
	And I have entered 2019,12,17 as end date
	When I press book
	Then the result should be false

@SDinB_and_EDinO
Scenario: Start date is before fully occupied range, end date is in a fully occupied range
	Given I have entered 2019,12,8 as start date
	And I have entered 2019,12,14 as end date
	When I press book
	Then the result should be false

@SDinA_and_EDinA
Scenario: Start and end date is before the fully occupied range
	Given I have entered 2019,12,1 as start date
	And I have entered 2019,12,8 as end date
	When I press book
	Then The booking should be approved

@SDinA_and_EDinB
Scenario: Start Date is before the fully occupied range End date is after
	Given I have entered 2019,12,7 as start date
	And I have entered 2019,12,16 as end date
	When I press book
	Then The result should be false

@SDinB_and_EDinA
Scenario: Start date is before fully occupied range, end date is after fully occupied range
	Given I have entered 2019,12,8 as start date
	And I have entered 2019,12,17 as end date
	When I press book
	Then the result should be false

@SdinB_and_EDinB
Scenario: Start and end date is after the fully occupied range
	Given I have entered 2019,12,16 as start date
	And I have entered 2019,12,22 as end date
	When I press book
	Then The booking should be approved