using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Domain.Modules.Procurement.Budgets;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders;
using HospitalSystem.Domain.Modules.Procurement.PurchaseRequests;
using HospitalSystem.Domain.Modules.Procurement.VendorContracts;
using HospitalSystem.Domain.Modules.Procurement.Vendors;

namespace HospitalSystem.Application.Modules.Procurement;
public static class Mappings
{
    public static VendorDto ToDto(this Vendor x)
        =>new(x.Id.Value,x.Name,x.ContactEmail,x.ContactPhone,x.Status);
    public static VendorContractDto ToDto(this VendorContract x)
        =>new(x.Id.Value,x.VendorId.Value,x.Category,x.Term.Start,x.Term.End!.Value,x.ContractValue.Amount,x.ContractValue.Currency,x.Status);
    
    public static BudgetDto ToDto(this Budget x)
        =>new(x.Id.Value,x.DepartmentId.Value,x.FiscalPeriod.Start,x.FiscalPeriod.End!.Value,x.AllocatedAmount.Amount,x.SpentAmount.Amount,x.RemainingAmount.Amount,x.AllocatedAmount.Currency);
    public static PurchaseRequestDto ToDto(this PurchaseRequest x)
        =>new(x.Id.Value,x.DepartmentId.Value,x.Reason,x.Status,x.Lines.Select(l=>new PurchaseRequestLineDto(l.ItemName,l.Quantity,l.EstimatedUnitPrice.Amount,l.EstimatedTotal.Amount,l.EstimatedUnitPrice.Currency)).ToList());
    public static PurchaseOrderDto ToDto(this PurchaseOrder x)
        =>new(x.Id.Value,x.VendorId.Value,x.PurchaseRequestId?.Value,x.TotalAmount.Amount,x.TotalAmount.Currency,x.Status,x.Lines.Select(l=>new PurchaseOrderLineDto(l.ItemName,l.Quantity,l.UnitPrice.Amount,l.Total.Amount,l.UnitPrice.Currency)).ToList());
}
