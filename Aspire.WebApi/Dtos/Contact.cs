using System.ComponentModel.DataAnnotations;

namespace Aspire.WebApi.Dtos;

/// <summary>
/// Represents a contact.
/// </summary>
/// <param name="Id">The contact ID</param>
/// <param name="Name">The contact name</param>
/// <param name="Email">The contact email</param>
public record class Contact(int Id, [MinLength(5)] string Name, [EmailAddress] string Email)
{
    /// <summary>
    /// Gets or sets the occupation of the contact (optional).
    /// </summary>
    public string? Occupation { get; init; }
}
