using System;
using System.Globalization;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WishMe.Service.Swagger
{
  public class SortDocumentFilter: IDocumentFilter
  {
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
      var culture = new CultureInfo("cs-CZ");

      var paths = new OpenApiPaths();
      var oldPaths = swaggerDoc
        .Paths
        .OrderBy(e => GetDefinitionFilterString(e.Value), StringComparer.Create(culture, false));

      foreach (var path in oldPaths)
        paths.Add(path.Key, path.Value);

      swaggerDoc.Paths = paths;
    }

    private static string GetDefinitionFilterString(OpenApiPathItem value)
    {
      string tag = string.Empty;

      if (value.Operations.TryGetValue(OperationType.Get, out var getOperation))
        tag = getOperation!.Tags[0].Name;
      if (value.Operations.TryGetValue(OperationType.Post, out var postOperation))
        tag = postOperation!.Tags[0].Name;
      if (value.Operations.TryGetValue(OperationType.Put, out var putOperation))
        tag = putOperation!.Tags[0].Name;
      if (value.Operations.TryGetValue(OperationType.Delete, out var deleteOperation))
        tag = deleteOperation!.Tags[0].Name;

      return string.IsNullOrWhiteSpace(tag)
        ? value.ToString()!
        : tag;
    }
  }
}
