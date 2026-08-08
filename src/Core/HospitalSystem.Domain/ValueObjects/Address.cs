

namespace HospitalSystem.Domain.ValueObjects
{
    public readonly record struct Address
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }  

        private Address(string street, string city, string state, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Create(string street, string city, string state, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street cannot be empty.", nameof(street));
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be empty.", nameof(city));

            return new Address(street.Trim(), city.Trim(), state.Trim(), zipCode.Trim());
        }
    }
}
