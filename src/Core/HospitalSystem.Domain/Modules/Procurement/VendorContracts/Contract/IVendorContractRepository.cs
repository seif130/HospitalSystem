using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;

namespace HospitalSystem.Domain.Modules.Procurement.VendorContracts.Contract;

public interface IVendorContractRepository : IRepository<VendorContract, VendorContractId>
{
    Task<(IReadOnlyList<VendorContract> Items, int TotalCount)> GetByVendorAsync(
        VendorId vendorId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}
