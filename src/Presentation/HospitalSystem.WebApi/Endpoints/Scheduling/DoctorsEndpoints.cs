using HospitalSystem.Application.Modules.Scheduling.Doctors.AddDoctorAvailability;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
    public static class DoctorsEndpoints
    {
        public static void MapDoctorsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/scheduling/doctors").WithTags("Doctors");

            group.MapPost("{doctorId:guid}/availability", async (Guid doctorId, AddDoctorAvailabilityRequest request, ISender sender, CancellationToken ct) =>
            {
                var command = new AddDoctorAvailabilityCommand(doctorId, request.StartUtc, request.EndUtc);
                var result = await sender.Send(command, ct);
                return result.IsFailure ? Results.BadRequest(result.Error) : Results.NoContent();
            });
        }
    }

    public sealed record AddDoctorAvailabilityRequest(DateTime StartUtc, DateTime EndUtc);
}
