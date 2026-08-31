using HospitalSystem.Application.Shared.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.WebApi
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResult(this Result result)
        {
            return result.IsSuccess? Results.NoContent(): ToProblem(result.Error);
        }

        public static IResult ToHttpResult<TValue>(this Result<TValue> result)
        {
            return result.IsSuccess? Results.Ok(result.Value): ToProblem(result.Error);
        }

        public static IResult ToCreatedResult<TValue>(this Result<TValue> result,Func<TValue, string> locationFactory)
        {
            return result.IsSuccess ? Results.Created(
                    locationFactory(result.Value),result.Value): ToProblem(result.Error);
        }

        private static IResult ToProblem(Error error)
        {
            return error.Type switch
            {
                ErrorType.NotFound =>
                    Results.Problem(title: error.Code,detail: error.Message,
                        statusCode: StatusCodes.Status404NotFound),

                ErrorType.Validation =>
                    Results.Problem( title: error.Code,detail: error.Message,
                        statusCode: StatusCodes.Status400BadRequest),

                ErrorType.Conflict =>
                    Results.Problem(
                        title: error.Code,detail: error.Message,
                        statusCode: StatusCodes.Status409Conflict),

                ErrorType.Unauthorized =>
                    Results.Problem(
                        title: error.Code, detail: error.Message,
                        statusCode: StatusCodes.Status401Unauthorized),

                ErrorType.Failure =>
                    Results.Problem(
                        title: error.Code, detail: error.Message,
                        statusCode: StatusCodes.Status500InternalServerError),

                _ =>
                    Results.Problem(
                        title: error.Code,detail: error.Message,
                        statusCode: StatusCodes.Status500InternalServerError)
            };
        }
    }
}
