using HospitalSystem.Application.Models;
using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrdersByPurchaseRequestQuery
{
    public sealed class GetPurchaseOrdersByPurchaseRequestHandler(
        IPurchaseOrderRepository orders)
        : IQueryHandler<GetPurchaseOrdersByPurchaseRequestQuery, PaginatedList<PurchaseOrderDto>>
    {
        public async Task<Result<PaginatedList<PurchaseOrderDto>>> Handle(
            GetPurchaseOrdersByPurchaseRequestQuery request,
            CancellationToken cancellationToken)
        {
            var (ordersList, total) = await orders.GetByPurchaseRequestAsync(
                request.PurchaseRequestId,
                request.PageNumber,
                request.PageSize,
                cancellationToken);

            var items = ordersList
                .Select(order => order.ToDto())
                .ToList();

            var result = new PaginatedList<PurchaseOrderDto>(
                items,
                total,
                request.PageNumber,
                request.PageSize);

            return Result.Success(result);
        }
    }
}
