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
    public static RouteGroupBuilder MapPurchaseOrderEndpoints(
     this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/purchase-orders")
            .WithTags("Procurement - Purchase Orders");

        // GET: /api/procurement/purchase-orders/{purchaseOrderId}
        group.MapGet(
            "/{purchaseOrderId:guid}",
            async (
                Guid purchaseOrderId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync<
                    GetPurchaseOrderByIdQuery,
                    PurchaseOrderDto>(
                    new GetPurchaseOrderByIdQuery(
                        new PurchaseOrderId(purchaseOrderId)),
                    sender,
                    services,
                    cancellationToken));

        // GET: /api/procurement/purchase-orders/by-vendor/{vendorId}
        group.MapGet(
            "/by-vendor/{vendorId:guid}",
            async (
                Guid vendorId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken,
                int pageNumber = 1,
                int pageSize = 20) =>
                EndpointHelper.SendAsync<
                    GetPurchaseOrdersByVendorQuery,
                    PaginatedList<PurchaseOrderDto>>(
                    new GetPurchaseOrdersByVendorQuery(
                        new VendorId(vendorId),
                        pageNumber,
                        pageSize),
                    sender,
                    services,
                    cancellationToken));

        // GET: /api/procurement/purchase-orders/by-purchase-request/{purchaseRequestId}
        group.MapGet(
            "/by-purchase-request/{purchaseRequestId:guid}",
            async (
                Guid purchaseRequestId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken,
                int pageNumber = 1,
                int pageSize = 20) =>
                EndpointHelper.SendAsync<
                    GetPurchaseOrdersByPurchaseRequestQuery,
                    PaginatedList<PurchaseOrderDto>>(
                    new GetPurchaseOrdersByPurchaseRequestQuery(
                        new PurchaseRequestId(purchaseRequestId),
                        pageNumber,
                        pageSize),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/purchase-orders
        group.MapPost(
            "",
            async (
                CreatePurchaseOrderRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync<
                    CreatePurchaseOrderCommand,
                    PurchaseOrderId>(
                    new CreatePurchaseOrderCommand(
                        new VendorId(request.VendorId),
                        request.Currency,
                        request.PurchaseRequestId.HasValue
                            ? new PurchaseRequestId(
                                request.PurchaseRequestId.Value)
                            : null),
                    sender,
                    services,
                    cancellationToken,
                    id => TypedResults.Created(
                        $"/api/procurement/purchase-orders/{id.Value}",
                        id.Value)));

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/lines
        group.MapPost(
            "/{purchaseOrderId:guid}/lines",
            async (
                Guid purchaseOrderId,
                AddLineRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new AddPurchaseOrderLineCommand(
                        new PurchaseOrderId(purchaseOrderId),
                        request.ItemName,
                        request.Quantity,
                        request.UnitPrice,
                        request.Currency),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/submit
        group.MapPost(
            "/{purchaseOrderId:guid}/submit",
            async (
                Guid purchaseOrderId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new SubmitPurchaseOrderCommand(
                        new PurchaseOrderId(purchaseOrderId)),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/approve
        group.MapPost(
            "/{purchaseOrderId:guid}/approve",
            async (
                Guid purchaseOrderId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new ApprovePurchaseOrderCommand(
                        new PurchaseOrderId(purchaseOrderId)),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/complete
        group.MapPost(
            "/{purchaseOrderId:guid}/complete",
            async (
                Guid purchaseOrderId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new CompletePurchaseOrderCommand(
                        new PurchaseOrderId(purchaseOrderId)),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/purchase-orders/{purchaseOrderId}/cancel
        group.MapPost(
            "/{purchaseOrderId:guid}/cancel",
            async (
                Guid purchaseOrderId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new CancelPurchaseOrderCommand(
                        new PurchaseOrderId(purchaseOrderId)),
                    sender,
                    services,
                    cancellationToken));

        return group;
    }

    public sealed record CreatePurchaseOrderRequest(
        Guid VendorId, string Currency,Guid? PurchaseRequestId);

    public sealed record AddLineRequest(
        string ItemName,int Quantity,decimal UnitPrice,string Currency);
}

