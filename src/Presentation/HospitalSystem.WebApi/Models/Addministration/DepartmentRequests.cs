using HospitalSystem.Domain.Modules.Administration.Enums;

namespace HospitalSystem.WebApi.Models.Addministration
{

    public sealed record CreateDepartmentApiRequest(
        string Name,
        string? Description,
        Guid? HeadDoctorId);

    public sealed record UpdateDepartmentDetailsApiRequest(
        string Name,
        string? Description,
        Guid? HeadDoctorId);

    public sealed record AddRoomApiRequest(
        string RoomNumber,
        RoomType Type);

    public sealed record AddEquipmentApiRequest(
        string EquipmentName,
        string SerialNumber,
        DateTime PurchaseDate);
}
