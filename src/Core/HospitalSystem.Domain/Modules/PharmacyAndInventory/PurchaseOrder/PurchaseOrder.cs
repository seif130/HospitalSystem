using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.PurchaseOrder
{
    public sealed class PurchaseOrder : AggregateRoot<PurchaseOrderId>
    {
        public SupplierId SupplierId { get; private set; } = null!;
        public PurchaseOrderStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ExpectedDeliveryUtc { get; private set; }

        private readonly List<PurchaseOrderLine> _lines = new();
        public IReadOnlyCollection<PurchaseOrderLine> Lines => _lines.AsReadOnly();

        public Money Total => _lines.Aggregate(Money.Zero(), (sum, l) => sum.Add(l.LineTotal));

        private PurchaseOrder() { }

        private PurchaseOrder(PurchaseOrderId id, SupplierId supplierId, DateTime? expectedDeliveryUtc) : base(id)
        {
            SupplierId = supplierId;
            ExpectedDeliveryUtc = expectedDeliveryUtc;
            Status = PurchaseOrderStatus.Draft;
            CreatedOnUtc = DateTime.UtcNow;
        }

        public static PurchaseOrder CreateDraft(SupplierId supplierId, DateTime? expectedDeliveryUtc = null) =>
            new(PurchaseOrderId.New(), supplierId, expectedDeliveryUtc);

        public void AddLine(InventoryItemId inventoryItemId, int quantity, Money unitCost)
        {
            if (Status != PurchaseOrderStatus.Draft) throw new DomainException("Cannot add lines once the order is submitted.");
            if (quantity <= 0) throw new DomainException("Quantity must be greater than zero.");
            _lines.Add(new PurchaseOrderLine(inventoryItemId, quantity, unitCost));
        }

        public void Submit()
        {
            if (_lines.Count == 0) throw new DomainException("Cannot submit a purchase order with no lines.");
            Status = PurchaseOrderStatus.Submitted;
            RaiseDomainEvent(new PurchaseOrderSubmittedDomainEvent(Id, SupplierId, Total));
        }

        public void ReceiveLine(InventoryItemId inventoryItemId, int quantity)
        {
            if (Status is not (PurchaseOrderStatus.Submitted or PurchaseOrderStatus.PartiallyReceived))
                throw new DomainException("Order must be submitted before receiving stock against it.");
            var line = _lines.FirstOrDefault(l => l.InventoryItemId == inventoryItemId)
                ?? throw new DomainException("No matching line found on this purchase order.");
            line.Receive(quantity);
            Status = _lines.All(l => l.IsFullyReceived) ? PurchaseOrderStatus.Received : PurchaseOrderStatus.PartiallyReceived;
        }

        public void Cancel()
        {
            if (Status == PurchaseOrderStatus.Received) throw new DomainException("Cannot cancel a fully received order.");
            Status = PurchaseOrderStatus.Cancelled;
        }
    }
}
