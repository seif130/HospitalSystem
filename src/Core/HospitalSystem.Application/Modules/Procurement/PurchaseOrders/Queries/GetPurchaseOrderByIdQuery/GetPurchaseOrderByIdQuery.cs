using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Identifiers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrderByIdQuery
{
    public sealed record GetPurchaseOrderByIdQuery(PurchaseOrderId PurchaseOrderId) : IQuery<PurchaseOrderDto>;

}
