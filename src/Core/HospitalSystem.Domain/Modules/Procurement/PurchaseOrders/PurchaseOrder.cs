using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Enums;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Events;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;

public sealed class PurchaseOrder : AggregateRoot<PurchaseOrderId>
{
    public VendorId VendorId { get; private set; }
    public PurchaseRequestId? PurchaseRequestId { get; private set; }
    public Money TotalAmount { get; private set; } = null!;
    public PurchaseOrderStatus Status { get; private set; }

    private readonly List<PurchaseOrderLine> _lines = [];
    public IReadOnlyCollection<PurchaseOrderLine> Lines => _lines.AsReadOnly();

    private PurchaseOrder() { }

    private PurchaseOrder(PurchaseOrderId id, VendorId vendorId, PurchaseRequestId? purchaseRequestId, Money totalAmount) : base(id)
    {
        VendorId = vendorId;
        PurchaseRequestId = purchaseRequestId;
        TotalAmount = totalAmount;
        Status = PurchaseOrderStatus.Draft;
    }

    public static PurchaseOrder Create(VendorId vendorId, string currency = "USD", PurchaseRequestId? purchaseRequestId = null)
    {
        if (vendorId.IsEmpty) throw new DomainException("Vendor ID is required.");
        return new PurchaseOrder(PurchaseOrderId.New(), vendorId, purchaseRequestId, Money.Zero(currency));
    }

    public void AddLine(string itemName, int quantity, Money unitPrice)
    {
        EnsureStatus(PurchaseOrderStatus.Draft, "Only draft purchase orders can be modified.");
        ArgumentNullException.ThrowIfNull(unitPrice);
        if (!string.Equals(unitPrice.Currency, TotalAmount.Currency, StringComparison.Ordinal))
            throw new DomainException("Purchase order line currency must match order currency.");

        _lines.Add(new PurchaseOrderLine(itemName, quantity, unitPrice));
        RecalculateTotal();
    }

    public void Submit()
    {
        EnsureStatus(PurchaseOrderStatus.Draft, "Only draft purchase orders can be submitted.");
        if (_lines.Count == 0) throw new DomainException("Purchase order must contain at least one line.");
        Status = PurchaseOrderStatus.Submitted;
    }

    public void Approve()
    {
        EnsureStatus(PurchaseOrderStatus.Submitted, "Only submitted purchase orders can be approved.");
        Status = PurchaseOrderStatus.Approved;
        AddDomainEvent(new PurchaseOrderApprovedDomainEvent(Id));
    }

    public void Cancel()
    {
        if (Status is PurchaseOrderStatus.Completed or PurchaseOrderStatus.Cancelled)
            throw new DomainException("Purchase order cannot be cancelled.");
        Status = PurchaseOrderStatus.Cancelled;
    }

    public void Complete()
    {
        EnsureStatus(PurchaseOrderStatus.Approved, "Only approved purchase orders can be completed.");
        Status = PurchaseOrderStatus.Completed;
        AddDomainEvent(new PurchaseOrderCompletedDomainEvent(Id));
    }

    private void RecalculateTotal()
    {
        TotalAmount = _lines.Aggregate(Money.Zero(TotalAmount.Currency), (sum, line) => sum.Add(line.Total));
    }

    private void EnsureStatus(PurchaseOrderStatus expected, string message)
    {
        if (Status != expected) throw new DomainException(message);
    }
}
