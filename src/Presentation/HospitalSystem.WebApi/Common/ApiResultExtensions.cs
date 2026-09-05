using HospitalSystem.Application.Abstractions.Messaging;
using HospitalSystem.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HospitalSystem.WebApi.Common;

public static class ApiResultExtensions
{
    public static IResult ToHttpResult(this Result result)
    {
        if (result.IsSuccess)
            return TypedResults.NoContent();

        return Failure(result.Error!);
    }

    public static IResult ToHttpResult<T>(this Result<T> result, Func<T, IResult>? onSuccess = null)
    {
        if (result.IsSuccess)
            return onSuccess?.Invoke(result.Value!) ?? TypedResults.Ok(result.Value);

        return Failure(result.Error!);
    }

    public static IResult Created<T>(this Result<T> result, string location)
    {
        if (result.IsFailure)
            return Failure(result.Error!);

        return TypedResults.Created(location, result.Value);
    }

    public static IResult ValidationErrors(IEnumerable<FluentValidation.Results.ValidationFailure> failures) =>
        TypedResults.ValidationProblem(
            failures.GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).Distinct().ToArray()));

    private static IResult Failure(Error error)
    {
        var statusCode = error.Code switch
        {
            var code when code.EndsWith(".NotFound", StringComparison.Ordinal) => StatusCodes.Status404NotFound,
            var code when code.Contains("Duplicate", StringComparison.OrdinalIgnoreCase) => StatusCodes.Status409Conflict,
            var code when code.Contains("Overlap", StringComparison.OrdinalIgnoreCase) => StatusCodes.Status409Conflict,
            var code when code.Contains("Inactive", StringComparison.OrdinalIgnoreCase) => StatusCodes.Status409Conflict,
            var code when code.Contains("NotApproved", StringComparison.OrdinalIgnoreCase) => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status400BadRequest
        };

        return TypedResults.Problem(
            statusCode: statusCode,
            title: statusCode switch
            {
                StatusCodes.Status404NotFound => "Resource not found",
                StatusCodes.Status409Conflict => "Business rule conflict",
                _ => "Request failed"
            },
            detail: error.Message,
            extensions: new Dictionary<string, object?> { ["code"] = error.Code });
    }
}
