using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace Web.Util.Validations;

public class CustomValidationErrorResponse : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        var errors = validationProblemDetails.Errors
            .Select(pair => new KeyValuePair<string, string[]>(pair.Key.ToLower(), pair.Value))
            .ToDictionary(t => t.Key.ToLower(), t => t.Value);
        var errorsResult = new ValidationProblemDetails
        {
            Errors = errors,
            Title = validationProblemDetails.Title,
            Detail = validationProblemDetails.Detail,
            Extensions = validationProblemDetails.Extensions,
            Instance = validationProblemDetails.Instance,
            Status = StatusCodes.Status422UnprocessableEntity,
            Type = validationProblemDetails.Type
        };
        return new UnprocessableEntityObjectResult(errorsResult);
    }
}