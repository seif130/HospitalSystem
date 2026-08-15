using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.LabAndRadiology.Specimen.Enums;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.LabAndRadiology.Specimen
{
    public sealed class Specimen : AggregateRoot<SpecimenId>
    {
        public LabOrderId LabOrderId { get; private set; } = null!;
        public PatientId PatientId { get; private set; } = null!;
        public SpecimenType Type { get; private set; }
        public string CollectedByStaffId { get; private set; } = null!;
        public DateTime CollectedOnUtc { get; private set; }
        public SpecimenStatus Status { get; private set; }
        public string? RejectionReason { get; private set; }

        private Specimen() { }

        private Specimen(SpecimenId id, LabOrderId labOrderId, PatientId patientId, SpecimenType type, string collectedByStaffId) : base(id)
        {
            LabOrderId = labOrderId;
            PatientId = patientId;
            Type = type;
            CollectedByStaffId = collectedByStaffId;
            CollectedOnUtc = DateTime.UtcNow;
            Status = SpecimenStatus.Collected;
        }

        public static Specimen Collect(LabOrderId labOrderId, PatientId patientId, SpecimenType type, string collectedByStaffId)
        {
            if (string.IsNullOrWhiteSpace(collectedByStaffId)) throw new DomainException("Collecting staff member is required.");
            var specimen = new Specimen(SpecimenId.New(), labOrderId, patientId, type, collectedByStaffId);
            specimen.AddDomainEvent(new SpecimenCollectedDomainEvent(specimen.Id, labOrderId));
            return specimen;
        }

        public void MarkInTransit()
        {
            if (Status != SpecimenStatus.Collected) throw new DomainException("Only a collected specimen can be marked in transit.");
            Status = SpecimenStatus.InTransit;
        }

        public void MarkReceivedByLab()
        {
            if (Status != SpecimenStatus.InTransit) throw new DomainException("Specimen must be in transit before receipt.");
            Status = SpecimenStatus.ReceivedByLab;
        }

        public void Reject(string reason)
        {
            if (Status == SpecimenStatus.Processed) throw new DomainException("Cannot reject an already-processed specimen.");
            if (string.IsNullOrWhiteSpace(reason)) throw new DomainException("Rejection reason is required.");
            Status = SpecimenStatus.Rejected;
            RejectionReason = reason.Trim();
            AddDomainEvent(new SpecimenRejectedDomainEvent(Id, LabOrderId, reason));
        }

        public void MarkProcessed()
        {
            if (Status != SpecimenStatus.ReceivedByLab) throw new DomainException("Specimen must be received by the lab before processing.");
            Status = SpecimenStatus.Processed;
        }
    }
}
