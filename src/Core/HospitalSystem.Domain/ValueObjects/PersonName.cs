using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class PersonName : ValueObject
    {
        public string FirstName { get; }
        public string? MiddleName { get; }
        public string LastName { get; }

        private PersonName(string firstName, string? middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public static PersonName Create( string firstName, string lastName, string? middleName = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainException("First name is required.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainException("Last name is required.");

            return new PersonName(
                firstName.Trim(), NormalizeOptional(middleName), lastName.Trim());
        }

        public string FullName
            => string.IsNullOrWhiteSpace(MiddleName)
                ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return FirstName;
            yield return MiddleName;
            yield return LastName;
        }

        public override string ToString() => FullName;

        private static string? NormalizeOptional(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }
    }


}
