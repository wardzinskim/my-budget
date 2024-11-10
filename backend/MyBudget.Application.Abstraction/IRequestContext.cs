namespace MyBudget.Application.Abstraction;

public interface IRequestContext
{
    Guid UserId { get; }
}