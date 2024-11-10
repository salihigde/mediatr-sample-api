using System.Reflection;
using AutoFixture;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using MediatrSample.Api.Filters;
using MediatrSample.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MediatrSample.Api.UnitTests
{
    public class TestBase
    {
        protected readonly IMediator Mediator;
        protected readonly Fixture Fixture;

        internal TestBase()
        {
            var services = new ServiceCollection();
            
            // Services
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            }).AddApplicationPart(Assembly.Load("MediatrSampleApi.UnitTest"));

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddAutoMapper(typeof(Startup));


            // Database
            services.AddDbContext<ApiDbContext>((y) => y.UseInMemoryDatabase("s"), ServiceLifetime.Transient);

			Fixture = new Fixture();

            var sp = services.BuildServiceProvider();

			Mediator = sp.GetService<IMediator>();
        }
    }
}
