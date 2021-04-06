using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WishMe.Service.Swagger
{
  public class ConsumesProducesAttributeOperationFilter: IOperationFilter
  {
    private const string _JsonMediaType = "application/json";

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      foreach (var response in operation.Responses.Values)
      {
        if (response.Content.TryGetValue(_JsonMediaType, out var responseMediaType))
          response.Content = new Dictionary<string, OpenApiMediaType> { { _JsonMediaType, responseMediaType } };
        else
          response.Content = new Dictionary<string, OpenApiMediaType> { { _JsonMediaType, new OpenApiMediaType() } };
      };

      if (operation.RequestBody is null)
        return;

      if (operation.RequestBody.Content.TryGetValue(_JsonMediaType, out var requestMediaType))
        operation.RequestBody.Content = new Dictionary<string, OpenApiMediaType> { { _JsonMediaType, requestMediaType } };
      else
        operation.RequestBody.Content = new Dictionary<string, OpenApiMediaType> { { _JsonMediaType, new OpenApiMediaType() } };
    }
  }
}
