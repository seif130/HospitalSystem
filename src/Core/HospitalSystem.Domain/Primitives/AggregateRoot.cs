using HospitalSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Primitives
{
    public abstract class AggregateRoot<TId> : BaseEntity<TId> where TId : notnull
    {
        protected AggregateRoot(TId id) : base(id) { }
        protected AggregateRoot() { }
    }
}
