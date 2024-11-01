using Aspire.WebApi.Dtos;
using Aspire.WebApi.Managers;
using Aspire.WebApi.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class ContactController(ContactManager contactManager) : ControllerBase
{
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Contact> Get(int id)
    {
        return new Contact(id, "John Doe", "john@gmail.com");
    }

    [HttpPost("AddContact")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(AspireValidationResponse), StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<Contact> AddContact(Contact contact)
    {
        contact = contactManager.AddContact(contact);

        return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
    }
}