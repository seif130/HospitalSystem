using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.CreateClinicRoom;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
    public static class ClinicRoomsEndpoints
    {
        public static void MapClinicRoomsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/scheduling/clinic-rooms").WithTags("Clinic Rooms");

            group.MapGet("", async (ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetClinicRoomsQuery(), ct);
                return Results.Ok(result.Value);
            });

            group.MapPost("", async (CreateClinicRoomCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Value);
            });
        }
    }
}
