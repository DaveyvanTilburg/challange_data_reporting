### Introduction

This project is created in response to a challange where you have to solve a problem in a reasonably short time.

Translated into my own words, the challange includes the following:
 - From a data source ([data](Infrastructure/MockedData/sales.csv) supplied in csv) create an overview per month that shows the performence of a product per month.
 - Vertical axis is 'value' and horizontal axis is time
 
 Self introducted goal: I don't really understand what "performence" means, so I wish to introduce something myself:
 - Make the chart period size variable (week/month)
 - Create chart of total value per unit type sold that period
 - Create chart of average value per unit sold that period
 
 On top of this I would like to add:
 - Design requires the following quality attribute scenarios:
  - Modifiability
   - Scenario: If a request arives to add a new type of point or value grouping. The development takes no more than 30 minutes, including the addition of UnitTests.
 - Data source and website should be a detail to the domain (union?)
 - Have 100% test coverage for the domain
 - Add design
 
 Things I deem out of scope
 - Everything regarding presentation
