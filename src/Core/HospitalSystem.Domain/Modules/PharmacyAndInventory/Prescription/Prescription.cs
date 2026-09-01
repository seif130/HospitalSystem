using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Clinic.Prescriptions.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.Prescription
{
    public sealed class Prescription : AggregateRoot<PrescriptionId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public DoctorId PrescribingDoctorId { get; private set; } = null!;
        public DateTime IssuedOnUtc { get; private set; }
        public PrescriptionStatus Status { get; private set; }

        private readonly List<PrescriptionItem> _items = new();
        public IReadOnlyCollection<PrescriptionItem> Items => _items.AsReadOnly();

        private Prescription() { }

        private Prescription(PrescriptionId id, PatientId patientId, DoctorId prescribingDoctorId) : base(id)
        {
            PatientId = patientId;
            PrescribingDoctorId = prescribingDoctorId;
            IssuedOnUtc = DateTime.UtcNow;
            Status = PrescriptionStatus.Issued;
        }

        public static Prescription Issue(PatientId patientId, DoctorId prescribingDoctorId) =>
            new(PrescriptionId.New(), patientId, prescribingDoctorId);

        public void AddItem(MedicationId medicationId, string dosage, string frequency, int durationInDays)
        {
            if (Status != PrescriptionStatus.Issued) throw new DomainException("Cannot modify a prescription once dispensing has begun.");
            if (durationInDays <= 0) throw new DomainException("Duration must be greater than zero.");
            _items.Add(new PrescriptionItem(medicationId, dosage, frequency, durationInDays));
        }

        public void DispenseItem(MedicationId medicationId)
        {
            var item = _items.FirstOrDefault(i => i.MedicationId == medicationId && !i.IsDispensed)
                ?? throw new DomainException("No matching un-dispensed item found on this prescription.");
            item.MarkDispensed();
            Status = _items.All(i => i.IsDispensed) ? PrescriptionStatus.Dispensed : PrescriptionStatus.PartiallyDispensed;
        }

        public void Cancel()
        {
            if (Status == PrescriptionStatus.Dispensed) throw new DomainException("Cannot cancel a fully dispensed prescription.");
            Status = PrescriptionStatus.Cancelled;
        }
    }
}
