using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.AddPurchaseRequestLineCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.ApprovePurchaseRequestCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.CancelPurchaseRequestCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.CreatePurchaseRequestCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.RejectPurchaseRequestCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Commands.SubmitPurchaseRequestCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestByIdQuery;
using HospitalSystem.Application.Modules.Procurement.PurchaseRequests.Queries.GetPurchaseRequestsByDepartmentQuery;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.WebApi.Common;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Modules.Procurement.PurchaseRequests;

public static class PurchaseRequestEndpoints
{
    public static RouteGroupBuilder MapPurchaseRequestEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/purchase-requests")
            .WithTags("Procurement - Purchase Requests");

        // GET: /api/procurement/purchase-requests/{purchaseRequestId}
        group.MapGet("/{purchaseRequestId:guid}", async (Guid purchaseRequestId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new GetPurchaseRequestByIdQuery(new PurchaseRequestId(purchaseRequestId)), ct);
            return result.ToHttpResult();
        });

        // GET: /api/procurement/purchase-requests/by-department/{departmentId}
        group.MapGet("/by-department/{departmentId:guid}", async (
            Guid departmentId,
            ISender sender,
            CancellationToken ct,
            int pageNumber = 1,
            int pageSize = 20) =>
        {
            var result = await sender.Send(new GetPurchaseRequestsByDepartmentQuery(new DepartmentId(departmentId), pageNumber, pageSize), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-requests
        group.MapPost("", async (CreatePurchaseRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new CreatePurchaseRequestCommand(
                new DepartmentId(request.DepartmentId),
                request.Reason), ct);

            return result.ToCreatedResult(id => $"/api/procurement/purchase-requests/{id.Value}");
        });

        // POST: /api/procurement/purchase-requests/{purchaseRequestId}/lines
        group.MapPost("/{purchaseRequestId:guid}/lines", async (
            Guid purchaseRequestId,
            AddLineRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new AddPurchaseRequestLineCommand(
                new PurchaseRequestId(purchaseRequestId),
                request.ItemName,
                request.Quantity,
                request.EstimatedUnitPrice,
                request.Currency), ct);

            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-requests/{purchaseRequestId}/submit
        group.MapPost("/{purchaseRequestId:guid}/submit", async (Guid purchaseRequestId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new SubmitPurchaseRequestCommand(new PurchaseRequestId(purchaseRequestId)), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-requests/{purchaseRequestId}/approve
        group.MapPost("/{purchaseRequestId:guid}/approve", async (Guid purchaseRequestId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new ApprovePurchaseRequestCommand(new PurchaseRequestId(purchaseRequestId)), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-requests/{purchaseRequestId}/reject
        group.MapPost("/{purchaseRequestId:guid}/reject", async (
            Guid purchaseRequestId,
            RejectRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new RejectPurchaseRequestCommand(
                new PurchaseRequestId(purchaseRequestId),
                request.Reason), ct);

            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-requests/{purchaseRequestId}/cancel
        group.MapPost("/{purchaseRequestId:guid}/cancel", async (Guid purchaseRequestId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new CancelPurchaseRequestCommand(new PurchaseRequestId(purchaseRequestId)), ct);
            return result.ToHttpResult();
        });

        return group;
    }

    public sealed record CreatePurchaseRequest(
        Guid DepartmentId,
        string Reason);

    public sealed record AddLineRequest(
        string ItemName,
        int Quantity,
        decimal EstimatedUnitPrice,
        string Currency);

    public sealed record RejectRequest(
        string Reason);
}