using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Primitives
{
    public abstract record DomainEvent : IDomainEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
    }
}
