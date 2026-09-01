using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.InsuranceClaim.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.InsuranceClaim
{
    public sealed class InsuranceClaim : AggregateRoot<InsuranceClaimId>
    {
        public InvoiceId InvoiceId { get; private set; } = null!;
        public PatientId PatientId { get; private set; } = null!;
        public InsurancePolicyId PolicyId { get; private set; } = null!;
        public Money ClaimedAmount { get; private set; } = null!;
        public Money? ApprovedAmount { get; private set; }
        public ClaimStatus Status { get; private set; }
        public string? RejectionReason { get; private set; }

        private InsuranceClaim() { }

        private InsuranceClaim(InsuranceClaimId id, InvoiceId invoiceId, PatientId patientId,
            InsurancePolicyId policyId, Money claimedAmount) : base(id)
        {
            InvoiceId = invoiceId;
            PatientId = patientId;
            PolicyId = policyId;
            ClaimedAmount = claimedAmount;
            Status = ClaimStatus.Submitted;
        }

        public static InsuranceClaim Submit(InvoiceId invoiceId, PatientId patientId, InsurancePolicyId policyId, Money claimedAmount)
        {
            var claim = new InsuranceClaim(InsuranceClaimId.New(), invoiceId, patientId, policyId, claimedAmount);
            claim.AddDomainEvent(new InsuranceClaimSubmittedDomainEvent(claim.Id, invoiceId, claimedAmount));
            return claim;
        }

        public void BeginReview()
        {
            if (Status != ClaimStatus.Submitted) throw new DomainException("Only a submitted claim can enter review.");
            Status = ClaimStatus.UnderReview;
        }

        public void Approve(Money approvedAmount)
        {
            if (Status != ClaimStatus.UnderReview) throw new DomainException("Claim must be under review to approve.");
            ApprovedAmount = approvedAmount;
            Status = approvedAmount.Amount >= ClaimedAmount.Amount ? ClaimStatus.Approved : ClaimStatus.PartiallyApproved;
        }

        public void Reject(string reason)
        {
            if (Status != ClaimStatus.UnderReview) throw new DomainException("Claim must be under review to reject.");
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Rejection reason is required.");
            Status = ClaimStatus.Rejected;
            RejectionReason = reason.Trim();
        }
    }
}
