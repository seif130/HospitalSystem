using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Repositories;

internal sealed class PurchaseRequestRepository(ProcurementDbContext context) : Repository<PurchaseRequest, PurchaseRequestId>(context), IPurchaseRequestRepository
{
    public async Task<(IReadOnlyList<PurchaseRequest> Items, int TotalCount)> GetByDepartmentAsync(DepartmentId departmentId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        var query = DbSet.AsNoTracking().Where(x => x.DepartmentId == departmentId).OrderByDescending(x => x.Id.Value);
        var total = await query.CountAsync(ct);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }
}
