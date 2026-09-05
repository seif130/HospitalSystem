using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.AddPurchaseOrderLineCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.ApprovePurchaseOrderCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.CancelPurchaseOrderCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.CompletePurchaseOrderCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.CreatePurchaseOrderCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Commands.SubmitPurchaseOrderCommand;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrderByIdQuery;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByPurchaseRequestQuery;
using HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByVendorQuery;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.WebApi.Common;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Modules.Procurement.PurchaseOrders;

public static class PurchaseOrderEndpoints
{
    public static IEndpointRouteBuilder MapPurchaseOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/purchase-orders")
            .WithTags("Procurement - Purchase Orders");

        // GET: /api/procurement/purchase-orders/{purchaseOrderId}
        group.MapGet("/{purchaseOrderId:guid}", async (Guid purchaseOrderId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new GetPurchaseOrderByIdQuery(new PurchaseOrderId(purchaseOrderId)), ct);
            return result.ToHttpResult();
        });

        // GET: /api/procurement/purchase-orders/by-vendor/{vendorId}
        group.MapGet("/by-vendor/{vendorId:guid}", async (
            Guid vendorId,
            ISender sender,
            CancellationToken ct,
            int pageNumber = 1,
            int pageSize = 20) =>
        {
            var result = await sender.Send(new GetPurchaseOrdersByVendorQuery(new VendorId(vendorId), pageNumber, pageSize), ct);
            return result.ToHttpResult();
        });

        // GET: /api/procurement/purchase-orders/by-purchase-request/{purchaseRequestId}
        group.MapGet("/by-purchase-request/{purchaseRequestId:guid}", async (
            Guid purchaseRequestId,
            ISender sender,
            CancellationToken ct,
            int pageNumber = 1,
            int pageSize = 20) =>
        {
            var result = await sender.Send(new GetPurchaseOrdersByPurchaseRequestQuery(new PurchaseRequestId(purchaseRequestId), pageNumber, pageSize), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-orders
        group.MapPost("", async (CreatePurchaseOrderRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new CreatePurchaseOrderCommand(
                new VendorId(request.VendorId),
                request.Currency,
                request.PurchaseRequestId.HasValue
                    ? new PurchaseRequestId(request.PurchaseRequestId.Value)
                    : null), ct);

            return result.ToCreatedResult(id => $"/api/procurement/purchase-orders/{id.Value}");
        });

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/lines
        group.MapPost("/{purchaseOrderId:guid}/lines", async (
            Guid purchaseOrderId,
            AddLineRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new AddPurchaseOrderLineCommand(
                new PurchaseOrderId(purchaseOrderId),
                request.ItemName,
                request.Quantity,
                request.UnitPrice,
                request.Currency), ct);

            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/submit
        group.MapPost("/{purchaseOrderId:guid}/submit", async (Guid purchaseOrderId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new SubmitPurchaseOrderCommand(new PurchaseOrderId(purchaseOrderId)), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/approve
        group.MapPost("/{purchaseOrderId:guid}/approve", async (Guid purchaseOrderId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new ApprovePurchaseOrderCommand(new PurchaseOrderId(purchaseOrderId)), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/complete
        group.MapPost("/{purchaseOrderId:guid}/complete", async (Guid purchaseOrderId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new CompletePurchaseOrderCommand(new PurchaseOrderId(purchaseOrderId)), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/cancel
        group.MapPost("/{purchaseOrderId:guid}/cancel", async (Guid purchaseOrderId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new CancelPurchaseOrderCommand(new PurchaseOrderId(purchaseOrderId)), ct);
            return result.ToHttpResult();
        });

        return group;
    }

    public sealed record CreatePurchaseOrderRequest(
        Guid VendorId, string Currency, Guid? PurchaseRequestId);

    public sealed record AddLineRequest(
        string ItemName, int Quantity, decimal UnitPrice, string Currency);
}

