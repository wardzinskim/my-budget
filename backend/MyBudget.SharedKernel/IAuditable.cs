namespace MyBudget.SharedKernel;
public interface IAuditable
{
    DateTime CreationDate { get; }
    DateTime? LastUpdated { get; }
}
