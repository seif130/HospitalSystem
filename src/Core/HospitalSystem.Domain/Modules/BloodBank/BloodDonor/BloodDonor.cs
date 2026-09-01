using HospitalSystem.Domain.Enums;
using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.BloodBank.BloodDonor
{
    public sealed class BloodDonor : AggregateRoot<BloodDonorId>
    {
        public PersonName Name { get; private set; } = null!;
        public BloodType BloodType { get; private set; }
        public PhoneNumber Phone { get; private set; } = null!;
        public DateTime? LastDonationOnUtc { get; private set; }
        public bool IsEligible { get; private set; } = true;

        private BloodDonor() { }

        private BloodDonor(BloodDonorId id, PersonName name, BloodType bloodType, PhoneNumber phone) : base(id)
        {
            Name = name;
            BloodType = bloodType;
            Phone = phone;
        }

        public static BloodDonor Register(PersonName name, BloodType bloodType, PhoneNumber phone) =>
            new(BloodDonorId.New(), name, bloodType, phone);

        public void RecordDonation()
        {
            if (!IsEligible) throw new DomainException("Donor is not currently eligible to donate.");
            LastDonationOnUtc = DateTime.UtcNow;
            // Standard whole-blood donation deferral is ~56 days; enforced by MarkIneligibleUntilNextCycle.
            IsEligible = false;
        }

        public void MarkEligible() => IsEligible = true;
    }

}
