using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Common
{
    public abstract class BaseEntity<TId> : IEquatable<BaseEntity<TId>> where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        public TId Id { get; protected set; } = default!;

        // Audit Fields
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public string? CreatedBy { get; protected set; }
        public DateTime? LastModifiedAt { get; protected set; }
        public string? LastModifiedBy { get; protected set; }

        // Soft Delete
        public bool IsDeleted { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }
        public string? DeletedBy { get; protected set; }

        protected BaseEntity(TId id) => Id = id;
        protected BaseEntity() { } // Required by EF Core

        // Domain Events Management
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();

        // Soft Delete Behaviors
        public virtual void SoftDelete(string? deletedBy = null)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = deletedBy;
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = deletedBy;
        }

        public virtual void UndoDelete(string? restoredBy = null)
        {
            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = restoredBy;
        }

        // Equality Comparison
        public bool Equals(BaseEntity<TId>? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object? obj) => Equals(obj as BaseEntity<TId>);
        public override int GetHashCode() => (GetType(), Id).GetHashCode();

        public static bool operator ==(BaseEntity<TId>? a, BaseEntity<TId>? b) => a is null ? b is null : a.Equals(b);
        public static bool operator !=(BaseEntity<TId>? a, BaseEntity<TId>? b) => !(a == b);
    }
}
