using HospitalSystem.Domain.Identifiers;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HospitalSystem.Infrastructure.Modules.Procurement.Persistence;

internal static class StrongIdValueConverters
{
    public static ValueConverter<VendorId, Guid> VendorId() =>
        new(id => id.Value, value => new VendorId(value));

    public static ValueConverter<VendorContractId, Guid> VendorContractId() =>
        new(id => id.Value, value => new VendorContractId(value));

    public static ValueConverter<BudgetId, Guid> BudgetId() =>
        new(id => id.Value, value => new BudgetId(value));

    public static ValueConverter<PurchaseRequestId, Guid> PurchaseRequestId() =>
        new(id => id.Value, value => new PurchaseRequestId(value));

    public static ValueConverter<PurchaseOrderId, Guid> PurchaseOrderId() =>
        new(id => id.Value, value => new PurchaseOrderId(value));

    public static ValueConverter<DepartmentId, Guid> DepartmentId() =>
        new(id => id.Value, value => new DepartmentId(value));
}
