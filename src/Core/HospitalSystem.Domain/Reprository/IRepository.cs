using HospitalSystem.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Domain.Reprository
{
    public interface IRepository<TAggregate, TId> where TAggregate : AggregateRoot<TId> where TId : notnull
    {
        Task<TAggregate?> GetByIdAsync(TId id,CancellationToken ct = default);

        Task AddAsync(TAggregate aggregate, CancellationToken ct = default);

        void Update(TAggregate aggregate);

        void Remove(TAggregate aggregate);
    }

}
