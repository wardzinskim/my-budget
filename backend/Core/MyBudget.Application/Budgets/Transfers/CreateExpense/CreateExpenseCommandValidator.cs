using FluentValidation;

namespace MyBudget.Application.Budgets.Transfers.CreateExpense;

public class CreateExpenseCommandValidator : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseCommandValidator()
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
    }
}