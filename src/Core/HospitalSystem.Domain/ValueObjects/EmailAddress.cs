using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class EmailAddress : ValueObject
    {
        public string Value { get; }

        private EmailAddress(string value)
        {
            Value = value;
        }

        public static EmailAddress Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Email address is required.");

            value = value.Trim().ToLowerInvariant();

            if (!IsValidEmail(value))
                throw new DomainException("Invalid email address.");

            return new EmailAddress(value);
        }

        private static bool IsValidEmail(string value)
        {
            var atIndex = value.IndexOf('@');

            if (atIndex <= 0)
                return false;

            if (atIndex != value.LastIndexOf('@'))
                return false;

            var domain = value[(atIndex + 1)..];

            if (string.IsNullOrWhiteSpace(domain))
                return false;

            if (!domain.Contains('.'))
                return false;

            if (domain.StartsWith('.') || domain.EndsWith('.'))
                return false;

            return true;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
            => Value;
    }

}
