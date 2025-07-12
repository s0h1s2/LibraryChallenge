namespace Web.Controllers;

public record SuccessResponse<T>(T Data, string? Message = null, int Status = 200);

public record FailureResponse
{
    public FailureResponse(string message)
    {
        Message = message;
    }

    public string Message { get; init; } = string.Empty;
}

public record FailureResponse<TData>
{
    public FailureResponse(string message, TData data)
    {
        Message = message;
        Data = data;
    }

    public string Message { get; init; } = string.Empty;
    public TData Data { get; init; } = default!;
}