using HospitalSystem.Application.Modules.Scheduling.Departments.Command.CreateDepartment;
using HospitalSystem.Application.Modules.Scheduling.Departments.GetDepartments;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
    public static class DepartmentsEndpoints
    {
        public static void MapDepartmentsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/scheduling/departments").WithTags("Departments");

            group.MapGet("", async (ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(new GetDepartmentsQuery(), ct);
                return Results.Ok(result.Value);
            });

            group.MapPost("", async (CreateDepartmentCommand command, ISender sender, CancellationToken ct) =>
            {
                var result = await sender.Send(command, ct);
                return result.IsFailure ? Results.BadRequest(result.Error) : Results.Ok(result.Value);
            });
        }
    }
}
