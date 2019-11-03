using System.Collections.Generic;
using System.Net;
using MediatrSampleApi.Handlers.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MediatrSampleApi.Middleware.Extensions
{
    /// <summary>
    /// Extension method for handling exceptions in centralized location
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// </summary>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                var error = new Dictionary<string, object>();
                error.Add("errorDetails", exception.StackTrace);
                error.Add("errorMessage", exception.Message);

                logger.LogCritical(JsonConvert.SerializeObject(error));

                var clientResponse = new ApiResponse
                {
                    Success = false,
                    Messages = new List<string> { "An Error Occured" }
                };

                var result = JsonConvert.SerializeObject(clientResponse);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
        }
    }
}
