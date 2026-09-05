using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Repositories;

internal sealed class VendorContractRepository(ProcurementDbContext context) : Repository<VendorContract, VendorContractId>(context), IVendorContractRepository
{
    public async Task<(IReadOnlyList<VendorContract> Items, int TotalCount)> GetByVendorAsync(VendorId vendorId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        var query = DbSet.AsNoTracking().Where(x => x.VendorId == vendorId).OrderByDescending(x => x.Term.Start);
        var total = await query.CountAsync(ct);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }
}
