﻿using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace MeuProjeto.Api.Extensions
{
    public class FormFileOperationFilter : IOperationFilter
    {
        private const string FormDataMimeType = "multipart/form-data";
        private static readonly string[] FormFilePropertyNames =
            typeof(IFormFile).GetTypeInfo().DeclaredProperties.Select(x => x.Name).ToArray();
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.RelativePath == "api/v1/users/update1")
            {
                //operation.Parameters.Clear();
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "formFile",
                    In = ParameterLocation.Header,
                    Description = "Upload File",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "file",
                        Format = "binary"
                    }
                });
                var uploadFileMediaType = new OpenApiMediaType()
                {
                    Schema = new OpenApiSchema()
                    {
                        Type = "object",
                        Properties =
                    {
                        ["uploadedFile"] = new OpenApiSchema()
                        {
                            Description = "Upload File",
                            Type = "file",
                            Format = "binary"

                        }
                    },
                        Required = new HashSet<string>()
                    {
                        "uploadedFile"
                    }
                    }
                };
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                {
                    ["multipart/form-data"] = uploadFileMediaType
                }
                };
            }
        }
    }
}
