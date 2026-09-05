using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence.Repositories;

internal sealed class PurchaseOrderRepository(ProcurementDbContext context) : Repository<PurchaseOrder, PurchaseOrderId>(context), IPurchaseOrderRepository
{
    public async Task<(IReadOnlyList<PurchaseOrder> Items, int TotalCount)> GetByVendorAsync(VendorId vendorId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        var query = DbSet.AsNoTracking().Where(x => x.VendorId == vendorId).OrderByDescending(x => x.Id.Value);
        var total = await query.CountAsync(ct);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }

    public async Task<(IReadOnlyList<PurchaseOrder> Items, int TotalCount)> GetByPurchaseRequestAsync(PurchaseRequestId purchaseRequestId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        var query = DbSet.AsNoTracking().Where(x => x.PurchaseRequestId == purchaseRequestId).OrderByDescending(x => x.Id.Value);
        var total = await query.CountAsync(ct);
        var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
        return (items, total);
    }
}
