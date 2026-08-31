namespace HospitalSystem.WebApi.Endpoints.Contracts.Scheduling.Departmet
{
    public sealed record CreateDepartmentRequest(
        string Name,
        string? Description);
}
