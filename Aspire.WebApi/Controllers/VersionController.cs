using System.Diagnostics;
using System.Reflection;
using Aspire.WebApi.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class VersionController : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    public ActionResult<VersionResponse> Get()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string buildNumber;
        string commitNumber;

        var fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
        buildNumber = fileVersionInfo?.ProductVersion?.Split('.')[2] ?? "unknown";
        commitNumber = fileVersionInfo?.ProductVersion?.Split('.')[1] ?? "unknown";

        var response = new VersionResponse(
            assembly.GetName().Version?.ToString() ?? "unknown",
            Environment.MachineName,
            buildNumber,
            commitNumber
        );
        return response;
    }
}