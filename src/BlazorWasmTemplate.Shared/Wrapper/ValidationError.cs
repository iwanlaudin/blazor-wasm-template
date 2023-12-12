namespace Shared.Wrapper;

public class ValidationError
{
    public string? PropertyName { get; set; }
    public object? AttemptedValue { get; set; }
    public string? ErrorCode { get; set; }
    public string? Message { get; set; }
}
