using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Enums;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Scheduling.Appointments
{
    public sealed class Appointment : AggregateRoot<AppointmentId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId DoctorId { get; private set; } = null!;
        public ClinicRoomId ClinicRoomId { get; private set; } = null!;
        public DateTime ScheduledAtUtc { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public string? Reason { get; private set; }
        public string? CancellationReason { get; private set; }

        private Appointment() { }

        private Appointment(AppointmentId id, PatientId patientId, DoctorId doctorId, ClinicRoomId clinicRoomId,
            DateTime scheduledAtUtc, string? reason) : base(id)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            ClinicRoomId = clinicRoomId;
            ScheduledAtUtc = scheduledAtUtc;
            Reason = reason;
            Status = AppointmentStatus.Scheduled;
        }

        public static Appointment Schedule(PatientId patientId, DoctorId doctorId, ClinicRoomId clinicRoomId,
            DateTime scheduledAtUtc, string? reason = null)
        {
            if (scheduledAtUtc <= DateTime.UtcNow) throw new DomainException("Appointment must be scheduled in the future.");

            var appointment = new Appointment(AppointmentId.New(), patientId, doctorId, clinicRoomId, scheduledAtUtc, reason);
            appointment.RaiseDomainEvent(new AppointmentScheduledDomainEvent(appointment.Id, patientId, doctorId, scheduledAtUtc));
            return appointment;
        }

        public void Reschedule(DateTime newTimeUtc)
        {
            EnsureModifiable();
            if (newTimeUtc <= DateTime.UtcNow) throw new DomainException("New appointment time must be in the future.");
            ScheduledAtUtc = newTimeUtc;
        }

        public void CheckIn()
        {
            if (Status != AppointmentStatus.Scheduled) throw new DomainException("Only a scheduled appointment can be checked in.");
            Status = AppointmentStatus.CheckedIn;
        }

        public void Complete()
        {
            if (Status != AppointmentStatus.CheckedIn) throw new DomainException("Only a checked-in appointment can be completed.");
            Status = AppointmentStatus.Completed;
        }

        public void Cancel(string reason)
        {
            EnsureModifiable();
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Cancellation reason is required.");
            Status = AppointmentStatus.Cancelled;
            CancellationReason = reason.Trim();
            RaiseDomainEvent(new AppointmentCancelledDomainEvent(Id, PatientId, DoctorId));
        }

        public void MarkAsNoShow()
        {
            if (Status != AppointmentStatus.Scheduled) throw new DomainException("Only a scheduled appointment can be marked as no-show.");
            Status = AppointmentStatus.NoShow;
        }

        private void EnsureModifiable()
        {
            if (Status is AppointmentStatus.Completed or AppointmentStatus.Cancelled)
                throw new DomainException($"Cannot modify an appointment that is already {Status}.");
        }
    }
}
