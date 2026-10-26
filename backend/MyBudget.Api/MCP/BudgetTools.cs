using MassTransit;
using MassTransit.Mediator;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using MyBudget.Application.Budgets.GetBudgets;
using MyBudget.Application.Budgets.Model;
using MyBudget.Application.Budgets.Transfers.CreateTransfer;
using MyBudget.Application.Budgets.Transfers.GetTransfers;
using System.ComponentModel;

namespace MyBudget.Api.MCP;

[McpServerToolType]
public class BudgetTools(IMediator mediator)
{
    [McpServerTool(UseStructuredContent = true)]
    [Description("""
                 Retrieves all budgets belonging to the currently authenticated user.
                 Use this tool when the user asks about their budgets, spending limits,
                 or wants an overview of budget categories.
                 Returns a list of budgets including id, name, currency, status and current period.
                 Note: transfers can only be added to budgets with status "Open".
                 Budgets with other statuses (e.g. "Closed") are read-only and will
                 reject transfer creation attempts.
                 Returns an empty list if the user has no budgets
                 """)]
    public async Task<IEnumerable<BudgetDTO>> GetBudgets(CancellationToken cancellationToken)
    {
        var result = await mediator.SendRequest(new GetBudgetsQuery(), cancellationToken);

        return result.Match(x => x, x => throw new McpException(x.Description));
    }

    [McpServerTool(UseStructuredContent = true)]
    [Description("""
                 Retrieves transfers for a given budget, with optional filtering.
                 Use this tool when the user asks to list, search or filter transfers
                 (income, expenses or taxes) within a specific budget — e.g. "show my
                 expenses from last month" or "find transfers named 'rent'".
                 All filter parameters are optional and can be combined.
                 Note: if any of dateFrom and dateTo are omitted, only transfers from the
                 last 30 days are returned by default — not the full history. To get
                 older transfers, explicitly provide dateFrom and/or dateTo.
                 """)]
    public async Task<TransfersQueryResponse> GetTransfers(
        [Description("Id of the budget to retrieve transfers from")]
        Guid budgetId,
        [Description("Optional filter by transfer type (Income, Expense or Tax). If omitted, all types are returned.")]
        TransferDTOType? type = null,
        [Description("Optional filter by transfer name. Performs a partial, case-insensitive match.")]
        string? name = null,
        [Description(
            "Optional filter by transfer category, must match an existing category name in budget.")]
        string? category = null,
        [Description(
            "Optional start date of the period to filter by (inclusive). If omitted along with dateTo, defaults to 30 days ago.")]
        DateTime? dateFrom = null,
        [Description(
            "Optional end date of the period to filter by (inclusive). If omitted along with dateFrom, defaults to today, giving a last-30-days window.")]
        DateTime? dateTo = null,
        CancellationToken cancellationToken = default
    )
    {
        var result = await mediator.SendRequest(
            new GetTransfersQuery(budgetId, type, name, category, dateFrom, dateTo),
            cancellationToken);

        return result.Match(x => x, x => throw new McpException(x.Description));
    }

    [McpServerTool(UseStructuredContent = true)]
    [Description("""
                 Creates a new transfer (income, expense or tax) within the specified budget.
                 Use this tool when the user wants to add, register or log a new transfer.
                 Note: transfers can only be added to budgets with status "Open".
                 """)]
    public async Task<TransferDTO> CreateTransfer(
        [Description("Id of the budget to add the transfer to")]
        Guid budgetId,
        [Description("Type of the transfer: Income, Expense or Tax")]
        TransferDTOType type,
        [Description("Name/title of the transfer")]
        string name,
        [Description("Transfer amount, must be a positive number")]
        decimal value,
        [Description("""
                     Currency code of the transfer. Must match the budget's default 
                     currency — use GetBudgets to check it before calling this tool.
                     """)]
        string currency,
        [Description("""
                     Optional category of the transfer. If provided, must be one of the existing categories defined 
                     in the budget — use GetBudgets or the categories tool to check valid values.
                      If omitted, the transfer is left uncategorized.
                     """)]
        string? category = null,
        [Description(
            "Optional date of the transfer. If omitted, the system automatically assigns the current date and time.")]
        DateTime? date = null,
        CancellationToken cancellationToken = default
    )
    {
        var result = await mediator.SendRequest(
            new CreateTransferCommand(budgetId, type, name, value, currency, category, date),
            cancellationToken);

        return result.Match(x => x, x => throw new McpException(x.Description));
    }
}