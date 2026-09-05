using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Budgets.Commands;
using HospitalSystem.Application.Modules.Procurement.Budgets.Commands.AllocateBudgetCommand;
using HospitalSystem.Application.Modules.Procurement.Budgets.Commands.RecordBudgetExpenseCommand;
using HospitalSystem.Application.Modules.Procurement.Budgets.Queries.GetBudgetByIdQuery;
using HospitalSystem.Application.Modules.Procurement.Budgets.Queries.GetBudgetsByDepartmentQuery;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.WebApi.Common;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Modules.Procurement.Budgets;

public static class BudgetEndpoints
{
    public static RouteGroupBuilder MapBudgetEndpoints(
     this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/hospitalsystem/procurement/budgets")
            .WithTags("Procurement - Budgets");

        group.MapGet(
            "/{budgetId:guid}",
            async (
                Guid budgetId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync<
                    GetBudgetByIdQuery,
                    BudgetDto>(
                    new GetBudgetByIdQuery(
                        new BudgetId(budgetId)),
                    sender,
                    services,
                    cancellationToken));

        group.MapGet(
            "/by-department/{departmentId:guid}",
            async (
                Guid departmentId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken,
                int pageNumber = 1,
                int pageSize = 20) =>
                EndpointHelper.SendAsync<
                    GetBudgetsByDepartmentQuery,
                    PaginatedList<BudgetDto>>(
                    new GetBudgetsByDepartmentQuery(
                        new DepartmentId(departmentId),
                        pageNumber,
                        pageSize),
                    sender,
                    services,
                    cancellationToken));

        group.MapPost(
            "",
            async (
                AllocateBudgetRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync<
                    AllocateBudgetCommand,
                    BudgetId>(
                    new AllocateBudgetCommand(
                        new DepartmentId(request.DepartmentId),
                        request.FiscalStart,
                        request.FiscalEnd,
                        request.Amount,
                        request.Currency),
                    sender,
                    services,
                    cancellationToken,
                    id => TypedResults.Created(
                        $"/api/hospitalsystem/procurement/budgets/{id.Value}",
                        id.Value)));

        group.MapPost(
            "/{budgetId:guid}/expenses",
            async (
                Guid budgetId,
                RecordExpenseRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new RecordBudgetExpenseCommand(
                        new BudgetId(budgetId),
                        request.Description,
                        request.Amount,
                        request.Currency,
                        request.IncurredOnUtc),
                    sender,
                    services,
                    cancellationToken));

        return group;
    }

    public sealed record AllocateBudgetRequest(
        Guid DepartmentId,
        DateTime FiscalStart,
        DateTime FiscalEnd,
        decimal Amount,
        string Currency);

    public sealed record RecordExpenseRequest(
        string Description,
        decimal Amount,
        string Currency,
        DateTime IncurredOnUtc);
}
