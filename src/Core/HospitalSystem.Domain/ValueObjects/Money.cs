using HospitalSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.ValueObjects
{
   public record Money
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }

        private Money()
        {
            Currency = "EGP";
        }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency.ToUpper();
        }

        public static Result<Money> Create(decimal amount, string currency = "EGP")
        {
            var errors = new List<Error>();

            if (amount < 0)
                errors.Add(Error.Validation("Money.NegativeAmount", "Amount cannot be negative."));

            if (string.IsNullOrWhiteSpace(currency))
                errors.Add(Error.Validation("Money.EmptyCurrency", "Currency is required."));

            if (errors.Any())
                return Result<Money>.Fail(errors);

            return Result<Money>.Ok(new Money(amount, currency));
        }

        public static Money Zero(string currency = "EGP") => new(0, currency.ToUpper());
    }
}
