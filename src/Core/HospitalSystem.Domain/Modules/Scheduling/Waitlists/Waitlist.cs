using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Waitlists.Enums;
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

        private Waitlist() { }

        private Waitlist(WaitlistId id, PatientId patientId, DoctorId doctorId, DateTime preferredFromUtc, DateTime preferredToUtc) : base(id)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            PreferredFromUtc = preferredFromUtc;
            PreferredToUtc = preferredToUtc;
            Status = WaitlistEntryStatus.Waiting;
            JoinedOnUtc = DateTime.UtcNow;
        }

        public static Waitlist Join(PatientId patientId, DoctorId doctorId, DateTime preferredFromUtc, DateTime preferredToUtc)
        {
            if (preferredToUtc <= preferredFromUtc) throw new DomainException("Preferred date range is invalid.");
            return new Waitlist(WaitlistId.New(), patientId, doctorId, preferredFromUtc, preferredToUtc);
        }

        public void OfferSlot(AppointmentId appointmentId)
        {
            if (Status != WaitlistEntryStatus.Waiting) throw new DomainException("Only a waiting entry can be offered a slot.");
            OfferedAppointmentId = appointmentId;
            Status = WaitlistEntryStatus.Offered;
            AddDomainEvent(new WaitlistSlotOfferedDomainEvent(Id, PatientId, appointmentId));
        }

        public void ConfirmBooking()
        {
            if (Status != WaitlistEntryStatus.Offered) throw new DomainException("No offer is pending confirmation.");
            Status = WaitlistEntryStatus.Booked;
        }

        public void ExpireOffer()
        {
            if (Status != WaitlistEntryStatus.Offered) throw new DomainException("No offer is pending expiry.");
            Status = WaitlistEntryStatus.Waiting;
            OfferedAppointmentId = null;
        }

        public void Cancel() => Status = WaitlistEntryStatus.Cancelled;
    }
}
