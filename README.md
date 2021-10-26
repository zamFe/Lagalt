# Lagalt

Lagalt is a project management platform designed to connect individuals in creative fields with projects that need their specific skill sets.

## The Team

- [Edlix](https://github.com/Edlix)
- [Emil Onsøyen](https://github.com/emilons)
- [lassoer](https://github.com/lassoer)
- [singh1999](https://github.com/singh1999)
- [zamfe](https://github.com/zamFe)

## Installation Instructons

### Website
1. Navigate to `Lagalt/frontend/lagalt`
2. Run `ng serve --open` in a terminal

### API (Dev mode)

1. Install PostgreSQL and set up a server on localhost
2. In the environment variables for your pc account, add a new variable
   `CONNECTION_STRING`, with your PostgreSQL connection string as value
3. Open `Lagalt/backend/LagaptAPI/LagaltAPI.sln` in Visual Studio 2019
4. Run `update-database` in the package manager console

## Technical

### Front-End

The Lagalt website is written in Angular.
Hostet at: [lagalt website](https://orange-tree-0b9310403.azurestaticapps.net)

### Back-End

The Lagalt API is written in ASP.NET.
Hostet at: [lagalt API](https://lagalt-api-f.azurewebsites.net)

### Data Storage

The Lagalt API communicates with a Heroku Postgres database using Entity Framework.

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
