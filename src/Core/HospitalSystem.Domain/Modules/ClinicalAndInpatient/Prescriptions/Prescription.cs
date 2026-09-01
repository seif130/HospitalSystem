using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Clinic.Prescriptions.Enums;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Prescriptions
{
    public sealed class Prescription : AggregateRoot<PrescriptionId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId PrescribedByDoctorId { get; private set; } = null!;
        public DateTime IssuedOnUtc { get; private set; }
        public PrescriptionStatus Status { get; private set; }

        private readonly List<PrescribedItem> _items = new();
        public IReadOnlyCollection<PrescribedItem> Items => _items.AsReadOnly();

        private Prescription() : base(PrescriptionId.New()) { }

        private Prescription(PrescriptionId id, PatientId patientId, DoctorId prescribedByDoctorId) : base(id)
        {
            PatientId = patientId;
            PrescribedByDoctorId = prescribedByDoctorId;
            IssuedOnUtc = DateTime.UtcNow;
            Status = PrescriptionStatus.Active;
        }

        public static Prescription Create(PatientId patientId, DoctorId prescribedByDoctorId)
        {
            var prescription = new Prescription(PrescriptionId.New(), patientId, prescribedByDoctorId);
            prescription.AddDomainEvent(new PrescriptionIssuedDomainEvent(prescription.Id, patientId));
            return prescription;
        }

        public void AddItem(MedicineId medicineId, Dosage dosage, string instructions, int durationInDays)
        {
            if (Status != PrescriptionStatus.Active)
                throw new DomainException("Cannot modify a prescription that is not active.");

            _items.Add(new PrescribedItem(medicineId, dosage, instructions, durationInDays));
        }

        public void Complete()
        {
            Status = PrescriptionStatus.Completed;
        }

        public void Cancel()
        {
            Status = PrescriptionStatus.Cancelled;
        }
    }
}
