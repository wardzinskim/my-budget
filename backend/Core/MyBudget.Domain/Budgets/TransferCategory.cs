﻿using MyBudget.SharedKernel;

namespace MyBudget.Domain.Budgets;

public record TransferCategory : ValueObject
{
    private TransferCategory(string name)
    {
        Name = name;
        Status = TransferCategoryStatus.Active;
    }

    public string Name { get; init; }
    public TransferCategoryStatus Status { get; private set; }

    internal void Archive()
    {
        Status = TransferCategoryStatus.Archived;
    }

    internal static TransferCategory Create(string name)
    {
        return new TransferCategory(name);
    }
}