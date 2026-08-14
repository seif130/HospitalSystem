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
            Amount = amount; Currency = currency;
        }

        public static Money Create(decimal amount, string currency = "USD")
        {
            if (amount < 0) throw new DomainException("Amount cannot be negative.");
            if (string.IsNullOrWhiteSpace(currency)) throw new DomainException("Currency is required.");
            return new Money(amount, currency.ToUpperInvariant());
        }

        public static Money Zero(string currency = "USD") => new(0, currency);

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);
            var result = Amount - other.Amount;
            if (result < 0) throw new DomainException("Resulting amount cannot be negative.");
            return new Money(result, Currency);
        }

        public Money Multiply(decimal factor) => new(Amount * factor, Currency);

        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency) throw new DomainException("Cannot operate on two different currencies.");
        }

        protected override IEnumerable<object?> GetEqualityComponents() { yield return Amount; yield return Currency; }
        public override string ToString() => $"{Amount:F2} {Currency}";
    }

}

