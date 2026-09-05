using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Modules.Procurement.Vendors.Commands;
using HospitalSystem.Application.Modules.Procurement.Vendors.Commands.ActivateVendorCommand;
using HospitalSystem.Application.Modules.Procurement.Vendors.Commands.CreateVendorCommand;
using HospitalSystem.Application.Modules.Procurement.Vendors.Commands.DeactivateVendorCommand;
using HospitalSystem.Application.Modules.Procurement.Vendors.Commands.RenameVendorCommand;
using HospitalSystem.Application.Modules.Procurement.Vendors.Commands.UpdateVendorContactCommand;
using HospitalSystem.Application.Modules.Procurement.Vendors.Queries;
using HospitalSystem.Application.Modules.Procurement.Vendors.Queries.GetVendorByIdQuery;
using HospitalSystem.Application.Modules.Procurement.Vendors.Queries.GetVendorsQuery;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.WebApi.Common;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Modules.Procurement.Vendors;

public static class VendorEndpoints
{
    public static RouteGroupBuilder MapVendorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("/api/procurement/vendors")
            .WithTags("Procurement - Vendors");

        // GET: /api/procurement/vendors
        group.MapGet(
            "",
            async (
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken,
                int pageNumber = 1,
                int pageSize = 20) =>
                EndpointHelper.SendAsync<
                    GetVendorsQuery,
                    PaginatedList<VendorDto>>(
                    new GetVendorsQuery(
                        pageNumber,
                        pageSize),
                    sender,
                    services,
                    cancellationToken));

        // GET: /api/procurement/vendors/{vendorId}
        group.MapGet(
            "/{vendorId:guid}",
            async (
                Guid vendorId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync<
                    GetVendorByIdQuery,
                    VendorDto>(
                    new GetVendorByIdQuery(
                        new VendorId(vendorId)),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/vendors
        group.MapPost(
            "",
            async (
                CreateVendorRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync<
                    CreateVendorCommand,
                    VendorId>(
                    new CreateVendorCommand(
                        request.Name,
                        request.ContactEmail,
                        request.ContactPhone),
                    sender,
                    services,
                    cancellationToken,
                    id => TypedResults.Created(
                        $"/api/procurement/vendors/{id.Value}",
                        id.Value)));

        // PUT: /api/procurement/vendors/{vendorId}/name
        group.MapPut(
            "/{vendorId:guid}/name",
            async (
                Guid vendorId,
                RenameVendorRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new RenameVendorCommand(
                        new VendorId(vendorId),
                        request.Name),
                    sender,
                    services,
                    cancellationToken));

        // PUT: /api/procurement/vendors/{vendorId}/contact
        group.MapPut(
            "/{vendorId:guid}/contact",
            async (
                Guid vendorId,
                UpdateVendorContactRequest request,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new UpdateVendorContactCommand(
                        new VendorId(vendorId),
                        request.ContactEmail,
                        request.ContactPhone),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/vendors/{vendorId}/activate
        group.MapPost(
            "/{vendorId:guid}/activate",
            async (
                Guid vendorId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new ActivateVendorCommand(
                        new VendorId(vendorId)),
                    sender,
                    services,
                    cancellationToken));

        // POST: /api/procurement/vendors/{vendorId}/deactivate
        group.MapPost(
            "/{vendorId:guid}/deactivate",
            async (
                Guid vendorId,
                ISender sender,
                IServiceProvider services,
                CancellationToken cancellationToken) =>
                EndpointHelper.SendAsync(
                    new DeactivateVendorCommand(
                        new VendorId(vendorId)),
                    sender,
                    services,
                    cancellationToken));

        return group;
    }

    public sealed record CreateVendorRequest(
        string Name,
        string? ContactEmail,
        string? ContactPhone);

    public sealed record RenameVendorRequest(
        string Name);

    public sealed record UpdateVendorContactRequest(
        string? ContactEmail,
        string? ContactPhone);
}
