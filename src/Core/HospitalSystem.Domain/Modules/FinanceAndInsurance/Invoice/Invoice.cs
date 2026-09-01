using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.FinanceAndInsurance.Invoice.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.FinanceAndInsurance.Invoice
{
    public sealed class Invoice : AggregateRoot<InvoiceId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public InvoiceStatus Status { get; private set; }
        public DateTime IssuedOnUtc { get; private set; }
        public string Currency { get; private set; } = "USD";

        private readonly List<InvoiceLineItem> _lineItems = new();
        public IReadOnlyCollection<InvoiceLineItem> LineItems => _lineItems.AsReadOnly();

        public Money Total => _lineItems.Aggregate(Money.Zero(Currency), (sum, li) => sum.Add(li.LineTotal));

        private Money _amountPaid;
        public Money AmountPaid => _amountPaid;
        public Money Balance => Total.Subtract(_amountPaid);

        private Invoice() { _amountPaid = Money.Zero(); }

        private Invoice(InvoiceId id, PatientId patientId, string currency) : base(id)
        {
            PatientId = patientId;
            Currency = currency;
            Status = InvoiceStatus.Draft;
            IssuedOnUtc = DateTime.UtcNow;
            _amountPaid = Money.Zero(currency);
        }

        public static Invoice CreateDraft(PatientId patientId, string currency = "USD") =>
            new(InvoiceId.New(), patientId, currency);

        public void AddLineItem(string description, Money unitPrice, int quantity)
        {
            EnsureEditable();
            if (quantity <= 0) throw new DomainException("Quantity must be greater than zero.");
            _lineItems.Add(new InvoiceLineItem(description, unitPrice, quantity));
        }

        public void Issue()
        {
            if (_lineItems.Count == 0) throw new DomainException("Cannot issue an invoice with no line items.");
            Status = InvoiceStatus.Issued;
            AddDomainEvent(new InvoiceIssuedDomainEvent(Id, PatientId, Total));
        }

        public void ApplyPayment(Money amount)
        {
            if (Status is InvoiceStatus.Draft or InvoiceStatus.Voided)
                throw new DomainException("Cannot apply payment to a draft or voided invoice.");
            _amountPaid = _amountPaid.Add(amount);
            Status = _amountPaid.Amount >= Total.Amount ? InvoiceStatus.Paid : InvoiceStatus.PartiallyPaid;
        }

        public void Void()
        {
            if (Status == InvoiceStatus.Paid) throw new DomainException("Cannot void a fully paid invoice.");
            Status = InvoiceStatus.Voided;
        }

        private void EnsureEditable()
        {
            if (Status != InvoiceStatus.Draft) throw new DomainException("Only a draft invoice can be modified.");
        }
    }
}
