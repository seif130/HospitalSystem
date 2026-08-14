using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class EmailAddress : ValueObject
    {
        public string Value { get; }
        private EmailAddress(string value) => Value = value;

        public static EmailAddress Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.Contains('@') || !value.Contains('.'))
                throw new DomainException("Invalid email address.");
            return new EmailAddress(value.Trim().ToLowerInvariant());
        }

        protected override IEnumerable<object?> GetEqualityComponents() { yield return Value; }
        public override string ToString() => Value;
    }
}
