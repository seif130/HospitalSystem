using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.Refund
{
    public sealed class Refund : AggregateRoot<RefundId>
    {
        public PaymentId OriginalPaymentId { get; private set; } = null!;
        public Money Amount { get; private set; } = null!;
        public string Reason { get; private set; } = null!;
        public RefundStatus Status { get; private set; }
        public DateTime RequestedOnUtc { get; private set; }
        public DateTime? ProcessedOnUtc { get; private set; }

        private Refund() { }

        private Refund(RefundId id, PaymentId originalPaymentId, Money amount, string reason) : base(id)
        {
            OriginalPaymentId = originalPaymentId;
            Amount = amount;
            Reason = reason;
            Status = RefundStatus.Requested;
            RequestedOnUtc = DateTime.UtcNow;
        }

        public static Refund Request(PaymentId originalPaymentId, Money amount, string reason)
        {
            if (amount.Amount <= 0) throw new DomainException("Refund amount must be greater than zero.");
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Refund reason is required.");
            return new Refund(RefundId.New(), originalPaymentId, amount, reason.Trim());
        }

        public void Approve()
        {
            if (Status != RefundStatus.Requested) throw new DomainException("Only a requested refund can be approved.");
            Status = RefundStatus.Approved;
        }

        public void MarkProcessed()
        {
            if (Status != RefundStatus.Approved) throw new DomainException("Refund must be approved before processing.");
            Status = RefundStatus.Processed;
            ProcessedOnUtc = DateTime.UtcNow;
            RaiseDomainEvent(new RefundProcessedDomainEvent(Id, OriginalPaymentId, Amount));
        }

        public void Reject(string reason)
        {
            if (Status != RefundStatus.Requested) throw new DomainException("Only a requested refund can be rejected.");
            Status = RefundStatus.Rejected;
            Reason = reason.Trim();
        }
    }
}
