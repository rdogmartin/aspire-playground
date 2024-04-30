using System.Globalization;

namespace Aspire.WebApi;

public class ValidationException : ApplicationException
{
    public ValidationException() : base() { }

    public ValidationException(string message) : base(message) { }

    public ValidationException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
    }
}
