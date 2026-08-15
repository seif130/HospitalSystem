using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Engagement.PatientFeedback.PatientFeedback;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Engagement.PatientFeedback
{
    public sealed class PatientFeedback : AggregateRoot<PatientFeedbackId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public int Rating { get; private set; } // 1-5
        public string? Comments { get; private set; }
        public string VisitContext { get; private set; } = null!; // "Appointment:{id}", "Admission:{id}"
        public DateTime SubmittedOnUtc { get; private set; }

        private PatientFeedback() { }

        private PatientFeedback(PatientFeedbackId id, PatientId patientId, int rating, string? comments, string visitContext) : base(id)
        {
            PatientId = patientId;
            Rating = rating;
            Comments = comments;
            VisitContext = visitContext;
            SubmittedOnUtc = DateTime.UtcNow;
        }

        public static PatientFeedback Submit(PatientId patientId, int rating, string visitContext, string? comments = null)
        {
            if (rating is < 1 or > 5) throw new DomainException("Rating must be between 1 and 5.");
            if (string.IsNullOrWhiteSpace(visitContext)) throw new DomainException("Visit context is required.");
            var feedback = new PatientFeedback(PatientFeedbackId.New(), patientId, rating, comments?.Trim(), visitContext);
            if (rating <= 2)
                feedback.AddDomainEvent(new LowPatientFeedbackSubmittedDomainEvent(feedback.Id, patientId, rating));
            return feedback;
        }
    }
}
