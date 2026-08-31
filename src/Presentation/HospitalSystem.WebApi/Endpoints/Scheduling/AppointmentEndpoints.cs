using HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CancelAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CheckInAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Command.CompleteAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Command.MarkAppointmentAsNoShow;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Command.RescheduleAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Command.ScheduleAppointment;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetAppointmentById;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetClinicRoomAppointments;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetDoctorSchedule;
using HospitalSystem.Application.Modules.Scheduling.Appointments.Query.GetPatientAppointments;
using MediatR;
namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
   

    public static class AppointmentEndpoints
    {
        public static IEndpointRouteBuilder MapAppointmentEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/hospital/scheduling/appointments").WithTags("Appointments");

            group.MapPost("/", async (
                ScheduleAppointmentCommand command,ISender sender,CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);

                return result.ToCreatedResult(id => $"/api/hospital/scheduling/appointments/{id}");
            });

 
            group.MapGet("/{appointmentId:guid}", async (
                Guid appointmentId,ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetAppointmentByIdQuery(
                    appointmentId);

                var result = await sender.Send(query, ct);

                return result.ToHttpResult();
            });
  
            group.MapGet("/doctor/{doctorId:guid}", async (
                Guid doctorId,
                DateTime fromUtc,DateTime toUtc,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetDoctorAppointmentsQuery(doctorId,fromUtc,toUtc);

                var result = await sender.Send(query, ct);

                return result.ToHttpResult();
            });


            group.MapGet("/patient/{patientId:guid}", async (
                Guid patientId,DateTime fromUtc,
                DateTime toUtc,ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetPatientAppointmentsQuery(patientId,fromUtc,toUtc);

                var result = await sender.Send(query, ct);

                return result.ToHttpResult();
            });


            group.MapGet("/clinic-room/{clinicRoomId:guid}", async (
                Guid clinicRoomId,
                DateTime fromUtc,
                DateTime toUtc,
                ISender sender,
                CancellationToken ct) =>
            {
                var query = new GetClinicRoomAppointmentsQuery(
                    clinicRoomId,
                    fromUtc,
                    toUtc);

                var result = await sender.Send(query, ct);

                return result.ToHttpResult();
            });


            group.MapPut("/{appointmentId:guid}/reschedule", async (
                Guid appointmentId,
                RescheduleAppointmentRequest request,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new RescheduleAppointmentCommand(
                    appointmentId,
                    request.StartUtc,
                    request.EndUtc);

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });


            group.MapPost("/{appointmentId:guid}/cancel", async (
                Guid appointmentId,
                CancelAppointmentRequest request,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new CancelAppointmentCommand(
                    appointmentId,
                    request.Reason);

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });


            group.MapPost("/{appointmentId:guid}/check-in", async (
                Guid appointmentId,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new CheckInAppointmentCommand(
                    appointmentId);

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });


            group.MapPost("/{appointmentId:guid}/complete", async (
                Guid appointmentId,
                ISender sender,
                CancellationToken ct) =>
            {
                var command = new CompleteAppointmentCommand(appointmentId);

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });


            group.MapPost("/{appointmentId:guid}/no-show", async (Guid appointmentId,ISender sender,
                CancellationToken ct) =>
            {
                var command = new MarkAppointmentAsNoShowCommand(
                    appointmentId);

                var result = await sender.Send(command, ct);

                return result.ToHttpResult();
            });

            return app;
        }

        public sealed record RescheduleAppointmentRequest(
            DateTime StartUtc,
            DateTime EndUtc);

        public sealed record CancelAppointmentRequest(
            string Reason);
    }
}
