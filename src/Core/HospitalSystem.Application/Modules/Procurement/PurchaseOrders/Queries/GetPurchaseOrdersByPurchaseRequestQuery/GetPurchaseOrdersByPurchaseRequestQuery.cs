using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByPurchaseRequestQuery
{
    public sealed record GetPurchaseOrdersByPurchaseRequestQuery(PurchaseRequestId PurchaseRequestId, int PageNumber = 1, int PageSize = 20) : IQuery<PaginatedList<PurchaseOrderDto>>;

}
