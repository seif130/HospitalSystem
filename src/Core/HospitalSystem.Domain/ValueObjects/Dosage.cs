using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{

    public readonly record struct Dosage
    {
        public decimal Quantity { get; }
        public string Unit { get; }

        private Dosage(decimal quantity, string unit)
        {
            Quantity = quantity;
            Unit = unit;
        }

        public static Dosage Create(decimal quantity, string unit)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity), "Dosage quantity must be positive.");
            if (string.IsNullOrWhiteSpace(unit)) throw new ArgumentException("Unit is required.", nameof(unit));

            return new Dosage(quantity, unit.ToLower().Trim());
        }
    }
}
