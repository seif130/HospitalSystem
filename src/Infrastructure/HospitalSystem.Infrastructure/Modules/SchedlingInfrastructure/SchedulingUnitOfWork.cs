using HospitalSystem.Domain;
using HospitalSystem.Domain.Reprository;

namespace HospitalSystem.Infrastructure.Persistence;

internal sealed class SchedulingUnitOfWork(SchedulingDbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        db.SaveChangesAsync(cancellationToken);
}
