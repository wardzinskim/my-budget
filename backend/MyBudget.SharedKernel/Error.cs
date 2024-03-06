namespace MyBudget.SharedKernel;

public record Error(string Code, string Description)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}

public record NotFoundError(string Code, string Description) : Error(Code, Description);

public record BadRequestError(string Code, string Description) : Error(Code, Description);

public record BusinessRuleValidationError(string Code, string Description) : Error(Code, Description);