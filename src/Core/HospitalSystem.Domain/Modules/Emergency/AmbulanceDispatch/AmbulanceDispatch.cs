using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Emergency.AmbulanceDispatch.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Emergency.AmbulanceDispatch
{
    public sealed class AmbulanceDispatch : AggregateRoot<AmbulanceDispatchId>
    {
        public AmbulanceId AmbulanceId { get; private set; } = null!;
        public string PickupAddress { get; private set; } = null!;
        public PatientId? PatientId { get; private set; }
        public DispatchStatus Status { get; private set; }
        public DateTime RequestedOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }

        private AmbulanceDispatch() { }

        private AmbulanceDispatch(AmbulanceDispatchId id, AmbulanceId ambulanceId, string pickupAddress) : base(id)
        {
            AmbulanceId = ambulanceId;
            PickupAddress = pickupAddress;
            Status = DispatchStatus.Requested;
            RequestedOnUtc = DateTime.UtcNow;
        }

        public static AmbulanceDispatch Request(AmbulanceId ambulanceId, string pickupAddress)
        {
            if (string.IsNullOrWhiteSpace(pickupAddress)) throw new DomainException("Pickup address is required.");
            var dispatch = new AmbulanceDispatch(AmbulanceDispatchId.New(), ambulanceId, pickupAddress.Trim());
            dispatch.RaiseDomainEvent(new AmbulanceDispatchedDomainEvent(dispatch.Id, ambulanceId, pickupAddress));
            return dispatch;
        }

        public void MarkEnRouteToPickup() => Status = DispatchStatus.EnRouteToPickup;

        public void MarkPickedUp(PatientId patientId)
        {
            PatientId = patientId;
            Status = DispatchStatus.PickedUp;
        }

        public void MarkEnRouteToHospital() => Status = DispatchStatus.EnRouteToHospital;

        public void Complete()
        {
            Status = DispatchStatus.Completed;
            CompletedOnUtc = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == DispatchStatus.Completed) throw new DomainException("Cannot cancel a completed dispatch.");
            Status = DispatchStatus.Cancelled;
        }
    }
}
