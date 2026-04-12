using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Swagger;

public class RemoveVersionFromParametersFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null || operation.Parameters.Count == 0)
            return;

        var versionParameter = operation.Parameters.FirstOrDefault(p => p.Name == "version");
        if (versionParameter != null)
            operation.Parameters.Remove(versionParameter);
    }
}

public class RemoveVersionFromPathsFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var pathsToRemove = swaggerDoc.Paths
            .Where(p => p.Key.Contains("{version}"))
            .Select(p => p.Key)
            .ToList();

        foreach (var path in pathsToRemove)
        {
            var newPath = path.Replace("/{version}", "").Replace("{version}/", "");
            if (!swaggerDoc.Paths.ContainsKey(newPath))
            {
                swaggerDoc.Paths.Add(newPath, swaggerDoc.Paths[path]);
            }
            swaggerDoc.Paths.Remove(path);
        }
    }
}