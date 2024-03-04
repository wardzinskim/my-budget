using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyBudget.Api.Installers.ExceptionHandlers;

internal sealed class ValidationExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken
    )
    {
        if (exception is not ValidationException validationException)
            return false;


        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        var validationProblemDetails = new HttpValidationProblemDetails(validationException.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(x => x.ErrorMessage).Distinct().ToArray()))
        {
            Title = "Validation Errors", Type = nameof(ValidationException)
        };


        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext, Exception = exception, ProblemDetails = validationProblemDetails,
        });
    }
}