﻿using Carter.OpenApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MyBudget.Infrastructure.Abstractions.Installer;
using System.Reflection;
using System.Text.Json.Serialization;

namespace MyBudget.Api.Installers;

public sealed class SwaggerInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo {Version = "v1", Title = "MyBudget API",});

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));


            options.DocInclusionPredicate((s, description) =>
            {
                foreach (var metaData in description.ActionDescriptor.EndpointMetadata)
                {
                    if (metaData is IIncludeOpenApi)
                    {
                        return true;
                    }
                }

                return false;
            });
        });

        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }

    public void Use(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}