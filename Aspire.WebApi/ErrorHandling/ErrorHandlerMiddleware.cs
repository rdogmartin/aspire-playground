using System.Net;
using System.Text.Json;
using Aspire.WebApi.ValueObjects;

namespace Aspire.WebApi.ErrorHandling;

/// <summary>
/// This is custom middleware that catches all exceptions and determines 
/// which HTTP response code to return based on the exception type.
/// </summary>
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

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
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await response.WriteAsync(JsonSerializer.Serialize(new { message = error?.Message }));
                    break;
                case AspireValidationException ex:
                    await WriteValidationExceptionResponse(context, ex);
                    break;
                default:
                    // Unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await response.WriteAsync(JsonSerializer.Serialize(new { message = error?.Message }));
                    break;
            }
        }
    }

    private async static Task WriteValidationExceptionResponse(HttpContext context, AspireValidationException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

        var errorSource = !string.IsNullOrWhiteSpace(ex.ErrorSource) ? ex.ErrorSource : "Error";

        var response = new AspireValidationResponse(
            Type: ex.GetType().Name,
            Title: "Aspire Business Validation Error",
            Status: context.Response.StatusCode,
            Errors: new Dictionary<string, string> { { errorSource, ex.Message } },
            TraceId: context.TraceIdentifier
        );

        await context.Response.WriteAsJsonAsync(response);
    }
}
