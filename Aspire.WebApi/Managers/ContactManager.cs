using Aspire.WebApi.Dtos;

namespace Aspire.WebApi.Managers;

public class ContactManager
{
    public Contact AddContact(Contact contact)
    {
        if (contact.Id > 0)
        {
            throw new ValidationErrorException("Contact already exists.", "Contact");
        }

        if (contact.Occupation == "Freeloader")
        {
            throw new ValidationErrorException("Freeloader is not a valid occupation.", nameof(contact.Occupation));
        }

        // TODO: Persist to DB. Fake it for now.
        contact = contact with { Id = 1 };

        return contact;
    }
}
