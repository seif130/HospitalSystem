using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.InsuranceProvider
{
    public sealed class InsuranceProvider : AggregateRoot<InsuranceProviderId>
    {
        public string Name { get; private set; } = null!;
        public string PayerId { get; private set; } = null!;
        public PhoneNumber ContactPhone { get; private set; } = null!;
        public EmailAddress ContactEmail { get; private set; } = null!;
        public bool IsActive { get; private set; } = true;

        private InsuranceProvider() { }

        private InsuranceProvider(InsuranceProviderId id, string name, string payerId, PhoneNumber contactPhone, EmailAddress contactEmail) : base(id)
        {
            Name = name;
            PayerId = payerId;
            ContactPhone = contactPhone;
            ContactEmail = contactEmail;
        }

        public static InsuranceProvider Register(string name, string payerId, PhoneNumber contactPhone, EmailAddress contactEmail)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Provider name is required.");
            if (string.IsNullOrWhiteSpace(payerId)) throw new DomainException("Payer ID is required.");
            return new InsuranceProvider(InsuranceProviderId.New(), name.Trim(), payerId.Trim(), contactPhone, contactEmail);
        }

        public void Deactivate() => IsActive = false;
        public void Reactivate() => IsActive = true;
    }
}
