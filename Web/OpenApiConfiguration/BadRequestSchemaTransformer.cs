using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace Web.OpenApiConfiguration;

public class BadRequestSchemaTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}