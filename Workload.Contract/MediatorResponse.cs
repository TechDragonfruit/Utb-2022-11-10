namespace Workload.Contract;

public class MediatorResponse
{
    public int Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public object? Data { get; set; }
}