using HospitalSystem.Domain.Reprository;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence;

public sealed class ProcurementUnitOfWork(ProcurementDbContext context) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        context.SaveChangesAsync(ct);
}
