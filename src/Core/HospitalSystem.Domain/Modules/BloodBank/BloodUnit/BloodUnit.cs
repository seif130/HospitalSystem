using HospitalSystem.Domain.Enums;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.BloodBank.BloodUnit.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.BloodBank.BloodUnit
{
    public sealed class BloodUnit : AggregateRoot<BloodUnitId>
    {
        public BloodDonorId DonorId { get; private set; } = null!;
        public BloodType BloodType { get; private set; }
        public decimal VolumeMl { get; private set; }
        public DateTime CollectedOnUtc { get; private set; }
        public DateTime ExpiresOnUtc { get; private set; }
        public BloodUnitStatus Status { get; private set; }

        private BloodUnit() { }

        private BloodUnit(BloodUnitId id, BloodDonorId donorId, BloodType bloodType, decimal volumeMl) : base(id)
        {
            DonorId = donorId;
            BloodType = bloodType;
            VolumeMl = volumeMl;
            CollectedOnUtc = DateTime.UtcNow;
            ExpiresOnUtc = CollectedOnUtc.AddDays(42); // standard whole-blood shelf life
            Status = BloodUnitStatus.Collected;
        }

        public static BloodUnit Collect(BloodDonorId donorId, BloodType bloodType, decimal volumeMl)
        {
            if (volumeMl <= 0) throw new DomainException("Volume must be greater than zero.");
            return new BloodUnit(BloodUnitId.New(), donorId, bloodType, volumeMl);
        }

        public void MarkTested()
        {
            if (Status != BloodUnitStatus.Collected) throw new DomainException("Unit must be freshly collected before testing.");
            Status = BloodUnitStatus.Tested;
        }

        public void MakeAvailable()
        {
            if (Status != BloodUnitStatus.Tested) throw new DomainException("Unit must pass testing before becoming available.");
            Status = BloodUnitStatus.Available;
        }

        public void Reserve()
        {
            if (Status != BloodUnitStatus.Available) throw new DomainException("Only an available unit can be reserved.");
            if (DateTime.UtcNow > ExpiresOnUtc) throw new DomainException("Cannot reserve an expired blood unit.");
            Status = BloodUnitStatus.Reserved;
        }

        public void MarkTransfused()
        {
            if (Status != BloodUnitStatus.Reserved) throw new DomainException("Unit must be reserved before transfusion.");
            Status = BloodUnitStatus.Transfused;
        }

        public void Discard(string reason)
        {
            if (Status == BloodUnitStatus.Transfused) throw new DomainException("Cannot discard an already-transfused unit.");
            Status = BloodUnitStatus.Discarded;
            AddDomainEvent(new BloodUnitDiscardedDomainEvent(Id, reason));
        }
    }
}
