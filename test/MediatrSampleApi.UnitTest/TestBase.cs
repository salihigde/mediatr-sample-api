using MediatrSampleApi.Models;
using AutoFixture;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediatrSampleApi.Filters;
using System.Reflection;

namespace MediatrSampleApi.UnitTest
{
    public class TestBase
    {
        protected readonly IMediator mediator;
        protected readonly Fixture fixture;

        public TestBase()
        {
            var services = new ServiceCollection();
            
            // Services
            services.AddMediatR(typeof(Startup));
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            }).AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); })
            .AddApplicationPart(Assembly.Load("MediatrSampleApi.UnitTest"));
            services.AddAutoMapper(typeof(Startup));


            // Database
            services.AddDbContext<ApiDbContext>((y) => y.UseInMemoryDatabase("s"), ServiceLifetime.Transient);

			fixture = new Fixture();

            var sp = services.BuildServiceProvider();

			mediator = sp.GetService<IMediator>();
        }
    }
}
