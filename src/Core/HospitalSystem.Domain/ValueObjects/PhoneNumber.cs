using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        public string Value { get; }
        private PhoneNumber(string value) => Value = value;

        public static PhoneNumber Create(string value)
        {
            var digits = new string(value.Where(char.IsDigit).ToArray());
            if (digits.Length < 7) throw new DomainException("Invalid phone number.");
            return new PhoneNumber(value.Trim());
        }

        protected override IEnumerable<object?> GetEqualityComponents() { yield return Value; }
        public override string ToString() => Value;
    }
}
