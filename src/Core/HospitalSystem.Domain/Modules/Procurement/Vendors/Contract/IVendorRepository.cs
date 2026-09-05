using HospitalSystem.Domain.Identifiers;
using HospitalSystem.Domain.Reprository;

namespace HospitalSystem.Domain.Modules.Procurement.Vendors.Contract;

public interface IVendorRepository : IRepository<Vendor, VendorId>
{
    Task<bool> ExistsByNormalizedNameAsync(
        string normalizedName,
        VendorId? excludingId = null,
        CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<Vendor> Items, int TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);
}
