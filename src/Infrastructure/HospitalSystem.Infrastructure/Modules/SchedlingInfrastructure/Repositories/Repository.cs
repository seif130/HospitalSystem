using HospitalSystem.Domain;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Domain.Reprository;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Persistence.Repositories;

internal class Repository<TEntity, TId>(SchedulingDbContext db) : IRepository<TEntity, TId>
    where TEntity : AggregateRoot<TId>
    where TId : notnull
{
    protected SchedulingDbContext Db { get; } = db;
    protected DbSet<TEntity> Set { get; } = db.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct = default) =>
        await Set.FindAsync([id], ct);

    public async Task AddAsync(TEntity entity, CancellationToken ct = default) =>
        await Set.AddAsync(entity, ct);

    public void Update(TEntity entity) => Set.Update(entity);

    public void Remove(TEntity entity) => Set.Remove(entity);
}
