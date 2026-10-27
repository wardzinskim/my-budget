---
name: add-mcp-tool
description: 'Expose a myBudget operation as an MCP tool in BudgetTools.cs with LLM-oriented descriptions and error mapping via McpException. Use when asked to "add MCP tool", "dodaj narzędzie MCP", "expose to AI assistant", "model context protocol".'
allowed-tools: shell
---

# add-mcp-tool

Add a `[McpServerTool]` method that delegates to an existing command/query through the mediator.

## When to Use This Skill

- User says "add MCP tool for X", "dodaj narzędzie MCP"
- An existing feature should be callable by an AI assistant
- Keywords: MCP, McpServerTool, ModelContextProtocol, BudgetTools

## Steps

### 1. Ensure the feature exists

Use an existing Application command/query (skill `add-api-feature` if missing). MCP tools never contain business logic.

### 2. Add the tool

In `backend/MyBudget.Api/MCP/BudgetTools.cs`:

```csharp
[McpServerTool(UseStructuredContent = true)]
[Description("""
             What it does, WHEN the assistant should use it, constraints (e.g. budget must be "Open"),
             defaults when optional args are omitted, and what it returns.
             """)]
public async Task<XDto> DoSomething(
    [Description("Id of the budget")] Guid budgetId,
    [Description("Meaning, allowed values, how to discover them (e.g. call GetBudgets)")] string name,
    [Description("Optional ... If omitted ...")] string? optional = null,
    CancellationToken cancellationToken = default
)
{
    var result = await mediator.SendRequest(new XCommand(budgetId, name, optional), cancellationToken);

    return result.Match(x => x, x => throw new McpException(x.Description));
}
```

For a command returning plain `Result` (no value), return `Task` and use `if (result.IsFailure) throw new McpException(result.Error.Description);`.

### 3. Verify

```bash
cd backend
dotnet build
```

Run the API and call the tool from an MCP client (auth via Bearer token).

## Requirements

- Tools live in the `[McpServerToolType]` class `BudgetTools`; inject only `IMediator`.
- Every tool and every parameter has a `[Description]` written for an LLM (when to use, defaults, valid values, cross-tool hints).
- Return DTOs from `Application/Budgets/Model`, not domain entities.
- Errors → `McpException(error.Description)`; do not leak stack traces.
- Do NOT bypass access checks; they run in the handlers.

## Good Example

`GetTransfers` / `CreateTransfer` in `BudgetTools.cs`.

## What to Avoid

- Terse descriptions ("Gets transfers").
- Calling repositories or `BudgetContext` directly from the tool.

## Error handling

- Tool not discovered → check `MCPInstaller` registers the assembly/type and the class has `[McpServerToolType]`.
