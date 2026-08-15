using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.BloodBank.BloodTransfusion
{
    public sealed class BloodTransfusion : AggregateRoot<BloodTransfusionId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public BloodUnitId BloodUnitId { get; private set; } = null!;
        public string AdministeredByStaffId { get; private set; } = null!;
        public DateTime StartedOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }
        public bool HadAdverseReaction { get; private set; }
        public string? ReactionNotes { get; private set; }

        private BloodTransfusion() { }

        private BloodTransfusion(BloodTransfusionId id, PatientId patientId, BloodUnitId bloodUnitId, string administeredByStaffId) : base(id)
        {
            PatientId = patientId;
            BloodUnitId = bloodUnitId;
            AdministeredByStaffId = administeredByStaffId;
            StartedOnUtc = DateTime.UtcNow;
        }

        public static BloodTransfusion Start(PatientId patientId, BloodUnitId bloodUnitId, string administeredByStaffId)
        {
            if (string.IsNullOrWhiteSpace(administeredByStaffId)) throw new DomainException("Administering staff member is required.");
            return new BloodTransfusion(BloodTransfusionId.New(), patientId, bloodUnitId, administeredByStaffId);
        }

        public void Complete()
        {
            if (CompletedOnUtc.HasValue) throw new DomainException("Transfusion is already completed.");
            CompletedOnUtc = DateTime.UtcNow;
        }

        public void RecordAdverseReaction(string notes)
        {
            if (string.IsNullOrWhiteSpace(notes)) throw new DomainException("Reaction notes are required.");
            HadAdverseReaction = true;
            ReactionNotes = notes.Trim();
            RaiseDomainEvent(new TransfusionAdverseReactionDomainEvent(Id, PatientId, notes));
        }
    }

}
