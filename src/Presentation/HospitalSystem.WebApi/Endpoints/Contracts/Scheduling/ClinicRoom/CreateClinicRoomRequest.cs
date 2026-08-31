namespace HospitalSystem.WebApi.Endpoints.Contracts.Scheduling.ClinicRoom
{
    public sealed record CreateClinicRoomRequest(
        string RoomNumber,
        Guid DepartmentId,
        int Capacity);
}
