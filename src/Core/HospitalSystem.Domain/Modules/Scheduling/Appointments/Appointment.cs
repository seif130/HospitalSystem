using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Scheduling.Appointments.Appointment_Events;
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

        public DateRange ScheduledPeriod { get; private set; } = null!;

        public AppointmentType Type { get; private set; }

        public AppointmentStatus Status { get; private set; }

        public string? Reason { get; private set; }

        public string? CancellationReason { get; private set; }

        private Appointment()
        {
        }

        private Appointment(
            AppointmentId id,
            PatientId patientId,
            DoctorId doctorId,
            ClinicRoomId clinicRoomId,
            DateRange scheduledPeriod,
            AppointmentType type,
            string? reason)
            : base(id)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            ClinicRoomId = clinicRoomId;
            ScheduledPeriod = scheduledPeriod;
            Type = type;
            Reason = NormalizeOptional(reason);
            Status = AppointmentStatus.Scheduled;
        }

        public static Appointment Schedule(
            PatientId patientId,
            DoctorId doctorId,
            ClinicRoomId clinicRoomId,
            DateRange scheduledPeriod,
            AppointmentType type,
            DateTime utcNow,
            string? reason = null)
        {
            ArgumentNullException.ThrowIfNull(scheduledPeriod);

            if (scheduledPeriod.Start <= utcNow)
            {
                throw new DomainException(
                    "Appointment must be scheduled in the future.");
            }

            if (scheduledPeriod.IsOpen)
            {
                throw new DomainException(
                    "Appointment must have an end time.");
            }

            var appointment = new Appointment(
                AppointmentId.New(),
                patientId,
                doctorId,
                clinicRoomId,
                scheduledPeriod,
                type,
                reason);

            appointment.AddDomainEvent(
                new AppointmentScheduledEvent(
                    appointment.Id,
                    patientId,
                    doctorId,
                    scheduledPeriod.Start));

            return appointment;
        }

        public void Reschedule(
            DateRange newPeriod,
            DateTime utcNow)
        {
            EnsureModifiable();

            ArgumentNullException.ThrowIfNull(newPeriod);

            if (newPeriod.Start <= utcNow)
            {
                throw new DomainException(
                    "New appointment time must be in the future.");
            }

            if (newPeriod.IsOpen)
            {
                throw new DomainException(
                    "Appointment must have an end time.");
            }

            var oldPeriod = ScheduledPeriod;

            ScheduledPeriod = newPeriod;

            AddDomainEvent(
                new AppointmentRescheduledEvent(
                    Id,
                    PatientId,
                    DoctorId,
                    oldPeriod.Start,
                    newPeriod.Start));
        }

        public void CheckIn()
        {
            if (Status != AppointmentStatus.Scheduled)
            {
                throw new DomainException(
                    "Only a scheduled appointment can be checked in.");
            }

            Status = AppointmentStatus.CheckedIn;

            AddDomainEvent(
                new AppointmentCheckedInEvent(
                    Id,
                    PatientId,
                    DoctorId));
        }

        public void Complete()
        {
            if (Status != AppointmentStatus.CheckedIn)
            {
                throw new DomainException(
                    "Only a checked-in appointment can be completed.");
            }

            Status = AppointmentStatus.Completed;

            AddDomainEvent(
                new AppointmentCompletedEvent(
                    Id,
                    PatientId,
                    DoctorId));
        }

        public void Cancel(string reason)
        {
            EnsureModifiable();

            if (string.IsNullOrWhiteSpace(reason))
            {
                throw new DomainException(
                    "Cancellation reason is required.");
            }

            var cancellationReason = reason.Trim();

            Status = AppointmentStatus.Cancelled;
            CancellationReason = cancellationReason;

            AddDomainEvent(
                new AppointmentCancelledEvent(
                    Id,
                    PatientId,
                    DoctorId,
                    cancellationReason));
        }

        public void MarkAsNoShow(DateTime utcNow)
        {
            if (Status != AppointmentStatus.Scheduled)
            {
                throw new DomainException(
                    "Only a scheduled appointment can be marked as no-show.");
            }

            if (utcNow < ScheduledPeriod.Start)
            {
                throw new DomainException(
                    "An appointment cannot be marked as no-show before its scheduled time.");
            }

            Status = AppointmentStatus.NoShow;

            AddDomainEvent(
                new AppointmentNoShowEvent(
                    Id,
                    PatientId,
                    DoctorId));
        }

        private void EnsureModifiable()
        {
            if (Status != AppointmentStatus.Scheduled)
            {
                throw new DomainException(
                    $"Cannot modify an appointment that is already {Status}.");
            }
        }

        private static string? NormalizeOptional(string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();
        }
    }

}
