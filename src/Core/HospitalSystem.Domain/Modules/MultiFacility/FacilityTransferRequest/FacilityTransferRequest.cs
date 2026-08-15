using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.MultiFacility.FacilityTransferRequest.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.MultiFacility.FacilityTransferRequest
{
    public sealed class FacilityTransferRequest : AggregateRoot<FacilityTransferRequestId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public FacilityId FromFacilityId { get; private set; } = null!;
        public FacilityId ToFacilityId { get; private set; } = null!;
        public string Reason { get; private set; } = null!;
        public TransferRequestStatus Status { get; private set; }
        public DateTime RequestedOnUtc { get; private set; }

        private FacilityTransferRequest() { }

        private FacilityTransferRequest(FacilityTransferRequestId id, PatientId patientId, FacilityId fromFacilityId,
            FacilityId toFacilityId, string reason) : base(id)
        {
            PatientId = patientId;
            FromFacilityId = fromFacilityId;
            ToFacilityId = toFacilityId;
            Reason = reason;
            Status = TransferRequestStatus.Requested;
            RequestedOnUtc = DateTime.UtcNow;
        }

        public static FacilityTransferRequest Create(PatientId patientId, FacilityId fromFacilityId, FacilityId toFacilityId, string reason)
        {
            if (fromFacilityId == toFacilityId) throw new DomainException("Origin and destination facility cannot be the same.");
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Transfer reason is required.");
            var request = new FacilityTransferRequest(FacilityTransferRequestId.New(), patientId, fromFacilityId, toFacilityId, reason.Trim());
            request.AddDomainEvent(new FacilityTransferRequestedDomainEvent(request.Id, patientId, fromFacilityId, toFacilityId));
            return request;
        }

        public void Accept()
        {
            if (Status != TransferRequestStatus.Requested) throw new DomainException("Only a requested transfer can be accepted.");
            Status = TransferRequestStatus.Accepted;
        }

        public void MarkInTransit()
        {
            if (Status != TransferRequestStatus.Accepted) throw new DomainException("Transfer must be accepted before it is in transit.");
            Status = TransferRequestStatus.InTransit;
        }

        public void Complete()
        {
            if (Status != TransferRequestStatus.InTransit) throw new DomainException("Transfer must be in transit before completion.");
            Status = TransferRequestStatus.Completed;
        }

        public void Reject(string reason)
        {
            if (Status != TransferRequestStatus.Requested) throw new DomainException("Only a requested transfer can be rejected.");
            Status = TransferRequestStatus.Rejected;
            Reason = reason.Trim();
        }

        public void Cancel()
        {
            if (Status is TransferRequestStatus.Completed) throw new DomainException("Cannot cancel a completed transfer.");
            Status = TransferRequestStatus.Cancelled;
        }
    }
}
