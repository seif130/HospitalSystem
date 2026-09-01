using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Engagement.DoctorReview
{
    public sealed class DoctorReview : AggregateRoot<DoctorReviewId>
    {
        public DoctorId DoctorId { get; private set; } = null!;
        public PatientId PatientId { get; private set; } = null!;
        public int Rating { get; private set; } // 1-5
        public string? Comment { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime SubmittedOnUtc { get; private set; }

        private DoctorReview() { }

        private DoctorReview(DoctorReviewId id, DoctorId doctorId, PatientId patientId, int rating, string? comment) : base(id)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            Rating = rating;
            Comment = comment;
            SubmittedOnUtc = DateTime.UtcNow;
        }

        public static DoctorReview Submit(DoctorId doctorId, PatientId patientId, int rating, string? comment = null)
        {
            if (rating is < 1 or > 5) throw new DomainException("Rating must be between 1 and 5.");
            return new DoctorReview(DoctorReviewId.New(), doctorId, patientId, rating, comment?.Trim());
        }

        public void Publish() => IsPublished = true;
        public void Unpublish() => IsPublished = false;
    }

}
