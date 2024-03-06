namespace MyBudget.SharedKernel;

public interface IIdGenerator
{
    Guid NextId();
}