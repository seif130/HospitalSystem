using HospitalSystem.Application.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.WebApi
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResult(this Result result) =>
            result.IsSuccess ? Results.NoContent() : ToProblem(result.Error);

        public static IResult ToHttpResult<TValue>(this Result<TValue> result) =>
            result.IsSuccess ? Results.Ok(result.Value) : ToProblem(result.Error);
        public static IResult ToCreatedResult<TValue>(this Result<TValue> result, Func<TValue, string> locationFactory) =>
            result.IsSuccess ? Results.Created(locationFactory(result.Value), result.Value) : ToProblem(result.Error);

        private static IResult ToProblem(Error error) => error.Type switch
        {
            ErrorType.NotFound => Results.Problem(title: error.Code, detail: error.Message, statusCode: StatusCodes.Status404NotFound),
            ErrorType.Validation => Results.Problem(title: error.Code, detail: error.Message, statusCode: StatusCodes.Status400BadRequest),
            ErrorType.Conflict => Results.Problem(title: error.Code, detail: error.Message, statusCode: StatusCodes.Status409Conflict),
            ErrorType.Unauthorized => Results.Problem(title: error.Code, detail: error.Message, statusCode: StatusCodes.Status401Unauthorized),
            _ => Results.Problem(title: error.Code, detail: error.Message, statusCode: StatusCodes.Status500InternalServerError),
        };
    }
}
