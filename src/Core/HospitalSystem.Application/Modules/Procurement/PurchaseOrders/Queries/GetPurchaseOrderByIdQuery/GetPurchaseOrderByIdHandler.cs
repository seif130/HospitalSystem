using HospitalSystem.Application.Modules.Procurement.Dtos;
using HospitalSystem.Application.Shared.Common;
using HospitalSystem.Application.Shared.Messaging;
using HospitalSystem.Domain.Modules.Procurement.PurchaseOrders.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalSystem.Application.Modules.Procurement.PurchaseOrders.Queries.GetPurchaseOrderByIdQuery
{
    public sealed class GetPurchaseOrderByIdHandler(IPurchaseOrderRepository orders)
        : IQueryHandler<GetPurchaseOrderByIdQuery, PurchaseOrderDto>
    {
        public async Task<Result<PurchaseOrderDto>> Handle(
            GetPurchaseOrderByIdQuery request,CancellationToken cancellationToken)
        {
            var purchaseOrder = await orders.GetByIdAsync(
                request.PurchaseOrderId,
                cancellationToken);

            if (purchaseOrder is null)
            {
                return Result.Failure<PurchaseOrderDto>(
                    Error.NotFound("PurchaseOrder.NotFound",
                        "Purchase order was not found."));
            }

            return Result.Success(purchaseOrder.ToDto());
        }
    }
}
