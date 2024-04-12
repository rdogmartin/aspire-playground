using System.Diagnostics;
using System.Reflection;
using Aspire.WebApi.Dtos;
using Aspire.WebApi.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class ContactController : ControllerBase
{
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Contact> Get(int id)
    {
        return new Contact(id, "John Doe", "none@nospam.com");
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Contact> Post(Contact contact)
    {
        // TODO: Persist to DB
        contact = contact with { Id = 1 };

        return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
    }
}