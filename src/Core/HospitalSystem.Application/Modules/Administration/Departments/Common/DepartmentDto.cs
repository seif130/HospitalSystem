using HospitalSystem.Domain.Modules.Administration.Enums;

namespace HospitalSystem.Application.Modules.Administration.Departments.Common;

public sealed record DepartmentDto(
    Guid Id,
    string Name,
    string Description,
    Guid? HeadDoctorId,
    IReadOnlyCollection<RoomDto> Rooms,
    IReadOnlyCollection<DepartmentEquipmentDto> Equipments,
    IReadOnlyCollection<DepartmentServiceDto> Services,
    int DoctorCount,
    int NurseCount,
    int OnCallScheduleCount);

public sealed record RoomDto(Guid Id, string RoomNumber, RoomType Type);

public sealed record DepartmentEquipmentDto( Guid Id, string EquipmentName, string SerialNumber, DateTime PurchaseDate);

public sealed record DepartmentServiceDto(
    Guid Id,
    string ServiceName,
    string Description,
    decimal Amount,
    string Currency);

public sealed record DepartmentListItemDto(
    Guid Id,
    string Name,
    string Description,
    Guid? HeadDoctorId,
    int RoomCount,
    int DoctorCount,
    int NurseCount,
    int ServiceCount);
