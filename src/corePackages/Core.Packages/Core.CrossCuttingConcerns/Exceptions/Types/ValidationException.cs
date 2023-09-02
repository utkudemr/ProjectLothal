
namespace Core.CrossCuttingConcerns.Exceptions.Types;

public class ValidationException : Exception
{
    public IEnumerable<ValidationExceptionModel> Errors { get; set; }
    public ValidationException() : base()
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }
    public ValidationException(string? message) : base(message)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
        Errors = Array.Empty<ValidationExceptionModel>();
    }

    public ValidationException(IEnumerable<ValidationExceptionModel> errors) : base(BuildErrorMessage(errors))
    {
        Errors = errors;
    }

    private static string BuildErrorMessage(IEnumerable<ValidationExceptionModel> errors)
    {
        var errorStringList = errors.Select(a => $"{Environment.NewLine} -- {a.Property}: {string.Join(Environment.NewLine, values: a.Errors ?? Array.Empty<string>())}");
        return $"Validation failed: {string.Join(string.Empty, errorStringList)}";
    }
}



public class ValidationExceptionModel
{
    public string? Property { get; set; }
    public IEnumerable<string>? Errors { get; set; }
}
