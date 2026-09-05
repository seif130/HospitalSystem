using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;

namespace HospitalSystem.Domain.Modules.Procurement.PurchaseRequests.Contract;

public interface IPurchaseRequestRepository : IRepository<PurchaseRequest, PurchaseRequestId>
{
    Task<(IReadOnlyList<PurchaseRequest> Items, int TotalCount)> GetByDepartmentAsync(
        DepartmentId departmentId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}
