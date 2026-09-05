using HospitalSystem.WebApi.Endpoints.Modules.Procurement.Budgets;
using HospitalSystem.WebApi.Endpoints.Modules.Procurement.PurchaseOrders;
using HospitalSystem.WebApi.Endpoints.Modules.Procurement.PurchaseRequests;
using HospitalSystem.WebApi.Endpoints.Modules.Procurement.VendorContracts;
using HospitalSystem.WebApi.Endpoints.Modules.Procurement.Vendors;

namespace HospitalSystem.WebApi.Extensions;

public static class ProcurementWebApiExtensions
{
    public static IEndpointRouteBuilder MapProcurementEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapVendorEndpoints();
        app.MapVendorContractEndpoints();
        app.MapBudgetEndpoints();
        app.MapPurchaseRequestEndpoints();
        app.MapPurchaseOrderEndpoints();
        return app;
    }
}
