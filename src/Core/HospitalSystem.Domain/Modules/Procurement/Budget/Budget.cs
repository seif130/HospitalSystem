using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Procurement.Budget
{
    public sealed class Budget : AggregateRoot<BudgetId>
    {
        public DepartmentId DepartmentId { get; private set; } = null!;
        public DateRange FiscalPeriod { get; private set; } = null!;
        public Money AllocatedAmount { get; private set; } = null!;

        private readonly List<BudgetExpenseLine> _expenses = new();
        public IReadOnlyCollection<BudgetExpenseLine> Expenses => _expenses.AsReadOnly();

        public Money SpentAmount => _expenses.Aggregate(Money.Zero(AllocatedAmount.Currency), (sum, e) => sum.Add(e.Amount));
        public Money RemainingAmount => AllocatedAmount.Subtract(SpentAmount.Amount > AllocatedAmount.Amount ? AllocatedAmount : SpentAmount);

        private Budget() { }

        private Budget(BudgetId id, DepartmentId departmentId, DateRange fiscalPeriod, Money allocatedAmount) : base(id)
        {
            DepartmentId = departmentId;
            FiscalPeriod = fiscalPeriod;
            AllocatedAmount = allocatedAmount;
        }

        public static Budget Allocate(DepartmentId departmentId, DateRange fiscalPeriod, Money allocatedAmount)
        {
            if (fiscalPeriod.IsOpen) throw new DomainException("A budget must have a defined fiscal period end date.");
            if (allocatedAmount.Amount <= 0) throw new DomainException("Allocated amount must be greater than zero.");
            return new Budget(BudgetId.New(), departmentId, fiscalPeriod, allocatedAmount);
        }

        public void RecordExpense(string description, Money amount)
        {
            if (string.IsNullOrWhiteSpace(description)) throw new DomainException("Expense description is required.");
            var projectedSpend = SpentAmount.Add(amount);
            _expenses.Add(new BudgetExpenseLine(description.Trim(), amount, DateTime.UtcNow));
            if (projectedSpend.Amount > AllocatedAmount.Amount)
                AddDomainEvent(new BudgetOverspentDomainEvent(Id, DepartmentId, projectedSpend, AllocatedAmount));
        }
    }
}
