using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatrSampleApi.Handlers.Behaviors
{
    /// <summary>
    /// Common logging for Mediatr to log before and after handler execution
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        ILogger logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public LoggingBehavior(ILoggerFactory logger)
        {
            this.logger = logger.CreateLogger(typeof(LoggingBehavior<,>));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            logger.LogInformation($"Handling {request.GetType().Name}");

            var response = await next();

            logger.LogInformation($"Handled {request.GetType().Name}");

            return response;
        }
    }
}
