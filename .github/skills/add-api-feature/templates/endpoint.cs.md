# Endpoint template (Carter module)

Request (`Features/Core/Requests.cs`):

```csharp
public record <Feature>Request(string Name);
```

Route (inside `AddRoutes`):

```csharp
group.MapPut("{transferId:guid}", <Feature>)
    .WithName(nameof(<Feature>))
    .Produces(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .ProducesProblem(StatusCodes.Status403Forbidden)
    .ProducesValidationProblem();
```

Handler:

```csharp
private static async Task<IResult> <Feature>(
    IMediator mediator,
    [FromRoute] Guid id,
    [FromBody] <Feature>Request request,
    CancellationToken cancellationToken
)
{
    var result = await mediator.SendRequest(new <Feature>Command(id, request.Name), cancellationToken);

    return result.Match(() => Results.Ok());   // queries: result.Match(Results.Ok); creation: result.Match(Results.Created)
}
```

New module: `public class <X>Module : ICarterModule` with
`app.MapGroup("/budget/{id:guid}/...").WithTags("...").IncludeInOpenApi().RequireAuthorization()`.
