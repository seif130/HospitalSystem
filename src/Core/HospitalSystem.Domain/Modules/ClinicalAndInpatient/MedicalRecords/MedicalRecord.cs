using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Clinic.MedicalRecords.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.MedicalRecords
{
    public sealed class MedicalRecord : AggregateRoot<MedicalRecordId>
    {
        public PatientId PatientId { get; private set; } = null!;

        private readonly List<Diagnosis> _diagnoses = new();
        public IReadOnlyCollection<Diagnosis> Diagnoses => _diagnoses.AsReadOnly();

        private readonly List<ClinicalNote> _notes = new();
        public IReadOnlyCollection<ClinicalNote> Notes => _notes.AsReadOnly();

        private readonly List<VitalSign> _vitalSigns = new();
        public IReadOnlyCollection<VitalSign> VitalSigns => _vitalSigns.AsReadOnly();

        private readonly List<Allergy> _allergies = new();
        public IReadOnlyCollection<Allergy> Allergies => _allergies.AsReadOnly();

        private readonly List<Immunization> _immunizations = new();
        public IReadOnlyCollection<Immunization> Immunizations => _immunizations.AsReadOnly();

        private MedicalRecord() : base(MedicalRecordId.New()) { }

        private MedicalRecord(MedicalRecordId id, PatientId patientId) : base(id) => PatientId = patientId;

        public static MedicalRecord OpenFor(PatientId patientId) => new(MedicalRecordId.New(), patientId);

        public void AddDiagnosis(string code, string description)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new DomainException("Diagnosis code is required.");
            _diagnoses.Add(new Diagnosis(code, description, DateTime.UtcNow));
            AddDomainEvent(new MedicalRecordUpdatedDomainEvent(Id, PatientId));
        }

        public void AddClinicalNote(string authorName, string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new DomainException("Clinical note text cannot be empty.");
            _notes.Add(new ClinicalNote(authorName, text.Trim(), DateTime.UtcNow));
            AddDomainEvent(new MedicalRecordUpdatedDomainEvent(Id, PatientId));
        }

        public void RecordVitalSigns(decimal temperature, int systolicBp, int diastolicBp, int pulseBpm)
        {
            if (systolicBp <= diastolicBp) throw new DomainException("Systolic BP must be greater than diastolic BP.");
            _vitalSigns.Add(new VitalSign(temperature, systolicBp, diastolicBp, pulseBpm, DateTime.UtcNow));
        }

        public void AddAllergy(string allergen, AllergySeverity severity, string? reaction = null)
        {
            if (_allergies.Any(a => a.Allergen.Equals(allergen, StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Allergy to '{allergen}' is already recorded.");
            _allergies.Add(new Allergy(allergen, severity, reaction));
            if (severity == AllergySeverity.LifeThreatening)
                AddDomainEvent(new LifeThreateningAllergyRecordedDomainEvent(Id, PatientId, allergen));
        }

        public void AddImmunization(string vaccineName, DateTime administeredOnUtc, string administeredByStaffId, DateTime? nextDoseDueUtc = null)
        {
            _immunizations.Add(new Immunization(vaccineName, administeredOnUtc, administeredByStaffId, nextDoseDueUtc));
        }
    }

}
