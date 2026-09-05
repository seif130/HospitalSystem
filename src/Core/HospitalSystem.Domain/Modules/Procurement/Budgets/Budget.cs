using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;

namespace HospitalSystem.Domain.Modules.Procurement.Budgets;

public sealed class Budget : AggregateRoot<BudgetId>
{
    public DepartmentId DepartmentId { get; private set; }
    public DateRange FiscalPeriod { get; private set; } = null!;
    public Money AllocatedAmount { get; private set; } = null!;

    private readonly List<BudgetExpenseLine> _expenses = [];
    public IReadOnlyCollection<BudgetExpenseLine> Expenses => _expenses.AsReadOnly();

    public Money SpentAmount => _expenses.Count == 0
        ? Money.Zero(AllocatedAmount.Currency)
        : _expenses.Select(x => x.Amount).Aggregate(Money.Zero(AllocatedAmount.Currency), (sum, amount) => sum.Add(amount));

    public Money RemainingAmount => SpentAmount.IsGreaterThanOrEqualTo(AllocatedAmount)
            ? Money.Zero(AllocatedAmount.Currency)
            : AllocatedAmount.Subtract(SpentAmount);

    private Budget() { }

    private Budget(BudgetId id, DepartmentId departmentId, DateRange fiscalPeriod, Money allocatedAmount)
        : base(id)
    {
        DepartmentId = departmentId;
        FiscalPeriod = fiscalPeriod;
        AllocatedAmount = allocatedAmount;
    }

    public static Budget Allocate(DepartmentId departmentId, DateRange fiscalPeriod, Money allocatedAmount)
    {
        if (departmentId.IsEmpty) throw new DomainException("Department ID is required.");
        ArgumentNullException.ThrowIfNull(fiscalPeriod);
        ArgumentNullException.ThrowIfNull(allocatedAmount);
        if (fiscalPeriod.IsOpen) throw new DomainException("A budget must have a defined fiscal period end date.");
        if (allocatedAmount.Amount <= 0) throw new DomainException("Allocated amount must be greater than zero.");

        return new Budget(BudgetId.New(), departmentId, fiscalPeriod, allocatedAmount);
    }

    public void RecordExpense(string description, Money amount, DateTime incurredOnUtc)
    {
        ArgumentNullException.ThrowIfNull(amount);
        if (amount.Amount <= 0) throw new DomainException("Expense amount must be greater than zero.");
        if (!string.Equals(amount.Currency, AllocatedAmount.Currency, StringComparison.Ordinal))
            throw new DomainException("Expense currency must match budget currency.");
        if (!FiscalPeriod.Contains(incurredOnUtc))
            throw new DomainException("Expense date must be within the fiscal period.");

        var wasOverspent = SpentAmount.IsGreaterThan(AllocatedAmount);
        _expenses.Add(new BudgetExpenseLine(description, amount, incurredOnUtc));
        var spent = SpentAmount;

        if (!wasOverspent && spent.IsGreaterThan(AllocatedAmount))
            AddDomainEvent(new BudgetOverspentDomainEvent(Id, DepartmentId, spent, AllocatedAmount));
    }
}
