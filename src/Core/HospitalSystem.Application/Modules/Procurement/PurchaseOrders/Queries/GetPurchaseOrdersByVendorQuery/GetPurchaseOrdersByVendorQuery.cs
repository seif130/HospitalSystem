using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByVendorQuery
{
    public sealed record GetPurchaseOrdersByVendorQuery(VendorId VendorId, int PageNumber = 1, int PageSize = 20) : IQuery<PaginatedList<PurchaseOrderDto>>;

}
