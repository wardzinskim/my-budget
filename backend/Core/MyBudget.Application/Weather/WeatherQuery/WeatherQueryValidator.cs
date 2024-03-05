using FluentValidation;

namespace MyBudget.Application.Weather.WeatherQuery;

public class WeatherQueryValidator : AbstractValidator<WeatherQuery>
{
    public WeatherQueryValidator()
    {
        RuleFor(x => x.Example)
            .NotEmpty()
            .MustAsync((value, ct) => Task.FromResult(true));
    }
}

public class WeatherQueryValidator1 : AbstractValidator<WeatherQuery>
{
    public WeatherQueryValidator1()
    {
        RuleFor(x => x.Example)
            .NotNull()
            .MustAsync((value, ct) => Task.FromResult(true));
    }
}