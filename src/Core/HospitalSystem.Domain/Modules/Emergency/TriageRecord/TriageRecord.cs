using HospitalSystem.Domain.Modules.Emergency.TriageRecord.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Emergency.TriageRecord
{
    public sealed class TriageRecord
    {
        public TriageLevel Level { get; }
        public string AssessedByStaffId { get; }
        public string PresentingComplaint { get; }
        public DateTime AssessedOnUtc { get; }

        internal TriageRecord(TriageLevel level, string assessedByStaffId, string presentingComplaint)
        {
            if (string.IsNullOrWhiteSpace(presentingComplaint)) throw new DomainException("Presenting complaint is required.");
            Level = level;
            AssessedByStaffId = assessedByStaffId;
            PresentingComplaint = presentingComplaint.Trim();
            AssessedOnUtc = DateTime.UtcNow;
        }
    }
}
