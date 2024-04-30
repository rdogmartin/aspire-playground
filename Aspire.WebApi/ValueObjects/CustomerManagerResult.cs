using System.ComponentModel.DataAnnotations;
using Aspire.WebApi.Dtos;

namespace Aspire.WebApi;

public record class CustomerManagerResult(Contact Contact, ValidationResult ValidationResult)
{ }
