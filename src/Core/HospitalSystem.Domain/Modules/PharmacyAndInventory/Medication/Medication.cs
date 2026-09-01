using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.Medication
{
    public sealed class Medication : AggregateRoot<MedicationId>
    {
        public string Name { get; private set; } = null!;
        public string Manufacturer { get; private set; } = null!;
        public string Unit { get; private set; } = null!; // e.g. "mg", "ml", "tablet"
        public bool RequiresPrescription { get; private set; }

        private Medication() { }

        private Medication(MedicationId id, string name, string manufacturer, string unit, bool requiresPrescription) : base(id)
        {
            Name = name;
            Manufacturer = manufacturer;
            Unit = unit;
            RequiresPrescription = requiresPrescription;
        }

        public static Medication Create(string name, string manufacturer, string unit, bool requiresPrescription)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Medication name is required.");
            return new Medication(MedicationId.New(), name.Trim(), manufacturer.Trim(), unit.Trim(), requiresPrescription);
        }
    }
}
