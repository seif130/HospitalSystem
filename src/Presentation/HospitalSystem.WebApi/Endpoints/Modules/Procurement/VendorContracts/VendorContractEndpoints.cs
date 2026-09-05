using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.ActivateVendorContractCommand;
using HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.CreateVendorContractCommand;
using HospitalSystem.Application.Modules.Procurement.VendorContracts.Commands.TerminateVendorContractCommand;
using HospitalSystem.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractByIdQuery;
using HospitalSystem.Application.Modules.Procurement.VendorContracts.Queries.GetVendorContractsByVendorQuery;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts.Enum;
using HospitalSystem.WebApi.Common;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Modules.Procurement.VendorContracts;

public static class VendorContractEndpoints
{
    public static RouteGroupBuilder MapVendorContractEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/vendor-contracts")
            .WithTags("Procurement - Vendor Contracts");

        // GET: /api/procurement/vendor-contracts/{vendorContractId}
        group.MapGet(
            "/{vendorContractId:guid}",
            async (
                Guid vendorContractId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync<
                    GetVendorContractByIdQuery,
                    VendorContractDto>(
                    new GetVendorContractByIdQuery(
                        new VendorContractId(vendorContractId)),
                    sender,
                    services,
                    cancellationToken));

        // GET: /api/procurement/vendor-contracts/by-vendor/{vendorId}
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
                    GetVendorContractsByVendorQuery,
                    PaginatedList<VendorContractDto>>(
                    new GetVendorContractsByVendorQuery(
                        new VendorId(vendorId),
                        pageNumber,
                        pageSize),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/vendor-contracts
        group.MapPost(
            "",
            async (
                CreateVendorContractRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync<
                    CreateVendorContractCommand,
                    VendorContractId>(
                    new CreateVendorContractCommand(
                        new VendorId(request.VendorId),
                        request.Category,
                        request.Start,
                        request.End,
                        request.Amount,
                        request.Currency),
                    sender,
                    services,
                    cancellationToken,
                    id => TypedResults.Created(
                        $"/api/procurement/vendor-contracts/{id.Value}",
                        id.Value)));

        // POST: /api/procurement/vendor-contracts/{vendorContractId}/activate
        group.MapPost(
            "/{vendorContractId:guid}/activate",
            async (
                Guid vendorContractId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new ActivateVendorContractCommand(
                        new VendorContractId(vendorContractId)),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/vendor-contracts/{vendorContractId}/terminate
        group.MapPost(
            "/{vendorContractId:guid}/terminate",
            async (
                Guid vendorContractId,
                TerminateVendorContractRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new TerminateVendorContractCommand(
                        new VendorContractId(vendorContractId),
                        request.Reason),
                    sender,
                    services,
                    cancellationToken));

        return group;
    }

    public sealed record CreateVendorContractRequest(
        Guid VendorId,
        VendorServiceCategory Category,
        DateTime Start,
        DateTime End,
        decimal Amount,
        string Currency);

    public sealed record TerminateVendorContractRequest(
        string Reason);
}
