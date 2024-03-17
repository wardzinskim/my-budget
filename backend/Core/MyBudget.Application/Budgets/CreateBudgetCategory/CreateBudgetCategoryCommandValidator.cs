using FluentValidation;

namespace MyBudget.Application.Budgets.CreateBudgetCategory;

public class CreateBudgetCategoryCommandValidator : AbstractValidator<CreateBudgetCategoryCommand>
{
    public CreateBudgetCategoryCommandValidator()
    {
        RuleFor(x => x.BudgetId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(32);
    }
}