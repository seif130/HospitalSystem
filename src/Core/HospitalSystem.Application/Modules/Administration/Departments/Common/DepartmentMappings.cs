using HospitalSystem.Domain.Modules.Administration.Entities;

namespace HospitalSystem.Application.Modules.Administration.Departments.Common;

public static class DepartmentMappings
{
    public static DepartmentDto ToDto(this Department department) => new(
        department.Id,
        department.Name,
        department.Description,
        department.HeadDoctorId,
        department.Rooms.Select(room => new RoomDto(room.Id, room.RoomNumber, room.Type)).ToArray(),
        department.Equipments.Select(item => new DepartmentEquipmentDto(
            item.Id, item.EquipmentName, item.SerialNumber, item.PurchaseDate)).ToArray(),
        department.Services.Select(service => new DepartmentServiceDto(
            service.Id,
            service.ServiceName,
            service.Description,
            service.Price.Amount,
            service.Price.Currency)).ToArray(),
        department.Doctors.Count,
        department.Nurses.Count,
        department.Schedules.Count);

    public static DepartmentListItemDto ToListItemDto(this Department department) => new(
        department.Id,
        department.Name,
        department.Description,
        department.HeadDoctorId,
        department.Rooms.Count,
        department.Doctors.Count,
        department.Nurses.Count,
        department.Services.Count);
}
