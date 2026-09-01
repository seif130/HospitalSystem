using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.DischargeSummaries
{
    public sealed class DischargeSummary : BaseEntity<DischargeSummaryId>
    {
        public AdmissionId AdmissionId { get; private set; } = null!;
        public PatientId PatientId { get; private set; } = null!;
        public string FinalDiagnosis { get; private set; } = null!;
        public string TreatmentGiven { get; private set; } = null!;
        public string FollowUpInstructions { get; private set; } = null!;
        public DateTime IssuedOnUtc { get; private set; }
        public string IssuedByDoctorStaffId { get; private set; } = null!;

        // Constructor for EF Core
        private DischargeSummary() : base(Guid.Empty) { }

        private DischargeSummary(Guid id, AdmissionId admissionId, PatientId patientId, string finalDiagnosis,
            string treatmentGiven, string followUpInstructions, string issuedByDoctorStaffId) : base(id)
        {
            AdmissionId = admissionId;
            PatientId = patientId;
            FinalDiagnosis = finalDiagnosis;
            TreatmentGiven = treatmentGiven;
            FollowUpInstructions = followUpInstructions;
            IssuedByDoctorStaffId = issuedByDoctorStaffId;
            IssuedOnUtc = DateTime.UtcNow;
        }

        public static DischargeSummary Issue(AdmissionId admissionId, PatientId patientId, string finalDiagnosis,
            string treatmentGiven, string followUpInstructions, string issuedByDoctorStaffId)
        {
            if (string.IsNullOrWhiteSpace(finalDiagnosis)) throw new DomainException("Final diagnosis is required.");
            if (string.IsNullOrWhiteSpace(treatmentGiven)) throw new DomainException("Treatment given is required.");

            var summary = new DischargeSummary(Guid.NewGuid(), admissionId, patientId, finalDiagnosis.Trim(),
                treatmentGiven.Trim(), followUpInstructions?.Trim() ?? string.Empty, issuedByDoctorStaffId);

            summary.AddDomainEvent(new DischargeSummaryIssuedDomainEvent(summary.Id, admissionId, patientId));
            return summary;
        }

        public void AmendFollowUpInstructions(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
                throw new DomainException("Follow-up instructions cannot be empty.");

            FollowUpInstructions = instructions.Trim();
        }
    }
}
