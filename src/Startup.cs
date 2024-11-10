using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using MediatrSample.Api.Filters;
using MediatrSample.Api.Handlers.Behaviors;
using MediatrSample.Api.Middleware.Extensions;
using MediatrSample.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace MediatrSample.Api;

/// <summary>
/// </summary>
public class Startup
{
    /// <summary>
    /// </summary>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Startup));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
        services.AddDbContext<ApiDbContext>(opt => opt.UseSqlite("Data Source=sampleapi.db"));

        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ValidateModelStateAttribute));
            options.Filters.Add(typeof(ApiResponseAttribute));
        });

        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mediatr Sample Api", Version = "v1" });
            c.ExampleFilters();

            var filePath = Path.Combine(System.AppContext.BaseDirectory, "MediatrSampleApi.xml");
            c.IncludeXmlComments(filePath);
        });

        services.AddSwaggerExamplesFromAssemblyOf<Startup>();
    }

    /// <summary>
    /// </summary>
    public void Configure(IApplicationBuilder app, IHostEnvironment env, ILogger<Startup> logger)
    {
        app.UseSwagger();

        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mediatr Sample Api V1"); });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.ConfigureExceptionHandler(logger);

        app.UseRouting();

        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("", context =>
            {
                context.Response.Redirect("./swagger", false);
                return Task.CompletedTask;
            });

            endpoints.MapControllers();
        });
    }
}