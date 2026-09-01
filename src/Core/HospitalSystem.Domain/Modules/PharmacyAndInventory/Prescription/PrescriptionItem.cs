using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.Prescription
{
    public sealed class PrescriptionItem
    {
        public MedicationId MedicationId { get; }
        public string Dosage { get; }
        public string Frequency { get; }
        public int DurationInDays { get; }
        public bool IsDispensed { get; private set; }

        internal PrescriptionItem(MedicationId medicationId, string dosage, string frequency, int durationInDays)
        {
            MedicationId = medicationId;
            Dosage = dosage;
            Frequency = frequency;
            DurationInDays = durationInDays;
        }

        internal void MarkDispensed() => IsDispensed = true;
    }
}
