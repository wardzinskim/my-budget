using FluentValidation;

namespace MyBudget.Application.Budgets.ArchiveBudgetCategory;

public class ArchiveBudgetCategoryCommandValidator : AbstractValidator<ArchiveBudgetCategoryCommand>
{
    public ArchiveBudgetCategoryCommandValidator()
    {
        RuleFor(x => x.BudgetId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(32);
    }
}