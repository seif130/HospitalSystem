using HospitalSystem.Application.Modules.Scheduling.Departments.Command.CreateDepartment;
using HospitalSystem.Application.Modules.Scheduling.Departments.Command.RenameDepartment;
using HospitalSystem.Application.Modules.Scheduling.Departments.Queries.GetDepartmentById;
using HospitalSystem.Application.Modules.Scheduling.Departments.Queries.GetDepartments;
using HospitalSystem.WebApi.Endpoints.Contracts.Scheduling.Departmet;
using MediatR;

namespace HospitalSystem.WebApi.Endpoints.Scheduling
{
    public static class DepartmentEndpoints
    {
        public static IEndpointRouteBuilder MapDepartmentEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/hospital/scheduling/departments").WithTags("Departments");

            group.MapPost("/", CreateDepartment);

            group.MapGet("/", GetDepartments);

            group.MapGet("/{id:guid}", GetDepartmentById);

            group.MapPut("/{id:guid}", RenameDepartment);

            return app;
        }

        private static async Task<IResult> CreateDepartment(
            CreateDepartmentRequest request,ISender sender, CancellationToken cancellationToken = default)
        {
            var command = new CreateDepartmentCommand(request.Name,request.Description);

            var result = await sender.Send(command,cancellationToken);

            return result.ToCreatedResult(id => $"/api/hospital/scheduling/departments/{id}");
        }

        private static async Task<IResult> GetDepartments(ISender sender,CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new GetDepartmentsQuery(),cancellationToken);

            return result.ToHttpResult();
        }

        private static async Task<IResult> GetDepartmentById(Guid id, ISender sender, CancellationToken cancellationToken = default)
        {
            var result = await sender.Send(new GetDepartmentByIdQuery(id),cancellationToken);

            return result.ToHttpResult();
        }

        private static async Task<IResult> RenameDepartment(Guid id,RenameDepartmentRequest request,ISender sender,
            CancellationToken cancellationToken = default)
        {
            var command = new RenameDepartmentCommand(id, request.Name);

            var result = await sender.Send(command, cancellationToken);

            return result.ToHttpResult();
        }
    }
}
