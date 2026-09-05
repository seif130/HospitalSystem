using FluentValidation;
using HospitalSystem.Application.Shared.Common;
using MediatR;

namespace HospitalSystem.WebApi.Common;

public static class EndpointHelper
{
    public static async Task<IResult> SendAsync<TRequest>(
        TRequest request,
        ISender sender,
        IServiceProvider services,
        CancellationToken ct)
        where TRequest : notnull, IRequest<Result>
    {
        var validator = services.GetService<IValidator<TRequest>>();
        if (validator is not null)
        {
            var validation = await validator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return ApiResultExtensions.ValidationErrors(validation.Errors);
        }

        var response = await sender.Send(request, ct);
        return response.ToHttpResult();
    }

    public static async Task<IResult> SendAsync<TRequest, TResponse>(
        TRequest request,
        ISender sender,
        IServiceProvider services,
        CancellationToken ct,
        Func<TResponse, IResult>? onSuccess = null)
        where TRequest : notnull, IRequest<Result<TResponse>>
    {
        var validator = services.GetService<IValidator<TRequest>>();
        if (validator is not null)
        {
            var validation = await validator.ValidateAsync(request, ct);
            if (!validation.IsValid)
                return ApiResultExtensions.ValidationErrors(validation.Errors);
        }

        var response = await sender.Send(request, ct);
        return response.ToHttpResult(onSuccess);
    }
}
