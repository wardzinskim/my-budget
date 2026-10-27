# Validator template

```csharp
using FluentValidation;

namespace MyBudget.Application.Budgets.<Feature>;

public class <Feature>CommandValidator : AbstractValidator<<Feature>Command>
{
    public <Feature>CommandValidator()
    {
        RuleFor(x => x.BudgetId).NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(32);
    }
}
```

Shape validation only; invariants go to business rules (skill `add-business-rule`).
