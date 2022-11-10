namespace EnergiBolaget.Auth.Mediator;

public class MediatorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; } = null;
}
