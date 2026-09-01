using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.AdministrationHrPayroll.RoomBed.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.AdministrationHrPayroll.RoomBed
{
    public sealed class RoomBed : AggregateRoot<RoomBedId>
    {
        public string WardName { get; private set; } = null!;
        public string BedNumber { get; private set; } = null!;
        public BedStatus Status { get; private set; }
        public PatientId? OccupiedByPatientId { get; private set; }

        private RoomBed() { }

        private RoomBed(RoomBedId id, string wardName, string bedNumber) : base(id)
        {
            WardName = wardName;
            BedNumber = bedNumber;
            Status = BedStatus.Available;
        }

        public static RoomBed Create(string wardName, string bedNumber)
        {
            if (string.IsNullOrWhiteSpace(wardName)) throw new DomainException("Ward name is required.");
            if (string.IsNullOrWhiteSpace(bedNumber)) throw new DomainException("Bed number is required.");
            return new RoomBed(RoomBedId.New(), wardName.Trim(), bedNumber.Trim());
        }

        public void Reserve(PatientId patientId)
        {
            if (Status != BedStatus.Available) throw new DomainException($"Bed {BedNumber} is not available (currently {Status}).");
            Status = BedStatus.Reserved;
            OccupiedByPatientId = patientId;
            AddDomainEvent(new RoomBedReservedDomainEvent(Id, patientId));
        }

        public void Occupy()
        {
            if (Status != BedStatus.Reserved) throw new DomainException("Bed must be reserved before it can be occupied.");
            Status = BedStatus.Occupied;
        }

        public void Release()
        {
            if (Status is BedStatus.Available) throw new DomainException("Bed is already available.");
            Status = BedStatus.Available;
            OccupiedByPatientId = null;
        }

        public void SendForMaintenance()
        {
            if (Status is BedStatus.Occupied or BedStatus.Reserved)
                throw new DomainException("Cannot send an occupied or reserved bed for maintenance.");
            Status = BedStatus.UnderMaintenance;
        }
    }

}
