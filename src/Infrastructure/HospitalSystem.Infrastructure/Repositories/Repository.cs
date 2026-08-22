using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.Reprository;
using HospitalSystem.Infrastructure.Contexts.DbContextsCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TId>: IRepository<TEntity, TId> where TEntity : AggregateRoot<TId>
    {
        protected readonly SchedulingDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(SchedulingDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(TId id,CancellationToken ct = default)
        {
            return await DbSet.FindAsync( new object?[] { id }, ct);
        }

        public virtual async Task AddAsync(TEntity entity,CancellationToken ct = default)
        {
            await DbSet.AddAsync(entity, ct);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }
    }

}
