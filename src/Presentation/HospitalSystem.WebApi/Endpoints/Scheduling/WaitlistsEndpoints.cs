using HospitalSystem.Application.Modules.Scheduling.Waitlists.JoinWaitlist;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
    public static class WaitlistsEndpoints
    {
        public static void MapWaitlistsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/scheduling/waitlists").WithTags("Waitlists");

            group.MapPost("", async (JoinWaitlistCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Value);
            });
        }
    }
}
