using FluentValidation;

namespace MyBudget.Application.Budgets.Transfers.UpdateTransfer;

public class UpdateTransferCommandValidator : AbstractValidator<UpdateTransferCommand>
{
    public UpdateTransferCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(128);

        RuleFor(x => x.Currency)
            .NotEmpty()
            .MaximumLength(8);

        RuleFor(x => x.Value)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.BudgetId)
            .NotEmpty();

        RuleFor(x => x.TransferId)
            .NotEmpty();
    }
}