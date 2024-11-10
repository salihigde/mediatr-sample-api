using System.Linq;
using MediatrSample.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MediatrSample.Api.Filters
{
    /// <summary>
    /// Centralized validation handler which is used to validate ModelState of the
    /// controller actions
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage)).ToList();

                var result = new ApiResponse
                {
                    Success = false,
                    Messages = errors
                };

                context.Result = new BadRequestObjectResult(result);
            }
        }
    }
}
