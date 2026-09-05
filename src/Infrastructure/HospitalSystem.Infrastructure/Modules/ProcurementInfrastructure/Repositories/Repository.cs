using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.Reprository;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Repositories;

public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId> where TId : notnull
{
    protected readonly ProcurementDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(ProcurementDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        DbSet = context.Set<TEntity>();
    }

    public virtual Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct = default) =>
        DbSet.FindAsync([id], ct).AsTask();

    public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        await DbSet.AddAsync(entity, ct);
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        DbSet.Update(entity);
    }

    public virtual void Remove(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        DbSet.Remove(entity);
    }
}
