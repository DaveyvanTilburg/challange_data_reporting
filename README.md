# Description

This project is created in response to a challenge where you have to solve a problem in a reasonable amount of time. Though I have given quite a huge twist to the content of the challenge because I think it is fun. I wish to both enjoy my time working on this small project and learn from the experience as much as possible by giving myself a few extra challenges regarding design.

### Translated into my own words, the challenge includes the following:
 - From a data source ([data](Infrastructure/MockedData/sales.csv) supplied in csv) create an overview per month that shows the performance of a product.
 - Vertical axis is 'value' and horizontal axis is time
 
### Self introduced goal: I don't really understand what "performence" means, so I wish to introduce something myself: (in retrospect, after having talked with the 'challengers', they said it was part of the challenge to see if I would ask questions to remove this ambiguity, which I did not and I feel kind of stupid for that. I took the challenge 'less serious' and saw opportunity in this ambiguity to make it fun, of course I would not act in such a way 'on the job' because then I am not doing a project for my own enjoyment, but for the benefit of someone else. You live, you learn)
 - Make the chart period size variable (week/month)
 - Create chart of total value per unit type sold that period
 - Create chart of average value per unit sold that period
 
### On top of this I would like to add:
 - Design requires the following quality attribute scenarios:
   - Modifiability:
     - Scenario: If a request arrives to add a new type of point or value grouping. The development takes no more than 30 minutes, including the addition of UnitTests
       - To validate this scenario, once everything is done, I will add a new period grouping type based on day of week
   - Performance:
     - Scenario: Generating the source for the full 10K load should not take more than 200ms
       - To validate this, I will add a unit tests. This is not the most accurate way to validate this as it is system dependent, but it works for now
 - Data source and website should be a detail to the domain (union?)
 - Have 100% test coverage for the domain
 - Add design
 - Changed a few dates in the sales.csv to have data over multiple years, I saw that only 2018 is in the data set, slightly increases the challenge for grouping per period
 
### Things I deem out of scope
 - Everything regarding presentation

# Documentation

The gist of it is that if the domain is presentation of data with the above mentioned quality attributes, I think it is important to have an architecture that represents interchangeability of components in the algorithm that creates the data set.

This UML only described the first version of the result, but the idea is that anything separated by an interface encapsulates a different responsibility. So extending the functionality means to only create a new implementation and using it in the composition:

![UML](Documentation/Overview.jpg)
