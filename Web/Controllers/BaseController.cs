using System.Net;

using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult Success<T>(T? data, string? message = null, int? status = 200)
        => StatusCode(status ?? 200, ApiResponse<T>.Ok(data, message));

    protected IActionResult Failure(string message, int statusCode = 400)
        => StatusCode(statusCode, ApiResponse<string>.Fail(message, statusCode));

}