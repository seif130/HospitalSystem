using HospitalSystem.Application.Modules.Scheduling.Appointments.CancelAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.CheckInAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Command.RescheduleAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Command.ScheduleAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.CompleteAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.GetAppointmentById;
using HospitalSystem.Application.Modules.Scheduling.Appointments.GetDoctorSchedule;
using HospitalSystem.Application.Modules.Scheduling.Appointments.GetPatientAppointments;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.WebApi;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
    public static class AppointmentsEndpoints
    {
        public static void MapAppointmentsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/scheduling/appointments").WithTags("Appointments");

            group.MapPost("", async (ScheduleAppointmentCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                return result.ToCreatedResult(value => $"/api/scheduling/appointments/{value}");
            });

            group.MapGet("{id:guid}", async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetAppointmentByIdQuery(id), ct);
                return result.ToHttpResult();
            }).WithName("GetAppointmentById");

            group.MapGet("doctor/{doctorId:guid}", async (Guid doctorId, DateTime date, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetDoctorScheduleQuery(doctorId, date), ct);
                return result.ToHttpResult();
            });

            group.MapGet("patient/{patientId:guid}", async (Guid patientId, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetPatientAppointmentsQuery(patientId), ct);
                return result.ToHttpResult();
            });

            group.MapPut("{id:guid}/cancel", async (Guid id, CancelAppointmentRequest request, ISender sender, CancellationToken ct) =>
            {
                var command = new CancelAppointmentCommand(id, request.Reason);
                var result = await sender.Send(command, ct);
                return result.ToHttpResult();
            });

            group.MapPut("{id:guid}/reschedule", async (Guid id, RescheduleAppointmentCommand command, ISender sender, CancellationToken ct) =>
            {
                if (id != command.AppointmentId) return Results.BadRequest("ID mismatch.");
                var result = await sender.Send(command, ct);
                return result.ToHttpResult();
            });

            group.MapPut("{id:guid}/check-in", async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new CheckInAppointmentCommand(id), ct);
                return result.ToHttpResult();
            });

            group.MapPut("{id:guid}/complete", async (Guid id, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new CompleteAppointmentCommand(id), ct);
                return result.ToHttpResult();
            });
        }
    }

    public sealed record CancelAppointmentRequest(string Reason);

    public sealed record CancelAppointmentRequest(string Reason);
}
