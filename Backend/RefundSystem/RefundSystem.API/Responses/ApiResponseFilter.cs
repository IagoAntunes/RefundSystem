using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RefundSystem.API.Responses // Certifique-se que o namespace está correto
{
    public class ApiResponseFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                return;
            }

            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.StatusCode >= 200 && objectResult.StatusCode < 300)
                {
                    var successResponse = ApiResponse<object>.CreateSuccess(objectResult.Value);

                    context.Result = new ObjectResult(successResponse)
                    {
                        StatusCode = objectResult.StatusCode
                    };
                }
            }
            else if (context.Result is StatusCodeResult statusCodeResult &&
                     statusCodeResult.StatusCode >= 200 &&
                     statusCodeResult.StatusCode < 300)
            {
                var successResponse = ApiResponse<object>.CreateSuccess(null);
                context.Result = new ObjectResult(successResponse)
                {
                    StatusCode = statusCodeResult.StatusCode
                };
            }
        }
    }
}