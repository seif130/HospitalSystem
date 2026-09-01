using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.DiscountAdjustment.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.DiscountAdjustment
{
    public sealed class DiscountAdjustment : AggregateRoot<DiscountAdjustmentId>
    {
        public InvoiceId InvoiceId { get; private set; } = null!;
        public AdjustmentType Type { get; private set; }
        public Money Amount { get; private set; } = null!;
        public string Reason { get; private set; } = null!;
        public string ApprovedByStaffId { get; private set; } = null!;
        public DateTime AppliedOnUtc { get; private set; }

        private DiscountAdjustment() { }

        private DiscountAdjustment(DiscountAdjustmentId id, InvoiceId invoiceId, AdjustmentType type, Money amount,
            string reason, string approvedByStaffId) : base(id)
        {
            InvoiceId = invoiceId;
            Type = type;
            Amount = amount;
            Reason = reason;
            ApprovedByStaffId = approvedByStaffId;
            AppliedOnUtc = DateTime.UtcNow;
        }

        public static DiscountAdjustment Apply(InvoiceId invoiceId, AdjustmentType type, Money amount, string reason, string approvedByStaffId)
        {
            if (amount.Amount <= 0) throw new DomainException("Adjustment amount must be greater than zero.");
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Adjustment reason is required.");
            if (string.IsNullOrWhiteSpace(approvedByStaffId)) throw new DomainException("Approving staff member is required.");
            return new DiscountAdjustment(DiscountAdjustmentId.New(), invoiceId, type, amount, reason.Trim(), approvedByStaffId);
        }
    }
}
