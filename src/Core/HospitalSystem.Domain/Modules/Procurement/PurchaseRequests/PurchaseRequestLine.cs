using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;

public sealed record PurchaseRequestLine
{
    public string ItemName { get; }
    public int Quantity { get; }
    public Money EstimatedUnitPrice { get; }
    public Money EstimatedTotal => EstimatedUnitPrice.Multiply(Quantity);

    public PurchaseRequestLine(string itemName, int quantity, Money estimatedUnitPrice)
    {
        if (string.IsNullOrWhiteSpace(itemName)) throw new DomainException("Item name is required.");
        if (quantity <= 0) throw new DomainException("Quantity must be greater than zero.");
        ArgumentNullException.ThrowIfNull(estimatedUnitPrice);

        ItemName = itemName.Trim();
        Quantity = quantity;
        EstimatedUnitPrice = estimatedUnitPrice;
    }
}
