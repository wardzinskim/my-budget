using Humanizer;
using Microsoft.AspNetCore.Mvc;
using MyBudget.SharedKernel;

namespace MyBudget.Api.Extensions;

public static class ResultExtensions
{
    public static IResult Match<TValue>(this Result<TValue> result, Func<TValue, IResult> success)
        => result.Match(success, x => Results.Problem(x.ToProblemDetail()));

    public static IResult Match(this Result result, Func<IResult> success)
        => result.Match(success, x => Results.Problem(x.ToProblemDetail()));

    private static ProblemDetails ToProblemDetail(this Error error)
    {
        return new ProblemDetails
        {
            Detail = error.Description,
            Status = error switch
            {
                NotFoundError => StatusCodes.Status404NotFound,
                BadRequestError => StatusCodes.Status400BadRequest,
                BusinessRuleValidationError => StatusCodes.Status400BadRequest,
                ForbiddenError => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            },
            Extensions = {["code"] = error.Code.Underscore()}
        };
    }
}