using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WishMe.Service.Swagger
{
    public class RemoveObjectIdOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var addedParams = new HashSet<string>();

            context
                .ApiDescription
                .ParameterDescriptions
                .Where(x => x.ModelMetadata != null)
                .Where(x => x.ModelMetadata.ContainerType == typeof(ObjectId) || x.ModelMetadata.ContainerType == typeof(ObjectId?))
                .ToList()
                .ForEach(x =>
                {
                    TryAddObjectIdQuery(operation, x, addedParams);

                    if (operation.Parameters == null)
                        return;

                    var objectIdParameters = operation.Parameters.Where(y => y.Name == x.Name).ToList();

                    foreach (var objectIdParameter in objectIdParameters)
                        operation.Parameters.Remove(objectIdParameter);
                });
        }

        private static void TryAddObjectIdQuery(OpenApiOperation operation, ApiParameterDescription description, HashSet<string> addedParams)
        {
            if (description.ParameterDescriptor.BindingInfo.BindingSource?.Id == BindingSource.Query.Id)
            {
                string paramName;

                if (description.Name.Contains('.'))
                {
                    string[] parts = description.Name.Split('.');
                    paramName = parts[0];
                }
                else
                    paramName = description.ParameterDescriptor.Name;

                if (addedParams.Add(paramName))
                    AddObjectIdQuery(operation, paramName);
            }
        }

        private static void AddObjectIdQuery(OpenApiOperation operation, string name)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                In = ParameterLocation.Query,
                Schema = new OpenApiSchema { Type = nameof(String).ToLower() },
                Name = name
            });
        }
    }
}
