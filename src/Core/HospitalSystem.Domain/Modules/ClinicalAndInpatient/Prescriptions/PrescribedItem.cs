using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Clinic.Prescriptions
{
    public sealed class PrescribedItem
    {
        public MedicineId MedicineId { get; private set; } 
        public Dosage Dosage { get; private set; }
        public string Instructions { get; private set; }
        public int DurationInDays { get; private set; }

        internal PrescribedItem(MedicineId medicineId, Dosage dosage, string instructions, int durationInDays)
        {
            if (durationInDays <= 0) throw new DomainException("Duration must be at least 1 day.");
            MedicineId = medicineId;
            Dosage = dosage;
            Instructions = instructions.Trim();
            DurationInDays = durationInDays;
        }
    }
}
