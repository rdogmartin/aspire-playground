
using System.ComponentModel.DataAnnotations;
using Aspire.WebApi.Dtos;

namespace Aspire.WebApi.Managers;

public class ContactManager
{
    public Contact AddContactThrowException(Contact contact)
    {
        if (contact.Id > 0)
        {
            throw new ValidationException("Contact already exists.");
        }

        // TODO: Persist to DB. Fake it for now.
        contact = contact with { Id = 1 };

        return contact;
    }

    public CustomerManagerResult AddContactUseReturnObject(Contact contact)
    {
        if (contact.Id > 0)
        {
            var valResult = new ValidationResult("Contact already exists.");

            return new CustomerManagerResult(contact, valResult);
        }

        // TODO: Persist to DB. Fake it for now.
        contact = contact with { Id = 1 };

        var response = new CustomerManagerResult(contact, ValidationResult.Success!);

        return response;
    }
}


