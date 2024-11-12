using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBudget.Domain.Budgets;
using MyBudget.Domain.Budgets.Transfers;
using MyBudget.Domain.Shared;

namespace MyBudget.Infrastructure.Database.Conversions;

//TODO: not validate values from db

public class BudgetIdConverter()
    : ValueConverter<BudgetId, Guid>(id => id.Value, value => BudgetId.Of(value).Value);

public class TransferIdConverter()
    : ValueConverter<TransferId, Guid>(id => id.Value, value => TransferId.Of(value).Value);

public class UserIdConverter()
    : ValueConverter<UserId, Guid>(id => id.Value, value => UserId.Of(value).Value);