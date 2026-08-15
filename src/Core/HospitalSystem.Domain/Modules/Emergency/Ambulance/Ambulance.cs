using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Emergency.Ambulance.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Emergency.Ambulance
{
    public sealed class Ambulance : AggregateRoot<AmbulanceId>
    {
        public string PlateNumber { get; private set; } = null!;
        public AmbulanceStatus Status { get; private set; }

        private Ambulance() { }

        private Ambulance(AmbulanceId id, string plateNumber) : base(id)
        {
            PlateNumber = plateNumber;
            Status = AmbulanceStatus.Available;
        }

        public static Ambulance Register(string plateNumber)
        {
            if (string.IsNullOrWhiteSpace(plateNumber)) throw new DomainException("Plate number is required.");
            return new Ambulance(AmbulanceId.New(), plateNumber.Trim());
        }

        public void MarkDispatched()
        {
            if (Status != AmbulanceStatus.Available) throw new DomainException("Ambulance is not available for dispatch.");
            Status = AmbulanceStatus.Dispatched;
        }

        public void MarkEnRoute() => Status = AmbulanceStatus.EnRoute;

        public void ReturnToService()
        {
            if (Status == AmbulanceStatus.OutOfService) throw new DomainException("Cannot return an out-of-service ambulance without maintenance clearance.");
            Status = AmbulanceStatus.Available;
        }

        public void TakeOutOfService() => Status = AmbulanceStatus.OutOfService;
    }
}
