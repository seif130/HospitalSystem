using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Telemedicine.TelemedicinePrescription
{
    public sealed class TelemedicinePrescription : AggregateRoot<TelemedicinePrescriptionId>
    {
        public TelemedicineSessionId SessionId { get; private set; } = null!;
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId PrescribingDoctorId { get; private set; } = null!;
        public string MedicationName { get; private set; } = null!;
        public string Dosage { get; private set; } = null!;
        public string Instructions { get; private set; } = null!;
        public DateTime IssuedOnUtc { get; private set; }

        private TelemedicinePrescription() { }

        private TelemedicinePrescription(TelemedicinePrescriptionId id, TelemedicineSessionId sessionId, PatientId patientId,
            DoctorId prescribingDoctorId, string medicationName, string dosage, string instructions) : base(id)
        {
            SessionId = sessionId;
            PatientId = patientId;
            PrescribingDoctorId = prescribingDoctorId;
            MedicationName = medicationName;
            Dosage = dosage;
            Instructions = instructions;
            IssuedOnUtc = DateTime.UtcNow;
        }

        public static TelemedicinePrescription Issue(TelemedicineSessionId sessionId, PatientId patientId, DoctorId prescribingDoctorId,
            string medicationName, string dosage, string instructions)
        {
            if (string.IsNullOrWhiteSpace(medicationName)) throw new DomainException("Medication name is required.");
            if (string.IsNullOrWhiteSpace(dosage)) throw new DomainException("Dosage is required.");
            var prescription = new TelemedicinePrescription(TelemedicinePrescriptionId.New(), sessionId, patientId,
                prescribingDoctorId, medicationName.Trim(), dosage.Trim(), instructions?.Trim() ?? string.Empty);
            prescription.RaiseDomainEvent(new TelemedicinePrescriptionIssuedDomainEvent(prescription.Id, sessionId, patientId));
            return prescription;
        }
    }
}
