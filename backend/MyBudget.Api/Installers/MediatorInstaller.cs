﻿using MassTransit;
using MassTransit.Internals;
using MyBudget.Api.Installers.MediatorFilters;
using MyBudget.Application.Weather.WeatherQuery;
using MyBudget.Infrastructure.Abstraction.Installer;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.Infrastructure.Application.Budgets;
using MyBudget.SharedKernel;

namespace MyBudget.Api.Installers;

public sealed class MediatorInstaller : IInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration, IHostEnvironment hostingEnvironment)
    {
        services.AddMediator(configure =>
        {
            configure.AddConsumers(typeof(WeatherQuery).Assembly);
            configure.AddConsumers(typeof(GetBudgetsQueryHandler).Assembly);

            configure.ConfigureMediator((context, cfg) =>
            {
                cfg.UseConsumeFilter(typeof(ValidationFilter<>), context);

                cfg.UseConsumeFilter(typeof(UnitOfWorkFilter<>), context,
                    x => x.Include(type => type.HasInterface<ICommand>()));

                cfg.UseSendFilter(typeof(UnitOfWorkResultFilter<>), context,
                    x => x.Include(type => type.IsConcreteAndAssignableTo<Result>()));
            });
        });
    }
}