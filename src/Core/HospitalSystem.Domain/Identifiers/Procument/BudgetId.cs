namespace HospitalSystem.Domain.Identifiers;

public readonly record struct BudgetId(Guid Value)
{
    public static BudgetId New() => new(Guid.NewGuid());
    public bool IsEmpty => Value == Guid.Empty;
    public override string ToString() => Value.ToString();
}
