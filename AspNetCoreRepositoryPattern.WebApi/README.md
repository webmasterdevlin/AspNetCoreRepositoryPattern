### Repository Pattern in ASP.NET Core

## Getting Started
- Create an ASP.NET Core Web API project
- Check first the installed Nuget packages in the csproj file of the projects inside the solution
- Delete WeatherForecast model and controller

## Building a Data Model
- Create entities
- Create dtos
- Add Automapper package configure the Program.cs file
- Configure AutoMapper by creating a mapping

## Setting up Database
- Create a class that inherit DbContext
- Add service of DbContext
- Add Connection String for the Database
- Install or update DotNet CLI below:
- dotnet tool install --global dotnet-ef
- dotnet tool update --global dotnet-ef
- Go to the terminal and go inside the project to run the EF Core commands
- Make sure you are not inside the solution folder, but inside the project folder of the Web API
- Do EF Core migrations below:
- dotnet ef migrations add initial
- dotnet ef database update

## Allowing local SSL in ASP.NET Core
- Allow dev-certs in the local machine below:
- dotnet dev-certs https --clean
- dotnet dev-certs https

## Repository Pattern in ASP.NET Core
- Create contracts (interfaces) (optional)
- Create repositories (services) (optional)
- Add Scrutor package configure the Program.cs file
- Add Scoped in the Program.cs file for contract and repository

## CORS
- Add and configure CORS Policy in the Program.cs file

## Open API (Swagger)
- Add API versioning
- Add Swashbuckle package
- Configure Swagger UI in the Program.cs file
- Create helpers and extensions for API versioning with Swagger UI integration

## Base Controller
- Create a base controller name ApiController

## API versioning
- Create todos controller on v1 only using repository pattern
- inherit the todos controller from ApiController
- Use the repository in todos controller
- Edit the launchSettings.json of the Properties folder from weatherforecast to swagger/index.html

## Logging
- Add Serilog Nuget packages
- Configure Serilog in the Program.cs

## Global Exception
- Create a global exception handler in Program.cs file

## Automated Tests
- Add Test project
- Add xunit packages
- Add reference to the web api project
- Create a MockData.cs and put it in the Data folder
- Add Fluent Assertions package
- Add test C# file for each controller
- Add test for each HTTP method

- Add books controller without repository pattern

## Scheduler/Cron Jobs/Timer
- Add Hangfire packages for chron jobs or scheduled jobs or timer jobs
- Configure Hangfire in the Program.cs file
- Add jobs controller on v1 and v1.1

## Security
- Add authentication and authorization
- Start with Entity, Dto, Interface, Service, Helpers, Controller, add JWT Secret in appsettings.json, register Auth, IUserService/UserService, and add JwtMiddleware in the Startup.cs
- Add authorize attribute in the base controller ApiController.cs

## Actuator
- Add health checks in Program.cs file

## Redis Caching
- Install redis on your local machine
- To install on macbook, run the command below:
- brew install redis
- Add Redis Cache
- Configure Program.cs file

- Configure appsettings.json
- Configure ApplicationDbContext
- Add Customers controller on v1 and v2 without using repository pattern
- Update the database using dotnet cli


## Test Coverage
- Add code coverage packages
- Run commands below:
- dotnet tool install -g dotnet-reportgenerator-globaltool
- dotnet test --collect:"XPlat Code Coverage"
- reportgenerator -reports:"Unit.Tests/TestResults/*/coverage.cobertura.xml" -targetdir:"Unit.Tests/coveragereport" -reporttypes:Html
- npx http-server Unit.Tests/coveragereport
- Check http://localhost:8080 to see generated test coverage UI