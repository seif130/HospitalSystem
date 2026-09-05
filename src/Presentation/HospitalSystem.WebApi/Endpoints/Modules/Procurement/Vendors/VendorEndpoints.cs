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
        var group = app.MapGroup("/api/procurement/vendors").WithTags("Procurement - Vendors");

        // GET: /api/procurement/vendors
        group.MapGet("", async (
            ISender sender,
            CancellationToken ct,
            int pageNumber = 1,
            int pageSize = 20) =>
        {
            var result = await sender.Send(new GetVendorsQuery(pageNumber, pageSize), ct);
            return result.ToHttpResult();
        });

        // GET: /api/procurement/vendors/{vendorId}
        group.MapGet("/{vendorId:guid}", async (Guid vendorId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new GetVendorByIdQuery(new VendorId(vendorId)), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/vendors
        group.MapPost("", async (CreateVendorRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new CreateVendorCommand(
                request.Name,
                request.ContactEmail,
                request.ContactPhone), ct);

            return result.ToCreatedResult(id => $"/api/procurement/vendors/{id.Value}");
        });

        // PUT: /api/procurement/vendors/{vendorId}/name
        group.MapPut("/{vendorId:guid}/name", async (
            Guid vendorId,
            RenameVendorRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new RenameVendorCommand(
                new VendorId(vendorId),
                request.Name), ct);

            return result.ToHttpResult();
        });

        // PUT: /api/procurement/vendors/{vendorId}/contact
        group.MapPut("/{vendorId:guid}/contact", async (
            Guid vendorId,
            UpdateVendorContactRequest request,
            ISender sender,
            CancellationToken ct) =>
        {
            var result = await sender.Send(new UpdateVendorContactCommand(
                new VendorId(vendorId),
                request.ContactEmail,
                request.ContactPhone), ct);

            return result.ToHttpResult();
        });

        // POST: /api/procurement/vendors/{vendorId}/activate
        group.MapPost("/{vendorId:guid}/activate", async (Guid vendorId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new ActivateVendorCommand(new VendorId(vendorId)), ct);
            return result.ToHttpResult();
        });

        // POST: /api/procurement/vendors/{vendorId}/deactivate
        group.MapPost("/{vendorId:guid}/deactivate", async (Guid vendorId, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new DeactivateVendorCommand(new VendorId(vendorId)), ct);
            return result.ToHttpResult();
        });

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