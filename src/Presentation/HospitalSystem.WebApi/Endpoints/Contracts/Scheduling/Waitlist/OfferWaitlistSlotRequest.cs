namespace HospitalSystem.WebApi.Endpoints.Contracts.Scheduling.Waitlist
{
    public sealed record OfferWaitlistSlotRequest(
        Guid AppointmentId);
}
