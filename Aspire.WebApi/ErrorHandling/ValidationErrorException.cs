using System.Globalization;

namespace Aspire.WebApi;

public class ValidationErrorException : ApplicationException
{
    public string ErrorSource { get; }
    
    public ValidationErrorException() : base() {
        ErrorSource = string.Empty;
     }

    public ValidationErrorException(string message) : base(message) {
        ErrorSource = string.Empty;
    }

    public ValidationErrorException(string message, string errorSource) : base(message) {
        ErrorSource = errorSource;
        //this.Data.Add("ErrorSource", errorSource);
    }

    public ValidationErrorException(string message, params object[] args)
        : base(String.Format(CultureInfo.CurrentCulture, message, args))
    {
        ErrorSource = string.Empty;
    }

}
