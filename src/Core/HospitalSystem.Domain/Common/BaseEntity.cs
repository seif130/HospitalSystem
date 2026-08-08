using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Common
{

    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; protected set; } = default!;
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public string? CreatedBy { get; protected set; }
        public DateTime? LastModifiedAt { get; protected set; }
        public string? LastModifiedBy { get; protected set; }
        public bool IsDeleted { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
        public void RemoveDomainEvent(IDomainEvent domainEvent) => _domainEvents.Remove(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();

        public virtual void SoftDelete(string? deletedBy = null)
        {
            IsDeleted = true;
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = deletedBy;
        }

        public virtual void UndoDelete(string? restoredBy = null)
        {
            IsDeleted = false;
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = restoredBy;
        }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
