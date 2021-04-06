using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using WishMe.Service.Swagger;

namespace WishMe.Service.Configs
{
  public class SwaggerConfig
  {
    private const string _RouteTemplate = "/docs/{documentName}/swagger.json";

    public static void SetupSwagger(SwaggerOptions options)
    {
      options.RouteTemplate = _RouteTemplate;
      options.SerializeAsV2 = true;
    }

    public static void SetupSwaggerGen(SwaggerGenOptions options)
    {
      options.SwaggerDoc("v1", new OpenApiInfo { Title = "WishMe", Version = "v1", Description = "WishMe REST API" });

      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Description = "Example: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer",
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header
          },
          new List<string>()
        }
      });

      options.MapType<DateTime>(() => new OpenApiSchema { Type = "string" });
      options.MapType<DateTime?>(() => new OpenApiSchema { Type = "string" });

      options.OperationFilter<ConsumesProducesAttributeOperationFilter>();
      options.OperationFilter<RouteControllerNameOperationFilter>();

      options.DocumentFilter<SortDocumentFilter>();

      options.DocInclusionPredicate((version, apiDescription) => apiDescription.RelativePath.Contains($"/{version}/"));

      foreach (string xmlFile in Directory.GetFiles(AppContext.BaseDirectory, "*.xml"))
        options.IncludeXmlComments(xmlFile);
    }
  }
}
