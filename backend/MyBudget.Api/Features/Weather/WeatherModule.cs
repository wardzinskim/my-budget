using Carter;
using Carter.OpenApi;
using MassTransit;
using MassTransit.Mediator;
using MyBudget.Application.Weather.Model;
using MyBudget.Application.Weather.WeatherQuery;

namespace MyBudget.Api.Features.Weather;

public class WeatherModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast", GetWeatherForecast)
            .WithName("GetWeatherForecast")
            .WithTags("weather")
            .Produces<WeatherForecast[]>()
            .WithOpenApi()
            .IncludeInOpenApi();
    }

    private static async Task<IResult> GetWeatherForecast(
        IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.SendRequest(new WeatherQuery(), cancellationToken);

        return Results.Ok(result);
    }

}