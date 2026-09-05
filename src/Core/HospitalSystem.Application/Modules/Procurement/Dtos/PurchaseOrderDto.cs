using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.Dtos
{
    public sealed record PurchaseOrderDto(Guid Id, Guid VendorId, Guid? PurchaseRequestId,
        decimal TotalAmount, string Currency, PurchaseOrderStatus Status, IReadOnlyList<PurchaseOrderLineDto> Lines);

}
