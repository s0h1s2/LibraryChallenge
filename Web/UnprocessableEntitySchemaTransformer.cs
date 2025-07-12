using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Web;

public class UnprocessableEntityOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        operation.Responses.TryAdd(StatusCodes.Status422UnprocessableEntity.ToString(), new OpenApiResponse
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
        });
        return Task.CompletedTask;
    }
}