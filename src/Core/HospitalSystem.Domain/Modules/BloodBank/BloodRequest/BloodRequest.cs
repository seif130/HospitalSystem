using HospitalSystem.Domain.Enums;
using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.BloodBank.BloodRequest
{
    public sealed class BloodRequest : AggregateRoot<BloodRequestId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public BloodType RequiredBloodType { get; private set; }
        public int UnitsRequested { get; private set; }
        public RequestUrgency Urgency { get; private set; }
        public BloodRequestStatus Status { get; private set; }

        private readonly List<BloodUnitId> _fulfilledUnitIds = new();
        public IReadOnlyCollection<BloodUnitId> FulfilledUnitIds => _fulfilledUnitIds.AsReadOnly();

        private BloodRequest() { }

        private BloodRequest(BloodRequestId id, PatientId patientId, BloodType requiredBloodType, int unitsRequested, RequestUrgency urgency) : base(id)
        {
            PatientId = patientId;
            RequiredBloodType = requiredBloodType;
            UnitsRequested = unitsRequested;
            Urgency = urgency;
            Status = BloodRequestStatus.Pending;
        }

        public static BloodRequest Create(PatientId patientId, BloodType requiredBloodType, int unitsRequested, RequestUrgency urgency)
        {
            if (unitsRequested <= 0) throw new DomainException("Units requested must be greater than zero.");
            var request = new BloodRequest(BloodRequestId.New(), patientId, requiredBloodType, unitsRequested, urgency);
            if (urgency == RequestUrgency.Emergency)
                request.RaiseDomainEvent(new EmergencyBloodRequestedDomainEvent(request.Id, patientId, requiredBloodType, unitsRequested));
            return request;
        }

        public void FulfillWithUnit(BloodUnitId unitId)
        {
            if (Status == BloodRequestStatus.Cancelled) throw new DomainException("Cannot fulfill a cancelled request.");
            if (_fulfilledUnitIds.Count >= UnitsRequested) throw new DomainException("Request is already fully fulfilled.");
            _fulfilledUnitIds.Add(unitId);
            Status = _fulfilledUnitIds.Count >= UnitsRequested ? BloodRequestStatus.Fulfilled : BloodRequestStatus.PartiallyFulfilled;
        }

        public void Cancel()
        {
            if (Status == BloodRequestStatus.Fulfilled) throw new DomainException("Cannot cancel a fully fulfilled request.");
            Status = BloodRequestStatus.Cancelled;
        }
    }
}
