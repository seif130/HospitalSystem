using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Modules.Compliance.ConsentRecord.Enum;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace HospitalSystem.Domain.Modules.Compliance.ConsentRecord
{
    public sealed class ConsentRecord : AggregateRoot<ConsentRecordId>
    {
        public PatientId PatientId { get; private set; } = null!;
        public ConsentType Type { get; private set; }
        public ConsentStatus Status { get; private set; }
        public DateTime GrantedOnUtc { get; private set; }
        public DateTime? ExpiresOnUtc { get; private set; }
        public DateTime? WithdrawnOnUtc { get; private set; }
        public string WitnessedByStaffId { get; private set; } = null!;

        private ConsentRecord() { }

        private ConsentRecord(ConsentRecordId id, PatientId patientId, ConsentType type, DateTime? expiresOnUtc, string witnessedByStaffId) : base(id)
        {
            PatientId = patientId;
            Type = type;
            ExpiresOnUtc = expiresOnUtc;
            WitnessedByStaffId = witnessedByStaffId;
            Status = ConsentStatus.Granted;
            GrantedOnUtc = DateTime.UtcNow;
        }

        public static ConsentRecord Grant(PatientId patientId, ConsentType type, string witnessedByStaffId, DateTime? expiresOnUtc = null)
        {
            if (string.IsNullOrWhiteSpace(witnessedByStaffId)) throw new DomainException("Witnessing staff member is required.");
            var consent = new ConsentRecord(ConsentRecordId.New(), patientId, type, expiresOnUtc, witnessedByStaffId);
            consent.AddDomainEvent(new ConsentGrantedDomainEvent(consent.Id, patientId, type));
            return consent;
        }

        public void Withdraw()
        {
            if (Status != ConsentStatus.Granted) throw new DomainException("Only active consent can be withdrawn.");
            Status = ConsentStatus.Withdrawn;
            WithdrawnOnUtc = DateTime.UtcNow;
            AddDomainEvent(new ConsentWithdrawnDomainEvent(Id, PatientId, Type));
        }

        public void ExpireIfPastDate(DateTime asOfUtc)
        {
            if (Status == ConsentStatus.Granted && ExpiresOnUtc.HasValue && asOfUtc > ExpiresOnUtc.Value)
                Status = ConsentStatus.Expired;
        }

        public bool IsValidOn(DateTime moment) =>
            Status == ConsentStatus.Granted && (!ExpiresOnUtc.HasValue || moment <= ExpiresOnUtc.Value);
    }
}
