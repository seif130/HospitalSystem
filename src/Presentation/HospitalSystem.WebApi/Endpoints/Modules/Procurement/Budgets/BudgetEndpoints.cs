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
    public static IEndpointRouteBuilder MapBudgetEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/hospitalsystem/procurement/budgets")
            .WithTags("Procurement - Budgets");

        group.MapGet("/{budgetId:guid}", async (Guid budgetId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new GetBudgetByIdQuery(new BudgetId(budgetId)), ct);
            return result.ToHttpResult();
        });

        group.MapGet("/by-department/{departmentId:guid}", async (
            Guid departmentId,
            ISender sender,
            CancellationToken ct,
            int pageNumber = 1,
            int pageSize = 20) =>
        {
            var result = await sender.Send(
                new GetBudgetsByDepartmentQuery(new DepartmentId(departmentId), pageNumber, pageSize), ct);
            return result.ToHttpResult();
        });

        group.MapPost("", async (AllocateBudgetRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(
                new AllocateBudgetCommand(
                    new DepartmentId(request.DepartmentId),
                    request.FiscalStart,
                    request.FiscalEnd,
                    request.Amount,
                    request.Currency), ct);

            return result.ToCreatedResult(id => $"/api/hospitalsystem/procurement/budgets/{id.Value}");
        });

        group.MapPost("/{budgetId:guid}/expenses", async (
            Guid budgetId,
            RecordExpenseRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(
                new RecordBudgetExpenseCommand(
                    new BudgetId(budgetId),
                    request.Description,
                    request.Amount,
                    request.Currency,
                    request.IncurredOnUtc), ct);

            return result.ToHttpResult();
        });

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
