using System.Diagnostics;
using System.Reflection;
using Aspire.WebApi.Dtos;
using Aspire.WebApi.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Aspire.WebApi.Managers;
using System.ComponentModel.DataAnnotations;

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
        return new Contact(id, "John Doe", "none@nospam.com");
    }

    [HttpPost("AddContact_ThrowException")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<Contact> AddContact_ThrowException(Contact contact)
    {
        contact = contactManager.AddContactThrowException(contact);

        return CreatedAtAction(nameof(Get), new { id = contact.Id }, contact);
    }

    [HttpPost("AddContact_ReturnObject")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public ActionResult<Contact> AddContact_ReturnObject(Contact contact)
    {
        var response = contactManager.AddContactUseReturnObject(contact);

        if (response.ValidationResult != ValidationResult.Success)
        {
            return UnprocessableEntity(response.ValidationResult);
        }

        return CreatedAtAction(nameof(Get), new { id = response.Contact.Id }, response.Contact);
    }
}