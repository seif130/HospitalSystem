using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Primitives
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime OccurredOnUtc { get; }
    }
}
