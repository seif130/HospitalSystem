using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Engagement.Complaint.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Engagement.Complaint
{
    public sealed class Complaint : AggregateRoot<ComplaintId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public string Subject { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public ComplaintSeverity Severity { get; private set; }
        public ComplaintStatus Status { get; private set; }
        public string? Resolution { get; private set; }
        public DateTime RaisedOnUtc { get; private set; }
        public DateTime? ResolvedOnUtc { get; private set; }

        private Complaint() { }

        private Complaint(ComplaintId id, PatientId patientId, string subject, string description, ComplaintSeverity severity) : base(id)
        {
            PatientId = patientId;
            Subject = subject;
            Description = description;
            Severity = severity;
            Status = ComplaintStatus.Open;
            RaisedOnUtc = DateTime.UtcNow;
        }

        public static Complaint Raise(PatientId patientId, string subject, string description, ComplaintSeverity severity)
        {
            if (string.IsNullOrWhiteSpace(subject)) throw new DomainException("Complaint subject is required.");
            if (string.IsNullOrWhiteSpace(description)) throw new DomainException("Complaint description is required.");
            var complaint = new Complaint(ComplaintId.New(), patientId, subject.Trim(), description.Trim(), severity);
            if (severity == ComplaintSeverity.Critical)
                complaint.RaiseDomainEvent(new CriticalComplaintRaisedDomainEvent(complaint.Id, patientId, subject));
            return complaint;
        }

        public void BeginInvestigation()
        {
            if (Status != ComplaintStatus.Open) throw new DomainException("Only an open complaint can enter investigation.");
            Status = ComplaintStatus.UnderInvestigation;
        }

        public void Resolve(string resolution)
        {
            if (Status != ComplaintStatus.UnderInvestigation) throw new DomainException("Complaint must be under investigation before resolving.");
            if (string.IsNullOrWhiteSpace(resolution)) throw new DomainException("Resolution details are required.");
            Status = ComplaintStatus.Resolved;
            Resolution = resolution.Trim();
            ResolvedOnUtc = DateTime.UtcNow;
        }

        public void Dismiss(string reason)
        {
            if (Status == ComplaintStatus.Resolved) throw new DomainException("Cannot dismiss an already-resolved complaint.");
            Status = ComplaintStatus.Dismissed;
            Resolution = reason.Trim();
        }
    }
}
