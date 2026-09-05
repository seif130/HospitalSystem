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
        public DateTime CreatedAt { get; protected set; }
        public string? CreatedBy { get; protected set; }

        public DateTime? LastModifiedAt { get; protected set; }
        public string? LastModifiedBy { get; protected set; }

        // Soft Delete
        public bool IsDeleted { get; protected set; }
        public DateTime? DeletedAt { get; protected set; }
        public string? DeletedBy { get; protected set; }


        // Domain Events
        public IReadOnlyCollection<IDomainEvent> DomainEvents
            => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            ArgumentNullException.ThrowIfNull(domainEvent);

            _domainEvents.Add(domainEvent);
        }

        protected void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }



        protected BaseEntity(TId id)
        {
            Id = id;
        }

        protected BaseEntity()
        {
            // Required by EF Core
        }

        // Soft Delete Behaviors

        public virtual void SoftDelete(string? deletedBy = null)
        {
            if (IsDeleted)
                return;

            var now = DateTime.UtcNow;

            IsDeleted = true;
            DeletedAt = now;
            DeletedBy = deletedBy;
            LastModifiedAt = now;
            LastModifiedBy = deletedBy;
        }

        public virtual void UndoDelete(string? restoredBy = null)
        {
            if (!IsDeleted)
                return;

            var now = DateTime.UtcNow;

            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;

            LastModifiedAt = now;
            LastModifiedBy = restoredBy;
        }

        // Equality

        public bool Equals(BaseEntity<TId>? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object? obj)
            => obj is BaseEntity<TId> other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(GetType(), Id);

        public static bool operator ==(
            BaseEntity<TId>? left,
            BaseEntity<TId>? right)
            => left is null ? right is null : left.Equals(right);

        public static bool operator !=(
            BaseEntity<TId>? left,
            BaseEntity<TId>? right)
            => !(left == right);
    }

}
