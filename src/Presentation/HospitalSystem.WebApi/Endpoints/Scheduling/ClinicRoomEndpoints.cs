using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.ChangeClinicRoomCapacity;
using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.ChangeClinicRoomDepartment;
using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Commands.RenameClinicRoom;
using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.CreateClinicRoom;
using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.DTOs;
using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetAvailableClinicRooms;
using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetClinicRoomById;
using HospitalSystem.Application.Modules.Scheduling.ClinicRooms.Queries.GetClinicRoomsByDepartment;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.WebApi.Endpoints.Contracts.Scheduling.ClinicRoom;
namespace HospitalSystem.WebApi.Endpoints.Scheduling
{

    public static class ClinicRoomEndpoints
    {
        public static IEndpointRouteBuilder MapClinicRoomEndpoints(
            this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/hospital/scheduling/clinic-rooms").WithTags("Clinic Rooms");

            group.MapGet("/{id:guid}",
                async (
                    Guid id,
                    IQueryHandler<GetClinicRoomByIdQuery, ClinicRoomDto> handler,
                    CancellationToken ct) =>
                {
                    var result = await handler.Handle(
                        new GetClinicRoomByIdQuery(id),
                        ct);

                    return result.ToHttpResult();
                });

            group.MapGet("/department/{departmentId:guid}",
                async (
                    Guid departmentId,
                    IQueryHandler<
                        GetClinicRoomsByDepartmentQuery,
                        IReadOnlyList<ClinicRoomDto>> handler,
                    CancellationToken ct) =>
                {
                    var result = await handler.Handle(
                        new GetClinicRoomsByDepartmentQuery(departmentId),
                        ct);

                    return result.ToHttpResult();
                });

            group.MapGet("/available",
                async (
                    Guid departmentId,
                    DateTime fromUtc,
                    DateTime toUtc,
                    IQueryHandler<
                        GetAvailableClinicRoomsQuery,
                        IReadOnlyList<ClinicRoomDto>> handler,
                    CancellationToken ct) =>
                {
                    var result = await handler.Handle(
                        new GetAvailableClinicRoomsQuery(
                            departmentId,
                            fromUtc,
                            toUtc),
                        ct);

                    return result.ToHttpResult();
                });

            group.MapPost("/",
                async (
                    CreateClinicRoomRequest request,
                    ICommandHandler<CreateClinicRoomCommand, Guid> handler,
                    CancellationToken ct) =>
                {
                    var result = await handler.Handle(
                        new CreateClinicRoomCommand(
                            request.RoomNumber,
                            request.DepartmentId,
                            request.Capacity),
                        ct);

                    return result.ToHttpResult();
                });

            group.MapPut("/{id:guid}/name",
                async (
                    Guid id,
                    RenameClinicRoomRequest request,
                    ICommandHandler<RenameClinicRoomCommand> handler,
                    CancellationToken ct) =>
                {
                    var result = await handler.Handle(
                        new RenameClinicRoomCommand(
                            id,
                            request.RoomNumber),
                        ct);

                    return result.ToHttpResult();
                });

            group.MapPut("/{id:guid}/capacity",
                async (
                    Guid id,
                    ChangeClinicRoomCapacityRequest request,
                    ICommandHandler<ChangeClinicRoomCapacityCommand> handler,
                    CancellationToken ct) =>
                {
                    var result = await handler.Handle(
                        new ChangeClinicRoomCapacityCommand(
                            id,
                            request.Capacity),
                        ct);

                    return result.ToHttpResult();
                });

            group.MapPut("/{id:guid}/department",
                async (
                    Guid id,
                    ChangeClinicRoomDepartmentRequest request,
                    ICommandHandler<ChangeClinicRoomDepartmentCommand> handler,
                    CancellationToken ct) =>
                {
                    var result = await handler.Handle(
                        new ChangeClinicRoomDepartmentCommand(
                            id,
                            request.DepartmentId),
                        ct);

                    return result.ToHttpResult();
                });

            return app;
        }
    }
}
