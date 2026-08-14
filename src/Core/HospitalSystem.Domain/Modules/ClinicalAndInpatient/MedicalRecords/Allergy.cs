using HospitalSystem.Domain.Modules.Clinic.MedicalRecords.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.MedicalRecords
{
    public sealed class Allergy
    {
        public string Allergen { get; private set; }
        public AllergySeverity Severity { get; private set; }
        public string? Reaction { get; private set; }
        public DateTime RecordedOnUtc { get; private set; }

        internal Allergy(string allergen, AllergySeverity severity, string? reaction)
        {
            if (string.IsNullOrWhiteSpace(allergen))
                throw new DomainException("Allergen is required.");

            Allergen = allergen.Trim();
            Severity = severity;
            Reaction = reaction?.Trim();
            RecordedOnUtc = DateTime.UtcNow;
        }
    }
}
