using System.Net;
using System.Text.Json;

namespace Aspire.WebApi.ErrorHandling;

/// <summary>
/// This is custom middleware that catches all exceptions and determines 
/// which HTTP response code to return based on the exception type.
/// </summary>
public class ErrorHandlerMiddleware
{
    /// <summary>
    /// The request delegate handles each HTTP request.
    /// </summary>
    private readonly RequestDelegate _next;


    /// <summary>
    /// Initialize an instance of the class.
    /// </summary>
    /// <param name="next">An instance of <see cref="RequestDelegate" /></param>
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// The action to take when an unhandled exception occurs.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext" /></param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case KeyNotFoundException e:
                    // not found error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException e:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
