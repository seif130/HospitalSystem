namespace HospitalSystem.Domain.Identifiers;

public readonly record struct PurchaseRequestId(Guid Value)
{
    public static PurchaseRequestId New() => new(Guid.NewGuid());
    public bool IsEmpty => Value == Guid.Empty;
    public override string ToString() => Value.ToString();
}
