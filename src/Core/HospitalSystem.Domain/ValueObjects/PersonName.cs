using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class PersonName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string? MiddleName { get; }

        private PersonName(string firstName, string lastName, string? middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public static PersonName Create(string firstName, string lastName, string? middleName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new DomainException("First name is required.");
            if (string.IsNullOrWhiteSpace(lastName)) throw new DomainException("Last name is required.");
            return new PersonName(firstName.Trim(), lastName.Trim(), middleName?.Trim());
        }

        public string FullName => string.IsNullOrWhiteSpace(MiddleName)
            ? $"{FirstName} {LastName}"
            : $"{FirstName} {MiddleName} {LastName}";

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return MiddleName;
        }

        public override string ToString() => FullName;
    }
}
