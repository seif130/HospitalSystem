using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.InsurancePolicy.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.InsurancePolicy
{
    public sealed class InsurancePolicy : AggregateRoot<InsurancePolicyId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public InsuranceProviderId ProviderId { get; private set; } = null!;
        public string PolicyNumber { get; private set; } = null!;
        public Money CoverageLimit { get; private set; } = null!;
        public decimal CoveragePercentage { get; private set; }
        public DateTime EffectiveFromUtc { get; private set; }
        public DateTime EffectiveToUtc { get; private set; }
        public PolicyStatus Status { get; private set; }

        private InsurancePolicy() { }

        private InsurancePolicy(InsurancePolicyId id, PatientId patientId, InsuranceProviderId providerId, string policyNumber,
            Money coverageLimit, decimal coveragePercentage, DateTime effectiveFromUtc, DateTime effectiveToUtc) : base(id)
        {
            PatientId = patientId;
            ProviderId = providerId;
            PolicyNumber = policyNumber;
            CoverageLimit = coverageLimit;
            CoveragePercentage = coveragePercentage;
            EffectiveFromUtc = effectiveFromUtc;
            EffectiveToUtc = effectiveToUtc;
            Status = PolicyStatus.Active;
        }

        public static InsurancePolicy Issue(PatientId patientId, InsuranceProviderId providerId, string policyNumber,
            Money coverageLimit, decimal coveragePercentage, DateTime effectiveFromUtc, DateTime effectiveToUtc)
        {
            if (string.IsNullOrWhiteSpace(policyNumber)) throw new DomainException("Policy number is required.");
            if (coveragePercentage is < 0 or > 100) throw new DomainException("Coverage percentage must be between 0 and 100.");
            if (effectiveToUtc <= effectiveFromUtc) throw new DomainException("Policy end date must be after the start date.");
            return new InsurancePolicy(InsurancePolicyId.New(), patientId, providerId, policyNumber.Trim(),
                coverageLimit, coveragePercentage, effectiveFromUtc, effectiveToUtc);
        }

        public bool IsValidOn(DateTime moment) =>
            Status == PolicyStatus.Active && moment >= EffectiveFromUtc && moment <= EffectiveToUtc;

        public Money CalculateCoveredAmount(Money billedAmount)
        {
            var covered = billedAmount.Multiply(CoveragePercentage / 100m);
            return covered.Amount > CoverageLimit.Amount ? CoverageLimit : covered;
        }

        public void Cancel() => Status = PolicyStatus.Cancelled;

        public void ExpireIfPastEndDate(DateTime asOfUtc)
        {
            if (Status == PolicyStatus.Active && asOfUtc > EffectiveToUtc)
                Status = PolicyStatus.Expired;
        }
    }
}
