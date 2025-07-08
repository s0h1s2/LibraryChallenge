namespace Web.Controllers;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public int Status { get; set; }

    public static ApiResponse<T> Ok(T data, string? message = null, int status = 200)
        => new()
        {
            Success = true, 
            Message = message, 
            Data = data,
            Status= status
        };


    public static ApiResponse<T> Fail(string message,int status = 400)
        => new()
        {
            Success = false, 
            Message = message, 
            Data = default,
            Status = status
        };
}
