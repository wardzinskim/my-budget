using FluentValidation;
using FluentValidation.Results;
using MassTransit;

namespace MyBudget.Api.Installers.MediatorFilters;

internal class ValidationFilter<TMessage> : IFilter<ConsumeContext<TMessage>>
    where TMessage : class
{
    private readonly IEnumerable<IValidator<TMessage>> validators;

    public ValidationFilter(IEnumerable<IValidator<TMessage>> validators)
    {
        this.validators = validators;
    }

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("validation");

    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        var validationFailures = await ValidateAsync(context.Message);

        if (validationFailures.Length == 0)
        {
            await next.Send(context);
            return;
        }

        throw new ValidationException(validationFailures);
    }

    private async Task<ValidationFailure[]> ValidateAsync(TMessage message)
    {
        if (!validators.Any())
            return [];

        var context = new ValidationContext<TMessage>(message);

        var validationResults = await Task.WhenAll(validators.Select(validator => validator.ValidateAsync(context)));

        return validationResults
            .Where(x => !x.IsValid)
            .SelectMany(x => x.Errors)
            .ToArray();
    }
}