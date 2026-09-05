using HospitalSystem.Domain.Common;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, string currency = "USD")
        {
            if (amount < 0)
                throw new DomainException("Amount cannot be negative.");

            if (string.IsNullOrWhiteSpace(currency))
                throw new DomainException("Currency is required.");

            var normalizedCurrency = currency.Trim().ToUpperInvariant();

            if (normalizedCurrency.Length != 3 || normalizedCurrency.Any(c => c is < 'A' or > 'Z'))
                throw new DomainException("Currency must be a valid 3-letter ISO currency code.");

            return new Money(decimal.Round(amount, 2, MidpointRounding.ToEven), normalizedCurrency);
        }

        public static Money Zero(string currency = "USD") => Create(0m, currency);

        public Money Add(Money other)
        {
            ArgumentNullException.ThrowIfNull(other);
            EnsureSameCurrency(other);
            return Create(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            ArgumentNullException.ThrowIfNull(other);
            EnsureSameCurrency(other);

            if (other.Amount > Amount)
                throw new DomainException("Resulting amount cannot be negative.");

            return Create(Amount - other.Amount, Currency);
        }

        public Money Multiply(int factor)
        {
            if (factor < 0)
                throw new DomainException("Multiplication factor cannot be negative.");

            return Create(Amount * factor, Currency);
        }

        public Money Multiply(decimal factor)
        {
            if (factor < 0)
                throw new DomainException("Multiplication factor cannot be negative.");

            return Create(Amount * factor, Currency);
        }

        public bool IsGreaterThan(Money other)
        {
            ArgumentNullException.ThrowIfNull(other);
            EnsureSameCurrency(other);
            return Amount > other.Amount;
        }

        public bool IsGreaterThanOrEqualTo(Money other)
        {
            ArgumentNullException.ThrowIfNull(other);
            EnsureSameCurrency(other);
            return Amount >= other.Amount;
        }

        private void EnsureSameCurrency(Money other)
        {
            if (!string.Equals(Currency, other.Currency, StringComparison.Ordinal))
                throw new DomainException("Cannot operate on two different currencies.");
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        public override string ToString() => $"{Amount:F2} {Currency}";
    }


}

