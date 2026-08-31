namespace HospitalSystem.WebApi.Endpoints.Contracts.Scheduling.Waitlist
{
    public sealed record JoinWaitlistRequest(
       Guid PatientId,
       Guid DoctorId,
       DateTime PreferredFromUtc,
       DateTime PreferredToUtc);
}
