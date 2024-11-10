using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using MediatrSample.Api.Exceptions;
using MediatrSample.Api.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MediatrSample.Api.Middleware.Extensions
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
                var jsonError = JsonSerializer.Serialize(error);

                string clientMessage;
                if (exception is ValidationException)
                {
                    logger.LogError(jsonError);
                    clientMessage = exception.Message;
                }
                else
                {
                    logger.LogCritical(jsonError);
                    clientMessage = "An Error Occured";
                }

                var clientResponse = new ApiResponse
                {
                    Success = false,
                    Messages = [clientMessage]
                };

                var result = JsonSerializer.Serialize(clientResponse);

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }));
        }
    }
}
