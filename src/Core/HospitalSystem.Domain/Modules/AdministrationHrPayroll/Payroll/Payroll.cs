using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.Payroll.Enum;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.Payroll
{
    public sealed class Payroll : AggregateRoot<PayrollId>
    {
        public StaffId StaffId { get; private set; } = null!;
        public DateRange Period { get; private set; } = null!;
        public Money GrossAmount { get; private set; } = null!;
        public PayrollStatus Status { get; private set; }

        private readonly List<PayrollDeductionLine> _deductions = new();
        public IReadOnlyCollection<PayrollDeductionLine> Deductions => _deductions.AsReadOnly();

        public Money NetAmount => _deductions.Aggregate(GrossAmount, (net, d) => net.Subtract(d.Amount));

        private Payroll() { }

        private Payroll(PayrollId id, StaffId staffId, DateRange period, Money grossAmount) : base(id)
        {
            StaffId = staffId;
            Period = period;
            GrossAmount = grossAmount;
            Status = PayrollStatus.Draft;
        }

        public static Payroll CreateDraft(StaffId staffId, DateRange period, Money grossAmount) =>
            new(PayrollId.New(), staffId, period, grossAmount);

        public void AddDeduction(string reason, Money amount)
        {
            EnsureDraft();
            _deductions.Add(new PayrollDeductionLine(reason, amount));
        }

        public void Approve()
        {
            EnsureDraft();
            Status = PayrollStatus.Approved;
        }

        public void MarkPaid()
        {
            if (Status != PayrollStatus.Approved) throw new DomainException("Payroll must be approved before it can be paid.");
            Status = PayrollStatus.Paid;
            AddDomainEvent(new PayrollPaidDomainEvent(Id, StaffId, NetAmount));
        }

        private void EnsureDraft()
        {
            if (Status != PayrollStatus.Draft) throw new DomainException("Only a draft payroll can be modified.");
        }
    }

}
