

using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Street { get; }
        public string City { get; }
        public string? State { get; }
        public string Country { get; }
        public string? PostalCode { get; }

        private Address(string street, string city, string? state, string country, string? postalCode)
        {
            Street = street; City = city; State = state; Country = country; PostalCode = postalCode;
        }

        public static Address Create(string street, string city, string country, string? state = null, string? postalCode = null)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new DomainException("Street is required.");
            if (string.IsNullOrWhiteSpace(city)) throw new DomainException("City is required.");
            if (string.IsNullOrWhiteSpace(country)) throw new DomainException("Country is required.");
            return new Address(street.Trim(), city.Trim(), state?.Trim(), country.Trim(), postalCode?.Trim());
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Street; yield return City; yield return State; yield return Country; yield return PostalCode;
        }
    }
}
