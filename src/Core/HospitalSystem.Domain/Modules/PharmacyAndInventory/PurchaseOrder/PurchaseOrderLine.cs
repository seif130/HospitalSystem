using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.PurchaseOrder
{

    public sealed record PurchaseOrderLine(InventoryItemId InventoryItemId, int QuantityOrdered, Money UnitCost)
    {
        public int QuantityReceived { get; private set; }
        public Money LineTotal => UnitCost.Multiply(QuantityOrdered);
        internal void Receive(int quantity) => QuantityReceived += quantity;
        public bool IsFullyReceived => QuantityReceived >= QuantityOrdered;
    }
}
