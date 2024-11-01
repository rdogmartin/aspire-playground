using System.Globalization;

namespace Aspire.WebApi;

public class AspireValidationException : ApplicationException
{
    public string ErrorSource { get; }
    
    public AspireValidationException() : base() {
        ErrorSource = string.Empty;
     }

    public AspireValidationException(string message) : base(message) {
        ErrorSource = string.Empty;
    }

    public AspireValidationException(string message, string errorSource) : base(message) {
        ErrorSource = errorSource;
    }

    public AspireValidationException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
        ErrorSource = string.Empty;
    }

}
