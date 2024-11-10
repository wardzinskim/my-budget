using MassTransit;
using MyBudget.Application.Abstraction;
using MyBudget.Infrastructure.Abstractions.Features;
using MyBudget.SharedKernel;

namespace MyBudget.Api.Installers.MediatorFilters;

internal delegate void ReturnedResult(Result result);

internal sealed class UnitOfWorkFilter<TMessage>(IUnitOfWork unitOfWork, ILogger<UnitOfWorkFilter<TMessage>> logger)
    : IFilter<ConsumeContext<TMessage>> where TMessage : class, ICommand
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger _logger = logger;

    public void Probe(ProbeContext context)
        => context.CreateFilterScope("UnitOfWork");

    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        _logger.LogDebug("Create UnitOfWork filter scope");
        Result? result = null;

        ReturnedResult action = (x) => result = x;

        context.AddOrUpdatePayload(() => action, (x) => action);

        await next.Send(context);

        if (result is null || result.IsSuccess)
        {
            _logger.LogDebug("Perform IUnitOfWork.CommitAsync");
            await _unitOfWork.CommitAsync(context.CancellationToken);
        }
    }
}

internal sealed class UnitOfWorkResultFilter<TResult> : IFilter<SendContext<TResult>>
    where TResult : Result
{
    public void Probe(ProbeContext context)
        => context.CreateFilterScope("UnitOfWork");

    public async Task Send(SendContext<TResult> context, IPipe<SendContext<TResult>> next)
    {
        if (context.TryGetPayload(out ConsumeContext<ICommand>? consumeContext) &&
            consumeContext.TryGetPayload<ReturnedResult>(out var action))
        {
            action(context.Message);
        }

        await next.Send(context);
    }
}