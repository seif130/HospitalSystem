using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Telemedicine.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Telemedicine.TelemedicineSession
{
    public sealed class TelemedicineSession : AggregateRoot<TelemedicineSessionId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId DoctorId { get; private set; } = null!;
        public DateTime ScheduledAtUtc { get; private set; }
        public string MeetingLink { get; private set; } = null!;
        public SessionStatus Status { get; private set; }
        public DateTime? StartedOnUtc { get; private set; }
        public DateTime? EndedOnUtc { get; private set; }
        public string? ConsultationNotes { get; private set; }

        private TelemedicineSession() { }

        private TelemedicineSession(TelemedicineSessionId id, PatientId patientId, DoctorId doctorId, DateTime scheduledAtUtc, string meetingLink) : base(id)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            ScheduledAtUtc = scheduledAtUtc;
            MeetingLink = meetingLink;
            Status = SessionStatus.Scheduled;
        }

        public static TelemedicineSession Schedule(PatientId patientId, DoctorId doctorId, DateTime scheduledAtUtc, string meetingLink)
        {
            if (scheduledAtUtc <= DateTime.UtcNow) throw new DomainException("Session must be scheduled in the future.");
            if (string.IsNullOrWhiteSpace(meetingLink)) throw new DomainException("Meeting link is required.");
            return new TelemedicineSession(TelemedicineSessionId.New(), patientId, doctorId, scheduledAtUtc, meetingLink);
        }

        public void Start()
        {
            if (Status != SessionStatus.Scheduled) throw new DomainException("Only a scheduled session can start.");
            Status = SessionStatus.InProgress;
            StartedOnUtc = DateTime.UtcNow;
        }

        public void Complete(string consultationNotes)
        {
            if (Status != SessionStatus.InProgress) throw new DomainException("Only an in-progress session can be completed.");
            if (string.IsNullOrWhiteSpace(consultationNotes)) throw new DomainException("Consultation notes are required to close a session.");
            Status = SessionStatus.Completed;
            EndedOnUtc = DateTime.UtcNow;
            ConsultationNotes = consultationNotes.Trim();
        }

        public void Cancel()
        {
            if (Status is SessionStatus.Completed) throw new DomainException("Cannot cancel a completed session.");
            Status = SessionStatus.Cancelled;
        }

        public void MarkNoShow()
        {
            if (Status != SessionStatus.Scheduled) throw new DomainException("Only a scheduled session can be marked as no-show.");
            Status = SessionStatus.NoShow;
        }
    }
}
