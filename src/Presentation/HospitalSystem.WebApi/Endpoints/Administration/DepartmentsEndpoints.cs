using HospitalSystem.Application.Modules.Administration.Departments.Commands.AddEquipmentCommand;
using HospitalSystem.Application.Modules.Administration.Departments.Commands.AddRoomCommand;
using HospitalSystem.Application.Modules.Administration.Departments.Commands.CreateDepartment;
using HospitalSystem.Application.Modules.Administration.Departments.Commands.DeleteDepartmentCommand;
using HospitalSystem.Application.Modules.Administration.Departments.Commands.RemoveEquipmentCommand;
using HospitalSystem.Application.Modules.Administration.Departments.Commands.RemoveRoomCommand;
using HospitalSystem.Application.Modules.Administration.Departments.Commands.UpdateDepartmentCommand;
using HospitalSystem.Application.Modules.Administration.Departments.Queries.GetDepartmentByIdQuery;
using HospitalSystem.Application.Modules.Administration.Departments.Queries.GetDepartmentsQuery;
using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Modules.Administration.Enums;
using HospitalSystem.WebApi.Models.Addministration;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace HospitalSystem.WebApi.Endpoints.Administration;

public static class DepartmentsEndpoints
{
    public static IEndpointRouteBuilder MapDepartmentsEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/departments")
            .WithTags("Departments");

        group.MapPost("/", CreateDepartment)
            .WithName("CreateDepartment")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        //group.MapGet("/", GetDepartments)
        //    .WithName("GetDepartments")
        //    .Produces(StatusCodes.Status200OK)
        //    .Produces(StatusCodes.Status400BadRequest);

        group.MapGet("/{id:guid}", GetDepartmentById)
            .WithName("GetDepartmentById")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id:guid}", UpdateDepartment)
            .WithName("UpdateDepartment")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", DeleteDepartment)
            .WithName("DeleteDepartment")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        //group.MapPost("/{id:guid}/rooms", AddRoom)
        //    .WithName("AddRoomToDepartment")
        //    .Produces(StatusCodes.Status200OK)
        //    .Produces(StatusCodes.Status400BadRequest)
        //    .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}/rooms/{roomId:guid}", RemoveRoom)
            .WithName("RemoveRoomFromDepartment")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/{id:guid}/equipment", AddEquipment)
            .WithName("AddEquipmentToDepartment")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}/equipment/{equipmentId:guid}", RemoveEquipment)
            .WithName("RemoveEquipmentFromDepartment")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        return app;
    }

    private static async Task<IResult> CreateDepartment(
        CreateDepartmentApiRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateDepartmentCommand(
            request.Name,
            request.Description,
            request.HeadDoctorId);

        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Results.Created($"/api/departments/{result.Data.Id}", result.Data)
            : Results.BadRequest(result.Errors);
    }

    //private static async Task<IResult> GetDepartments(
    //    int? pageNumber,
    //    int? pageSize,
    //    string? searchTerm,
    //    ISender sender,
    //    CancellationToken cancellationToken)
    //{
    //    var query = new GetDepartmentsQuery(
    //        pageNumber ?? 1,
    //        pageSize ?? 10,
    //        searchTerm);

    //    var result = await sender.Send(query, cancellationToken);

    //    return result.IsSuccess
    //        ? Results.Ok(result.Data)
    //        : Results.BadRequest(result.Errors);
    //}

    private static async Task<IResult> GetDepartmentById(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDepartmentByIdQuery(id),
            cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Data)
            : Results.NotFound(result.Errors);
    }

    private static async Task<IResult> UpdateDepartment(
        Guid id,
        UpdateDepartmentDetailsApiRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new UpdateDepartmentDetailsCommand(
            id,
            request.Name,
            request.Description,
            request.HeadDoctorId);

        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : ToErrorResult(result.Errors);
    }

    private static async Task<IResult> DeleteDepartment(
        Guid id,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new DeleteDepartmentCommand(id),
            cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : ToErrorResult(result.Errors);
    }

    //private static async Task<IResult> AddRoom(
    //    Guid id,
    //    AddRoomApiRequest request,
    //    ISender sender,
    //    CancellationToken cancellationToken)
    //{
    //    if (!Enum.TryParse<RoomType>(request.RoomType, true, out var roomType))
    //    {
    //        return Results.BadRequest(new
    //        {
    //            Code = "Room.InvalidType",
    //            Message = $"'{request.RoomType}' is not a valid room type."
    //        });
    //    }

    //    var command = new AddRoomCommand(
    //        id,
    //        request.RoomNumber,
    //        roomType);

    //    var result = await sender.Send(command, cancellationToken);

    //    return result.IsSuccess
    //        ? Results.Ok(result.Data)
    //        : ToErrorResult(result.Errors);
    //}

    private static async Task<IResult> RemoveRoom(
        Guid id,
        Guid roomId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new RemoveRoomCommand(id, roomId),
            cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : ToErrorResult(result.Errors);
    }

    private static async Task<IResult> AddEquipment(
        Guid id,
        AddEquipmentApiRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new AddEquipmentCommand(
            id,
            request.EquipmentName,
            request.SerialNumber,
            request.PurchaseDate);

        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Data)
            : ToErrorResult(result.Errors);
    }

    private static async Task<IResult> RemoveEquipment(
        Guid id,
        Guid equipmentId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new RemoveEquipmentCommand(id, equipmentId),
            cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : ToErrorResult(result.Errors);
    }

    private static IResult ToErrorResult(
        IReadOnlyCollection<Error> errors)
    {
        if (errors.Any(error =>
            error.Code.Contains("NotFound", StringComparison.OrdinalIgnoreCase)))
        {
            return Results.NotFound(errors);
        }

        if (errors.Any(error =>
            error.Code.Contains("Conflict", StringComparison.OrdinalIgnoreCase) ||
            error.Code.Contains("Duplicate", StringComparison.OrdinalIgnoreCase)))
        {
            return Results.Conflict(errors);
        }

        return Results.BadRequest(errors);
    }
}
