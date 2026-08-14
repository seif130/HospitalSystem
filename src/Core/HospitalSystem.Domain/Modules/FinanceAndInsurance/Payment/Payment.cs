using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.Payment.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.Payment
{
    public sealed class Payment : AggregateRoot<PaymentId>
    {
        public InvoiceId InvoiceId { get; private set; } = null!;
        public Money Amount { get; private set; } = null!;
        public PaymentMethod Method { get; private set; }
        public DateTime PaidOnUtc { get; private set; }
        public string? ReferenceNumber { get; private set; }

        private Payment() { }

        private Payment(PaymentId id, InvoiceId invoiceId, Money amount, PaymentMethod method, string? referenceNumber) : base(id)
        {
            InvoiceId = invoiceId;
            Amount = amount;
            Method = method;
            ReferenceNumber = referenceNumber;
            PaidOnUtc = DateTime.UtcNow;
        }

        public static Payment Record(InvoiceId invoiceId, Money amount, PaymentMethod method, string? referenceNumber = null)
        {
            if (amount.Amount <= 0) throw new DomainException("Payment amount must be greater than zero.");
            var payment = new Payment(PaymentId.New(), invoiceId, amount, method, referenceNumber);
            payment.RaiseDomainEvent(new PaymentRecordedDomainEvent(payment.Id, invoiceId, amount));
            return payment;
        }
    }
}
