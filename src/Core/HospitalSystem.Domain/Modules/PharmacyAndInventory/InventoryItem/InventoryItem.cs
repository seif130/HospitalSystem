using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.InventoryItem
{
    public sealed class InventoryItem : AggregateRoot<InventoryItemId>
    {
        public MedicationId? MedicationId { get; private set; }
        public string Sku { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public int QuantityOnHand { get; private set; }
        public int ReorderLevel { get; private set; }
        public SupplierId PrimarySupplierId { get; private set; } = null!;

        private InventoryItem() { }

        private InventoryItem(InventoryItemId id, string sku, string name, int reorderLevel, SupplierId supplierId, MedicationId? medicationId) : base(id)
        {
            Sku = sku;
            Name = name;
            ReorderLevel = reorderLevel;
            PrimarySupplierId = supplierId;
            MedicationId = medicationId;
            QuantityOnHand = 0;
        }

        public static InventoryItem Create(string sku, string name, int reorderLevel, SupplierId supplierId, MedicationId? medicationId = null)
        {
            if (string.IsNullOrWhiteSpace(sku)) throw new DomainException("SKU is required.");
            if (reorderLevel < 0) throw new DomainException("Reorder level cannot be negative.");
            return new InventoryItem(InventoryItemId.New(), sku.Trim(), name.Trim(), reorderLevel, supplierId, medicationId);
        }

        public void ReceiveStock(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Received quantity must be greater than zero.");
            QuantityOnHand += quantity;
        }

        public void DispenseStock(int quantity)
        {
            if (quantity <= 0) throw new DomainException("Dispensed quantity must be greater than zero.");
            if (quantity > QuantityOnHand) throw new DomainException("Cannot dispense more than the quantity on hand.");
            QuantityOnHand -= quantity;
            if (QuantityOnHand <= ReorderLevel)
                RaiseDomainEvent(new InventoryItemBelowReorderLevelDomainEvent(Id, QuantityOnHand, ReorderLevel));
        }
    }
}
