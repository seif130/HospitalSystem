using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class Dosage : ValueObject
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
            if (quantity <= 0)
                throw new DomainException("Dosage quantity must be greater than zero.");

            if (string.IsNullOrWhiteSpace(unit))
                throw new DomainException("Dosage unit is required.");

            return new Dosage(quantity, unit.Trim().ToLowerInvariant());
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Quantity;
            yield return Unit;
        }

        public override string ToString() => $"{Quantity} {Unit}";
    }
}