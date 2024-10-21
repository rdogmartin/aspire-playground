using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Aspire.WebApi.ErrorHandling;

public static class InvalidModelStateResponse
{
    /// <summary>
    /// Generates a custom error response for the scenario where the .NET model validation fails.
    /// </summary>
    /// <param name="context">The context of the current action, containing information about the HTTP request and model state.</param>
    /// <returns>An <see cref="IActionResult"/> containing a custom validation error response with a status code of 422 (Unprocessable Entity).</returns>
    public static IActionResult Generate(ActionContext context)
    {
        var statusCode = HttpStatusCode.UnprocessableEntity;
        var type = "ModelValidationException";
        var title = "One or more validation errors occurred.";
        // var detail = "Correct the validation errors and try again.";
        // var instance = $"Path: {context.HttpContext.Request.Path}";

        var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();
        ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState, (int)statusCode, title, type);

        return new ObjectResult(problemDetails)
        {
            StatusCode = (int)HttpStatusCode.UnprocessableEntity
        };
    }
}

// NOTES:
// This is an example of a default .NET-generated error response based on a DataAnnotation validation error.
// {
//   "type": "https://tools.ietf.org/html/rfc4918#section-11.2",
//   "title": "One or more validation errors occurred.",
//   "status": 400,
//   "errors": {
//     "Name": [
//       "The field Name must be a string or array type with a minimum length of '5'."
//     ],
//     "Email": [
//       "The Email field is not a valid e-mail address."
//     ],
//   },
//   "traceId": "00-4239d864e1a7b3e9d21193b3f6157ee1-a976fb171a85feb4-00"
// }