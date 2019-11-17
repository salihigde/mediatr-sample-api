# Sample .Net Core 3.0 API implementation by using Mediator Design Pattern

Purpose of this project is to provide fully working .Net Core 3.0 API example by using Mediator Design Pattern. 

## Remarks
- SQLite is used as relational database solution
- Middleware is used for having common exception handling (see [ExceptionMiddlewareExtensions](https://github.com/salihigde/mediatr-sample-api/blob/master/src/Middleware/Extensions/ExceptionMiddlewareExtensions.cs))
- Action Filters are used both for validation and returning common response for every action (see [ValidateModelStateAttribute](https://github.com/salihigde/mediatr-sample-api/blob/master/src/Filters/ValidateModelStateAttribute.cs) and [ApiResponseAttribute](https://github.com/salihigde/mediatr-sample-api/blob/master/src/Filters/ApiResponseAttribute.cs))
- Mediatr Behaviors are used for having common logging in before and after handler execution (see [LoggingBehavior](https://github.com/salihigde/mediatr-sample-api/blob/master/src/Handlers/Behaviors/LoggingBehavior.cs))  

## Swagger
![Swagger](swagger.png)


## Getting Started

### Prerequisites
- .Net Core 3.0

### Starting Application
- Build the project
- Either start application in debug more or execute `dotnet run` command in `/src` folder

### Unit Tests
- Run `dotnet test` command in `MediatrSampleApi.UnitTest` folder

## Built With
List of development and testing NuGet packages can be found below.
### Development
- [FluentValidation](https://github.com/JeremySkinner/FluentValidation) - Library for building strongly-typed validation rules.
- [AutoMapper](https://github.com/AutoMapper/AutoMapper) - A convention-based object-object mapper in .NET
- [Mediatr](https://github.com/jbogard/MediatR) - mediator implementation in .NET
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle) - adds a swagger to WebApi projects
- [Entity Framework](https://github.com/aspnet/EntityFrameworkCore)

### Testing
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions) - Fluent API for asserting the results of unit tests that targets
- [MyTested](https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc) - Fluent testing library for ASP.NET Core MVC
- [AutoFixture](https://github.com/AutoFixture/AutoFixture)
- [Moq](https://github.com/moq/moq4)
