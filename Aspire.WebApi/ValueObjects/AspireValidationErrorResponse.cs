using System.Text.Json.Serialization;

namespace Aspire.WebApi.ValueObjects;

/// <summary>
/// Represents a validation error response in the Aspire Web API. This object is intended to match the JSON structure that
/// is returned when the .NET model validation fails.
/// </summary>
/// <param name="Type">The type of the error.</param>
/// <param name="Title">The title of the error.</param>
/// <param name="Status">The HTTP status code associated with the error.</param>
/// <param name="Errors">A dictionary containing the validation errors, where the key is the field name and the value is the error message.</param>
/// <param name="TraceId">The trace identifier for the request, useful for debugging and logging.</param>
 public record class AspireValidationErrorResponse(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("status")]int Status,
    [property: JsonPropertyName("errors")] Dictionary<string, string> Errors,
    [property: JsonPropertyName("traceId")] string TraceId);


