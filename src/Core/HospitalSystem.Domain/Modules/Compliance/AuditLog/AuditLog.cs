using HospitalSystem.Domain.Identififers;
using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Modules.Compliance.AuditLog
{
    public sealed class AuditLog : AggregateRoot<AuditLogId>
    {
        public string EntityType { get; private set; } = null!; // e.g. "Patient", "Invoice"
        public Guid EntityId { get; private set; }
        public AuditAction Action { get; private set; }
        public UserId PerformedByUserId { get; private set; } = null!;
        public string? ChangeSummary { get; private set; } // human-readable diff summary, not raw PII
        public DateTime OccurredOnUtc { get; private set; }

        private AuditLog() { }

        private AuditLog(AuditLogId id, string entityType, Guid entityId, AuditAction action, UserId performedByUserId, string? changeSummary) : base(id)
        {
            EntityType = entityType;
            EntityId = entityId;
            Action = action;
            PerformedByUserId = performedByUserId;
            ChangeSummary = changeSummary;
            OccurredOnUtc = DateTime.UtcNow;
        }

        public static AuditLog Record(string entityType, Guid entityId, AuditAction action, UserId performedByUserId, string? changeSummary = null)
        {
            if (string.IsNullOrWhiteSpace(entityType)) throw new DomainException("Entity type is required.");
            return new AuditLog(AuditLogId.New(), entityType.Trim(), entityId, action, performedByUserId, changeSummary?.Trim());
        }
        // Immutable once written — no mutating methods beyond creation, by design (audit trail).
    }
}
