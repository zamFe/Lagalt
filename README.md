# Lagalt

Lagalt is a project management platform designed to connect individuals in creative fields with projects that need their specific skill sets.

## The Team

- [Edvard Lindgren](https://github.com/Edlix)
- [Emil Onsøyen](https://github.com/emilons)
- [Lasse Sørmo](https://github.com/lassoer)
- [Rahul Singh](https://github.com/singh1999)
- [Felix Amundsen Zamora](https://github.com/zamFe)

## Installation Instructons

### Website
1. Navigate to `Lagalt/frontend/lagalt`
2. Run `npm install`
3. Run `ng serve --open`

### API (Dev mode)

*Optional:* Set up Auth0 to access restricted endpoints (or remove \[Authorize] in the api controllers)
1. Set up a PostgreSQL server
2. The code supports Local, Heroku and Azure deployment, but you need to specify some environment variables:
    * **ASPNETCORE_ENVIRONMENT:** Set to "Production" *!only for Heroku and Azure!*
    * **AUTH_AUDIENCE:** Audience for Auth0
    * **AUTH_DOMAIN:** Domain for Auth0
    * **CLOUD_PLATFORM:** Either "HEROKU" or "AZURE" *!only for Heroku and Azure!*
    * **DATABASE_URL:** The connection string for pgsql. Supports the formats given by Heroku and Azure
    * **FRONTEND_URL** The url to your front-end *can be localhost*. This is to bypass CORS restrictions
4. Open `Lagalt/backend/LagaptAPI/LagaltAPI.sln` in Visual Studio 2019
5. Run `update-database` in the package manager console

## Technical

### Front-End

The Lagalt website is written in Angular.
It is a single page web application hosted as an Azure static web app.

Hostet at: [lagalt website](https://orange-tree-0b9310403.azurestaticapps.net)

### Back-End

The Lagalt API is written in ASP.NET.
It is a RESTful api with CORS protection and authorization.
It is hosted on an Azure App Service.

Hostet at: [lagalt API](https://lagalt-api-f.azurewebsites.net)

### Data Storage

The Lagalt API communicates with an Azure PostgreSQL database using Entity Framework.

## Features

### Create and join projects withing your field

Lagalt allows you to create projects within your field. Specify what skills you need to attract contributers.

Use the filter and Search functionalities to find projects that need your skill sets.

### Start interacting immediately 

Browse our website anonymously *without logging in*, before you decide to join. 

### Privacy and Security comes first

Browse our website freely *without logging in* and find projects that interest you.

Toggle 'Hidden Mode' to reduce what you share with other users.

Lagalt only stores anonymous data and data required for login. No private information is kept in our databases.

External OAuth login minimizes the security risks when creating a user and logging in.
