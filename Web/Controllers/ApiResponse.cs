namespace Web.Controllers;

public abstract record ApiResponseParent;

public record SuccessResponse<T>(T Data, string? Message = null, int Status = 200);

public record FailureResponse<T>(T Data, string? Message = null, int Status = 400);

public class ApiResponse<T>
{
    public static SuccessResponse<T> Ok(T data, string? message = null, int status = 200)
    {
        return new SuccessResponse<T>(data, message, status);
    }

    public static FailureResponse<T> Fail(string message, int status = 400)
    {
        return new FailureResponse<T>(default!, message, status);
    }
}