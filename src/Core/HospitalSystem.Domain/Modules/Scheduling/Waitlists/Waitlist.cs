using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Enums;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.WaitlistsEvents;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Waitlists
{
    public sealed class Waitlist : AggregateRoot<WaitlistId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId DoctorId { get; private set; } = null!;

        public DateTime PreferredFromUtc { get; private set; }
        public DateTime PreferredToUtc { get; private set; }

        public WaitlistEntryStatus Status { get; private set; }

        public DateTime JoinedOnUtc { get; private set; }

        public AppointmentId? OfferedAppointmentId { get; private set; }

        private Waitlist()
        {
        }

        private Waitlist(WaitlistId id, PatientId patientId, DoctorId doctorId,
            DateTime preferredFromUtc, DateTime preferredToUtc): base(id)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            PreferredFromUtc = preferredFromUtc;
            PreferredToUtc = preferredToUtc;
            Status = WaitlistEntryStatus.Waiting;
            JoinedOnUtc = DateTime.UtcNow;
        }

        public static Waitlist Join(
            PatientId patientId,DoctorId doctorId, DateTime preferredFromUtc, DateTime preferredToUtc)
        {
            if (preferredToUtc <= preferredFromUtc)
                throw new DomainException("Preferred date range is invalid.");

            var waitlist = new Waitlist(
                WaitlistId.New(), patientId, doctorId,preferredFromUtc, preferredToUtc);

            waitlist.AddDomainEvent(
                new WaitlistJoinedEvent(waitlist.Id,patientId, doctorId, preferredFromUtc, preferredToUtc));

            return waitlist;
        }

        public void OfferSlot(AppointmentId appointmentId)
        {
            if (Status != WaitlistEntryStatus.Waiting)
                throw new DomainException("Only a waiting entry can be offered a slot.");

            OfferedAppointmentId = appointmentId;
            Status = WaitlistEntryStatus.Offered;

            AddDomainEvent(new WaitlistSlotOfferedEvent( Id, PatientId, appointmentId));
        }

        public void ConfirmBooking()
        {
            if (Status != WaitlistEntryStatus.Offered)
                throw new DomainException( "No offer is pending confirmation.");

            var appointmentId = OfferedAppointmentId
                ?? throw new DomainException( "No appointment has been offered.");

            Status = WaitlistEntryStatus.Booked;

            AddDomainEvent( new WaitlistBookingConfirmedEvent( Id, PatientId, appointmentId));
        }

        public void ExpireOffer()
        {
            if (Status != WaitlistEntryStatus.Offered)
                throw new DomainException( "No offer is pending expiry.");

            var appointmentId = OfferedAppointmentId
                ?? throw new DomainException("No appointment has been offered.");

            Status = WaitlistEntryStatus.Waiting;
            OfferedAppointmentId = null;

            AddDomainEvent( new WaitlistOfferExpiredEvent( Id, PatientId, appointmentId));
        }

        public void Cancel()
        {
            if (Status == WaitlistEntryStatus.Booked)
                throw new DomainException( "A booked waitlist entry cannot be cancelled.");

            if (Status == WaitlistEntryStatus.Cancelled)
                return;

            Status = WaitlistEntryStatus.Cancelled;
            OfferedAppointmentId = null;

            AddDomainEvent( new WaitlistCancelledEvent( Id, PatientId, DoctorId));
        }
    }


}
