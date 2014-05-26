// adds an error message to a ValidationSummary control
using System.Web.UI;
public class ErrorSummary : IValidator
{
    string _message;

    public static void AddError(string message, Page page)
    {
        ErrorSummary error = new ErrorSummary(message);
        page.Validators.Add(error);
    }

    private ErrorSummary(string message)
    {
        _message = message;
    }

    public string ErrorMessage
    {
        get { return _message; }
        set { }
    }

    public bool IsValid
    {
        get { return false; }
        set { }
    }

    public void Validate()
    { }
}