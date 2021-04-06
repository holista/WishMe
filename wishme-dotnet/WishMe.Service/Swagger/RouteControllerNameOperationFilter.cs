using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WishMe.Service.Swagger
{
  public class RouteControllerNameOperationFilter: IOperationFilter
  {
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
      var routeAttribute = context
        .MethodInfo
        .DeclaringType?
        .GetCustomAttribute<RouteAttribute>();

      if (routeAttribute is null)
        return;

      operation.Tags = new List<OpenApiTag> { new() { Name = routeAttribute.Name } };
    }
  }
}
