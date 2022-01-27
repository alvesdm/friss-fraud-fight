# FightFraud exercise
So in this exercise my goal was not to have a boolet proof / production-ready solution, but rather show a bit of my development style as well as address the challenge proposed in the exercise, also not on preduction-ready level, but instead getting it to work.

## Assumptions

- Decided to use this new out-of-the-box Angular-SPA + built in IdentityServer + ASP.Net Core API VIsual Studio template as some of the requirements were both a UI in place as well as security. So for the sake of simplicity and speed for this challenge, this seemed like a perfect candidate. In a production application, would not have it combined, but instead have it split into different projects, like:
    - If IdentityServer(like the one used in this template) was a the decided choice for the IdP, then an specific project(service/api) for the IdP;
    - If another IdP, like AWS Cognito or Auth0, then... problem solved. One project less.
    - What's important to say here is that this combined solution would't be my choice of preference, specially with having the IdP directly attached to the SPA... seems like a good solution for a prototyping, but and nothing more than that. Instead the SPA should comunicate with the (likely) company's IdP provider.
- Also noticed that this built-in template is not handling silent token refresh whatsoever, which would be required in a production-ready SPA, but should be fine for the sake of this exercise. So keep in mind that token expiration glitches might happen;
- InMemory database has been chosen as it provides what it takes for this exercise. In order to get it up and running by the time the application starts, Im feeding with the minimum data, at the initialization of the application. So keep in mind that this is not realy persisting the data changed, once the appliation is terminated;
- IDistributedCache(InMemoryCache) has also been chosen so that it shares the same api as most common caching solutions like Redis, NCache and so on, that are distributed caching solutions. This one has the purpose to emulate a distributed caching with the same api, but ofc, keeps it in memory, which again for the purpose of easy-setup and simplicity of this exercise, makes it a good candidate;
- As for domain events, I had it implemented in on of the domain entities, just to show how it gets implemented, but didn't do it for all other entities, as the purpose is to show how I implement it;
- On the UI side, I didn't pay much attention to pixel perfect design, data validations(which has been added to the backend in some of the models, again just to show how I do it, therefore didn't pply it for every client model) and such;
- As for logging, it's currently logging to both console and FileSystem. In a production application I think a good approach is to have it logged locally to a flat file and have those log collected by a logging collector like FluentD(Fluentbit), so that you can have not only transformation, but offloading the pressure on the application itself to deal with sending the log to an external service, like ElasticSearch and such, but instead the Logging Collector would do that in the background for us;
- Left some comments in the code on purpose with some clarifications to help guide on my decisions, but I totally agree with avoiding code comments.


## Design choices

Some of the design choices and their purposes:

- Decide to use the Mediator pattern(by using MediatR package), as it allows us to cover lots os aspects that helps with maing the code more well structured, clear and simple, like:
    - Slin(Non-fat) controllers, by handling commands, making the controllers very clean and simply being used as input/output facade;
    - Pipeline behaviors, like Request Logging, Unhandled Exception Handling, Request models validation, and so on;
- NSwag as a choice so that we can generate the API documentation as well as helping with scafolding some code for the client(typescript).. so very handy.
The Swagger documentation endpoint is: https://localhost:44358/api
- Serilog, as it gives a great way of formating the logs in compact json format as well as allowing parametrized log events;
- For request model validation FLuentvalidation makes it a good candidate as it can be easily integrate with MediatR pipelines/behaviors;

## The Exercise solution
- For the matching part, specially for the name matching, I have decided to use the 'Levenshtein Distance' approach, which seems to work, at least for the scenarios provided, but assuming that it might end up sliping up on some edge case scenarios.
A better approach I believe would be a database SP that would deal with all the matching criteria as well as taking care of all the fuzzy ones by using the Soundex algorithm(i.e: SQL DIFFERENCE() function). It can be that a pure C# solution is possible in a way that perforance does not become an issue in case we have a huge set of data, but at first glance all the trip to-from(SQL - Application(C#)) seems to be a point of concern to me.
A custom Linq function to use the SQL DIFFERENCE() function could be created in order to be used with Entity Framework, for instance, where a pure C# solution would be in place,, but still Im not sure how that would perform.
On top of that, depending on the requirement a background service could also be invoked to perform the search and return in a later event, but again that would be totally asyncronous that would depend on the existing requirement.
In the code, I have chosen to adopt the Bridge Pattern so that we could have different implementations(Soundex, Levenshtein Distance, etc) of that part.

## Installation

The requirements seem to be:
- NPM(NodeJS);
- .Net 5

If using VS2022, looks like everything is in place already and it's just a matter of running the application.

Important: 
- Make sure the path where the logs are created is set to an existing path, in the appsettings.json file.
- The first time you run the application on VS2022, it might take a long time to download all the NPM dependencies, that comes bundled with the built-in SPA template used. That sucks!... and one more reason nnot to use such template in production ;)

The application is likely going to run on this URL: https://localhost:44358/
