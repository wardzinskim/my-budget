using MassTransit.Mediator;
using MyBudget.Application.Weather.Model;

namespace MyBudget.Application.Weather.WeatherQuery;

public record WeatherQuery(string? Example = null) : Request<WeatherForecast[]>;

public class WeatherQueryHandler : MediatorRequestHandler<WeatherQuery, WeatherForecast[]>
{
    protected override Task<WeatherForecast[]> Handle(WeatherQuery request, CancellationToken cancellationToken)
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
            .ToArray();

        return Task.FromResult(forecast);
    }
}