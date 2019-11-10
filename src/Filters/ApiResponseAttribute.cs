using MediatrSampleApi.Handlers.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MediatrSampleApi.Filters
{
    /// <summary>
    /// Returns ApiResponse which is a wrapper for controllers to have
    /// common response contract
    /// </summary>
    public class ApiResponseAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                objectResult.Value = new ApiResponse<object>
                {
                    Result = objectResult.Value,
                    Success = true
                };
            }
        }
    }
}
