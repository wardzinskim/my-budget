using Microsoft.AspNetCore.Mvc;
using MyBudget.Application.Budgets.Model;

namespace MyBudget.Api.Features.Core;

public record CreateBudgetRequest(string Name, string? Description = null);

public record CreateBudgetCategoryRequest(string Name);

public record CreateTransferRequest(
    TransferDTOType Type,
    string Name,
    decimal Value,
    string Currency,
    string? Category = null,
    DateTime? Date = null
);

public record UpdateTransferRequest(
    string Name,
    decimal Value,
    string Currency,
    DateTime Date,
    string? Category = null
);

public record GetTransfersRequest(
    [FromRoute] Guid Id,
    [FromQuery(Name = "type")] TransferDTOType? Type,
    [FromQuery(Name = "dateFrom")] DateTime? DateFrom,
    [FromQuery(Name = "dateTo")] DateTime? DateTo
);

public record ShareBudgetRequest(string Login);