using FluentValidation;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Web.Util.OpenApiConfiguration;

public class UnprocessableEntityOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        // Check if arguments of the controller are of type AbstractValidator<T> of fluent validation
        // If so, add a 422 Unprocessable Entity response to the operation
        Type fluentValidationType = typeof(AbstractValidator<>);
        if (context.Description.ParameterDescriptions.Any(obj =>
                obj.Type.BaseType != null && obj.Type.BaseType.IsAssignableFrom(fluentValidationType)))
        {
            operation.Responses[StatusCodes.Status422UnprocessableEntity.ToString()] = new OpenApiResponse
            {
                Description = "Unprocessable Entity",
                Content =
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties =
                            {
                                ["title"] = new OpenApiSchema { Type = "string" },
                                ["detail"] = new OpenApiSchema { Type = "string" },
                                ["status"] = new OpenApiSchema { Type = "integer", Format = "int32" },
                                ["errors"] = new OpenApiSchema
                                {
                                    Type = "object",
                                    AdditionalProperties = new OpenApiSchema
                                    {
                                        Type = "array",
                                        Items = new OpenApiSchema { Type = "string" }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        return Task.CompletedTask;
    }
}