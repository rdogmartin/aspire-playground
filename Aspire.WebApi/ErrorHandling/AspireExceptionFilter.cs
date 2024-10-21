using System.Net;
using Aspire.WebApi.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aspire.WebApi.ErrorHandling;

public class AspireExceptionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        switch (context.Exception)
        {
            case AspireValidationException ex:

                var response = new AspireValidationResponse(
                    Type: ex.GetType().Name,
                    Title: "Aspire Business Validation Error!!",
                    Status: (int)HttpStatusCode.UnprocessableEntity,
                    Errors: new Dictionary<string, string> { { ex.ErrorSource, ex.Message } },
                    TraceId: context.HttpContext.TraceIdentifier
                );

                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.UnprocessableEntity
                };
                context.ExceptionHandled = true;
                break;
        }
    }

    public void OnActionExecuting(ActionExecutingContext context) { }
}
