using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.PharmacyAndInventory.StockTransaction.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.StockTransaction
{
    public sealed class StockTransaction : AggregateRoot<StockTransactionId>
    {
        public InventoryItemId InventoryItemId { get; private set; } = null!;
        public StockTransactionType Type { get; private set; }
        public int Quantity { get; private set; }
        public string PerformedByStaffId { get; private set; } = null!;
        public string? Notes { get; private set; }
        public DateTime OccurredOnUtc { get; private set; }

        private StockTransaction() { }

        private StockTransaction(StockTransactionId id, InventoryItemId inventoryItemId, StockTransactionType type,
            int quantity, string performedByStaffId, string? notes) : base(id)
        {
            InventoryItemId = inventoryItemId;
            Type = type;
            Quantity = quantity;
            PerformedByStaffId = performedByStaffId;
            Notes = notes;
            OccurredOnUtc = DateTime.UtcNow;
        }

        public static StockTransaction Record(InventoryItemId inventoryItemId, StockTransactionType type,
            int quantity, string performedByStaffId, string? notes = null)
        {
            if (quantity == 0) throw new DomainException("Stock transaction quantity cannot be zero.");
            if (string.IsNullOrWhiteSpace(performedByStaffId)) throw new DomainException("Performing staff member is required.");
            return new StockTransaction(StockTransactionId.New(), inventoryItemId, type, quantity, performedByStaffId, notes?.Trim());
        }
    }
}
