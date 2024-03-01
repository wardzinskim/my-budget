﻿using MyBudget.Api.Installers.Abstraction;

namespace MyBudget.Api.Installers;

public sealed class HttpContextAccessorInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddHttpContextAccessor();
    }
}