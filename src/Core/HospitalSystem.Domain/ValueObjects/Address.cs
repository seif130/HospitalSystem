

using HospitalSystem.Domain.Primitives;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Street { get; private set; } = null!;
        public string City { get; private set; } = null!;
        public string? State { get; private set; }
        public string Country { get; private set; } = null!;
        public string? PostalCode { get; private set; }

        private Address()
        {
            // Required by EF Core
        }

        private Address(
            string street,
            string city,
            string country,
            string? state,
            string? postalCode)
        {
            Street = street;
            City = city;
            Country = country;
            State = state;
            PostalCode = postalCode;
        }

        public static Address Create(
            string street,
            string city,
            string country,
            string? state = null,
            string? postalCode = null)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new DomainException("Street is required.");

            if (string.IsNullOrWhiteSpace(city))
                throw new DomainException("City is required.");

            if (string.IsNullOrWhiteSpace(country))
                throw new DomainException("Country is required.");

            return new Address(
                street.Trim(),
                city.Trim(),
                country.Trim(),
                NormalizeOptional(state),
                NormalizeOptional(postalCode));
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return PostalCode;
        }

        private static string? NormalizeOptional(string? value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? null
                : value.Trim();
        }
    }

}
