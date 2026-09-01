using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.PharmacyAndInventory.Batch
{
    public sealed class Batch : AggregateRoot<BatchId>
    {
        public MedicationId MedicationId { get; private set; } = null!;
        public string LotNumber { get; private set; } = null!;
        public int Quantity { get; private set; }
        public DateTime ManufacturedOnUtc { get; private set; }
        public DateTime ExpiresOnUtc { get; private set; }
        public bool IsRecalled { get; private set; }

        private Batch() { }

        private Batch(BatchId id, MedicationId medicationId, string lotNumber, int quantity, DateTime manufacturedOnUtc, DateTime expiresOnUtc) : base(id)
        {
            MedicationId = medicationId;
            LotNumber = lotNumber;
            Quantity = quantity;
            ManufacturedOnUtc = manufacturedOnUtc;
            ExpiresOnUtc = expiresOnUtc;
        }

        public static Batch Register(MedicationId medicationId, string lotNumber, int quantity, DateTime manufacturedOnUtc, DateTime expiresOnUtc)
        {
            if (string.IsNullOrWhiteSpace(lotNumber)) throw new DomainException("Lot number is required.");
            if (quantity <= 0) throw new DomainException("Quantity must be greater than zero.");
            if (expiresOnUtc <= manufacturedOnUtc) throw new DomainException("Expiry date must be after the manufacture date.");
            return new Batch(BatchId.New(), medicationId, lotNumber.Trim(), quantity, manufacturedOnUtc, expiresOnUtc);
        }

        public bool IsExpired(DateTime asOfUtc) => asOfUtc >= ExpiresOnUtc;

        public void ConsumeQuantity(int quantity)
        {
            if (IsRecalled) throw new DomainException("Cannot dispense from a recalled batch.");
            if (quantity > Quantity) throw new DomainException("Cannot consume more than the batch quantity.");
            Quantity -= quantity;
        }

        public void Recall()
        {
            IsRecalled = true;
            AddDomainEvent(new BatchRecalledDomainEvent(Id, MedicationId, LotNumber));
        }
    }
}
