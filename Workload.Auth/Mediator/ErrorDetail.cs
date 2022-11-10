namespace EnergiBolaget.Auth.Mediator;

public class ErrorDetail
{
    private ErrorDetail(string message)
    {
        Message = message;
    }

    public static ErrorDetail Create(string message)
    {
        return new ErrorDetail(message);
    }

    public string Message { get; set; }
}