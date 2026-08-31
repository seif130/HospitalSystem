using HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorDepartmentCommand;
using HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorLicenseNumberCommand;
using HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorNameCommand;
using HospitalSystem.Application.Modules.Scheduling.Doctors.Command.ChangeDoctorSpecialtyCommand;
using HospitalSystem.Application.Modules.Scheduling.Doctors.Command.CreateDoctorCommand;
using HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorByIdQuery;
using HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorsByDepartmentQuery;
using HospitalSystem.Application.Modules.Scheduling.Doctors.Quries.GetDoctorsBySpecialtyQuery;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
    public static class DoctorEndpoints
    {
        public static RouteGroupBuilder MapDoctorEndpoints(this IEndpointRouteBuilder app)
        {
            var doctors = app
                .MapGroup("/api/hospital/scheduling/doctors")
                .WithTags("Doctors");

            doctors.MapPost("/", async (
                CreateDoctorCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    command,
                    cancellationToken);

                if (result.IsSuccess)
                {
                    return Results.Created(
                        $"/api/hospital/scheduling/doctors/{result.Value}",
                        result.Value);
                }

                return Results.BadRequest(result);
            });

            doctors.MapGet("/{doctorId:guid}", async (
                Guid doctorId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetDoctorByIdQuery(doctorId),
                    cancellationToken);

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.NotFound(result);
            });

            doctors.MapGet("/department/{departmentId:guid}", async (
                Guid departmentId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetDoctorsByDepartmentQuery(departmentId),
                    cancellationToken);

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.BadRequest(result);
            });

            doctors.MapGet("/specialty/{specialtyId:guid}", async (
                Guid specialtyId,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(
                    new GetDoctorsBySpecialtyQuery(specialtyId),
                    cancellationToken);

                if (result.IsSuccess)
                    return Results.Ok(result.Value);

                return Results.BadRequest(result);
            });

            doctors.MapPut("/{doctorId:guid}/name", async (
                Guid doctorId,
                ChangeDoctorNameCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var request = command with
                {
                    DoctorId = doctorId
                };

                var result = await sender.Send(
                    request,
                    cancellationToken);

                if (result.IsSuccess)
                    return Results.NoContent();

                return Results.BadRequest(result);
            });

            doctors.MapPut("/{doctorId:guid}/specialty", async (
                Guid doctorId,
                ChangeDoctorSpecialtyCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var request = command with
                {
                    DoctorId = doctorId
                };

                var result = await sender.Send(
                    request,
                    cancellationToken);

                if (result.IsSuccess)
                    return Results.NoContent();

                return Results.BadRequest(result);
            });

            doctors.MapPut("/{doctorId:guid}/department", async (
                Guid doctorId,
                ChangeDoctorDepartmentCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var request = command with
                {
                    DoctorId = doctorId
                };

                var result = await sender.Send(
                    request,
                    cancellationToken);

                if (result.IsSuccess)
                    return Results.NoContent();

                return Results.BadRequest(result);
            });

            doctors.MapPut("/{doctorId:guid}/license-number", async (
                Guid doctorId,
                ChangeDoctorLicenseNumberCommand command,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var request = command with
                {
                    DoctorId = doctorId
                };

                var result = await sender.Send(
                    request,
                    cancellationToken);

                if (result.IsSuccess)
                    return Results.NoContent();

                return Results.BadRequest(result);
            });

            return doctors;
        }
    }
    }

