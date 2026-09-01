using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.MultiFacility.Facility.Enums;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.MultiFacility.Facility
{
    public sealed class Facility : AggregateRoot<FacilityId>
    {
        public string Name { get; private set; } = null!;
        public FacilityType Type { get; private set; }
        public Address Address { get; private set; } = null!;
        public bool IsActive { get; private set; } = true;

        private Facility() { }

        private Facility(FacilityId id, string name, FacilityType type, Address address) : base(id)
        {
            Name = name;
            Type = type;
            Address = address;
        }

        public static Facility Create(string name, FacilityType type, Address address)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Facility name is required.");
            return new Facility(FacilityId.New(), name.Trim(), type, address);
        }

        public void Deactivate() => IsActive = false;
        public void Reactivate() => IsActive = true;
    }
}
