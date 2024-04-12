namespace Aspire.WebApi.ValueObjects;

public record class VersionResponse(string ApiVersionNumber, string WebServerName, string WebBuildNumber, string WebCommitNumber)
{
}
