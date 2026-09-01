
using HospitalSystem.Application.Modules.Scheduling.Specialty.Command.CreateSpecialty;
using HospitalSystem.Application.Modules.Scheduling.Specialty.Command.DeleteSpecialty;
using HospitalSystem.Application.Modules.Scheduling.Specialty.Command.ReactivateSpecialty;
using HospitalSystem.Application.Modules.Scheduling.Specialty.Command.RenameSpecialty;
using HospitalSystem.Application.Modules.Scheduling.Specialty.Command.UpdateSpecialty;
using HospitalSystem.Application.Modules.Scheduling.Specialty.Quires.GetSpecialties;
using HospitalSystem.Application.Modules.Scheduling.Specialty.Quires.GetSpecialtyById;
using MediatR;
namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
    public static class SpecialtyEndpoints
    {
        public static IEndpointRouteBuilder MapSpecialtyEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app
                .MapGroup("/api/hospital/scheduling/specialties")
                .WithTags("Specialties");

            group.MapPost("/", async (
                CreateSpecialtyCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);

                return result.ToCreatedResult(
                    id => $"/api/hospital/scheduling/specialties/{id}");
            });


            group.MapGet("/", async (
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetSpecialtiesQuery();

                var result = await sender.Send(query, ct);

                return result.ToHttpResult();
            });

            group.MapGet("/{specialtyId:guid}", async (
                Guid specialtyId,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetSpecialtyByIdQuery(
                    specialtyId);

                var result = await sender.Send(query, ct);

                return result.ToHttpResult();
            });

            group.MapPut("/{specialtyId:guid}/name", async (
                Guid specialtyId,
                RenameSpecialtyCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with
                {
                    SpecialtyId = specialtyId
                };

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });

            group.MapPut("/{specialtyId:guid}/description", async (
                Guid specialtyId,
                UpdateSpecialtyDescriptionCommand command,
                ISender sender,
                CancellationToken ct) =>
            {
                command = command with
                {
                    SpecialtyId = specialtyId
                };

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });


            group.MapPost("/{specialtyId:guid}/deactivate", async (
                Guid specialtyId,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new DeactivateSpecialtyCommand(
                    specialtyId);

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });

            group.MapPost("/{specialtyId:guid}/reactivate", async (
                Guid specialtyId,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new ReactivateSpecialtyCommand(
                    specialtyId);

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });

            return app;
        }
    }
}
