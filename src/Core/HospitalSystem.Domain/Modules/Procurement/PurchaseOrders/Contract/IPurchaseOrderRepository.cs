using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;

public interface IPurchaseOrderRepository : IRepository<PurchaseOrder, PurchaseOrderId>
{
    Task<(IReadOnlyList<PurchaseOrder> Items, int TotalCount)> GetByVendorAsync(
        VendorId vendorId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);

    Task<(IReadOnlyList<PurchaseOrder> Items, int TotalCount)> GetByPurchaseRequestAsync(
        PurchaseRequestId purchaseRequestId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}
