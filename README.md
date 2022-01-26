# FightFraud exercise
So in this exercise my goal was not to have a boolet proof / production-ready solution, but rather show a bit of my development style as well as address the challenge proposed in the exercise, also not on preduction-ready level, but instead getting it to work.

## Assumptions

- Decided to use this new out-of-the-box Angular-SPA + built in IdentityServer + ASp.Net Core API VIsual Studio template as some of the requirements were both a UI in place as well as security. So for the sake of simplicity and speed for this challenge, this seemed like a perfect candidate. In a production application, would not have it combined, but instead have it split into different projects, like:
    - If IdentityServer(like the one used in this template) was a the decided choice for the IdP, then an specific project(service/api) for the IdP;
    - If another IdP, like AWS Cognito or Oauth0, then... problem solved. One project less.
    - What's important to say here is that this combined solution would't be my choice of preference, specially with having the IdP directly attached to the SPA... seems like a good solution for a prototyping, but and nothing more than that. Instead the SPA should comunicate with the (likely) company's IdP provider.
- Also noticed that this built-in template is not handling silent token refresh whatsoever, which would be required in a production-ready SPA, but should be fine for the sake of this exercise. So keep in mind that token expiration glitches might happen;
- InMemory database has been chosen as it provides what it takes for this exercise. In order to get it up and running by the time the application starts, Im feeding with the minimum data, at the initialization of the application. So keep in mind that this is not realy persisting the data changed, once the appliation is terminated;
- IDistributedCache(InMemoryCache) has also been chosen so that it shares the same api as most common caching solutions like Redis, NCache and so on, that are distributed caching solutions. This one has the purpose to emulate a distributed caching with the same api, but ofc, keeps it in memory, which again for the purpose of easy-setup and simplicity of this exercise, makes it a good candidate;
- As for domain events, I had it implemented in on of the domain entities, just to show how it gets implemented, but didn't do it for all other entities, as the purpose is to show how I implement it;
- On the UI side, I didn't pay much attention to pixel perfect design, data validations(which has been added to the backend in some of the models, again just to show how I do it, therefore didn't pply it for every client model) and such;
- 

